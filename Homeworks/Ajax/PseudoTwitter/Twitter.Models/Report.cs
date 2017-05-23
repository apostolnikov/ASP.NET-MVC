namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateOfReport { get; set; }

        [Required(ErrorMessage = "Report text is required")]
        [MinLength(3)]
        [MaxLength(400)]
        public string ReportText { get; set; }

        public virtual Tweet ReportedTweet { get; set; }

        public virtual User ReportedBy { get; set; }
    }
}
