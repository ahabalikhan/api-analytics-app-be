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

        
    }
}
