using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models
{
    interface IArtworkInformation
    {
        public int ArtId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Source { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Type { get; set; }
    }
}
