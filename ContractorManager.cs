using RecruitmentSystem;
using System;

namespace RecruitmentSystem
{
  public class ContractorManager
  {
    public ContractorManager()
    {
    }

    public void AddContractor(string firstName, string lastName, DateTime startDate, decimal hourlyWage)
    {
      // Create the new contractor
      Contractor contractor = new Contractor(firstName, lastName, startDate, hourlyWage);

      // Add the contractor to the list
      employedContractors.AddContractor(contractor);
    }
  }
}