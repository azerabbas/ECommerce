using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApi.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            // Mediatr kitabxanasin servise add edirik
            collection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
