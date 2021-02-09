using DSU21_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IMemberRepository
    {
        Task<Member> AddMember(Member member);
        Task<Member> GetMember(string Id);
        Task<List<Member>> GetAllMembers();
        Task<Member> UpdateBio(string Id, string bio);
    }
}
