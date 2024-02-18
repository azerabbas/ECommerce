using ECommerceApi.Application.Abstraction.Storage.Local;
using ECommerceApi.Infrastructure.Services.Storages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storagee , ILocalStorage
    {

        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment=webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");
 

        private async Task<bool> CopyFileName(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024*1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //to do log
                throw ex;

            }
        }


        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
           return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {

            // _webHostEnvironment.WebRootPath : path up to wwwroot
            //wwwroot/resourse/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            //numerical random value
            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                string newFileName = await FileRenameAsync(path, file.Name, HasFile);

                await CopyFileName($"{uploadPath}\\{newFileName}", file);
                datas.Add((newFileName, $"{path}\\{newFileName}"));

            }


            return datas;

        }
    }
}
