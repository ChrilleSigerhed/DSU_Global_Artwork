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
    public class CommunityController : Controller
    {
        public IMemberRepository MemberRepository {get; set;}
        public IImageRepository ImageRepository { get; set; }
        public CommunityViewModel CommunityViewModel { get; set; }

        public CommunityController(IMemberRepository memberRepository, IImageRepository imageRepository)
        {
            MemberRepository = memberRepository;
            ImageRepository = imageRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Member> listOfMembers = await MemberRepository.GetAllMembers();
            List<Image> listOfImages = ImageRepository.GetAllImagesFromDbConnectedToUsers(listOfMembers);
            
            CommunityViewModel = new CommunityViewModel(listOfImages, listOfMembers);
            return View(CommunityViewModel);
        }
    }
}
