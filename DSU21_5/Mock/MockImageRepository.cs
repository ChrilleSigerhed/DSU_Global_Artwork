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
using System.Collections.ObjectModel;

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

      

        Task<IEnumerable<Artwork>> IArtRepository.GetPostedArtFromUniqueUser(Member member)
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

        
        public Task<ArtworkViewModel> GetViewModel(List<Member> members)
        {
            throw new NotImplementedException();
        }


        public Task<Artwork> AddArt(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork imageModel, Member member, Exhibit exhibit)
        {
            throw new NotImplementedException();
        }

        public Task<Exhibit> CreateExhibit(ImageDbContext context, Member member)
        {
            throw new NotImplementedException();
        }

   
        public Task<IEnumerable<Artwork>> GetAllArtToExhibitions()
        {
            throw new NotImplementedException();
        }

        public bool CheckIfIdExists(Member member)
        {
            throw new NotImplementedException();
        }

        public Exhibit GetExhibitId(Member member)
        {
            throw new NotImplementedException();
        }

        public Task<Artwork> AddArtWithExistingExhibitId(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, Exhibit exhibit)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artwork>> GetArtFromExhibit(Member member)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<ArtworkInformation>> GetArtConnectedToExhibit(List<string> ids)
        {
            throw new NotImplementedException();
        }

        
        public Task<Exhibit> UpdateExhibition(Member member, Exhibit exhibit)
        {
            throw new NotImplementedException();
        }

        public Task<Artwork> DeleteArtworkFromExhibit(IWebHostEnvironment hostEnvironment, string artwork)
        {
            throw new NotImplementedException();
        }

      

        Task<List<Artwork>> IArtRepository.GetArtFromExhibit(Member member)
        {
            throw new NotImplementedException();
        }

        public Task<List<Exhibit>> GetExhibits()
        {
            throw new NotImplementedException();
        }

        public Task<List<Artwork>> GetExhibitArt(List<Exhibit> exhibits)
        {
            throw new NotImplementedException();
        }

        public Task<List<Artwork>> GetExhibitArt()
        {
            throw new NotImplementedException();
        }

        public Task<List<Artwork>> GetExhibitArt(Exhibit exhibit)
        {
            throw new NotImplementedException();
        }

    }
}
