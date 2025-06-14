using DotNetEnv;
using HotelBookingPlatform.APIs.Extensions;
using HotelBookingPlatform.Infrastructure.Data;

namespace HotelBookingPlatform.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Load environment variables
            Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

            webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration);

            var app = webApplicationBuilder.Build();
            await app.ConfigureMiddleWaresAsync();


            app.Run();
        }
    }
}
