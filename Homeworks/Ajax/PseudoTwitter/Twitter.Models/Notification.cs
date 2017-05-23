namespace Twitter.Models
{
    using Enums;

    using System;
    using System.ComponentModel.DataAnnotations;    

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Notification text is required")]
        [MinLength(3)]
        [MaxLength(400)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public bool Read { get; set; }

        public NotificationKind NotificationKind { get; set; }

        public virtual User SendTo { get; set; }

        public virtual User TriggeredBy { get; set; }
    }
}
