using DSU21_5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        {
        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Exhibit> Exhibit { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favourite>()
                .HasKey(vf => new { vf.MemberId, vf.ArtworkId });
        }
    }
}
