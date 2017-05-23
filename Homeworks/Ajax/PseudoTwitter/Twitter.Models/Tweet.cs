namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        private ICollection<User> favoredBy;

        private ICollection<Tweet> reTweets;
        private ICollection<Tweet> tweetReplys;
        private ICollection<Tweet> tweetMentions;

        private ICollection<Tag> tags;

        private ICollection<Report> reports;

        public Tweet()
        {
            this.favoredBy = new HashSet<User>();

            this.reTweets = new HashSet<Tweet>();
            this.tweetReplys = new HashSet<Tweet>();
            this.tweetMentions = new HashSet<Tweet>();

            this.tags = new HashSet<Tag>();

            this.reports = new HashSet<Report>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Enter tweet content.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Text { get; set; }

        public string URL { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string ReTweetComment { get; set; }

        public virtual User Author { get; set; }
        public virtual Tweet InitialTweet { get; set; }
        public virtual Tweet OriginalReTweetedTweet { get; set; }

        public virtual ICollection<User> FavoredBy
        {
            get { return this.favoredBy; }
            set { this.favoredBy = value; }
        }

        public virtual ICollection<Tweet> ReTweets
        {
            get { return this.reTweets; }
            set { this.reTweets = value; }
        }
        public virtual ICollection<Tweet> TweetReplays
        {
            get { return this.tweetReplys; }
            set { this.tweetReplys = value; }
        }
        public virtual ICollection<Tweet> TweetMentions
        {
            get { return this.tweetMentions; }
            set { this.tweetMentions = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Report> Reports
        {
            get { return this.reports; }
            set { this.reports = value; }
        }
    }
}
