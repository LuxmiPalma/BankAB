using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class DataInitializer
    {
        private readonly BankAppDataContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(BankAppDataContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public void SeedData()
        {
            _dbContext.Database.Migrate();
            SeedRoles();
            SeedUsers();
            SeedCountries();
            LinkCustomersToCountries();
        }
        private void SeedCountries()
        {
            AddCountryIfDoesntExist("FI", "Finland");
            AddCountryIfDoesntExist("DK", "Denmark");
            AddCountryIfDoesntExist("NO", "Norway");
            AddCountryIfDoesntExist("SE", "Sweden");

        }
        private void AddCountryIfDoesntExist(string code, string name)
        {
            if (_dbContext.Countries.Any(c => c.CountryCode == code)) return;
            _dbContext.Countries.Add(new Country
            {
                CountryCode = code,
                CountryName = name
            });
            _dbContext.SaveChanges();
        }
        private void LinkCustomersToCountries()
        {
            var countries = _dbContext.Countries.ToList();

            var fi = countries.FirstOrDefault(c => c.CountryCode == "FI")?.Id ?? 0;
            var dk = countries.FirstOrDefault(c => c.CountryCode == "DK")?.Id ?? 0;
            var no = countries.FirstOrDefault(c => c.CountryCode == "NO")?.Id ?? 0;
            var se = countries.FirstOrDefault(c => c.CountryCode == "SE")?.Id ?? 0;

            if (fi == 0 || dk == 0 || no == 0 || se == 0) return; // Fail-safe if seed missing

            var updates = new (string[] Cities, int CountryId)[]
            {
        (new[] { "JESSHEIM", "KRISTIANSAND S", "SKIEN" }, no),
        (new[] { "København V" }, dk),
        (new[] { "ESPOO", "JYVÄSKYLÄ", "KEMI", "HELSINKI", "KERIMÄKI", "TAMPERE", "SEINÄJOKI" }, fi),
        (new[] { "VÄSTERVIK", "VEINGE", "STOCKHOLM", "GUNNARSBYN", "HAPARANDA" }, se)
            };

            foreach (var (cities, countryId) in updates)
            {
                var customersToUpdate = _dbContext.Customers
                    .Where(c => cities.Contains(c.City.ToUpper()) && c.CountryId == null)
                    .ToList();

                foreach (var customer in customersToUpdate)
                    customer.CountryId = countryId;
                Console.WriteLine($"Updated {customersToUpdate.Count} customers with CountryId = {countryId}");


                _dbContext.SaveChanges();
            }
        }



        // Här finns möjlighet att uppdatera dina användares loginuppgifter
        private void SeedUsers()
        {
            AddUserIfNotExists("richard.chalk@admin.se", "Abc123#", new string[] { "Admin" });
            AddUserIfNotExists("richard.chalk@cashier.se", "Abc123#", new string[] { "Cashier" });
        }

        // Här finns möjlighet att uppdatera dina användares roller
        private void SeedRoles()
        {
            AddRoleIfNotExisting("Admin");
            AddRoleIfNotExisting("Cashier");
        }

        private void AddRoleIfNotExisting(string roleName)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                _dbContext.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
                _dbContext.SaveChanges();
            }
        }

        private void AddUserIfNotExists(string userName, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();
        }
    }
}
