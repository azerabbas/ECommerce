using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Features.Queries.Product.GetAllProduct
{
    //public class GetAllProductQueryResponse
    //{     
    //    public string ProductName { get; set; }
    //}

    //mvc and angular ucun
    public class GetAllProductQueryResponse
    {
        public int TotalProductCount { get; set; }
        public object Products { get; set; }

    }
}
