using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDAPIOLELib;
namespace TB2
{
    public class QCHelper
    {
        TDConnection qcConnect;
        public QCHelper()
        {
            try
            {
                qcConnect = new TDConnection();
                qcConnect.InitConnectionEx("http://qualitycenter-v10.homedepot.com/qcbin");
                qcConnect.Login(HelperClass.QCUserName, HelperClass.QCPassword);
                qcConnect.Connect("ONLINE", "EBWCS");
                if (qcConnect.Connected)
                    Console.WriteLine("Connected to QC");
                else
                    Console.WriteLine("Not Connected to QC");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public void UploadResults(TestCase currentTestCase)
        {
            string testFolder = Convert.ToString(ExecutionSession.dictCommonData["QCFolder"]);
            string testSetName = currentTestCase.Category;

            TestSetFactory tsFactory = (TestSetFactory)qcConnect.TestSetFactory;
            TestSetTreeManager tsTreeMgr = (TestSetTreeManager)qcConnect.TestSetTreeManager;
            TestSetFolder tsFolder = (TestSetFolder)tsTreeMgr.get_NodeByPath(testFolder);
            List tsList = tsFolder.FindTestSets(testSetName, false, null);
            TestSet testSet = tsList[1];
            tsFolder = (TestSetFolder)testSet.TestSetFolder;
            TSTestFactory tsTestFactory = (TSTestFactory)testSet.TSTestFactory;
            List tsTestList = tsTestFactory.NewList("");

            //  And finally, update each test case status:
            foreach (TSTest tsTest in tsTestList)
            {
               if (currentTestCase.TestCaseName == tsTest.Name.Remove(0, 3))
                {

                    RunFactory runFactory = (RunFactory)tsTest.RunFactory;
                    List allfields = runFactory.Fields;

                    String browserValue = tsTest["TC_USER_TEMPLATE_10"];

                    Run lastRun = (Run)tsTest.LastRun;

                    string runName = runFactory.UniqueRunName;
                    RunFactory objRunFactory = tsTest.RunFactory;
                    Run theRun = objRunFactory.AddItem(runName);
                    theRun.Name = runName;
                    theRun.CopyDesignSteps();
                    StepFactory Step = theRun.StepFactory;
                    List stepList = (List)Step.NewList("");
                    if (currentTestCase.OverAllResult == OverAllResult.PASS)
                        theRun.Status = "Passed";
                    else
                        theRun.Status = "Failed";
                    theRun.Post();

                    //Delete current attachment from QC test set test case
                    AttachmentFactory objAttachmentFactory = tsTest.Attachments;

                    var objCurrentAttachments = objAttachmentFactory.NewList("");

                    for (int objC = 1; objC <= objCurrentAttachments.Count; objC++)
                    {
                        try
                        {
                            objAttachmentFactory.RemoveItem(tsTest.Attachments.NewList("").Item(1).ID);
                        }
                        catch { }
                    }

                    IAttachment objAttachment = objAttachmentFactory.AddItem(DBNull.Value);
                    objAttachment.FileName = currentTestCase.QCHTMLReportPath;
                    objAttachment.Type = 1;
                    objAttachment.Post();

                    string[] filePaths = System.IO.Directory.GetFiles(currentTestCase.QCScreenShotPath);
                    foreach (string file in filePaths)
                    {
                        objAttachment = objAttachmentFactory.AddItem(DBNull.Value);
                        objAttachment.FileName = file;
                        objAttachment.Type = 1;
                        objAttachment.Post();
                    }
                    break;
                }

            }
        }

        public void SendEmail()
        {
            int noOfTCPassed;
            int noOfTCFailed;
            int totalTC;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("Dear Team,");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>HDC regression on ##QAEnv## environment is completed.");
            mailBody.AppendFormat("Please find regression report below:</p>");
            mailBody.Append("<table style=\"border-color: Black;border-width: 0 0 0 0;border-style: solid;width:60%;cellspacing:0px;border-spacing: 0;\">");
            mailBody.Append("<thead><tr style=\" background-color: #808080;font-weight:bold;font-size:17;font-family:Calibri;text-align:center;\">");
            mailBody.Append("<td style=\"width:30%; margin: 0;border-collapse:collapse;border-color: Black; border-width: 0 0 1px 0;border-style: solid;\"> Date</td>");
            mailBody.Append("<td style=\"width:20%; margin: 0;border-collapse:collapse;border-color: Black; border-width: 0 0 1px 1px;border-style: solid;\">");
            mailBody.Append("Environment</td><td style=\"width:15%; margin: 0;border-collapse:collapse;border-color: Black; border-width: 0 0 1px 1px;border-style:");
            mailBody.Append("solid;\">Build</td><td style=\"width:10%; margin: 0;border-collapse:collapse;border-color: Black; border-width: 0 0 1px 1px;");
            mailBody.Append("border-style: solid;\">Pass</td><td style=\"width:10%; margin: 0;border-collapse:collapse;border-color: Black;");
            mailBody.Append("border-width: 0 0 1px 1px;border-style: solid;\">Fail</td><td style=\"width:15%; margin: 0;border-collapse:collapse;");
            mailBody.Append("border-color: Black; border-width: 1px 1px 1px 1px;border-style: solid;\">Total</td></tr></thead><tr style=\"font-size:17;");
            mailBody.Append("font-family:Calibri;text-align:center;\"><td style=\"margin: 0;border-collapse:collapse;border-color: Black; border-width: 0 0 1px 1px;");
            mailBody.Append("border-style: solid;\">##Date##</td><td style=\"margin: 0;border-collapse:collapse;border-color: Black; border-width: 0 0 1px 1px;");
            mailBody.Append("border-style: solid;\">##Environment##</td><td style=\"margin: 0;border-collapse:collapse;border-color: Black;");
            mailBody.Append(" border-width: 0 0 1px 1px;border-style: solid;\">##Build##</td><td style=\"margin: 0;border-collapse:collapse;");
            mailBody.Append("border-color: Black; border-width: 0 0 1px 1px;border-style: solid;\">##Pass##</td><td style=\"margin: 0;border-collapse:collapse;");
            mailBody.Append("border-color: Black; border-width: 0 0 1px 1px;border-style: solid;\">##Fail##</td><td style=\"margin: 0;border-collapse:collapse;");
            mailBody.Append("border-color: Black; border-width: 0 0 1px 1px;border-style: solid;\"> ##Total##</td></tr></table>");
            mailBody.Append("</br> Once Analysis of failed test cases(if any) is done, final report will be sent.<br/>");
            mailBody.Append("<br/>Regards,</br>##RegardsFrom##");

            noOfTCPassed = ExecutionSession.lstTestCase.Where(testCase => testCase.OverAllResult == OverAllResult.PASS).ToList().Count;
            noOfTCFailed = ExecutionSession.lstTestCase.Where(testCase => testCase.OverAllResult == OverAllResult.FAIL).ToList().Count;
            totalTC = noOfTCPassed + noOfTCFailed;
            mailBody.Replace("##Date##", Convert.ToString(DateTime.Now.ToString()));
            mailBody.Replace("##Environment##", ExecutionSession.dictCommonData["Environment"]);
            mailBody.Replace("##Build##", ExecutionSession.dictCommonData["BuildVersion"]);
            mailBody.Replace("##Pass##", Convert.ToString(noOfTCPassed));
            mailBody.Replace("##Fail##", Convert.ToString(noOfTCFailed));
            mailBody.Replace("##Total##", Convert.ToString(totalTC));
            mailBody.Replace("##QAEnv##", ExecutionSession.dictCommonData["Environment"]);
            mailBody.Replace("##RegardsFrom##", HelperClass.emailRegardsFrom);
            qcConnect.SendMail(HelperClass.emailTo, HelperClass.emailFrom, HelperClass.emailSubject, mailBody.ToString(), Type.Missing);
        }

    }
}
