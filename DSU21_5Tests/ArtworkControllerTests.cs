using DSU21_5.Data;
using DSU21_5.Models;
using DSU21_5;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DSU21_5.Models.ViewModel;
namespace DSU21_5Tests
{
    public class ArtworkControllerTests
    {
        [Fact]
        public void CheckIfValuesMatchFromViewModel_AllArtFollowsFromViewModel()
        {
            List<Artwork> listOfArt = new List<Artwork>();
            for (int i = 0; i < 10; i++)
            {
                Artwork art = new Artwork()
                {
                    ArtName = "Test"
                };
                listOfArt.Add(art);
            }
            List<Member> member = new List<Member>();
            IEnumerable<Artwork> collectiveArt = listOfArt;
            ArtworkViewModel artworkViewmodel = new ArtworkViewModel(collectiveArt, member);

            int expected = 10;
            int actual = artworkViewmodel.CollectiveArt.Count;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CheckIfFirstAddedArtAlsoIsFirstInViewModel_FirstArtworkMatch()
        {
            List<Artwork> listOfArt = new List<Artwork>();
            Artwork uniqueArt = new Artwork()
            {
                ArtName = "SameName"
            };
            listOfArt.Add(uniqueArt);

            for (int i = 0; i < 10; i++)
            {
                Artwork art = new Artwork()
                {
                    ArtName = "Test"
                };
                listOfArt.Add(art);
            }
            IEnumerable<Artwork> collectiveArt = listOfArt;
            List<Member> member = new List<Member>();
            ArtworkViewModel artworkViewmodel = new ArtworkViewModel(collectiveArt, member);

            string expected = "SameName";
            string actual = artworkViewmodel.CollectiveArt[0].ArtName;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CheckIfViewModelCanContainEmptyList_ReturnsEmptyList()
        {
            IEnumerable<Artwork> collectiveArt = new List<Artwork>();
            List<Member> member = new List<Member>();
            ArtworkViewModel artworkViewmodel = new ArtworkViewModel(collectiveArt, member);
            Assert.NotNull(artworkViewmodel);
        }
    }
}
