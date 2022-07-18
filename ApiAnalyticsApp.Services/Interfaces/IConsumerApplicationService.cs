using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Interfaces
{
    public interface IConsumerApplicationService
    {
        Task<KeysDto> CreateConsumerApplication(CreateConsumerApplicationRequestDto request);
        Task<CountPercentageDto> GetTodaysRequestsAsync(string token);
        Task<CountPercentageDto> GetThisMonthRequestsAsync(string token);
        Task<CountPercentageDto> GetTotalRequestsAsync(string token);
        Task<BarChartDto> GetChartAsync(string token);
    }
}
