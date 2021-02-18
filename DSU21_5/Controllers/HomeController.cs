using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DSU21_5.Models;
using DSU21_5.Data;

namespace DSU21_5.Controllers
{
    public class HomeController : Controller
    {
        private IArtRepository ArtRepository { get; set; }
        private IMemberRepository MemberRepository { get; set; }


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IArtRepository artRepository, IMemberRepository memberRepository)
        {
            _logger = logger;
            ArtRepository = artRepository;
            MemberRepository = memberRepository;
        }

        public async Task<IActionResult> Index()
        {
           var members =  MemberRepository.GetAllMembers();
           var model = await ArtRepository.GetViewModel(members);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
