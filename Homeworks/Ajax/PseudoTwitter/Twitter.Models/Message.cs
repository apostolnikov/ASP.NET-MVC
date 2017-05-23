namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Message text is required")]
        [MinLength(3)]
        [MaxLength(400)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public bool Read { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }        
    }
}
