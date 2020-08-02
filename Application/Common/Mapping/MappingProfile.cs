using Application.Articals;
using Application.Artists;
using Application.Identity;
using Application.Interfaces.ExternalAuth;
using Application.Presenters;
using Application.Programs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>()
                 .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.ToShortDateString()));
                 //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl)?null: "/files/"+src.ImgUrl));
            
            CreateMap<GoogleUserInfo, AppUser>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Picture.ToString()));


            CreateMap<FacebookUserInfoResult, AppUser>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Picture.Data.Url.ToString()));


            CreateMap<Artical, ArticalDto>()
                //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/" + src.ImgUrl))
                .ForMember(dest => dest.CreateAt, opts => opts.MapFrom(src => src.CreateAt.ToShortDateString()));

            CreateMap<Artist, ArtistDto>();
                  //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/" + src.ImgUrl));


            CreateMap<Presenter, PresenterDto>()
                 //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/" + src.ImgUrl))
                 .ForMember(dest => dest.Programs, opts => opts.MapFrom(src => src.Programs.Select(x => x.Name)));

            CreateMap<SocialMedia, SocialMediaDto>();
                 //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/" + src.ImgUrl));

            CreateMap<Program, ProgramDto>()
                //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/" + src.ImgUrl))
                .ForMember(dest => dest.Presenter, opts => opts.MapFrom(src => src.Presenter.FirstName + " " + src.Presenter.LastName))
                .ForMember(dest => dest.DayOfWeek, opts => opts.MapFrom(src => src.ShowTime.DayOfWeek.ToString()))
                .ForMember(dest => dest.ShowTimeFrom, opts => opts.MapFrom(src => src.ShowTime.FirstShowTime.Value.ToLongTimeString()))
                .ForMember(dest => dest.ShowTimeTo, opts => opts.MapFrom(src => src.ShowTime.FirstShowTime.Value.AddMinutes(src.DefaultDuration).ToLongTimeString()));

            CreateMap<Episode, EpisodeDto>()
            .ForMember(dest => dest.ProgramName, opts => opts.MapFrom(src => src.Program.Name))
            .ForMember(dest => dest.ShowDate, opts => opts.MapFrom(src => src.ShowDate.ToLongDateString()))
            .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ?
                (string.IsNullOrEmpty(src.Program.ImgUrl) ? null : src.Program.ImgUrl) : src.ImgUrl));

            CreateMap<Schedule, WeeklyScheduleDto>()
            .ForMember(dest => dest.ProgramName, opts => opts.MapFrom(src => src.Program.Name))
            .ForMember(dest => dest.Presenter, opts => opts.MapFrom(src => src.Program.Presenter.FirstName + " " + src.Program.Presenter.LastName))
            .ForMember(dest => dest.UAE, opts => opts.MapFrom(src => src.Program.ShowTime.FirstShowTime.Value.AddHours(4).ToLongTimeString()))
            .ForMember(dest => dest.KSA, opts => opts.MapFrom(src => src.Program.ShowTime.FirstShowTime.Value.AddHours(3).ToLongTimeString()))
            //.ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.Program.ImgUrl) ? null : "/files/" + src.Program.ImgUrl))
            .ForMember(dest => dest.OnAir, opts => opts.MapFrom<OnAirResolver>());



        }

    }
}
