using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Abstraction.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<DTOs.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
