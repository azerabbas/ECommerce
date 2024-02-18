﻿using ECommerceApi.Application.Repositories;
using ECommerceApi.Domain.Entities.Common;
using ECommerceApi.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceApi.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepositories<T> where T : BaseEntity
    {

        readonly ECommerceApiDbContext _context;

        public ReadRepository(ECommerceApiDbContext context)
        {
            _context=context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if(!tracking)
                query = query.AsNoTracking();
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
         }
 
       //GetById metodu Id FirsOrDefault ile cagiririq.   
        public async Task<T> GetByIdAsync(string id, bool tracking =true)
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id==Guid.Parse(id));
        }
    }
}
