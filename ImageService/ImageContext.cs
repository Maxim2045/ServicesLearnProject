using ImageService.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageService
{
    public class ImageContext:DbContext
    {
        public DbSet<Image> Images { get; set; }
        public ImageContext(DbContextOptions<ImageContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }
    }
}