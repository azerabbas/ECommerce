﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        readonly IConfiguration _configuration;

        public FileController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        [HttpGet("[action]")]
        public IActionResult GetBaseUrl()
        {
            return Ok(new
            {
                Url = _configuration["BaseStorageUrl"]
            });
        }
    }
}
