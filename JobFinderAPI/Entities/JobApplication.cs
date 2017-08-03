using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobFinderAPI.Entities
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ApplicantId { get; set; }
        public string Status { get; set; }


        public virtual UserDetail UserDetails { get; set; }
        public virtual Job Job { get; set; }

    }
}