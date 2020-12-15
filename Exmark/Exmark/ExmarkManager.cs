
namespace Exmark
{
    using Exmark.Business;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Exmark.Entity;
    using System.Linq;
    using System.Collections.ObjectModel;

    public class ExmarkManager<TEntity> : IExmarkService<TEntity> where TEntity : class, XEntity, new()
    {
        StatusAbout sa;
        CategoryAbout ca;
        MapAbout ma;

        public ExmarkManager()
        {
            sa = new StatusAbout();
            ca = new CategoryAbout();
            ma = new MapAbout();
            _workOnTheMap = false;
        }

        // Kullanıcı varlığın adı dışında farklı bir isimle çalışmak isteyebilir.
        private string SetName { get; set; }
        private string SetRoot { get; set; }

        public void SetEntityName(string name)
        => SetName = name;

        public void SetEntityName(string name, string root)
        {
            SetName = name;
            SetRoot = root;
        }

        // Yapım aşamasında: verilen varlığın tek olması, tek veri varındırması. Örn: Ayarlar.
        private bool Single { get; set; } = false;

        // Dosya hazırda varsa içindeki yönergelere göre çalışır. Yoksa bir kereye mahsus oluşturulur ve entity eklenir. (File-Entity)
        // True olduğu takdirde harici manager tanımlamaları dikkate alınmayacaktır.
        // Her entity için tanım bilgileri ayrı tutulur. Nerede çalışılacağı, otomatik id vb. gibi durumlar da dahil.
        // - Bunun sebebi entity' ler için farklı özellikler tanımlanmak istenmesi.
        // Bir özelliğin değiştirilmesi istendiğinde ayar false durumuna getirilerek, entity'in silinmesi sağlanır.
        // Daha sonra istenilen özellikler verilerek tekrar true durumuna getirilere eklenmesi sağlanır.
        // Diğer ayarları içerisinde barındırdığı için diğer configuration dosyaları arasında en güçlü dosyadır.
        // + Recent dosyasını içeren Status bilgilerini buraya dahil edemem. Çünkü map dosyasında entity bilgilerinin
        // silinmesi gibi durumlarda bu bilgi gidecektir.

        // True: Verilen Id'ye göre işlem gerçekleştir. False: Verilen id'yi dikkate alma, otomatikleştir.
        // False durumunda, ekleme ve silme işlemlerinde sorunlar doğurabilir.
        public bool AutomaticIdOnAdd { get; set; } = true; // İsimleri kontrol et, Genel.

        // True: Haritada varsa ekle, yoksa ekleme. False: Yine de ekle, yoksa tanımla. Varsayılan: false
        public bool CheckBeforeAdding { get; set; } = false;

        // Harici çalışılmak istenen bilgisayar yolu
        public string ExternalLocation { get; set; } = "";

        // Güncelleme: bu özellik gereksiz görüldüğü için kullanıma kapatıldı. Belki daha sonra kaldırılabilir.
        // True: Kategori ismini yoksa tanımla. (GetAll aktif olur) False: Kategori ismi tanımlanmaz, GetAll çalışmaz.
        private bool CategoryMapOption { get; set; } = true;

        private PropertyInfo _categoryProperty = null;
        public string CategoryProperty
        {
            get
            {
                return _categoryProperty.Name;
            }
            set
            {
                try
                {
                    _categoryProperty = new TEntity().GetType().GetProperty(value.ToString());
                }
                catch (Exception)
                {

                }
            }
        }

        private bool _workOnTheMap;
        public bool WorkOnTheMap
        {
            get { return _workOnTheMap; }
            set
            {
                _workOnTheMap = value;

                EMap map = new EMap();
                map.Entity = new TEntity().GetType().Name;
                map.ExternalLocation = ExternalLocation;

                if (WorkOnTheMap == false)
                {
                    ma.Delete(map); // Varsa Sil
                }
                else
                {
                    map.Single = Single;
                    map.AutomaticId = AutomaticIdOnAdd;
                    map.CheckBeforeAdding = CheckBeforeAdding;
                    if (!(_categoryProperty is null))
                        map.CategoryProperty = CategoryProperty;
                    map.Name = SetName;
                    map.Root = SetRoot;
                    ma.Create(map); // Yoksa Oluştur
                }
            }
        }

        private void BringInstruction()
        {
            EMap map = new EMap();
            // Farklı bir lokasyonda çalışan veriler için programa komut vermek gerekiyor.
            // Lokal veriler için buna gerek yoktur.
            map = ma.Get(ExternalLocation, new TEntity().GetType().Name);

            if (!(map is null))
            {
                SetName = map.Name;
                SetRoot = map.Root;
                Single = map.Single;
                AutomaticIdOnAdd = map.AutomaticId;
                CheckBeforeAdding = map.CheckBeforeAdding;
                ExternalLocation = map.ExternalLocation;
                CategoryProperty = map.CategoryProperty;
            }
        }

