using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TB2;
using System.IO;
using System.Globalization;

namespace FrameworkLibrary.Reports
{
    public class HTMLChartSummaryReport
    {

        StringBuilder reportStringBuilder = null;
        TimeSpan executionTime;
        bool isSingleThreadExec = true;

        /**
         * Method to generate summary report
         * @throws IOException
         */
        public void generateSummaryReport()
        {

            generateChartSummaryReport();
        }

        /**
         * Method to generate summary report
         * @param overAllTime
         * @throws IOException
         */
        public void generateSummaryReport(TimeSpan overAllTime)
        {

            isSingleThreadExec = false;
            executionTime = overAllTime;
            generateChartSummaryReport();
        }

        /**
         * Method to generate chart summary report
         */
        public void generateChartSummaryReport() {

		int totalTC = 0, passedTC = 0;

		try {

			totalTC = ExecutionSession.TestCaseExecuted;

			/*switch (HelperClass.baseModel.getExecutionRunType()) {

			case MultiBrowser:
				totalTC = ExecutionSession.lstTestCase.size();
				break;
			case GridMultiBrowser:
				totalTC = ExecutionSession.lstTestCaseExecuted.size();
				break;
			}*/


			//long timeTaken =  System.DateTime.Now - ExecutionSession.startTime;
			
            




			/*overallTimeTaken = TimeUtil.millisToShortDHMS(timeTaken);
			
			if (overallTimeTaken.equalsIgnoreCase("00:00:00")) {

				overallTimeTaken = "00:00:01";
             * 
			}*/

			passedTC = ExecutionSession.lstTestCase.Where(testCase => testCase.OverAllResult == OverAllResult.PASS).ToList().Count;
           

		} catch (Exception ex) {

			Console.WriteLine("Exception occured : " + ex.Message);
		}

		try {

			reportStringBuilder = new StringBuilder(HelperClass.rptChartHTML);

			String replaceValue;
			replaceValue = "##" + "ReportTitle" + "##";

            reportStringBuilder.Replace(replaceValue,"Summary report");
            replaceValue = "##" + "DateTime" + "##";
            reportStringBuilder.Replace(replaceValue,DateTime.Now.ToString());
			replaceValue = "##" + "URL" + "##";
            reportStringBuilder.Replace(replaceValue,ExecutionSession.dictCommonData["EnvironmentUrl"]);
			replaceValue = "##" + "MachineName" + "##";
            reportStringBuilder.Replace(replaceValue,Environment.MachineName);

			replaceValue = "##" + "TimeTaken" + "##";
            
            TimeSpan  timeTaken = System.DateTime.Now.Subtract(ExecutionSession.startTime);
            reportStringBuilder.Replace(replaceValue, timeTaken.ToString(@"hh\:mm\:ss"));

			replaceValue = "##" + "TotalTestCases" + "##";
            reportStringBuilder.Replace(replaceValue,Convert.ToString(totalTC));
			replaceValue = "##" + "NoofTCPassed" + "##";
            reportStringBuilder.Replace(replaceValue,Convert.ToString(passedTC));
			replaceValue = "##" + "NoofTCFailed" + "##";
            reportStringBuilder.Replace(replaceValue,Convert.ToString(totalTC - passedTC));

			double percentage;

			percentage = (double)passedTC / totalTC;

			replaceValue = "##" + "SuccessRate" + "##";

            reportStringBuilder.Replace(replaceValue, percentage.ToString("P", CultureInfo.InvariantCulture));
			replaceValue = "##" + "FAILPercentage" + "##";
            reportStringBuilder.Replace(replaceValue,(1 - percentage).ToString("P", CultureInfo.InvariantCulture));			

			replaceValue = "##" + "PASSPercentage" + "##";
            reportStringBuilder.Replace(replaceValue, percentage.ToString("P", CultureInfo.InvariantCulture));
			replaceValue = "##" + "PieChartDiv" + "##";
            reportStringBuilder.Replace(replaceValue,getChartDiv(percentage*100));

			StringBuilder tcRowBuilder = new StringBuilder();
			tcRowBuilder.Append(addTCResults());

			replaceValue = "##" + "TCResultRows" + "##";
            reportStringBuilder.Replace(replaceValue,tcRowBuilder.ToString());

			saveSummaryReport();

		} catch (IOException ioEx) {

			Console.WriteLine(ioEx.Message);

		} finally {

		}
	}

