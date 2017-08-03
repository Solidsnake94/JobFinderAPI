using JobFinderAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using JobFinderAPI.Models;
using System.Reflection;

namespace JobFinderAPI.Repositories
{
    public class JobRepository
    {

        private AuthContext dbContext;

        public JobRepository()
        {
            dbContext = new AuthContext();
        }

        public async Task<IQueryable<Job>> GetAllPendingJobs()
        {
            try
            {
                var jobs = dbContext.Jobs.Where(j => j.Status == "PENDING");
                return jobs;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<PagedResult<Job>> GetPendingJobs(int offset = 0, int limit = 10, string filter = "DateStart", bool orderByDesc = false)
        {
            try
            {
                // get the class property for given class
                var orderByProperty = typeof(Job).GetProperty(filter);

                // get the total amount of jobs in table
                var totalJobs = dbContext.Jobs.Count();

                // get the jobs page according to set parameters
                var jobs = dbContext.Jobs
                            .Where(j => j.Status == "PENDING");


                // set in what order the jobs are sorted 
                switch (orderByDesc)
                {
                    case false:
                        jobs.OrderBy(j => orderByProperty.GetValue(j, null));
                        break;

                    case true:
                        jobs.OrderByDescending(j => orderByProperty.GetValue(j, null));
                        break;
                };

                // SKIP the amount of jobs specified by the OFFSET and TAKE less or equal amount of jobs specified by the LIMIT
                jobs.Skip(offset)
                     .Take(limit);

                //get the amount of jobs returned for the page
                var jobsReturned = jobs.Count();

                // set the return PageResult with pages IQuerable and extra pagind details 
                var pagedResult = new PagedResult<Job>(jobs, offset, limit, filter, orderByDesc, totalJobs, jobsReturned);

                return pagedResult;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public async Task<bool> CreateJob(Job job)
        {
            try
            {
                dbContext.Jobs.Add(job);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateJob(Job newJob)
        {
            try
            {
                var oldJob = dbContext.Jobs.SingleOrDefault(j => j.Id == newJob.Id);
                if (oldJob != null)
                {
                    oldJob = newJob;
                }
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



      

        public async Task<bool> CreateJobApplication(int jobId, int jobApplicationUserId)
        {
            try
            {
                var jobApplication = new JobApplication() {

                    JobId = jobId,
                    ApplicantId = jobApplicationUserId,
                    Status = "PENDING"

                };

                dbContext.JobsApplications.Add(jobApplication);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> UpdateJobApplicationStatus(int jobApplicationId, string status)
        {
            try
            {
                JobApplication jobApplication = dbContext.JobsApplications.SingleOrDefault(j => j.Id == jobApplicationId);
                jobApplication.Status = status;
                
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