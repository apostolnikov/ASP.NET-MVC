using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twitter.App.Models.ViewModels.HomeViewModels;

namespace Twitter.App.Models.ViewModels.UsersViewModels
{
    public class UserProfileViewModel
    {
        private List<TweetViewModel> tweets;
        private ICollection<NotificationViewModel> notifications; 

        public UserProfileViewModel()
        {
            this.tweets = new List<TweetViewModel>();
            this.notifications = new List<NotificationViewModel>();
        } 
        public string Username { get; set; }

        public List<TweetViewModel> Tweets
        {
            get { return this.tweets; }
            set { this.tweets = value; }
        }
        public ICollection<NotificationViewModel> Notifications
        {
            get { return this.notifications; }
            set { this.notifications = value; }
        }
        public int FollowersCount { get; set; }
        public int FollowingsCount { get; set; }
        public int FavoritiesCount { get; set; }
    }
}