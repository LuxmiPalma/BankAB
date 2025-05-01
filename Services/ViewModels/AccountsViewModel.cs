using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class AccountsViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Frequency is required")]
        public string? Frequency { get; set; }
        public DateOnly Created { get; set; }
        [Range(50, 50000, ErrorMessage = "Initial deposit must be between 50 and 50.000 SEK.")]
        public decimal Balance { get; set; }




    }
}
