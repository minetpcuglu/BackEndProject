using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BaseServices.Interface
{
  public interface IBaseService<T> 
    {
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task<T> GetById(int id);
        Task Update(T entity);
        Task Delete(int id);
    }
}
