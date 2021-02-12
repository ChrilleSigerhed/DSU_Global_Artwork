using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Areas.Identity.Data;
using DSU21_5.Data;
using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class ArtworkDetailController : Controller
    {
        public IArtRepository ArtRepository { get; set; }
        public IMemberRepository MemberRepository { get; set; }
        public ArtworkDetailViewModel artworkDetailViewModel { get; set; }
        public IFavouritesRepository FavouritesRepository { get; set; }
        public ArtworkDetailController(IArtRepository artRepository, IMemberRepository memberRepository, IFavouritesRepository favouritesRepository)
        {
            ArtRepository = artRepository;
            MemberRepository = memberRepository;
            FavouritesRepository = favouritesRepository;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var artwork = ArtRepository.GetArtworkForUser(Id);
            var allArtworksFromUser = await ArtRepository.GetPostedArtFromUniqueUser(artwork.UserId);
            var user = await MemberRepository.GetMember(artwork.UserId);

            artworkDetailViewModel = new ArtworkDetailViewModel(allArtworksFromUser, artwork, user);
            return View(artworkDetailViewModel);
        }

        [HttpPost("/ArtworkDetail/EditFavourite/{Id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFavourite([Bind("DetailArtwork")]ArtworkDetailViewModel artwork, string Id)
        {
            int artId = artwork.DetailArtwork.ArtworkId;

            await FavouritesRepository.EditFavourite(Id, artwork.DetailArtwork.ArtworkId);

            return RedirectToAction("Index", Id );
        }
    }
}
