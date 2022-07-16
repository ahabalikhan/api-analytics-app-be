using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class Node : BaseEntity
    {
        public string Name { get; set; }
        public int ConsumerApplicationId { get; set; }

        public ConsumerApplication ConsumerApplication { get; set; }
    }
}
