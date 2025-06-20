using HotelBookingPlatform.Core.Entities.Business;
using HotelBookingPlatform.Core.Entities.Identity;
using HotelBookingPlatform.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Infrastructure.Data
{
    public class AppDbContextSeeding
    {
        public static async Task SeedAsync(AppDbContext dbContext, UserManager<AppUsers> userManager)
        {
            if (!dbContext.Hotels.Any())
            {
                var hotel1 = new Hotels
                {
                    Id = Guid.NewGuid(),
                    Name = "Mövenpick Resort El Sokhna",
                    Description = "5-star beach resort with Red Sea view and luxury amenities.",
                    StarRating = StarRating.FiveStars,
                    Address = new Address
                    {
                        City = "Ain Sokhna",
                        Country = "Egypt",
                        Street = "Zafarana Road"
                    }
                };

                var hotel2 = new Hotels
                {
                    Id = Guid.NewGuid(),
                    Name = "Hilton Alexandria Corniche",
                    Description = "4-star hotel with Mediterranean Sea view and private beach.",
                    StarRating = StarRating.FourStars,
                    Address = new Address
                    {
                        City = "Alexandria",
                        Country = "Egypt",
                        Street = "Corniche El Maamoura"
                    }
                };

                await dbContext.Hotels.AddRangeAsync(hotel1, hotel2);
                await dbContext.SaveChangesAsync();

                var rooms = new List<Rooms>
        {
            new Rooms
            {
                Name = "Sea View Deluxe",
                Description = "Spacious room with Red Sea view.",
                PricePerNight = 2500,
                Capacity = 2,
                IsAvailable = true,
                RoomType = RoomType.Double,
                HotelId = hotel1.Id
            },
            new Rooms
            {
                Name = "Family Suite",
                Description = "Suite suitable for families with kids.",
                PricePerNight = 3500,
                Capacity = 4,
                IsAvailable = true,
                RoomType = RoomType.Suite,
                HotelId = hotel1.Id
            },
            new Rooms
            {
                Name = "Standard Twin Room",
                Description = "Comfortable twin beds with modern decor.",
                PricePerNight = 1800,
                Capacity = 2,
                IsAvailable = true,
                RoomType = RoomType.Double,
                HotelId = hotel2.Id
            }
        };

                await dbContext.Rooms.AddRangeAsync(rooms);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
