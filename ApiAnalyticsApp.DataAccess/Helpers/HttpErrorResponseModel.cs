using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Helpers
{
    public class HttpErrorResponseModel<T>
    {
        /// <summary>Gets or sets the http status code.</summary>
        public ErrorResponse Response { get; set; }

        /// <summary>Gets or sets the data.</summary>
        public T Data { get; set; }
    }
    public class ErrorResponse : Response
    {
        public int ErrorCode { get; set; }

        public List<string> Errors { get; set; }
    }
}
