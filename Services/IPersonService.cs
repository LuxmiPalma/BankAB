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
        Task<List<string>> UpdateCustomerAsync(Customer updatedCustomer);


        Task<Customer> GetCustomerAsync(int id);

        Task<CustomerDTO?> GetCustomerDtoByIdAsync(int id);
        Task DeleteCustomerAsync(int id);
        Task<List<Country>> GetCountriesAsync();

    }
}
