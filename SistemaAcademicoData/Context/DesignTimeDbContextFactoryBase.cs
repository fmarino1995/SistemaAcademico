using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SistemaAcademicoData.Context
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
      IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}SistemaAcademicoCore", Path.DirectorySeparatorChar);
            return Create(basePath);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();


            var appSettings = configuration.GetSection("AppSettings");

#if DEBUG
            var useConection = appSettings.GetSection("ConexaoDebug").Value;
#elif DEVELOPMENT
            var useConection = appSettings.GetSection("ConexaoDevelopment").Value;
#else
            var useConection = appSettings.GetSection("ConexaoProducao").Value;
#endif

            var connectionString = configuration.GetConnectionString(useConection);

            return CreateDbContext(connectionString);
        }

        private TContext CreateDbContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{connectionString}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString.Substring(0, connectionString.IndexOf("Password") - 1)}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
