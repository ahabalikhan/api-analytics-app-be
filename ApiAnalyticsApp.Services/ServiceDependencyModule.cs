using ApiAnalyticsApp.Algorithms.PredictionEngineAlgorithm;
using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.Services.ConsumerApplication;
using ApiAnalyticsApp.Services.Interfaces;
using ApiAnalyticsApp.Services.Node;
using ApiAnalyticsApp.Services.PortalSession;
using Autofac;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiAnalyticsApp.Services
{
    public class ServiceDependencyModule : Module
    {
        public ServiceDependencyModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IConfiguration configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AuditableRepository<>)).WithParameter("enableSoftDelete", true).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<NodeService>().As<INodeService>().InstancePerLifetimeScope();
            builder.RegisterType<ConsumerApplicationService>().As<IConsumerApplicationService>().InstancePerLifetimeScope();
            builder.RegisterType<PortalSessionService>().As<IPortalSessionService>().InstancePerLifetimeScope();

            builder.RegisterType<PredictionEngineService>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
