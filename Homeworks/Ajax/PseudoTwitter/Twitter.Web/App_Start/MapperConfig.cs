namespace Twitter.Web.App_Start
{
    using System.Linq;

    using AutoMapper;

    using Twitter.Models;
    using Twitter.Web.Models.ViewModels;

    public static class MapperConfig
    {
        public static void RegisterMappings()
        {            
            Mapper.CreateMap<User, UserViewModel>().ForMember(u => u.FullName,
                map => map.MapFrom(us => us.FirstName + " " + us.LastName))
                .ForMember(u => u.TweetsCount, map => map.MapFrom(us => us.MyTweets.Count()))
                .ForMember(u => u.FollowersCount, map => map.MapFrom(us => us.Followers.Count()))
                .ForMember(u => u.FavoritesCount, map => map.MapFrom(us => us.MyFavoriteTweets.Count()))
                .ForMember(u => u.FollowingCount, map => map.MapFrom(us => us.Following.Count()));

            Mapper.CreateMap<UserViewModel, ProfileUserViewModel>();
            Mapper.CreateMap<ProfileUserViewModel, PagedUserViewModel>();            

            Mapper.CreateMap<PagedNotificationViewModel, Notification>();

            Mapper.CreateMap<User, ToolTipUserModel>().ForMember(u => u.FullName,
               map => map.MapFrom(us => us.FirstName + " " + us.LastName));
        }
    }
}