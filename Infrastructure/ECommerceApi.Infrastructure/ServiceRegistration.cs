using ECommerceApi.Application.Abstraction.Storage;
using ECommerceApi.Application.Abstraction.Storage.Local;
using S =  ECommerceApi.Infrastructure.Services.Storages;
using Microsoft.Extensions.DependencyInjection;
using ECommerceApi.Infrastructure.Storage;
using ECommerceApi.Application.Abstraction.Token;
using ECommerceApi.Infrastructure.Services.Token;

namespace ECommerceApi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : S.Storagee, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();

        }

        // to do silinecek
        // ve ya enumlarla ala bilerik. bu usulla
        //public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        //{
        //    switch (storageType)
        //    {

        //        case StorageType.Local:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>(); break;

        //        case StorageType.Azure:
        //            //Azure ile elaqeli scoped
        //              break;

        //        case StorageType.AWS:
        //           //aws ile elaqeli scoped
        //           break;

        //               default:
        //            serviceCollection.AddScoped<IStorage, LocalStorage>(); break;
        //    }
        //}
    }
}
