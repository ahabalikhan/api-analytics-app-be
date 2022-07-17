using ApiAnalyticsApp.DataAccess.Enums;
using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataAccess.Models;
using ApiAnalyticsApp.DataTransferObjects.Services.Node;
using ApiAnalyticsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Node
{
    public class NodeService : INodeService
    {
        private readonly AuditableRepository<DataAccess.Models.Node> nodeRepository;
        private readonly AuditableRepository<NodeTransition> nodeTransitionRepository;
        public NodeService(AuditableRepository<DataAccess.Models.Node> nodeRepository, AuditableRepository<NodeTransition> nodeTransitionRepository)
        {
            this.nodeRepository = nodeRepository;
            this.nodeTransitionRepository = nodeTransitionRepository;
        }
        public async Task<int> GetNextNode(int consumerApplicationId, NodeTransitionDto request)
        {
            var transitionNodes = nodeRepository.GetAll().Where(n => new int[] { request.FromNodeId, request.ToNodeId }.Contains(n.Id)).ToList();

            if (transitionNodes.Any(node => node.ConsumerApplicationId != consumerApplicationId))
                CustomError.InvalidRequest.ThrowCustomErrorException(HttpStatusCode.BadRequest, "Invalid Node Ids");

            var newTransition = new NodeTransition
            {
                NodeFromId = request.FromNodeId,
                NodeToId = request.ToNodeId,
                OccurredOn = DateTime.UtcNow
            };

            nodeTransitionRepository.Insert(newTransition);
            await nodeTransitionRepository.SaveAsync();

            //To Do: Algorithm

            return request.FromNodeId;
        }
    }
}
