using ArtGallery.Data.Configurations;
using ArtGallery.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.EF
{
    public class ArtGalleryDbContext : DbContext
    {
        public ArtGalleryDbContext( DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //base.OnModelCreating(modelbuilder);
            //configure using Fluent API
            modelbuilder.ApplyConfiguration(new AccountConfiguration());
            modelbuilder.ApplyConfiguration(new AmountInAuctionConfiguration());
            modelbuilder.ApplyConfiguration(new AuctionConfiguration());
            modelbuilder.ApplyConfiguration(new CartConfiguration());
            modelbuilder.ApplyConfiguration(new CategoryConfiguration());

            modelbuilder.ApplyConfiguration(new CommissionConfiguration());
            modelbuilder.ApplyConfiguration(new FeedBackConfiguration());
            modelbuilder.ApplyConfiguration(new OrderConfiguration());
            modelbuilder.ApplyConfiguration(new ProductConfiguration());
            modelbuilder.ApplyConfiguration(new ProductImageConfiguration());

            modelbuilder.ApplyConfiguration(new ProductInCartConfiguration());
            modelbuilder.ApplyConfiguration(new ProfileUserConfiguration());
            modelbuilder.ApplyConfiguration(new TransactionConfiguration());
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AmountAuction> AmountAuctions { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Commission> Commissions { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductInCart> ProductInCarts { get; set; }
        public DbSet<ProfileUser> ProfileUsers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
