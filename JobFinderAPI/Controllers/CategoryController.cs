using JobFinderAPI.Entities;
using JobFinderAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{

    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        private CategoryRepository _repo = new CategoryRepository();
       // AuthContext db = new AuthContext();



        //[Route("allCategories")]
        //[HttpGet]
        //public IQueryable<Category> GetCategories() {
            
        //}

        public IHttpActionResult GetCategoryById() {

            return Ok();
        }


        [Route("addCategory")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCategory()
        {
            Category category = new Category()
            {
                Name = "Building",
                Details = "Category for all the Building related odd jobs / services"
            };

            await _repo.CreateCategory(category);

            return Ok(String.Format("The category {0} has been successfully created", category.Name ));
        }
    }
}
