using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WallpaperChanger.Models.Workers
{
    public class WeatherReceiver
    {
        private Settings.Settings _settings;
        public WeatherReceiver(Settings.Settings settings)
        {
            _settings = settings;
        }

        public async Task<string> GetInfoAboutWeather()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("GET"),
                RequestUri =
                    new Uri($"https://api.openweathermap.org/data/2.5/weather?" +
                            $"q={_settings.Temperature.City}&appid={_settings.Temperature.AppId}&units=metric")
            };
            var response = await client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}