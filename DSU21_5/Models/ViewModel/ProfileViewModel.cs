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
        private IEnumerable<Artwork> artwork;
        private Image image;
        private List<Artwork> art;

        public List<Artwork> AllArtwork { get; set; }
        public Member Member { get; set; }
        public Artwork Artwork { get; set; }
        public Exhibit Exhibit { get; set; }
        public List<Member> AcceptedFriends { get; set; }
        public List<Member> PendingFriends { get; set; }
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
     

            Member.ProfilePicture = image.ImageName;
           
            AllArtwork = artwork.ToList();
        }

        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image)
        {
            AllArtwork = artwork.ToList();
            Member = member;
            if (image == null)
            {
                image = new Image()
                {
                    ImageName = "profile.jpeg"

                };
            }
            Member.ProfilePicture = image.ImageName;
        }

        public ProfileViewModel(Member member, List<Artwork> art)
        {
            Member = member;
            ListOfArtwork = art;
        }

        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image, List<Member> acceptedFriends, List<Member> pendingFriends)
        {
            AllArtwork = artwork.ToList();
            Member = member;
            ProfilePicture = image;
            AcceptedFriends = acceptedFriends;
            PendingFriends = pendingFriends;
            if(image == null)
            {
                Image img = new Image()
                {
                    ImageName = "profile.jpeg"
                };
                Member.ProfilePicture = img.ImageName;
            }
            else
            {
                Member.ProfilePicture = image.ImageName;
            }

            for (int i = 0; i < acceptedFriends.Count; i++)
            {
                if(acceptedFriends[i].ProfilePicture == null)
                {
                    Image img = new Image()
                    {
                        ImageName = "profile.jpeg"
                    };
                    acceptedFriends[i].ProfilePicture = img.ImageName;
                }
            }
            for (int i = 0; i < pendingFriends.Count; i++)
            {
                if (pendingFriends[i].ProfilePicture == null)
                {
                    Image img = new Image()
                    {
                        ImageName = "profile.jpeg"
                    };
                    pendingFriends[i].ProfilePicture = img.ImageName;
                }
            }
        }
    }
}
