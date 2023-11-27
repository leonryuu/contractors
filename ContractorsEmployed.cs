using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace RecruitmentSystem
{
    public class EmployedContractors
    {
        public List<Contractor> employedContractors = new List<Contractor>();

        // method to add a contractor
        public void AddContractor(Contractor contractor)
        {
            employedContractors.Add(contractor);
        }

        public List<Contractor> GetContractors()
        {
            return employedContractors;
        }

        public void RemoveContractor(Contractor contractor)
        {
            employedContractors.Remove(contractor);
        }
    }


}