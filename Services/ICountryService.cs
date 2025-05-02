using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICountryService
    {
        Task<List<CountryDetailsViewModel>> GetTopCustomersByCountryAsync(string country);

    }
}
