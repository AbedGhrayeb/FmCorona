using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Artical> Articals { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<ExternalLogin> ExternalLogins { get; set; }
        public DbSet<FavoriteArtist> FavoriteArtists { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Presenter> Presenters { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Live> Lives { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FavoriteArtist>()
                .HasKey(bc => new { bc.ArtistId, bc.AppUserId });
            builder.Entity<FavoriteArtist>()
                .HasOne(bc => bc.Artist)
                .WithMany(b => b.FavoriteArtists)
                .HasForeignKey(bc => bc.ArtistId);
            builder.Entity<FavoriteArtist>()
                .HasOne(bc => bc.AppUser)
                .WithMany(c => c.FavoriteArtists)
                .HasForeignKey(bc => bc.AppUserId);
        }
    }
}
