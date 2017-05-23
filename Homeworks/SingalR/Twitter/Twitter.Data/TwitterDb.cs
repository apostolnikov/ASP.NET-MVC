using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Policy;
using Microsoft.AspNet.Identity.EntityFramework;
using Twitter.Data.Migrations;
using Twitter.Models;

namespace Twitter.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TwitterDb : IdentityDbContext<User>
    {
        public TwitterDb()
            : base("name=TwitterDb")
        {
            Database.SetInitializer(
                     new MigrateDatabaseToLatestVersion<TwitterDb, Configuration>());
        }


        public virtual DbSet<Tweet> Tweets { get; set; }
        public virtual DbSet<Notification> Notifications  { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
       
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
               .HasMany(u => u.Followers)
               .WithMany()
               .Map(m =>
               {
                   m.ToTable("User_Followers");
                   m.MapLeftKey("UserId");
                   m.MapRightKey("FollowerId");
               });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followings)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("Users_Followings");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FollowingId");
                });

            modelBuilder.Entity<User>()
                .HasMany(t => t.SharedTweets)
                .WithMany(s => s.UsersShared)
                .Map(m =>
                {
                    m.ToTable("SharedTweets");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TweetId");
                });

            modelBuilder.Entity<User>()
                .HasMany(t => t.FavoritesTweets)
                .WithMany(s => s.UsersFavorites)
                .Map(m =>
                {
                    m.ToTable("FavoritesTweets");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TweetId");
                });


            base.OnModelCreating(modelBuilder);
        }

        public static TwitterDb Create()
        {
            return new TwitterDb();
        }
    }

}