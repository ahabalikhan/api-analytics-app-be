using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication
{
    public class BarChartDto
    {
        public List<int> Counts { get; set; }
        public List<string> Months { get; set; }
    }
}
