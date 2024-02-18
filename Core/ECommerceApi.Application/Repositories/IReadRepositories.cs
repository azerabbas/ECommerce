using ECommerceApi.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerceApi.Application.Repositories
{
    public interface IReadRepositories<T> : IRepositories<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
