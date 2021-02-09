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
        Task<IEnumerable<Artwork>> GetPostedArtFromUniqueUser(string Id);
        Task<Artwork> DeleteArtworkFromArtworkTable(IWebHostEnvironment webHostEnvironment, Artwork artwork);
        Artwork GetArtworkThatsGonnaBeDeleted(int id);
        Task<ArtworkViewModel> GetViewModel(List<Member> members);
        Task<List<ArtworkInformation>> GetAllInformation(string Id);
        Task<Exhibit> CreateExhibit(ImageDbContext context, Member member);
        Task<IEnumerable<Artwork>> GetArtToExhibitions(string id);
        Task<List<Artwork>> GetArtFromExhibit(string id);
        Task<ObservableCollection<ArtworkInformation>> GetArtConnectedToExhibit(List<string> ids);
        Task<List<string>> GetUniqueIdsConnectedToExhibit();
        bool CheckIfIdExists(string id);
        int? GetExhibitId(string id);
        Task<Artwork> AddArtWithExistingExhibitId(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork artworkModel, Member member, int? exhibit);
        Task<IEnumerable<Artwork>> GetAllArtToExhibitions();
        Task<Exhibit> UpdateExhibition(string id, Exhibit exhibit);

    }
}
