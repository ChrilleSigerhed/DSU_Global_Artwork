using DSU21_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Data
{
    public interface IArtRepository
    {
        Task<IEnumerable<Artwork>> GetArtThatsPosted();
    }
}
