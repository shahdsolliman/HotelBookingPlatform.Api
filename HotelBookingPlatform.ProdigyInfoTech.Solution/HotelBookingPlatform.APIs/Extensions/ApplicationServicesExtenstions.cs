using HotelBookingPlatform.Application;
using HotelBookingPlatform.Application.Services;
using HotelBookingPlatform.Core;
using HotelBookingPlatform.Core.Entities.Identity;
using HotelBookingPlatform.Core.Repositories.Contract;
using HotelBookingPlatform.Core.Services.Contract;
using HotelBookingPlatform.Infrastructure;
using HotelBookingPlatform.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SnapShop.API.Errors;
using StackExchange.Redis;

namespace HotelBookingPlatform.APIs.Extensions
{
    public static class ApplicationServicesExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddUserDefinedServices();
            services.AddDbContextServices();
            //services.AddAutoMapperServices();
            services.ConfigureInValidResponseServices();
            services.AddRedisServices(configuration);


            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped<IResponseCacheService, ResponseCacheService>();

            return services;
        }
        private static IServiceCollection ConfigureInValidResponseServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
               options.InvalidModelStateResponseFactory = (ActionContext) =>
               {
                   var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(E => E.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();
                   var response = new ApiValidationErrorResponse
                   {
                       Errors = errors
                   };

                   return new BadRequestObjectResult(response);

               }
            );

            return services;
        }
        private static IServiceCollection AddDbContextServices(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
        private static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                var connectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
                return ConnectionMultiplexer.Connect(connectionString);
            });
            return services;
        }

    }
}
