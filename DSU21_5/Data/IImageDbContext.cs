using DSU21_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IImageDbContext
    {
        EntityEntry Add(object entity);
        EntityEntry Update(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<Image> Images { get; set; }
        DbSet<Artwork> Artworks { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<Exhibit> Exhibit { get; set; }
        DbSet<Relationship> Relationships { get; set; }
    }
}