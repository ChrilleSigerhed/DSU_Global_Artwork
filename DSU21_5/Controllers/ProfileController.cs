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

namespace DSU21_5.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IImageRepository ImageRepository { get; set; }
        public IMemberRepository MemberRepository { get; set; }
        public IArtRepository ArtRepository { get; set; }


        public ProfileController(ImageDbContext context, IWebHostEnvironment hostEnvironment, IImageRepository imageRepository, IMemberRepository memberRepository, IArtRepository artRepository)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            ImageRepository = imageRepository;
            MemberRepository = memberRepository;
            ArtRepository = artRepository;
        }

        // GET: Profile
        public IActionResult Index(string Id)
        {
           var image=  ImageRepository.GetImageFromDb(Id);
            if(image != null)
            {
                return View(image);
            }
            else
            {
                image = new Image()
                {
                    ImageName = "profile.jpeg"
                };
                return View(image);
            }
           // return View(await _context.Images.ToListAsync());
        }

        // GET: Profile/Create
        public IActionResult Create(string Id)
        {
            // ImageRepository.CreateNewProfilePicture(_context, _hostEnvironment, Id);l
            var image = ImageRepository.GetImageFromDb(Id);
            return View(image);
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            catch(Exception ex)
            {
                //TODO: Fixa en errorsida
                return View("Error", ex);
            }
            return RedirectToAction($"Index", new {Id});

        }
        //TODO: Skapa CreateArtView
        public IActionResult CreateArt(string Id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArt([Bind("ImageId,ImageFile,UserId")] Artwork imageModel, string Id)
        {
            Member member = await MemberRepository.GetMember(Id);
            try
            {
                if (ModelState.IsValid)
                {
                    var artwork = await ArtRepository.AddArt( _context,_hostEnvironment, imageModel, member);
                }
            }
            catch (Exception ex)
            {
                //TODO: Fixa en errorsida
                return View();
            }
            return RedirectToAction($"Index", new { Id });
        }
    }
}


