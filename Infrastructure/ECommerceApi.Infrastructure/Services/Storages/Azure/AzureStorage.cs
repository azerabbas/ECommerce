using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerceApi.Application.Abstraction.Storage.Azure;
using ECommerceApi.Infrastructure.Services.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerceApi.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storagee,  IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient= new(configuration["Storage:Azure"]);
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            // faylin containerin getiririk
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            // bu containerdeki fayli getiririk
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            //bunu silirik
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            // faylin containerin getiririk
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            // buradaki fayllarin sadece adin getirsin
            return _blobContainerClient.GetBlobs().Select(a => a.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            // faylin containerin getiririk
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            // verilen addaki fayli getirmek
            return _blobContainerClient.GetBlobs().Any(b => b.Name==fileName);

        }


        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            // Containeri getiririk
            _blobContainerClient =  _blobServiceClient.GetBlobContainerClient(containerName);
            //container var ya yoxdur>
            await _blobContainerClient.CreateIfNotExistsAsync();
            // blogContainere- catmaq icazesi
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(containerName, file.Name, HasFile);

                //Upload edeceyimiz fayli getiririk
                BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name);
                //Upload medonu file Strem-e cevirib gonderirik
                await blobClient.UploadAsync(file.OpenReadStream());
                //datas kolleksiyonuna bu datani add edirik
                datas.Add((fileNewName, $"{containerName}{fileNewName}"));
            }
            return datas;
        }
    }
}
