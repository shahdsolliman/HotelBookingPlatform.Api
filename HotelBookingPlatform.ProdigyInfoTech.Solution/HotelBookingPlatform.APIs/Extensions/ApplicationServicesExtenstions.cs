using HotelBookingPlatform.Application.Services;
using HotelBookingPlatform.Application.Services.Contract;
using HotelBookingPlatform.Core;
using HotelBookingPlatform.Core.Repositories.Contract;
using HotelBookingPlatform.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SnapShop.API.Errors;

namespace HotelBookingPlatform.APIs.Extensions
{
    public static class ApplicationServicesExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddUserDefinedServices();
            //services.AddAutoMapperServices();
            services.ConfigureInValidResponseServices();
            //services.AddRedisServices(configuration);

            

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
            services.AddScoped(typeof(IUnitOfWork), typeof(InMemoryUnitOfWork));
            services.AddScoped(typeof(IUserService), typeof(UserService));
  
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

    }
}
