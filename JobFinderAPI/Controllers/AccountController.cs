using JobFinderAPI.Models;
using JobFinderAPI.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository authRepo = null;
        private UserRepository userRepo = null;

        
        public AccountController()
        {
            authRepo = new AuthRepository();
            userRepo = new UserRepository();
        }


        // Fake users method generation
        private async Task<bool> generateUsers() {

            string[] names = new string[] { "Adam", "Bruce", "Conrad", "Dequan", "Emmanuel", "Frederick", "Gene", "Henry", "Igor", "Jamal", "Komandos", "Mustafa" };

            for (int i = 0; i < names.Length; i++)
            {
                var surname = names[i] + "ski";
                var username = names[i] + surname;
                var email = username.ToLower() + "@gmail.com";

                var newUser = new UserModel()
                {
                    Name = names[i],
                    Surname = surname,
                    UserName = username,
                    Email = email,
                    DateOfBirth = new DateTime(1994, names[i].Length + 1, names[i].Length + 2),
                    Password = "SuperPass",
                    ConfirmPassword = "SuperPass"
                };

                IdentityResult result = await authRepo.RegisterUser(newUser);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return false;
                }
                else
                {

                    IdentityUser user = await authRepo.FindUser(newUser.UserName, newUser.Password);
                    await userRepo.AddUserDetail(user.Id, newUser);

               }
            }

            return true;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await authRepo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }
            else
            {

                IdentityUser user = await authRepo.FindUser(userModel.UserName, userModel.Password);
                await userRepo.AddUserDetail(user.Id, userModel);

            }


            return Ok();
        }

        [AllowAnonymous]
        [Route("FindUser")]
        [HttpGet]
        public async Task<IHttpActionResult> FindUser()
        {
            var user = await authRepo.FindUserByName("PawelSmolinski");

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                authRepo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }



    }
}
