using Azure.Core;
using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.Abstraction.Token;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.DTOs.Facebook;
using ECommerceApi.Application.DTOs.Facebook.FacebookUserAccessTokenValidation;
using ECommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using A = ECommerceApi.Domain.Entities.Identity;

namespace ECommerceApi.Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly ITokenHandler _tokenHandler;
        readonly UserManager<A.AppUser> _userManager;
        readonly SignInManager<A.AppUser> _signInManager;
        readonly IUserService _userService;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<A.AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _httpClient=httpClientFactory.CreateClient();
            _configuration=configuration;
            _userManager=userManager;
            _tokenHandler=tokenHandler;
            _signInManager=signInManager;
            _userService=userService;
        }

        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user!= null;
            if (user==null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user==null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdatdeRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new Exception("Invalid external authentication");
        }

        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {

            // facebook login settings
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternarLoginSettings:FacebookLogin:ClientId"]}&client_secret={_configuration["ExternarLoginSettings:FacebookLogin:ClientSecret"]}&grant_type=client_credentials");
            // jsona ceviririk
            FacebookAccessTokenResponse_DTO? facebookAccessToken = JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessToken?.AccessToken}");
            FacebookUserTokenValidation? validator = JsonSerializer.Deserialize<FacebookUserTokenValidation>(userAccessTokenValidation);

            //bilgiler dogrudursa token ile data teleb edirik. validation null deyilse IsValid-e baxacaq
            if (validator?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validator.Data.UserId, "FACEBOOK");
                A.AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
            }

            throw new Exception("Invalid external authentication");
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            ValidationSettings settings = new()
            {
                //google client id
                Audience = new List<string> { _configuration["ExternarLoginSettings:Google:Client_ID"] }
            };

            // bu id token ile settingsi dogrula
            var payload = await ValidateAsync(idToken, settings);
            // xarici qaynaqdan gelen useri AspNetUSerLogins-e yukle
            UserLoginInfo info = new("GOOGLE", payload.Subject, "GOOGLE");

            A.AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            //user null deyilse resulta true eksi halda false ver
            return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            // bucur username oldugunu yoxlayiriq
            A.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                //yoxdursa email ile yoxlayiriq
                user =  await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                //to do Exceptionlari duzelt
                throw new Exception("UserName ve ya Sifre xetali");
            // gelen usernameOrEmail ile sifrenin ust uste dusub dusmediyini yoxlayiriq. sehv oldugu halda true verib bloklaya bilerik
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                //Token yaratmaq ve parametr ile vaxtini vermek
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user );
                await _userService.UpdatdeRefreshToken(token.RefreshToken, user, token.Expiration, 5);
                return token;
            }

            // todo buraya exception add edib message silmek ve bu response-da silmek mumkundur.
            throw new Exception();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken==refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow.AddMinutes(240))
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdatdeRefreshToken(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
                throw new Exception("Bu user tapilmadi");
        }
    }

}