        public List<TEntity> GetAll()
        {
            if (WorkOnTheMap == true)
                BringInstruction();

            if (CategoryMapOption == true)
            {
                ca.UpdateList(ExternalLocation);

                DEntity d = new DEntity();
                Envoy envoy = new Envoy();

                d.Name = SetName;
                d.Root = SetRoot;
                d.TargetLocation = ExternalLocation;

                TEntity t = new TEntity();

                List<TEntity> allList = new List<TEntity>();

                if (ca.s.Count > 0)
                {

                    foreach (var item in ca.s)
                    {
                        d.CategorizeName = item.Name;
                        d = envoy.Before(t, d);
                        foreach (var item2 in envoy.DataToList<TEntity>(d))
                        {
                            allList.Add(item2);
                        }
                    }
                }
                else // Tekil dosyalar için önemli
                {
                    d = envoy.Before(t, d);
                    foreach (var item2 in envoy.DataToList<TEntity>(d))
                    {
                        allList.Add(item2);
                    }
                }
                return allList;
            }
            else
                return null;
        }

        public List<TEntity> GetCategorized(string categorizedName)
        {
            if (WorkOnTheMap == true)
                BringInstruction();

            DEntity d = new DEntity();
            Envoy envoy = new Envoy();
            d.Name = SetName;
            d.Root = SetRoot;
            d.CategorizeName = categorizedName;
            d.TargetLocation = ExternalLocation;

            TEntity t = new TEntity();
            d = envoy.Before(t, d);
            return envoy.DataToList<TEntity>(d);
        }

        public void Add(TEntity e)
        {
            if (WorkOnTheMap == true)
                BringInstruction();

            Envoy envoy = new Envoy();
            DEntity d = new DEntity();

            if (!(_categoryProperty is null))
                d.CategorizeName = CategoryProperty;

            d.Name = SetName;
            d.Root = SetRoot;
            d.TargetLocation = ExternalLocation;

            sa.Create(ExternalLocation, e.GetType());

            sa.UpdateList(ExternalLocation);

            EStatus st = sa.IsThereAny(e.GetType().Name);

            if (AutomaticIdOnAdd)
                e.GetType().GetProperty(envoy.FindKey(e)).SetValue(e, st.Recent + 1);

            bool IsAdded = false;
            bool IsDefine = false;

            if (CheckBeforeAdding) // Eklemeden önce kategori kontrol etme durumu
            {
                ca.UpdateList(ExternalLocation);
                if (ca.IsThereAny(d.CategorizeName) == true)
                {
                    d = envoy.Before(e, d);
                    envoy.CreateIfNotThere(d);
                    envoy.DataInsert(e, d);

                    IsAdded = true;
                    IsDefine = true;
                }
                else
                {
                    // Şarta bağlı kılınarak ekleme durumu tanımlanmış.
                    // Ancak hangi property üzerinden çalışılacağı söylenmediğinde,
                    // Ekleme yapılmaz. Çünkü seçenek açıktır ve durum verilmemiştir.
                }
            }
            else
            {
                d = envoy.Before(e, d);
                envoy.CreateIfNotThere(d);
                envoy.DataInsert(e, d);

                IsAdded = true;
                IsDefine = true;
            }

            if (CategoryMapOption && IsDefine) // Haritaya kategorileri ekleyen kısım
            {
                if (!string.IsNullOrWhiteSpace(d.CategorizeName))
                    ca.Add(ExternalLocation, d.CategorizeName);
            }

            // Define hakkında;
            // CheckBeforeAdding açık olmasına rağmen, listede bulunamadığında, CategoryMapOption açık olduğunda
            // Yine de Map'e ekleme oluşuyor. Kısaca; CBE bloğunda ki else kısmı burayı etkilemiyor.
            // Bunu düzeltmek için,
            // Kontrol kapalıysa ve kontrol sonrasında kategori listede bulunmuşsa, map'e ekleme işlemi gerçekleşiyor.

            if (IsAdded && AutomaticIdOnAdd)
                sa.Add(ExternalLocation, e.GetType());
        }

        public void Update(TEntity e)
        {
            if (WorkOnTheMap == true)
                BringInstruction();

            DEntity d = new DEntity();
            Envoy envoy = new Envoy();

            if (!(_categoryProperty is null))
                d.CategorizeName = CategoryProperty;

            d.Name = SetName;
            d.Root = SetRoot;
            d.TargetLocation = ExternalLocation;

            d = envoy.Before(e, d);
            envoy.DataUpdate(e, d);
        }

        public void Delete(TEntity e)
        {
            if (WorkOnTheMap == true)
                BringInstruction();

            DEntity d = new DEntity();
            Envoy envoy = new Envoy();

            if (!(_categoryProperty is null))
                d.CategorizeName = CategoryProperty;

            d.Name = SetName;
            d.Root = SetRoot;
            d.TargetLocation = ExternalLocation;

            d = envoy.Before(e, d);
            envoy.DataDelete(e, d);
        }

    }
}
