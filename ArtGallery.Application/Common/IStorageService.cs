using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Common
{
    public interface IStorageService
    {
        Task<string> GetFileUrl(string pathfileName);
        Task SaveFile(Stream mediaBinaryStream, string fileName);
        Task DeleteFile(string filenamme);
    }
}
