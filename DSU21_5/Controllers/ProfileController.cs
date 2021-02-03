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

namespace DSU21_5.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IImageRepository ImageRepository { get; set; }
        public IMemberRepository MemberRepository { get; set; }
        public IArtRepository ArtRepository { get; set; }
        public ProfileViewModel ProfileViewModel { get; set; }


        public ProfileController(ImageDbContext context, IWebHostEnvironment hostEnvironment, IImageRepository imageRepository, IMemberRepository memberRepository, IArtRepository artRepository)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            ImageRepository = imageRepository;
            MemberRepository = memberRepository;
            ArtRepository = artRepository;
        }

        [Route("/Profile/Index/{Id}")]
        public async Task<IActionResult> Index(string Id)
        {
            Image image = ImageRepository.GetImageFromDb(Id);
            Member member = await MemberRepository.GetMember(Id);
            IEnumerable<Artwork> artwork = await ArtRepository.GetPostedArtFromUniqueUser(Id);
            ProfileViewModel = new ProfileViewModel(artwork, member, image);
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
                //TODO: Fixa en errorsida
                return View("Error", ex);
            }
            return RedirectToAction($"Index", new { Id });

        }
        public IActionResult CreateArt(string Id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArt([Bind("ImageId,ImageFile,UserId, Description, ArtName")] Artwork imageModel, string Id)
        {
            Member member = await MemberRepository.GetMember(Id);
            try
            {
                if (ModelState.IsValid)
                {
                    var artwork = await ArtRepository.AddArt(_context, _hostEnvironment, imageModel, member);
                }
            }
            catch (Exception ex)
            {
                //TODO: Fixa en errorsida
                return View(ex);
            }
            return RedirectToAction($"Index", new { Id });
        }

        [HttpPost, ActionName("DeleteArt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArtConfirm(int Id)
        {
            try
            {
                Artwork artwork = ArtRepository.GetArtworkThatsGonnaBeDeleted(Id);
                await ArtRepository.DeleteArtworkFromArtworkTable(_hostEnvironment, artwork);
            }
            catch (Exception ex)
            {
                //TODO: Fixa en errorsida
                return View(ex);
            }
            return Json(Id);
        }
    }
}


