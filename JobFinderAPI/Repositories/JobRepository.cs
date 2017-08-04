﻿using JobFinderAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using JobFinderAPI.Models;
using System.Reflection;
using JobFinderAPI.Extensions;

namespace JobFinderAPI.Repositories
{
    public class JobRepository
    {

        private AuthContext dbContext;

        public JobRepository()
        {
            dbContext = new AuthContext();
        }

        // ====== JOB METHODS

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

        public async Task<PagedResult<Job>> GetPendingJobs(int offset = 0, int limit = 10, string filter = "DateStart", bool orderByAscen = true)
        {
            try
            {
                IQueryable<Job> jobs = dbContext.Jobs.Where(j => j.Status == "PENDING").OrderByField(filter, orderByAscen).Skip(offset).Take(limit);

                int totalJobs = dbContext.Jobs.Count();
                int jobsReturned = jobs.Count();

                // set the return PageResult with pages IQuerable and extra pagind details 
                var pagedResult = new PagedResult<Job>(jobs, offset, limit, filter, orderByAscen, totalJobs, jobsReturned);

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

        // ======= END JOB METHODS =================





        // --------------------------------------------------------------------




        // ======= JOB_APPLICATION METHODS =================

        public async Task<IQueryable<JobApplication>> GetJobApplicationsForApplicant(int applicantId)
        {
            try
            {
                var applications = dbContext.JobsApplications.Where(j => j.ApplicantId == applicantId);
                return applications;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get all applications where its specific job id
        public async Task<IQueryable<JobApplication>> GetJobApplicationsForJobOwner(int jobId)
        {
            try
            {
                var applications = dbContext.JobsApplications.Where(j => j.JobId == jobId);
                return applications;
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
                var hasJobId = dbContext.Jobs.Any(j => j.Id == jobId);
                var hasApplicantId = dbContext.UsersDetails.Any(u => u.Id == jobApplicationUserId);

                // if cant find job or applicant id return false
                if (!hasJobId || !hasApplicantId)
                {
                    return false;
                }

                // if application is already existing return false
                var existingApplication = dbContext.JobsApplications.Any(a => (a.JobId == jobId && a.Id == jobApplicationUserId));
                if (existingApplication) {

                    return false;

                }

                // Else continue and create jobApplication
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

        // ====== END JOB_APPLICATION METHODS =================



    }
}