﻿using ArtGallery.Data.Configurations;
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
            modelbuilder.ApplyConfiguration(new CategoryConfiguration());
            
            modelbuilder.ApplyConfiguration(new FeedBackConfiguration());
            modelbuilder.ApplyConfiguration(new OrderConfiguration());
            modelbuilder.ApplyConfiguration(new ProductConfiguration());
            modelbuilder.ApplyConfiguration(new ProductImageConfiguration());

            modelbuilder.ApplyConfiguration(new ProfileUserConfiguration());

            modelbuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelbuilder.ApplyConfiguration(new CategoryInProfileConfiguration());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AmountInAuction> AmountInAuctions { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProfileUser> ProfileUsers { get; set; }
        public DbSet<ProductInCategory>  ProductInCategories { get; set; }
        public DbSet<CategoryInProfile> CategoryInProfiles { get; set; }
    }
}
