using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.Abstraction.Services.Authentications;
using ECommerceApi.Application.AutoMapper;
using ECommerceApi.Application.Repositories;
using ECommerceApi.Domain.Entities.Identity;
using ECommerceApi.Persistance.Context;
using ECommerceApi.Persistance.Services;
using ECommerceApi.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerceApi.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            
            services.AddDbContext<ECommerceApiDbContext>(options => options.UseSqlServer(Configurations.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric =false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ECommerceApiDbContext>();
            services.AddScoped<IUoW, UoW>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

        }
    }
}

