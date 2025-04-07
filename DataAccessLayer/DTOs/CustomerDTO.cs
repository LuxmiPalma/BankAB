using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Streetaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public string Telephonecountrycode { get; set; } = string.Empty;
        public string Telephonenumber { get; set; } = string.Empty;
        public string? NationalId { get; set; }
        public DateOnly? Birthday { get; set; }
        public decimal? TotalBalance { get; set; }
    }
}
