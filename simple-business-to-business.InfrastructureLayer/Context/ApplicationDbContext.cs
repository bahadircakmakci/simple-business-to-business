using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Enums;
using simple_business_to_business.InfrastructureLayer.Mapping.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.InfrastructureLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUsers, AppUserRoles, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AppUserRoles> AppUserRoles { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<AppUserManagerRoles> AppUserManagerRoles { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BrandDiscount> BrandDiscounts { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<CompanyDiscount> CompanyDiscounts { get; set; }
        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<ExchangeRates> ExchangeRates { get; set; }
        public DbSet<MainAttributes> MainAttributes { get; set; }
        public DbSet<MainCategories> MainCategories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ProductPictures> ProductPictures { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SubAttributes> SubAttributes { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new BasketsMap());
            builder.ApplyConfiguration(new BrandDiscountsMap());
            builder.ApplyConfiguration(new BrandsMap());
            builder.ApplyConfiguration(new CompaniesMap());
            builder.ApplyConfiguration(new CompanyDiscountsMap());
            builder.ApplyConfiguration(new CurrenciesMap());
            builder.ApplyConfiguration(new ExchangeRatesMap());
            builder.ApplyConfiguration(new MainAttributesMap());
            builder.ApplyConfiguration(new OrderDetailsMap());
            builder.ApplyConfiguration(new OrdersMap());
            builder.ApplyConfiguration(new ProductPicturesMap());
            builder.ApplyConfiguration(new ProductsMap());
            builder.ApplyConfiguration(new SubAttributesMap());
            builder.ApplyConfiguration(new SubCategoriesMap());


            //seed Data
            builder.Entity<Companies>()
                .HasData(
                 new Companies
                 {
                     Id = 1,
                     CompanyName = "DefaultCompany",
                     CreateDate = DateTime.Now,
                     City = "İstanbul",
                     Address = "deneme",
                     State = "Eyüp",
                     Status = Status.Active,
                     AccountingCode = "1111111111"

                 });


            builder.Entity<AppUserRoles>()
                .HasData(
                new AppUserRoles
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    Name = "admin",
                    NormalizedName = "ADMİN",
                    Status = Status.Active
                },
                new AppUserRoles
                {
                    Id = 2,
                    CreateDate = DateTime.Now,
                    Name = "plasiyer",
                    NormalizedName = "PLASİYER",
                    Status = Status.Active
                },
                new AppUserRoles
                {
                    Id = 3,
                    CreateDate = DateTime.Now,
                    Name = "member",
                    NormalizedName = "MEMBER",
                    Status = Status.Active
                }
                );
            builder.Entity<AppUsers>()
                .HasData(
                new AppUsers
                {
                    Id = 1,
                    CompanyName = "DefaultCompany",
                    CompanyId = 1,
                    UserName = "admin",
                    CreateDate = DateTime.Now,
                    Email = "admin@simpleb2b.com",
                    NormalizedEmail = "admin@simpleb2b.com",
                    NormalizedUserName = "ADMİN",
                    EmailConfirmed = false,
                    Status = Status.Active,
                    FullName = "Bahadır ÇAKMAKÇI",
                    ImagePath = "/images/users/default.jpg",
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    SecurityStamp = "simpleb2badmin",
                    PasswordHash = new PasswordHasher<AppUsers>().HashPassword(new AppUsers { UserName = "admin" }, "admin123")

                }
                );

            builder.Entity<AppUserManagerRoles>()
                .HasData(
                new AppUserManagerRoles { RoleId = 1, UserId = 1 }
                );
            builder.Entity<MainCategories>().HasData(new MainCategories
            {
                CategoyName = "Aydınlatma",
                CreateDate = DateTime.Now,
                Description = "Aydınlarma ve Ekipmanları",
                Id = 1,
                ImagePath = "/images/MainCategory/default.jpg",
                Status = Status.Active

            },
            new MainCategories
            {
                CategoyName = "Fren Sistemi",
                CreateDate = DateTime.Now,
                Description = "Fren Sistemi ve Ekipmanları",
                Id = 2,
                ImagePath = "/images/SubCategory/default.jpg",
                Status = Status.Active

            }
            );

            builder.Entity<SubCategories>().HasData(
                new SubCategories
                {
                    CategoyName = "Far",
                    CreateDate = DateTime.Now,
                    Description = "Far ve Ekipmanları",
                    Id = 1,
                    ImagePath = "/images/MainCategory/default.jpg",
                    Status = Status.Active,
                    MainCategoryId = 1

                },
            new SubCategories
            {
                CategoyName = "Balata",
                CreateDate = DateTime.Now,
                Description = "Balatalar",
                Id = 2,
                ImagePath = "/images/SubCategory/default.jpg",
                Status = Status.Active,
                MainCategoryId = 2

            }
            );
            builder.Entity<Brands>().HasData(

                new Brands
                {
                    BrandName = "Ayfar",
                    Description = "Ayfar Farları",
                    CreateDate = DateTime.Now,
                    Id = 1,
                    ImagePath = "/images/Brands/ayfar.jpg",
                    Status = Status.Active
                },
                new Brands
                {
                    BrandName = "TRW",
                    Description = "TRW Balataları",
                    CreateDate = DateTime.Now,
                    Id = 2,
                    ImagePath = "/images/Brands/trw.jpg",
                    Status = Status.Active
                }
                );
            builder.Entity<Currencies>().HasData(
                new Currencies
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    CurrencyCode = "TL",
                    CurrencyName = "Türk Lirası",
                    Symbol = '₺',
                    Status = Status.Active
                },
                new Currencies
                {
                    Id = 2,
                    CreateDate = DateTime.Now,
                    CurrencyCode = "USD",
                    CurrencyName = "Amerikan Doları",
                    Symbol = '$',
                    Status = Status.Active
                },
                new Currencies
                {
                    Id = 3,
                    CreateDate = DateTime.Now,
                    CurrencyCode = "EUR",
                    CurrencyName = "EURO",
                    Symbol = '€',
                    Status = Status.Active
                }
                );


            //


            base.OnModelCreating(builder);
        }

    }
}
