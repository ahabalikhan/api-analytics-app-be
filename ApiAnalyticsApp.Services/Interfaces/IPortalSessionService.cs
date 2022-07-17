using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Interfaces
{
    using ConsumerApplication = ApiAnalyticsApp.DataAccess.Models.ConsumerApplication;
    public interface IPortalSessionService
    {
        Task<string> GetToken(KeysDto request);
        int GetConsumerApplicationId(string token);
    }
}
