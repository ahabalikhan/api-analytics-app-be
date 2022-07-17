using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Interfaces
{
    public interface IPortalSessionService
    {
        Task<string> GetToken(KeysDto request);
    }
}
