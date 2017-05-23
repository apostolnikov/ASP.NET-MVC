using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        T GetById(object id);
        void Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
