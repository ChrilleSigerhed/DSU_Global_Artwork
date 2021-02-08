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
        public async Task<IActionResult> Index(string Id)
        {
            //Id = "da83057b-424a-419c-994c-ab8c12208c9f";
            var postedArt = await artRepository.GetPostedArtFromUniqueUser(Id);
            var member = await memberRepository.GetMember(Id);
            
            return View(new ShowroomViewModel(postedArt.ToList(), member));
        }
    }
}
