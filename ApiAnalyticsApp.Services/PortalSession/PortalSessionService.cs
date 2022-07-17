using ApiAnalyticsApp.DataAccess.Enums;
using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using ApiAnalyticsApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.PortalSession
{
    using PortalSession = ApiAnalyticsApp.DataAccess.Models.PortalSession;
    using ConsumerApplication = ApiAnalyticsApp.DataAccess.Models.ConsumerApplication;
    public class PortalSessionService : IPortalSessionService
    {
        private readonly AuditableRepository<PortalSession> portalSessionRepository;
        private readonly AuditableRepository<ConsumerApplication> consumerApplicationRepository;
        private readonly IConfiguration configuration;
        public PortalSessionService(
            AuditableRepository<PortalSession> portalSessionRepository,
            AuditableRepository<ConsumerApplication> consumerApplicationRepository,
            IConfiguration configuration
            )
        {
            this.portalSessionRepository = portalSessionRepository;
            this.consumerApplicationRepository = consumerApplicationRepository;
            this.configuration = configuration;
        }
        public async Task<string> GetToken(KeysDto request)
        {
            var consumerApplication = consumerApplicationRepository.GetAll().Where(ca => ca.ApplicationKey.ToString() == request.ApplicationKey && ca.SecretKey.ToString() == request.SecretKey).FirstOrDefault();

            if (consumerApplication == null)
                CustomError.NotFound.ThrowCustomErrorException(HttpStatusCode.BadRequest);

            string token = Guid.NewGuid().ToString();

            double.TryParse(configuration["PortalSessionExpiryMinutes"], out double minutesToExpiry);

            var newSession = new PortalSession
            {
                ConsumerApplication = consumerApplication,
                Token = token,
                ExpiryDate = DateTime.UtcNow.AddMinutes(minutesToExpiry)
            };

            portalSessionRepository.Insert(newSession);
            await portalSessionRepository.SaveAsync();

            return token;
        }
    }
}
