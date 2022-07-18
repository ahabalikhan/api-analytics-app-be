using ApiAnalyticsApp.DataTransferObjects.Services.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Interfaces
{
    public interface INodeService
    {
        Task<int> GetNextNode(NodeTransitionDto request);
        Task<List<NodeDto>> GetNodeListAsync(string token);
        Task<GraphDto> GetGraphAsync(string token);
    }
}
