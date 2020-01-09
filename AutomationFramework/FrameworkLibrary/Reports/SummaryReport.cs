using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TB2
{
    public class SummaryReport
    {
        ExcelSummaryReport excelSummaryReport;
        HTMLSummaryReport htmlSummaryReport;
        public SummaryReport()
        {
            excelSummaryReport = new ExcelSummaryReport();
            //htmlSummaryReport = new HTMLSummaryReport();
        }
        public void GenerateSummaryReport()
        {
            //htmlSummaryReport.GenerateSummaryReport();
            excelSummaryReport.GenerateSummaryReport();
        }
    }
}
