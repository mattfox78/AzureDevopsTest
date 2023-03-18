using System.Net;
using Polly;
using Polly.Contrib.WaitAndRetry;
using WeatherForecast.Api.Models;
using Microsoft.Extensions.Configuration;

namespace WeatherForecast.Api.Clients
{
	public class OpenWeatherClient : IWeatherClient
	{
		public const string ClientName = "weatherapi";
        private readonly string? OpenWeatherMapApiKey;

		private readonly IHttpClientFactory _httpClientFactory;

		public OpenWeatherClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClientFactory = httpClientFactory;
            OpenWeatherMapApiKey = configuration.GetValue<string>("OpenWeatherMapApiKey");
        }

		public async Task<WeatherResponse?> GetCurrentWeatherForCity(string city)
		{
			var client = _httpClientFactory.CreateClient(ClientName);
			HttpResponseMessage response = await client.GetAsync($"weather?q={city}&appid={OpenWeatherMapApiKey}");
			return await response.Content.ReadFromJsonAsync<WeatherResponse>();
		}
	}
}

public interface IWeatherClient
{
	Task<WeatherResponse?> GetCurrentWeatherForCity(string city);
}

