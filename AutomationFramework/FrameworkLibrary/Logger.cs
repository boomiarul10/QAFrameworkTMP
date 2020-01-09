using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TB2
{
    public static class Logger
    {
        public static void Log(string message)
        {
            string logFile = string.Format(HelperClass.errorLogPath + "\\Logs_{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now);

             File.AppendAllText(logFile, DateTime.Now.ToShortDateString() + 
                 " " + DateTime.Now.ToShortTimeString() + ":\t" + message + Environment.NewLine);             
         }

        public static void Log(TestCase testCase, string message)
        {
            StringBuilder sbErrorMessage = new StringBuilder();
            sbErrorMessage.Append("Test Case Name : " + testCase.TestCaseName + Environment.NewLine);
            sbErrorMessage.Append("Execution Time : " + DateTime.Now.ToShortDateString() + 
                " " + DateTime.Now.ToShortTimeString() + Environment.NewLine);
            sbErrorMessage.Append("Error Details : " + Environment.NewLine);
            sbErrorMessage.Append(message);

            File.AppendAllText(HelperClass.tcErrorLogPath, Convert.ToString(sbErrorMessage));
        }
    }
}
