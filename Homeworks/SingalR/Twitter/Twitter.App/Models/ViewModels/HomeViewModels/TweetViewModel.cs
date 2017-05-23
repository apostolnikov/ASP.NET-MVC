using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.App.Models.ViewModels.HomeViewModels
{
    public class TweetViewModel
    {
        public int  Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeOfPublication { get; set; }
        public string Username { get; set; }
    }
}