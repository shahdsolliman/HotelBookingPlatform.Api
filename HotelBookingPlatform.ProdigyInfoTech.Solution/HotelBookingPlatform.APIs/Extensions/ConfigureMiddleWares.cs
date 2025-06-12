using HotelBookingPlatform.APIs.MiddleWares;

namespace HotelBookingPlatform.APIs.Extensions
{
    public static class ConfigureMiddleWares
    {
        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");  // Not Found

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
    }
}
