using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.Node
{
    public class NodeTransitionDto
    {
        public int FromNodeId { get; set; }
        public int ToNodeId { get; set; }
    }
}
