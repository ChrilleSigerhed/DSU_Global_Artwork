using DSU21_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IRelationshipRepository
    {
        Task<Relationship> Create(Relationship relationship);
        Task<Relationship> GetRelationship(string id, string id2);
        Task<IEnumerable<Relationship>> GetPendingRelationship(string id);
        Task<IEnumerable<Relationship>> GetRelationshipsByUserId(string id);
        Task<Relationship> AcceptRelationshipRequest(string id1, string id2);
        Task<Relationship> DenyRelationshipRequest(string id1, string id2);
        Task<bool> CheckIfRelationshipAlreadyExists(string id, string id2);
    }
}
