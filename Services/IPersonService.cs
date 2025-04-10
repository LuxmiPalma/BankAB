using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.DTOs;



namespace Services
{
    public interface IPersonService
    {
        Task<(Customer, List<string>)> UpdateCustomerAsync(
    int id, string gender, string givenName, string surname, string streetAddress, string city, string zipcode,
    int countryId, string emailaddress, string telephoneCountryCode, string telephoneNumber,
    string? nationalId, int birthdayYear, int birthdayMonth, int birthdayDay);

        Task<Customer> GetCustomerAsync(int id);

        Task<CustomerDTO?> GetCustomerDtoByIdAsync(int id);
        Task DeleteCustomerAsync(int id);
        Task<List<Country>> GetCountriesAsync();

    }
}
