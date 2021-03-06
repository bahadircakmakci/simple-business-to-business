using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.Base;
using simple_business_to_business.InfrastructureLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.InfrastructureLayer.Repositories.Concrete.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> table;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }


        public async Task Add(T entity) => await table.AddAsync(entity);

        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await table.AnyAsync(expression);

        public void Delete(T entity) => table.Remove(entity);

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await table.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression) => await table.AsNoTracking().Where(expression).ToListAsync();

        public async Task<List<T>> GetAll() => await table.AsNoTracking().ToListAsync();

        public async Task<T> GetById(int id) => await table.FindAsync(id);

        
        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = table;
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            if (orderBy != null) return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            else return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, int pageIndex = 1, int pageSize = 3)
        {
            IQueryable<T> query = table;
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            if (orderBy != null) return await orderBy(query).Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            else return await query.Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public void Update(T entity) => _context.Entry<T>(entity).State = EntityState.Modified;

        public async Task Commit() => await _context.SaveChangesAsync();

        private bool isDispose = false;

        public async ValueTask DisposeAsync()
        {
            if (!isDispose)
            {
                isDispose = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this); //https://docs.microsoft.com/en-us/dotnet/api/system.gc.suppressfinalize?view=net-5.0, https://stackoverflow.com/questions/151051/when-should-i-use-gc-suppressfinalize
            }
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing) await _context.DisposeAsync();
        }
    }
}
