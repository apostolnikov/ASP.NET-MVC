﻿namespace Twitter.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddTweetBindingModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Content { get; set; }

        [StringLength(255)]
        [UIHint("SingleLineText")]
        public string PageUrl { get; set; }
    }
}