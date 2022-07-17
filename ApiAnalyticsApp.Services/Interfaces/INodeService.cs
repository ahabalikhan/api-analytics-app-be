using ApiAnalyticsApp.DataTransferObjects.Services.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Interfaces
{
    public interface INodeService
    {
        Task<int> GetNextNode(int consumerApplicationId, NodeTransitionDto request);
    }
}
