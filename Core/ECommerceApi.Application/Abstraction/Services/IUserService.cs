using ECommerceApi.Application.DTOs.User;
using ECommerceApi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUserRequest model);
        Task UpdatdeRefreshToken(string refreshToken, AppUser user, DateTime accessDateToken, int addOnAccessTokenDate);
    }
}
