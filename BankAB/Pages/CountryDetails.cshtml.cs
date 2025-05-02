using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.ViewModels;

namespace BankAB.Pages
{
    public class CountryDetailsModel : PageModel
    {
        private readonly ICountryService _countryService;
        public string Country { get; set; }
        public List<CountryDetailsViewModel> TopCustomers { get; set; }
        public CountryDetailsModel(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task OnGetAsync(string country)
        {
            Country = country;
            TopCustomers = await _countryService.GetTopCustomersByCountryAsync(country);
        }
    }
}
