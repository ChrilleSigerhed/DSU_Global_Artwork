using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class RelationshipViewModel
    {
        public string UserId { get; set; }
        public List<Member> PendingFriends { get; set; }
        public List<Member> AcceptedFriends { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Image ProfileImage { get; set; }

        public RelationshipViewModel()
        {
            foreach (var pendingFriend in PendingFriends)
            {
                Firstname = pendingFriend.Firstname;
                Lastname = pendingFriend.Lastname;
            }
        }
    }
}
