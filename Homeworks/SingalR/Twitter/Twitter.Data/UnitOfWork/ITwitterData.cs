using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Repositories;
using Twitter.Models;

namespace Twitter.Data.UnitOfWork
{
    public interface ITwitterData
    {
        IRepository<User> Users { get; }
        IRepository<Tweet> Tweets { get; }
        IRepository<Notification> Notifications { get; } 
        int SaveChanges();
    }
}
