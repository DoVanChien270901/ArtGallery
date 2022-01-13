using ArtGallery.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Configurations
{
    public class CategoryInProfileConfiguration : IEntityTypeConfiguration<CategoryInProfile>
    {
        public void Configure(EntityTypeBuilder<CategoryInProfile> builder)
        {
            builder.ToTable("CategoryInProfiles");
            builder.HasKey(c => new { c.CategoryId, c.ProfileId });
            builder.HasOne(c => c.Category).WithMany(c => c.CategoryInProfiles)
                .HasForeignKey(c => c.CategoryId);
            builder.HasOne(c => c.ProfileUser).WithMany(c => c.CategoryInProfiles)
                .HasForeignKey(c => c.ProfileId);
        }
    }
}
