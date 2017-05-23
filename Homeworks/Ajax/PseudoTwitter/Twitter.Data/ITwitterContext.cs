namespace Twitter.Data
{
    using Twitter.Models;

    using System.Data.Entity;    

    public interface ITwitterContext
    {
        DbSet<Tweet> Tweets { get; set; }

        DbSet<Message> Messages { get; set; }

        DbSet<Notification> Notifications { get; set; }

        DbSet<Report> Reports { get; set; }

        DbSet<Tag> Tags { get; set; }
    }
}