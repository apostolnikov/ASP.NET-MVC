﻿namespace Twitter.Data.UnitOfWork
{
    using Microsoft.AspNet.Identity;

    using Twitter.Models;
    using Twitter.Data.Repositories;    

    public interface ITwitterData
    {
        IRepository<User> Users { get; }

        IRepository<Tweet> Tweets { get; }

        IRepository<Message> Messages { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<Report> Reports { get; }

        IRepository<Tag> Tags { get; }

        IRepository<Address> Address { get; }

        IUserStore<User> UserStore { get; }

        IRepository<City> Cities { get; }

        void SaveChanges();
    }
}
