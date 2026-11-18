using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TodoRepository, InMemoryTodoRepository>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("Redis");
    option.InstanceName = "todo:"; // Prefix every keys
});
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(10),
        LocalCacheExpiration = TimeSpan.FromMinutes(2)
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
    app.UseDeveloperExceptionPage();
}
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();