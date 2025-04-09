using rubber_tree_test_backend.Interfaces;
using rubber_tree_test_backend.Services;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", builder =>
                builder
                .WithOrigins(
                    "https://localhost:5173" // primary app
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition")
                .Build());
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// register the IJsonDataService
builder.Services.AddSingleton<IJsonDataService, JsonDataService>();

// configurre swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Rubber Tree API", Version = "v1" });
});

WebApplication? app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("v1/swagger.json", "Rubber Tree API");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
