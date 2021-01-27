using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IImageRepository
    {
        ImageModel GetImageFromDb(string Id);
        ImageModel RemoveImageFromDb(ImageModel imgModel);
        Task<ImageModel> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, ImageModel imageModel, string Id, ImageModel image);
    }
}