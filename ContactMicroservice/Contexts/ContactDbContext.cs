using ContactMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactMicroservice.Contexts
{
    public class ContactDbContext : DbContext
    { 
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Person> Person { get; set; } 
        protected IConfiguration Configuration { get; } 
        public ContactDbContext()
        {

        } 
        protected ContactDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United Kingdom.1252"); 
            
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("contact");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.HasOne(d => d.Person)
                    .WithMany()
                    .HasForeignKey(d => d.Personid)
                    .HasConstraintName("contact_personid_fkey");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Personid)
                    .ValueGeneratedNever()
                    .HasColumnName("personid");

                entity.Property(e => e.Company).HasColumnName("company");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Surname).HasColumnName("surname");
            });

             
            OnModelCreating(modelBuilder);  
        } 
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString")).EnableSensitiveDataLogging().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

                //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
        }
    }
}
