using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.PortalSession
{
    class TokenizedRequestDto<T>
    {
        public Guid Token { get; set; }
        public T Request { get; set; }
    }
}
