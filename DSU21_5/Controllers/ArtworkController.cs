using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Data;
using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class ArtworkController : Controller
    {
        public IArtRepository ArtRepository { get; set; }
        public IMemberRepository MemberRepository { get; set; }


        public ArtworkViewModel ArtworkViewModel;

        public ArtworkController(IArtRepository artRepository, IMemberRepository memberRepository)
        {
            ArtRepository = artRepository;
            MemberRepository = memberRepository;
        }
        public async Task<IActionResult> Index(string Id)
        {
            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted();
            ArtworkViewModel = new ArtworkViewModel(listOfArtworks, member);
            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> Category(string Id)
        {
            List<Artwork> ArtList = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted();

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == Id)
                {
                    ArtList.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(ArtList, member);
            
            return View(Id, ArtworkViewModel);
        }
    }
}
