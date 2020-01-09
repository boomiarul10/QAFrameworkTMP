using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace TB2
{     
    public class ExcelHelper : IDisposable
    {
        private string excelObject = "Provider=Microsoft.{0}.OLEDB.{1};Data Source={2};Extended Properties=\"Excel {3};HDR=YES\"";
        private string filepath = string.Empty;
        private OleDbConnection con = null;

        public string ConnectionString
        {
            get
            {
                if (!(this.filepath == string.Empty))
                {
                    //Check for File Format
                    FileInfo fi = new FileInfo(this.filepath);
                    if (fi.Extension.Equals(".xls"))
                    {
                        return string.Format(this.excelObject, "Jet", "4.0", this.filepath, "8.0");
                    }
                    else if (fi.Extension.Equals(".xlsx"))
                    {
                        return string.Format(this.excelObject, "Ace", "12.0", this.filepath, "12.0");
                    }
                }
                else
                {
                    return string.Empty;
                }
                return string.Empty;
            }

        }

        public OleDbConnection Connection
        {
            get
            {
                if (con == null)
                {
                    OleDbConnection _con = new OleDbConnection { ConnectionString = this.ConnectionString };
                    this.con = _con;
                }
                return this.con;
            }
        }

        public System.Data.DataTable GetSchema(string excelFilePath)
        {
            this.filepath = excelFilePath;
            System.Data.DataTable dtSchema = null;
            if (this.Connection.State != ConnectionState.Open) this.Connection.Open();
            dtSchema = this.Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            return dtSchema;
        }

        public System.Data.DataTable ReadTable(string excelFilePath, string tableName)
        {
            return this.ReadTable(excelFilePath, tableName, "");
        }

        public System.Data.DataTable ReadTable(string excelFilePath, string tableName, string criteria)
        {
            this.filepath = excelFilePath;
            try
            {
                System.Data.DataTable resultTable = null;
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();

                }


                string cmdText = "Select * from [{0}]";
                if (!string.IsNullOrEmpty(criteria))
                {
                    cmdText += " Where " + criteria;
                }
                OleDbCommand cmd = new OleDbCommand(string.Format(cmdText, tableName));
                cmd.Connection = this.Connection;
                OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

                DataSet ds = new DataSet();

                adpt.Fill(ds, tableName);

                if (ds.Tables.Count == 1)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
                this.Connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public System.Data.DataTable GetTestCaseCategories()
        {
            this.filepath = HelperClass.runManagerPath;
            try
            {
                
                System.Data.DataTable resultTable = null;
                string execSheetName = HelperClass.runModuleToExecute + "$";
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();

                }

                string cmdText = "Select distinct(Category) from [{0}]";
                string criteria = @"[Execute]=""Yes""";
                if (!string.IsNullOrEmpty(criteria))
                {
                    cmdText += " Where " + criteria;
                }


                OleDbCommand cmd = new OleDbCommand(string.Format(cmdText, execSheetName));
                cmd.Connection = this.Connection;
                OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

                DataSet ds = new DataSet();

                adpt.Fill(ds, execSheetName);

                if (ds.Tables.Count == 1)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public System.Data.DataTable GetTestCasePriority()
        {
            this.filepath = HelperClass.runManagerPath;
            try
            {

                System.Data.DataTable resultTable = null;
                string execSheetName = HelperClass.runModuleToExecute + "$";
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();

                }

                string cmdText = "Select distinct(Priority) from [{0}]";
                string criteria = @"[Execute]=""Yes""";
                if (!string.IsNullOrEmpty(criteria))
                {
                    cmdText += " Where " + criteria;
                }


                OleDbCommand cmd = new OleDbCommand(string.Format(cmdText, execSheetName));
                cmd.Connection = this.Connection;
                OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

                DataSet ds = new DataSet();

                adpt.Fill(ds, execSheetName);

                if (ds.Tables.Count == 1)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int GetIterationCount(string excelFilePath, string tableName, string criteria)
        {
            this.filepath = excelFilePath;
            try
            {

                System.Data.DataTable resultTable = null;
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();

                }

                string cmdText = "Select max(Iteration) from [{0}]";
                if (!string.IsNullOrEmpty(criteria))
                {
                    cmdText += " Where " + criteria;
                }


                OleDbCommand cmd = new OleDbCommand(string.Format(cmdText, tableName));
                cmd.Connection = this.Connection;
                OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);

                DataSet ds = new DataSet();

                adpt.Fill(ds, tableName);
                int returnValue;
                if (ds.Tables[0].Rows.Count == 1)
                {
                    try
                    {
                        returnValue = Int32.Parse(Convert.ToString(ds.Tables[0].Rows[0][0]));  
                    }  
                    catch  
                    {
                        returnValue = -1;  
                    }

                    return returnValue;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

        }

        public static void AddFailedTCToExcel()
        {
            Application app = null;
            Workbook workbook = null;
            Worksheet worksheet = null;
            try{
            
            app = new Microsoft.Office.Interop.Excel.Application();
                workbook= app.Workbooks.Open(@"C:\Projects\AutomationFramework\LastRun_FailedTC.xls");
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                worksheet.UsedRange.ClearContents();
                worksheet.Cells[1, 1] = "Test Case Name";
                worksheet.Cells[1, 2] = "Execute";
                worksheet.Cells[1, 3] = "Browser";
                worksheet.Cells[1, 4] = "Category";
                worksheet.Cells[1, 5] = "Priority";
                worksheet.Cells[1, 6] = "RunIterations";

                List<TestCase> lstFailedTC=ExecutionSession.lstTestCase.Where(testCase=>testCase.OverAllResult==OverAllResult.FAIL).ToList();
                int rowCnt = 2;
                foreach (TestCase testCase in lstFailedTC)
                {
                    worksheet.Cells[rowCnt, 1] = testCase.TestCaseName;
                    worksheet.Cells[rowCnt, 2] = "Yes";
                    worksheet.Cells[rowCnt, 3] = Enum.GetName(typeof(Browser), testCase.Browser);
                    worksheet.Cells[rowCnt, 4] = testCase.Category;
                    worksheet.Cells[rowCnt, 5] = Enum.GetName(typeof(Priority), testCase.Priority);
                    if(testCase.RunIterations)
                        worksheet.Cells[rowCnt, 6] = "Yes";
                    else
                        worksheet.Cells[rowCnt, 6] = "No";
                    rowCnt++;
                }
                workbook.Save();
            }
            catch(Exception ex)
            {
                Logger.Log(ex.ToString());
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

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);   

        public void Dispose()
        {
            if (this.con != null && this.con.State == ConnectionState.Open)
                this.con.Close();
            if (this.con != null)
                this.con.Dispose();
            this.con = null;
            this.filepath = string.Empty;
        }
    }
}
