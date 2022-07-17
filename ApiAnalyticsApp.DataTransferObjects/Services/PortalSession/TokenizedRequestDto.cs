using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.PortalSession
{
    public class TokenizedRequestDto<T>
    {
        public string Token { get; set; }
        public T Request { get; set; }
    }
}
