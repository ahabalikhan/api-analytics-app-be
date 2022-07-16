using ApiAnalyticsApp.DataAccess.Helpers;
using ApiAnalyticsApp.Services.Interfaces;
using ApiAnalyticsApp.Services.Node;
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
        }
    }
}
