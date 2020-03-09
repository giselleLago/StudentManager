using System.Collections.Generic;

namespace StudentManager.Common.Seedwork
{
    public interface IRepository<T> where T: Entity
    {
        IEnumerable<T> GetAll();
        T Create(T entity);
        T Update(T entity);
        T DeleteById(int id);
    }
}
