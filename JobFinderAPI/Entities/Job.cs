using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobFinderAPI.Entities
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int Hours { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }

        // Job location
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}