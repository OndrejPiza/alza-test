using System;
using AlzaTest.Services.Utils.Tasks.Database;
using Microsoft.Extensions.Logging;

namespace AlzaTest.Services.Products.API.Tasks
{
    public class MigrationTask : FluentMigratorMigrationStartupTask
    {
        public MigrationTask(IServiceProvider serviceProvider, ILogger<FluentMigratorMigrationStartupTask> logger) : base(serviceProvider, logger)
        {
        }
    }
}