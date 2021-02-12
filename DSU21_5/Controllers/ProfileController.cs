using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSU21_5.Data;
using DSU21_5.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using DSU21_5.Areas.Identity.Data;
using DSU21_5.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace DSU21_5.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IImageRepository ImageRepository { get; set; }
        public IMemberRepository MemberRepository { get; set; }
        public IArtRepository ArtRepository { get; set; }
        public IRelationshipRepository RelationshipRepository { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileViewModel ProfileViewModel { get; set; }

        public ProfileController(IImageDbContext context, IWebHostEnvironment hostEnvironment, IImageRepository imageRepository, IMemberRepository memberRepository, IArtRepository artRepository, IRelationshipRepository relationshipRepository, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            ImageRepository = imageRepository;
            MemberRepository = memberRepository;
            ArtRepository = artRepository;
            RelationshipRepository = relationshipRepository;
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            return currentUserId;
        }

        [Route("/Profile/Index/{Id}")]
        public async Task<IActionResult> Index(string Id)
        {
            var userId = GetCurrentUserId();
            var acceptedFriendsRelationships = await RelationshipRepository.GetRelationshipsByUserId(userId);
            var pendingFriendsRelationships = await RelationshipRepository.GetPendingRelationship(userId);
            List<Member> members = await MemberRepository.GetAllMembers();
            var art = await ArtRepository.GetArtFromExhibit(Id);
            var test = await ArtRepository.GetUniqueIdsConnectedToExhibit();
            var test1 = await ArtRepository.GetArtConnectedToExhibit(test);
            Image image = ImageRepository.GetImageFromDb(Id);
            Member member = await MemberRepository.GetMember(Id);
            IEnumerable<Artwork> artwork = await ArtRepository.GetPostedArtFromUniqueUser(Id);


            var pendingFriends = new List<Member>();

            foreach (var pending in pendingFriendsRelationships)
            {
                member = await MemberRepository.GetMember(pending.Requester);
                pendingFriends.Add(member);
            }

            var acceptedFriends = new List<Member>();

            foreach (var friend in acceptedFriendsRelationships)
            {
                //Member member;
                if (friend.Requester == userId)
                    member = await MemberRepository.GetMember(friend.Requestee);
                else
                    member = await MemberRepository.GetMember(friend.Requester);

                acceptedFriends.Add(member);
            }



            ProfileViewModel = new ProfileViewModel(artwork, member, image, test1, art)
            {
                AcceptedFriends = acceptedFriends,
                PendingFriends = pendingFriends
            };
            return View(ProfileViewModel);
        }

        public IActionResult Create(string Id)
        {
            var image = ImageRepository.GetImageFromDb(Id);
            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,ImageFile,UserId")] Image imageModel, string Id)
        {
            var image = imageModel;
            try
            {
                if (ModelState.IsValid)
                {
                    var checkIfUserHadProfilePictureAlready = ImageRepository.GetImageFromDb(Id);
                    if (checkIfUserHadProfilePictureAlready != null)
                    {
                        ImageRepository.RemoveImageFromDb(_hostEnvironment, checkIfUserHadProfilePictureAlready);
                    }
                    image = await ImageRepository.CreateNewProfilePicture(_context, _hostEnvironment, imageModel, Id);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return RedirectToAction($"Edit", new { Id });

        }
        public async Task<IActionResult> CreateArt(string Id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArt([Bind("Artwork")] ProfileViewModel profileView, string Id)
        {
            Artwork imageModel = profileView.Artwork;
            Member member = await MemberRepository.GetMember(Id);
            Exhibit exhibit = null;

            var category = Request.Form["category"];
            imageModel.Type = category;
            try
            {
                if (ModelState.IsValid)
                { 
                        var artwork = await ArtRepository.AddArt(_context, _hostEnvironment, imageModel, member, exhibit);
                }
            }
            catch (Exception ex)
            {

                return View("Error", ex);

            }
            return RedirectToAction($"Edit", new { Id });
        }

        [HttpPost, ActionName("DeleteArt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArtConfirm(int Id)
        {
            try
            {
                Artwork artwork = ArtRepository.GetArtworkForUser(Id);
                await ArtRepository.DeleteArtworkFromArtworkTable(_hostEnvironment, artwork);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return Json(Id);
        }

        [HttpPost("Profile/DeleteArtConfirmExhibit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArtConfirmExhibit(string Id)
        {
            try
            {
                Artwork artwork = await ArtRepository.DeleteArtworkFromExhibit(_hostEnvironment, Id);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return Json(Id);
        }

        public async Task<IActionResult> CreateExhibition(string Id)
        {
            var art = await ArtRepository.GetArtFromExhibit(Id);
            var test = await ArtRepository.GetUniqueIdsConnectedToExhibit();
            var listOfArtFromExhibit = await ArtRepository.GetArtConnectedToExhibit(test);
            Image image = ImageRepository.GetImageFromDb(Id);
            Member member = await MemberRepository.GetMember(Id);
            IEnumerable<Artwork> artwork = await ArtRepository.GetPostedArtFromUniqueUser(Id);
            ProfileViewModel = new ProfileViewModel(artwork, member, image, listOfArtFromExhibit, art);
            return View(ProfileViewModel);
        }
        [HttpPost("/Profile/CreateExhibition/{Id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExhibition([Bind("Artwork")] ProfileViewModel profileView, string Id)
        {
            Artwork imageModel = profileView.Artwork;
      
            var selected = Request.Form.Files[0];
            var category = Request.Form["category"];
           
            imageModel.Type = category;
            imageModel.ImageFile = selected;
            imageModel.ImageName = selected.FileName;
            Member member = await MemberRepository.GetMember(Id);
            bool exist = ArtRepository.CheckIfIdExists(Id);

            Exhibit exhibit = null;
            try
            {
                if (ModelState.IsValid)
                { 
                    if (exist == true)
                    {
                        var exhibitId = ArtRepository.GetExhibitId(Id);
                        var artwork = await ArtRepository.AddArtWithExistingExhibitId(_context, _hostEnvironment, imageModel, member, exhibitId);
                    }
                    else if (exist == false)
                    {
                        exhibit = await ArtRepository.CreateExhibit(_context, member);
                        var artwork = await ArtRepository.AddArt(_context, _hostEnvironment, imageModel, member, exhibit);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Json(imageModel.ImageName);
        }
        [HttpPost("/Profile/UploadExhibition")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadExhibition([Bind("Exhibit, Member")] ProfileViewModel profileView)
        {
        
            Exhibit exhibit = profileView.Exhibit;
            var startDate = Request.Form["trip-start"];
            var stopDate = Request.Form["trip-stop"];
            exhibit.StartDate = startDate;
            exhibit.StopDate = stopDate;
            exhibit.MemberId = profileView.Member.MemberId;
            bool exist = ArtRepository.CheckIfIdExists(profileView.Member.MemberId);

            try
            {
                if (ModelState.IsValid)
                {
                    if (exist == true)
                    {
                        await ArtRepository.UpdateExhibition(profileView.Member.MemberId, exhibit);
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            string Id = profileView.Member.MemberId;
            return RedirectToAction($"Index", new { Id });
        }

        public async Task<IActionResult> Edit(string Id)
        {
            Image image = ImageRepository.GetImageFromDb(Id);
            Member member = await MemberRepository.GetMember(Id);
            IEnumerable<Artwork> artwork = await ArtRepository.GetPostedArtFromUniqueUser(Id);
            ProfileViewModel = new ProfileViewModel(artwork, member, image);
            return View(ProfileViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Bio")] Member member, string Id)
        {
            var task = await MemberRepository.UpdateBio(Id, member.Bio);
            return Json(member.Bio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFacebook([Bind("Facebook")] Member member, string Id)
        {
            var task = await MemberRepository.UpdateFacebook(Id, member.Facebook);
            return Json(member.Facebook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTwitter([Bind("Twitter")] Member member, string Id)
        {
            var task = await MemberRepository.UpdateTwitter(Id, member.Twitter);
            return Json(member.Twitter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInstagram([Bind("Instagram")] Member member, string Id)
        {
            var task = await MemberRepository.UpdateInstagram(Id, member.Instagram);
            return Json(member.Instagram);
        }


        public async Task<IActionResult> AcceptFriendRequest(string id)
        {
            string Id = GetCurrentUserId();
            string requester = id;
            await RelationshipRepository.AcceptRelationshipRequest(requester, Id);

            return RedirectToAction("Index", new { Id });
        }


        public async Task<IActionResult> DeclineFriendRequest(string id)
        {
            string Id = GetCurrentUserId();
            string requester = id;
            await RelationshipRepository.DenyRelationshipRequest(requester, Id);

            return RedirectToAction("Index", new { Id });

        }
    }
}


