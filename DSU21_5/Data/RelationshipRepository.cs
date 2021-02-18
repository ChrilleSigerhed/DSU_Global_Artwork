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

        /// <summary>
        /// Method that creates a relationship between two members and saves it in database
        /// </summary>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public async Task<Relationship> Create(Relationship relationship)
        {
            db.Add(relationship);
            await db.SaveChangesAsync();
            return relationship;
        }

        /// <summary>
        /// Method that gets every relationship with status 0 from database where 0 stands for pending relationship
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Relationship>> GetPendingRelationship(string id)
        {
            List<Relationship> pendingRelationships = db.Relationships.Where(x => x.Requestee == id && x.Status == 0).ToList();
            await db.SaveChangesAsync();

            return pendingRelationships;
        }

        /// <summary>
        /// Method that gets every relationship for a certain user id with status 1 where 1 stands for friends
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Relationship>> GetRelationshipsByUserId(string id)
        {
            List<Relationship> friendsList = db.Relationships.Where(x => (x.Requester == id || x.Requestee == id) && x.Status == 1).ToList();
            await db.SaveChangesAsync();

            return friendsList;
        }

        /// <summary>
        /// Method that updates relationship status to 1 when a user wants to accept a friendship request
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public async Task<Relationship> AcceptRelationshipRequest(string id1, string id2)
        {
            Relationship relationship = db.Relationships.Where(x => x.Requester == id1 && x.Requestee == id2 && x.Status == 0).FirstOrDefault();

            relationship.Status = 1;

            await db.SaveChangesAsync();

            return relationship;
        }

        /// <summary>
        /// Method that updates relationship status to 2 when a user wants to decline a friendship request
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public async Task<Relationship> DenclineRelationshipRequest(string id1, string id2)
        {
            Relationship relationship = db.Relationships.Where(x => x.Requester == id1 && x.Requestee == id2 && x.Status == 0).FirstOrDefault();

            relationship.Status = 2;

            await db.SaveChangesAsync();

            return relationship;
        }

        /// <summary>
        /// Method that gets an existing relationship from database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public async Task<Relationship> GetRelationship(string id, string id2)
        {
            Relationship relationship = db.Relationships.Where(x => (x.Requestee == id && x.Requester == id2 || x.Requestee == id2 && x.Requester == id) && (x.Status == 0 || x.Status == 1)).FirstOrDefault();
            await db.SaveChangesAsync();
            return relationship;
        }

        /// <summary>
        /// Method that checks if two users are friends or pending friends to keep them from being able to send several friend requests
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfRelationshipAlreadyExists(string id, string id2)
        {

            var relationship = await GetRelationship(id, id2);

            if (relationship == null || relationship.Status == 2)
            {
                return false;
            }
            return true;

        }
    }
}
