using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ProfileViewModel 
    {
        public List<Artwork> AllArtwork { get; set; }
        public Member Member { get; set; }
        public Artwork Artwork { get; set; }

        public Image ProfilePicture { get; set; }
        public ObservableCollection<ArtworkInformation> ListOfArtInExhibits { get; set; }
        public List<Artwork> ListOfArtwork { get; set; }

        public ProfileViewModel()
        {

        }
        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image, ObservableCollection<ArtworkInformation> collection, List<Artwork> artworks)
        {
            ListOfArtwork = artworks;
            ListOfArtInExhibits = collection;
            Member = member;

            if (image == null)
            {
                image = new Image()
                {
                    ImageName = "profile.jpeg"

                };
            }
     

             ProfilePicture = image;
           
            AllArtwork = artwork.ToList();
        }
    }
}
