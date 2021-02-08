using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSU21_5.Areas.Identity.Data;
using DSU21_5.Data;
using DSU21_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DSU21_5.Controllers
{
    public class SearchController : Controller
    {
        public IMemberRepository MemberRepository { get; set; }
        public IRelationshipRepository RelationshipRepository { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;


        public SearchController(IMemberRepository memberRepository, IRelationshipRepository relationshipRepository, UserManager<ApplicationUser> userManager)
        {
            MemberRepository = memberRepository;
            RelationshipRepository = relationshipRepository;
            _userManager = userManager;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            var pendingFriends = await RelationshipRepository.GetPendingRelationship(id);
            return View(pendingFriends);

            //List<Member> listOfMembers = await MemberRepository.GetAllMembers();
            //return View(listOfMembers);
        }

        //[HttpPost]
        //public async Task<IActionResult> SendFriendRequest(/*Relationship relationship*/ )
        //{
        //    //await RelationshipRepository.Create(relationship);
        //    return View();
        //}
    }
}
