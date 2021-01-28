using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IImageRepository
    {
        Image GetImageFromDb(string Id);
        Image RemoveImageFromDb(IWebHostEnvironment hostEnvironment,Image imgModel);
        Task<Image> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, Image imageModel, string Id);
    }
}