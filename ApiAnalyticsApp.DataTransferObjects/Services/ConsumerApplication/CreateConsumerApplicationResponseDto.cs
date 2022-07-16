using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication
{
    public class CreateConsumerApplicationResponseDto
    {
        public Guid ApplicationKey { get; set; }
        public Guid SecretKey { get; set; }
    }
}
