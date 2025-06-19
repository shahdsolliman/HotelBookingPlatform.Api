using HotelBookingPlatform.Core.Entities;
using HotelBookingPlatform.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options)
            :base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
