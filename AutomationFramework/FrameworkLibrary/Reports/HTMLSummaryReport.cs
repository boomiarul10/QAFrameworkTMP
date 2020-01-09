using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace TB2
{
    public class HTMLSummaryReport
    {
        StringBuilder reportBuider;
        public static string rptHTML;
        public static string rptHeader;
        public static string rptFooter;
        public static string rptPASSRow;
        public static string rptFAILRow;
        public static string rptWARNINGRow;
        public int testCaseNo { get; set; }

        public void GenerateSummaryReport()
        {
            reportBuider = new StringBuilder();
            SetReportHTML();
            SetReportPASSRow();
            SetReportFAILRow();
            CreateReportHeader();
            AddTCResult();
            SetReportFooter();
            GenerateFooter();
        }

        public static void SetReportHTML()
        {
            using (StreamReader reader = new StreamReader(HelperClass.summaryReportTemplatePath))
            {
                string line = string.Empty;
                string replaceLabel = string.Empty;
                line = reader.ReadToEnd();
                rptHTML = line;
            }
        }
        private void CreateReportHeader()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int trIndex = line.IndexOf(@"<tr id=""trPASSRow"">");
            line = line.Substring(0, trIndex);
            StringBuilder rptHeaderBuider = new StringBuilder(line);
            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(HelperClass.reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList headerFields = xmlReportConfig.SelectNodes("Report/SummaryReport/ReportHeader/var");

                foreach (XmlNode headerField in headerFields)
                {
                    replaceLabel = "##" + headerField.Attributes["name"].Value + "##";
                    rptHeaderBuider.Replace(replaceLabel, headerField.Attributes["value"].Value);
                }

                XmlNodeList tcResultFields = xmlReportConfig.SelectNodes("Report/SummaryReport/TestResult/var");

                foreach (XmlNode tcResultField in tcResultFields)
                {
                    replaceLabel = "##" + tcResultField.Attributes["name"].Value + "##";
                    rptHeaderBuider.Replace(replaceLabel, tcResultField.Attributes["value"].Value);
                }

            }
            rptHeader = rptHeaderBuider.ToString();
            reportBuider.Append(rptHeader);
        }

        public static void SetReportFooter()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            string footerRow = "</table><table ";
            string[] tables = line.Split(new string[] { "<table" }, StringSplitOptions.None);
            footerRow += tables.Where(table => table.Contains(@"id=""tblRptFooter""")).First();
            StringBuilder rptFooterBuider = new StringBuilder(footerRow);
            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(HelperClass.reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList footerFields = xmlReportConfig.SelectNodes("Report/SummaryReport/ReportFooter/var");

                foreach (XmlNode footerField in footerFields)
                {
                    replaceLabel = "##" + footerField.Attributes["name"].Value + "##";
                    rptFooterBuider.Replace(replaceLabel, footerField.Attributes["value"].Value);
                }
            }
            rptFooter = rptFooterBuider.ToString();
        }

        public static void SetReportPASSRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trPASSRow"">");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptPASSRow = line;
        }

        public static void SetReportFAILRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trFAILRow"">");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptFAILRow = line;
        }

        public void AddTCResult()
        {
            string reportStepRow = string.Empty;
            string replaceValue = string.Empty;
            StringBuilder reportStepBuilder;
            int testCaseNo = 1;
            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {
                switch (testCase.OverAllResult)
                {
                    case OverAllResult.PASS:
                        reportStepRow = rptPASSRow;
                        break;
                    case OverAllResult.FAIL:
                        reportStepRow = rptFAILRow;
                        break;
                }
                reportStepBuilder = new StringBuilder(reportStepRow);
                replaceValue = "##" + "TCNoValue" + "##";
                reportStepBuilder.Replace(replaceValue, Convert.ToString(testCaseNo));
                replaceValue = "##" + "TCNameValue" + "##";
                reportStepBuilder.Replace(replaceValue, testCase.TestCaseName);
                replaceValue = "##" + "ReportPath" + "##";
                reportStepBuilder.Replace(replaceValue, testCase.HTMLReportPath);
                replaceValue = "##" + "BrowserValue" + "##";
                string browser = Enum.GetName(typeof(Browser), testCase.Browser);
                if (HelperClass.runMultipleBrowsers || HelperClass.runAllBrowsers)
                    browser = "Multiple Browsers";
                reportStepBuilder.Replace(replaceValue, browser);
                replaceValue = "##" + "PriorityValue" + "##";
                reportStepBuilder.Replace(replaceValue, Enum.GetName(typeof(Priority), testCase.Priority));
                reportBuider.Append(reportStepBuilder);
                testCaseNo++;
            }
        }


        public void GenerateFooter()
        {
            string replaceValue = string.Empty;
            int noOfTCPassed;
            int noOfTCFailed;
            TimeSpan overAllTimeTaken = new TimeSpan();
            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {
                overAllTimeTaken = overAllTimeTaken.Add(testCase.ExecutionTime);
            }
            string executionTime = overAllTimeTaken.Minutes + " min(s) and "
                 + overAllTimeTaken.Seconds + " secs";
            noOfTCPassed = ExecutionSession.lstTestCase.Where(testCase => testCase.OverAllResult == OverAllResult.PASS).ToList().Count;
            noOfTCFailed = ExecutionSession.lstTestCase.Where(testCase => testCase.OverAllResult == OverAllResult.FAIL).ToList().Count;

            StringBuilder reportFooterBuilder = new StringBuilder(rptFooter);
            replaceValue = "##" + "TCPassedValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Convert.ToString(noOfTCPassed));
            replaceValue = "##" + "TCFailedValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Convert.ToString(noOfTCFailed));
            replaceValue = "##" + "ExecutionDateValue" + "##";
            reportFooterBuilder.Replace(replaceValue, DateTime.Now.ToString());
            replaceValue = "##" + "ExecutionMachineValue" + "##";
            reportFooterBuilder.Replace(replaceValue, Environment.MachineName);
            replaceValue = "##" + "TimeTakenValue" + "##";
            reportFooterBuilder.Replace(replaceValue, executionTime);

            reportBuider.Append(reportFooterBuilder);

            SaveReport();
        }

        private void SaveReport()
        {
            string testReportPath = HelperClass.reportRunPath + "\\" + "SummaryReport_NoChart" + ".html";
            if (File.Exists(testReportPath))
                File.Delete(testReportPath);
            using (StreamWriter writer = new StreamWriter(testReportPath, true))
            {
                writer.WriteLine(reportBuider.ToString());
            }
        }
    }
}
