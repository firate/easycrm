﻿using EasyCRM.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Data.EF
{
    public class DataContext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CommunicationInfo> CommunicationInfos { get; set; }
        public DbSet<CommunicationType> CommunicationTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderLine> SalesOrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId);

                entity.HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.HasOne(e => e.BillToAddress)
                .WithMany(e => e.SalesOrderBillToAddress)
                .HasForeignKey(e => e.BillToAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.ShipToAddress)
                .WithMany(e => e.SalesOrderShipToAddress)
                .HasForeignKey(e => e.ShipToAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SalesOrderLine>()
                .HasOne(sol => sol.SalesOrder)
                .WithMany(so => so.OrderLines)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalesOrderLine>()
                .HasKey(s => new { s.Id, s.SalesOrderId });

            modelBuilder.Entity<OpportunityContact>()
                .HasKey(oc=> new { oc.OpportunityId, oc.ContactId });

            modelBuilder.Entity<OpportunityContact>()
                .HasOne(oc => oc.Contact)
                .WithMany(c => c.OpportunityContacts)
                .HasForeignKey(oc=>oc.ContactId);

            modelBuilder.Entity<OpportunityContact>()
                .HasOne(oc => oc.Opportunity)
                .WithMany(o => o.OpportunityContacts)
                .HasForeignKey(oc=>oc.OpportunityId);

            
        }




        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Code = "TR",
                    Name = "Turkey",
                    Note = ""
                }
                );

            modelBuilder.Entity<AccountType>().HasData(
                new AccountType
                {
                    Id = 1,
                    Name = "Customer",
                    Description = ""
                },
                new AccountType
                {
                    Id = 2,
                    Name = "Supplier",
                    Description = ""
                },
                new AccountType
                {
                    Id = 3,
                    Name = "Partner",
                    Description = ""
                }
                );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    AccountTypeId = 1,
                    OrganizationName = "Microsoft",
                    Description = "uuu",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                }
                );

            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id=1,
                    AccountId=1,
                    CountryId=1,
                    IsMain=true
                }
                );
        }
    }


    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DataContext> builder = new DbContextOptionsBuilder<DataContext>();

            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=easycrm;Trusted_Connection=True;";
            builder.UseSqlServer(connectionString);

            Console.WriteLine($"Running DesignTime DB context. ({connectionString})");
            
            return new DataContext(builder.Options);
        }
    }
}