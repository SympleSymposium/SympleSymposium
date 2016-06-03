using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace ConferenceApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Slot> Slots { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //FluentAPI
            builder.Entity<Address>()
               .HasOne<Conference>()
               .WithOne(c => c.Address)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Address>()
               .HasOne<Speaker>()
               .WithOne(c => c.Address)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Room>()
                .HasMany(r => r.Slots)
                .WithOne(s => s.Room)
                .OnDelete(DeleteBehavior.Restrict);

            // Added by Wendy - Need to test
            //builder.Entity<Presentation>()
            //    .HasMany(p => p.Slots)
            //    .WithOne(r => r.Presentation)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
