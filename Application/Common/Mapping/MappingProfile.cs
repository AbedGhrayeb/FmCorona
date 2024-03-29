﻿using Application.Advertisings;
using Application.Articals;
using Application.Artists;
using Application.Identity;
using Application.Interfaces.ExternalAuth;
using Application.Presenters;
using Application.Programs;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>()
                 .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                 .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/users/" + src.ImgUrl));

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
                .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/articls/" + src.ImgUrl))
                .ForMember(dest => dest.CreateAt, opts => opts.MapFrom(src => src.CreateAt.ToShortDateString()));

            CreateMap<Artist, ArtistDto>()
                  .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/artists/" + src.ImgUrl))
                  .ForMember(dest => dest.FansCount, opts => opts.MapFrom(src => src.FavoriteArtists.Count));


            CreateMap<Presenter, PresenterDto>()
                 .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/presenters/" + src.ImgUrl))
                 .ForMember(dest => dest.Programs, opts => opts.MapFrom(src => src.Programs.Select(x => x.Name)));

            CreateMap<Program, ProgramDto>()
                .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/programs/" + src.ImgUrl))
                .ForMember(dest => dest.Presenter, opts => opts.MapFrom(src => src.Presenter.FirstName + " " + src.Presenter.LastName))
                .ForMember(dest => dest.DayOfWeek, opts => opts.MapFrom(src => src.Schedules.LastOrDefault().DayOfWeek.ToString()))
                .ForMember(dest => dest.ShowTimeFrom, opts => opts.MapFrom(src => src.Schedules.LastOrDefault().ShowTime.ToShortTimeString()))
                .ForMember(dest => dest.ShowTimeTo, opts => opts.MapFrom(src => src.Schedules.LastOrDefault().ShowTime.AddMinutes(src.Schedules.LastOrDefault().Duration).ToShortTimeString()));

            CreateMap<Episode, EpisodeDto>()
            .ForMember(dest => dest.ProgramId, opts => opts.MapFrom(src => src.Program.Id))
            .ForMember(dest => dest.ProgramName, opts => opts.MapFrom(src => src.Program.Name))
            .ForMember(dest => dest.ShowDate, opts => opts.MapFrom(src => src.ShowDate.ToShortDateString()))
            .ForMember(dest => dest.Url, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.Url) ? null : "/files/episodes/" + src.Url))
            .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.Program.ImgUrl) ? null : "/files/programs/" + src.Program.ImgUrl));

            CreateMap<Schedule, WeeklyScheduleDto>()
            .ForMember(dest => dest.Guest, opts => opts.MapFrom(src => src.Guest))
            .ForMember(dest => dest.GuestName, opts => opts.MapFrom(src => src.GuestName))
            .ForMember(dest => dest.ProgramName, opts => opts.MapFrom(src => src.Program.Name))
            .ForMember(dest => dest.Presenter, opts => opts.MapFrom(src => src.Program.Presenter.FirstName + " " + src.Program.Presenter.LastName))
            .ForMember(dest => dest.UAE, opts => opts.MapFrom(src => src.ShowTime.AddHours(4).ToShortTimeString()))
            .ForMember(dest => dest.KSA, opts => opts.MapFrom(src => src.ShowTime.AddHours(3).ToShortTimeString()))
            .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.Program.ImgUrl) ? null : "/files/programs/" + src.Program.ImgUrl))
            .ForMember(dest => dest.OnAir, opts => opts.MapFrom<OnAirResolver>());

            CreateMap<Schedule, ProgramScheduleDto>()
            .ForMember(dest => dest.ProgramName, opts => opts.MapFrom(src => src.Program.Name))
            .ForMember(dest => dest.Presenter, opts => opts.MapFrom(src => src.Program.Presenter.FirstName + " " + src.Program.Presenter.LastName))
            .ForMember(dest => dest.UAE, opts => opts.MapFrom(src => src.ShowTime.ToShortTimeString()))
            .ForMember(dest => dest.KSA, opts => opts.MapFrom(src => src.ShowTime.AddHours(-1).ToShortTimeString()));


            CreateMap<Schedule, EditScheduleVm>();

            CreateMap<Advertising, AdvertisingVm>()
               .ForMember(dest => dest.ImgUrl, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.ImgUrl) ? null : "/files/advertisings/" + src.ImgUrl));

        }

    }
}
