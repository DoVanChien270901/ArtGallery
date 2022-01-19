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
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.ToTable("Auctions");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.StartingPrice).HasColumnType("decimal(15,2)");
            builder.Property(c => c.StartDateTime).IsRequired();
            builder.Property(c => c.EndDateTime).IsRequired();
            builder.Property(c => c.Status).HasDefaultValue(false);
            builder.HasOne(c=>c.Product).WithOne(c=>c.Auction).HasForeignKey<Auction>(c=>c.ProductId);
            builder.HasOne(c => c.Account).WithMany(c => c.Auctions).HasForeignKey(c => c.AccountId);
        }
    }
}
