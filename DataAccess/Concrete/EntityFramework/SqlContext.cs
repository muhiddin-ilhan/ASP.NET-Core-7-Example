using System;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class SqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Northwind;User ID=SA; Password=123456789Aa;TrustServerCertificate=true");
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OperationClaim> OperationClaims { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
