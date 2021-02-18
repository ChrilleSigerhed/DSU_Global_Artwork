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
        private IEnumerable<Artwork> ArtToExhibits { get; set; }


        public ArtRepository(ImageDbContext context)
        {
            db = context;
        }
        /// <summary>
        /// Method that gets all art from one user that is connected to an exhibition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a list of art</returns>
        public List<Artwork> GetArtFromExhibit(Member member)
        {
            var listOfArtFromExhibit = db.Artworks
                .Where(x => x.UserId == member.MemberId && x.ExhibitId != null)
                .Include("Member")
                .Include("Exhibit")
                .ToList();
            return listOfArtFromExhibit;
        }  

        //}
        /// <summary>
        /// Method that checks if an id already exists in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        public bool CheckIfIdExists(Member member)
        {
            if (db.Exhibit.Any(x => x.MemberId == member.MemberId))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Gets the exhibit that is connected to the member
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Exhibit GetExhibitId(Member member)
        {
            var exhibit = db.Exhibit
                .Where(x => x.MemberId == member.MemberId)
                .FirstOrDefault();
            return exhibit;
            
        }
        /// <summary>
        /// Gets the exhibits that are ready to be published and are shown to the visitors
        /// </summary>
        /// <returns></returns>
        public async Task<List<Exhibit>> GetExhibits()
        {
            List<Exhibit> exhibits = new List<Exhibit>();
            exhibits =  db.Exhibit.Where(x => x.Publish == true).ToList();
            await db.SaveChangesAsync();
            return exhibits;
        }
        /// <summary>
        /// After a member adds his first image to the exhibition, the exhibition is created. This method sets the name, start, stop and publish properties-
        /// and confirms that the exhibition is ready to be shown to the other visitors on the page
        /// </summary>
        /// <param name="member"></param>
        /// <param name="exhibit"></param>
        /// <returns></returns>
        public async Task<Exhibit> UpdateExhibition(Member member, Exhibit exhibit)
        {
            var exhibition = db.Exhibit.Where(x => x.MemberId == member.MemberId).FirstOrDefault();
            exhibition.Name = exhibit.Name;
            exhibition.StartDate = exhibit.StartDate;
            exhibition.StopDate = exhibit.StopDate;
            exhibition.Publish = true;
            await db.SaveChangesAsync();
            return exhibition;
          
        }
        /// <summary>
        /// Returns the ViewModel and enables presentation of posted Artworks and Exhibition on the Home page.
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        public async Task<ArtworkViewModel> GetViewModel(List<Member> members)
        {
            var listOfArt = await GetArtThatsPosted();
            var listOfArtFromExhibitions = await GetAllArtToExhibitions();
            List<Exhibit> exhibits = await GetExhibits();
            var model = new ArtworkViewModel(listOfArt, members, exhibits, listOfArtFromExhibitions.ToList());
            return model;
        }
        /// <summary>
        /// Returns a list of artworks that are not connected to any exhibit for display on the page Artwork
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Artwork>> GetArtThatsPosted()
        {
            var listOfArt = db.Artworks
                .Where(x => x.ExhibitId == null)
                .Include("Member")
                .ToList();
            await db.SaveChangesAsync();
            return listOfArt;
        }
        /// <summary>
        /// Method that creates a new exhibition after a member adds a image
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <returns>an exhibition</returns>
        public async Task<Exhibit> CreateExhibit(Member member)
        {
            Exhibit exhibit = new Exhibit()
            {
                Name = "not available",
                MemberId = member.MemberId,
            };
            db.Add(exhibit);
            await db.SaveChangesAsync();
            return exhibit;
        }
        /// <summary>
        /// Adds a new artwork to a members Artwork collection. Stores the Artwork in the database and also adds the image to the imageArt folder
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="artworkModel"></param>
        /// <param name="member"></param>
        /// <param name="exhibit"></param>
        /// <returns></returns>
        public async Task<Artwork> AddArt(IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, Exhibit exhibit)
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
            db.Add(artworkModel);
            await db.SaveChangesAsync();
            return artworkModel;
        }
        /// <summary>
        /// Method that creates a new artwork and adds the existing exhibition-ID
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="artworkModel"></param>
        /// <param name="member"></param>
        /// <param name="exhibit"></param>
        /// <returns>artwork</returns>
        public async Task<Artwork> AddArtWithExistingExhibitId(IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, Exhibit exhibit)
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
            db.Add(artworkModel);
            await db.SaveChangesAsync();
            return artworkModel;
        }
        /// <summary>
        /// method that returns all art that a member has posted
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>list of art from one user</returns>
        public async Task<IEnumerable<Artwork>> GetPostedArtFromUniqueUser(Member member)
        {
            IEnumerable<Artwork> art = db.Artworks
                .Where(x => x.UserId == member.MemberId && x.ExhibitId == null)
                .Include("Member")
                .ToList();
            await db.SaveChangesAsync();
            return art;
        }
        /// <summary>
        /// Gets all art that is connected to Exhibitions
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns artwork that matches the artworkId
        /// </summary>
        /// <param name="artworkId"></param>
        /// <returns></returns>
        public Artwork GetArtworkForUser(int artworkId)
        {
            Artwork artwork = db.Artworks.Where(x => x.ArtworkId == artworkId).Include("Member").FirstOrDefault();
            return artwork;
        }
        /// <summary>
        /// Not implemented, but deletes artwork from database and also removes it from imageArt
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="artwork"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Not implemented, but removes the artwork from exhibition and also removes it from imageArt
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="artwork"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the artworks from selected Exhibit and returns a list containing the artwork connected to that exhibit
        /// </summary>
        /// <param name="exhibit"></param>
        /// <returns></returns>
        public async Task<List<Artwork>> GetExhibitArt(Exhibit exhibit)
        {
            List<Artwork> artworks = new List<Artwork>();
            artworks = db.Artworks.Where(x => x.ExhibitId == exhibit.Id).Include("Member").ToList();
            await db.SaveChangesAsync();
            return artworks;
        }
    }
}
