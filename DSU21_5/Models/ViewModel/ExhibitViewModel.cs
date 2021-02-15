using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DSU21_5.Models.ViewModel
{
    public class ExhibitViewModel
    {
        public List<Exhibit> Exhibits { get; set; }
        public List<Artwork> Artworks { get; set; }
        public Random random = new Random();

        public ExhibitViewModel(List<Exhibit> exhibits, List<Artwork> artworks)
        {
            Exhibits = exhibits;
            Artworks = artworks;
        }

    }
}
