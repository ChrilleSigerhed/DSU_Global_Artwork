using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Areas.Identity.Data;
using DSU21_5.Data;
using DSU21_5.Models;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class CommunityController : Controller
    {
        public IMemberRepository MemberRepository {get; set;}
        public IImageRepository ImageRepository { get; set; }
        public IArtRepository ArtRepository { get; set; }
        public ProfileViewModel ProfileViewModel { get; set; }
        public IRelationshipRepository RelationshipRepository { get; set; }
        public CommunityViewModel CommunityViewModel { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;


        public CommunityController(IMemberRepository memberRepository, IImageRepository imageRepository, IArtRepository artRepository, IRelationshipRepository relationshipRepository, UserManager<ApplicationUser> userManager)
        {
            MemberRepository = memberRepository;
            ImageRepository = imageRepository;
            ArtRepository = artRepository;
            RelationshipRepository = relationshipRepository;
            _userManager = userManager;


        }

        public string GetCurrentUserId()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            return currentUserId;
        }

        public async Task<IActionResult> Index()
        {
            //List<Member> members = await MemberRepository.GetAllMembers();
            List<Member> listOfMembers = await MemberRepository.GetAllMembers();
            List<Image> listOfImages = ImageRepository.GetAllImagesFromDbConnectedToUsers(listOfMembers);




            //var viewModel = new CommunityViewModel(listOfImages, members)
            //{
            //    Members = members
            //};

            //return View(viewModel);



            CommunityViewModel = new CommunityViewModel(listOfImages, listOfMembers);
            return View(CommunityViewModel);
        
        }
    
        public async Task<IActionResult> Profile(string Id)
        {
            var art = await ArtRepository.GetArtFromExhibit(Id);
            var test = await ArtRepository.GetUniqueIdsConnectedToExhibit();
            var test1 = await ArtRepository.GetArtConnectedToExhibit(test);
            Image image = ImageRepository.GetImageFromDb(Id);
            Member member = await MemberRepository.GetMember(Id);
            IEnumerable<Artwork> artwork = await ArtRepository.GetPostedArtFromUniqueUser(Id);
            ProfileViewModel = new ProfileViewModel(artwork, member, image, test1, art);
            return View(ProfileViewModel);
        
        }
        public async Task<IActionResult> SendFriendRequest(string id)
        {
            Relationship relationship = new Relationship()
            {
                Requester = GetCurrentUserId(),
                Requestee = id
            };
            var relationshipExists = await RelationshipRepository.CheckIfRelationshipAlreadyExists(relationship);

            if (!relationshipExists)
            {
                await RelationshipRepository.Create(relationship);
            }

            return RedirectToAction("Profile", new { id });
        }

    }
}
