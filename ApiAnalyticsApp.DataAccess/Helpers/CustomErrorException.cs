using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ApiAnalyticsApp.DataAccess.Helpers
{
    public class CustomErrorException : Exception
    {
        public CustomErrorException(
            int error = 0,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            string description = null,
            object data = null)
        {
            Error = error;
            StatusCode = statusCode;
            Description = description;
            ResponseData = data;
        }

        public int Error { get; }

        /// <summary>Gets the status code.</summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>Gets the description.</summary>
        public string Description { get; }

        /// <summary>Gets the data.</summary>
        public object ResponseData { get; }

        public override string Message => Description;
    }
}
