using DSU21_5.Models;
using DSU21_5.Models.ViewModels;
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
        /// <summary>
        /// Input is UserId from current user that is using the application, the method takes that ID and checks it against the DataBase, to controll if the user already has a profilepicture uploaded.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ImageModel GetImageFromDb(string Id)
        {
            var image = db.Images.Where(x => x.UserId == Id).FirstOrDefault();
            return image;
        }
        /// <summary>
        /// Removes the entry in Database and also clears the /image/ folder
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="imgModel"></param>
        /// <returns></returns>
        public ImageModel RemoveImageFromDb(IWebHostEnvironment hostEnvironment, ImageModel imgModel)
        {
            db.Images.Remove(imgModel); 
            string wwwRootPath = hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/image/", imgModel.ImageName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                File.Delete(path);
            }
            return imgModel;
        }
        /// <summary>
        /// Input is the UserId from the current user that is using the application, also an empty imageModel that is beeing build in the method. Later it is added to the database and returns the imageModel for display
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="imageModel"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ImageModel> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, ImageModel imageModel, string Id)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extention = Path.GetExtension(imageModel.ImageFile.FileName);
            imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention; 
            imageModel.UserId = Id;
            string path = Path.Combine(wwwRootPath + "/image/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }
            context.Add(imageModel);
            await context.SaveChangesAsync();
            return imageModel;
        }

        public Task<ShowroomViewModel> GetShowroomImages()
        {
            throw new NotImplementedException();
        }
    }
}

