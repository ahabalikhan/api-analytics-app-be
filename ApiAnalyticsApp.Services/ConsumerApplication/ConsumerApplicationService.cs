﻿using ApiAnalyticsApp.Algorithms.PredictionEngineAlgorithm;
using ApiAnalyticsApp.DataAccess.Enums;
using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataAccess.Models;
using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using ApiAnalyticsApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.ConsumerApplication
{
    using ConsumerApplication = ApiAnalyticsApp.DataAccess.Models.ConsumerApplication;
    using Node = ApiAnalyticsApp.DataAccess.Models.Node;
    public class ConsumerApplicationService : IConsumerApplicationService
    {
        private readonly AuditableRepository<ConsumerApplication> consumerApplicationRepository;
        private readonly IPortalSessionService portalSessionService;
        private readonly AuditableRepository<NodeTransition> nodeTransitionRepository;
        private readonly PredictionEngineService predictionEngineService;
        public ConsumerApplicationService(
            AuditableRepository<ConsumerApplication> consumerApplicationRepository, 
            IPortalSessionService portalSessionService,
            AuditableRepository<NodeTransition> nodeTransitionRepository,
            PredictionEngineService predictionEngineService
            )
        {
            this.consumerApplicationRepository = consumerApplicationRepository;
            this.portalSessionService = portalSessionService;
            this.nodeTransitionRepository = nodeTransitionRepository;
            this.predictionEngineService = predictionEngineService;
        }
        public async Task<KeysDto> CreateConsumerApplication(CreateConsumerApplicationRequestDto request)
        {

            if (request.NodeNames.Count < 2)
                CustomError.InvalidRequest.ThrowCustomErrorException(HttpStatusCode.BadRequest, description: "Minimum two nodes are required");

            if (request.NodeNames.Any(name => string.IsNullOrEmpty(name)))
                CustomError.InvalidRequest.ThrowCustomErrorException(HttpStatusCode.BadRequest, description: "Nodes names should be non-empty");

            string applicationKey = Guid.NewGuid().ToString();
            string secretKey = Guid.NewGuid().ToString();

            var consumerApplication = new ConsumerApplication
            {
                ApplicationKey = applicationKey,
                SecretKey = secretKey,
                Nodes = request.NodeNames.Select(name => new Node { Name = name }).ToList()
            };

            consumerApplicationRepository.Insert(consumerApplication);
            await consumerApplicationRepository.SaveAsync();

            var response = new KeysDto
            {
                ApplicationKey = applicationKey,
                SecretKey = secretKey
            };

            return response;
        }
        
        public async Task<CountPercentageDto> GetTodaysRequestsAsync(string token)
        {
            var appId = portalSessionService.GetConsumerApplicationId(token);

            var app = await consumerApplicationRepository.GetAll().Where(ca => ca.Id == appId).Include(ca => ca.Nodes).FirstOrDefaultAsync();

            var nodeIds = app.Nodes.Select(n => n.Id).ToList();

            DateTime now = DateTime.UtcNow;
            DateTime startDateTime = now.Date.AddMinutes(-(now.Date.Minute - 1)).AddDays(-1);
            DateTime endDateTime = startDateTime.AddMonths(2).AddTicks(-1);

            var a = await nodeTransitionRepository.GetAll().OrderByDescending(nt => nt.OccurredOn)
                .Where(nt => (nodeIds.Contains(nt.NodeFromId) || nodeIds.Contains(nt.NodeToId)) && nt.OccurredOn > startDateTime && nt.OccurredOn < endDateTime)
                .ToListAsync();

            var counts = a.GroupBy(nt => nt.OccurredOn.DayOfWeek);

            var todayCount = (decimal)(counts.Where(g => g.Key == endDateTime.DayOfWeek).FirstOrDefault()?.Count() ?? 0);
            var yesterdayCount = (decimal)(counts.Where(g => g.Key == startDateTime.DayOfWeek).FirstOrDefault()?.Count() ?? 0);

            decimal percentage = 0;

            if (yesterdayCount == 0)
            {
                if (todayCount != 0)
                    percentage = 100;
            }
            else
                percentage = ((todayCount - yesterdayCount) / yesterdayCount) * 100;

            var response = new CountPercentageDto
            {
                Count = (int)todayCount,
                Percentage = decimal.Round(percentage,2)
            };

            return response;
        }

        public async Task<CountPercentageDto> GetThisMonthRequestsAsync(string token)
        {
            var appId = portalSessionService.GetConsumerApplicationId(token);

            var app = await consumerApplicationRepository.GetAll().Where(ca => ca.Id == appId).Include(ca => ca.Nodes).FirstOrDefaultAsync();

            var nodeIds = app.Nodes.Select(n => n.Id).ToList();

            DateTime now = DateTime.UtcNow;
            DateTime startDateTime = now.Date.AddDays(-(now.Date.Day - 1)).AddMonths(-1);
            DateTime endDateTime = startDateTime.AddMonths(2).AddTicks(-1);

            var a = await nodeTransitionRepository.GetAll().OrderByDescending(nt => nt.OccurredOn)
                .Where(nt => (nodeIds.Contains(nt.NodeFromId) || nodeIds.Contains(nt.NodeToId)) && nt.OccurredOn > startDateTime && nt.OccurredOn < endDateTime)
                .ToListAsync();

            var counts = a.GroupBy(nt => nt.OccurredOn.Month);

            var thisMonthCount = (decimal)(counts.Where(g => g.Key == endDateTime.Month).FirstOrDefault()?.Count() ?? 0);
            var prevMonthCount = (decimal)(counts.Where(g => g.Key == startDateTime.Month).FirstOrDefault()?.Count() ?? 0);

            decimal percentage = 0;

            if (prevMonthCount == 0)
            {
                if (thisMonthCount != 0)
                    percentage = 100;
            }
            else
                percentage = ((thisMonthCount - prevMonthCount) / prevMonthCount) * 100;

            var response = new CountPercentageDto
            {
                Count = (int)thisMonthCount,
                Percentage = decimal.Round(percentage,2)
            };

            return response;
        }
        public async Task<CountPercentageDto> GetTotalRequestsAsync(string token)
        {
            var appId = portalSessionService.GetConsumerApplicationId(token);

            var app = await consumerApplicationRepository.GetAll().Where(ca => ca.Id == appId).Include(ca => ca.Nodes).FirstOrDefaultAsync();

            var nodeIds = app.Nodes.Select(n => n.Id).ToList();

            var count = await nodeTransitionRepository.GetAll().OrderByDescending(nt => nt.OccurredOn)
                .Where(nt => (nodeIds.Contains(nt.NodeFromId) || nodeIds.Contains(nt.NodeToId)))
                .CountAsync();

            var response = new CountPercentageDto
            {
                Count = count,
                Percentage = null
            };

            return response;
        }
        public async Task<CountPercentageDto> GetPredictionsAsync(string token)
        {
            int appId = portalSessionService.GetConsumerApplicationId(token);

            var app = await consumerApplicationRepository.GetAll().Where(ca => ca.Id == appId).Include(ca => ca.Nodes).FirstOrDefaultAsync();
            var nodeIds = app.Nodes.Select(n => n.Id).ToList();

            var response = await predictionEngineService.GetPredictionAccuracyAsync(nodeIds);

            return response;
        }
        public async Task<BarChartDto> GetChartAsync(string token)
        {
            int appId = portalSessionService.GetConsumerApplicationId(token);

            var app = await consumerApplicationRepository.GetAll().Where(ca => ca.Id == appId).Include(ca => ca.Nodes).FirstOrDefaultAsync();
            var nodeIds = app.Nodes.Select(n => n.Id).ToList();

            DateTime startDateTime = DateTime.Today.AddMonths(-8);
            startDateTime = startDateTime.Date.AddDays(-(startDateTime.Date.Day - 1));
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

            var nodeTransitions = await nodeTransitionRepository.GetAll().OrderByDescending(nt => nt.OccurredOn)
                .Where(nt => (nodeIds.Contains(nt.NodeFromId) || nodeIds.Contains(nt.NodeToId)) && nt.OccurredOn > startDateTime && nt.OccurredOn < endDateTime)
                .ToListAsync();

            var groups = nodeTransitions.GroupBy(nt => nt.OccurredOn.Month).ToList();

            var counts = new List<int>();
            var months = new List<string>();
            for(int i = 0; i < 9; i++)
            {
                var date = startDateTime.AddMonths(i);
                months.Add(date.ToString("MMM"));
                counts.Add(groups.Where(g => g.FirstOrDefault()?.OccurredOn.Month == date.Month).FirstOrDefault()?.Count() ?? 0);
            }
            var response = new BarChartDto
            {
                Counts = counts,
                Months = months
            };

            return response;
        }
    }
}
