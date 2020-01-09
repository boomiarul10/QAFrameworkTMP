using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TB2
{

    public class ErrorSteps
    {
        public string clientUrl { get; set; }
        public int FailedStepNo { get; set; }
        public string FailedStep { get; set; }
    }

    public class clientList
    {
        public string env { get; set; }
        public string EnvironmentName { get; set; }
        public string clientUrl { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Group { get; set; }
        public string AdvancedSearch { get; set; }
        public string BasicSearch { get; set; }
        public string FilterModule { get; set; }
        public string RecentJobs { get; set; }
        public string SocialMedia { get; set; }
        public string JobAlert { get; set; }
        public string SiteMap { get; set; }
        public string RSSFeed { get; set; }
        public string MeetUs { get; set; }
        public string JobMatching { get; set; }
    }


    public static class prevbrowser
    {
        public static string browser = "";
    }

    public class ExecutedTestCase
    {
        public string TestCaseName { get; set; }
        public string clientUrl { get; set; }
        public string TestCaseResultUrl { get; set; }
        public OverAllResult Status { get; set; }
        public string No { get; set; }
        public Browser Browser { get; set; }
        public Priority Priority { get; set; }
        public string Category { get; set; }
        public int FailedStepNo { get; set; }
        public string FailedStep { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        
    }

    public class TestCase
    {
        public string TestCaseName { get; set; }
        public Browser Browser { get; set; }
        public string Category { get; set; }
        public Priority Priority { get; set; }
        public bool RunIterations { get; set; }
        public string browserDetails { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status ExecutionStatus { get; set; }
        public OverAllResult OverAllResult { get; set; }
        public int NoOfStepsPassed { get; set; }
        public int NoOfStepsFailed { get; set; }
        public int NoOfWarningSteps { get; set; }
        public string TimeTaken { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public string ScreenShotPath { get; set; }
        public string HTMLReportPath { get; set; }
        public string ExcelReportPath { get; set; }
        public string QCScreenShotPath { get; set; }
        public string QCHTMLReportPath { get; set; }
        public string TestCaseReportHTML { get; set; }
        public int ErrorStepNo { get; set; }
        public string ErrorStep { get; set; }
    }

    public class ExcelTestCase
    {
        public string TestCaseName { get; set; }
        public List<String> lstMethods;
    }

    public enum Browser
    {
        FireFox, Chrome, IE, Safari
    }

    public enum PrevBrowser
    {
        FireFox, Chrome, IE, Safari
    }

    public enum Status
    {
        NotStarted, InProgress, Completed
    }

    public enum StepResult
    {
        PASS, FAIL, WARNING
    }

    public enum Priority
    {
        P1, P2, P3
    }

    public enum OverAllResult
    {
        NA = 0, PASS = 1, FAIL = 2, WARNING = 3
    }

    public class CategoryClass
    {
        public string CategoryName { get; set; }
        public string ClassName { get; set; }
    }

    public class ThreadClass
    {
        public string ThreadName { get; set; }
        public Thread currentThread { get; set; }
    }

    public enum Device
    {
        Iphone,Ipad,Android
    }
}
