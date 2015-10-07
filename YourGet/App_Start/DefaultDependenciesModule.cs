using System;
using Autofac;
using YourGet.Core.Configuration;
using YourGet.Core.Enums;
using YourGet.Core.Services;

namespace YourGet
{
    public class DefaultDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configuration = new ConfigurationService();
            builder.RegisterInstance(configuration)
                   .AsSelf();
            builder.Register(c => configuration.Current)
                   .AsSelf()
                   .As<IAppConfiguration>();

            switch (configuration.Current.StorageType)
            {
                case StorageType.FileSystem:
                case StorageType.NotSpecified:
                    ConfigureForLocalFileSystem(builder);
                    break;
                case StorageType.AzureStorage:
                    ConfigureForAzureStorage(builder, configuration);
                    break;
            }

            base.Load(builder);
        }

        private static void ConfigureForLocalFileSystem(ContainerBuilder builder)
        {
            builder.RegisterType<FileSystemFileStorageService>()
                .AsSelf()
                .As<IFileStorageService>()
                .SingleInstance();

            /*builder.RegisterInstance(NullReportService.Instance)
                .AsSelf()
                .As<IReportService>()
                .SingleInstance();

            builder.RegisterInstance(NullStatisticsService.Instance)
                .AsSelf()
                .As<IStatisticsService>()
                .SingleInstance();

            builder.RegisterInstance(AuditingService.None)
                .AsSelf()
                .As<AuditingService>()
                .SingleInstance();

            // If we're not using azure storage, then aggregate stats comes from SQL
            builder.RegisterType<SqlAggregateStatsService>()
                .AsSelf()
                .As<IAggregateStatsService>()
                .InstancePerLifetimeScope();*/
        }

        private static void ConfigureForAzureStorage(ContainerBuilder builder, ConfigurationService configuration)
        {
            throw new NotImplementedException("Cloud storage is not yet implemeted");
        }
    }
}
