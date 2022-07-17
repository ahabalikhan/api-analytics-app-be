using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Node
{
    public class NodeService : INodeService
    {
        private readonly AuditableRepository<DataAccess.Models.Node> nodeRepository;
        public NodeService(AuditableRepository<DataAccess.Models.Node> nodeRepository)
        {
            this.nodeRepository = nodeRepository;
        }
    }
}