        /**
         * Method to get chart div
         * 
         * @param percentage
         * @return
         */
        public String getChartDiv(double percentage)
        {

            String pieChartDiv;
            String[] colors = { "#60782a", "#ad2d2d", "#ad2d2d", "#ad2d2d", };

            double radius = 8;

            if (percentage > 50)
            {
                percentage = 100 - percentage;
                colors[0] = "#ad2d2d";
                colors[1] = "#60782a";
                colors[2] = "#60782a";
                colors[2] = "#60782a";
            }

            double[] values = { 0, 180 };

            if (percentage <= 25)
            {

                values[0] = 2.4 * percentage;

            }
            else if (percentage <= 50)
            {

                values[0] = ((percentage - 25) * 4.8) + 60;
            }
            // else if(percentage <=75)
            // values[0] = ((percentage - 50) * 14.4) + 180;

            pieChartDiv = PieChart.generate(values, colors, radius);
            return pieChartDiv;
        }


        /**
         * Method to get test case result row
         * 
         * @param testCaseName
         * @param timeTaken
         * @param status
         * @param reportPath
         * @return
         */
        public String getTCResultRow(String testCaseName, String timeTaken,
                String status, String reportPath)
        {

            StringBuilder tcRowBuilder = new StringBuilder("");

            if (status == "FAIL")
            {

                tcRowBuilder.Append("<tr valign='top' class='fail'>");

            }
            else
            {

                tcRowBuilder.Append("<tr valign='top'>");
            }

            tcRowBuilder
                    .Append("<td>")
                    .Append(testCaseName)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(timeTaken)
                    .Append("</td>")
                    .Append("<td>")
                    .Append(status)
                    .Append("</td>")
                    .Append("<td><a href='" + reportPath
                            + "' target='about_blank'>Click Here</a></td>");

            return tcRowBuilder.ToString();
        }

        /**
         * Method to save summary report
         * 
         * @throws IOException
         */
        public void saveSummaryReport()
        {

            string testReportPath = HelperClass.reportRunPath + "\\" + "SummaryReport" + ".html";
            if (File.Exists(testReportPath))
                File.Delete(testReportPath);
            using (StreamWriter writer = new StreamWriter(testReportPath, true))
            {
                writer.WriteLine(reportStringBuilder.ToString());
            }
        }

        /**
         * Method to add test case results
         * 
         * @return
         */
        public String addTCResults()
        {

            StringBuilder tcRowBuilder = new StringBuilder("");
            int testCaseNo = 1;
            String browser;
            String reportPath;

            foreach (TestCase testCase in ExecutionSession.lstTestCase)
            {

                String reportHTMLPath = testCase.HTMLReportPath;

                if (reportHTMLPath == null)
                {
                    continue;
                }

                switch (testCase.OverAllResult)
                {
                    case OverAllResult.PASS:
                        tcRowBuilder.Append("<tr valign='top'>");
                        break;
                    case OverAllResult.FAIL:
                        tcRowBuilder.Append("<tr valign='top' class='fail'>");
                        break;
                    default:
                        break;
                }

                browser = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Enum.GetName(typeof(Browser), testCase.Browser));

                /*switch (HelperClass.baseModel.getExecutionRunType()) {

                case MultiBrowser:
                    browser = SBReplaceUtil.getBrowserList();
                    break;
                case GridMultiBrowser:
                    browser = HelperClass.getGridBrowsers();
                    break;
                }*/

                StringBuilder reportStepBuilder = new StringBuilder(
                        "<td><a target='_blank' href=\"##ReportPath##\">"
                                + testCase.TestCaseName + "</a></td>");
                String replaceValue = "##" + "ReportPath" + "##";

                if (String.IsNullOrEmpty(reportHTMLPath))
                {
                    reportHTMLPath = "";
                }

                /*reportStepBuilder = new StringBuilder(
                        SBReplaceUtil.replaceReportPath(reportStepBuilder,
                                reportHTMLPath));*/
                reportStepBuilder.Replace(replaceValue, reportHTMLPath);

                tcRowBuilder
                        .Append(reportStepBuilder.ToString().Replace("\\", "\\\\"))
                        .Append("<td>").Append(browser).Append("</td><td>")
                        .Append(testCase.Priority).Append("</td><td>")
                        .Append(testCase.OverAllResult).Append("</td>");
                testCaseNo++;
            }

            return tcRowBuilder.ToString();
        }
    }
}
