﻿namespace Twitter.Models
{
    using System;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<User> followers;
        private ICollection<User> following;

        private ICollection<Tweet> myTweets;
        private ICollection<Tweet> myFavoriteTweets;
        private ICollection<Tweet> reTweets;

        private ICollection<Message> sentMessages;
        private ICollection<Message> recievedMessages;

        private ICollection<Notification> notificationsDelivered;
        private ICollection<Notification> notificationsTriggered;

        private ICollection<Report> reportsSent;

        public User()
        {
            this.followers = new HashSet<User>();
            this.following = new HashSet<User>();

            this.myTweets = new HashSet<Tweet>();
            this.myFavoriteTweets = new HashSet<Tweet>();
            this.reTweets = new HashSet<Tweet>();

            this.sentMessages = new HashSet<Message>();
            this.recievedMessages = new HashSet<Message>();

            this.notificationsDelivered = new HashSet<Notification>();
            this.notificationsTriggered = new HashSet<Notification>();

            this.reportsSent = new HashSet<Report>();
        }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string UserPicture { get; set; }

        public string ProfileTheme { get; set; }

        public string AboutMe { get; set; }

        public virtual Address Address{ get; set; }

        //[Required]
        public DateTime? CreatedAt { get; set; }

        public DateTime? LastUpdated { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<User> Followers
        {
            get { return this.followers; }
            set { this.followers = value; }
        }
        public virtual ICollection<User> Following
        {
            get { return this.following; }
            set { this.following = value; }
        }

        public virtual ICollection<Tweet> MyTweets
        {
            get { return this.myTweets; }
            set { this.myTweets = value; }
        }
        public virtual ICollection<Tweet> MyFavoriteTweets
        {
            get { return this.myFavoriteTweets; }
            set { this.myFavoriteTweets = value; }
        }
        public virtual ICollection<Tweet> ReTweets
        {
            get { return this.reTweets; }
            set { this.reTweets = value; }
        }

        public virtual ICollection<Message> SentMessages
        {
            get { return this.sentMessages; }
            set { this.sentMessages = value; }
        }
        public virtual ICollection<Message> RecievedMessages
        {
            get { return this.recievedMessages; }
            set { this.recievedMessages = value; }
        }

        public virtual ICollection<Notification> NotificationsDelivered
        {
            get { return this.notificationsDelivered; }
            set { this.notificationsDelivered = value; }
        }
        public virtual ICollection<Notification> NotificationsTriggered
        {
            get { return this.notificationsTriggered; }
            set { this.notificationsTriggered = value; }
        }

        public virtual ICollection<Report> ReportsSent
        {
            get { return this.reportsSent; }
            set { this.reportsSent = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
