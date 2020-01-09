using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using FrameworkLibrary.Reports;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
namespace TB2
{
    public class RunManager
    {
        public static string CategoryName = string.Empty;
        public static string LocationName = string.Empty;
        public static int TestCaseCount;
        public static int Progresscount = 0;
        public static int ClientCount;
        public static int ClientProgresscount = 0;
        public static int totalJobCount = 0;
        public static int filterJobCount=0;
        public static int locJobCount = 0;
        public static int keywordJobCOunt = 0;
        public static string FilterName = string.Empty;
      //  public static string stateName = string.Empty;
      //  public static string cityName = string.Empty;

        public void RunTestCases()
        {
            ExecutionSession.startTime = DateTime.Now;
            bool exceptionOccured = false;
            try
            {
                
                TestSetUp.InitializeSetUp();
                //SendStartMail(ExecutionSession.dictCommonData["Environment"]);
                //Environment.Exit(0);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured during Test Setup. Please look into logs for more details.");
                Logger.Log(ex.ToString());
                Environment.Exit(0);
            }

            try
            {
                ExecutionSession.lstExecutedTestCases = new List<ExecutedTestCase>();
                ExecutionSession.lstErrorsteps = new List<ErrorSteps>();
                foreach (clientList ClientList in ExecutionSession.lstClient)
                {
                    if (ClientProgresscount != 0)
                        ProgressBar(ClientProgresscount, ClientCount);
                    ExecutionSession.dictCommonData.Clear();
                    ExecutionSession.dictCommonData.Add("Environment", ClientList.EnvironmentName);
                    ExecutionSession.dictCommonData.Add("EnvironmentUrl", ClientList.clientUrl);
                    if (!HelperClass.runAllBrowsers && !HelperClass.runMultipleBrowsers
                        && !HelperClass.runMultipleThreads)
                        ExecuteTestCases(ClientList.clientUrl);
                    else if (HelperClass.runAllBrowsers || HelperClass.runMultipleBrowsers)
                        ExecuteMultipleBrowsers();
                    else if (HelperClass.runMultipleThreads)
                        ExecuteMultipleTestCases();
                    	 Progresscount = 0;
                    ClientProgresscount++;
                    //Thread.Sleep(2000);
                    Console.Clear();
                    if (ClientProgresscount == ClientCount)
                        ProgressBar(ClientProgresscount, ClientCount);
                }
            }
            catch (Exception e)
            {
                exceptionOccured = true;
                
            }


            if (!exceptionOccured)
            {
                try
                {
                    SummaryReport report = new SummaryReport();
                    report.GenerateSummaryReport();
                    TestSetUp.AddFailedTCToExcel();
                  //  SendEndMail(ExecutionSession.dictCommonData["Environment"]);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                }
            }

            if (HelperClass.sendEmail)
            {
                QCHelper qcHelper = new QCHelper();
                qcHelper.SendEmail();
            }
            Driver.QuitDriver(Driver.driver);
            //HTMLChartSummaryReport htmlChartSummaryReport = new HTMLChartSummaryReport();
            //htmlChartSummaryReport.generateSummaryReport();
            //System.Diagnostics.Process.Start(HelperClass.reportRunPath + "\\" + "SummaryReport" + ".html");
        }

        private static void TestcaseProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + " TestCases Completed" + "\n");
        }

