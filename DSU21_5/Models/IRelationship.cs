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
        public string UserId1 { get; set; }
        
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserId2 { get; set; }
        
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public int Status { get; set; }
        
    }
}
