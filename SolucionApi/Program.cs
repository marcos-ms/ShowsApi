using SolucionApi.ApiExternas.TvMazeApi;
using SolucionApi.Data.Repositories;
using SolucionApi.Data;
using SolucionApi.Filters;
using SolucionApi.Services;
using SolucionApi.Swagger;
using SolucionApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger(builder.Configuration);

///////////////////////////////////////////////////////////////////////////////////////////

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterDbContext(connectionString);

var tvMazeApiUrl = builder.Configuration["ServicesApiUrls:TvMazeApi"];
builder.Services.RegisterTvMaziApi(tvMazeApiUrl);

builder.Services.RegisterServices();
builder.Services.RegisterRepositories();

///////////////////////////////////////////////////////////////////////////////////////////

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<CustomHeaderValidatorMiddleware>(ApiKeyAuthHeaderAttribute.ApiKeyHeaderName);
app.UseMiddleware<CustomHeaderValidatorMiddleware>(ApiKeyAuthHeaderAttribute.UserHeaderName);

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
