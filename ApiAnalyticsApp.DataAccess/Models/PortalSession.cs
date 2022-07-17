using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class PortalSession : BaseEntity
    {
        public int ConsumerApplicationId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }


        public ConsumerApplication ConsumerApplication { get; set; }
    }
}
