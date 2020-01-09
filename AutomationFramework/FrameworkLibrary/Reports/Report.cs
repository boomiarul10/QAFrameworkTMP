using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace TB2
{
    public class Report
    {
        HTMLReport htmlReport;
        ExcelReport excelReport;
        TestCase testCase;
        string testReportFolder;
        string screenShotFolder;
        string screenShotFilePath;
        DateTime startTime;
        public RemoteWebDriver Driver { get; set; }

        public Report()
        {

        }

        public Report(TestCase currentTestCase, RemoteWebDriver driver)
        {
            startTime = DateTime.Now;
            testCase = currentTestCase;
            Driver = driver;
            CreateReportFolder();
            try
            {
                ICapabilities capabilities = ((RemoteWebDriver)driver).Capabilities;
                string browserDetails = currentTestCase.Browser + " " + capabilities.Version;
                currentTestCase.browserDetails = browserDetails;
                htmlReport = new HTMLReport(testReportFolder, screenShotFolder, browserDetails, currentTestCase);
                if (HelperClass.runGenerateExcelReport)
                    excelReport = new ExcelReport(testReportFolder, screenShotFolder, browserDetails, currentTestCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        public void GenerateHeader()
        {
            htmlReport.GenerateHeader();
            if (HelperClass.runGenerateExcelReport)
                excelReport.GenerateHeader();
        }

        private void CreateReportFolder()
        {
            string testCaseFolder = HelperClass.reportRunPath + "\\" + testCase.TestCaseName;
            if (!Directory.Exists(testCaseFolder))
                Directory.CreateDirectory(testCaseFolder);
            testReportFolder = testCaseFolder;
            string screenShotPath = testCaseFolder + "\\" + "ScreenShots";
            if (!Directory.Exists(screenShotPath))
                Directory.CreateDirectory(screenShotPath);
            screenShotFolder = screenShotPath;
            testCase.QCScreenShotPath = screenShotFolder;
            
        }

        public void AddReportStep(string description, string actualResult, StepResult stepResult)
        {
            screenShotFilePath = string.Empty;
            if (stepResult == StepResult.FAIL)
                TakeScreenShot();
            htmlReport.AddReportStep(description, actualResult, stepResult, screenShotFilePath);
            if (HelperClass.runGenerateExcelReport)
                excelReport.AddReportStep(description, actualResult, stepResult, screenShotFilePath);

        }

        // Venky added this method for scenario headers
        public void AddScenarioHeader(string ScenarioName)
        {
            
                excelReport.CreateScenarioHeader(ScenarioName);
        }

        private void TakeScreenShot()
        {
            string formattedDate = string.Format("_{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            if (htmlReport.TotalIterationCount > 1)
                screenShotFilePath = ".\\Screenshots\\" + testCase.TestCaseName + "_It_No_" + htmlReport.CurrentIteration + "_Step-No-" + (htmlReport.stepNo + 1) + formattedDate + ".png";
            else
                screenShotFilePath = ".\\Screenshots\\" + testCase.TestCaseName + "_Step-No-" + (htmlReport.stepNo + 1) + formattedDate + ".png";
            
            string screnShotSavePath = string.Empty;           
            if (htmlReport.TotalIterationCount > 1)
                screnShotSavePath = screenShotFolder + "\\" + testCase.TestCaseName + "_It_No_" + htmlReport.CurrentIteration + "_Step-No-" + (htmlReport.stepNo + 1) + formattedDate + ".png";
            else
                screnShotSavePath = screenShotFolder + "\\" + testCase.TestCaseName + "_Step-No-" + (htmlReport.stepNo + 1) + formattedDate + ".png";
            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
                ss.SaveAsFile(screnShotSavePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SetCurrentIteration(int currentIteration)
        {
            htmlReport.CurrentIteration = currentIteration;
            htmlReport.AddNewIteration();
            if (HelperClass.runGenerateExcelReport)
            {
                excelReport.CurrentIteration = currentIteration;
                excelReport.AddNewIteration();
            }
        }

        public void SetTotalIterationCount(int totalIterationCount)
        {
            htmlReport.TotalIterationCount = totalIterationCount;
            if (HelperClass.runGenerateExcelReport)
                excelReport.TotalIterationCount = totalIterationCount;
        }


        public void GenerateFooter()
        {
            DateTime endTime = DateTime.Now;
            TimeSpan timeTaken = endTime.Subtract(startTime);
            string executionTime = timeTaken.Minutes + " mins and "
                + timeTaken.Seconds + " secs";

            htmlReport.GenerateFooter(executionTime);
            if (HelperClass.runGenerateExcelReport)
                excelReport.GenerateFooter(executionTime);

            testCase.OverAllResult = htmlReport.testCase.OverAllResult;
            testCase.TimeTaken = executionTime;
            testCase.ExecutionTime = timeTaken;
        }

        public void AddErrorStep()
        {
            htmlReport.AddErrorStep();
            if (HelperClass.runGenerateExcelReport)
                excelReport.AddErrorStep();
        }

        public void GenerateMultiBrowserDetailedReport(List<TestCase> lstMultiBrowserTc, TimeSpan timeTaken)
        {
            HTMLReport rptMultiBrowserDetailed = new HTMLReport();
            rptMultiBrowserDetailed.GenerateMultiBrowserDetailedReport(lstMultiBrowserTc, timeTaken);
        }
    }
}
