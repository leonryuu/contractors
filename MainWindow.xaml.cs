using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecruitmentSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creating instances of ActiveJobs/EmployedContractors
        public EmployedContractors employedContractors = new EmployedContractors();
        public ActiveJobs activeJobs = new ActiveJobs();


        public MainWindow()
        {
            InitializeComponent();

            // Setting data context of the window to employedContractors
            DataContext = employedContractors;

        }

        public void AddContractor_buttonclick(object sender, RoutedEventArgs e)
        {
            // Get input data from the textboxes and date picker

            string firstName = Textbox_FirstName.Text;
            string lastName = Textbox_LastName.Text;
            DateTime startDate = DatePicker_StartDate.SelectedDate ?? DateTime.Now;

            // TryParse the inputted to value to ensure it is a decimal

             if (decimal.TryParse(Textbox_HourlyWage.Text, out decimal hourlyWage))
             {
                // Create the new contractor
                Contractor contractor = new Contractor(firstName, lastName, startDate, hourlyWage);


                // Add the contractor to the list
                employedContractors.AddContractor(contractor);

                // Add the contractor to the ComboBox
                Contractor_Assigned.Items.Add(contractor);

                // Message to show that contractor is added
                MessageBox.Show("Contractor added successfully. Please refresh Get Contractors.");

                // clear the input fields
                Textbox_FirstName.Text = "Enter First Name";
                Textbox_LastName.Text = "Enter Last Name";
                Textbox_HourlyWage.Text = "Enter Hourly Wage";
                DatePicker_StartDate.SelectedDate = null; // Reset the date picker
             }
            else
            {
                // Handle error where wage is not a numerical value
                MessageBox.Show("Please enter numerical values for wage.");
            }
        }



        public void RemoveContractor(object sender, RoutedEventArgs e)
        {
            if (Contractor_display.SelectedItem != null)
            {
                // Get the selected ListBoxItem
                ListBoxItem selectedListBoxItem = (ListBoxItem)Contractor_display.SelectedItem;

                // Extract the content from the ListBoxItem
                string formattedString = (string)selectedListBoxItem.Content;

                // Parse the string to extract the contractor details
                string[] parts = formattedString.Split(',');

                if (parts.Length == 3)
                {
                    string[] nameSegments = parts[0].Split(' ');

                    if (nameSegments.Length == 2)
                    {
                        string firstName = nameSegments[0].Trim();
                        string lastName = nameSegments[1].Trim();
                        DateTime startDate = DateTime.Parse(parts[1].Trim());
                        decimal hourlyWage = decimal.Parse(parts[2].Trim());

                        // Find the selected contractor in the contractor list
                        Contractor selectedContractor = null;

                        foreach (var contractor in employedContractors.GetContractors())
                        {
                            if (contractor.FirstName == firstName && contractor.LastName == lastName
                                && contractor.StartDate == startDate && contractor.HourlyWage == hourlyWage)
                            {
                                selectedContractor = contractor;
                                break;
                            }
                        }

                        if (selectedContractor != null)
                        {
                            // Remove the selected contractor from the ListBox
                            Contractor_display.Items.Remove(selectedListBoxItem);

                            // Remove the selected contractor from the list of contractors
                            employedContractors.RemoveContractor(selectedContractor);
                        }
                    }
                }
            }
        }



        public void GetContractors(object sender, RoutedEventArgs e)
        {
            Contractor_display.Items.Clear();

            // Retrieve the list of contractors from the employedContractors instance
            var contractorList = employedContractors.GetContractors();

            foreach (var contractor in contractorList)
            {
                // Creating a new ListBoxItem to represent each contractor in the Listbox

                ListBoxItem listBoxItem = new ListBoxItem();

                // Format the text to include the Contractor ID
                string formattedString = $"{contractor.FirstName} {contractor.LastName}, {contractor.StartDate.ToShortDateString()}, {contractor.HourlyWage}";

                listBoxItem.Content = formattedString;

                Contractor_display.Items.Add(listBoxItem);
            }
        }


        public void GetUnassignedJobs(object sender, RoutedEventArgs e)
        {
            Jobs_display.Items.Clear();

            
            // Retrieve list of jobs where the assigned contractor is equal to null

            var unassignedJobs = activeJobs.GetJobs().Where(job => job.ContractorAssigned == null);

            foreach (var job in unassignedJobs)
            {
                // Creating a ListBoxItem to represent every unassigned job
                ListBoxItem listBoxItem = new ListBoxItem();

                // Display the unassigned job
                listBoxItem.Content = job;

                Jobs_display.Items.Add(listBoxItem);
            }

            // Clear the input fields for Jobs unpon Unassigned Jobs button click
            Textbox_JobName.Text = "Enter Job Name";
            Textbox_Cost.Text = "Enter cost";
            DatePicker_JobStartDate.SelectedDate = null;
            Contractor_Assigned.SelectedItem = null;

            // Clear the selected item in Jobs_display
            Jobs_display.SelectedItem = null;
        }

        public void AddJob(object sender, RoutedEventArgs e)
        {
            
            // Get input data from the textboxes and date picker
            string jobName = Textbox_JobName.Text;
            string cost = Textbox_Cost.Text;
            DateTime jobStartDate = DatePicker_JobStartDate.SelectedDate ?? DateTime.Now;

            Contractor contractorAssigned = (Contractor)Contractor_Assigned.SelectedItem;

            if (decimal.TryParse(cost, out decimal costValue))
            {
                // Input a new job and its details in the Jobs List 
                Jobs job = new Jobs(jobName, decimal.Parse(cost), jobStartDate, contractorAssigned);

                // Add the job to the list of active jobs
                activeJobs.AddJob(job);

                // Message to show that a job has been added
                MessageBox.Show("Job added successfully. Please refresh Job List.");

                // Clear input fields

                Textbox_JobName.Text = "Enter Job Name";
                Textbox_Cost.Text = "Enter cost";
                DatePicker_JobStartDate.SelectedDate = null;
                Contractor_Assigned.SelectedItem = null;
            }
            else
            {
                // Handle error where cost is not a numerical value
                MessageBox.Show("Please enter a numerical value for Cost.");
            }

        }

        public void GetJobs(object sender, RoutedEventArgs e)
        {
            Jobs_display.Items.Clear();

            // Retrieve the list of jobs from the activeJobs instance
            var jobsList = activeJobs.GetJobs();

            foreach (var job in jobsList)
            {
                ListBoxItem listBoxItem = new ListBoxItem();

                // Display the Job and assigned contractor
                listBoxItem.Content = job;

                Jobs_display.Items.Add(listBoxItem);
            }

            // Clear the input fields when Job List button is clicked
            Textbox_JobName.Text = "Enter Job Name";
            Textbox_Cost.Text = "Enter cost";
            DatePicker_JobStartDate.SelectedDate = null;
            Contractor_Assigned.SelectedItem = null;

            // Clear the selected item in Jobs_display when Job list button is clicked
            Jobs_display.SelectedItem = null;
        }


        public void CompleteJob(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the Jobs_display ListBox
            if (Jobs_display.SelectedItem != null)
            {
                // Attempt to extract the Jobs object from the ListBoxItem's Content
                if (Jobs_display.SelectedItem is ListBoxItem selectedListBoxItem)
                {
                    if (selectedListBoxItem.Content is Jobs selectedJob)
                    {
                        // Remove the selected job from the Jobs_display ListBox
                        Jobs_display.Items.Remove(selectedListBoxItem);

                        // Remove the selected job from the activeJobs list
                        activeJobs.RemoveJob(selectedJob);

                        // Clear the input fields when Complete Job button is clicked
                        Textbox_JobName.Text = "Enter Job Name";
                        Textbox_Cost.Text = "Enter cost";
                        DatePicker_JobStartDate.SelectedDate = null;
                        Contractor_Assigned.SelectedItem = null;

                        // Clear the selected item in Jobs_display when Complete Job button is clicked
                        Jobs_display.SelectedItem = null;
                    }
                }
            }
        }

        public void GetAvailableContractors(object sender, RoutedEventArgs e)
        {
            // Get the list of all contractors
            var allContractors = employedContractors.GetContractors();

            // Get the list of contractors assigned to jobs
            var assignedContractors = activeJobs.GetJobs().Where(job => job.ContractorAssigned != null)
                .Select(job => job.ContractorAssigned);

            // Get the list of available contractors (not assigned to any job)
            var availableContractors = allContractors.Except(assignedContractors);

            Contractor_display.Items.Clear();

            foreach (var contractor in availableContractors)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                string formattedString = $"{contractor.FirstName} {contractor.LastName}, {contractor.StartDate.ToShortDateString()}, {contractor.HourlyWage}";
                listBoxItem.Content = formattedString;
                Contractor_display.Items.Add(listBoxItem);
            }
        }

        public void Jobs_display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Jobs_display.SelectedItem != null)
            {
                // Get the selected ListBoxItem
                ListBoxItem selectedListBoxItem = (ListBoxItem)Jobs_display.SelectedItem;

                // Extract Jobs object from the contents of the selection
                Jobs selectedJob = (Jobs)selectedListBoxItem.Content;

                // DIsplay the extracted information in the respective Job textboxes
                Textbox_JobName.Text = selectedJob.JobName;
                Textbox_Cost.Text = selectedJob.Cost.ToString();
                DatePicker_JobStartDate.SelectedDate = selectedJob.DateStarted;

                if (selectedJob.ContractorAssigned != null)
                {
                    // Sets the selected contractor in the Contractor_Assigned ComboBox
                    Contractor_Assigned.SelectedItem = selectedJob.ContractorAssigned;
                }
            }
        }

        public void UpdateJob(object sender, RoutedEventArgs e)
        {
            // Check if a job is selected in Jobs_display
            if (Jobs_display.SelectedItem != null)
            {
                // Get the selected ListBoxItem
                ListBoxItem selectedListBoxItem = (ListBoxItem)Jobs_display.SelectedItem;

                // Extract the Jobs object from the content
                if (selectedListBoxItem.Content is Jobs selectedJob)
                {
                    // Get input data from the textboxes and date picker
                    string jobName = Textbox_JobName.Text;
                    string cost = Textbox_Cost.Text;
                    DateTime jobStartDate = DatePicker_JobStartDate.SelectedDate ?? DateTime.Now;

                    // Check if a contractor is selected in the ComboBox
                    if (Contractor_Assigned.SelectedItem != null)
                    {
                        // create a new variable to assign a selected contractor
                        Contractor contractorAssigned = null;

                        // Check if the selected item is a contractor
                        if (Contractor_Assigned.SelectedItem is Contractor selectedContractor)
                        {
                            contractorAssigned = selectedContractor;
                        }

                        // Attempt to parse cost to decimal
                        if (decimal.TryParse(cost, out decimal costValue))
                        {
                            // Update the details of the selected job
                            selectedJob.JobName = jobName;
                            selectedJob.Cost = costValue;
                            selectedJob.DateStarted = jobStartDate;
                            selectedJob.ContractorAssigned = contractorAssigned;

                            // Update the content of the ListBoxItem directly
                            selectedListBoxItem.Content = selectedJob;

                            // Message to show that the job was updated
                            MessageBox.Show("Job UPDATED successfully. Please refresh Job List");

                            // Clear the input fields
                            Textbox_JobName.Text = "Enter Job Name";
                            Textbox_Cost.Text = "Enter cost";
                            DatePicker_JobStartDate.SelectedDate = null;
                            Contractor_Assigned.SelectedItem = null;

                            // Clear the selected item in Jobs_display
                            Jobs_display.SelectedItem = null;
                        }
                        else
                        {
                            // Handle error where cost value is not numerical
                            MessageBox.Show("Please enter numerical value for Cost.");
                        }
                    }
                }
            }
        }

        public void CostRangeJobs(object sender, RoutedEventArgs e)
        {
            // Get input data from the textboxes
            if (decimal.TryParse(Textbox_mincost.Text, out decimal minCost) && decimal.TryParse(Textbox_maxcost.Text, out decimal maxCost))
            {
                // Filter jobs based on the cost range
                var filteredJobs = activeJobs.GetJobs().Where(job => job.Cost >= minCost && job.Cost <= maxCost).ToList();

                // Clear Jobs_display
                Jobs_display.Items.Clear();

                foreach (var job in filteredJobs)
                {
                    ListBoxItem listBoxItem = new ListBoxItem();

                    // Display the Job and assigned contractor
                    string formattedString = $"{job.JobName}, {job.Cost}, {job.DateStarted.ToShortDateString()}, {job.ContractorAssigned}";

                    listBoxItem.Content = formattedString;

                    Jobs_display.Items.Add(listBoxItem);
                }
            }
            
            else
            {
                // Handle invalid input
                MessageBox.Show("Please enter valid numerical values for the cost range.");
            }
        }

    }
}