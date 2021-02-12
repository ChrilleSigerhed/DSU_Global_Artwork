using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    public class Favourite : IFavourite
    {
        public Favourite()
        {
        }

        public Favourite(string Member, int Artwork)
        {
            MemberId = Member;
            ArtworkId = Artwork;
        }

        [ForeignKey("Member")]
        public string MemberId { get; set; }
        public Member Member { get; set; }
        
        
        [ForeignKey("Artworks")]
        public int ArtworkId { get; set; }
    }
}
