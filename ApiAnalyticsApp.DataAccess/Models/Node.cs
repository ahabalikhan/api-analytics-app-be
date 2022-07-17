using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class Node : BaseEntity
    {
        public int ConsumerApplicationId { get; set; }
    }
}
