using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ArtworkViewModel 
    {
        public List<Artwork> CollectiveArt { get; set; } = new List<Artwork>();
        public List<Member> Members { get; set; }
        public List<ArtworkInformation> ArtworkInformation { get; set; } = new List<ArtworkInformation>();

        /// <summary>
        /// Slumpar ordningen på listan som ska presentera uppladdade konstverk
        /// </summary>
        /// <param name="collectiveArt"></param>
        public ArtworkViewModel(IEnumerable<Artwork> collectiveArt, List<Member> members)
        {
            Members = members;
            CollectiveArt = collectiveArt.ToList();

            var listOFArtworkinformation  = GetListOfArtworkInformation();
            ArtworkInformation = GetRandomListOfArtworkInformation(listOFArtworkinformation);
        }
        public List<ArtworkInformation> GetRandomListOfArtworkInformation(List<ArtworkInformation> artworkInformation)
        {

            List<ArtworkInformation> artworkInformationNewOrder = new List<ArtworkInformation>();

            int?[] newOrderOfArt = new int?[artworkInformation.Count];
            Random random = new Random();
            random.Next(0, artworkInformation.Count);
            for (int i = 0; i < artworkInformation.Count; i++)
            {
                if (artworkInformation.ElementAt(i).Title == null)
                {
                    artworkInformation.ElementAt(i).Title = "Name is missing";
                }

                int number = random.Next(0, artworkInformation.Count);
                bool keepgoing = true;
                while (keepgoing)
                {
                    if (newOrderOfArt[number] == null)
                    {
                        newOrderOfArt[number] = i;
                        keepgoing = false;
                    }
                    number = random.Next(0, artworkInformation.Count);
                }
            }

            for (int i = 0; i < artworkInformation.Count; i++)
            {
                for (int j = 0; j < artworkInformation.Count; j++)
                {
                    if (i == newOrderOfArt[j])
                    {
                        artworkInformationNewOrder.Add(artworkInformation.ElementAt(j));
                    }
                }
            }
            return artworkInformationNewOrder;
        }
        public List<ArtworkInformation> GetListOfArtworkInformation()
        {

            foreach (var item in Members)
            {
                for (int i = 0; i < CollectiveArt.Count; i++)
                {
                    if (CollectiveArt[i].UserId == item.MemberId)
                    {
                        ArtworkInformation.Add(new ArtworkInformation
                        {
                            Firstname = item.Firstname,
                            Source = CollectiveArt[i].ImageName,
                            Lastname = item.Lastname,
                            Height = CollectiveArt[i].Height,
                            Width = CollectiveArt[i].Width,
                            Type = CollectiveArt[i].Type,
                            Year = CollectiveArt[i].Year,
                            Description = CollectiveArt[i].Description,
                            Title = CollectiveArt[i].ArtName,
                            UserId = item.MemberId,
                            ArtId = CollectiveArt[i].ArtworkId

                        });
                    }

                }
            }

            return ArtworkInformation;
        }
    }
}
