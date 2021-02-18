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

        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ArtName { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Year is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Height is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Height { get; set; }
        [Required(ErrorMessage = "Width is required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Width { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Type { get; set; }
        [ForeignKey("Member")]
        public string UserId { get; set; }
        public Member Member { get; set; }
        [ForeignKey("Exhibit")]
        public int? ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
