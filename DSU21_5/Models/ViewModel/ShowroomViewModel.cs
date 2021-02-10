using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ShowroomViewModel
    {
        public List<Artwork> Images { get; set; }

        public Member Member { get; set; }
        public List<Member> Members { get; set; }
        //public List<Artwork> ListOfArtToExhibit { get; set; } = new List<Artwork>();
        public List<ArtworkInformation> ArtworkInformation { get; set; } = new List<ArtworkInformation>();
        public List<ArtworkInformation> ArtToExhibit { get; set; } = new List<ArtworkInformation>();
        public ShowroomViewModel(List<Artwork> list, Member member, List<Member> members)
        {
            Member = member;
            Members = members;
            GetListOfArtworkInformation(member, list);
            for (int i = list.Count; i < 17; i++)
            {
                list.Add(new Artwork
                {
                    ImageName = "EmptySpaceInShowroom.jpg"
                });
            }
            Images = list;
        }
        public List<ArtworkInformation> GetListOfArtworkInformation(Member member, List<Artwork> postedArt)
        {
            for (int i = 0; i < 17; i++)
            {
                if (i < postedArt.Count)
                {
                    ArtworkInformation.Add(new ArtworkInformation
                    {
                        Firstname = member.Firstname,
                        Source = postedArt[i].ImageName,
                        Lastname = member.Lastname,
                        Height = postedArt[i].Height,
                        Width = postedArt[i].Width,
                        Type = postedArt[i].Type,
                        Year = postedArt[i].Year,
                        Description = postedArt[i].Description,
                        Title = postedArt[i].ArtName,
                        UserId = member.MemberId
                    }) ;
                }
                else
                {
                    ArtworkInformation.Add(new ArtworkInformation
                    {
                        Source = "EmptySpaceInShowroom.jpg"
                    });

                }
            }
            return ArtworkInformation;
        }
        //public List<ArtworkInformation> GetExhibitionArt()
        //{

        //    foreach (var item in Members)
        //    {
        //        for (int i = 0; i < ListOfArtToExhibit.Count; i++)
        //        {
        //            if (CollectiveArt[i].UserId == item.MemberId)
        //            {
        //                ArtToExhibit.Add(new ArtworkInformation
        //                {
        //                    Firstname = item.Firstname,
        //                    Source = CollectiveArt[i].ImageName,
        //                    Lastname = item.Lastname,
        //                    Height = CollectiveArt[i].Height,
        //                    Width = CollectiveArt[i].Width,
        //                    Type = CollectiveArt[i].Type,
        //                    Year = CollectiveArt[i].Year,
        //                    Description = CollectiveArt[i].Description,
        //                    Title = CollectiveArt[i].ArtName,
        //                    UserId = item.MemberId

        //                });
        //            }

        //        }
        //    }

        //    return ArtToExhibit;
        //}
    }
}
