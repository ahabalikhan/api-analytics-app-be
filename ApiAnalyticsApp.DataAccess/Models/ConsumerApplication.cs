using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class ConsumerApplication : BaseEntity
    {
        public string ApplicationKey { get; set; }
        public string SecretKey { get; set; }

        public List<Node> Nodes { get; set; }
        public List<PortalSession> PortalSessions { get; set; }
    }
}
