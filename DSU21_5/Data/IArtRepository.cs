using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IArtRepository
    {
        Task<IEnumerable<Artwork>> GetArtThatsPosted();
        Task<Artwork> AddArt(ImageDbContext context, IWebHostEnvironment hostEnvironment, Artwork imageModel, Member member); //TODO: Kolla varför interface inte klagar på att man inte implementerat alla metoder
        Task<IEnumerable<Artwork>> GetPostedArtFromUniqueUser(string Id);
        Task<Artwork> DeleteArtworkFromArtworkTable(IWebHostEnvironment webHostEnvironment, Artwork artwork);
        Artwork GetArtworkThatsGonnaBeDeleted(int id);


    }
}
