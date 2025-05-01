using AutoMapper;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BankAB.Pages.Accounts
{
    public class CreateAccountModel : PageModel
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;

        public CreateAccountModel(BankAppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }
        public bool Created { get; set; } = false;


        [BindProperty]
        public AccountsViewModel Input { get; set; } = new();



        public IActionResult OnGet()
        {
            return Page();

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var account = _mapper.Map<Account>(Input);
            account.Created = DateOnly.FromDateTime(DateTime.Now);


            _context.Accounts.Add(account);
            _context.SaveChanges();

            var disposition = new Disposition
            {
                AccountId = account.AccountId,
                CustomerId = CustomerId,
                Type = "OWNER"
            };

            _context.Dispositions.Add(disposition);
            _context.SaveChanges();

            Created = true;
            ModelState.Clear(); // clear the form
            Input = new();

            return RedirectToPage("/Customers/Customer", new { id = CustomerId });

        }

    }
}
