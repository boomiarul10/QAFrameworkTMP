using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.Drawing;
using System.Net;
namespace TB2
{
    public class ExcelSummaryReport
    {
        Application xlApp;
        Workbook xlWorkBook;
        Worksheet xlWorkSheet;
        Chart oChart;
        ChartObjects myCharts;
        ChartObject myCharts1;
        Range chartRange;
        Range workSheet_range;
        int clientCount;
        int passCount;
        int failCount;
        int rowNo;
        int testCaseNo = 1;
        int currentRowNo = 2;
        int noOfTCPassed;
        int noOfTCFailed;
        string currentDirectory = Environment.CurrentDirectory;
        String basePath;
        string summaryChartsFolder;
        List<String> PrevRepData = new List<String>();
        DateTime now = DateTime.Now;
        private string var;
        const int SHEETNEEDED = 2;

        public void GenerateSummaryReport()
        {
            try
            {
                xlApp = new Application();
                CreateScreenShotFolder();
                GenerateChartsSheets();
               // GenerateStepsChartsSheets();
                GenerateTabularReport();
                SaveSummaryReport();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (xlApp != null)
                {
                    xlApp.Quit();
                    int hWnd = xlApp.Application.Hwnd;
                    uint processID;
                    GetWindowThreadProcessId((IntPtr)hWnd, out processID);
                    Process[] procs = Process.GetProcessesByName("EXCEL");
                    foreach (Process p in procs)
                    {
                        if (p.Id == processID)
                            p.Kill();
                    }
                    Marshal.FinalReleaseComObject(xlApp);
                }
            }
        }



        private void GenerateChartsSheets()
        {
            xlWorkBook = xlApp.Workbooks.Add(Missing.Value);
            int sheetAvailable = xlWorkBook.Sheets.Count;
           
            if (sheetAvailable < SHEETNEEDED)
            {
                xlWorkBook.Sheets.Add(Count:SHEETNEEDED - sheetAvailable);
           
            }
            //int count = xlWorkBook.Sheets.Count;
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = "SummaryTestCaseReport";
            setColumnWidthOverAllSheet();
            GeneratePreviousRunReport();
            GenerateByClient();
           GenerateOverallPieChart();
           SetPrevDataResults();
           // GenerateByPriorityChart();
            //GenerateByCategoryChart();
            
        }

        private void GenerateStepsChartsSheets()
        {
            int count = xlWorkBook.Sheets.Count;
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
            xlWorkSheet.Name = "SummaryStepsReport";
            setColumnWidthStepsSheet();
            GenerateOverAllStepsPieChart();
            GenerateStepsPriorityChart();
            GenerateStepsCategoryChart();
        }


        private void GeneratePreviousRunReport()
        {

          
            if (currentDirectory.Contains("Executables"))
            {
                basePath = currentDirectory.Substring(0, currentDirectory.IndexOf("Executables"));
            }
            else
            {
                basePath = currentDirectory.Substring(0, currentDirectory.IndexOf("AutomationFramework"));
            }
            string []text = System.IO.File.ReadAllText(basePath + "/PrevRunResults.txt").Split(',');


            workSheet_range = xlWorkSheet.Range["W3:Y3"];
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;
            xlWorkSheet.Range["W3"].Value = "TESTCASE";
            xlWorkSheet.Range["X3"].Value = "PASS";
            xlWorkSheet.Range["Y3"].Value = "FAIL";
            xlWorkSheet.Range["W4"].Value = (Convert.ToInt32(text[3]) + Convert.ToInt32(text[4])).ToString();
            xlWorkSheet.Range["X4"].Value = text[3].ToString();
            xlWorkSheet.Range["Y4"].Value = text[4].ToString();
            Range temp = xlWorkSheet.Range["W4:Y4"];
            temp.Borders.Color = System.Drawing.Color.Black.ToArgb();
            temp.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(800, 30, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["X3", "Y4"];
            oChart.SetSourceData(chartRange);
            oChart.PlotBy = XlRowCol.xlRows;
            oChart.ChartType = XlChartType.xl3DPie;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowPercent);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Previous Execution Testcase summary report - "+text[5].ToString();
            string overAllPieChartPath = summaryChartsFolder + "\\PrevTestcaseSummaryReport.jpg";
            oChart.Export(overAllPieChartPath, "jpg", Missing.Value);


            workSheet_range = xlWorkSheet.Range["W23:Y23"];
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;
            xlWorkSheet.Range["W23"].Value = "TOTAL CLIENT";
            xlWorkSheet.Range["X23"].Value = "PASS";
            xlWorkSheet.Range["Y23"].Value = "FAIL";
            xlWorkSheet.Range["W24"].Value = (Convert.ToInt32(text[0]) + Convert.ToInt32(text[1])).ToString();
            xlWorkSheet.Range["X24"].Value = text[0].ToString();
            xlWorkSheet.Range["Y24"].Value = text[1].ToString();
            temp = xlWorkSheet.Range["W24:Y24"];
            temp.Borders.Color = System.Drawing.Color.Black.ToArgb();
            temp.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(800, 320, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["X23", "Y24"];
            oChart.SetSourceData(chartRange);
            oChart.PlotBy = XlRowCol.xlRows;
            oChart.ChartType = XlChartType.xl3DPie;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowPercent);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Previous Execution client summary report - "+text[3].ToString();
            string overAllPrevPieChartPath = summaryChartsFolder + "\\PrevClientSummaryReport.jpg";
            oChart.Export(overAllPrevPieChartPath, "jpg", Missing.Value);


          

        }

