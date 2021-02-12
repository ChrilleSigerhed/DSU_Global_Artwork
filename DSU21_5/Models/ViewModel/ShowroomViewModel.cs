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
        public List<Member> Members { get; set; }
        //public List<Artwork> ListOfArtToExhibit { get; set; } = new List<Artwork>();
        public List<ArtworkInformation> ArtworkInformation { get; set; } = new List<ArtworkInformation>();

        public List<ArtworkInformation> ArtToExhibit { get; set; } = new List<ArtworkInformation>();

        public List<string> ShowroomList { get; set; }
        public int PositionInList { get; set; }
        public int PreviousInList { get; set; }
        public int NextInList { get; set; }
        public int RandomIndex { get; set; }
        public string ImageRatio { get; set; } = "1";
        public string ShowroomFloor { get; set; }


        public ShowroomViewModel(List<Artwork> list, Member member, List<Member> members)
        {
            Member = member;
            Members = members;

            SetRandomIndex();
            SetShowRoomFloor();
            SetShowroomPositions(member, members);
            GetListOfArtworkInformation(member, list);
            
            Images = FillOutListWithArtworks(list);
            //GetShowroomListFromMock();
        }
        public void SetRandomIndex()
        {
            RandomIndex = random.Next(0, Members.Count);
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




        //public void GetShowroomListFromMock()
        //{
        //    //Gets a list of userId's from a mock file.
        //    ShowroomList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("Mock/ExhibitionsMock.json"));
        //}

        public void SetShowroomPositions(Member member, List<Member> members)
        {
            //Figures out which position in the list the current member is located, and then sets the previous and next Id's.
            for (int i = 0; i < members.Count; i++)
            {
                if (member.MemberId == members[i].MemberId)
                {
                    if (i == 0)
                    {
                        PositionInList = i;
                        PreviousInList = members.Count - 1;
                        NextInList = 1;
                    }
                    else if (i == members.Count - 1)
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
            if(members.Count <= 1)
            {
                NextInList = 0;
                PreviousInList = 0;
            }
        }

        public void SetShowRoomFloor()
        {
            //Gets a list of image paths from a text file, and then randomize which of the path floors that should be used(ShowroomFloor).
            List<string> floors = new List<string>();
            floors = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("mock/floorImages.json"));
            ShowroomFloor = floors[random.Next(0, 5)];  
        }
        public List<Artwork> GiveMeasurementsToArtworks(List<Artwork> list)
        {
            //Gives measurements to all paintings as there are no measurements that gets registered at the moment. Temp function.
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Width = "10";
                list[i].Height = "11";
            }
            return list;
        }

        public List<Artwork> FillOutListWithArtworks(List<Artwork> list)
        {
            //Fills out the list of artworks that should be presented incase, there is less than 17 paintings, as Index(Showroom) always try to display 17 items.
            for (int i = list.Count; i < 17; i++)
            {
                list.Add(new Artwork
                {
                    ImageName = "EmptySpaceInShowroom.jpg",
                    Height = "1",
                    Width = "1"
                });
            }
            return list;
        }

    }
}
