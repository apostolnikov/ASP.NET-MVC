namespace Twitter.Web.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using Twitter.Models;

    public class ToolTipUserModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string UserName { get; set; }

        public string UserPicture { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Address Address { get; set; }

        //public static Expression<Func<User, ToolTipUserModel>> Create
        //{
        //    get
        //    {
        //        return user => new ToolTipUserModel
        //        {
        //            Username = user.UserName,
        //            UserPicture = user.UserPicture,
        //            FullName = user.FirstName + " " + user.LastName,
        //            Email = user.Email,
        //            Address = user.Address,
        //        };
        //    }
        //}
    }
}