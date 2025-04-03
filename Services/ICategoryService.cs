using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;


namespace Services
{
    public interface ICategoryService
    {
        IEnumerable<Customer> ReadCategories();

    }
}
