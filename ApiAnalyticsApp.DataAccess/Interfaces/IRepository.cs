using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
