using DSU21_5.Data;
using DSU21_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace DSU21_5Tests.Data
{
    //internal class MockImageDbContext : IImageDbContext
    //{
    //    public DbSet<Image> Images { get => new TestDbSet<Image>(); set => throw new System.NotImplementedException(); }
    //    public DbSet<Artwork> Artworks { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //    public DbSet<Member> Members { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //    public DbSet<Exhibit> Exhibit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    //    public DbSet<Relationship> Relationships { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //    public EntityEntry Add(object entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public EntityEntry Update(object entity)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}