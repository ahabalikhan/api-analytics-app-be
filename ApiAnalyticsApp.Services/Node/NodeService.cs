using ApiAnalyticsApp.DataAccess.Enums;
using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataAccess.Models;
using ApiAnalyticsApp.DataTransferObjects.Services.Node;
using ApiAnalyticsApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly AuditableRepository<DataAccess.Models.ConsumerApplication> consumerApplicationRepository;
        private readonly IPortalSessionService portalSessionService;
        public NodeService(
            AuditableRepository<DataAccess.Models.Node> nodeRepository, 
            AuditableRepository<NodeTransition> nodeTransitionRepository,
            AuditableRepository<DataAccess.Models.ConsumerApplication> consumerApplicationRepository,
            IPortalSessionService portalSessionService
            )
        {
            this.nodeRepository = nodeRepository;
            this.nodeTransitionRepository = nodeTransitionRepository;
            this.consumerApplicationRepository = consumerApplicationRepository;
            this.portalSessionService = portalSessionService;
        }
        public async Task<int> GetNextNode(NodeTransitionDto request)
        {
            var transitionNodes = nodeRepository.GetAll().Include(n => n.ConsumerApplication).Where(n => new int[] { request.FromNodeId, request.ToNodeId }.Contains(n.Id)).ToList();

            if (transitionNodes.Any(node => node.ConsumerApplication.ApplicationKey != request.ApplicationKey))
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
        public async Task<List<NodeDto>> GetNodeListAsync(string token)
        {
            int appId = portalSessionService.GetConsumerApplicationId(token);

            var consumerApp = await consumerApplicationRepository.GetAll().Where(ca => ca.Id == appId).Include(ca => ca.Nodes).FirstOrDefaultAsync();

            var response = consumerApp.Nodes.Select(n => new NodeDto { Id = n.Id, Name = n.Name }).ToList();

            return response;
        }
    }
}
