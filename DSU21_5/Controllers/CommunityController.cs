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

        private IMemberRepository MemberRepository { get; set; }
        private IImageRepository ImageRepository { get; set; }
        private IArtRepository ArtRepository { get; set; }
        public ProfileViewModel ProfileViewModel { get; set; }
        private IRelationshipRepository RelationshipRepository { get; set; }
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

        public async Task<IActionResult> Index()
        {
            List<Member> listOfMembers = await MemberRepository.GetAllMembers();
            List<Image> listOfImages = ImageRepository.GetAllImagesFromDbConnectedToUsers(listOfMembers);


            CommunityViewModel = new CommunityViewModel(listOfImages, listOfMembers);
            return View(CommunityViewModel);
        
        }
        public string GetCurrentUserId()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            return currentUserId;
        }
        public async Task<IActionResult> Profile(string Id)
        {
            var currentUserId = GetCurrentUserId();
            Image image = ImageRepository.GetImageFromDb(Id);
            Member member = await MemberRepository.GetMember(Id);
            IEnumerable<Artwork> artwork = await ArtRepository.GetPostedArtFromUniqueUser(member);

            var doesRelationshipExist = await RelationshipRepository.CheckIfRelationshipAlreadyExists(currentUserId, Id);


            ProfileViewModel = new ProfileViewModel(artwork, member, image, doesRelationshipExist, currentUserId);
            return View(ProfileViewModel);
        }
        public async Task<IActionResult> SendFriendRequest(string id)
        {

            Relationship relationship = new Relationship()
            {
                Requester = GetCurrentUserId(),
                Requestee = id
            };

            var doesRelationshipExist = await RelationshipRepository.CheckIfRelationshipAlreadyExists(relationship.Requester, relationship.Requestee);

            if (!doesRelationshipExist)
            {
                await RelationshipRepository.Create(relationship);
            }
            return RedirectToAction("Profile", new { id });
        }
    }
}
