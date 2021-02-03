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
using DSU21_5.Models.ViewModel;

namespace DSU21_5.Mock
{
    public class MockImageRepository : IImageRepository
    { 
        private string basePath;
        public MockImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            basePath = $"{webHostEnvironment.ContentRootPath}\\Mock\\";
        }
        
        public Task<Image> CreateNewProfilePicture(ImageDbContext context, IWebHostEnvironment hostEnvironment, Image imageModel, string Id)
        {
            throw new NotImplementedException();
        }

        public Image GetImageFromDb(string Id)
        {
            throw new NotImplementedException();
        }

        public Image RemoveImageFromDb(IWebHostEnvironment hostEnvironment, Image imgModel)
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
