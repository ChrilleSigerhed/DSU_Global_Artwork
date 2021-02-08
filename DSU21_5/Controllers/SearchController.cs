using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var pendingFriendsRelationships = await RelationshipRepository.GetPendingRelationship(userId);
            var acceptedFriendsRelationships = await RelationshipRepository.GetRelationshipsByUserId(userId);

            var pendingFriends = new List<Member>();

            foreach (var pending in pendingFriendsRelationships)
            {
                var member = await MemberRepository.GetMember(pending.UserId2);
                pendingFriends.Add(member);
            }

            var acceptedFriends = new List<Member>();

            foreach (var friend in acceptedFriendsRelationships)
            {
                Member member;
                if (friend.UserId1 == userId)
                    member = await MemberRepository.GetMember(friend.UserId2);
                else
                    member = await MemberRepository.GetMember(friend.UserId1);

                acceptedFriends.Add(member);
            }

            var viewModel = new RelationshipViewModel
            {
                UserId = userId,
                PendingFriends = pendingFriends,
                AcceptedFriends = acceptedFriends
            };

            return View(viewModel);

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
