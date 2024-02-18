﻿using ECommerceApi.Application.Abstraction.Token;
using ECommerceApi.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int sec, AppUser user)
        {
            Application.DTOs.Token token = new();

            //SecurityKey-in simmetrikliyini aliriq
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Sifrelenmis kimlik yaradiriq
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //yaradilacaq token tenzimlemeleri
            token.Expiration = DateTime.UtcNow.AddSeconds(sec+14400);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                issuer: _configuration["Token:Issuer"],
                notBefore: DateTime.UtcNow,
                //sifrelemesi yuxaridaki kimi olacaq.
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken =  tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            //metoddan cixdiqda despos edilecek
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
