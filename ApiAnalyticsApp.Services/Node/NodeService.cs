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
        public async Task<GraphDto> GetGraphAsync(string token, DateTime fromDate, DateTime toDate)
        {
            int appId = portalSessionService.GetConsumerApplicationId(token);

            var nodes = await nodeRepository.GetAll().Where(n => n.ConsumerApplicationId == appId).ToListAsync();

            var nodeIds = nodes.Select(n => n.Id).ToList();

            var nodeTransitions = await nodeTransitionRepository.GetAll()
                .Where(nt => (nodeIds.Contains(nt.NodeFromId) || nodeIds.Contains(nt.NodeToId)) && nt.OccurredOn > fromDate && nt.OccurredOn < toDate)
                .GroupBy(x => new { x.NodeFromId, x.NodeToId })
                .Select(g => new { g.Key.NodeFromId, g.Key.NodeToId, Count = g.Count() }).ToListAsync();

            var response = new GraphDto
            {
                Nodes = nodes.Select(n => new GraphNodeDto { Id = n.Name }).ToList(),
                Links = nodeTransitions.Select(nt => new GraphLinksDto
                {
                    Source = nodes.Where(n => n.Id == nt.NodeFromId).FirstOrDefault().Name,
                    Target = nodes.Where(n => n.Id == nt.NodeToId).FirstOrDefault().Name,
                    Value = nt.Count
                }).ToList()
            };

            return response;
        }
    }
}
