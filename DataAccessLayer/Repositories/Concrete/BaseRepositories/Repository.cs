using DataAccessLayer.Repositories.Interface.BaseRepositories;
using EntityLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete.BaseRepositories
{
    public abstract class Repository<T> : IBaseRepositories<T> where T : class, IBaseEntity// => "IRepository"'de yazdığımız methodlara burada gövde kazandıracağız ve abstract olarak işaretlediğim "BaseRepository" sınıfını child sınıflarda çağıracağım.
    {
        public Task Delete(T t)
        {
            throw new NotImplementedException();
        }

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task Get(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAll(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task insert(T t)
        {
            throw new NotImplementedException();
        }

        public Task Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
