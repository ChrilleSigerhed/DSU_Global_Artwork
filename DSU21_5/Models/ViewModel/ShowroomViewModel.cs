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
        public Exhibit Exhibit { get; set; }
        public List<Exhibit> Exhibits { get; set; }
        public List<Artwork> ArtToDisplay { get; set; } = new List<Artwork>();
        public int PositionInList { get; set; }
        public int PreviousInList { get; set; }
        public int NextInList { get; set; }
        public int RandomIndex { get; set; }
        public string ShowroomFloor { get; set; }
        public string Artist { get; set; }

        public ShowroomViewModel(List<Artwork> list, Exhibit exhibit, List<Exhibit> exhibits)
        {
            Exhibit = exhibit;
            Exhibits = exhibits;
            SetRandomIndex();
            Artist = $"{Exhibit.Member.Firstname} {Exhibit.Member.Lastname}";
            SetShowRoomFloor();
            SetShowroomPositionsInList(exhibit, exhibits);
            ArtToDisplay = FillOutListWithArtworks(list);

        }

        public void SetRandomIndex()
        {
            //Sets a random number for the elevator randomizer in Showroom.
            RandomIndex = random.Next(0, Exhibits.Count);
        }

        public void SetShowroomPositionsInList(Exhibit exhibit, List<Exhibit> exhibits)
        {
            //Figures out which position in the list the current member is located, and then sets the previous and next Id's.
            for (int i = 0; i < exhibits.Count; i++)
            {
                if (exhibit.Id == exhibits[i].Id)
                {
                    if (i == 0)
                    {
                        PositionInList = i;
                        PreviousInList = exhibits.Count - 1;
                        NextInList = 1;
                    }
                    else if (i == exhibits.Count - 1)
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
            if(exhibits.Count <= 1)
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
