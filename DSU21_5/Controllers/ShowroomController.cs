using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Data;
using DSU21_5.Models;
using DSU21_5.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class ShowroomController : Controller
    {
        private IImageRepository imageRepository;
        public ShowroomViewModel viewModel;
        public ShowroomController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [Route("Showroom")]
        public async Task<IActionResult> Index()
        {
            var viewModel = await imageRepository.GetShowroomImages();
            return View(viewModel);
        }
    }
}
