using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArtGallery.Application.Common
{
    public interface ITokenService
    {
        public ClaimsPrincipal ValidateToken(string jwtToken);
    }
}
