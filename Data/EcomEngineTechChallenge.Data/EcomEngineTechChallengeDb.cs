using System.Linq;
using Eml.ConfigParser.Helpers;
using Microsoft.EntityFrameworkCore;
using Eml.DataRepository;
using EcomEngineTechChallenge.Business.Common.Entities;

namespace EcomEngineTechChallenge.Data
{
    public class EcomEngineTechChallengeDb : DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ConfigBuilder.GetConfiguration();
            var mainDbConnectionString = new MainDbConnectionString(config);

            optionsBuilder.UseSqlServer(mainDbConnectionString.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var pb in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.Name.Equals("Id"))
                .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name)))
            {
                pb.ValueGeneratedNever();
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}

