using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<Artwork> AddArt(ImageDbContext context,IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(artworkModel.ImageFile.FileName);
            string extention = Path.GetExtension(artworkModel.ImageFile.FileName);
            artworkModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            artworkModel.UserId = member.MemberId;
            artworkModel.Firstname = member.Firstname;
            artworkModel.Lastname = member.Lastname;
            artworkModel.Description = "En jättefin tavla"; //TODO: Hämta description från gränssnittet
            artworkModel.ArtName = "Jättefin tavla"; //TODO: Hämta Artname från gränssnittet
            
            string path = Path.Combine(wwwRootPath + "/imagesArt/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await artworkModel.ImageFile.CopyToAsync(fileStream);
            }
            context.Add(artworkModel);
            await db.SaveChangesAsync();
            return artworkModel;
        }
    }
}
