using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication
{
    public class CountPercentageDto
    {
        public int Count { get; set; }
        public decimal? Percentage { get; set; }
    }
}
