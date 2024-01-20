using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestHiberusNet.AppServices.TerminalServices;
using TestHiberusNet.Mappers;
using TestHiberusNet.Models;

namespace TestHiberusNet.AppServices;

public static class ServiceExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        IConfiguration configuration2 = configuration;
        services.AddDbContext<HiberusTestDBContext>(options =>
        {
            string connectionString = configuration2.GetConnectionString("Default");
            options.UseSqlServer(connectionString);
        });
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
