using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    public interface IRelationship
    {
        [Key]
        public int RelationshipId { get; set; }

        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Requester { get; set; }

        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Requestee { get; set; }

        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public int Status { get; set; }
    }
}
