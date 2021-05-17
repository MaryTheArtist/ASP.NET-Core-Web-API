using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductID = "1",
                Name = "Galaxy A70",
                Brand = "Samsung",
                Quantity = 5,
                Price = 800,
                Weight = 200,
            },
            new Product 
            {
                ProductID = "2",
                Name = "Galaxy A50",
                Brand = "Samsung",
                Quantity = 10,
                Price = 600,
                Weight = 190
            }
            ,
            new Product
            {
                ProductID = "3",
                Name = "Galaxy A5",
                Brand = "Samsung",
                Quantity = 9,
                Price = 600,
                Weight = 190
            }
            ,
            new Product
            {
                ProductID = "4",
                Name = "Galaxy S7 ",
                Brand = "Samsung",
                Quantity = 10,
                Price = 700,
                Weight = 205
            },
            new Product
            {
                ProductID = "5",
                Name = "Note 10",
                Brand = "Samsung",
                Quantity = 5,
                Price = 1000,
                Weight = 180
            }

                );
        }
    }
}
