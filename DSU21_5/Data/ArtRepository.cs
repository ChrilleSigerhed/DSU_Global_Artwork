using DSU21_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class ArtRepository : IArtRepository
    {
        ImageDbContext db;
        public ArtRepository(ImageDbContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Artwork>> GetArtThatsPosted()
        {
            IEnumerable<Artwork> images = db.Artworks;
            await db.SaveChangesAsync();
            return images;
        }
    }
}
