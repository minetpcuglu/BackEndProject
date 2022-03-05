using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface.BaseRepositories;
using EntityLayer.Entities.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            _table = _context.Set<T>();
        }

        public async Task Insert(T t) => await _table.AddAsync(t);
        public void Delete(T t) =>  _table.Remove(t); //** 

        public async Task<T> Get(Expression<Func<T, bool>> filter) => await _table.Where(filter).FirstOrDefaultAsync();
        //public async Task<T> Get(Expression<Func<T, bool>> filter)
        //{
        //    await _table.Where(filter).FirstOrDefaultAsync();
            
        //}
        public async Task<List<T>> GetAll() => await _table.ToListAsync();

        public async Task<T> GetById(int id) => await _table.FindAsync(id);

        public async Task<List<T>> GetListAll(Expression<Func<T, bool>> filter) => await _table.Where(filter).ToListAsync();

    
        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                          Expression<Func<T, bool>> expression = null,
                                                          Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                          bool disableTracking = true, int pageIndex = 1, int pageSize = 3)
        {
            IQueryable<T> query = _table;
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            if (orderby != null) return await orderby(query).Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            else return await query.Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        //public async Task Update(T t) =>  _context.Entry(t).State = EntityState.Modified; //********

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,// => ilk paramatre Entity tipince olacak ikinci aldığı parametre ise dönüş tipide TResult olacak.
                                                               Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, // ilk parametredeki verinin bool tipinde dönmesini sağlayacaktır.
                                                               IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, // öğeleri bir düzene göre sıralar.
                                                               IIncludableQueryable<T, object>> inculude = null,// Tanımlanan öğeyi içerip içermediğini kontrol eder.
                                                               bool disableTracking = true)
        {
            IQueryable<T> query = _table; //=> Sorgu geldikçe Db den ye gidip gelecek.

            if (disableTracking) query = query.AsNoTracking(); // => disableTracking varlık üzerinde ki değişiklikleri kontrol edip Save'e gönderiyoru. Biz filtreleme yaptığımızdan filtreleyip gönderiyoruz. Burada disableTracking'e gerek olmadığı için kapattık.
            if (inculude != null) query = inculude(query); // => include edilen nesneleri query'e attık.
            if (expression != null) query = query.Where(expression); // => expression ile gelenleri linq to sorgusu yazılması için Where sorgusunu query'e attık.
            if (orderby != null) return await orderby(query).Select(selector).FirstOrDefaultAsync(); // => Gelen orderby sorgusu dolu ise bu şart çalışacak.
            else return await query.Select(selector).FirstOrDefaultAsync(); // => şayet Null geliyorsa da bu satır çalışsın.
        }

        //public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        //{
        //    IQueryable<T> query = _table;
        //    if (include != null)
        //    {
        //        query = include(query);
        //    }
        //    if (expression != null)
        //    {
        //        query = query.Where(expression);
        //    }
        //    if (orderBy != null)
        //    {
        //        return await orderBy(query).Select(selector).FirstOrDefaultAsync();
        //    }
        //    else
        //    {
        //        return await query.Select(selector).FirstOrDefaultAsync();
        //    }
        //}


        public async Task Update(T t)
        {
            _table.Update(t);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await _table.Where(expression).FirstOrDefaultAsync();
    }
}
