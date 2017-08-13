using JobFinderAPI.Entities;
using JobFinderAPI.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{
    [RoutePrefix("api/jobs")]
    public class JobController : ApiController
    {
        private JobRepository _repo = new JobRepository();


        private async Task<bool> generateFakeJobs()
        {
            string[] titlesP1 = new string[] { "Walking", "Cleaning", "Showing", "Taking care of", "Moving", "Exercising with", "Talking to", "Entertaining", "Joking with", "Brushing", "Watching after", };
            string[] titlesP2 = new string[] { "cats", "dogs", "old people", "birds", "Donald Trump", "Michael Jordan", "Cristiano Ronaldo", "Lionel Messi", "Pope Francis", "Rocky Balboa", "John Rambo", };

            var randomNumber = new Random();

            for (var i = 0; i < 40; i++)
            {
                var titleP1Random = randomNumber.Next(titlesP1.Length - 1);
                var titleP2Random = randomNumber.Next(titlesP2.Length - 1);
                var createdBy = randomNumber.Next(1, 12);
                var hours = randomNumber.Next(1, 100);
                var categoryId = randomNumber.Next(1, 4);
                var price = randomNumber.Next(100, 10000);

                //55.694478, 12.550819 KEA ORIGINAL 
                // 55.706468, 12.539151 KEA LYNGTEN 16
                //  LAT, LONG
                var keaGeoLatitude = i % 2 == 0 ? "55.694478" : "55.706468";
                var keaGeoLongitude = i % 2 == 0 ? "12.550819" : "12.539151";

                var job = new Job()
                {
                    Title = titlesP1[titleP1Random] + " " + titlesP2[titleP2Random],
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. ",
                    CreatedBy = createdBy,
                    Hours = hours,
                    CategoryID = categoryId,
                    DateStart = DateTime.Now.AddDays(20 + i),
                    DateEnd = DateTime.Now.AddDays(22 + i),
                    Price = price,
                    Status = "PENDING",
                    Latitude = keaGeoLatitude,
                    Longitude = keaGeoLongitude,

                };

                await _repo.CreateJob(job);

            };

            return true;

        }

        // api/jobs?jobId=11
        [Route("")]
        public async Task<IHttpActionResult> GetJobDetails(int jobId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = _repo.GetJobDetails(jobId);

            return Ok(job);
        }

        // [Authorize]
        [Route("pending")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPendingJobs()
        {
            //var id = RequestContext.Principal.Identity.GetUserId();
            //var name = RequestContext.Principal.Identity.Name;
            //var id2 = User.Identity.GetUserId();
            //var name2 = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable jobs = await _repo.GetAllPendingJobs();

            return Ok(jobs);
        }

        [Route("pending/page")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPendingJobs(int userId, int offset, int limit, string filter, bool orderByAscen)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // get the paged job with paging details
            var pagedJobResult = await _repo.GetPendingJobs(userId, offset, limit, filter, orderByAscen);

            return Ok(pagedJobResult);
        }

        [Route("approved")]
        [HttpGet]
        public async Task<IHttpActionResult> GetApprovedJobsByUserId(int userId)
        {
            //var id = RequestContext.Principal.Identity.GetUserId();
            //var name = RequestContext.Principal.Identity.Name;
            //var id2 = User.Identity.GetUserId();
            //var name2 = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable jobs = await _repo.GetApprovedJobByUserId(userId);

            return Ok(jobs);
        }

        // Get the created job by the job id in the db
        [Route("created")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCreatedJobById(int jobId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = await _repo.GetCreatedJobByJobId(jobId);

            return Ok(job);
        }


        // Get all created jobs by for specific employer by employer id
        [Route("created/employer")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCreatedJobsByEmployerId(int id)
        {
            return Ok();
        }





        [Route("created")]
        // Create a new job by employer
        [HttpPost]
        public async Task<IHttpActionResult> CreateJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //await generateFakeJobs();

            await _repo.CreateJob(job);

            return Ok("Created new Job");
        }


        [Route("created")]
        //Update a job already created by employer
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCreatedJob(Job job)
        {
            return Ok("Updated job");
        }


        // Delete a job created by employer
        [Route("created")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCreatedJob(int jobId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //await generateFakeJobs();

            await _repo.DeleteJob(jobId);

            return Ok("Job was deleted");
        }



        [Route("application/job")]
        [HttpGet]
        public async Task<IHttpActionResult> GetJobApplicationsForJobOwner(int jobId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplications = await _repo.GetJobApplicationsForJobOwner(jobId);

            return Ok(jobApplications);
        }

        [Route("application/applicant")]
        [HttpGet]
        public async Task<IHttpActionResult> GetJobApplicationsForApplicant(int applicantId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplications = await _repo.GetJobApplicationsForApplicant(applicantId);

            return Ok(jobApplications);
        }

        [Route("application")]
        [HttpGet]
        public async Task<IHttpActionResult> GetJobApplicationsBasedOnStatus(string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplications = await _repo.GetJobApplicationsBasedOnStatus(status);

            return Ok(jobApplications);
        }

        [Route("application/create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateJobApplication(JobApplication jobApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _repo.CreateJobApplication(jobApplication.JobId, jobApplication.ApplicantId);

            if (success)
            {
                return Ok("Created new job application");
            }
            else
            {
                return BadRequest("Failed to create a new job application");
            }
        }



    }
}
