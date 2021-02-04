using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
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
        public async Task<ArtworkViewModel> GetViewModel()
        {
            var list = await GetArtThatsPosted();
            var model = new ArtworkViewModel(list);
            return model;
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
            
            string path = Path.Combine(wwwRootPath + "/imagesArt/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await artworkModel.ImageFile.CopyToAsync(fileStream);
            }
            context.Add(artworkModel);
            await db.SaveChangesAsync();
            return artworkModel;
        }
        public async Task<IEnumerable<Artwork>> GetPostedArtFromUniqueUser(string Id)
        {
            IEnumerable<Artwork> art = db.Artworks.Where(x => x.UserId == Id);
            await db.SaveChangesAsync();
            return art;
        }
        public Artwork GetArtworkThatsGonnaBeDeleted(int id)
        {
            Artwork artwork = db.Artworks.Where(x => x.ArtworkId == id).FirstOrDefault();
            return artwork;
        }
        public async Task<Artwork> DeleteArtworkFromArtworkTable(IWebHostEnvironment hostEnvironment, Artwork artwork)
        {
            db.Artworks.Remove(artwork);
            await db.SaveChangesAsync();
            string wwwRootPath = hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/imagesArt/", artwork.ImageName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                File.Delete(path);
            }
           
            return artwork;
        }

    }
}
