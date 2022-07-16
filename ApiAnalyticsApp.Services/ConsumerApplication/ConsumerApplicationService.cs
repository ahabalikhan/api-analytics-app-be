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
        public async Task<CreateConsumerApplicationResponseDto> CreateConsumerApplication(CreateConsumerApplicationRequestDto request)
        {

            if (request.NodeNames.Count < 2 || request.NodeNames.Any(name => string.IsNullOrEmpty(name)))
                CustomError.InvalidRequest.ThrowCustomErrorException(HttpStatusCode.BadRequest, description: "Nodes names should be non-empty and more than two");

            Guid applicationKey = Guid.NewGuid();
            Guid secretKey = Guid.NewGuid();

            var consumerApplication = new ConsumerApplication
            {
                ApplicationKey = applicationKey,
                SecretKey = secretKey,
                Nodes = request.NodeNames.Select(name => new Node { Name = name }).ToList()
            };

            consumerApplicationRepository.Insert(consumerApplication);
            await consumerApplicationRepository.SaveAsync();

            var response = new CreateConsumerApplicationResponseDto
            {
                ApplicationKey = applicationKey,
                SecretKey = secretKey
            };

            return response;
        }
    }
}
