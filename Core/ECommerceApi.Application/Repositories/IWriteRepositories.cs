using ECommerceApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Repositories
{
    public interface IWriteRepositories<T>: IRepositories<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entity);
        bool Remove(T entity);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id);
        bool UpdateAsync(T entity);
    }
}
