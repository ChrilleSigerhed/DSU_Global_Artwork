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
        public string Requester { get; set; } 

        [Column(TypeName = "nvarchar(100)")]
        [ForeignKey("Member")]
        public string Requestee { get; set; } 

        [Column(TypeName = "nvarchar(100)")]
        public int Status { get; set; } 
    }
}
