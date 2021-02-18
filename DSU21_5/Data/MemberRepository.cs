using DSU21_5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class MemberRepository : IMemberRepository
    {
        public ImageDbContext db { get; set; }
        public MemberRepository(ImageDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// method that adds a member to db
        /// </summary>
        /// <param name="member"></param>
        /// <returns>a member</returns>
        public async Task<Member> AddMember(Member member)
        {
            db.Add(member);
            await db.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// method that gets a member from db
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>a member</returns>
        public async Task<Member> GetMember(string Id)
        {
            Member member = db.Members.Where(x => x.MemberId == Id).FirstOrDefault();
            await db.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// method that gets a list of all members from db
        /// </summary>
        /// <returns>a list of members</returns>      
        public List<Member> GetAllMembers()
        {

             var members = db.Members.ToList();
             return members;
        }

        /// <summary>
        /// updates member's bio
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="bio"></param>
        /// <returns>an updated member's bio</returns>
        public async Task<Member> UpdateBio(string Id, string bio)
        {
            var update = db.Members.Where(x => x.MemberId == Id).FirstOrDefault();
            update.Bio = bio;
            db.Update(update);

            await db.SaveChangesAsync();
            return update;
        }

        /// <summary>
        /// updates member's facebook link
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="facebook"></param>
        /// <returns>an updated member's facebook link</returns>
        public async Task<Member> UpdateFacebook(string Id, string facebook)
        {
            var update = db.Members.Where(x => x.MemberId == Id).FirstOrDefault();
            update.Facebook = facebook;
            db.Update(update);

            await db.SaveChangesAsync();
            return update;
        }

        /// <summary>
        /// updates member's twitter link
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="twitter"></param>
        /// <returns>an updated member's twitter link</returns>
        public async Task<Member> UpdateTwitter(string Id, string twitter)
        {
            var update = db.Members.Where(x => x.MemberId == Id).FirstOrDefault();
            update.Twitter = twitter;
            db.Update(update);

            await db.SaveChangesAsync();
            return update;
        }

        /// <summary>
        /// updates member's instagram link
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="instagram"></param>
        /// <returns>an updated member's instagram link</returns>
        public async Task<Member> UpdateInstagram(string Id, string instagram)
        {
            var update = db.Members.Where(x => x.MemberId == Id).FirstOrDefault();
            update.Instagram = instagram;
            db.Update(update);

            await db.SaveChangesAsync();
            return update;
        }
    }
}
