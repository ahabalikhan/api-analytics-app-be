using ApiAnalyticsApp.DataAccess.Enums;
using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using ApiAnalyticsApp.Services.Interfaces;
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
        public ConsumerApplicationService(AuditableRepository<ConsumerApplication> consumerApplicationRepository)
        {
            this.consumerApplicationRepository = consumerApplicationRepository;
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
    }
}
