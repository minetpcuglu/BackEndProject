using EntityLayer.Entities.Interface;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interface.BaseRepositories
{
    // Repository: Temel olarak veritabanı sorgulama işlemlerinin bir merkezden yapılmasını sağlar kod tekrarını önler.
    public interface IBaseRepositories<T> where T : class, IBaseEntity
    {
        Task Insert(T t);
        void Delete(T t);
        Task Update(T t);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAll(); // Asenkron programlama yapmak istediğimiz methodlarımızı "TASK" olarak işaretlenir.
        Task<List<T>> GetListAll(Expression<Func<T, bool>> filter);  //filter 
        Task<T> Get(Expression<Func<T, bool>> filter);  //dışarıdann bir şart alıcak

        //Task<bool> Any(Expression<Func<T, bool>> expression);
        //Task<T> FirstOrDefault(Expression<Func<T, bool>> filter);

        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                     Expression<Func<T, bool>> expression = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                     bool disableTracing = true,
                                                     int pageIndex = 1,
                                                     int pageSize = 3);


        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, /*** samet*/
                                                       Expression<Func<T, bool>> expression = null,
                                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                       Func<IQueryable<T>, IIncludableQueryable<T, object>> inculude = null,
                                                       bool disableTracking = true);

        ////normally unitofwork dont prefer getquery. But I am learning step by step . so I used here .
        //Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
        //                                                 Expression<Func<T, bool>> expression,
        //                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //                                                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> GetById(int id);
    }
}
