using CompanyData.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyData.Services.Repositor
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private CompanyDataDbContext context = null;
        private DbSet<T> table = null;
        public GenericRepository(CompanyDataDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
       
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public async Task Create(T obj)
        {
            table.Add(obj);
            await Save();
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

    }
}
