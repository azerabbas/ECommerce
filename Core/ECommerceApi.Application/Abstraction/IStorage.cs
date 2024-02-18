using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Abstraction.Storage.Local
{
    public interface IStorage
    {

        // FIle upload method
        //string path : path after wwwroot
        // Uploaddan sonra file name-in ve path-in geri qaytarmasin isteyirem. database add etmek ucun
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);

        // gelen file name qarsiliq olan fayli Containerden sil
        Task DeleteAsync(string pathOrContainerName, string fileName);

        // bu path ve ya containerde olan butun fayllari getir
        List<string> GetFiles(string pathOrContainerName);

        // bu kontainer name ve ya path-de bu file name olan file var. varsa true yoxdursa false    
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
