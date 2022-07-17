using ApiAnalyticsApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.EntityFrameworkConfigurations
{
    public class ConsumerApplicationConfiguration : IEntityTypeConfiguration<ConsumerApplication>
    {
        public void Configure(EntityTypeBuilder<ConsumerApplication> builder)
        {
            builder
                .HasMany(ca => ca.Nodes)
                .WithOne(n => n.ConsumerApplication)
                .HasPrincipalKey(ca => ca.Id)
                .HasForeignKey(n => n.ConsumerApplicationId);

            builder
                .HasMany(ca => ca.PortalSessions)
                .WithOne(ps => ps.ConsumerApplication)
                .HasPrincipalKey(ca => ca.Id)
                .HasForeignKey(ps => ps.ConsumerApplicationId);
        }
    }
}
