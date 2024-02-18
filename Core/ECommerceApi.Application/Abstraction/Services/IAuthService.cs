using ECommerceApi.Application.Abstraction.Services.Authentications;

namespace ECommerceApi.Application.Abstraction.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {

    }
}
