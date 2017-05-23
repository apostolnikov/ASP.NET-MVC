using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Twitter.App.Models.BindingModels
{
    public class CreateTweetBindingModel
    {
        [Required]
        [MaxLength(300)]
        public string TweetContent { get; set; }
    }
}