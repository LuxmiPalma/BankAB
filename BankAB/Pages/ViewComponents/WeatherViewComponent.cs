using Microsoft.AspNetCore.Mvc;

namespace BankAB.Pages.ViewComponents
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
            var model = new WeatherModel
            {
                Location = "Stockholm",
                Temperature = "25"
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
