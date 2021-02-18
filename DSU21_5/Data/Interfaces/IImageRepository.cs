using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IImageRepository
    {

        Image GetImageFromDb(string Id);
        Task<Image> RemoveImageFromDb(IWebHostEnvironment hostEnvironment, Image imgModel);
        Task<Image> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, Image imageModel, string Id);
     
        List<Image> GetAllImagesFromDbConnectedToUsers();

    }
        
}