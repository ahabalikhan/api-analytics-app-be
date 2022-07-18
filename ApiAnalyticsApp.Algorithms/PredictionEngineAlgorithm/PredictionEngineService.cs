using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataAccess.Models;
using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Algorithms.PredictionEngineAlgorithm
{
    public class PredictionEngineService
    {
        private readonly AuditableRepository<NodeTransition> nodeTransitionRepository;
        private readonly AuditableRepository<PredictionLog> predictionLogRepository;
        public PredictionEngineService(
            AuditableRepository<NodeTransition> nodeTransitionRepository,
            AuditableRepository<PredictionLog> predictionLogRepository
            )
        {
            this.nodeTransitionRepository = nodeTransitionRepository;
            this.predictionLogRepository = predictionLogRepository;
        }

        public async Task<int> PredictNextNodeId(int fromNodeId, int toNodeId)
        {
            var nodeScores = await  nodeTransitionRepository.GetAll().Where(nt => nt.NodeFromId == toNodeId)
                .GroupBy(nt => nt.NodeToId).Select(g => new { NodeId = g.Key, Score = g.Count() }).ToListAsync();

            int predictedNodeId = nodeScores.OrderByDescending(s => s.Score).FirstOrDefault()?.NodeId ?? fromNodeId;

            var newLog = new PredictionLog
            {
                NodeFromId = toNodeId,
                PredictedNodeId = predictedNodeId,
                PredictedAt = DateTime.UtcNow
            };

            predictionLogRepository.Insert(newLog);
            await predictionLogRepository.SaveAsync();

            return predictedNodeId;
        }

        public async Task<CountPercentageDto> GetPredictionAccuracyAsync(List<int> nodeIdsToConsider)
        {
            var linkScores = await nodeTransitionRepository.GetAll()
                .Where(nt => nodeIdsToConsider.Contains(nt.NodeFromId) || nodeIdsToConsider.Contains(nt.NodeToId))
                .GroupBy(nt => new { nt.NodeFromId, nt.NodeToId }).Select(g => new { g.Key.NodeFromId, Score = g.Count() }).ToListAsync();

            var logScores = await predictionLogRepository.GetAll()
                .Where(pl => nodeIdsToConsider.Contains(pl.NodeFromId) || nodeIdsToConsider.Contains(pl.PredictedNodeId))
                .GroupBy(nt => new { nt.NodeFromId, nt.PredictedNodeId }).Select(g => new { g.Key.NodeFromId, Score = g.Count() }).ToListAsync();

            var data = linkScores.Join(logScores, x => x.NodeFromId, y => y.NodeFromId, (x, y) =>
            {
                decimal max = new decimal[] { x.Score, y.Score }.Max();
                decimal min = new decimal[] { x.Score, y.Score }.Min();

                return new { Count = (int)(max - min), Perc = 100 - (((max - min) / max) * 100) };
            });

            var response = new CountPercentageDto
            {
                Percentage = data.Select(d => d.Perc).Average(),
                Count = data.Sum(d => d.Count)
            };
            return response;
        }
    }
}
