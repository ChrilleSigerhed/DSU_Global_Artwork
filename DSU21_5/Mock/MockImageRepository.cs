﻿using DSU21_5.Data;
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
    public class MockImageRepository : IArtRepository
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

        public Task<IEnumerable<Artwork>> GetArtThatsPosted()
        {
            throw new NotImplementedException();
        }

        public Task<Artwork> AddArt(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork imageModel, Member member)
        {
            throw new NotImplementedException();
        }

      

        Task<IEnumerable<Artwork>> IArtRepository.GetPostedArtFromUniqueUser(string Id)
        {
            throw new NotImplementedException();
        }
        
        public List<Image> GetAllImagesFromDbConnectedToUsers(List<Member> members)
        {
            throw new NotImplementedException();
        }


        public Task<Artwork> DeleteArtworkFromArtworkTable(IWebHostEnvironment webHostEnvironment, Artwork artwork)
        {
            throw new NotImplementedException();
        }

        public Artwork GetArtworkForUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ArtworkViewModel> GetViewModel()

        {
            throw new NotImplementedException();
        }

        public Task<List<ArtworkInformation>> GetAllInformation(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<ArtworkViewModel> GetViewModel(List<Member> members)
        {
            throw new NotImplementedException();
        }

        public Artwork GetArtworkThatsGonnaBeDeleted(int id)
        {
            throw new NotImplementedException();
        }
    }
}
