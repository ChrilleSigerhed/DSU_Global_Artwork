using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ShowroomViewModel
    {
        public List<Artwork> Images { get; set; }

        public ShowroomViewModel(List<Artwork> list)
        {
            for (int i = list.Count; i < 17; i++)
            {
                list.Add(new Artwork
                {
                    ImageName = "EmptySpaceInShowroom.jpg"
                });
            }
            Images = list;
        }
    }
}
