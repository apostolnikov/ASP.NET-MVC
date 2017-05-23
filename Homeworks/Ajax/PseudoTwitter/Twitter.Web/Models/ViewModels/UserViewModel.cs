using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Username { get; set; }

        public string UserPicture { get; set; }

        public string ProfileTheme { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string FullName { get; set; }

        public string AboutMe { get; set; }

        public int TweetsCount { get; set; }

        public int FollowingCount { get; set; }

        public int FollowersCount { get; set; }

        public int FavoritesCount { get; set; }
        
        public DateTime? CreatedAt { get; set; }

        public DateTime? LastUpdated { get; set; }

        public bool IsMe { get; set; }

        public bool UserProfile { get; set; }
    }
}