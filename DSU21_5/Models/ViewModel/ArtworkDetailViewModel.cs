using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ArtworkDetailViewModel
    {
        public List<Artwork> ListOfArt { get; set; } = new List<Artwork>();
        public Artwork DetailArtwork { get; set; }
        public Member Member { get; set; }
        public ArtworkDetailViewModel(IEnumerable<Artwork> artworks, Artwork artwork, Member member)
        {
            Member = member;
            DetailArtwork = artwork;
            ListOfArt = RelatedArtToArtist(artworks);
        }

        public ArtworkDetailViewModel()
        {
        }

        private List<Artwork> RelatedArtToArtist(IEnumerable<Artwork> artworks)
        {
            List<Artwork> artworkForPresentation = new List<Artwork>();
            for (int i = 0; i < artworks.ToList().Count; i++)
            {
                if (artworks.ElementAt(i).ArtworkId != DetailArtwork.ArtworkId)
                {
                    if (artworks.ElementAt(i).Height == null || artworks.ElementAt(i).Width == null)
                    {
                        artworks.ElementAt(i).Height = "Not";
                        artworks.ElementAt(i).Width = "Available";
                    }
                    if (artworks.ElementAt(i).ArtName == null)
                    {
                        artworks.ElementAt(i).ArtName = "Name is missing";
                    }
                    artworkForPresentation.Add(artworks.ElementAt(i));
                }
                if (artworkForPresentation.Count == 4)
                {
                    break;
                }
            }
            return artworkForPresentation;
        }
    }
}
