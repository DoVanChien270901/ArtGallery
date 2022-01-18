using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ArtGallery.Application.Common
{
    public class StorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private readonly string USER_CONTENT_FOLDER_NAME = "use-content";
        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public async Task<string> GetFileUrl(string pathfileName)
        {
            string port = "http://localhost:5000//use-content/";
            return $"{port}/{pathfileName}";
        }

        public async Task SaveFile(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
