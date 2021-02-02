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
    public class ShowroomController : Controller
    {
        private IArtRepository artRepository;
        public ShowroomViewModel viewModel;
        public ShowroomController(IArtRepository artRepository)
        {
            this.artRepository = artRepository;
        }
        [Route("Showroom")]
        public async Task<IActionResult> Index(string id)
        {
            id = "aca28772-e443-4b6f-a7bb-6088c20131e6";
            var listfuck = await artRepository.GetPostedArtFromUniqueUser(id);
            
            return View(new ShowroomViewModel(listfuck.ToList()));
        }
    }
}
