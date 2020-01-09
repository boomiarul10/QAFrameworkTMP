using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing;
namespace TB2
{
    public class ExcelReport
    {
        Application app = null;
        Workbook workbook = null;
        Worksheet worksheet = null;
        Excel.Range workSheet_range = null;
        int stepNo = 1;
        int currentRowNo = 6;
        int noOfStepsPassed;
        int noOfStepsFailed;
        int overAllStepsPassed;
        int overAllStepsFailed;
        public string testReportFolder { get; set; }
        public string screenShotFolder { get; set; }
        public TestCase testCase { get; set; }
        public int CurrentIteration { get; set; }
        public int TotalIterationCount { get; set; }
        public string BrowserDetails { get; set; }

        public ExcelReport()
        {
            CreateReort();
            GenerateHeader();
        }

        public ExcelReport(string reportPath, string screenShotPath, string browserDetails, TestCase currentTestCase)
        {
            testReportFolder = reportPath;
            screenShotFolder = screenShotPath;
            BrowserDetails = browserDetails;
            testCase = currentTestCase;
            CreateReort();
        }

        public void CreateReort()
        {
            try
            {
                app = new Excel.Application();
                workbook = app.Workbooks.Add(1);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
            }
            catch (Exception e)
            {
                Console.Write("Error");
            }
            finally
            {

            }
        }

        public void GenerateHeader()
        {
            CreateReportHeader();
            CreateSubHeader();
            AddSubHeaderValues();
            if (TotalIterationCount > 1)
            {
                string tcNameRange = string.Format("A{0}:G{0}", currentRowNo);
                worksheet.Cells[currentRowNo, 2] = "Iteration - " + Convert.ToString((CurrentIteration + 1));
                workSheet_range = worksheet.get_Range(tcNameRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                workSheet_range.Font.Bold = true;
                workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderBackgound);
                workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderFontColor);
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                currentRowNo++;
                CurrentIteration++;
            }
            CreateStepHeader();
        }

