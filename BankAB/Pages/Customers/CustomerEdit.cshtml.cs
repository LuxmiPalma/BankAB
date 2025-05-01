using AutoMapper;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using Services.ViewModels;


namespace BankAB.Pages.Customers
{
    public class CustomerEditModel : PageModel
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public CustomerEditModel(IPersonService personService,IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;

        }


        [BindProperty] public CustomerFormViewModel Input { get; set; }

       

        public List<SelectListItem> CountryList { get; set; } = new();
        public List<string> ChangeMessages { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customer = await _personService.GetCustomerAsync(id);
            if (customer == null)
                return NotFound();

            Input = _mapper.Map<CustomerFormViewModel>(customer);


            if (customer.Birthday.HasValue)
            {
                Input.BirthdayYear = customer.Birthday.Value.Year;
                Input.BirthdayMonth = customer.Birthday.Value.Month;
                Input.BirthdayDay = customer.Birthday.Value.Day;
            }

            await LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return Page();
            }
            var customer = _mapper.Map<Customer>(Input);
            customer.CustomerId = Input.CustomerId; // manually set if not in viewmodel
           
            if (Input.BirthdayYear.HasValue && Input.BirthdayMonth.HasValue && Input.BirthdayDay.HasValue)
            {
                try
                {
                    customer.Birthday = new DateOnly(Input.BirthdayYear.Value, Input.BirthdayMonth.Value, Input.BirthdayDay.Value);
                }
                catch
                {
                    ModelState.AddModelError("", "Invalid birth date.");
                    await LoadDropdowns();
                    return Page();
                }
            }

            List<string> changes = await _personService.UpdateCustomerAsync(customer);

            ChangeMessages = changes;

            await LoadDropdowns();
            return Page();
        }

        private async Task LoadDropdowns()
        {
            var countries = await _personService.GetCountriesAsync();
            CountryList = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            }).ToList();
        }
    }
}