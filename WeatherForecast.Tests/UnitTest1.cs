using WeatherForecast.Api.Models;

namespace WeatherForecast.Tests
{
    public class Tests
    {
        public IWeatherClient WeatherClient { get; set; }
        [SetUp]
        public void Setup()
        {
            WeatherClient = A.Fake<IWeatherClient>();
        }

        [Test]
        public void Test1()
        {
            A.CallTo(() => WeatherClient.GetCurrentWeatherForCity(A<string>.Ignored)).Returns(new Api.Models.WeatherResponse());

            var response = WeatherClient.GetCurrentWeatherForCity("knoxville").Result;
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<Api.Models.WeatherResponse>());

            var response2 = WeatherClient.GetCurrentWeatherForCity("maryville").Result;
            Assert.That(response2, Is.Not.Null);
            Assert.That(response2, Is.TypeOf<Api.Models.WeatherResponse>());

            A.CallTo(() => WeatherClient.GetCurrentWeatherForCity(A<string>.Ignored)).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public void Test2()
        {
            Assert.That(WeatherClient, Is.Not.Null);
            A.CallTo(() => WeatherClient.GetCurrentWeatherForCity(A<string>.Ignored)).Returns(new Api.Models.WeatherResponse());

            var response = WeatherClient.GetCurrentWeatherForCity("knoxville").Result;
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<Api.Models.WeatherResponse>());

            A.CallTo(() => WeatherClient.GetCurrentWeatherForCity(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}