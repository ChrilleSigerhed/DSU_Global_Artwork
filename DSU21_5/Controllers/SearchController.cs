using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Data;
using DSU21_5.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class SearchController : Controller
    {
        public IMemberRepository MemberRepository { get; set; }

        public SearchController(IMemberRepository memberRepository)
        {
            MemberRepository = memberRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Member> listOfMembers = await MemberRepository.GetAllMembers();
            return View(listOfMembers);
        }
    }
}
