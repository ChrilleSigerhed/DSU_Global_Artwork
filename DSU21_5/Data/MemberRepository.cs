﻿using DSU21_5.Models;
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
        
        public async Task<Member> AddMember(Member member)
        {
            db.Add(member);
            await db.SaveChangesAsync();
            return member;
        }
        public async Task<Member> GetMember(string Id)
        {
            Member member = db.Members.Where(x => x.MemberId == Id).FirstOrDefault();
            await db.SaveChangesAsync();
            return member;
        }

        public async Task<List<Member>> GetAllMembers()
        {
            List <Member> members = new List<Member>();

            List<String> Ids = db
            .Members
            .Select(u => u.MemberId)
            .ToList();

            foreach (string id in Ids)
            {
                Member member = await GetMember(id);
                members.Add(member);
            }

            return members;
        }
    }
}