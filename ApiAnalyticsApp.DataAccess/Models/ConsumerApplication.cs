using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class ConsumerApplication
    {
        public int Id { get; set; }
        public Guid ApiKey { get; set; }
        public Guid SecretKey { get; set; }
    }
}
