using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using ApiAnalyticsApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/portal-auth")]
    [ApiVersion("1.0")]
    public class PortalAuthController : ControllerBase
    {
        private readonly IPortalSessionService portalSessionService;
        public PortalAuthController(IPortalSessionService portalSessionService)
        {
            this.portalSessionService = portalSessionService;
        }

        [HttpPost("token")]
        public async Task<HttpResponseModel<string>> GetToken(KeysDto request)
        {
            var response = await portalSessionService.GetToken(request);
            return response.AsSuccess();
        }
    }
}
