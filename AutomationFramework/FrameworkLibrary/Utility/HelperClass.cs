using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace TB2
{
    public static class HelperClass
    {
        public static string rptHTML;
        public static string rptHeader;
        public static string rptFooter;
        public static string rptPASSRow;
        public static string rptFAILRow;
        public static string rptWARNINGRow;
        public static string rptErrorRow;
        public static string rptIterationRow;
        public static string rptStepHeaderRow;
        public static string rptBrowserRow;
        public static string rptBrowserIterationRow;

        public static string excelRptHeader;
        public static string excelTCName;
        public static string excelEnvironment;
        public static string excelBuildVersion;
        public static string excelBrowser;
        public static string excelUrl;
        public static string excelSteps;
        public static string excelDescription;
        public static string excelActualResult;
        public static string excelStepStatus;
        public static string excelStepsPassed;
        public static string excelStepsFailed;
        public static string excelExecutionDate;
        public static string excelHostName;
        public static string excelExecutionMachine;
        public static string excelTimeTaken;
        public static string excelHeaderBackgound;
        public static string excelHeaderFontColor;
        public static string excelSubHeaderBackgound;
        public static string excelSubHeaderFontColor;
        public static string excelSummHeaderBackgound;
        public static string excelSummHeaderFontColor;
        public static string excelSummSubHeaderBackgound;
        public static string excelSummSubHeaderFontColor;

        public static string rptChartHTML;

        public static string basePath;
        public static string dataSheetPath;
        public static string reportPath;
        public static string reportTemplateFolderPath;
        public static string reportTemplatePath;
        public static string summaryReportTemplatePath;
        public static string chartSummaryTemplatePath;
        public static string reportRunPath;
        public static string runManagerPath;
        public static string runFailedTCPath;
        public static string runConfigPath;
        public static string reportConfigPath;
        public static string driversPath;
        public static string xmlDataPath;
        public static string excelDataPath;
        public static string tcErrorLogPath;
        public static string errorLogPath;
        public static string categoryClassXmlPath;

        public static string runEnvironment;
        public static string runBuildVersion;
        public static string runEnvironmentName;
        public static bool runAllTcs;
        public static bool runFailedTcs;
        public static string runModuleToExecute;
        public static string runHTMLTemplate;
        public static bool runGenerateExcelReport;
        public static bool runMultipleThreads;
        public static int runNoofThreads;
        public static bool runMultipleBrowsers;
        public static bool runAllBrowsers;
        public static string runDataSource;
        public static string runExcelTemplate;
        public static bool sendEmail;
        public static string emailRegardsFrom;
        public static string emailTo;
        public static string emailFrom;
        public static string emailSubject;
        public static List<Browser> lstRunBrowsers;
        public static Browser DefaultBrowser { get; set; }
        public static bool QCUpdateResults { get; set; }
        public static string QCServerName { get; set; }
        public static string QCDomainName { get; set; }
        public static string QCProjectName { get; set; }
        public static string QCUserName { get; set; }
        public static string QCPassword { get; set; }

        public static void InitializeRunManager()
        {
            SetFolderPaths();
            ReadRunConfig();
            SetQCDetails();
            AddCategoryClassList();
            DataHelper.SetCommonData();
            InitializeReportFields();
        }

        public static void InitializeReportFields()
        {
            SetReportHTML();
            SetReportHeader();
            SetReportFooter();
           SetStepHeader();
            //SetIterationRow();
            //SetReportPASSRow();
            //SetReportFAILRow();
            //SetReportWARNINGRow();
            //SetReportErrorRow();
            //if (runMultipleBrowsers || runAllBrowsers)
            //{
            //    SetReportBrowserRow();
            //    SetReportBrowserIterationRow();
            //}
            SetExcelConfigValues();
            CreateReportFolder();
            //SetChartReportHTML();
        }

        public static void SetReportHTML()
        {
            using (StreamReader reader = new StreamReader(reportTemplatePath))
            {
                string line = string.Empty;
                //string replaceLabel = string.Empty;
                line = reader.ReadToEnd();
                rptHTML = line;
            }
        }

        public static void SetChartReportHTML()
        {
            using (StreamReader reader = new StreamReader(chartSummaryTemplatePath))
            {
                string line = string.Empty;
                string replaceLabel = string.Empty;
                line = reader.ReadToEnd();
                rptChartHTML = line;
            }
        }

        public static void SetReportHeader()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int trIndex = line.IndexOf(@"<tr id=""trBrowserRow""");
            line = line.Substring(0, trIndex);
            StringBuilder rptHeaderBuider = new StringBuilder(line);
            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList headerFields = xmlReportConfig.SelectNodes("Report/TestCaseReport/ReportHeader/var");

                foreach (XmlNode headerField in headerFields)
                {
                    replaceLabel = "##" + headerField.Attributes["name"].Value + "##";
                    rptHeaderBuider.Replace(replaceLabel, headerField.Attributes["value"].Value);

                    switch (headerField.Attributes["name"].Value)
                    {
                        case "RptHeader":
                            excelRptHeader = headerField.Attributes["value"].Value;
                            break;
                        case "TCName":
                            excelTCName = headerField.Attributes["value"].Value;
                            break;
                        case "Environment":
                            excelEnvironment = headerField.Attributes["value"].Value;
                            break;
                        case "HostName":
                            excelHostName = headerField.Attributes["value"].Value;
                            break;
                        case "BuildVersion":
                            excelBuildVersion = headerField.Attributes["value"].Value;
                            break;
                        case "Browser":
                            excelBrowser = headerField.Attributes["value"].Value;
                            break;
                        case "Url":
                            excelUrl = headerField.Attributes["value"].Value;
                            break;
                    }

                }
                string replaceValue = string.Empty;
                replaceValue = "##" + "EnvironmentValue" + "##";
                rptHeaderBuider.Replace(replaceValue, runEnvironmentName);
                replaceValue = "##" + "BuildVersionValue" + "##";
                rptHeaderBuider.Replace(replaceValue, runBuildVersion);
                replaceValue = "##" + "UrlValue" + "##";
                rptHeaderBuider.Replace(replaceValue, ExecutionSession.dictCommonData["EnvironmentUrl"]);

                XmlNodeList stepResultFields = xmlReportConfig.SelectNodes("Report/TestCaseReport/ReportStep/var");

                foreach (XmlNode stepResultField in stepResultFields)
                {
                    replaceLabel = "##" + stepResultField.Attributes["name"].Value + "##";
                    rptHeaderBuider.Replace(replaceLabel, stepResultField.Attributes["value"].Value);

                    switch (stepResultField.Attributes["name"].Value)
                    {
                        case "Steps":
                            excelSteps = stepResultField.Attributes["value"].Value;
                            break;
                        case "Description":
                            excelDescription = stepResultField.Attributes["value"].Value;
                            break;
                        case "ActualResult":
                            excelActualResult = stepResultField.Attributes["value"].Value;
                            break;
                        case "StepStatus":
                            excelStepStatus = stepResultField.Attributes["value"].Value;
                            break;
                    }

                }

            }
            rptHeader = rptHeaderBuider.ToString();
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
            xmlReportConfig.Load(reportConfigPath);

            if (!(xmlReportConfig == null))
            {
                XmlNodeList footerFields = xmlReportConfig.SelectNodes("Report/TestCaseReport/ReportFooter/var");

                foreach (XmlNode footerField in footerFields)
                {
                    replaceLabel = "##" + footerField.Attributes["name"].Value + "##";
                    rptFooterBuider.Replace(replaceLabel, footerField.Attributes["value"].Value);

                    switch (footerField.Attributes["name"].Value)
                    {
                        case "StepsPassed":
                            excelStepsPassed = footerField.Attributes["value"].Value;
                            break;
                        case "StepsFailed":
                            excelStepsFailed = footerField.Attributes["value"].Value;
                            break;
                        case "ExecutionDate":
                            excelExecutionDate = footerField.Attributes["value"].Value;
                            break;
                        case "ExecutionMachine":
                            excelExecutionMachine = footerField.Attributes["value"].Value;
                            break;
                        case "TimeTaken":
                            excelTimeTaken = footerField.Attributes["value"].Value;
                            break;
                    }

                }
            }
            rptFooter = rptFooterBuider.ToString();
        }

        public static void SetStepHeader()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trSubHeader""");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptStepHeaderRow = line;
            StringBuilder rptStepHeaderBuider = new StringBuilder(rptStepHeaderRow);
            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList headerFields = xmlReportConfig.SelectNodes("Report/TestCaseReport/ReportStep/var");

                foreach (XmlNode headerField in headerFields)
                {
                    replaceLabel = "##" + headerField.Attributes["name"].Value + "##";
                    rptStepHeaderBuider.Replace(replaceLabel, headerField.Attributes["value"].Value);
                }
            }
            rptStepHeaderRow = rptStepHeaderBuider.ToString();
        }

        public static void SetIterationRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trIterationRow""");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptIterationRow = line;
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

        public static void SetReportWARNINGRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trWARNINGRow"">");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptWARNINGRow = line;
        }

        public static void SetReportErrorRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trErrorRow"">");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptErrorRow = line;
        }

        public static void SetReportBrowserRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trBrowserRow""");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptBrowserRow = line;
        }

        public static void SetReportBrowserIterationRow()
        {
            string line = rptHTML;
            string replaceLabel = string.Empty;
            int startIndex = line.IndexOf(@"<tr id=""trBrowserRowIteration""");
            line = line.Substring(startIndex);
            int endIndex = line.IndexOf("</tr>");
            line = line.Substring(0, endIndex + 5);
            rptBrowserIterationRow = line;
        }

        public static void SetFolderPaths()
        {
            string currentDirectory = Environment.CurrentDirectory;
            if (currentDirectory.Contains("Executables"))
            {
                basePath = currentDirectory.Substring(0, currentDirectory.IndexOf("Executables"));
            }
            else
            {
                basePath = currentDirectory.Substring(0, currentDirectory.IndexOf("AutomationFramework\\TB2\\bin"));
            }
            dataSheetPath = basePath + "TestData";
            driversPath = basePath + "Drivers";
            reportPath = basePath + "Reports";
            errorLogPath = basePath + "ErrorLogs";
            reportTemplateFolderPath = basePath + "ReportTemplate";
            runManagerPath = basePath + "RunManager.xls";
            runFailedTCPath = basePath + "LastRun_FailedTC.xls";
            runConfigPath = basePath + "RunConfig.xml";
            xmlDataPath = dataSheetPath + "\\XML";
            excelDataPath = dataSheetPath + "\\Excel";
            reportConfigPath = reportTemplateFolderPath + "\\ReportConfig.xml";
            categoryClassXmlPath = basePath + "ExecutionClassConfig.xml";
        }

        private static void CreateReportFolder()
        {
            String env = ExecutionSession.dictCommonData["Environment"];
            string testRunFolder = string.Format("\\"+env+"\\TestRun_{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            testRunFolder = HelperClass.reportPath + testRunFolder;
            Console.WriteLine(testRunFolder);
            if (!Directory.Exists(testRunFolder))
                Directory.CreateDirectory(testRunFolder);
            reportRunPath = testRunFolder;
            tcErrorLogPath = reportRunPath + "\\Log.txt";
        }

        private static void ReadRunConfig()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node;
            doc.Load(runConfigPath);
            node = doc.SelectSingleNode("GlobalSettings/Environment");
            runEnvironment = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/BuildVersion");
            runBuildVersion = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/EnvironmentName");
            runEnvironmentName = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/DefaultBrowser");
            string defaultBrowser = node.InnerText.Trim();

            if (defaultBrowser.Trim().StartsWith("F"))
                DefaultBrowser = Browser.FireFox;
            if (defaultBrowser.Trim().StartsWith("C"))
                DefaultBrowser = Browser.Chrome;
            if (defaultBrowser.Trim().StartsWith("I"))
                DefaultBrowser = Browser.IE;
            if (defaultBrowser.Trim().StartsWith("S"))
                DefaultBrowser = Browser.Safari;

            node = doc.SelectSingleNode("GlobalSettings/ModuleToExecute");
            runModuleToExecute = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/TestCasesToExecute/RunAllTCs");
            runAllTcs = Convert.ToBoolean(node.InnerText.Trim());
            node = doc.SelectSingleNode("GlobalSettings/TestCasesToExecute/RunFailedTc");
            runFailedTcs = Convert.ToBoolean(node.InnerText.Trim());
            node = doc.SelectSingleNode("GlobalSettings/Report/HTMLTemplate");
            runHTMLTemplate = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/Report/GenerateExcelReport");
            runGenerateExcelReport = Convert.ToBoolean(node.InnerText);
            node = doc.SelectSingleNode("GlobalSettings/MultiThreading/Execute");
            runMultipleThreads = Convert.ToBoolean(node.InnerText);
            node = doc.SelectSingleNode("GlobalSettings/MultiThreading/NoofThreads");
            if (!string.IsNullOrEmpty(node.InnerText))
            {
                runNoofThreads = Convert.ToInt32(node.InnerText);
            }
            node = doc.SelectSingleNode("GlobalSettings/MultiBrowser/Execute");
            runMultipleBrowsers = Convert.ToBoolean(node.InnerText);
            node = doc.SelectSingleNode("GlobalSettings/MultiBrowser/AllBrowsers");
            if (!string.IsNullOrEmpty(node.InnerText))
            {
                runAllBrowsers = Convert.ToBoolean(node.InnerText);
            }
            node = doc.SelectSingleNode("GlobalSettings/MultiBrowser/BrowsersToExecute");
            if (!string.IsNullOrEmpty(node.InnerText))
            {
                string[] browsers = node.InnerText.Split(',');
                lstRunBrowsers = new List<Browser>();
                foreach (string browser in browsers)
                {
                    if (browser.Trim().StartsWith("F"))
                        lstRunBrowsers.Add(Browser.FireFox);
                    if (browser.Trim().StartsWith("C"))
                        lstRunBrowsers.Add(Browser.Chrome);
                    if (browser.Trim().StartsWith("I"))
                        lstRunBrowsers.Add(Browser.IE);
                    if (browser.Trim().StartsWith("S"))
                        lstRunBrowsers.Add(Browser.Safari);
                }

            }
            node = doc.SelectSingleNode("GlobalSettings/DataSource");
            runDataSource = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/Report/HTMLTemplate");
            string reportTemplate = node.InnerText.Trim();
            reportTemplatePath = reportTemplateFolderPath + "\\" + reportTemplate + ".htm";
            node = doc.SelectSingleNode("GlobalSettings/Report/SummaryReportTemplate");
            reportTemplate = node.InnerText.Trim();
            summaryReportTemplatePath = reportTemplateFolderPath + "\\" + reportTemplate + ".htm";           
            node = doc.SelectSingleNode("GlobalSettings/Report/ExcelTemplate");
            runExcelTemplate = node.InnerText.Trim();            
            
            node = doc.SelectSingleNode("GlobalSettings/Report/ChartSummaryTemplate");
            reportTemplate = node.InnerText.Trim();
            chartSummaryTemplatePath = reportTemplateFolderPath + "\\" + reportTemplate + ".htm"; 

            node = doc.SelectSingleNode("GlobalSettings/Email/SendEmail");
            if (!string.IsNullOrEmpty(node.InnerText))
            {
                sendEmail = Convert.ToBoolean(node.InnerText);
            }

            node = doc.SelectSingleNode("GlobalSettings/Email/emailTo");
            emailTo = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/Email/emailFrom");
            emailFrom = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/Email/emailSubject");
            emailSubject = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/Email/RegardsFrom");
            emailRegardsFrom = node.InnerText.Trim();
            node = doc.SelectSingleNode("GlobalSettings/DeviceTesting/Execute");
        }

        public static void SetQCDetails()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node;
            doc.Load(runConfigPath);
            node = doc.SelectSingleNode("GlobalSettings/QCDetails/UpdateResultsToQC");
            QCUpdateResults = Convert.ToBoolean(node.InnerText.Trim());
            if (QCUpdateResults || sendEmail)
            {
                node = doc.SelectSingleNode("GlobalSettings/QCDetails/QCServerName");
                QCServerName = Convert.ToString(node.InnerText.Trim());
                node = doc.SelectSingleNode("GlobalSettings/QCDetails/QCDomainName");
                QCDomainName = Convert.ToString(node.InnerText.Trim());
                node = doc.SelectSingleNode("GlobalSettings/QCDetails/QCProjectName");
                QCProjectName = Convert.ToString(node.InnerText.Trim());
                node = doc.SelectSingleNode("GlobalSettings/QCDetails/QCUserName");
                QCUserName = Convert.ToString(node.InnerText.Trim());
                node = doc.SelectSingleNode("GlobalSettings/QCDetails/QCPassword");
                QCPassword = Convert.ToString(node.InnerText.Trim());
            }

        }

        public static int GetIterationCount(TestCase currentTestCase)
        {
            int iterationCount = 0;
            switch (HelperClass.runDataSource.ToLower())
            {
                case "excel":
                    iterationCount = ExcelDataHelper.GetIterationCount(currentTestCase);
                    break;
                case "xml":
                    iterationCount = XMLDataHelper.GetIterationCount(currentTestCase);
                    break;
            }
            return iterationCount;
        }

        public static void AddCategoryClassList()
        {
            XmlDocument xmlCategoryClassConfig = new XmlDocument();
            xmlCategoryClassConfig.Load(categoryClassXmlPath);
            if (!(xmlCategoryClassConfig == null))
            {
                ExecutionSession.lstCategoryClass = new List<CategoryClass>();
                CategoryClass categoryClass;
                XmlNodeList categoryFields = xmlCategoryClassConfig.SelectNodes("Category/var");

                foreach (XmlNode headerField in categoryFields)
                {

                    categoryClass = new CategoryClass();
                    categoryClass.CategoryName = headerField.Attributes["category"].Value;
                    categoryClass.ClassName = headerField.Attributes["classname"].Value;
                    ExecutionSession.lstCategoryClass.Add(categoryClass);
                }
            }
        }

        public static void SetExcelConfigValues()
        {
            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList headerFields = xmlReportConfig.SelectNodes("Report/TestCaseReport/ExcelReport/"+runExcelTemplate+"/var");

                foreach (XmlNode headerField in headerFields)
                {

                    switch (headerField.Attributes["name"].Value)
                    {
                        case "HeaderBackgound":
                            excelHeaderBackgound = headerField.Attributes["value"].Value;
                            break;
                        case "HeaderFontColor":
                            excelHeaderFontColor = headerField.Attributes["value"].Value;
                            break;
                        case "SubHeaderBackgound":
                            excelSubHeaderBackgound = headerField.Attributes["value"].Value;
                            break;
                        case "SubHeaderFontColor":
                            excelSubHeaderFontColor = headerField.Attributes["value"].Value;
                            break;
                    }

                }

                headerFields = xmlReportConfig.SelectNodes("Report/SummaryReport/ExcelReport/" + runExcelTemplate + "/var");

                foreach (XmlNode headerField in headerFields)
                {

                    switch (headerField.Attributes["name"].Value)
                    {
                        case "HeaderBackgound":
                            excelSummHeaderBackgound = headerField.Attributes["value"].Value;
                            break;
                        case "HeaderFontColor":
                            excelSummHeaderFontColor = headerField.Attributes["value"].Value;
                            break;
                        case "SubHeaderBackgound":
                            excelSummSubHeaderBackgound = headerField.Attributes["value"].Value;
                            break;
                        case "SubHeaderFontColor":
                            excelSummSubHeaderFontColor = headerField.Attributes["value"].Value;
                            break;
                    }

                }

            }
        }

    }
}
