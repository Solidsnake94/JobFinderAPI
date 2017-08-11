using JobFinderAPI.Repositories;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private UserRepository _repo = new UserRepository();

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
