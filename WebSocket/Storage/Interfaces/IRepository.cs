using NHibernate;
using Shared.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Interfaces
{
    public interface IRepository<T> where T : PersistentObject, new()
    {
        T Load(object nodeId);

        T Get(long Id);

        IQueryable<T> GetAll();

        ISQLQuery ExecuteSqlQuery(string sqlQuery);

        void Add(T entity);

        void Update(T entity);

        void Update(T entity, long id);

        void Remove(T entity);
    }
}
