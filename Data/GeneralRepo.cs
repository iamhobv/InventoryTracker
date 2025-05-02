using System.Linq.Expressions;
using InventoryTracker.Models;

namespace InventoryTracker.Data
{
    public class GeneralRepo<T> : IGeneralRepo<T> where T : BaseModel
    {
        private readonly DataBaseContext context;

        public GeneralRepo(DataBaseContext context)
        {
            this.context = context;
        }
        public void Add(T Item)
        {
            context.Set<T>().Add(Item);
        }

        public IQueryable<T> GetFilter(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetByID(int Id)
        {
            return GetFilter(x => x.ID == Id).FirstOrDefault();
        }

        public void Remove(int Id)
        {
            T entity = GetByID(Id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
        }

        public void Update(T Item)
        {
            context.Set<T>().Update(Item);
        }
        public void Delete(T Item)
        {
            context.Set<T>().Remove(Item);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
