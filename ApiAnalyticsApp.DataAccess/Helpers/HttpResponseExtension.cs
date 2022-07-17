using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Helpers
{
    public static class HttpResponseExtension
    {
        public static HttpResponseModel<T> AsSuccess<T>(this T data, string message = "Success") =>
            new HttpResponseModel<T>
            {
                Data = data,
                Response = new Response
                {
                    Message = message
                }
            };

        public static HttpResponseModel<object> AsSuccess() => AsSuccess<object>(null);

        public static HttpErrorResponseModel<object> AsFailure(this CustomErrorException exception)
        {
            var responseModel = new HttpErrorResponseModel<object>
            {
                Response = new ErrorResponse
                {
                    Message = exception.Description,
                    ErrorCode = exception.Error
                },
                Data = exception.ResponseData,
            };

            return responseModel;
        }
    }
}
