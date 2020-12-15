
namespace Exmark.Business
{
    using System.Collections.Generic;

    internal interface IExmarkService<TEntity>
    {
        List<TEntity> GetAll();
        List<TEntity> GetCategorized(string categorizedName);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
