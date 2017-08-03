using JobFinderAPI.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JobFinderAPI
{
    public class AuthContext: IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<UserDetail> UsersDetails { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobsApplications {get; set;}


    }
}