using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Models
{
    public class PredictionLog
    {
        public int Id { get; set; }
        public int NodeTransitionId { get; set; }
        public int PredictedNodeId { get; set; }
        public DateTime PredictedAt { get; set; }
    }
}
