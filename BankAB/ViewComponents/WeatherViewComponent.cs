using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BankAB.ViewComponents
{
    public class WeatherViewComponent: ViewComponent
    {
        private readonly HttpClient _httpClient;

        public WeatherViewComponent()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string city = "Stockholm";
            string apiKey = "abe8c18ec17e0b8c21b223ab458ab6df"; 
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            string temperature = "N/A";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var json = await JsonDocument.ParseAsync(stream);
                    temperature = json.RootElement.GetProperty("main").GetProperty("temp").GetDouble().ToString("0");
                }
            }
            catch
            {
                temperature = "Err";
            }

            var model = new WeatherModel
            {
                Location = city,
                Temperature = temperature
            };

            return View(model);
        }
    }

    
       
    public class WeatherModel
    {
        public string Location { get; set; }
        public string Temperature { get; set; }
    }
}
