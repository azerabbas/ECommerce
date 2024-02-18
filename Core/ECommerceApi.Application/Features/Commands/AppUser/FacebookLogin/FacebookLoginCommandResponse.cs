using ECommerceApi.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandResponse 
    {
        public Token Token { get; set; }
    }
}
