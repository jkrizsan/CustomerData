using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Services.Repositor
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        Task Create(T obj);
        void Update(T obj);
        void Delete(object id);
        Task Save();
    }

}
