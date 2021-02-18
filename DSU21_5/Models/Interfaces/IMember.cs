using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    public interface IMember
    {
        [Key]
        public string MemberId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Lastname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Bio { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Facebook { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Twitter { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Instagram { get; set; }
    }
}
