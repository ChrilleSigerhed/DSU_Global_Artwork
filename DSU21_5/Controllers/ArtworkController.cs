using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Data;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class ArtworkController : Controller
    {
        public IArtRepository ArtRepository { get; set; }

        public ArtworkController(IArtRepository artRepository)
        {
            ArtRepository = artRepository;
        }
        public IActionResult Index()
        {
            var art = ArtRepository.GetArtThatsPosted();
            return View(art);
        }
    }
}
