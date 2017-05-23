namespace Twitter.Web.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using Twitter.Models;

    public class PagedUserViewModel
    {
        public string Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Username { get; set; }

        public string UserPicture { get; set; }

        public string ProfileTheme { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string FullName { get; set; }

        public string AboutMe { get; set; } 

        public static Expression<Func<User, PagedUserViewModel>> Create
        {
            get
            {
                return user => new PagedUserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    UserPicture = user.UserPicture,
                    ProfileTheme = user.ProfileTheme,
                    FullName = user.FirstName + " " + user.LastName,
                    AboutMe = user.AboutMe
                };
            }
        }
    }
}