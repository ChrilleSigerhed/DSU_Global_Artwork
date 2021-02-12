using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    interface IFavourite
    {
        [ForeignKey("Member")]
        public string MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey("Artworks")]
        public int ArtworkId { get; set; }

    }
}
