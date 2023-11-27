using RecruitmentSystem;
using System;

namespace RecruitmentSystem
{
    public class Contractor
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public decimal HourlyWage { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Contractor(string firstName, string lastName, DateTime startDate, decimal hourlyWage)
        {
            FirstName = firstName;
            LastName = lastName;
            StartDate = startDate;
            HourlyWage = hourlyWage;
        }
        public override string ToString()
        {
            return FullName;
        }
    }
}