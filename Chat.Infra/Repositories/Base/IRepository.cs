using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Infra.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid id);
    }
} 