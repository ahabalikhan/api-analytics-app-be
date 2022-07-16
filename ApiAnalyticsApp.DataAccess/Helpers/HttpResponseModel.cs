using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Helpers
{
    public class HttpResponseModel<T>
    {
        /// <summary>Gets or sets the http status code.</summary>
        public Response Response { get; set; }

        /// <summary>Gets or sets the data.</summary>
        public T Data { get; set; }
    }
    public class Response
    {
        /// <summary>Gets or sets the message.</summary>
        public string Message { get; set; }
    }
}
