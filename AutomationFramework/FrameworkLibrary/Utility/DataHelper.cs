using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace TB2
{
    public abstract class DataHelper
    {
        public TestCase currentTestCase { get; set; }
        public abstract string GetData(string keyword);
        public abstract void SetToCurrentIteration(int currentIteration);
        public static void SetCommonData()
        {
            if (HelperClass.runDataSource.ToLower().Trim() == "excel")
                ExcelDataHelper.SetExcelCommonData();
            else
                XMLDataHelper.SetXMLCommonData();
        }

        public string GetCommonData(string keyword)
        {
            if (ExecutionSession.dictCommonData.ContainsKey(keyword))
                return ExecutionSession.dictCommonData[keyword];
            else
                return string.Empty;
        }
    }

    public class ExcelDataHelper : DataHelper
    {
        DataTable dtExcelData;
        ExcelHelper excelhelper;
        List<string> lstKeywords;
        string excelDataFile;
        string testCaseName;

        public ExcelDataHelper()
        {
            excelhelper = new ExcelHelper();
            currentTestCase = new TestCase();
            lstKeywords = new List<string>();
            string testCaseName = "\"" + currentTestCase.TestCaseName + "\"";
            excelDataFile = HelperClass.excelDataPath + "\\" + HelperClass.runModuleToExecute + ".xls";
            dtExcelData = excelhelper.ReadTable(excelDataFile, "DataSheet$", @"[TestCaseName]=" + testCaseName + " and [Iteration]=1");
        }

        public ExcelDataHelper(TestCase testCase)
        {
            excelhelper = new ExcelHelper();
            currentTestCase = currentTestCase = testCase;
            lstKeywords = new List<string>();
            testCaseName = "\"" + currentTestCase.TestCaseName + "\"";
            excelDataFile = HelperClass.excelDataPath + "\\" + HelperClass.runModuleToExecute + ".xls";
            dtExcelData = excelhelper.ReadTable(excelDataFile, "DataSheet$", @"[TestCaseName]=" + testCaseName);
        }

        public override string GetData(string keyword)
        {
            if (dtExcelData.Rows.Count > 0)
                return Convert.ToString(dtExcelData.Rows[0][keyword]);
            else
                return string.Empty;
        }
        
        public override void SetToCurrentIteration(int currentIteration)
        {
            lstKeywords.Clear();            
            dtExcelData = excelhelper.ReadTable(excelDataFile, "DataSheet$", @"[TestCaseName]=" + testCaseName + " and [Iteration]="+currentIteration+"");
        }

        public static int GetIterationCount(TestCase currentTestCase)
        {
            ExcelHelper excelhelper = new ExcelHelper();
            string excelDataFile = HelperClass.excelDataPath + "\\" + HelperClass.runModuleToExecute + ".xls";
            string testCaseName = "\"" + currentTestCase.TestCaseName + "\"";
            int count = excelhelper.GetIterationCount(excelDataFile, "DataSheet$", @"[TestCaseName]=" + testCaseName + "");
            return count;
        }

        public static void SetExcelCommonData()
        {
            ExcelHelper excelhelper = new ExcelHelper();
            DataTable dtCommonData;
            DataTable dtReferenceData;
            string excelCommonDataFile = HelperClass.excelDataPath + "\\CommonData.xls";
            dtCommonData = excelhelper.ReadTable(excelCommonDataFile, "Data$", "");
            ExecutionSession.dictCommonData=new Dictionary<string,string>();
            foreach (DataRow  dRow in dtCommonData.Rows)
            {
                ExecutionSession.dictCommonData.Add(Convert.ToString(dRow["Key"]),Convert.ToString( dRow["Value"]));
            }
            string environmentUrl = ExecutionSession.dictCommonData["Environment"];
               // HelperClass.runEnvironment;

            try
            {
                if (!String.IsNullOrEmpty(environmentUrl))
                {

                    //  environmentUrl = "\"" + environmentUrl + "\"";
                    dtReferenceData = excelhelper.ReadTable(excelCommonDataFile, environmentUrl + "$");
                    if (dtCommonData.Rows.Count > 0)
                    {
                        ExecutionSession.dictCommonData.Add("EnvironmentUrl", Convert.ToString(dtReferenceData.Rows[0]["Value"]));
                    }
                }


                else
                {
                    Logger.Log("No Environment in Mentioned");
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Invalid Environment Name :" + environmentUrl);
                Environment.Exit(0);
            }
        }
    }

    public class XMLDataHelper : DataHelper
    {
        XmlDocument doc;
        XmlNode node;
        List<string> lstKeyword;
       // int subIteration;

        public XMLDataHelper()
        {

        }
        
        public XMLDataHelper(TestCase testCase)
        {
            currentTestCase = testCase;
            doc = new XmlDocument();
            lstKeyword = new List<string>();
            string xmlDataFile = HelperClass.xmlDataPath + "\\" + HelperClass.runModuleToExecute + ".xml";
            if (File.Exists(xmlDataFile))
            {
                doc.Load(xmlDataFile);
                node = doc.SelectSingleNode("TestCases/TestCase[@name='" + currentTestCase.TestCaseName + "']/Iteration");
            }
        }

        public override string GetData(string keyword)
        {
            XmlNode currentNode;
            if (node != null)
            {
                currentNode = node.SelectSingleNode(keyword);
                if (currentNode != null)
                    return currentNode.InnerText.Trim();
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
            
        }

        public static int GetIterationCount(TestCase currentTestCase)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string xmlDataFile = HelperClass.xmlDataPath + "\\" + HelperClass.runModuleToExecute + ".xml";
            if (File.Exists(xmlDataFile))
            {
                xmlDoc.Load(xmlDataFile);
                string testCaseName = currentTestCase.TestCaseName;
                int iterationCount = xmlDoc.SelectNodes("TestCases//TestCase[@name='" + currentTestCase.TestCaseName + "']//Iteration").Count;
                return iterationCount;
            }
            else
                return -1;
        }

        public override void SetToCurrentIteration(int currentIteration)
        {
            node = doc.SelectSingleNode("TestCases/TestCase[@name='" + currentTestCase.TestCaseName + "']/Iteration[" + currentIteration + "]");
            lstKeyword.Clear();
        }

        public static void SetXMLCommonData()
        {
            XmlDocument xmlCommonData = new XmlDocument();
            string xmlCommonDataFile = HelperClass.xmlDataPath + "\\CommonData.xml";
            xmlCommonData.Load(xmlCommonDataFile);
            if (!(xmlCommonData == null))
            {
                XmlNodeList commonDataFields = xmlCommonData.SelectNodes("Data/CommonData/var");
                ExecutionSession.dictCommonData = new Dictionary<string, string>();
                foreach (XmlNode commonData in commonDataFields)
                {
                    ExecutionSession.dictCommonData.Add(commonData.Attributes["key"].Value, commonData.Attributes["value"].Value);
                }

                XmlNode environmentNode = xmlCommonData.
                    SelectSingleNode("Data/ReferenceData/EnvironmentUrl/var[@key='" + ExecutionSession.dictCommonData["Environment"] + "']");
                ExecutionSession.dictCommonData.Add("EnvironmentUrl", environmentNode.Attributes["value"].Value);
            }
        }
    }
}
 