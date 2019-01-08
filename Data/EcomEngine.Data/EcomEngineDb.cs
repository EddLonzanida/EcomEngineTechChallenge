using Eml.ConfigParser.Helpers;
using Microsoft.EntityFrameworkCore;
using Eml.DataRepository;
using EcomEngine.Business.Common.Entities;

namespace EcomEngine.Data
{
    public class EcomEngineDb : DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ConfigBuilder.GetConfiguration();
            var mainDbConnectionString = new MainDbConnectionString(config);

            optionsBuilder.UseSqlServer(mainDbConnectionString.Value);
        }
    }
}
