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
    [Route("api/v{version:apiVersion}/consumer-application")]
    [ApiVersion("1.0")]
    public class ConsumerApplicationController : ControllerBase
    {
        private readonly IConsumerApplicationService consumerApplicationService;
        public ConsumerApplicationController(IConsumerApplicationService consumerApplicationService)
        {
            this.consumerApplicationService = consumerApplicationService;
        }

        [HttpPost]
        public async Task<HttpResponseModel<KeysDto>> CreateConsumerApplication(CreateConsumerApplicationRequestDto request)
        {
            var response = await consumerApplicationService.CreateConsumerApplication(request);
            return response.AsSuccess();
        }
    }
}
