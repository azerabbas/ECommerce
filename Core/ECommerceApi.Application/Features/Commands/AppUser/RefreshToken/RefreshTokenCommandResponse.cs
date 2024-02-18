using ECommerceApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.AppUser.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public Token Token { get; set; }
    }
}
