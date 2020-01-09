using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TB2
{
    public class BaseExecutionClass
    {
        public Report report { get; set; }

        public DataHelper dataHelper { get; set; }

        public RemoteWebDriver driver { get; set; }

        
        public BaseExecutionClass()
        {
            
        }

        public BaseExecutionClass(TestCase currentTestCase)
        {            
            dataHelper = TestSetUp.GetDataHelper(currentTestCase);
            Driver driverClass = new Driver();
            driver = driverClass.GetWebDriver(currentTestCase);
            report = new Report(currentTestCase, driver);
            report.Driver = this.driver;
        }
       

        public void GenerateFooter()
        {
            report.GenerateFooter();
        }

        public void AddErrorStep()
        {
            report.AddErrorStep();
        }

        public void EndTestCase()
        {
            Driver.QuitDriver(driver);
        }


        public void EndTestExecution()
        {
            Driver.QuitDriver(driver);
        }
        public void SetToCurrentIteration(int currentIteration)
        {
            dataHelper.SetToCurrentIteration(currentIteration);
            report.SetCurrentIteration(currentIteration);
        }

        public void SetTotalIterationCount(int totalIterationCount)
        {
            report.SetTotalIterationCount(totalIterationCount);
        }

        public void GenerateHeader()
        {
            report.GenerateHeader();
        }

    }
   
    public class Browse : BaseExecutionClass
    {
        public Browse()
        {

        }

        public Browse(TestCase currentTestCase)
            : base(currentTestCase)
        {
                 
        }


        public void MethodPASS()
        {
            Console.WriteLine("Method PASS Called");
            Console.WriteLine("Method Pass First Name is :" +dataHelper.GetData("FirstName"));
            report.AddReportStep("Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called",
                              "Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called Method PASS Called",
                              StepResult.PASS);
        }

        public void MethodFAIL()
        {
            //driver.Navigate().GoToUrl("http://www.theautomatedtester.co.uk");
            Console.WriteLine("Method FAIL Called");
            Console.WriteLine("Method FAIL First Name is :" + dataHelper.GetData("FirstName"));
            report.AddReportStep("Method FAIL Called",
                              "Method FAIL Called",
                              StepResult.FAIL);
        }

        public void MethodWARNING()
        {
            Console.WriteLine("Method WARNING Called");
            Console.WriteLine("Method Warning LastName Name is :" + dataHelper.GetData("LastName"));
            report.AddReportStep("Method WARNING Called",
                              "Method WARNING Called",
                              StepResult.WARNING);
        }

        public void Method1()
        {
            Console.WriteLine("Method 1 Called");
            report.AddReportStep("Method 1 Called",
                              "Method 1 Called Passed",
                              StepResult.PASS);
        }

        public void Method2()
        {
            report.AddReportStep("Method 2 Called",
                             "Method 2 Called Failed",
                             StepResult.FAIL);
        }

        public void Method3()
        {
            Console.WriteLine("Method 3 Called");
            report.AddReportStep("Method 3 Called",
                             "Method 3 Called Failed",
                             StepResult.WARNING);
        }

        public void Method4()
        {
            Console.WriteLine("Method 4 Called");
            report.AddReportStep("Method 4 Called",
                             "Method 4 Called Failed",
                             StepResult.PASS);
        }

        public void Method5()
        {
            Console.WriteLine("Method 5 Called");
            report.AddReportStep("Method 5 Called",
                             "Method 5 Called Failed",
                             StepResult.FAIL);
        }

        public void Method6()
        {
            Console.WriteLine("Method 6 Called");
            report.AddReportStep("Method 6 Called",
                             "Method 6 Called Failed",
                             StepResult.FAIL);
        }

        public void Method7()
        {
            Console.WriteLine("Method 7 Called");
            report.AddReportStep("Method 7 Called",
                             "Method 7 Called Failed",
                             StepResult.PASS);
        }

        public void Method8()
        {
            Console.WriteLine("Method 8 Called");
            report.AddReportStep("Method 8 Called",
                            "Method 8 Called Failed",
                            StepResult.PASS);
        }

        public void Method9()
        {
            Console.WriteLine("Method 9 Called");
            report.AddReportStep("Method 9 Called",
                            "Method 9 Called Failed",
                            StepResult.FAIL);
        }

        public void Method10()
        {
            Console.WriteLine("Method 10 Called");
            report.AddReportStep("Method 10 Called",
                            "Method 10 Called Failed",
                            StepResult.PASS);
        }

        public void Method11()
        {
            Console.WriteLine("Method 11 Called");
        }

        public void Method22()
        {
            Console.WriteLine("Method 22 Called");
        }

        public void Method33()
        {
            Console.WriteLine("Method 33 Called");
        }
        
    }       
}
