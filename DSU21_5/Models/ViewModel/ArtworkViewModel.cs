using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    //TODO: Should we have all interfaces implemented here?  
    public class ArtworkViewModel : IImage, IArtwork, IMember
    {
        public List<Artwork> CollectiveArt { get; set; }
        public int ImageId { get; set;}
        public string ImageName { get; set; }
        public string UserId { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ArtworkId { get; set; }
        public string ArtName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Description { get; set; }
        public string MemberId { get; set; }
        public string Email { get; set; }

        public ArtworkViewModel(IEnumerable<Artwork> collectiveArt)
        {
            CollectiveArt = collectiveArt.ToList();
        }
    }
}
