using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Repositories;
using Twitter.Models;

namespace Twitter.Data.UnitOfWork
{
    public class TwitterData : ITwitterData
    {
        private readonly IDictionary<Type, object> _repositories;
        public TwitterData(TwitterDb context)
        {
            this.Context = context;
            this._repositories = new Dictionary<Type, object>();
        }

        public TwitterDb Context { get; private set; }

        public IRepository<User> Users
        {
             get { return this.GetRepository<User>(); }
        }

        public IRepository<Tweet> Tweets
        {
            get { return GetRepository<Tweet>(); }
        }

        public IRepository<Notification> Notifications
        {
            get { return GetRepository<Notification>(); }
        } 
        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var modelType = typeof(T);
            if (!this._repositories.ContainsKey(modelType))
            {
                var repositoryType = typeof(Repository<T>);
                this._repositories.Add(modelType, Activator.CreateInstance(repositoryType, this.Context));
            }

            return (IRepository<T>)this._repositories[modelType];
        }
    }
}
