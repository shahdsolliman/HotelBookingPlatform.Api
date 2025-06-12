using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Application.Services.Helper
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Data = data,
            };
        }

        public static ServiceResult<T> Failure()
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
            };
        }
    }
}
