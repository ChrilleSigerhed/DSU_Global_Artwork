using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ProfileViewModel 
    {
        public List<Artwork> AllArtwork { get; set; }
        public Member Member { get; set; }
        public Image ProfilePicture { get; set; }

        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image)
        {
            Member = member;

            if(member.Bio == null)
            {
                member.Bio = "My personal bio.";
            }

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
