using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class ConsumerApplication : BaseEntity
    {
        public Guid ApplicationKey { get; set; }
        public Guid SecretKey { get; set; }

        public List<Node> Nodes { get; set; }
    }
}
