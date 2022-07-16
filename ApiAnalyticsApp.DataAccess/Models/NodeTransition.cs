using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class NodeTransition : BaseEntity
    {
        public int NodeFromId { get; set; }
        public int NodeToId { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
