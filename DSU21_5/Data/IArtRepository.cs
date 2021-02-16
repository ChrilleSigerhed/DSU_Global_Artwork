using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IArtRepository
    {
        Task<IEnumerable<Artwork>> GetArtThatsPosted();
        Task<Artwork> AddArt(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork imageModel, Member member, Exhibit exhibit); //TODO: Kolla varför interface inte klagar på att man inte implementerat alla metoder
        Task<IEnumerable<Artwork>> GetPostedArtFromUniqueUser(Member member);
        Task<Artwork> DeleteArtworkFromArtworkTable(IWebHostEnvironment webHostEnvironment, Artwork artwork);
        Task<List<Artwork>> GetExhibitArt(List<Exhibit> exhibits);
        Task<ArtworkViewModel> GetViewModel(List<Member> members);
        Task<Exhibit> CreateExhibit(ImageDbContext context, Member member);
        Task<List<Artwork>> GetArtFromExhibit(Member member);
        Task<ObservableCollection<ArtworkInformation>> GetArtConnectedToExhibit(List<string> ids);
        Task<List<Exhibit>> GetUniqueIdsConnectedToExhibit();
        bool CheckIfIdExists(Member member);
        Exhibit GetExhibitId(Member member);
        Task<Artwork> AddArtWithExistingExhibitId(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, Exhibit exhibit);
        Task<IEnumerable<Artwork>> GetAllArtToExhibitions();
        Task<Exhibit> UpdateExhibition(Member member, Exhibit exhibit);
        Artwork GetArtworkForUser(int id);
        Task<Artwork> DeleteArtworkFromExhibit(IWebHostEnvironment hostEnvironment, string artwork);
        Task<List<Exhibit>> GetExhibits();
        Task<List<Artwork>> GetExhibitArt(Exhibit exhibit);

    }
}
