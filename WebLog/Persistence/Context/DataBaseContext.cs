using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class DataBaseContext: IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {

        }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.ProviderKey, p.LoginProvider });
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider });
            builder.Entity<IdentityUser<string>>().HasKey(p => new { p.Id });

            builder.Entity<Picture>()
                .HasOne(p => p.Profile)
                .WithOne(p => p.Picture).HasForeignKey<Picture>(p=>p.ProfileId);
        }
    }
}
