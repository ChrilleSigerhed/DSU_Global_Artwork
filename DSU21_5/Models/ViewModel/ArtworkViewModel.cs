using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ArtworkViewModel
    {
        public List<Artwork> CollectiveArt { get; set; }
        public ArtworkViewModel(IEnumerable<Artwork> collectiveArt)
        {
            CollectiveArt = collectiveArt.ToList();
        }
    }
}
