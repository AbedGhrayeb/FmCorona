using Application.Identity;
using Application.Interfaces.ExternalAuth;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>();

            CreateMap<AppUser, UserDto>();

            CreateMap<GoogleUserInfo, AppUser>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.GivenName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FamilyName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<FacebookUserInfoResult, AppUser>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Id));

        }

    }
}
