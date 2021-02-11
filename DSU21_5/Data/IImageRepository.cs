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
        Image RemoveImageFromDb(IWebHostEnvironment hostEnvironment, Image imgModel);
        Task<Image> CreateNewProfilePicture(IImageDbContext context, IWebHostEnvironment hostEnvironment, Image imageModel, string Id);
        Task<ShowroomViewModel> GetShowroomImages();
        List<Image> GetAllImagesFromDbConnectedToUsers(List<Member> members);

    }
        
}