        private void CreateReportHeader()
        {
            string color = string.Empty;
            color = "BLACK";
            currentRowNo = 2;
            worksheet.Cells[2, 2] = HelperClass.excelRptHeader;
            workSheet_range = worksheet.get_Range("A2", "G2");
            workSheet_range.Merge(System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderBackgound);
            workSheet_range.Font.Bold = true;
            workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderFontColor);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            currentRowNo++;
        }

        public void CreateSubHeader()
        {
            string tcNameRange = string.Format("A{0}:B{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(tcNameRange, System.Type.Missing);

            workSheet_range.Merge(System.Type.Missing);

            workSheet_range = worksheet.get_Range("A:A", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 18;
            workSheet_range = worksheet.get_Range("B:B", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 28;
            workSheet_range = worksheet.get_Range("C:C", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 23.29;
            workSheet_range = worksheet.get_Range("D:D", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 18;
            workSheet_range = worksheet.get_Range("E:E", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 12;
            workSheet_range = worksheet.get_Range("F:F", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 22;
            workSheet_range = worksheet.get_Range("G:G", System.Type.Missing);
            workSheet_range.EntireColumn.ColumnWidth = 37;
            worksheet.Cells[3, 1] = HelperClass.excelTCName;
            worksheet.Cells[3, 3] = HelperClass.excelEnvironment;
            worksheet.Cells[3, 4] = HelperClass.excelHostName;
            worksheet.Cells[3, 5] = HelperClass.excelBuildVersion;
            worksheet.Cells[3, 6] = HelperClass.excelBrowser;
            worksheet.Cells[3, 7] = HelperClass.excelUrl;

            string entireRow = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(entireRow, System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSubHeaderBackgound);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Font.Bold = true;
            currentRowNo++;
        }

        public void AddSubHeaderValues()
        {
            string tcNameRange = string.Format("A{0}:B{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(tcNameRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);

            worksheet.Cells[currentRowNo, 1] = testCase.TestCaseName;
            worksheet.Cells[currentRowNo, 3] = ExecutionSession.dictCommonData["Environment"];
            worksheet.Cells[currentRowNo, 4] = "";
            worksheet.Cells[currentRowNo, 5] = HelperClass.runBuildVersion;
            worksheet.Cells[currentRowNo, 6] = BrowserDetails;
            worksheet.Cells[currentRowNo, 7] = ExecutionSession.dictCommonData["EnvironmentUrl"];
            string entireRow = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(entireRow, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            currentRowNo++;
        }


        public void CreateStepHeader()
        {
            string stepsRange = string.Format("B{0}:C{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(stepsRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            string descRange = string.Format("D{0}:F{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(descRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            worksheet.Cells[currentRowNo, 1] = HelperClass.excelSteps;
            worksheet.Cells[currentRowNo, 2] = HelperClass.excelDescription;
            worksheet.Cells[currentRowNo, 4] = HelperClass.excelActualResult;
            worksheet.Cells[currentRowNo, 7] = HelperClass.excelStepStatus;
            string entireRow = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(entireRow, System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSubHeaderBackgound);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Font.Bold = true;
            currentRowNo++;
        }

        //Venky added this method
        public void CreateScenarioHeader(string ScenarioName)
        {
            string stepsRange = string.Format("B{0}:C{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(stepsRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            string descRange = string.Format("D{0}:F{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(descRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            //worksheet.Cells[currentRowNo, 1] = ScenarioName;
            worksheet.Cells[currentRowNo, 2] = ScenarioName;
            worksheet.Cells[currentRowNo, 4] = ScenarioName;
           // worksheet.Cells[currentRowNo, 7] = ScenarioName;
            string entireRow = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(entireRow, System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSubHeaderBackgound);            
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.Font.Bold = true;
            currentRowNo++;
        }

        public void AddReportStep(string description, string actualResult, StepResult stepResult, string screenShotFilePath)
        {
            string descRange = string.Format("B{0}:C{0}", currentRowNo);
            string actualResultRange = string.Format("D{0}:F{0}", currentRowNo);
            string rowRange = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(descRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            workSheet_range = worksheet.get_Range(actualResultRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            worksheet.Cells[currentRowNo, 1] = stepNo;
            worksheet.Cells[currentRowNo, 2] = description;
            worksheet.Cells[currentRowNo, 4] = actualResult;

            switch (stepResult)
            {
                case StepResult.PASS:
                    noOfStepsPassed++;
                    overAllStepsPassed++;
                    worksheet.Cells[currentRowNo, 7] = Enum.GetName(typeof(StepResult), stepResult);
                    worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
                    worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Green);
                    break;
                case StepResult.FAIL:
                    noOfStepsFailed++;
                    overAllStepsFailed++;
                    testCase.ErrorStepNo = stepNo;
                    testCase.ErrorStep = testCase.ErrorStep + stepNo + ". " + description + "\r\n";
                    Range range = worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing);
                    Hyperlink hyperlink = (Hyperlink)range.Hyperlinks.Add(range, screenShotFilePath,
                    System.Type.Missing, System.Type.Missing, Enum.GetName(typeof(StepResult), stepResult));
                    worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
                    worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Red);
                    break;
                case StepResult.WARNING:
                    worksheet.Cells[currentRowNo, 7] = Enum.GetName(typeof(StepResult), stepResult);
                    worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
                    worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Brown);
                    break;
            }

            workSheet_range = worksheet.get_Range(rowRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;
            workSheet_range.Rows.AutoFit();
            workSheet_range = worksheet.get_Range(descRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            workSheet_range.InsertIndent(5);
            workSheet_range = worksheet.get_Range(actualResultRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            workSheet_range.InsertIndent(5);

            stepNo++;
            currentRowNo++;
        }

        public void AddErrorStep()
        {
            string descRange = string.Format("B{0}:C{0}", currentRowNo);
            string actualResultRange = string.Format("D{0}:F{0}", currentRowNo);
            string rowRange = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(descRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            workSheet_range = worksheet.get_Range(actualResultRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            worksheet.Cells[currentRowNo, 1] = stepNo;
            worksheet.Cells[currentRowNo, 2] = "Error";
            worksheet.Cells[currentRowNo, 4] = "Error occured during this step.Please look into error log for more details." +
                Environment.NewLine + "Error Log Path : " + HelperClass.tcErrorLogPath;
            noOfStepsFailed++;
            overAllStepsFailed++;
            testCase.ErrorStepNo = stepNo;
            testCase.ErrorStep = "Error occured during this step.Please look into error log for more details." +
                Environment.NewLine + "Error Log Path : " + HelperClass.tcErrorLogPath;
            worksheet.Cells[currentRowNo, 7] = Enum.GetName(typeof(StepResult), StepResult.FAIL);
            worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Bold = true;
            worksheet.get_Range("G" + currentRowNo.ToString(), System.Type.Missing).Font.Color = ColorTranslator.ToOle(System.Drawing.Color.Red);
            workSheet_range = worksheet.get_Range(rowRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            workSheet_range.WrapText = true;
            workSheet_range.Rows.AutoFit();
            workSheet_range = worksheet.get_Range(descRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            workSheet_range.InsertIndent(5);
            workSheet_range = worksheet.get_Range(actualResultRange, System.Type.Missing);
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            workSheet_range.InsertIndent(5);

            stepNo++;
            currentRowNo++;
        }


        public void GenerateFooter(string executionTime)
        {
            string execDateRange = string.Format("C{0}:D{0}", currentRowNo);
            string execMachineRange = string.Format("E{0}:F{0}", currentRowNo);
            string footerRange = string.Format("A{0}:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(execDateRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            workSheet_range = worksheet.get_Range(execMachineRange, System.Type.Missing);
            workSheet_range.Merge(System.Type.Missing);
            worksheet.Cells[currentRowNo, 1] = HelperClass.excelStepsPassed;
            worksheet.Cells[currentRowNo, 2] = HelperClass.excelStepsFailed;
            worksheet.Cells[currentRowNo, 3] = HelperClass.excelExecutionDate;
            worksheet.Cells[currentRowNo, 5] = HelperClass.excelExecutionMachine;
            worksheet.Cells[currentRowNo, 7] = HelperClass.excelTimeTaken;
            workSheet_range = worksheet.get_Range(footerRange, System.Type.Missing);
            workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelSubHeaderBackgound);
            workSheet_range.Font.Bold = true;
            workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            stepNo++;
            currentRowNo++;
            addFooterDetails(executionTime);
        }

        public void addFooterDetails(string executionTime)
        {
            try
            {
                string execDateRange = string.Format("C{0}:D{0}", currentRowNo);
                string execMachineRange = string.Format("E{0}:F{0}", currentRowNo);
                string footerRange = string.Format("A{0}:G{0}", currentRowNo);
                workSheet_range = worksheet.get_Range(execDateRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                workSheet_range = worksheet.get_Range(execMachineRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                worksheet.Cells[currentRowNo, 1] = overAllStepsPassed;
                worksheet.Cells[currentRowNo, 2] = overAllStepsFailed;
                worksheet.Cells[currentRowNo, 3] = DateTime.Now;
                worksheet.Cells[currentRowNo, 5] = Environment.MachineName;
                worksheet.Cells[currentRowNo, 7] = executionTime;
                workSheet_range = worksheet.get_Range(footerRange, System.Type.Missing);
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                FormatReport();
                SaveReport();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                GC.Collect();

                GC.WaitForPendingFinalizers();
                if (app != null)
                {
                    app.Quit();
                    int hWnd = app.Application.Hwnd;
                    uint processID;
                    GetWindowThreadProcessId((IntPtr)hWnd, out processID);
                    Process[] procs = Process.GetProcessesByName("EXCEL");
                    foreach (Process p in procs)
                    {
                        if (p.Id == processID)
                            p.Kill();
                    }
                    Marshal.FinalReleaseComObject(app);
                }
            }
        }

        public void AddNewIteration()
        {
            if (CurrentIteration != 1)
            {
                stepNo = 1;
                noOfStepsPassed = 0;
                noOfStepsFailed = 0;
                string tcNameRange = string.Format("A{0}:G{0}", currentRowNo);
                worksheet.Cells[currentRowNo, 2] = "Iteration - " + Convert.ToString((CurrentIteration));
                workSheet_range = worksheet.get_Range(tcNameRange, System.Type.Missing);
                workSheet_range.Merge(System.Type.Missing);
                workSheet_range.Font.Bold = true;
                workSheet_range.Interior.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderBackgound);
                workSheet_range.Font.Color = ColorTranslator.FromHtml(HelperClass.excelHeaderFontColor);
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                currentRowNo++;
                CurrentIteration++;
                CreateStepHeader();
            }
        }


        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private void FormatReport()
        {
            string entireReport = string.Format("A2:G{0}", currentRowNo);
            workSheet_range = worksheet.get_Range(entireReport, System.Type.Missing);
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.WrapText = true;
            workSheet_range.Rows.AutoFit();
            //workSheet_range.AutoFit();
        }

        private void SaveReport()
        {
            string formattedDate = string.Format("_{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            string excelReportPath = testReportFolder + "\\" + testCase.TestCaseName + formattedDate + ".xlsx";
            string excelReportLinkPath = excelReportPath.Replace(HelperClass.reportRunPath, ".");
            testCase.ExcelReportPath = excelReportLinkPath;
            workbook.SaveAs(excelReportPath, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value, false, false,
            Excel.XlSaveAsAccessMode.xlShared, false, false,
            System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value);
        }
    }
}
