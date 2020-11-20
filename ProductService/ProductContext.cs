using Microsoft.EntityFrameworkCore;
using System;
using ProductService.Models;

namespace ProductService
{
    public class ProductContext:DbContext 
    {
        
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    
    }
}