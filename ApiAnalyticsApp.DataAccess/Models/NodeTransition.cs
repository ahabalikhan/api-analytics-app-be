using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class NodeTransition
    {
        public int Id { get; set; }
        public int NodeFromId { get; set; }
        public int NodeToId { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
