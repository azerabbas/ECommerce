using Azure.Core;
using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.DTOs.User;
using ECommerceApi.Application.Features.Commands.AppUser;
using ECommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager=userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserRequest model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameUsername
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Istifadeci ugurla yaradilmisdir";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}";
            return response;
        }

        public async Task UpdatdeRefreshToken(string refreshToken, AppUser user, DateTime accessDateToken, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessDateToken.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("User tapilmadi");
        }
    }
}
