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
            CreateMap<AppUser, UserDto>()
                 .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.ToShortDateString()));


            CreateMap<GoogleUserInfo, AppUser>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<FacebookUserInfoResult, AppUser>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Id));


        }

    }
}
