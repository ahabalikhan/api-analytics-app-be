using ApiAnalyticsApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection;

namespace ApiAnalyticsApp.DataAccess
{
    public class ApiAnalyticsAppDbContext : DbContext
    {
        public ApiAnalyticsAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ConsumerApplication> ConsumerApplications { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<NodeTransition> NodeTransitions { get; set; }
        public DbSet<PredictionLog> PredictionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ConsumerApplication>().ToTable("tbl_ConsumerApplication").HasKey(m => m.Id);
            modelBuilder.Entity<Node>().ToTable("tbl_Node").HasKey(m => m.Id);
            modelBuilder.Entity<NodeTransition>().ToTable("tbl_NodeTransition").HasKey(m => m.Id);
            modelBuilder.Entity<PredictionLog>().ToTable("tbl_PredictionLog").HasKey(m => m.Id);
            modelBuilder.Entity<PortalSession>().ToTable("tbl_PortalSession").HasKey(m => m.Id);
        }
    }
}
