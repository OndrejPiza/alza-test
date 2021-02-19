using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlzaTest.Services.Utils.Tasks.Database
{
    public class FluentMigratorMigrationStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger _logger; 

        public FluentMigratorMigrationStartupTask(IServiceProvider serviceProvider, ILogger<FluentMigratorMigrationStartupTask> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using var scope = _serviceProvider.CreateScope();
            _logger.LogInformation("Migration started");
            IMigrationRunner migrationRunner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            try
            {
                migrationRunner.MigrateUp();
                _logger.LogInformation("Migration ended");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error during migration");
            }
            
            return Task.CompletedTask;
        }
    }
}