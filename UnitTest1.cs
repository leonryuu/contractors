using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest_Test_Project
{

    [TestClass]
    public class UnitTest1
    {

        private ContractorManager contractorManager;

        [TestInitialize]
        public void TestInitialize()
        {
            // Perform setup tasks here that you want to run before each test method
            // This method will be called before every test method in this test class

            contractorManager = new ContractorManager();
        }

        [TestMethod]
        public void TestAddContractor_buttonclick2()
        {
            // Set the apartment state to STA before creating the window
            Thread thread = new Thread(() =>
            {
                MainWindow mainWindow = new MainWindow();

                // Set input values
                mainWindow.Dispatcher.Invoke(() =>
                {
                    mainWindow.Textbox_FirstName.Text = "Bob";
                    mainWindow.Textbox_LastName.Text = "Smith";
                    mainWindow.DatePicker_StartDate.SelectedDate = new DateTime(2023, 11, 25);
                    mainWindow.Textbox_HourlyWage.Text = "25";
                });

                // Act
                mainWindow.Dispatcher.Invoke(() => mainWindow.AddContractor_buttonclick(null, null));

                // Assert
                mainWindow.Dispatcher.Invoke(() =>
                {
                    var contractors = mainWindow.employedContractors.GetContractors();

                    Assert.IsTrue(contractors.Count > 0);
                });
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }


        public void JAMES_TestAddContractor_buttonclick()
        {
            // Arrange
            Contractor contractor = new Contractor("David", "Do", new DateTime(2023, 1, 1), 100M)

            // Act
            contractorManager.AddContractor("david", "do", new DateTime(), 100.1);

            // Assert
            var contractors = mainWindow.employedContractors.GetContractors();
            Assert.AreEqual(contractors[0], contractor)
        }

        [TestMethod]
        public void TestAddjob()
        {
            // Set the apartment state to STA before creating the window
            Thread thread = new Thread(() =>
            {
                MainWindow mainWindow = new MainWindow();

                // Set input values
                mainWindow.Dispatcher.Invoke(() =>
                {
                    mainWindow.Textbox_JobName.Text = "Roofworks";
                    mainWindow.Textbox_Cost.Text = "2500";
                    mainWindow.DatePicker_JobStartDate.SelectedDate = new DateTime(2023, 11, 25);
                });

                // Act
                mainWindow.Dispatcher.Invoke(() => mainWindow.AddJob(null, null));

                // Assert
                mainWindow.Dispatcher.Invoke(() =>
                {
                    var jobs = mainWindow.activeJobs.GetJobs();

                    Assert.IsTrue(jobs.Count > 0);
                });
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }


        public void TestCompleteJob()
        {
            // Arrange
            MainWindow mainWindow = new MainWindow();

            // Set up a job for testing
            Jobs job = new Jobs("Lights", 500, DateTime.Now, new Contractor("Bob", "Smith", DateTime.Now, 25));
            mainWindow.activeJobs.AddJob(job);

            // Add the job to Jobs_display
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = job;

            // Use a thread to set up the UI elements
            Thread thread = new Thread(() =>
            {
                mainWindow.Jobs_display.Items.Add(listBoxItem);
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Act
            thread = new Thread(() =>
            {
                mainWindow.CompleteJob(null, null);
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Assert
            thread = new Thread(() =>
            {
                Assert.AreEqual(0, mainWindow.Jobs_display.Items.Count); // Check if the ListBox is empty
                Assert.AreEqual(0, mainWindow.activeJobs.GetJobs().Count); // Check if the job is removed from the activeJobs list
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }


        public void TestAddContractorAndRemove()
        {
            Thread thread = new Thread(() =>
            {
                // Arrange
                MainWindow mainWindow = new MainWindow();

                // Set input values
                mainWindow.Dispatcher.Invoke(() =>
                {
                    mainWindow.Textbox_FirstName.Text = "Adam";
                    mainWindow.Textbox_LastName.Text = "Smith";
                    mainWindow.DatePicker_StartDate.SelectedDate = new DateTime(2023, 1, 1);
                    mainWindow.Textbox_HourlyWage.Text = "250";
                });

                // Act
                mainWindow.Dispatcher.Invoke(() => mainWindow.AddContractor_buttonclick(null, null));

                // Assert
                mainWindow.Dispatcher.Invoke(() =>
                {
                    var contractors = mainWindow.employedContractors.GetContractors();

                    // Ensure that the contractor is added
                    Assert.AreEqual(1, contractors.Count);
                });

                // Act
                mainWindow.Dispatcher.Invoke(() => mainWindow.RemoveContractor(null, null));

                // Assert
                mainWindow.Dispatcher.Invoke(() =>
                {
                    var contractors = mainWindow.employedContractors.GetContractors();

                    // Ensure that the contractor is removed
                    Assert.AreEqual(0, contractors.Count);

                    Assert.IsFalse(contractors.Any(c =>
                    c.FirstName == "Adam" && c.LastName == "Smith" && c.StartDate == new DateTime(2023, 1, 1) && c.HourlyWage == 20));
                });
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

    }
}