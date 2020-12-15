using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    public class ETradeContext:DbContext
    {
        /*
         
        ENTITY FRAMEWORK ÇALIŞMA ŞEKLİ
           
        DbContext 'ten kalıtım alan sınıfımızın ismi önemlidir ETradeContext
            
            
        Proje içerisinde App.config 'e gidin. configuration içerisine connectionStrings eklenecek ve gerekli bilgiler verilecektir.

        ETradeContext class ismi verilir varsayılan olarak projede onu aramaktadır.

        <connectionStrings>
         <add name="ETradeContext" 
         connectionString=
         "server=(localdb)\mssqllocaldb;initial catalog=ETrade;integrated security=true"
         providerName="System.Data.SqlClient"/>
         </connectionStrings>

        server=(localdb) yerel veritabanları
        initial catalog=database ismi
        integrated security güvenli bağlantı
        providerName çalışma için gerekli kütüphanesi

         */

        public DbSet<Product> Products { get; set; }
    }
}
