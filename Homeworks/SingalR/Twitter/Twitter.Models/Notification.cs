using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string  Content { get; set; }
        public DateTime TimeOfPublication { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public bool IsRead { get; set; }

        public Notification()
        {
            this.TimeOfPublication = DateTime.Now;
        }
    }
}
