using DSU21_5.Data;
using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using DSU21_5.Infrastructure;
using DSU21_5.Models.ViewModels;

namespace DSU21_5.Mock
{
    public class MockImageRepository : IImageRepository
    { 
        private string basePath;
        public MockImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            basePath = $"{webHostEnvironment.ContentRootPath}\\Mock\\";
        }
        
        public Task<ImageModel> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, ImageModel imageModel, string Id)
        {
            throw new NotImplementedException();
        }

        public ImageModel GetImageFromDb(string Id)
        {
            throw new NotImplementedException();
        }

        public ImageModel RemoveImageFromDb(IWebHostEnvironment hostEnvironment, ImageModel imgModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ShowroomViewModel> GetShowroomImages()
        {
            await Task.Delay(0);
            return  new ShowroomViewModel(FileHandler.GetData<List<ShowroomImageModel>>($"{basePath}ShowroomImageMock.json"));
        }


      
    }
}
