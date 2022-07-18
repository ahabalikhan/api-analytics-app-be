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
        private readonly IPortalSessionService portalSessionService;
        public ConsumerApplicationController(IConsumerApplicationService consumerApplicationService, IPortalSessionService portalSessionService)
        {
            this.consumerApplicationService = consumerApplicationService;
            this.portalSessionService = portalSessionService;
        }

        [HttpPost]
        public async Task<HttpResponseModel<KeysDto>> CreateConsumerApplication(CreateConsumerApplicationRequestDto request)
        {
            var response = await consumerApplicationService.CreateConsumerApplication(request);
            return response.AsSuccess();
        }
        [HttpGet("{token}/todays-requests")]
        public async Task<HttpResponseModel<CountPercentageDto>> GetTodaysRequests(string token)
        {
            var response = await consumerApplicationService.GetTodaysRequestsAsync(token);
            return response.AsSuccess();
        }
        [HttpGet("{token}/months-requests")]
        public async Task<HttpResponseModel<CountPercentageDto>> GetThisMonthRequests(string token)
        {
            var response = await consumerApplicationService.GetThisMonthRequestsAsync(token);
            return response.AsSuccess();
        }
        [HttpGet("{token}/total-requests")]
        public async Task<HttpResponseModel<CountPercentageDto>> GetTotalRequests(string token)
        {
            var response = await consumerApplicationService.GetTotalRequestsAsync(token);
            return response.AsSuccess();
        }
        [HttpGet("{token}/chart")]
        public async Task<HttpResponseModel<BarChartDto>> GetChart(string token)
        {
            var response = await consumerApplicationService.GetChartAsync(token);
            return response.AsSuccess();
        }
    }
}
