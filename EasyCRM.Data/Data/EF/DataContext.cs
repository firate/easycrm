using EasyCRM.Entity.Models;
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

        public DbSet<Account> Account { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CommunicationInfo> CommunicationInfo { get; set; }
        public DbSet<CommunicationType> CommunicationType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Currency> CurrencyRate { get; set; }
        public DbSet<Currency> CurrencyRateType { get; set; }
        public DbSet<Industry> Industry { get; set; }
        public DbSet<Lead> Lead { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Opportunity> Opportunity { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<SalesOrder> SalesOrder { get; set; }
        public DbSet<SalesOrderLine> SalesOrderLine { get; set; }
        public DbSet<UnitCode> UnitCode { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<AccountGroup> AccountGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId);

                entity.HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<AccountGroup>()
                .HasKey(ac => new { ac.AccountId,ac.GroupId });

            modelBuilder.Entity<AccountGroup>(entity=> 
            {
                entity.HasOne(e => e.Account)
                    .WithMany(e => e.AccountGroups)
                    .HasForeignKey(e => e.AccountId);

                entity.HasOne(e => e.Group)
                    .WithMany(e => e.AccountGroups)
                    .HasForeignKey(e => e.GroupId);
            
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

            modelBuilder.Entity<ContactAddress>()
                .HasKey(ca=>new {ca.ContactId, ca.AddressId });

            modelBuilder.Entity<ContactAddress>()
                .HasOne(ca => ca.Contact)
                .WithMany(c => c.ContactAddresses)
                .HasForeignKey(ca=>ca.ContactId);

            modelBuilder.Entity<ContactAddress>()
                .HasOne(ca => ca.Address)
                .WithMany(a => a.ContactAddresses)
                .HasForeignKey(ca => ca.AddressId);


            Seed(modelBuilder);

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

            //modelBuilder.Entity<Account>().HasData(
            //    new Account
            //    {
            //        AccountId = 1,
            //        AccountTypeId = 1,
            //        OrganizationName = "Microsoft",
            //        Description = "uuu",
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now

            //    }
            //    );

            //modelBuilder.Entity<Address>().HasData(
            //    new Address
            //    {
            //        Id=1,
            //        AccountId=1,
            //        CountryId=1,
            //        IsMain=true
            //    }
            //    );

            //modelBuilder.Entity<CommunicationType>().HasData( 
            //    new CommunicationType 
            //    { 
            //        Id=1,
            //        Name="email",
            //        Description="E-Mail"
            //    },

            //    new CommunicationType
            //    {
            //        Id = 2,
            //        Name = "phone",
            //        Description = "Phone"
            //    },

            //    new CommunicationType
            //    {
            //        Id = 3,
            //        Name = "fax",
            //        Description = "Fax"
            //    },
            //    new CommunicationType
            //    {
            //        Id = 4,
            //        Name = "www",
            //        Description = ""
            //    }

            //    );

            
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