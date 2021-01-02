using Application.Identity.Queries;
using Application.Interfaces;
using Application.MappingProfile;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Photos;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;
using WebUI.Middleware;
using WebUI.Services;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add DataContext
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            //add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin();
                });
            });
            //add Ideintity
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
            })
                .AddDefaultUI()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            //add mediarR
            services.AddMediatR(typeof(CurrentUser.Handler).Assembly);
            //add automapper config
            var mappingConfig = new MapperConfiguration(map =>
                 map.AddProfile(new MappingProfile()));
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddScoped<IJwtGenerator, JwtJenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IFilesAccessor, FilesAccesor>();

            //token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7592d6c3-fe20-488b-8761-1f5fe317a96c"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("Fm-Corona", new OpenApiInfo
                {

                    Description = "Use this API to access Fm-Corona App",
                    Title = "Fm-Corona API",
                    Version = "v1.0",
                    Contact = new OpenApiContact
                    {
                        Email = "abedghrayeb@gmail.com",
                        Name = "abdulrahman ghrayeb",
                        Url = new Uri("https://fmcorona.com/")
                    }

                });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new List<string>()
                }});
            });

            //External Login

            services.AddHttpClient();
            services.AddSingleton<IExternalLoginService, ExternalLoginService>();

            services.AddAuthorization();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseMiddleware<ErrorHandlingMeddleware>();
            app.UseExceptionHandler("/Home/Error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseRouting();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/Fm-Corona/swagger.json", "Fm-Corona API");
                setupAction.RoutePrefix = "swagger";
                setupAction.DefaultModelExpandDepth(2);
                setupAction.DocExpansion(DocExpansion.None);
                setupAction.EnableDeepLinking();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });
        }
    }
}
