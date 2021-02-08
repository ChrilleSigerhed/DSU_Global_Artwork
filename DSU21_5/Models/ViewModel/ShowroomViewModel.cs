using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DSU21_5.Models.ViewModel
{
    public class ShowroomViewModel
    {
        Random random = new Random();
        public List<Artwork> Images { get; set; }
        public Member Member { get; set; }
        public List<ArtworkInformation> ArtworkInformation { get; set; } = new List<ArtworkInformation>();
        public List<string> ShowroomList { get; set; }
        public int PositionInList { get; set; }
        public int PreviousInList { get; set; }
        public int NextInList { get; set; }
        public int RandomIndex { get; set; }
        public ShowroomViewModel(List<Artwork> list, Member member)
        {
            Member = member;
            GetListOfArtworkInformation(member, list);
            for (int i = list.Count; i < 17; i++)
            {
                list.Add(new Artwork
                {
                    ImageName = "EmptySpaceInShowroom.jpg"
                });
            }
            Images = list;
            GetShowroomListFromMock();
            GetRandomIndex();
            for (int i = 0; i < ShowroomList.Count; i++)
            {
                if(member.MemberId == ShowroomList[i])
                {
                    if (i == 0)
                    {
                        PositionInList = i;
                        PreviousInList = ShowroomList.Count -1;
                        NextInList = 1;
                    }
                    else if (i == ShowroomList.Count-1)
                    {
                        PositionInList = i;
                        PreviousInList = i - 1;
                        NextInList = 0;
                    }
                    else
                    {
                        PositionInList = i;
                        PreviousInList = PositionInList - 1;
                        NextInList = PositionInList + 1;
                    }
                }
            }
        }
        public void GetRandomIndex()
        {
            RandomIndex = random.Next(0, ShowroomList.Count);
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

        public void GetShowroomListFromMock()
        {
            ShowroomList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("Mock/ExhibitionsMock.json"));
        }
    }
}
