
using HotelBookingPlatform.APIs.Extensions;

namespace HotelBookingPlatform.APIs
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration);

            var app = webApplicationBuilder.Build();
            await app.ConfigureMiddleWaresAsync();
            
            app.Run();
        }
    }
}
