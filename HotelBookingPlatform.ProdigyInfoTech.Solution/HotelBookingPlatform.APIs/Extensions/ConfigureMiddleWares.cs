using HotelBookingPlatform.APIs.MiddleWares;
using HotelBookingPlatform.Core.Entities.Identity;
using HotelBookingPlatform.Infrastructure.Data;
using HotelBookingPlatform.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.APIs.Extensions
{
    public static class ConfigureMiddleWares
    {
        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {
            #region Update-Database Explicity

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var _dbContext = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUsers>>();
            var _identityContext = services.GetRequiredService<AppIdentityDbContext>();


            try
            {
                await _dbContext.Database.MigrateAsync();
                await AppDbContextSeeding.SeedAsync(_dbContext, userManager);
                await _identityContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }

            #endregion

            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");  // Not Found

            app.UseHttpsRedirection();
            // app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 

            #endregion

            return app;
        }
    }
}
