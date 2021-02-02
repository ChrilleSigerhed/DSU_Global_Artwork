using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ShowroomViewModel
    {
        public List<ShowroomImageModel> Images { get; set; }

        public ShowroomViewModel(List<ShowroomImageModel> list)
        {
            Images = list;
        }
    }
}
