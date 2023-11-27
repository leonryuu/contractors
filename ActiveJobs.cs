using System;
using System.Collections.Generic;

namespace RecruitmentSystem
{
    public class ActiveJobs
    {
        public List<Jobs> activeJobs { get; } = new List<Jobs>();


        // Method to add a job
        public void AddJob(Jobs job)
        {
            activeJobs.Add(job);
        }

        public List<Jobs> GetJobs()
        {
            return activeJobs;
        }
        public void RemoveJob(Jobs job)
        {
            activeJobs.Remove(job);
        }
    }
}