        private void CreateScreenShotFolder()
        {
            summaryChartsFolder = HelperClass.reportRunPath + "\\SummaryCharts";
            if (!Directory.Exists(summaryChartsFolder))
                Directory.CreateDirectory(summaryChartsFolder);
        }

        private void setColumnWidthOverAllSheet()
        {
            workSheet_range = xlWorkSheet.get_Range("A:A", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 25;
            workSheet_range = xlWorkSheet.get_Range("B:B", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 15;
            workSheet_range = xlWorkSheet.get_Range("C:C", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 15;
            workSheet_range = xlWorkSheet.get_Range("W:W", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 15;
            workSheet_range = xlWorkSheet.get_Range("X:X", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 15;
            workSheet_range = xlWorkSheet.get_Range("Y:Y", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 15;
        }

        private void setColumnWidthStepsSheet()
        {
            workSheet_range = xlWorkSheet.get_Range("A:A", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 25;
            workSheet_range = xlWorkSheet.get_Range("B:B", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 13;
            workSheet_range = xlWorkSheet.get_Range("C:C", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 13;
            workSheet_range = xlWorkSheet.get_Range("D:D", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 18;
        }


        private void GenerateByClient()
        {
            workSheet_range = xlWorkSheet.Range["A23:C23"];

            //columns heading

           // xlWorkSheet.Range["A60"].Value = "TOTAL CLIENT";
            xlWorkSheet.Range["A23"].Value = "TOTAL CLIENT";
            xlWorkSheet.Range["B23"].Value = "PASS";
            xlWorkSheet.Range["C23"].Value = "FAIL";


            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;
            clientCount = ExecutionSession.lstClient.Count();
            int count=0;
            foreach (clientList ClientList in ExecutionSession.lstClient)
            {   
                if (ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Status == OverAllResult.FAIL && testcase.clientUrl == ClientList.clientUrl).Count()>0)
                {

                    count++;
                }
            }
            xlWorkSheet.Range["A24"].Value = clientCount;
            xlWorkSheet.Range["B24"].Value = clientCount - count;
            xlWorkSheet.Range["C24"].Value = count;
            PrevRepData.Add((clientCount - count).ToString());
            PrevRepData.Add(count.ToString());
            PrevRepData.Add(now.ToString("d"));

            workSheet_range = xlWorkSheet.Range["A24", "C24"];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(340, 320, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["B23", "C24"];
            oChart.SetSourceData(chartRange);
            oChart.PlotBy = XlRowCol.xlRows;
            oChart.ChartType = XlChartType.xl3DPie;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowPercent);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Current client summary report";

            string overAllPieChartPath = summaryChartsFolder + "\\ClientSummaryReport.jpg";
            oChart.Export(overAllPieChartPath, "jpg", Missing.Value);

        }


        private void GenerateOverallPieChart()
        {
            workSheet_range = xlWorkSheet.Range["A3:C3"];
            
            //columns heading

            xlWorkSheet.Range["A3"].Value = "TESTCASE";
            xlWorkSheet.Range["B3"].Value = "PASS";
            xlWorkSheet.Range["C3"].Value = "FAIL";


            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;

            passCount = ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Status == OverAllResult.PASS).Count();
            failCount = ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Status == OverAllResult.FAIL).Count();
            xlWorkSheet.Range["A4"].Value = passCount+failCount;
            xlWorkSheet.Range["B4"].Value = passCount;
            xlWorkSheet.Range["C4"].Value = failCount;
            PrevRepData.Add(passCount.ToString());
            PrevRepData.Add(failCount.ToString());
            PrevRepData.Add(now.ToString("d"));
            
            workSheet_range = xlWorkSheet.Range["A3", "C4"];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(340, 30, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["B3", "C4"];
            oChart.SetSourceData(chartRange);
            oChart.PlotBy = XlRowCol.xlRows;
            oChart.ChartType = XlChartType.xl3DPie;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowPercent);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Current testcase summary report";

            string overAllPieChartPath = summaryChartsFolder + "\\testcaseSummaryReport.jpg";
            oChart.Export(overAllPieChartPath, "jpg", Missing.Value);

        }



        private void SetPrevDataResults()
        {
            try
            {
                String temp = "";
                foreach (var item in PrevRepData)
                {
                    temp = temp + item.ToString() + ",";
                    //System.IO.File.WriteAllText(basePath + "/PrevRunResults.txt", var.ToString()+",");
                }
                System.IO.StreamWriter file = new System.IO.StreamWriter(basePath + "/PrevRunResults.txt");
                    file.WriteLine(temp);
                    file.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Setting Previous run results in the textfile - " + e.ToString());
                
            }
        }

        private void GenerateByPriorityChart()
        {            
            workSheet_range = xlWorkSheet.Range["A23:C23"];
            
            //columns heading

            xlWorkSheet.Range["A23"].Value = "Priority";
            xlWorkSheet.Range["B23"].Value = "PASS";
            xlWorkSheet.Range["C23"].Value = "FAIL";

            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;

            rowNo = 23;

            foreach (Priority prority in ExecutionSession.lstTestPriority)
            {
                rowNo++;
                xlWorkSheet.Range["A" + rowNo.ToString()].Value = Enum.GetName(typeof(Priority), prority);
                passCount = ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Priority == prority
                    && testcase.Status == OverAllResult.PASS).Count();
                failCount = ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Priority == prority
                    && testcase.Status == OverAllResult.FAIL).Count();
                xlWorkSheet.Range["B" + rowNo.ToString()].Value = passCount;
                xlWorkSheet.Range["C" + rowNo.ToString()].Value = failCount;

                xlWorkSheet.Range["A" + rowNo.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.Range["A" + rowNo.ToString()].WrapText = true;
                xlWorkSheet.Range["A" + rowNo.ToString()].InsertIndent(5);
            }

            workSheet_range = xlWorkSheet.Range["A23", "C" + rowNo];
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            workSheet_range = xlWorkSheet.Range["B24", "C" + rowNo];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(340, 320, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["A23", "C" + rowNo];
            oChart.SetSourceData(chartRange); ;
            oChart.PlotBy = XlRowCol.xlColumns;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Summary report by priority";           

            Excel.Axis axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlValue,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "No of Test Cases";

            axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlCategory,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "Priority";

            string priorityChartPath = summaryChartsFolder + "\\SummaryReport_Priority.jpg";
            oChart.Export(priorityChartPath, "jpg", Missing.Value);
        }

        private void GenerateByCategoryChart()
        {            
            workSheet_range=xlWorkSheet.Range["A42:C42"];  
 
            //columns heading

            xlWorkSheet.Range["A42"].Value = "Category";
            xlWorkSheet.Range["B42"].Value = "PASS";
            xlWorkSheet.Range["C42"].Value = "FAIL";

            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;

            rowNo = 42;
            foreach (string category in ExecutionSession.lstTestCategories)
            {
                rowNo++;
                xlWorkSheet.Range["A" + rowNo.ToString()].Value = category;
                passCount = ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Category == category
                    && testcase.Status == OverAllResult.PASS).Count();
                failCount = ExecutionSession.lstExecutedTestCases.Where(testcase => testcase.Category == category
                    && testcase.Status == OverAllResult.FAIL).Count();

                xlWorkSheet.Range["B" + rowNo.ToString()].Value = passCount;
                xlWorkSheet.Range["C" + rowNo.ToString()].Value = failCount;

                xlWorkSheet.Range["A" + rowNo.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.Range["A" + rowNo.ToString()].WrapText = true;
                xlWorkSheet.Range["A" + rowNo.ToString()].InsertIndent(5);

            }

            workSheet_range = xlWorkSheet.Range["A42", "C" + rowNo];
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            workSheet_range = xlWorkSheet.Range["B43", "C" + rowNo];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(340, 610, 370, 210);
            oChart = myCharts1.Chart;

            chartRange = xlWorkSheet.Range["A42", "C" + rowNo];
            oChart.SetSourceData(chartRange);
            oChart.PlotBy = XlRowCol.xlColumns;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Summary report by category";

            Excel.Axis axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlValue,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "No of Test Cases";

            axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlCategory,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "Category";

            string categoryChartPath = summaryChartsFolder + "\\SummaryReport_Category.jpg";
            oChart.Export(categoryChartPath, "jpg", Missing.Value);
        }

        private void GenerateOverAllStepsPieChart()
        {
            //format headings
            workSheet_range = xlWorkSheet.Range["A3:D3"];
            //columns heading

            xlWorkSheet.Range["A3"].Value = "";
            xlWorkSheet.Range["B3"].Value = "Pass Steps";
            xlWorkSheet.Range["C3"].Value = "Fail Steps";
            xlWorkSheet.Range["D3"].Value = "Warning Steps";

            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;

            int noOfStepsPassed = (from testCase in ExecutionSession.lstTestCase
                                   select testCase.NoOfStepsPassed)
                                         .Sum();
            int noOfStepsFailed = (from testCase in ExecutionSession.lstTestCase
                                   select testCase.NoOfStepsFailed)
                                         .Sum();
            int noOfWarningSteps = (from testCase in ExecutionSession.lstTestCase
                                    select testCase.NoOfWarningSteps)
                                         .Sum();

            xlWorkSheet.Range["B4"].Value = noOfStepsPassed;
            xlWorkSheet.Range["C4"].Value = noOfStepsFailed;
            xlWorkSheet.Range["D4"].Value = noOfWarningSteps;

            workSheet_range = xlWorkSheet.Range["A3", "D4"];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(400, 30, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["A3", "D4"];
            oChart.SetSourceData(chartRange);
            oChart.PlotBy = XlRowCol.xlRows;
            oChart.ChartType = XlChartType.xl3DPie;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowPercent);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Overall steps report";

            string overAllPieChartPath = summaryChartsFolder + "\\OverallStepsSummaryReport.jpg";
            oChart.Export(overAllPieChartPath, "jpg", Missing.Value);
        }

        private void GenerateStepsPriorityChart()
        {
            workSheet_range = xlWorkSheet.Range["A23:D23"];

            //columns heading
            xlWorkSheet.Range["A23"].Value = "Priority";
            xlWorkSheet.Range["B23"].Value = "Steps Passed";
            xlWorkSheet.Range["C23"].Value = "Steps Failed";
            xlWorkSheet.Range["D23"].Value = "Steps Warning";

            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;

            rowNo = 23;

            var stepstestCaseList =
               from testCase in ExecutionSession.lstTestCase
               group testCase by testCase.Priority into stepsTestCase
               select new
               {
                   Priority = stepsTestCase.Key,
                   StepsPassed = stepsTestCase.Sum(testCase => testCase.NoOfStepsPassed),
                   StepsFailed = stepsTestCase.Sum(testCase => testCase.NoOfStepsFailed),
                   StepsWarning = stepsTestCase.Sum(testCase => testCase.NoOfWarningSteps)
               };
            foreach (var item in stepstestCaseList)
            {
                rowNo++;
                xlWorkSheet.Range["A" + rowNo.ToString()].Value = Enum.GetName(typeof(Priority), item.Priority);
                xlWorkSheet.Range["B" + rowNo.ToString()].Value = item.StepsPassed;
                xlWorkSheet.Range["C" + rowNo.ToString()].Value = item.StepsFailed;
                xlWorkSheet.Range["D" + rowNo.ToString()].Value = item.StepsWarning;

                xlWorkSheet.Range["A" + rowNo.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.Range["A" + rowNo.ToString()].WrapText = true;
                xlWorkSheet.Range["A" + rowNo.ToString()].InsertIndent(5);
            }

            workSheet_range = xlWorkSheet.Range["A23", "D" + rowNo];
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            workSheet_range = xlWorkSheet.Range["B24", "D" + rowNo];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(400, 320, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["A23", "D" + rowNo];
            oChart.SetSourceData(chartRange); ;
            oChart.PlotBy = XlRowCol.xlColumns;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Summary report by priority";

            Excel.Axis axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlValue,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "No of steps";

            axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlCategory,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "Priority";

            string priorityChartPath = summaryChartsFolder + "\\SummaryReport_Priority.jpg";
            oChart.Export(priorityChartPath, "jpg", Missing.Value);

        }

        private void GenerateStepsCategoryChart()
        {
            workSheet_range = xlWorkSheet.Range["A42:D42"];
            //columns heading

            xlWorkSheet.Range["A42"].Value = "Category";
            xlWorkSheet.Range["B42"].Value = "Steps Passed";
            xlWorkSheet.Range["C42"].Value = "Steps Failed";
            xlWorkSheet.Range["D42"].Value = "Steps Warning";

            //format headings
            workSheet_range.Font.Bold = true;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;
            rowNo = 42;


            var stepstestCaseList =
               from testCase in ExecutionSession.lstTestCase
               group testCase by testCase.Category into stepsTestCase
               select new
               {
                   Category = stepsTestCase.Key,
                   StepsPassed = stepsTestCase.Sum(testCase => testCase.NoOfStepsPassed),
                   StepsFailed = stepsTestCase.Sum(testCase => testCase.NoOfStepsFailed),
                   StepsWarning = stepsTestCase.Sum(testCase => testCase.NoOfWarningSteps)
               };
            foreach (var item in stepstestCaseList)
            {
                rowNo++;
                xlWorkSheet.Range["A" + rowNo.ToString()].Value = item.Category;
                xlWorkSheet.Range["B" + rowNo.ToString()].Value = item.StepsPassed;
                xlWorkSheet.Range["C" + rowNo.ToString()].Value = item.StepsFailed;
                xlWorkSheet.Range["D" + rowNo.ToString()].Value = item.StepsWarning;

                xlWorkSheet.Range["A" + rowNo.ToString()].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.Range["A" + rowNo.ToString()].WrapText = true;
                xlWorkSheet.Range["A" + rowNo.ToString()].InsertIndent(5);
            }

            workSheet_range = xlWorkSheet.Range["A42", "D" + rowNo];
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;

            workSheet_range = xlWorkSheet.Range["B43", "D" + rowNo];
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            myCharts = xlWorkSheet.ChartObjects(Type.Missing);
            myCharts1 = myCharts.Add(400, 610, 370, 210);
            oChart = myCharts1.Chart;
            chartRange = xlWorkSheet.Range["A42", "D" + rowNo];
            oChart.SetSourceData(chartRange); ;
            oChart.PlotBy = XlRowCol.xlColumns;
            oChart.ApplyDataLabels(XlDataLabelsType.xlDataLabelsShowNone);
            oChart.HasLegend = true;
            oChart.HasTitle = true;
            oChart.ChartTitle.Text = "Summary report by category";

            Excel.Axis axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlValue,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "No of steps";

            axis = (Excel.Axis)oChart.Axes(
            Excel.XlAxisType.xlCategory,
            Excel.XlAxisGroup.xlPrimary);

            axis.HasTitle = true;
            axis.AxisTitle.Text = "Category";

            string priorityChartPath = summaryChartsFolder + "\\SummaryReport_Category.jpg";
            oChart.Export(priorityChartPath, "jpg", Missing.Value);
        }

        private void SaveSummaryReport()
        {
            string summaryReportPath = HelperClass.reportRunPath + "\\SummaryReport.xlsx";

            xlWorkBook.SaveAs(summaryReportPath, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value, false, false,
            Excel.XlSaveAsAccessMode.xlShared, false, false,
            System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value);
            //xlWorkBook.upda
        }


        private void GenerateTabularReport()
        {
            //if(xlWorkBook.Sheets.Count==1)
                
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
            xlWorkSheet.Name = "Tabular Report";
            setColumnWidth();
            CreateReportHeader();
            CreateStepHeader();
            AddTestCasesResult();
            GenerateFooter();
        }

        private void setColumnWidth()
        {
            workSheet_range = xlWorkSheet.get_Range("A:A", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 50;
            workSheet_range = xlWorkSheet.get_Range("B:B", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 28;
            workSheet_range = xlWorkSheet.get_Range("C:C", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 23.29;
            workSheet_range = xlWorkSheet.get_Range("D:D", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 18;
            workSheet_range = xlWorkSheet.get_Range("E:E", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 12;
            workSheet_range = xlWorkSheet.get_Range("F:F", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 22;
            workSheet_range = xlWorkSheet.get_Range("G:G", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 22;
            workSheet_range = xlWorkSheet.get_Range("H:H", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 37;
        }

        private void CreateReportHeader()
        {
            string color = string.Empty;
            
            xlWorkSheet.Cells[2, 2] = "Automation Summary Report";
            workSheet_range = xlWorkSheet.get_Range("A2", "I2");
            workSheet_range.Merge(System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderBackgound);
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelSummHeaderFontColor);
            workSheet_range.Font.Bold = true;
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            currentRowNo++;
        }

        public void CreateStepHeader()
        {
            string tcNameRange = string.Format("C{0}:E{0}", currentRowNo);
            workSheet_range = xlWorkSheet.get_Range(tcNameRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(HelperClass.reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList tcDetailsFields = xmlReportConfig.SelectNodes("Report/SummaryReport/TestResult/var");
                xlWorkSheet.Cells[currentRowNo, 1] = tcDetailsFields.Item(0).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 2] = tcDetailsFields.Item(1).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 3] = tcDetailsFields.Item(2).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 6] = tcDetailsFields.Item(3).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 7] = tcDetailsFields.Item(4).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 8] = tcDetailsFields.Item(5).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 9] = tcDetailsFields.Item(6).Attributes["value"].Value;
            }



            string entireRowRange = string.Format("A{0}:I{0}", currentRowNo);
            workSheet_range = xlWorkSheet.get_Range(entireRowRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSubHeaderBackgound);
            workSheet_range.Font.Bold = true;
            currentRowNo++;
        }

        public void AddTestCasesResult()
        {
          //foreach (clientList ClientList in ExecutionSession.lstClient)
            //{
            foreach (ExecutedTestCase testCase in ExecutionSession.lstExecutedTestCases)
            {
                string tcNameRange = string.Format("C{0}:E{0}", currentRowNo);
                string rowRange = string.Format("A{0}:I{0}", currentRowNo);
                workSheet_range = xlWorkSheet.get_Range(tcNameRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                String[] HostName = testCase.clientUrl.Split('/');
                IPAddress[] ip_Addresses = Dns.GetHostAddresses(HostName[2].ToString());
                xlWorkSheet.Cells[currentRowNo, 1] = testCase.clientUrl + " (" + ip_Addresses[0].ToString()+ ")";
                xlWorkSheet.Cells[currentRowNo, 2] = testCaseNo;
                Range range = xlWorkSheet.get_Range("C" + currentRowNo.ToString(), System.Type.Missing);
                Hyperlink hyperlink = (Hyperlink)range.Hyperlinks.Add(range, testCase.TestCaseResultUrl,
                System.Type.Missing, System.Type.Missing, testCase.TestCaseName);
                string browser = Enum.GetName(typeof(Browser), testCase.Browser);
                if (HelperClass.runMultipleBrowsers || HelperClass.runAllBrowsers)
                    browser = "Multiple Browsers";
                xlWorkSheet.Cells[currentRowNo, 8] = browser;
                xlWorkSheet.Cells[currentRowNo, 9] = Enum.GetName(typeof(Priority), testCase.Priority);
                switch (testCase.Status)
                {
                    case OverAllResult.PASS:
                        xlWorkSheet.Cells[currentRowNo, 6] = Enum.GetName(typeof(OverAllResult), testCase.Status);
                        xlWorkSheet.get_Range("F" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
                        xlWorkSheet.get_Range("F" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Green);
                        break;
                    case OverAllResult.FAIL:
                        xlWorkSheet.Cells[currentRowNo, 6] = Enum.GetName(typeof(OverAllResult), testCase.Status);
                        Range CommentRange = xlWorkSheet.Cells[currentRowNo, 6];
                        xlWorkSheet.Cells[currentRowNo, 7] = testCase.FailedStep;
                        CommentRange.AddComment(testCase.FailedStep);
                        float val1 = 2.001f;
                        float val2 = 2.001f;
                        CommentRange.Comment.Shape.ScaleWidth(val2, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
                        CommentRange.Comment.Shape.ScaleHeight(val1, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoScaleFrom.msoScaleFromTopLeft);
                        xlWorkSheet.get_Range("F" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
                        xlWorkSheet.get_Range("F" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Red);
                      //  xlWorkSheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).WrapText = false;
                        break;
                    case OverAllResult.WARNING:
                        xlWorkSheet.Cells[currentRowNo, 6] = Enum.GetName(typeof(OverAllResult), testCase.Status);
                        xlWorkSheet.get_Range("F" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
                        xlWorkSheet.get_Range("F" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Brown);
                        break;
                }

                workSheet_range = xlWorkSheet.get_Range(rowRange, System.Type.Missing);
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workSheet_range = xlWorkSheet.get_Range(tcNameRange, System.Type.Missing);
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                testCaseNo++;
                currentRowNo++;
            }
            
          
           //}
            

        }

        public void GenerateFooter()
        {
            string execDateRange = string.Format("C{0}:E{0}", currentRowNo);
            string execMachineRange = string.Format("F{0}:G{0}", currentRowNo);
            string footerRange = string.Format("A{0}:I{0}", currentRowNo);
            workSheet_range = xlWorkSheet.get_Range(execDateRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            workSheet_range = xlWorkSheet.get_Range(execMachineRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);

            XmlDocument xmlReportConfig = new XmlDocument();
            xmlReportConfig.Load(HelperClass.reportConfigPath);
            if (!(xmlReportConfig == null))
            {
                XmlNodeList footerFields = xmlReportConfig.SelectNodes("Report/SummaryReport/ReportFooter/var");
                xlWorkSheet.Cells[currentRowNo, 1] = footerFields.Item(0).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 2] = footerFields.Item(1).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 3] = footerFields.Item(2).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 6] = footerFields.Item(3).Attributes["value"].Value;
                xlWorkSheet.Cells[currentRowNo, 8] = footerFields.Item(4).Attributes["value"].Value;
            }
            workSheet_range = xlWorkSheet.get_Range(footerRange, System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSubHeaderBackgound);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Font.Bold = true;
            testCaseNo++;
            currentRowNo++;
            AddFooterDetails();
        }

        public void AddFooterDetails()
        {
            try
            {
                TimeSpan overAllTimeTaken = new TimeSpan();
                foreach (ExecutedTestCase testCase in ExecutionSession.lstExecutedTestCases)
                {
                    overAllTimeTaken = overAllTimeTaken.Add(testCase.ExecutionTime);
                }
                string executionTime = overAllTimeTaken.Minutes + " mins and "
                     + overAllTimeTaken.Seconds + " secs";
                string execDateRange = string.Format("C{0}:E{0}", currentRowNo);
                string execMachineRange = string.Format("F{0}:G{0}", currentRowNo);
                string footerRange = string.Format("A{0}:H{0}", currentRowNo);
                workSheet_range = xlWorkSheet.get_Range(execDateRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                workSheet_range = xlWorkSheet.get_Range(execMachineRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                noOfTCPassed = ExecutionSession.lstExecutedTestCases.Where(testCase => testCase.Status == OverAllResult.PASS).ToList().Count;
                noOfTCFailed = ExecutionSession.lstExecutedTestCases.Where(testCase => testCase.Status == OverAllResult.FAIL).ToList().Count;
                xlWorkSheet.Cells[currentRowNo, 1] = noOfTCPassed;
                xlWorkSheet.Cells[currentRowNo, 2] = noOfTCFailed;
                xlWorkSheet.Cells[currentRowNo, 3] = DateTime.Now;
                xlWorkSheet.Cells[currentRowNo, 6] = Environment.MachineName;
                xlWorkSheet.Cells[currentRowNo, 8] = executionTime;
                workSheet_range = xlWorkSheet.get_Range(footerRange, System.Type.Missing);
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //  workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
                FormatReport();
                // SaveReport();
            }
            catch (Exception ex)
            {

            }
        }

        private void FormatReport()
        {
            string entireReport = string.Format("A2:I{0}", currentRowNo);
            workSheet_range = xlWorkSheet.get_Range(entireReport, System.Type.Missing);
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = false;
        }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }
}
