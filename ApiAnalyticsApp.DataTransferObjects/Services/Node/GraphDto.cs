using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.Node
{
    public class GraphDto
    {
        public List<GraphNodeDto> Nodes { get; set; }
        public List<GraphLinksDto> Links { get; set; }
    }
    public class GraphNodeDto
    {
        public string Id { get; set; }
    }
    public class GraphLinksDto
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public int Value { get; set; }
    }
}
