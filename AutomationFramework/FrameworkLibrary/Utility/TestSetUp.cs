using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace TB2
{
    public class TestSetUp
    {
        public static void InitializeSetUp()
        {
            HelperClass.InitializeRunManager();
            GetExecutionClientList(ExecutionSession.dictCommonData["Environment"]);
            GetExecutionTestCases();
            if (HelperClass.runAllBrowsers)
                SetAllBrowsersList();
            else if (HelperClass.runMultipleBrowsers)
                SetMultipleBrowsersList();
            GetTestCaseCategories();
            GetTestCasePriority();
        }


        public static void GetExecutionClientList(String Environment)
        {
            ExecutionSession.lstClient = new List<clientList>();
            clientList ClientList;
            DataTable dtReferenceData;
            ExcelHelper excelHelper = new ExcelHelper();
            string excelCommonDataFile = HelperClass.excelDataPath + "\\CommonData.xls";
            //string excelCommonDataFile = "C:\\DONOT_Delete_MindTree_Automation_Scripts\\Selenium Automation Scripts\\PROD\\TB Client\\CommonData.xls";
            //dtReferenceData = excelHelper.ReadTable(excelCommonDataFile, Environment + "$", @"[Execution_Indicator]=""Yes""");
            string [] path_split = HelperClass.basePath.Split('\\');
            string [] temp = path_split[path_split.Count() - 2].Split('_');
            string indicator = "1"; //temp[temp.Count()-1]; Boomi
            dtReferenceData = excelHelper.ReadTable(excelCommonDataFile, Environment + "$", "[Instance_Indicator]='"+indicator+"' AND [Execution_Indicator]='Yes'");
            RunManager.ClientCount = dtReferenceData.Rows.Count;
             foreach (DataRow dRow in dtReferenceData.Rows)
            {
                ClientList = new clientList();
               // ClientList.env = Convert.ToString(dRow["Key"]);
                ClientList.clientUrl = Convert.ToString(dRow["Value"]);
                ClientList.EnvironmentName = Environment;

                ClientList.Category = Convert.ToString(dRow["Category"]);
                ClientList.Location = Convert.ToString(dRow["Location"]);
                ClientList.Group = Convert.ToString(dRow["Group"]);
                ClientList.AdvancedSearch = Convert.ToString(dRow["AdvancedSearch"]);
                ClientList.BasicSearch = Convert.ToString(dRow["BasicSearch"]);
                ClientList.FilterModule = Convert.ToString(dRow["FilterModule"]);
                ClientList.RecentJobs = Convert.ToString(dRow["RecentJobs"]);
                ClientList.SocialMedia = Convert.ToString(dRow["SocialMedia"]);
                ClientList.JobAlert = Convert.ToString(dRow["JobAlert"]);
                ClientList.SiteMap = Convert.ToString(dRow["SiteMap"]);
                ClientList.RSSFeed = Convert.ToString(dRow["RSSFeed"]);
                ClientList.MeetUs = Convert.ToString(dRow["MeetUs"]);
                ClientList.JobMatching = Convert.ToString(dRow["JobMatching"]);
                ExecutionSession.lstClient.Add(ClientList);

            }





        }
        public static void GetExecutionTestCases()
        {
            ExecutionSession.lstTestCase = new List<TestCase>();
            TestCase testCase;
            string browser;
            string priority;
            ExcelHelper excelHelper = new ExcelHelper();
            string execSheetName = HelperClass.runModuleToExecute + "$";

            if (!HelperClass.runAllTcs && HelperClass.runFailedTcs)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to execute failed Test cases?", "Execution Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.No)
                    Environment.Exit(0);
            }
            string excelFilePath = string.Empty;
            if (HelperClass.runAllTcs || !HelperClass.runFailedTcs)
                excelFilePath = HelperClass.runManagerPath;
            else if (HelperClass.runFailedTcs)
                excelFilePath = HelperClass.runFailedTCPath;

            DataTable dt = excelHelper.ReadTable(excelFilePath, execSheetName, @"[Execute]=""Yes""");

            foreach (DataRow dRow in dt.Rows)
            {
                testCase = new TestCase();
                testCase.TestCaseName = Convert.ToString(dRow["Test Case Name"]);
                testCase.Category = Convert.ToString(dRow["Category"]);
                browser = Convert.ToString(dRow["Browser"]);
                switch (browser)
                {
                    case "FireFox":
                        testCase.Browser = Browser.FireFox;
                        break;
                    case "Chrome":
                        testCase.Browser = Browser.Chrome;
                        break;
                    case "IE":
                        testCase.Browser = Browser.IE;
                        break;
                    case "Safari":
                        testCase.Browser = Browser.Safari;
                        break;
                }
                priority = Convert.ToString(dRow["Priority"]);
                switch (priority)
                {
                    case "P1":
                        testCase.Priority = Priority.P1;
                        break;
                    case "P2":
                        testCase.Priority = Priority.P2;
                        break;
                    case "P3":
                        testCase.Priority = Priority.P3;
                        break;
                }
                if (Convert.ToString(dRow["RunIterations"]).Trim().ToLower() == "yes")
                    testCase.RunIterations = true;
                else
                    testCase.RunIterations = false;
                ExecutionSession.lstTestCase.Add(testCase);
            }
        }

        public static void SetAllBrowsersList()
        {
            List<Browser> lstBrowsers = new List<Browser>();
            lstBrowsers.Add(Browser.FireFox);
            lstBrowsers.Add(Browser.Chrome);
            lstBrowsers.Add(Browser.IE);
            ExecutionSession.lstAllBrowsersTC = new List<TestCase>();
            TestCase allBrowserTC;
            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {
                foreach (Browser browser in lstBrowsers)
                {
                    allBrowserTC = new TestCase();
                    allBrowserTC.TestCaseName = testCase.TestCaseName;
                    allBrowserTC.Category = testCase.Category;
                    allBrowserTC.Priority = testCase.Priority;
                    allBrowserTC.RunIterations = testCase.RunIterations;
                    allBrowserTC.Browser = browser;
                    ExecutionSession.lstAllBrowsersTC.Add(allBrowserTC);
                }
            }
        }

        public static void SetMultipleBrowsersList()
        {
            ExecutionSession.lstAllBrowsersTC = new List<TestCase>();
            TestCase allBrowserTC;
            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {
                foreach (Browser browser in HelperClass.lstRunBrowsers)
                {
                    allBrowserTC = new TestCase();
                    allBrowserTC.TestCaseName = testCase.TestCaseName;
                    allBrowserTC.Category = testCase.Category;
                    allBrowserTC.Priority = testCase.Priority;
                    allBrowserTC.RunIterations = testCase.RunIterations;
                    allBrowserTC.Browser = browser;
                    ExecutionSession.lstAllBrowsersTC.Add(allBrowserTC);
                }
            }
        } 

        public static void GetTestCaseCategories()
        {
            ExcelHelper helper = new ExcelHelper();
            DataTable dt = helper.GetTestCaseCategories();

            ExecutionSession.lstTestCategories = new List<string>();

            foreach (DataRow dRow in dt.Rows)
            {
                ExecutionSession.lstTestCategories.Add(Convert.ToString(dRow[0]).Trim());
            }

        }

        public static void GetTestCasePriority()
        {
            ExcelHelper helper = new ExcelHelper();
            DataTable dt = helper.GetTestCasePriority();

            ExecutionSession.lstTestPriority = new List<Priority>();

            foreach (DataRow dRow in dt.Rows)
            {
                switch (Convert.ToString(dRow[0]).Trim())
                {
                    case "P1":
                        ExecutionSession.lstTestPriority.Add(Priority.P1);
                        break;
                    case "P2":
                        ExecutionSession.lstTestPriority.Add(Priority.P2);
                        break;
                    case "P3":
                        ExecutionSession.lstTestPriority.Add(Priority.P3);
                        break;
                }
            }

        }

        public static IRunTestCases GetRunTestCase()
        {
            String executionType = "TestCaseDriven";
            IRunTestCases runTestCases;
            switch (executionType)
            {
                case "ComponentDriven":
                    runTestCases = new ExcelRunTestCases();
                    break;
                case "TestCaseDriven":
                    //runTestCases = new TxtRunTestCases();
                    runTestCases = new RunTestCases();
                    break;
                default:
                    //  runTestCases = new ExcelRunTestCases();
                    runTestCases = new ExcelRunTestCases();
                    break;
            }
            return runTestCases;
        }

        public static DataHelper GetDataHelper(TestCase currentTestCase)
        {
            DataHelper dataHelper;
            switch (HelperClass.runDataSource.ToLower())
            {
                case "excel":
                    dataHelper = new ExcelDataHelper(currentTestCase);
                    break;
                case "xml":
                    dataHelper = new XMLDataHelper(currentTestCase);
                    break;
                default:
                    dataHelper = new ExcelDataHelper();
                    break;
            }
            return dataHelper;
        }

        public static void AddFailedTCToExcel()
        {
            ExcelHelper.AddFailedTCToExcel();
        }

    }
}
