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
        public IRelationshipRepository RelationshipRepository { get; set; }

        public SearchController(IMemberRepository memberRepository, IRelationshipRepository relationshipRepository)
        {
            MemberRepository = memberRepository;
            RelationshipRepository = relationshipRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Member> listOfMembers = await MemberRepository.GetAllMembers();
            return View(listOfMembers);
        }

        //[HttpPost]
        //public async Task<IActionResult> SendFriendRequest(string id1, string id2)
        //{
        //    var friendsList = await RelationshipRepository.AcceptRelationshipRequest(id1, id2);
        //    return View();
        //}
    }
}
