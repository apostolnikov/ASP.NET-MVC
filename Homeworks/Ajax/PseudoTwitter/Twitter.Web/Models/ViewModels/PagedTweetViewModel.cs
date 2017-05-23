namespace Twitter.Web.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using Twitter.Models;

    public class PagedTweetViewModel
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string AuthorUsername { get; set; }

        public string AuthorProfilePicture { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string AuthorFirstName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string AuthorLastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Text { get; set; }

        public int FavoriteCount { get; set; }

        public int RetweetedCount { get; set; }

        public static Expression<Func<Tweet, PagedTweetViewModel>> Create
        {
            get
            {
                return tweet => new PagedTweetViewModel
                {
                    Id = tweet.Id,
                    AuthorUsername = tweet.Author.UserName,
                    AuthorProfilePicture = tweet.Author.UserPicture,
                    AuthorFirstName = tweet.Author.FirstName,
                    AuthorLastName = tweet.Author.LastName,
                    CreatedAt = tweet.CreatedAt,
                    Text = tweet.Text,
                    FavoriteCount = tweet.FavoredBy.Count(),
                    RetweetedCount = tweet.ReTweets.Count()
                };
            }
        }
    }
}