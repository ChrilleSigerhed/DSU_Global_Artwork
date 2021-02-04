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
        public Member Member { get; set; }
        public List<ArtworkInformation> ArtworkInformation { get; set; } = new List<ArtworkInformation>();

        /// <summary>
        /// Slumpar ordningen på listan som ska presentera uppladdade konstverk
        /// </summary>
        /// <param name="collectiveArt"></param>
        public ArtworkViewModel(IEnumerable<Artwork> collectiveArt, Member member)
        {
            Member = member;
            int?[] newOrderOfArt = new int?[collectiveArt.ToList().Count];
            Random random = new Random();
            random.Next(0, collectiveArt.ToList().Count);
            for (int i = 0; i < collectiveArt.ToList().Count; i++)
            {
                if(collectiveArt.ElementAt(i).ArtName == null)
                {
                    collectiveArt.ElementAt(i).ArtName = "Name is missing";
                }

                int number = random.Next(0, collectiveArt.ToList().Count);
                bool keepgoing = true;
                while (keepgoing)
                {
                    if (newOrderOfArt[number] == null)
                    {
                        newOrderOfArt[number] = i;
                        keepgoing = false;
                    }
                    number = random.Next(0, collectiveArt.ToList().Count); 
                }
            }

            for (int i = 0; i < collectiveArt.ToList().Count; i++)
            {
                for (int j = 0; j < collectiveArt.ToList().Count; j++)
                {
                    if(i == newOrderOfArt[j])
                    {
                        CollectiveArt.Add(collectiveArt.ElementAt(j));
                    }
                }
            }
            GetListOfArtworkInformation(member, CollectiveArt);
        }
        public List<ArtworkInformation> GetListOfArtworkInformation(Member member, List<Artwork> postedArt)
        {
            foreach (var item in postedArt)
            {

                    ArtworkInformation.Add(new ArtworkInformation
                    {
                        Firstname = member.Firstname,
                        Source = item.ImageName,
                        Lastname = member.Lastname,
                        Height = item.Height,
                        Width = item.Width,
                        Type = item.Type,
                        Year = item.Year,
                        Description = item.Description,
                        Title = item.ArtName,
                        UserId = member.MemberId

                    });
            }
                
                
       
            
            return ArtworkInformation;
        }
    }
}
