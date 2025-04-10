using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DTOs;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly BankAppDataContext _context;

        public PersonService(BankAppDataContext context)
        {
            _context = context;
        }

        public BankAppDataContext GetDbContext()
        {
            return _context;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.Include(c => c.Dispositions)
                                                   .ThenInclude(d => d.Account)
                                                   .ThenInclude(a => a.Transactions)
                                                   .FirstAsync(c => c.CustomerId == id);

            if (customer != null)
            {
                foreach (var disposition in customer.Dispositions.ToList())
                {
                    _context.Transactions.RemoveRange(disposition.Account.Transactions);
                    _context.Dispositions.Remove(disposition);
                    _context.Accounts.Remove(disposition.Account);
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
        }

        public async Task<(Customer, List<string>)> UpdateCustomerAsync(
            int id, string gender, string givenName, string surname, string streetAddress, string city, string zipcode,
            int countryId,  string emailaddress, string telephoneCountryCode, string telephoneNumber,
            string? nationalId, int birthdayYear, int birthdayMonth, int birthdayDay)
        {
            var customerFromDb = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerFromDb == null)
                throw new InvalidOperationException($"Customer with ID {id} not found.");

            var oldValues = new
            {
                customerFromDb.Givenname,
                customerFromDb.Surname,
                customerFromDb.Emailaddress
            };

            customerFromDb.Gender = gender;
            customerFromDb.Givenname = givenName;
            customerFromDb.Surname = surname;
            customerFromDb.Streetaddress = streetAddress;
            customerFromDb.City = city;
            customerFromDb.Zipcode = zipcode;
            customerFromDb.CountryId = countryId;
            customerFromDb.Emailaddress = emailaddress;
            customerFromDb.Telephonecountrycode = telephoneCountryCode;
            customerFromDb.Telephonenumber = telephoneNumber;
            customerFromDb.NationalId = nationalId;


            if (birthdayYear > 0 && birthdayMonth > 0 && birthdayDay > 0)
            {
                customerFromDb.Birthday = new DateOnly(birthdayYear, birthdayMonth, birthdayDay);
            }

            var changes = new List<string>();
            if (oldValues.Givenname != givenName) changes.Add($"Givenname changed");
            if (oldValues.Surname != surname) changes.Add($"Surname changed");
            if (oldValues.Emailaddress != emailaddress) changes.Add($"Email changed");

            if (changes.Count > 0)
            {
                _context.Attach(customerFromDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return (customerFromDb, changes);
        }
        public async Task<CustomerDTO?> GetCustomerDtoByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Dispositions)
                    .ThenInclude(d => d.Account)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return null;

            var totalBalance = customer.Dispositions
                .Where(d => d.Account != null)
                .Sum(d => d.Account.Balance);

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Gender = customer.Gender,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Zipcode = customer.Zipcode,
                Country = customer.Country?.CountryName ?? string.Empty,
                Emailaddress = customer.Emailaddress,
                Telephonecountrycode = customer.Telephonecountrycode,
                Telephonenumber = customer.Telephonenumber,
                NationalId = customer.NationalId,
                Birthday = customer.Birthday,
                TotalBalance = totalBalance
            };
        

             
        }
        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

    }
}
   