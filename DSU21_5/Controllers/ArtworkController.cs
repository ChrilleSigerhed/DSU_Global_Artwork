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
            IEnumerable<Artwork> listOfMovies = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?
            ArtworkViewModel = new ArtworkViewModel(listOfMovies, member);
            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> DigitalArt()
        {
            List<Artwork> DigitalArt = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == "DigitalArt")
                {
                    DigitalArt.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(DigitalArt, member);
            
            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> Drawing()
        {
            List<Artwork> Drawings = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == "Drawing")
                {
                    Drawings.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(Drawings, member);

            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> Painting()
        {
            List<Artwork> Paintings = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == "Painting")
                {
                    Paintings.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(Paintings, member);

            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> Photography()
        {
            List<Artwork> Photos = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == "Photography")
                {
                    Photos.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(Photos, member);

            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> ReadyMade()
        {
            List<Artwork> ReadyMades = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == "ReadyMade")
                {
                    ReadyMades.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(ReadyMades, member);

            return View(ArtworkViewModel);
        }

        public async Task<IActionResult> Sculpture()
        {
            List<Artwork> Sculptures = new List<Artwork>();

            var member = await MemberRepository.GetAllMembers();
            IEnumerable<Artwork> listOfArtworks = await ArtRepository.GetArtThatsPosted(); //TODO: ListOfMovies?

            foreach (Artwork piece in listOfArtworks)
            {
                if (piece.Type == "Sculpture")
                {
                    Sculptures.Add(piece);
                }
            }

            ArtworkViewModel = new ArtworkViewModel(Sculptures, member);

            return View(ArtworkViewModel);
        }
    }
}
