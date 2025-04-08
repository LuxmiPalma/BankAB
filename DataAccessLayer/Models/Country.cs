using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Country
    {
        public int Id { get; set; }

        [MaxLength(2)]
        public string CountryCode{ get; set; } = null!;

        [MaxLength(50)]
        public string CountryName { get; set; } = null!;
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    }

}
