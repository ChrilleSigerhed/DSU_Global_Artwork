using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class ImageRepository : IImageRepository
    {
        
        ImageDbContext db;
        public ImageRepository(ImageDbContext context)
        {
            
            db = context;
        }
        public ImageModel GetImageFromDb(string Id)
        {
            var image = db.Images.Where(x => x.UserId == Id).FirstOrDefault();
            return image;
        }
        public ImageModel RemoveImageFromDb(ImageModel imgModel)
        {
            db.Images.Remove(imgModel);
            return imgModel;
        }
        public async Task<ImageModel> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, ImageModel imageModel, string Id, ImageModel img)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extention = Path.GetExtension(imageModel.ImageFile.FileName);
            imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention; //TODO Change date extention
            imageModel.UserId = Id;
            string path = Path.Combine(wwwRootPath + "/image/", fileName);
            string PATH = Path.Combine(wwwRootPath + "/image/", img.ImageName);
            FileInfo file = new FileInfo(PATH);
            if (file.Exists)
            {
                File.Delete(PATH);
            }
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }
            context.Add(imageModel);
            await context.SaveChangesAsync();
            return imageModel;
        }

      

       
    }
}

