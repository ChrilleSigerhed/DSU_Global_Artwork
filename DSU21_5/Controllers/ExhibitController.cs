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
    public class ExhibitController : Controller
    {
        private IArtRepository artRepository;
       
        public ExhibitController(IArtRepository artRepository)
        {
            this.artRepository = artRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            List<Exhibit> exhibits = await artRepository.GetExhibits();
            var artworks = await artRepository.GetAllArtToExhibitions();

            List<Artwork> listOfArt = artworks.ToList();
            listOfArt = artworks.ToList();
            ExhibitViewModel viewModel = new ExhibitViewModel(exhibits, listOfArt);

            return View(viewModel);
        }
    }
}
