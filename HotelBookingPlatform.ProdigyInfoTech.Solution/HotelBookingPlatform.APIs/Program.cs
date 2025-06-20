using DotNetEnv;
using HotelBookingPlatform.APIs.Extensions;
using HotelBookingPlatform.Infrastructure.Data;

namespace HotelBookingPlatform.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            // Load environment variables
            Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration)
                .AddIdentityServices();

            var app = webApplicationBuilder.Build();
            await app.ConfigureMiddleWaresAsync();

            app.Run();
        }
    }
}
