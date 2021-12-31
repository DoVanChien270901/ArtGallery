using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Common
{
    public class StogareService : IStogareService
    {
        public Task DeleteFile(string filenamme)
        {
            throw new NotImplementedException();
        }

        public Task SaveFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
