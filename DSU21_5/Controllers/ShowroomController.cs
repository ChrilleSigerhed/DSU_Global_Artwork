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

            Id = "638aa03d-c00c-4c9f-8e3e-2206b57f404d";
            var postedArtMALIN = await artRepository.GetAllArtToExhibitions();
            var membersMALIN = await memberRepository.GetAllMembers();

            //Id = "aca28772-e443-4b6f-a7bb-6088c20131e6";
            var postedArt = await artRepository.GetPostedArtFromUniqueUser(Id);
            var member = await memberRepository.GetMember(Id);
         
            
            return View(new ShowroomViewModel(postedArt.ToList(), member, members));
        }
    }
}
