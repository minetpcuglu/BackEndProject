﻿using DataAccessLayer.Context;
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
        private DbSet<T> _table;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this._table = _context.Set<T>();
        }
        public async Task insert(T t) => await _table.AddAsync(t);
        public void Delete(T t) =>  _table.Remove(t); //** 
        public async Task Get(Expression<Func<T, bool>> filter)
        {
            await _table.Where(filter).ToListAsync();
            
        }
        public async Task<List<T>> GetAll() => await _table.ToListAsync();

        public async Task GetById(int id) => await _table.FindAsync(id);

        public async Task<List<T>> GetListAll(Expression<Func<T, bool>> filter) => await _table.Where(filter).ToListAsync();

        public void Update(T t) => _context.Entry(t).State = EntityState.Modified;  //********

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
    }
}
