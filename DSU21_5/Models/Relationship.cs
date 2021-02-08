using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    public class Relationship: IRelationship
    {
        [Key]
        public int RelationshipId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [ForeignKey("Member")]
        public string UserId1 { get; set; } /*= "49fda989-4a3f-47a1-864d-04149c15743b";*/

        [Column(TypeName = "nvarchar(100)")]
        [ForeignKey("Member")]
        public string UserId2 { get; set; } /*= "cd2ba34a-f8d4-41b0-8230-e2d935f69f52";*/

        [Column(TypeName = "nvarchar(100)")]
        public int Status { get; set; } /*= 0;*/
    }
}
