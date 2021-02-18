using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Data;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class ArtworkDetailController : Controller
    {
        private IArtRepository ArtRepository { get; set; }
        private IMemberRepository MemberRepository { get; set; }
        public ArtworkDetailViewModel artworkDetailViewModel { get; set; }
        public ArtworkDetailController(IArtRepository artRepository, IMemberRepository memberRepository)
        {
            ArtRepository = artRepository;
            MemberRepository = memberRepository;
        }
        public async Task<IActionResult> Index(int Id)
        {
            var artwork = ArtRepository.GetArtworkForUser(Id);
            var allArtworksFromUser = await ArtRepository.GetPostedArtFromUniqueUser(artwork.Member);

            artworkDetailViewModel = new ArtworkDetailViewModel(allArtworksFromUser,artwork, artwork.Member);
            return View(artworkDetailViewModel);
        }
    }
}
