using System.Collections.Generic;
using ImageService.Models;
using ImageService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ImageService
{
    public class ImageContext : DbContext
    {
        public DbSet<ImageRepository> Image { get; set; }

        public ImageContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}