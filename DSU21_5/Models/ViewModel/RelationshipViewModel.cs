using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class RelationshipViewModel
    {
        public string Requester { get; set; }
        public string Requestee { get; set; }
        public List<Member> PendingFriends { get; set; }
        public List<Member> AcceptedFriends { get; set; }
        public List<Relationship> Relationships { get; set; }
        public List<Member> Members { get; set; }
        public Relationship Relationship { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Image ProfileImage { get; set; }

        public RelationshipViewModel(/*List<Member> pendingFriends*/)
        {
            //this.PendingFriends = pendingFriends.Select(x => new Member
            //{
            //    Firstname = x.Firstname,
            //    Lastname = x.Lastname
            //}).ToList();
        }
    }
}
