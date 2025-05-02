using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.ViewModels;



namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly BankAppDataContext _context;

        public CountryService(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<List<CountryDetailsViewModel>> GetTopCustomersByCountryAsync(string country)
        {
            return await _context.Customers
              .Include(c => c.Dispositions)
               .ThenInclude(d => d.Account)
                .Where(c => c.Country != null && c.Country.CountryName == country)
                .Select(c => new CountryDetailsViewModel
                {
                CustomerId = c.CustomerId,
              Givenname = c.Givenname,
                Surname = c.Surname,
                TotalBalance = c.Dispositions
                   .Where(d => d.Account != null)
                    .Sum(d => d.Account!.Balance)
                })
                .OrderByDescending(c => c.TotalBalance)
                .Take(10)
                .ToListAsync();
        }
    }
}
  