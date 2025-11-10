using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.DLL.Interfaces.IBase
{
    public interface IGenericRepository<T> where T : class
    {
        public Task Add(T entity);
        public Task Add(IEnumerable<T> entities);
        public Task Delete(IEnumerable<T> entities);
        public Task Update(T entity);
        public Task Update(IEnumerable<T> entities);
        public Task<IEnumerable<T>> Get();
        public Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        public Task Delete(T entity);
    }
}
