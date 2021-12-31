using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.EF
{
    public class ArtGalleryDbContextFactory : IDesignTimeDbContextFactory<ArtGalleryDbContext>
    {
        public ArtGalleryDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("ArtGalleryShop");

            var optionBuilder = new DbContextOptionsBuilder<ArtGalleryDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new ArtGalleryDbContext(optionBuilder.Options);
        }
    }

}
