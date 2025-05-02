using System.Linq.Expressions;
using InventoryTracker.Models;

namespace InventoryTracker.Data
{
    public interface IGeneralRepo<T> where T : BaseModel
    {
        void Add(T Item);
        void Update(T Item);
        void Delete(T Item);
        void Remove(int Id);
        void Save();
        T GetByID(int Id);
        IQueryable<T> GetAll();
        IQueryable<T> GetFilter(Expression<Func<T, bool>> expression);
    }
}
