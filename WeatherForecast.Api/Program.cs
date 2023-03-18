using Polly;
using Polly.Contrib.WaitAndRetry;
using WeatherForecast.Api.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IWeatherClient, OpenWeatherClient>();

builder.Services.AddHttpClient(OpenWeatherClient.ClientName,
	client =>
	{
		client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
	})
	.AddTransientHttpErrorPolicy(policyBuilder =>
		policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5))

 );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

