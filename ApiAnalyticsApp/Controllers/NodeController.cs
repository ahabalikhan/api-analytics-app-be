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
        private readonly IPortalSessionService portalSessionService;

        public NodeController(INodeService nodeService, IPortalSessionService portalSessionService)
        {
            this.nodeService = nodeService;
            this.portalSessionService = portalSessionService;
        }

        [HttpPost("transition")]
        public async Task<HttpResponseModel<int>> GetNextNode(NodeTransitionDto tokenizedRequest)
        {
            int response = await nodeService.GetNextNode(tokenizedRequest);
            return response.AsSuccess();
        }
    }
}
