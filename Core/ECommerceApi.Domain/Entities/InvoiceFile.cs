using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class InvoiceFile : File
    {
        // faturalarin fiyat bilgileri
        public decimal Price { get; set; }
    }
}
