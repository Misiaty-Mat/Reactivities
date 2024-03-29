using Application.Activities;
using Application.Comments;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(dto => dto.HostUsername, opt => opt.MapFrom(src => src.Attendees
                .FirstOrDefault(attendee => attendee.IsHost).AppUser.UserName));
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(destMember => destMember.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(destMember => destMember.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(destMember => destMember.Bio, opt => opt.MapFrom(src => src.AppUser.Bio))
                .ForMember(profile => profile.Image, opt => 
                    opt.MapFrom(src => src.AppUser.Photos.FirstOrDefault(photo => photo.IsMain).Url))
                .ForMember(profile => profile.FollowersCount, opt => opt.MapFrom(src => src.AppUser.Followers.Count))
                .ForMember(profile => profile.FollowingCount, opt => opt.MapFrom(src => src.AppUser.Followings.Count))
                .ForMember(profile => profile.Following, opt =>
                     opt.MapFrom(src => src.AppUser.Followers.Any(user => user.Observer.UserName == currentUsername)));;
            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(profile => profile.Image, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(photo => photo.IsMain).Url))
                .ForMember(profile => profile.FollowersCount, opt => opt.MapFrom(src => src.Followers.Count))
                .ForMember(profile => profile.FollowingCount, opt => opt.MapFrom(src => src.Followings.Count))
                .ForMember(profile => profile.Following, opt =>
                     opt.MapFrom(src => src.Followers.Any(user => user.Observer.UserName == currentUsername)));
            CreateMap<Comment, CommentDto>()
                .ForMember(destMember => destMember.DisplayName, opt => opt.MapFrom(src => src.Author.DisplayName))
                .ForMember(destMember => destMember.Username, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(profile => profile.Image, opt => opt.MapFrom(src => src.Author.Photos.FirstOrDefault(photo => photo.IsMain).Url));
            CreateMap<ActivityAttendee, Profiles.UserActivityDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(attendee => attendee.Activity.Id))
                .ForMember(dto => dto.Date, opt => opt.MapFrom(attendee => attendee.Activity.Date))
                .ForMember(dto => dto.Title, opt => opt.MapFrom(attendee => attendee.Activity.Title))
                .ForMember(dto => dto.Category, opt => opt.MapFrom(attendee => attendee.Activity.Category))
                .ForMember(dto => dto.HostUsername, opt => opt.MapFrom(attendee =>
                    attendee.Activity.Attendees.FirstOrDefault(att => att.IsHost).AppUser.UserName));
        }
    }
}