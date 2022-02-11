using EntityLayer.Entities.Interface;
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
        Task GetById(int id);


  
    }
}
