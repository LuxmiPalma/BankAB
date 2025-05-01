using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enum;
using Services.Infrastructure.Validation;
using System.Reflection;
using AutoMapper;
using Services.ViewModels;



namespace BankAB.Pages.CustomerEntry
{
    public class CreateModel : PageModel
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;

        public CreateModel(BankAppDataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        [BindProperty]
        public CustomerFormViewModel Input { get; set; } = new();

       

        public List<SelectListItem> CountryList { get; set; } = new();
        public List<SelectListItem> GenderList { get; set; } = new();

        public bool Created { get; set; } = false;

        public void OnGet()
        {
            LoadDropdowns();
            LoadGenderDropdown();
        }

        public IActionResult OnPost()
        {
            LoadDropdowns();
            LoadGenderDropdown();

            if (string.IsNullOrWhiteSpace(Input.Gender))
            {
                ModelState.AddModelError("Input.Gender", "Gender is required.");
            }

            if (Input.CountryId == null || Input.CountryId <= 0)
            {
                ModelState.AddModelError("Input.CountryId", "Country selection is required.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (!ModelState.IsValid)
                return Page();

            var customer = _mapper.Map<Customer>(Input);
            

            if (Input.BirthdayYear.HasValue && Input.BirthdayMonth.HasValue && Input.BirthdayDay.HasValue)
            {
                try
                {
                    customer.Birthday = new DateOnly
                        (Input.BirthdayYear.Value, 
                        Input.BirthdayMonth.Value, 
                        Input.BirthdayDay.Value);
                }
                catch
                {
                    ModelState.AddModelError("", "Invalid birth date.");
                    return Page();
                }
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            Created = true;
            ModelState.Clear(); 
            Input = new(); 
            
            return Page();
        }

        private void LoadDropdowns()
        {
            CountryList = _context.Countries
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CountryName
                }).ToList();
        }
        private void LoadGenderDropdown()
        {
            GenderList = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Where(g => g != Gender.Choose)
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.GetType()
                    .GetMember(g.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()?.Name ?? g.ToString()
                }).ToList();

            GenderList.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "-- Select Gender --"
            });
        }

    }
}
