using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Operations
{
    public static class NameOperation
    {
        //correcting incoming path names
        public static string CharcterRegulatory(string name) =>
            name.Replace("\"","")
            .Replace("!", "/")
            .Replace("'", "/")
            .Replace("^", "/")
            .Replace("+", "/")
            .Replace("%", "/")
            .Replace("&", "/")
            .Replace("/", "/")
            .Replace("(", "/")
            .Replace(")", "/")
            .Replace("=", "/")
            .Replace("?", "/")
            .Replace("_", "/")
            .Replace("", "/")
            .Replace("@", "/")
            .Replace("~", "/")
            .Replace(",", "/")
            .Replace(":", "/")
            .Replace(";", "/")
            .Replace(".", "/")
            .Replace("Ö", "o")
            .Replace("ö", "o")
            .Replace("Ü", "u")
            .Replace("ü", "u")
            .Replace("İ", "i")
            .Replace("ı", "i")
            .Replace("Ğ", "g")
            .Replace("ğ", "g")
            .Replace("ə", "a")
            .Replace("Ə", "a")
            .Replace("Ş", "s")
            .Replace("ş", "s")
            .Replace("Ç", "c")
            .Replace("ç", "c")
            .Replace(">", "")
            .Replace("<", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("{", "")
            .Replace("}", "")
            .Replace("€", "")
            .Replace("¡", "")
            .Replace("#", "")
            .Replace("§", "")
            .Replace("©", "")
            .Replace("®", "")
            .Replace("™", "")
            .Replace("™", "")
            .Replace("*", "")
            .Replace("-", "")
            .Replace("|", "");

    }
}



