using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Common
{
    public interface IStogareService
    {
        Task SaveFile(IFormFile file);
        Task DeleteFile(string filenamme);
    }
}
