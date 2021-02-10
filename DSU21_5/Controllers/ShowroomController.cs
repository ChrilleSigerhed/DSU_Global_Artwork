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


            List<ArtworkInformation> artToExhibits = new List<ArtworkInformation>();
            List<Member> exhibitMembers = new List<Member>();

            Id = "770613df-f24e-4aff-b9f8-c56d2be98ca1";
            var postedArt = await artRepository.GetAllArtToExhibitions();
            //var postedArt = await artRepository.GetArtFromExhibit(Id);
            var members = await memberRepository.GetAllMembers();
            var member = await memberRepository.GetMember(Id);
         
            
            return View(new ShowroomViewModel(postedArt.ToList(), member, members));
        }
    }
}
