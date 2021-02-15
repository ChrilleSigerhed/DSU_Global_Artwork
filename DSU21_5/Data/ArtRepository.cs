using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class ArtRepository : IArtRepository
    {
        ImageDbContext db;
        public List<ArtworkInformation> ArtworkInformation { get; set; } = new List<ArtworkInformation>();
        public IEnumerable<Artwork> ArtToExhibits { get; set; }
        public List<Exhibit> ListOfIds { get; set; } = new List<Exhibit>();
        public ObservableCollection<ArtworkInformation> ListOfArtToExhibit { get; set; }


        public ArtRepository(ImageDbContext context)
        {
            db = context;
        }
        /// <summary>
        /// Method that gets all art from one user that is connected to an exhibition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a list of art</returns>
        public async Task<List<Artwork>> GetArtFromExhibit(string id)
        {
            List<Artwork> AllArt = new List<Artwork>();
            var allArt = db.Artworks.Where(x => x.UserId == id && x.ExhibitId != null).Select(x => x.ImageName);
            foreach (var item in allArt)
            {
                Artwork artwork = new Artwork
                {
                    ImageName = item
                };
                AllArt.Add(artwork);
            }
            await db.SaveChangesAsync();
            return AllArt;
        }
        /// <summary>
        /// Returns a list of unique ids
        /// </summary>
        /// <returns>list of ids</returns>
        public async Task<List<Exhibit>> GetUniqueIdsConnectedToExhibit()
        {
            //List<string> listOfIds = new List<string>();
            var listOfIds = db.Exhibit
                .Include("Member")
                .ToList();

            //var ids = db.Artworks.Where(x => x.ExhibitId != null).Select(x => x.UserId);
            //foreach (var item in ids)
            //{
            //    if (!listOfIds.Contains(item))
            //    {
            //        listOfIds.Add(item);
            //    }
            //}
            //await db.SaveChangesAsync();
            ListOfIds = listOfIds;
            return ListOfIds;
        }
        /// <summary>
        /// creates a list with all information of the art connected to an exhibition
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>observablecollection of artwork-information</returns>
        public async Task<ObservableCollection<ArtworkInformation>> GetArtConnectedToExhibit(List<string> ids)
        {
            ListOfArtToExhibit = new ObservableCollection<ArtworkInformation>();
            ArtworkInformation artworkInformation;
            foreach (var item in ids)
            {
                //TODO: hämtar en member, den metoden ligger också i memberrepository, kanske slå ihop dessa repon?
                var member = db.Members.Where(x => x.MemberId == item).FirstOrDefault();
                var artworks = db.Artworks.Where(x => x.UserId == item && x.ExhibitId != null).Select(x => x.ArtworkId);
                foreach (var artwork in artworks)
                {
                    var art = db.Artworks.Where(x => x.ArtworkId == artwork).FirstOrDefault();
                    artworkInformation = new ArtworkInformation()
                    {
                        Firstname = member.Firstname,
                        Source = art.ImageName,
                        Lastname = member.Lastname,
                        Height = art.Height,
                        Width = art.Width,
                        Type = art.Type,
                        Year = art.Year,
                        Description = art.Description,
                        Title = art.ArtName,
                        UserId = member.MemberId
                    };
                    ListOfArtToExhibit.Add(artworkInformation);
                }
                await db.SaveChangesAsync();
            }
            return ListOfArtToExhibit;

        }
        /// <summary>
        /// method that checks if an id already exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        public bool CheckIfIdExists(string id)
        {
            if (db.Exhibit.Any(x => x.MemberId == id))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// method that returns an ID of the existing exhibition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ID of existing exhibition </returns>
        public int? GetExhibitId(string id)
        {
            var getId = db.Exhibit
                .Where(x => x.MemberId == id)
                .FirstOrDefault();
            int? exhibitId = getId.Id;
            return exhibitId;
            
        }
    
        public async Task<Exhibit> UpdateExhibition(string id, Exhibit exhibit)
        {
            var exhibition = db.Exhibit.Where(x => x.MemberId == id).FirstOrDefault();
            exhibition.Name = exhibit.Name;
            exhibition.StartDate = exhibit.StartDate;
            exhibition.StopDate = exhibit.StopDate;
            exhibition.Publish = true;
            await db.SaveChangesAsync();
            return exhibition;
          
        }
        /// <summary>
        /// Method that returns the artwork-viewmodel with all art uploaded in the database
        /// </summary>
        /// <param name="members"></param>
        /// <returns>artwork-viewmodel</returns>
        public async Task<ArtworkViewModel> GetViewModel(List<Member> members)
        {
            var list = await GetArtThatsPosted();
            var model = new ArtworkViewModel(list, members);
            return model;
        }
        public async Task<IEnumerable<Artwork>> GetArtThatsPosted()
        {
            var art = db.Artworks
                .Include("Member")
                .ToList();
            await db.SaveChangesAsync();
            return art;
        }
        /// <summary>
        /// method that creates a new exhibition
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <returns>an exhibition</returns>
        public async Task<Exhibit> CreateExhibit(ImageDbContext context, Member member)
        {
            Exhibit exhibit = new Exhibit()
            {

                StartDate = "not available",
                StopDate = "not available",
                Name = "not available",
                MemberId = member.MemberId,
                

            };
            context.Add(exhibit);
            await db.SaveChangesAsync();
            return exhibit;
        }
        public async Task<Artwork> AddArt(ImageDbContext context,IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, Exhibit exhibit)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(artworkModel.ImageName);
            string extention = Path.GetExtension(artworkModel.ImageFile.FileName);
            artworkModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            artworkModel.UserId = member.MemberId;
            if (exhibit != null)
            {
            artworkModel.ExhibitId = exhibit.Id;

            }
            string path = Path.Combine(wwwRootPath + "/imagesArt/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await artworkModel.ImageFile.CopyToAsync(fileStream);
            }
            context.Add(artworkModel);
            await db.SaveChangesAsync();
            return artworkModel;
        }
        /// <summary>
        /// method that creates a new artwork and adds the existing exhibition-ID
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="artworkModel"></param>
        /// <param name="member"></param>
        /// <param name="exhibit"></param>
        /// <returns>artwork</returns>
        public async Task<Artwork> AddArtWithExistingExhibitId(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, int? exhibit)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(artworkModel.ImageName);
            string extention = Path.GetExtension(artworkModel.ImageFile.FileName);
            artworkModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            artworkModel.UserId = member.MemberId;
            if (exhibit != null)
            {
                artworkModel.ExhibitId = exhibit;

            }
            string path = Path.Combine(wwwRootPath + "/imagesArt/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await artworkModel.ImageFile.CopyToAsync(fileStream);
            }
            context.Add(artworkModel);
            await db.SaveChangesAsync();
            return artworkModel;
        }
        /// <summary>
        /// method that returns all art that a member has posted
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>list of art from one user</returns>
        public async Task<IEnumerable<Artwork>> GetPostedArtFromUniqueUser(string Id)
        {
            IEnumerable<Artwork> art = db.Artworks
                .Where(x => x.UserId == Id && x.ExhibitId == null)
                .Include("Member")
                .ToList();
            await db.SaveChangesAsync();
            return art;
        }

        public async Task<IEnumerable<Artwork>> GetArtToExhibitions(string id)
        {
            IEnumerable<Artwork> art = db.Artworks
                .Where(x => x.UserId == id && x.ExhibitId != null)
                .Include("Member")
                .ToList();
            await db.SaveChangesAsync();
            return art;
        }
        public async Task<IEnumerable<Artwork>> GetAllArtToExhibitions()
        {
            IEnumerable<Artwork> art = db.Artworks
                .Where(x => x.ExhibitId != null)
                .Include("Exhibit")
                .Include("Member")
                .ToList();
            await db.SaveChangesAsync();
            ArtToExhibits = art;
            return ArtToExhibits;
        }
        public async Task<List<ArtworkInformation>> GetAllInformation(string Id)
        {
            //TODO: kanske inte behöver artworkInformation? använda include istället
            IEnumerable<Artwork> artwork = db.Artworks.Where(x => x.UserId == Id);
          
            await db.SaveChangesAsync();
            foreach (var item in artwork)
            {
                ArtworkInformation.Add(new ArtworkInformation()
                {
                    Description = item.Description,
                    Source = item.ImageName,

                }) ;
            }
            
            return ArtworkInformation.ToList();
        }
        public Artwork GetArtworkForUser(int id)
        {
            Artwork artwork = db.Artworks.Where(x => x.ArtworkId == id).FirstOrDefault();
            return artwork;
        }
        public async Task<Artwork> DeleteArtworkFromArtworkTable(IWebHostEnvironment hostEnvironment, Artwork artwork)
        {
            db.Artworks.Remove(artwork);
            await db.SaveChangesAsync();
            string wwwRootPath = hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/imagesArt/", artwork.ImageName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                File.Delete(path);
            }
           
            return artwork;
        }
        public async Task<Artwork> DeleteArtworkFromExhibit(IWebHostEnvironment hostEnvironment, string artwork)
        {
            Artwork art = db.Artworks.Where(x => x.ImageName == artwork).FirstOrDefault();
            db.Artworks.Remove(art);
            await db.SaveChangesAsync();
            string wwwRootPath = hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/imagesArt/", artwork);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                File.Delete(path);
            }

            return art;
        }

        public Artwork GetArtworkThatsGonnaBeDeleted(int id)
        {
            throw new NotImplementedException();
        }
    }
}
