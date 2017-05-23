using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Tweet
    {
        public ICollection<User> usersShared;
        public ICollection<User> usersFavorites;
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public DateTime TimeOfPublicated { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public virtual ICollection<User> UsersShared
        {
            get { return this.usersShared; }
            set { this.usersShared = value; }
        }

        public virtual ICollection<User> UsersFavorites
        {
            get { return this.usersFavorites; }
            set { this.usersFavorites = value; }
        }

        public Tweet()
        {
            this.TimeOfPublicated = DateTime.Now;
            this.usersShared = new HashSet<User>();
            this.usersFavorites = new HashSet<User>();
        }
    }
}
