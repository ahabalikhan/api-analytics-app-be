using ApiAnalyticsApp.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class PredictionLog : BaseEntity
    {
        public int NodeFromId { get; set; }
        public int PredictedNodeId { get; set; }
        public DateTime PredictedAt { get; set; }
    }
}
