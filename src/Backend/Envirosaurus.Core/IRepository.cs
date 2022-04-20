using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Envirosaurus
{
    public interface IRepository<T> where T : IGUIDable
    {
        Guid Insert(T entity);
        IList<Guid> Insert(IList<T> entity);
        void Update(T entity);
        void Delete(T entity);
        IList<T> Find(Expression<Func<T, bool>> predicate);
        IList<T> Find(Expression<Func<T, bool>> predicate, int limit, Expression<Func<T, object>> orderby);
        IList<T> Find(Expression<Func<T, bool>> predicate, int limit, Expression<Func<T, object>> orderby, bool orderdescending);
        IList<T> GetAll();
        T GetById(Guid id);
        T GetById(string id);
    }
}