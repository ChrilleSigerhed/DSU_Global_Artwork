﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DSU21_5.Models.ViewModel
{
    public class ProfileViewModel 
    {
        public List<Artwork> AllArtwork { get; set; }
        public Member Member { get; set; }
        public Artwork Artwork { get; set; }
        public Exhibit Exhibit { get; set; }
        public List<Member> AcceptedFriends { get; set; }
        public List<Member> PendingFriends { get; set; }
        public Image ProfilePicture { get; set; }
        public ObservableCollection<ArtworkInformation> ListOfArtInExhibits { get; set; }
        public List<Artwork> ListOfArtwork { get; set; }
        public List<Artwork> ArtworkConnectedToExhibition { get; set; }
        public bool DoesRelationshipExist { get; set; }
        public string CurrentUser { get; set; }
        public DateTime Start { get; } = DateTime.Now.Date;
        public string Stop { get; set; }

        public ProfileViewModel()
        {

        }
        
        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image)
        {
            AllArtwork = artwork.ToList();
            Member = member;
            Member.ProfilePicture = ChangeProfilePictureIfNull(image);
            Member.Bio = ChangeMemberDescriptionIfNull(Member.Bio);
        }

        public ProfileViewModel(Member member, List<Artwork> art, Exhibit exhibit)
        {
            Member = member;
            if(exhibit == null)
            {
                exhibit = new Exhibit()
                {
                    Publish = false,
                };
            }
            Exhibit = exhibit;
            ArtworkConnectedToExhibition = art;
            SetStopDate();
        }

        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image, List<Member> acceptedFriends, List<Member> pendingFriends)
        {
            AllArtwork = artwork.ToList();
            Member = member;
            ProfilePicture = image;
            AcceptedFriends = acceptedFriends;
            PendingFriends = pendingFriends;
            Member.ProfilePicture = ChangeProfilePictureIfNull(image);
            Member.Bio = ChangeMemberDescriptionIfNull(Member.Bio);

            for (int i = 0; i < acceptedFriends.Count; i++)
            {
                if(acceptedFriends[i].ProfilePicture == null)
                {
                    acceptedFriends[i].ProfilePicture = ChangeProfilePictureIfNull();
                }
            }
            for (int i = 0; i < pendingFriends.Count; i++)
            {
                if (pendingFriends[i].ProfilePicture == null)
                {
                    pendingFriends[i].ProfilePicture = ChangeProfilePictureIfNull();
                }
            }
        }

        public ProfileViewModel(IEnumerable<Artwork> artwork, Member member, Image image, bool doesRelationshipExist,string currentUser)
        {
            CurrentUser = currentUser;
            Member = member;
            DoesRelationshipExist = doesRelationshipExist;
            if(currentUser == Member.MemberId)
            {
                DoesRelationshipExist = true;
            }
            Member.ProfilePicture = ChangeProfilePictureIfNull(image);
            Member.Bio = ChangeMemberDescriptionIfNull(Member.Bio);
            AllArtwork = artwork.ToList();
        }

        private string SetStopDate()
        {
            int year = Start.Year;
            year += 1;
            Stop = $"{year}-{Start.Month}-{Start.Day}";
            
            return Stop;
        }

        private string ChangeProfilePictureIfNull()
        {
           
                Image image = new Image()
                {
                    ImageName = "profile.jpeg"

                };
          
            return image.ImageName;
        }

        /// <summary>
        /// If member has not yet provided a ProfilePicture, this will give them a default profilepicture
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private string ChangeProfilePictureIfNull(Image image)
        {
            if (image == null)
            {
                image = new Image()
                {
                    ImageName = "profile.jpeg"

                };
            }
            return image.ImageName;
        }
        /// <summary>
        /// If member has not yet provided a Description, a default description is given to the member to encourage them to provide one!
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        private string ChangeMemberDescriptionIfNull(string Description)
        {
            if(Description == null)
            {
                Description = "You have not yet provided a description for your profile, click on Edit Profile and tell us more about who you are!";
            }
            return Description;
        }
    }
}
