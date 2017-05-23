using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Twitter.Models
{
    public class User : IdentityUser
    {

        private ICollection<User> _followers;
        private ICollection<User> _followings;
        private ICollection<Tweet> ownedTweets;
        private ICollection<Tweet> favoritesTweets;
        private ICollection<Tweet> sharedTweets;

        public User()
        {
            this._followers = new HashSet<User>();
            this._followings = new HashSet<User>();
            this.ownedTweets = new HashSet<Tweet>();
            this.favoritesTweets = new HashSet<Tweet>();
            this.sharedTweets = new HashSet<Tweet>();
        }
        public virtual ICollection<Tweet> OwnedTweets
        {
            get { return this.ownedTweets; }
            set { this.ownedTweets = value; }
        }

        public virtual ICollection<Tweet> SharedTweets
        {
            get { return this.sharedTweets; }
            set { this.sharedTweets = value; }
        }

        public virtual ICollection<User> Followers
        {
            get { return this._followers; }
            set { this._followers = value; }
            
        }

        public virtual ICollection<User> Followings
        {
            get { return this._followings; }
            set { this._followings = value; }
        }

        public virtual ICollection<Tweet> FavoritesTweets
        {
            get { return this.favoritesTweets; }
            set { this.favoritesTweets = value; }
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
