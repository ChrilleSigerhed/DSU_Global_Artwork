using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class CommunityViewModel
    {
        public List<Member> CommunityMembers { get; set; }
        public CommunityViewModel(List<Image> listOfImages, List<Member> listOfMembers)
        {
            for (int i = 0; i < listOfMembers.Count; i++)
            {
                for (int j = 0; j < listOfImages.Count; j++)
                {
                    if (listOfMembers[i].MemberId == listOfImages[j].UserId)
                    {
                        listOfMembers[i].ProfilePicture = listOfImages[j].ImageName;
                    }
                }
                if(listOfMembers[i].ProfilePicture == null)
                {
                    listOfMembers[i].ProfilePicture = "profile.jpeg";
                }
            }
            CommunityMembers = listOfMembers.OrderBy(x=> x.Lastname).ToList();
        }
    }
}
