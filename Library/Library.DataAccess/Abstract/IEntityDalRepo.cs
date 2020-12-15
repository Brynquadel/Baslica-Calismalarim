
namespace Library.DataAccess.Abstract
{
    using Library.Entity.Abstract;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IEntityDalRepo<T> where T : class, IEntity, new()
    {
        List<T> GetAll();
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
