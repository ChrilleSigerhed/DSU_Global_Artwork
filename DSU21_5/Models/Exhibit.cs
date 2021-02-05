using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    public class Exhibit
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Date { get; set; }
        //[ForeignKey("Member")]
        //public string MemberId { get; set; }
        //public Member Member { get; set; }
    }
}
