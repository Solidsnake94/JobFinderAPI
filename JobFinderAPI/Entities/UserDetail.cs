using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobFinderAPI.Entities
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        
        // the id from the dbo.AspNetUser table to reference the authenticated user but avoiding modifing the orignal ASP.NET Identity classes / schemas/ models 
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }


    }
}