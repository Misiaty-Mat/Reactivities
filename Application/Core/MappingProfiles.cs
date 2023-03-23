using Application.Activities;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(dto => dto.HostUsername, opt => opt.MapFrom(src => src.Attendees
                .FirstOrDefault(attendee => attendee.IsHost).AppUser.UserName));
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(destMember => destMember.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(destMember => destMember.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(destMember => destMember.Bio, opt => opt.MapFrom(src => src.AppUser.Bio))
                .ForMember(profile => profile.Image, opt => opt.MapFrom(src => src.AppUser.Photos.FirstOrDefault(photo => photo.IsMain).Url));
            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(profile => profile.Image, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(photo => photo.IsMain).Url));
        }
    }
}