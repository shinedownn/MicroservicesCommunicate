using System;
using ReportMicroservice.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace ReportMicroservice.Contexts
{
    public partial class ReportMicroserviceContext : DbContext
    {
        public virtual DbSet<Report> Reports { get; set; }
        protected IConfiguration Configuration { get; }
        public ReportMicroserviceContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected ReportMicroserviceContext(DbContextOptions<ReportMicroserviceContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).ValueGeneratedNever(); 
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString")));

                //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
        }
    }
}
