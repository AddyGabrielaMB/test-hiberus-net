using EFCoreSecondLevelCacheInterceptor;
using TestHiberusNet.AppServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEFSecondLevelCache(options =>
{
    options.UseMemoryCacheProvider()
    .DisableLogging(true)
    .UseCacheKeyPrefix("EF_")
    .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(1));
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddControllers();
builder.Services.AddAutoMapper();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
