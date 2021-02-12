using DSU21_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class FavouritesRepository : IFavouritesRepository
    {
        ImageDbContext db;

        public FavouritesRepository(ImageDbContext context)
        {
            db = context;
        }

        public async Task<Favourite> EditFavourite(string Id, int ArtworkId)
        {
            var post = db.Favourites.Find(Id, ArtworkId);
            Favourite favourite = new Favourite(Id,ArtworkId);

            if (post == null)
            {
                db.Favourites.Add(favourite);
                await db.SaveChangesAsync();
                return post;
                
            }
            else
            {
                db.Favourites.Remove(post);
                await db.SaveChangesAsync();
                return null;
            }
        }
    }
}
