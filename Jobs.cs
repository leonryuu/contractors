 using System;

namespace RecruitmentSystem
{
    public class Jobs
    {
        public string JobName { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateStarted { get; set; }
        public Contractor ContractorAssigned { get; set; }

        public Jobs(string jobName, decimal cost, DateTime dateStarted, Contractor contractorAssigned)
        {
            JobName = jobName;
            Cost = cost;
            DateStarted = dateStarted;
            ContractorAssigned = contractorAssigned;
        }

        public override string ToString()
        {
            return $"{JobName}, {Cost}, {DateStarted.ToShortDateString()}, {ContractorAssigned}";
        }
    }
}