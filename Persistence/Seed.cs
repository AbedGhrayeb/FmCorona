﻿using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Seed
    {
        public static async Task SeedData(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                var admin = new IdentityRole("admin");
                var employee = new IdentityRole("employee");
                var user = new IdentityRole("user");
                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(employee);
                await roleManager.CreateAsync(user);
            }

            if (!await userManager.Users.AnyAsync())
            {
                var admin = new AppUser
                {
                    UserName = "fmcorona@info.com",
                    Email = "fmcorona@info.com",
                    LockoutEnabled = false
                };
                await userManager.CreateAsync(admin, "Corona.1234");
                await userManager.AddToRoleAsync(admin, "admin");
                var emp = new AppUser
                {
                    UserName = "test@mail.com",
                    Email = "test@mail.com",
                    FullName = "tast tast",
                    LockoutEnabled = false
                };
                await userManager.CreateAsync(emp, "test.1234");
                await userManager.AddToRoleAsync(admin, "user");

            }

            if (!await context.Articals.AnyAsync())
            {
                var artical = new Artical
                {
                    Title = "Sample Title",
                    ShortDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry",
                    Details = "Lorem Ipsuis simply dummy text of the printing and typesetting industrym . Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum"
                };
                context.Articals.Add(artical);
                await context.SaveChangesAsync();
            }
            if (!await context.Artists.AnyAsync())
            {
                var artist = new Artist
                {
                    Id = "a",
                    Name = "Artist",
                };
                context.Artists.Add(artist);
                await context.SaveChangesAsync();
            }


            if (!await context.Topics.AnyAsync())
            {
                var topic = new Topic
                {
                    Title = "About Us",
                    Body = "Lorem Ipsuis simply dummy text of the printing and typesetting industrym . Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                };
                context.Topics.Add(topic);
                await context.SaveChangesAsync();
            }

            //if (!await context.Presenters.AnyAsync())
            //{
            //    var presenter = new Presenter
            //    {
            //        FirstName = "first",
            //        LastName = "last",
            //        Bio = "Lorem Ipsuis simply dummy text of the printing and typesetting industrym"
            //    };
            //    context.Presenters.Add(presenter);
            //    await context.SaveChangesAsync();
            //}

            if (!await context.Programs.AnyAsync())
            {
                var presenter = new Presenter
                {
                    FirstName = "first",
                    LastName = "last",
                    Bio = "Lorem Ipsuis simply dummy text of the printing and typesetting industrym"
                };
                var program = new Program
                {
                    Description = "Lorem Ipsuis simply dummy text of the printing and typesetting industrym",
                    Name = "Corona Online",
                    Presenter = presenter,
                };
                var episode = new Episode
                {
                    Duration = 60,
                    Guest = true,
                    GuestName = "Guest",
                    Number = 1,
                    ShowDate = DateTime.Now,
                    Title = "Title1",
                    Url = "episode link",
                    Program=program
                };

                context.Presenters.Add(presenter);
                context.Programs.Add(program);
                context.Episodes.Add(episode);

                await context.SaveChangesAsync();
            }




        }
    }
}



