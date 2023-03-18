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
            CreateMap<ActivityAttendee, Profiles.Profile>()
                .ForMember(destMember => destMember.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
                .ForMember(destMember => destMember.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(destMember => destMember.Bio, opt => opt.MapFrom(src => src.AppUser.Bio));
        }
    }
}