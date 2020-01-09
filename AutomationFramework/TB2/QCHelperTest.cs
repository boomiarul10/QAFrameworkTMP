using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TB2;
using TB2.DAO;
using TB2.UIStore;
using TDAPIOLELib;

namespace AutomationFramework
{    
   public class QCHelperTest
    {
       TDConnection qcConnect;
       public QCHelperTest()
       {
           qcConnect = new TDConnection();
           qcConnect.InitConnectionEx("http://qualitycenter-v10.homedepot.com/qcbin");
           qcConnect.Login("PXS8677", "tree$123");
           qcConnect.Connect("ONLINE", "EBWCS");
           if (qcConnect.Connected)
               Console.WriteLine("Connected to QC");
           else
               Console.WriteLine("Not Connected to QC");
       }

       public void UploadResults(TB2.TestCase currentTestCase)
       {
            string testFolder = @"Root\WCS 7up Core - 5022\zz Automation\PracticeExecution\Temp_Prashant\QA72_7_31";
            string testSetName = currentTestCase.Category;

            TestSetFactory tsFactory = (TestSetFactory)qcConnect.TestSetFactory;
            TestSetTreeManager tsTreeMgr = (TestSetTreeManager)qcConnect.TestSetTreeManager;
            TestSetFolder tsFolder = (TestSetFolder)tsTreeMgr.get_NodeByPath(testFolder);
            List tsList = tsFolder.FindTestSets(testSetName, false, null);
            TestSet testSet = tsList[1];
            //foreach (TestSet testSet in tsList)
            //{
                tsFolder = (TestSetFolder)testSet.TestSetFolder;
                TSTestFactory tsTestFactory = (TSTestFactory)testSet.TSTestFactory;
                List tsTestList = tsTestFactory.NewList("");

                //  And finally, update each test case status:
                foreach (TSTest tsTest in tsTestList)
                {
                    //System.Console.Out.WriteLine("Test Case ID: " + tsTest.ID + ", Test Case Name: " + tsTest.Name + "\n");
                    if (currentTestCase.TestCaseName == tsTest.Name.Remove(0,3))
                    {

                        RunFactory runFactory = (RunFactory)tsTest.RunFactory;
                        List allfields = runFactory.Fields;

                        String browserValue = tsTest["TC_USER_TEMPLATE_10"];

                       // Console.WriteLine("Browser value : " + browserValue);

                        Run lastRun = (Run)tsTest.LastRun;

                        string runName = runFactory.UniqueRunName;
                        RunFactory objRunFactory = tsTest.RunFactory;
                        Run theRun = objRunFactory.AddItem(runName);
                        theRun.Name = runName;

                        //Get the count of test steps and compare it with the number of steps that were actually executed
                        //and define the Execution status accordinagly
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

                    objSkipExec:

                        var objCurrentAttachments = objAttachmentFactory.NewList("");


                        for (int objC = 1; objC <= objCurrentAttachments.Count; objC++)
                        {
                            try
                            {
                                objAttachmentFactory.RemoveItem(tsTest.Attachments.NewList("").Item(1).ID);
                            }
                            catch { }
                        }

                        if (objAttachmentFactory.NewList("").Count > 0)
                            goto objSkipExec;

                        IAttachment objAttachment = objAttachmentFactory.AddItem(DBNull.Value);
                        objAttachment.FileName = currentTestCase.HTMLReportPath;
                        objAttachment.Type = 1;
                        objAttachment.Post();

                        string[] filePaths = System.IO.Directory.GetFiles(currentTestCase.ScreenShotPath);
                        foreach (string file in filePaths)
                        {
                            objAttachment = objAttachmentFactory.AddItem(DBNull.Value);
                            objAttachment.FileName = file;
                            objAttachment.Type = 1;
                            objAttachment.Post();
                        }
                        break;
                   // }
                }

            } 
        }
    }
}
