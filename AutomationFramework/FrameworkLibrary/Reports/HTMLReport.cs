using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TB2
{
    public class HTMLReport
    {
        StringBuilder reportBuider;
        //int stepNo;
        int noOfStepsPassed;
        int noOfStepsFailed;
        int overAllStepsPassed;
        int overAllStepsFailed;
        int overAllWarningSteps;
        DateTime startTime;
        public string testReportFolder { get; set; }
        public string screenShotFolder { get; set; }
        public TestCase testCase { get; set; }
        public int stepNo { get; set; }
        public int CurrentIteration { get; set; }
        public int TotalIterationCount { get; set; }
        public string BrowserDetails { get; set; }

        public HTMLReport()
        {
            
        }

        public HTMLReport(string reportPath, string screenShotPath, string browserDetails, TestCase currentTestCase)
        {
            testReportFolder = reportPath;
            screenShotFolder = screenShotPath;
            BrowserDetails = browserDetails;
            testCase = currentTestCase;
        }

        public void GenerateHeader()
        {
            reportBuider = new StringBuilder();
            StringBuilder reportHeaderBuilder = new StringBuilder(HelperClass.rptHeader);
            reportHeaderBuilder.Replace("##TCNameValue##", testCase.TestCaseName);
            reportHeaderBuilder.Replace("##BrowserValue##", BrowserDetails);
            reportBuider.Append(reportHeaderBuilder);
            if (TotalIterationCount > 1)
            {
                reportBuider.Append(HelperClass.rptIterationRow);
                string replaceValue = "##" + "IterationNo" + "##";
                reportBuider.Replace(replaceValue, Convert.ToString(CurrentIteration + 1));
                CurrentIteration++;
            }
            reportBuider.Append(HelperClass.rptStepHeaderRow);
        }

        public void AddReportStep(string description, string actualResult, StepResult stepResult, string screenShotPath)
        {
            string reportStepRow = string.Empty;
            string replaceValue = string.Empty;
            StringBuilder reportStepBuilder;
            switch (stepResult)
            {
                case StepResult.PASS:
                    noOfStepsPassed++;
                    overAllStepsPassed++;
                    reportStepRow = HelperClass.rptPASSRow;
                    break;
                case StepResult.FAIL:
                    noOfStepsFailed++;
                    overAllStepsFailed++;
                    reportStepRow = HelperClass.rptFAILRow;
                    break;
                case StepResult.WARNING:
                    reportStepRow = HelperClass.rptWARNINGRow;
                    overAllWarningSteps++;
                    break;
            }
            stepNo++;
            reportStepBuilder = new StringBuilder(reportStepRow);
            replaceValue = "##" + "StepNoValue" + "##";
            reportStepBuilder.Replace(replaceValue, Convert.ToString(stepNo));
            replaceValue = "##" + "StepNoDesc" + "##";
            reportStepBuilder.Replace(replaceValue, description);
            replaceValue = "##" + "StepActualResult" + "##";
            reportStepBuilder.Replace(replaceValue, actualResult);
            if (stepResult == StepResult.FAIL)
            {
                replaceValue = "##" + "ScreenShotPath" + "##";
                reportStepBuilder.Replace(replaceValue, screenShotPath);
            }
            reportBuider.Append(reportStepBuilder);
        }

        public void AddNewIteration()
        {
            if (CurrentIteration != 1)
            {
                stepNo = 0;
                noOfStepsPassed = 0;
                noOfStepsFailed = 0;
                reportBuider.Append(HelperClass.rptIterationRow);
                string replaceValue = "##" + "IterationNo" + "##";
                reportBuider.Replace(replaceValue, Convert.ToString(CurrentIteration));
                reportBuider.Append(HelperClass.rptStepHeaderRow);
            }
        }

        public void GenerateFooter(string executionTime)
        {
            testCase.NoOfStepsPassed = overAllStepsPassed;
            testCase.NoOfStepsFailed = overAllStepsFailed;
            testCase.NoOfWarningSteps = overAllWarningSteps;
            string replaceValue = string.Empty;
            StringBuilder reportFooterBuilder = new StringBuilder(HelperClass.rptFooter);
            replaceValue = "##" + "StepsPassedValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Convert.ToString(overAllStepsPassed));
            replaceValue = "##" + "StepsFailedValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Convert.ToString(overAllStepsFailed));
            replaceValue = "##" + "ExecutionDateValue" + "##";
            reportFooterBuilder.Replace(replaceValue, DateTime.Now.ToString());
            replaceValue = "##" + "ExecutionMachineValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Environment.MachineName);
            replaceValue = "##" + "TimeTakenValue" + "##";
            reportFooterBuilder.Replace(replaceValue, executionTime);

            reportBuider.Append(reportFooterBuilder);

          //  SaveReport();

            if (noOfStepsFailed > 0)
                testCase.OverAllResult = OverAllResult.FAIL;
            else
                testCase.OverAllResult = OverAllResult.PASS;
        }

        private void SaveReport()
        {
            string htmlReportPath;
            string formattedDate = string.Format("_{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            if (HelperClass.runAllBrowsers || HelperClass.runMultipleBrowsers)
            {
                htmlReportPath = testReportFolder + "\\" + testCase.TestCaseName + "_"
               + Enum.GetName(typeof(Browser), testCase.Browser) + formattedDate + ".html";
            }
            else
            {
                htmlReportPath = testReportFolder + "\\" + testCase.TestCaseName + formattedDate + ".html";
            }
            string htmlReportLinkPath = htmlReportPath.Replace(HelperClass.reportRunPath, ".");
            testCase.HTMLReportPath = htmlReportLinkPath;
            testCase.QCHTMLReportPath = htmlReportPath;
            if (!HelperClass.runGenerateExcelReport)
                testCase.ExcelReportPath = htmlReportLinkPath;
            using (StreamWriter writer = new StreamWriter(htmlReportPath, true))
            {
                writer.WriteLine(reportBuider.ToString());
            }

            if (HelperClass.runAllBrowsers || HelperClass.runMultipleBrowsers)
            {
                string multiBrowserReportHTML = reportBuider.ToString();
                int startIndex = multiBrowserReportHTML.IndexOf(@"<table id=""tblSubHeader""");
                multiBrowserReportHTML = multiBrowserReportHTML.Substring(startIndex);
                int endIndex = multiBrowserReportHTML.IndexOf(@"<table  id=""tblRptFooter""");
                multiBrowserReportHTML = multiBrowserReportHTML.Substring(0, endIndex);
                testCase.TestCaseReportHTML = multiBrowserReportHTML.Replace("</table>", string.Empty); ;
            }
        }

        public void AddErrorStep()
        {
            string reportStepRow = string.Empty;
            string replaceValue = string.Empty;
            StringBuilder reportStepBuilder;
            noOfStepsFailed++;
            overAllStepsFailed++;
            stepNo++;
            reportStepRow = HelperClass.rptErrorRow;
            reportStepBuilder = new StringBuilder(reportStepRow);
            replaceValue = "##" + "StepNoValue" + "##";
            reportStepBuilder.Replace(replaceValue, Convert.ToString(stepNo));
            replaceValue = "##" + "StepNoDesc" + "##";
            reportStepBuilder.Replace(replaceValue, "Error");
            replaceValue = "##" + "StepActualResult" + "##";
            reportStepBuilder.Replace(replaceValue, "Error occured during this step.Please look into error log for more details." +
                "<br />Error Log Path : " + HelperClass.tcErrorLogPath);
            reportBuider.Append(reportStepBuilder);
        }

        public void GenerateMultiBrowserDetailedReport(List<TestCase> lstMultiBrowserTc, TimeSpan timeTaken)
        {
            reportBuider = new StringBuilder();
            StringBuilder reportHeaderBuilder = new StringBuilder(HelperClass.rptHeader);
            reportHeaderBuilder.Replace("##TCNameValue##", lstMultiBrowserTc[1].TestCaseName);
            reportHeaderBuilder.Replace("##BrowserValue##", "Multiple Browsers");
            reportBuider.Append(reportHeaderBuilder);
            string rptBrowserRow = HelperClass.rptBrowserRow;

            if (lstMultiBrowserTc[1].RunIterations)
                rptBrowserRow = HelperClass.rptBrowserIterationRow;

            foreach (TestCase testCase in lstMultiBrowserTc)
            {
                reportBuider.Append(rptBrowserRow);
                reportBuider.Replace("##Browser##", testCase.browserDetails);
                reportBuider.Append(testCase.TestCaseReportHTML);
            }
            reportBuider.Append("</table>");
            GenerateMultiBrowserFooter(lstMultiBrowserTc[1], timeTaken);
            SaveMultiBrowserReport(lstMultiBrowserTc[1].TestCaseName);
        }

        public void GenerateMultiBrowserFooter(TestCase testCase, TimeSpan timeTaken)
        {
            List<TestCase> lstCurrentTC = ExecutionSession.lstAllBrowsersTC.Where(
                   currentTestCase => currentTestCase.TestCaseName == testCase.TestCaseName).ToList();
            System.Nullable<long> stepsPassed = (from currentTestCase in lstCurrentTC
                                                 select (long)currentTestCase.NoOfStepsPassed).Sum();
            System.Nullable<long> stepsFailed = (from currentTestCase in lstCurrentTC
                                                 select (long)currentTestCase.NoOfStepsFailed).Sum();
            System.Nullable<long> warningSteps = (from currentTestCase in lstCurrentTC
                                                  select (long)currentTestCase.NoOfWarningSteps).Sum();

            string executionTime = timeTaken.Minutes + " min(s) and "
                 + timeTaken.Seconds + " secs";
            string replaceValue = string.Empty;
            StringBuilder reportFooterBuilder = new StringBuilder(HelperClass.rptFooter);
            replaceValue = "##" + "StepsPassedValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Convert.ToString(stepsPassed));
            replaceValue = "##" + "StepsFailedValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Convert.ToString(stepsFailed));
            replaceValue = "##" + "ExecutionDateValue" + "##";
            reportFooterBuilder.Replace(replaceValue, DateTime.Now.ToString());
            replaceValue = "##" + "ExecutionMachineValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Environment.MachineName);
            replaceValue = "##" + "TimeTakenValue" + "##";
            reportFooterBuilder.Replace(replaceValue, executionTime);
            reportBuider.Append(reportFooterBuilder);

            TestCase currentTC = ExecutionSession.lstTestCase.First(tc => tc.TestCaseName == testCase.TestCaseName);
            currentTC.NoOfStepsPassed = Convert.ToInt32(stepsPassed);
            currentTC.NoOfStepsFailed = Convert.ToInt32(stepsFailed);
            currentTC.NoOfWarningSteps = Convert.ToInt32(warningSteps);
            currentTC.TimeTaken = executionTime;

            if (stepsFailed >= 1)
                currentTC.OverAllResult = OverAllResult.FAIL;
            else
                currentTC.OverAllResult = OverAllResult.PASS;

        }

        private void SaveMultiBrowserReport(string testCaseName)
        {
            string htmlReportPath;
            string formattedDate = string.Format("_{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            string testReportFolder = HelperClass.reportRunPath + "\\" + testCaseName;
            htmlReportPath = testReportFolder + "\\" + testCaseName + "_MultiBrowser_Detailed_Report" + formattedDate + ".html";
            string htmlReportLinkPath = htmlReportPath.Replace(HelperClass.reportRunPath, ".");
            TestCase currentTC = ExecutionSession.lstTestCase.First(tc => tc.TestCaseName == testCaseName);
            currentTC.HTMLReportPath = htmlReportLinkPath;
            currentTC.QCHTMLReportPath = htmlReportPath;
            if (!HelperClass.runGenerateExcelReport)
                currentTC.ExcelReportPath = htmlReportLinkPath;
            using (StreamWriter writer = new StreamWriter(htmlReportPath, true))
            {
                writer.WriteLine(reportBuider.ToString());
            }
        }
    }
}
