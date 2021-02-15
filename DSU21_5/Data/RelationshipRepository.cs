using DSU21_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public class RelationshipRepository : IRelationshipRepository
    {
        ImageDbContext db;

        public RelationshipRepository(ImageDbContext context)
        {
            db = context;
        }

        public async Task<Relationship> Create(Relationship relationship)
        {
            db.Add(relationship);
            await db.SaveChangesAsync();
            return relationship;
        }

        public async Task<Relationship> GetRelationship(string id)
        {
            Relationship relationship = db.Relationships.Where(x => x.Requester == id).FirstOrDefault();
            await db.SaveChangesAsync();
            return relationship;
        }

        public async Task<IEnumerable<Relationship>> GetPendingRelationship(string id)
        {
            List<Relationship> pendingRelationships = db.Relationships.Where(x => x.Requestee == id && x.Status == 0).ToList();
            await db.SaveChangesAsync();

            return pendingRelationships;
        }

        public async Task<IEnumerable<Relationship>> GetRelationshipsByUserId(string id)
        {
            List<Relationship> friendsList = db.Relationships.Where(x => (x.Requester == id || x.Requestee == id) && x.Status == 1).ToList();
            await db.SaveChangesAsync();

            return friendsList;
        }

        public async Task<Relationship> AcceptRelationshipRequest(string id1, string id2)
        {
            Relationship relationship = db.Relationships.Where(x => x.Requester == id1 && x.Requestee == id2 && x.Status == 0).FirstOrDefault();

            relationship.Status = 1;

            await db.SaveChangesAsync();

            return relationship;
        }

        public async Task<Relationship> DenyRelationshipRequest(string id1, string id2)
        {
            Relationship relationship = db.Relationships.Where(x => x.Requester == id1 && x.Requestee == id2 && x.Status == 0).FirstOrDefault();

            relationship.Status = 2;

            await db.SaveChangesAsync();

            return relationship;
        }
        public async Task<Relationship> GetRelationship(string id, string id2)
        {
            Relationship relationship = db.Relationships.Where(x => (x.Requestee == id && x.Requester == id2 || x.Requestee == id2 && x.Requester == id) && (x.Status == 0 || x.Status == 1)).FirstOrDefault();
            await db.SaveChangesAsync();
            return relationship;
        }
        public async Task<bool> CheckIfRelationshipAlreadyExists(string id, string id2)
        {

            var entity = await GetRelationship(id, id2);

            if (entity == null || entity.Status == 2)
            {
                return false;
            }
            return true;

        }
    }
}
