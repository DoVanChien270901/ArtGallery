﻿// <auto-generated />
using System;
using ArtGallery.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArtGallery.Data.Migrations
{
    [DbContext(typeof(ArtGalleryDbContext))]
    [Migration("20220120114352_new_1")]
    partial class new_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArtGallery.Data.Entities.Account", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.HasKey("Name");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.AmountInAuction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("AuctionId")
                        .HasColumnType("int");

                    b.Property<decimal>("NewPrice")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AuctionId");

                    b.ToTable("AmountInActions");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Auction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PriceStep")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("StartingPrice")
                        .HasColumnType("decimal(15,2)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.CategoryInProfile", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("CategoryInProfiles");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.FeedBack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Content")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Commision")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(15,2)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ViewCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<bool>("Thumbnail")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProductInCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductInCategories");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProfileUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique()
                        .HasFilter("[AccountId] IS NOT NULL");

                    b.ToTable("ProfileUsers");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(15,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.AmountInAuction", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Account", "Account")
                        .WithMany("AmountInAcctions")
                        .HasForeignKey("AccountId");

                    b.HasOne("ArtGallery.Data.Entities.Auction", "Auction")
                        .WithMany("AmountInAcctions")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Auction");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Auction", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Account", "Account")
                        .WithMany("Auctions")
                        .HasForeignKey("AccountId");

                    b.HasOne("ArtGallery.Data.Entities.Product", "Product")
                        .WithOne("Auction")
                        .HasForeignKey("ArtGallery.Data.Entities.Auction", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.CategoryInProfile", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Category", "Category")
                        .WithMany("CategoryInProfiles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Data.Entities.ProfileUser", "ProfileUser")
                        .WithMany("CategoryInProfiles")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ProfileUser");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.FeedBack", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Account", "Account")
                        .WithMany("FeedBacks")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Order", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Product", null)
                        .WithMany("Orders")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Product", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Account", "Account")
                        .WithMany("Products")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProductImage", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProductInCategory", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Category", "Category")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Data.Entities.Product", "Product")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProfileUser", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Account", "Account")
                        .WithOne("ProfileUser")
                        .HasForeignKey("ArtGallery.Data.Entities.ProfileUser", "AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Transaction", b =>
                {
                    b.HasOne("ArtGallery.Data.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Account", b =>
                {
                    b.Navigation("AmountInAcctions");

                    b.Navigation("Auctions");

                    b.Navigation("FeedBacks");

                    b.Navigation("Products");

                    b.Navigation("ProfileUser");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Auction", b =>
                {
                    b.Navigation("AmountInAcctions");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Category", b =>
                {
                    b.Navigation("CategoryInProfiles");

                    b.Navigation("ProductInCategories");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.Product", b =>
                {
                    b.Navigation("Auction");

                    b.Navigation("Orders");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductInCategories");
                });

            modelBuilder.Entity("ArtGallery.Data.Entities.ProfileUser", b =>
                {
                    b.Navigation("CategoryInProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
