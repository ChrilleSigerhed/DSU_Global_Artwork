using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
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
        /// <param name="id"></param>
        /// <returns></returns>
        public Image GetImageFromDb(string id)
        {
            var image = db.Images.Where(x => x.UserId == id).FirstOrDefault();
            return image;
        }
        /// <summary>
        /// method that returns a list of all profile pictures in database
        /// </summary>
        /// <param name="members"></param>
        /// <returns>a list of profile pictures</returns>
        public List<Image> GetAllImagesFromDbConnectedToUsers()
        {
            var listOfImages = db.Images
                .Include("Member")
                .ToList();
            return listOfImages;
        }

        /// <summary>
        /// Removes the entry in Database and also clears the /image/ folder
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="imgModel"></param>
        /// <returns></returns>
        public async Task<Image> RemoveImageFromDb(IWebHostEnvironment hostEnvironment, Image imgModel)
        {
            db.Images.Remove(imgModel);
            await db.SaveChangesAsync();
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
        public async Task<Image> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, Image imageModel, string Id)
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
    }
}

