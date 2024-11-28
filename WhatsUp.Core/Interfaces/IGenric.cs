using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Interfaces
{
    public interface IGenric<T> where T : class
    {
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        IEnumerable<T> GetAll(string[] includes = null);
        Task<IEnumerable<T>> GetAllAsync();
        T Find(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        //IEnumerable<T> AddRange(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);
        //void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        //void AttachRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    }
}
