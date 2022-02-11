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
        Task insert(T t);
        void Delete(T t);
        void Update(T t);
        Task<List<T>> GetAll(); // Asenkron programlama yapmak istediğimiz methodlarımızı "TASK" olarak işaretlenir.
        Task<List<T>> GetListAll(Expression<Func<T, bool>> filter);  //filter 
        Task Get(Expression<Func<T, bool>> filter);  //dışarıdann bir şart alıcak

        //Task<bool> Any(Expression<Func<T, bool>> expression);
        //Task<T> FirstOrDefault(Expression<Func<T, bool>> filter);

        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                     Expression<Func<T, bool>> expression = null,
                                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                     bool disableTracing = true,
                                                     int pageIndex = 1,
                                                     int pageSize = 3);
        Task GetById(int id);


  
    }
}
