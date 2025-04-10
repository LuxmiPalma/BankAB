using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;





namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CustomerDTO> GetCustomers()
        {
            return _dbContext.Customers
                .Include(c => c.Country)
                .Include(c => c.Dispositions).ThenInclude(d => d.Account)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Gender = c.Gender,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Zipcode = c.Zipcode,
                    Country = c.Country != null ? c.Country.CountryName : "Unknown",
                    CountryCode = c.Country != null ? c.Country.CountryCode : "",
                    Emailaddress = c.Emailaddress,
                    Telephonecountrycode = c.Telephonecountrycode,
                    Telephonenumber = c.Telephonenumber,
                    NationalId = c.NationalId,
                    Birthday = c.Birthday,
                    TotalBalance = c.Dispositions
                         .Where(d => d.Account != null)
                         .Sum(d => d.Account.Balance)
                }).
                           ToList();
        }
    }
}
