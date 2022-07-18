using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataTransferObjects.Services.Node;
using ApiAnalyticsApp.DataTransferObjects.Services.PortalSession;
using ApiAnalyticsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/node")]
    [ApiVersion("1.0")]
    public class NodeController : ControllerBase
    {
        private readonly INodeService nodeService;

        public NodeController(INodeService nodeService)
        {
            this.nodeService = nodeService;
        }

        [HttpPost("transition")]
        public async Task<HttpResponseModel<int>> GetNextNode(NodeTransitionDto request)
        {
            int response = await nodeService.GetNextNode(request);
            return response.AsSuccess();
        }
        [HttpGet("{token}")]
        public async Task<HttpResponseModel<List<NodeDto>>> GetNodeList(string token)
        {
            var response = await nodeService.GetNodeListAsync(token);
            return response.AsSuccess();
        }
        [HttpGet("graph/{token}")]
        public async Task<HttpResponseModel<GraphDto>> GetGraph(string token)
        {
            var response = await nodeService.GetGraphAsync(token);
            return response.AsSuccess();
        }
    }
}
