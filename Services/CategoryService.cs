using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService:ICategoryService
    {
        private readonly BankAppDataContext _dbContext;

        public CategoryService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Category> ReadCategories()
        {

        }

    }
}
