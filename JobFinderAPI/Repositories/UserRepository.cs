using JobFinderAPI.Entities;
using JobFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JobFinderAPI.Repositories
{
    public class UserRepository
    {
        private AuthContext dbContext;

        public UserRepository()
        {
            dbContext = new AuthContext();
        }


        public async Task<bool> AddUserDetail(string id,  UserModel userModel)
        {
            try
            {
                UserDetail newUserDetail = new UserDetail()
                {
                    UserId = id,
                    Username = userModel.UserName,
                    DateOfBirth = userModel.DateOfBirth,
                    Email = userModel.Email,
                    Name = userModel.Name,
                    Surname = userModel.Surname
                };

                dbContext.UsersDetails.Add(newUserDetail);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}