        private static void ProgressBar(int progress, int total)
        {
            Console.Write("\n");
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + " Clients Completed" + "\n");
        }

        public void ExecuteTestCases(String ClienURL)
        {

            bool exceptionOccured = false;
            RunManager.TestCaseCount = ExecutionSession.lstTestCase.Count;
            IRunTestCases runTestCases = TestSetUp.GetRunTestCase();
            QCHelper qcHelper = null;

            if (HelperClass.QCUpdateResults)
                qcHelper = new QCHelper();

            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {
                try
                {
                    Console.WriteLine(testCase.TestCaseName + " Called");
                    runTestCases.CurrentTestCase = testCase;
                    runTestCases.RunTestCase();
                    if (HelperClass.QCUpdateResults)
                        qcHelper.UploadResults(testCase);
                }
                catch (Exception ex)
                {
                    exceptionOccured = true;
                    Logger.Log(ex.ToString());
                }
                RunManager.Progresscount++;
                TestcaseProgressBar(RunManager.Progresscount, RunManager.TestCaseCount);
            }
            //if (!exceptionOccured)
            //{
            //    try
            //    {
            //        SummaryReport report = new SummaryReport();
            //        report.GenerateSummaryReport();
            //        TestSetUp.AddFailedTCToExcel();
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger.Log(ex.ToString());
            //    }
            //}
            
        }

        public void ExecuteMultipleTestCases()
        {
            IRunTestCases runTestCases = TestSetUp.GetRunTestCase();

            List<ThreadClass> lstThreads = new List<ThreadClass>();
            ThreadClass storeThread;
            int count = ExecutionSession.lstTestCase.Count;
            int runTestCaseCount = 0;
            if (ExecutionSession.lstTestCase.Count < HelperClass.runNoofThreads)
            {
                runTestCaseCount = ExecutionSession.lstTestCase.Count;
            }
            else
            {
                runTestCaseCount = HelperClass.runNoofThreads;
            }
            int threadCnt = 0;

            while (threadCnt < count)
            {
                if (threadCnt == 0)
                {
                    for (int iLp = 1; iLp <= runTestCaseCount; iLp++)
                    {
                        TestCase testCase = ExecutionSession.lstTestCase[threadCnt];
                        runTestCases.CurrentTestCase = testCase;
                        Thread runThread = new Thread(runTestCases.RunTestCase);
                        runThread.Name = "Thread" + iLp;
                        storeThread = new ThreadClass();
                        storeThread.ThreadName = runThread.Name;
                        storeThread.currentThread = runThread;
                        lstThreads.Add(storeThread);
                        runThread.Start();
                        threadCnt++;
                        Console.WriteLine("Threads executed is : " + threadCnt);
                        Thread.Sleep(700);
                    }
                }
                else
                {
                    foreach (ThreadClass currentThread in lstThreads)
                    {
                        if (currentThread.currentThread.IsAlive == false)
                        {
                            if (threadCnt < count)
                            {
                                Console.WriteLine("Thread  " + currentThread.ThreadName + " is alive :" + currentThread.currentThread.IsAlive);
                                runTestCases = TestSetUp.GetRunTestCase();
                                TestCase testCase = ExecutionSession.lstTestCase[threadCnt];
                                runTestCases.CurrentTestCase = testCase;
                                Thread runThread = new Thread(runTestCases.RunTestCase);
                                runThread.Name = currentThread.ThreadName;
                                currentThread.currentThread = runThread;
                                runThread.Start();
                                threadCnt++;
                                Console.WriteLine("Threads executed is : " + threadCnt);
                                Thread.Sleep(700);
                            }
                        }
                    }
                }
            }

            while (lstThreads.Any(thread => thread.currentThread.IsAlive))
            {

            }

            try
            {
                SummaryReport report = new SummaryReport();
                report.GenerateSummaryReport();
                TestSetUp.AddFailedTCToExcel();
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public void ExecuteMultipleBrowsers()
        {
            List<TestCase> lstRunTC;
            Report rptTCMultiBrowser;
            DateTime startTime;
            QCHelper qcHelper = null;
            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {
                lstRunTC = ExecutionSession.lstAllBrowsersTC.Where(browserTC => browserTC.TestCaseName == testCase.TestCaseName).ToList();

                IRunTestCases runTestCases;
                List<ThreadClass> lstThreads = new List<ThreadClass>();
                ThreadClass storeThread;
                int count = lstRunTC.Count;
                int threadCnt = 1;
                startTime = DateTime.Now;
                foreach (TestCase currentTestCase in lstRunTC)
                {
                    runTestCases = TestSetUp.GetRunTestCase();
                    runTestCases.CurrentTestCase = currentTestCase;
                    Thread runThread = new Thread(runTestCases.RunTestCase);
                    runThread.Name = "Thread" + threadCnt;
                    storeThread = new ThreadClass();
                    storeThread.ThreadName = runThread.Name;
                    storeThread.currentThread = runThread;
                    lstThreads.Add(storeThread);
                    runThread.Start();
                    if (currentTestCase.Browser == Browser.FireFox)
                        Thread.Sleep(8000);
                    threadCnt++;
                }

                while (lstThreads.Any(thread => thread.currentThread.IsAlive))
                {

                }
                DateTime endTime = DateTime.Now;
                TimeSpan timeTaken = endTime.Subtract(startTime);
                string executionTime = timeTaken.Minutes + " mins and "
                    + timeTaken.Seconds + " secs";
                rptTCMultiBrowser = new Report();
                rptTCMultiBrowser.GenerateMultiBrowserDetailedReport(lstRunTC, timeTaken);
                try
                {
                    if (HelperClass.QCUpdateResults)
                    {
                        qcHelper = new QCHelper();
                        qcHelper.UploadResults(testCase);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                }
            }

            try
            {
                SummaryReport report = new SummaryReport();
                report.GenerateSummaryReport();
                TestSetUp.AddFailedTCToExcel();
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public void SendEndMail(String Environment)
        {
            try
            {
                Process[] process = Process.GetProcessesByName("AutomationFramework");
                if (process.Length.Equals(1))
                {
                    string batDir = string.Format(@""+HelperClass.basePath+"\\copyfiles\\");
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = batDir;
                    proc.StartInfo.FileName = "CopyFiles.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();
                    proc.WaitForExit();
                    Thread.Sleep(15000);
                    string batDir1 = string.Format(@"" + HelperClass.basePath + "\\copyfiles\\test\\");
                    Process proc1 = new Process();
                    proc1.StartInfo.WorkingDirectory = batDir1;
                    proc1.StartInfo.FileName = "TB2ReportGenerationAutomation.bat";
                    proc1.StartInfo.CreateNoWindow = false;
                    proc1.Start();
                    proc1.WaitForExit();
                    String mindtree_ID = "bshanmug@tmp.com";
                    //String mindtree_ID1 = "rmasadi@tmp.com";  
                    //String mindtree_ID2 = "pravinat@tmp.com";
                    //String mindtree_ID3 = "knagalin@tmp.com";
                    //String mindtree_ID4 = "panantha@tmp.com";
                    //String mindtree_ID5 = "Rselvama@tmp.com";
                    //String mindtree_ID6 = "vjakhar@tmp.com";
                    String from = "selenium@tmp.com";
                    String host = "mta.core.dc";
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(host);
                    SmtpServer.Port = 25;
                    mail.From = new MailAddress(from);
                    mail.To.Add(mindtree_ID);
                    //mail.To.Add(mindtree_ID1);
                    //mail.To.Add(mindtree_ID2);
                    //mail.To.Add(mindtree_ID3);
                    //mail.To.Add(mindtree_ID4);
                    //mail.To.Add(mindtree_ID5);
                    //mail.To.Add(mindtree_ID6);

                    mail.Subject = "TB 2.0|| " + Environment + " Script Execution Completed";

                    mail.Body = @"<style>
                p{
                    font-family:""Trebuchet MS"", Arial, Helvetica, sans-serif;
                    font-size:0.8em;
                    text-align:left;
                }
            </style>" + "<p>Hi All,</p> <p>TB 2.0 Automation Script is executed in " + Environment + " and PFA the Results. </p> <p>Find the detailed report from the path \\\\10.132.0.218\\" + HelperClass.reportRunPath.Replace(':', '$') + "</p>";
                    mail.IsBodyHtml = true;
                    System.Net.Mail.Attachment attachment;
                    //System.Net.Mail.Attachment attachment1;
                    attachment = new System.Net.Mail.Attachment(HelperClass.basePath + "\\copyfiles\\test\\TB2_Automation_Summary Report.xlsx");
                    //attachment1 = new System.Net.Mail.Attachment(HelperClass.reportRunPath + "/SummaryReport.html");
                    mail.Attachments.Add(attachment);
                    //mail.Attachments.Add(attachment1);
                    SmtpServer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendStartMail(String Environment)
        {
            try
            {
                //String mindtree_ID = "rbandi@tmp.com";
                String mindtree_ID = "bshanmug@tmp.com";
                //String mindtree_ID1 = "rmasadi@tmp.com";
                //String mindtree_ID2 = "pravinat@tmp.com";
                //String mindtree_ID4 = "panantha@tmp.com";
                //String mindtree_ID5 = "Rselvama@tmp.com";
                //String mindtree_ID6 = "vjakhar@tmp.com";
                
                String from = "selenium@tmp.com";
                String host = "mta.core.dc";
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(host);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Port = 25;
                double ExecutionTime = 1.5;
                mail.From = new MailAddress(from);
                mail.To.Add(mindtree_ID);
                //mail.To.Add(mindtree_ID1);
                //mail.To.Add(mindtree_ID2);
                //mail.To.Add(mindtree_ID3);
                //mail.To.Add(mindtree_ID4);
                //mail.To.Add(mindtree_ID5);
                //mail.To.Add(mindtree_ID6);
                
                mail.Subject = "TB 2.0 (Instances from 1 to 10) || " + Environment + " Script Execution Started";
                mail.Body = @"<style>
                p{
                    font-family:""Trebuchet MS"", Arial, Helvetica, sans-serif;
                    font-size:0.8em;
                    text-align:left;
                }
            </style>" + "<body> <p>Hi All,</p> <p>TB 2.0 (Instances from 1 to 10) - Automation Script execution Started in " + Environment + "</p>" + "<p><strong>Build Name:</strong> TB 2.0 (Instances from 1 to 10) Automation Framework</p>"
                      + "<p><strong>StartTime:</strong>" + DateTime.Now + "</p>" + "<p><strong>Environment:</strong>" + Environment + "</p>" + "<p><strong>Approximate Execution Time: </strong>" + ExecutionTime + " hrs </p>" + "<p><strong>Note:</strong> Please do not click the run button in bamboo until we receive the results for the previous run</p>" + "</body>";
                mail.IsBodyHtml = true;
                
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public interface IRunTestCases
    {
        TestCase CurrentTestCase { get; set; }
        void RunTestCase();
    }

    public class RunTestCases : IRunTestCases
    {
        public TestCase CurrentTestCase { get; set; }
        
        
        public void RunTestCase()
        {
            //ExecutionSession.lstExecutedTestCases = new List<ExecutedTestCase>();
            bool runMultipleIteration = CurrentTestCase.RunIterations;
            HTMLChartSummaryReport htmlChartSummaryReport;
            int iterationCount = HelperClass.GetIterationCount(CurrentTestCase);
            if (!HelperClass.runAllBrowsers && !HelperClass.runMultipleBrowsers
                && !HelperClass.runMultipleThreads)
            {
                if (runMultipleIteration && iterationCount > 1)
                {
                    RunMultipleIterations(CurrentTestCase, iterationCount);
                }
                else
                {
                    RunOneIteration(CurrentTestCase);
                }
            }
            else
            {
                RunOneIteration(CurrentTestCase);
            }
            ExecutedTestCase t = new ExecutedTestCase();
            t.Browser = CurrentTestCase.Browser;
            t.clientUrl = ExecutionSession.dictCommonData["EnvironmentUrl"];
            t.Priority = CurrentTestCase.Priority;
            t.No = "1";
            t.Status = CurrentTestCase.OverAllResult;
            t.TestCaseName = CurrentTestCase.TestCaseName;
            t.TestCaseResultUrl = CurrentTestCase.ExcelReportPath;
            t.Category = CurrentTestCase.Category;
            t.FailedStepNo = CurrentTestCase.ErrorStepNo;
            t.FailedStep = CurrentTestCase.ErrorStep;
            t.ExecutionTime = CurrentTestCase.ExecutionTime;
            CurrentTestCase.ErrorStep = "";
            ExecutionSession.lstExecutedTestCases.Add(t);
            //htmlChartSummaryReport = new HTMLChartSummaryReport();
           //htmlChartSummaryReport.generateSummaryReport();
        }

        private void RunOneIteration(TestCase testCase)
        {
            
            string className = string.Empty;
            Assembly assembly = Assembly.GetEntryAssembly();
            CategoryClass categoryClass = ExecutionSession.lstCategoryClass.First(category => category.CategoryName.ToLower().Trim() == testCase.Category.ToLower().Trim());
            className = "TB2." + categoryClass.ClassName;
            Type type = assembly.GetType(className);
            object classInstance = Activator.CreateInstance(type, testCase);
            
            MethodInfo methodInfo;
            methodInfo = type.GetMethod("GenerateHeader");
            if (methodInfo != null)
            {
                methodInfo.Invoke(classInstance, null);
            }
            string methodName = testCase.TestCaseName.Replace(" ", "_");
            methodInfo = type.GetMethod(methodName);
            if (methodInfo != null)
            {
                try
                {
                    methodInfo.Invoke(classInstance, null);
                }
                catch (TargetInvocationException ex)
                {
                    Logger.Log(testCase, ex.ToString());
                    methodInfo = type.GetMethod("AddErrorStep");
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(classInstance, null);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                }
            }

            methodInfo = type.GetMethod("GenerateFooter");
            if (methodInfo != null)
            {
                methodInfo.Invoke(classInstance, null);
            }
            //methodInfo = type.GetMethod("EndTestCase");
            //if (methodInfo != null)
            //{
            //    methodInfo.Invoke(classInstance, null);
            //}

            ExecutionSession.TestCaseExecuted = ExecutionSession.TestCaseExecuted + 1;
        }

        private void RunMultipleIterations(TestCase testCase, int iterationCount)
        {
            string className = string.Empty;
            Assembly assembly = Assembly.GetEntryAssembly();
            CategoryClass categoryClass = ExecutionSession.lstCategoryClass.First(category => category.CategoryName == testCase.Category);
            className = "AutomationFramework." + categoryClass.ClassName;
            Type type = assembly.GetType(className);
            object classInstance = Activator.CreateInstance(type, testCase);
            MethodInfo methodInfo;

            methodInfo = type.GetMethod("SetTotalIterationCount");
            if (methodInfo != null)
            {
                object[] paramArray = new object[] { iterationCount };
                ParameterInfo[] parameters = methodInfo.GetParameters();
                methodInfo.Invoke(classInstance, paramArray);
            }
            methodInfo = type.GetMethod("GenerateHeader");
            if (methodInfo != null)
            {
                methodInfo.Invoke(classInstance, null);
            }
            string methodName = testCase.TestCaseName.Replace(" ", "_");
            for (int i = 1; i <= iterationCount; i++)
            {
                Console.WriteLine("Current Iteration");
                methodInfo = type.GetMethod("SetToCurrentIteration");
                if (methodInfo != null)
                {
                    object[] paramArray = new object[] { i };
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    methodInfo.Invoke(classInstance, paramArray);
                }
                Thread.Sleep(1000);
                methodInfo = type.GetMethod(methodName);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(classInstance, null);
                }
            }
            methodInfo = type.GetMethod("GenerateFooter");
            if (methodInfo != null)
            {
                methodInfo.Invoke(classInstance, null);
            }
            ExecutionSession.TestCaseExecuted = ExecutionSession.TestCaseExecuted + 1;
        }
        

    }

    public class ExcelRunTestCases : IRunTestCases
    {
        public TestCase CurrentTestCase { get; set; }

        public void RunTestCase()
        {
            throw new NotImplementedException();
        }

    }
}
