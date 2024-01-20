using AutoMapper;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using TestHiberusNet.AppServices.TerminalServices;
using TestHiberusNet.Mappers;
using TestHiberusNet.Models;

namespace TestHiberusNet.AppServices;

public static class ServiceExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<HiberusTestDBContext>((serviceProvider, optionsBuilder) =>
                    optionsBuilder
                        .UseSqlServer(
                            connectionString,
                            sqlServerOptionsBuilder =>
                            {
                                sqlServerOptionsBuilder
                                    .CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds)
                                    .EnableRetryOnFailure()
                                    .MigrationsAssembly(typeof(ServiceExtension).Assembly.FullName);
                            })
                        .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()));
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        MapperConfiguration mapperConfig = new MapperConfiguration(delegate (IMapperConfigurationExpression mc)
        {
            mc.AddProfile(new AutoMapping());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITerminalService, TerminalService>();
        return services;
    }
}
