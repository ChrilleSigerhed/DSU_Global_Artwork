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
       private IMemberRepository memberRepository;
        public ShowroomViewModel viewModel;
        public ShowroomController(IArtRepository artRepository, IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
            this.artRepository = artRepository;
        }
        [Route("Showroom")]
        public async Task<IActionResult> Index(int Id)
        {
            List<Exhibit> exhibits = await artRepository.GetExhibits();
            Exhibit exhibit = exhibits.Where(x => x.Id == Id).FirstOrDefault(); 
            List<Artwork> exhibitArt = await artRepository.GetExhibitArt(exhibit);

            
            return View(new ShowroomViewModel(exhibitArt, exhibit, exhibits));
        }
    }
}
