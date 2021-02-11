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

        public string GetCurrentUserId()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            return currentUserId;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var pendingFriendsRelationships = await RelationshipRepository.GetPendingRelationship(userId);
            var acceptedFriendsRelationships = await RelationshipRepository.GetRelationshipsByUserId(userId);
            List<Member> members = await MemberRepository.GetAllMembers();


            var pendingFriends = new List<Member>();

            foreach (var pending in pendingFriendsRelationships)
            {
                var member = await MemberRepository.GetMember(pending.Requester);
                pendingFriends.Add(member);
            }

            var acceptedFriends = new List<Member>();

            foreach (var friend in acceptedFriendsRelationships)
            {
                Member member;
                if (friend.Requester == userId)
                    member = await MemberRepository.GetMember(friend.Requestee);
                else
                    member = await MemberRepository.GetMember(friend.Requester);

                acceptedFriends.Add(member);
            }
            var viewModel = new RelationshipViewModel()
            {
                AcceptedFriends = acceptedFriends,
                PendingFriends = pendingFriends,
                Members = members
            };

            return View(viewModel);

        }

        
        public async Task<IActionResult> SendFriendRequest(string id)
        {
            Relationship relationship = new Relationship()
            {
                Requester = GetCurrentUserId(),
                Requestee = id
            };

            await RelationshipRepository.Create(relationship);

            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> AcceptFriendRequest(string id)
        {
            string requestee = GetCurrentUserId();
            string requester = id;
            await RelationshipRepository.AcceptRelationshipRequest(requester, requestee);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeclineFriendRequest(string id)
        {
            string requestee = GetCurrentUserId();
            string requester = id;
            await RelationshipRepository.DenyRelationshipRequest(requester, requestee);

            return RedirectToAction("Index");

        }
    }
}
