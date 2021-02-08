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
        public string UserId1 { get; set; }/* = "acb9a065-7600-420d-ab1e-db3b584604bc";*/

        [Column(TypeName = "nvarchar(100)")]
        [ForeignKey("Member")]
        public string UserId2 { get; set; } /*= "ec0b3314-24bb-4592-b835-72c145db7c8f";*/

        [Column(TypeName = "nvarchar(100)")]
        public int Status { get; set; } /*= 0;*/
    }
}
