using JobFinderAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JobFinderAPI.Repositories
{
    public class CategoryRepository
    {
        private AuthContext dbContext;

        public CategoryRepository()
        {
            dbContext = new AuthContext();
        }


        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async void GetCategoryById(int id)
        {
           
        }

        public async void GetAllCategories()
        {
            
        }
    }
}