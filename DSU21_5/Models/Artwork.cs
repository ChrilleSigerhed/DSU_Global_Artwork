using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    public class Artwork : IArtwork
    {
        [Key]
        public int ArtworkId { get; set; }
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ArtName { get; set; }
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserId { get; set; }
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
        //[Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Lastname { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
