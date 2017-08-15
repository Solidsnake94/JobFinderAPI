using JobFinderAPI.Repositories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private UserRepository _repo = new UserRepository();


        // The method returns the authenticated user, logged in one, based on the token used for authentication

        
        [Route("userId")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAuthenticatedUser(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         

            var user = await _repo.GetAuthenticatedUser(userName);

            return Ok(user);
        }


        [Route("details")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserDetailsById(int userId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _repo.GetUserDetailsById(userId);

            return Ok(user);
        }



    }
}
