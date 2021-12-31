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
    public class ProfileUserConfiguration : IEntityTypeConfiguration<ProfileUser>
    {
        public void Configure(EntityTypeBuilder<ProfileUser> builder)
        {
            builder.ToTable("ProfileUsers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.FullName).HasMaxLength(50);
            builder.Property(c => c.Gender).HasMaxLength(20);
            builder.Property(c => c.Address).HasMaxLength(150);
            builder.Property(c => c.District).HasMaxLength(20);
            builder.Property(c => c.Wards).HasMaxLength(20);
            builder.Property(c => c.City).HasMaxLength(20);
            builder.Property(c => c.Hobby).HasMaxLength(100);
            builder.Property(c => c.Avatar).HasMaxLength(250);
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.HasOne(c => c.Account).WithOne(c => c.ProfileUser).HasForeignKey<ProfileUser>(c => c.AccountId);
        }
    }
}
