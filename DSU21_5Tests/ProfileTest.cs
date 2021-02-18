using DSU21_5.Data;
using DSU21_5.Models;
using DSU21_5;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DSU21_5.Models.ViewModel;

namespace DSU21_5Tests.Profile
{
    public class ProfileTest
    {
        [Fact]
        public void CheckIfViewModelReturnsCorrectArtName_ReturnsCorrectValues()
        {
            Member member = new Member();
            Image image = new Image();
            Artwork art = new Artwork()
            {
                ArtName = "Mona Lisa"
            };

            List<Artwork> listOfArt = new List<Artwork>();
            listOfArt.Add(art);
            IEnumerable<Artwork> artwork = listOfArt;

            ProfileViewModel profileViewModel = new ProfileViewModel(artwork, member, image);
            string expected = art.ArtName;
            string actual = profileViewModel.AllArtwork[0].ArtName;

            Assert.Equal(art.ArtName, profileViewModel.AllArtwork[0].ArtName);
        }
        [Fact]
        public void CheckIfViewModelReturnsCorrectImageName_ReturnsCorrectValues()
        {
            Member member = new Member();
            Artwork art = new Artwork();
            List<Artwork> listOfArt = new List<Artwork>();
            Image image = new Image()
            {
                ImageName = "Röda drake"
            };

            listOfArt.Add(art);
            IEnumerable<Artwork> artwork = listOfArt;

            ProfileViewModel profileViewModel = new ProfileViewModel(artwork, member, image);
            string expected = image.ImageName;
            string actual = profileViewModel.Member.ProfilePicture;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void CheckIfViewModelReturnsCorrectImageNameWhenNull_ReturnsCorrectValues()
        {
            Member member = new Member();
            Artwork art = new Artwork();
            Image image = null;
            List<Artwork> listOfArt = new List<Artwork>();

            listOfArt.Add(art);
            IEnumerable<Artwork> artwork = listOfArt;

            ProfileViewModel profileViewModel = new ProfileViewModel(artwork, member, image);
            string expected = "profile.jpeg";
            string actual = profileViewModel.Member.ProfilePicture;

            Assert.Equal(expected, actual);
        }

    }
}
