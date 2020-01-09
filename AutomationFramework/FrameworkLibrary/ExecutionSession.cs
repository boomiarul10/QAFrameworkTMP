using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TB2
{
    public class ExecutionSession
    {
        public static List<clientList> lstClient;
        public static List<TestCase> lstTestCase;
        public static List<TestCase> lstAllBrowsersTC;
        public static List<string> lstTestCategories;
        public static List<Priority> lstTestPriority;
        public static List<CategoryClass> lstCategoryClass;
        public static Dictionary<string, string> dictCommonData;
        public static List<ErrorSteps> lstErrorsteps;
        public static int TestCaseExecuted;
        public static DateTime startTime;

        public static List<ExecutedTestCase> lstExecutedTestCases;

    }
}
