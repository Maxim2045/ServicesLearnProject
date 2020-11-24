using Microsoft.EntityFrameworkCore;
using System;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService
{
    public class ProductContext:DbContext 
    {
        
        public DbSet<ProductDb> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    
    }
}