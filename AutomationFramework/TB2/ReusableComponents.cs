using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TB2.UIStore;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using System.Text.RegularExpressions;
using TB2.DAO;
using System.Threading;


namespace TB2
{
    public class ReusableComponents
    {
        Report report;
        RemoteWebDriver driver;
        WebDriverHelper wdHelper;
        DataHelper dataHelper;
        public Dictionary<string, string> dictStore = new Dictionary<string, string>();
        public ReusableComponents(RemoteWebDriver driver, Report report, WebDriverHelper wdHelper, DataHelper dataHelper)
        {
            this.report = report;
            this.driver = driver;
            this.dataHelper = dataHelper;
            this.wdHelper = wdHelper;
        }

        public string getDataCommon(string keyword)
        {
            string currentUrl = dataHelper.GetCommonData("EnvironmentUrl");
            string key = string.Empty;
            foreach (var item in ExecutionSession.lstClient)
            {
                if (item.clientUrl == currentUrl)
                {
                    switch (keyword)
                    {
                        case "Category":
                            key = item.Category;
                            break;

                        case "Location":
                            key = item.Location;
                            break;

                        case "Group":
                            key = item.Group;
                            break;
                        case "AdvancedSearch":
                            key = item.AdvancedSearch;
                            break;
                        case "BasicSearch":
                            key = item.BasicSearch;
                            break;
                        case "FilterModule":
                            key = item.FilterModule;
                            break;
                        case "RecentJobs":
                            key = item.RecentJobs;
                            break;
                        case "SocialMedia":
                            key = item.SocialMedia;
                            break;
                        case "JobAlert":
                            key = item.JobAlert;
                            break;
                        case "SiteMap":
                            key = item.SiteMap;
                            break;
                        case "RSSFeed":
                            key = item.RSSFeed;
                            break;
                        case "MeetUs":
                            key = item.MeetUs;
                            break;
                        case "JobMatching":
                            key = item.JobMatching;
                            break;

                        default:
                            key = string.Empty;
                            break;
                    }
                }
            }
            return key;
        }

        public bool com_NewLaunchUrl(String URL)
        {
            bool launch = false;
            try
            {
                //driver.Manage().Cookies.DeleteAllCookies();
                if (driver.Url == URL)
                {
                    if (com_IsElementPresent(By.XPath("//div[@id='gdpr-alert']/button")))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//div[@id='gdpr-alert']/button"), "Clicked on Accept for cookies", "Problem in clicking on accept button for cookies");
                        Thread.Sleep(3000);
                    }
                    //Ramya Added Latest
                    if (driver.Url.Contains("jobs.dell.com"))
                    {
                        if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//*[contains(@class,'toggle')]"), "Clicked on Search Jobs button", "Problem in clicking on Search Jobs button");
                        }
                    }
                    //Ramya Added Latest
                    else if (driver.Url.Contains("jobs.cooperhealth.org"))
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                        if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                        {
                            com_ClickOnInvisibleElement(By.Id("search-anchor"), "Clicked on External Job Search button", "Problem in clicking on External Job Search button");
                        }
                    }
                    //Mani changed

                    else if (URL.Contains("jobs.deluxe.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//*[@class='toggle-button']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//*[@class='toggle-button']"), "Clicked on drop down", "Problem in clicking drop down");
                            Thread.Sleep(2000);
                        }
                    }
                    //Ramya Added Latest
                    else if (URL.Contains("campus.capitalone.com"))
                    {
                        if (!com_IsElementPresent(By.XPath("//div[contains(@class, 'advanced-search-form-fields')]")))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on Search Jobs button", "Problem in clicking on Search Jobs button");
                        } Thread.Sleep(2000);
                    }
                    //Mani added
                    if (driver.Url.Contains("explore.lockheedmartinjobs.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//*[@id='cookie-bar']//a[text()='Accept']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//div[@id='cookie-bar']//a[contains(text(),'Accept')]"), "Clicked on Accept button", "Problem in clicking accept button");
                            Thread.Sleep(2000);
                        }
                    }
                    //Mani added--Have to check for other functionalities
                    if (driver.Url.Contains("internal.santanderjobsus.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//div[@id='cookie-bar']//a[contains(text(),'Accept')]")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//div[@id='cookie-bar']//a[contains(text(),'Accept')]"), "Clicked on Accept button", "Problem in clicking accept button");
                            Thread.Sleep(2000);
                        }
                        if (com_IsElementPresent(By.XPath("//a[@data-custom-label='Santander Bank Employee']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//a[@data-custom-label='Santander Bank Employee']"), "Clicked on option", "Problem in clicking the option");
                            Thread.Sleep(2000);
                        }
                    }
                    launch = true;
                }
                else
                {
                    driver.Navigate().GoToUrl(URL);
                    launch = true;
                    report.AddReportStep("Launch the URL - " + URL, "URL has been launched sucessfully - " + URL, StepResult.PASS);
                    //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));

                    if (com_IsElementPresent(By.XPath("//div[@id='gdpr-alert']/button")))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//div[@id='gdpr-alert']/button"), "Clicked on Accept for cookies", "Problem in clicking on accept button for cookies");
                        Thread.Sleep(3000);
                    }
                    //Ramya Added Latest
                    if (driver.Url.Contains("jobs.cooperhealth.org"))
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                        if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                        {
                            com_ClickOnInvisibleElement(By.Id("search-anchor"), "Clicked on External Job Search button", "Problem in clicking on External Job Search button");
                        }
                    }
                    else if (driver.Url.Contains("jobs.deluxe.com"))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//*[contains(@class,'toggle-button')]"), "Clicked on drop down", "Problem in clicking drop down");
                        Thread.Sleep(2000);
                    }
                    //Ramya Added Latest
                    else if (driver.Url.Contains("jobs.dell.com"))
                    {
                        if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//*[contains(@class,'toggle')]"), "Clicked on Search Jobs button", "Problem in clicking on Search Jobs button");
                        }
                    }
                    //Ramya Added Latest
                    else if (driver.Url.Contains("campus.capitalone.com"))
                    {
                        if (!com_IsElementPresent(By.XPath("//div[contains(@class, 'advanced-search-form-fields')]")))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on Search Jobs button", "Problem in clicking on Search Jobs button");
                        }
                        Thread.Sleep(2000);
                    }

                    //if(driver.Url.Contains("it.carriere.primark.com"))
                    //{
                    //   com_ClickOnInvisibleElement(TalentBrewUI.DropDownPrimark, "Clicked on Drop down", "Problem in clicking drop down");
                    //}

                    //if (URL.Contains("www.nespressojobs.com"))
                    //{
                    //    com_ClickOnInvisibleElement(By.XPath("www.nespressojobs.com"), "Clicked on Advanced search button", "Problem in clicking advanced search button");
                    //}

                    //Mani added
                    if (driver.Url.Contains("explore.lockheedmartinjobs.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//*[@id='cookie-bar']//a[text()='Accept']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//div[@id='cookie-bar']//a[contains(text(),'Accept')]"), "Clicked on Accept button", "Problem in clicking accept button");
                            Thread.Sleep(2000);
                        }
                    }
                    //Mani added--Have to check for other functionalities
                    if (driver.Url.Contains("internal.santanderjobsus.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//div[@id='cookie-bar']//a[contains(text(),'Accept')]")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//div[@id='cookie-bar']//a[contains(text(),'Accept')]"), "Clicked on Accept button", "Problem in clicking accept button");
                            Thread.Sleep(2000);
                        }
                        if (com_IsElementPresent(By.XPath("//a[@data-custom-label='Santander Bank Employee']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//a[@data-custom-label='Santander Bank Employee']"), "Clicked on option", "Problem in clicking the option");
                            Thread.Sleep(2000);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Launch the URL - " + URL, "URL has not launched sucessfully - " + URL, StepResult.FAIL);
            }
            return launch;
        }

        //new method--Mani-31/10
        public bool tillEnabled(By elementBy,int iterator)
        {
            bool flag = false;
                for (int i = 0; i < iterator; i++)
                {
                    if (driver.FindElement(elementBy).Enabled)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                return flag;

                if (!flag)
                    report.AddReportStep("Verify Element - " +elementBy+ " is enabled even after "+iterator, "Element - " +elementBy+ " is not enabled even after "+iterator, StepResult.FAIL);
        }

        public bool com_LaunchUrl(String URL)
        {
            bool launch = false;
            try
            {
                //driver.Manage().Cookies.DeleteAllCookies();
                driver.Navigate().GoToUrl(URL);
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                launch = true;
                report.AddReportStep("Launch the URL - " + URL, "URL has been launched sucessfully - " + URL, StepResult.PASS);
                if (URL.Contains("jobs.deluxe.com"))
                {
                    com_ClickOnInvisibleElement(By.XPath("//*[contains(@class,'toggle-button')]"), "Clicked on drop down", "Problem in clicking drop down");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Launch the URL - " + URL, "URL has not launched sucessfully - " + URL, StepResult.FAIL);
            }
            return launch;
        }

        public bool com_closewindow(String basewindow)
        {
            bool temp = false;
            try
            {
                int windowCount = 0;
                ICollection<string> windowids = null;
                windowids = driver.WindowHandles;
                windowCount = windowids.Count();

                if (windowCount > 1)
                {
                    //report.AddReportStep("New Window is not closed.", "New window is not closed.", StepResult.FAIL);

                    foreach (String item in windowids)
                    {
                        Console.Write(item);

                        if (item != basewindow)
                        {
                            driver.SwitchTo().Window(item);
                            driver.Close();
                            temp = true;
                        }
                    }
                }
                else
                {
                    report.AddReportStep("New Window is closed successfully.", "New window is closed successfully.", StepResult.PASS);
                    temp = true;
                }


            }
            catch (Exception e)
            {
                report.AddReportStep("New Window is not closed.", "New window is not closed.", StepResult.FAIL);
            }

            driver.SwitchTo().Window(basewindow);

            return temp; ;
        }

        public bool com_moveToTab(string expectedTabTitle, String basewindow)
        {
            bool temp = false;
            String childWindow1 = "";
            String childWindow1Title = "";
            try
            {
                //string baseWindow = driver.CurrentWindowHandle;
                ICollection<string> windowids = null;
                windowids = driver.WindowHandles;
                foreach (String item in windowids)
                {
                    Console.Write(item);

                    if (item != basewindow && item.Contains(""))
                    {
                        childWindow1 = item;
                    }
                }

                driver.SwitchTo().Window(childWindow1);

                //driver.Manage().Cookies.DeleteAllCookies();

                for (int x = 1; x <= 10; x++)
                {
                    if (driver.Title.Equals(""))
                    {
                        System.Threading.Thread.Sleep(3000);
                    }
                    else
                    {
                        childWindow1Title = driver.Title;
                        break;
                    }

                }


                if (childWindow1Title.Contains(expectedTabTitle))
                {
                    report.AddReportStep(expectedTabTitle + "is opened in New Window", expectedTabTitle + "is opened in New Window", StepResult.PASS);
                    temp = true;
                }
                else
                {
                    report.AddReportStep(expectedTabTitle + "is not opened.", expectedTabTitle + ":is not opened.", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(expectedTabTitle + "is not opened.", expectedTabTitle + "is not opened.", StepResult.FAIL);
            }

            return temp;
        }

        //public void SearchKeyword(By element)
        //{
        //    string keyword = dataHelper.GetData(DataColumn.Keyword);

        //   // wdHelper.SendKeys(element, keyword);
        //    wdHelper.ClickElement(By.Id("searchButton"));

        //    if (wdHelper.IsElementPresent(By.Id("hd_plp")))
        //    {
        //        report.AddReportStep("Search for hammer",
        //            " '" + keyword + "' search page is displayed",
        //            StepResult.PASS);
        //    }
        //    else
        //    {
        //        report.AddReportStep("Search for hammer",
        //            " '" + keyword + "' search page is displayed",
        //            StepResult.FAIL);
        //    }
        //}  



        //WebDriver Helper Methods

        public bool com_VerifyObjPresent(By elementBy, String PassMsg, String FailMsg)
        {
            bool verifyObj = false;
            try
            {

                if (com_IsElementPresent(elementBy))
                {
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    verifyObj = true;
                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
            }
            return verifyObj;
        }

        public bool com_VerifyOptionalObjPresent(By elementBy, String PassMsg, String FailMsg)
        {
            bool verifyObj = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    verifyObj = true;
                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.WARNING);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.WARNING);
            }
            return verifyObj;
        }

        public bool com_VerifyObjNOTPresent(By elementBy, String PassMsg, String FailMsg)
        {
            bool verifyNOTObj = false;
            try
            {

                if (!com_IsElementPresent(elementBy))
                {
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    verifyNOTObj = true;
                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
            }
            return verifyNOTObj;
        }

        public bool com_Compare(By elementBy, string CompareValue, String PassMsg, String FailMsg)
        {
            bool Compare = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {

                    if (driver.FindElement(elementBy).Text.Equals(CompareValue))
                    {
                        Compare = true;
                        report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    }
                    else
                    {
                        report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                    }

                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);

            }
            return Compare;
        }

        public bool com_IsTextPresent(string verifyText)
        {
            bool isTextPresent = false;
            isTextPresent = com_GetText(By.CssSelector("BODY")).Contains(verifyText);
            return isTextPresent;
        }

        //public bool com_ClickOnInvisibleElement(By elementBy, String PassMsg, String FailMsg)
        //{
        //    bool clickElement = false;
        //    //bool exceptionOccured = false;
        //    try
        //    {
        //        if (com_IsElementPresent(elementBy))
        //        {
        //            driver.FindElement(elementBy).Click();
        //            clickElement = true;
        //            report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
        //        }
        //        else
        //        {
        //            report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
        //    }

        //    return clickElement;
        //}

        public bool com_ContainsTxt(By elementBy, string CompareValue, String PassMsg, String FailMsg)
        {
            bool ContainsTxt = false;

            try
            {
                if (com_IsElementPresent(elementBy))
                {

                    if (driver.FindElement(elementBy).Text.ToUpper().Contains(CompareValue.ToUpper()))
                    {
                        ContainsTxt = true;
                        report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    }
                    //else if (driver.FindElement(elementBy).Text.Contains(JobCount))
                    //{
                    //    ContainsTxt = true;
                    //    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    //}
                    else
                    {
                        report.AddReportStep(FailMsg, FailMsg, StepResult.WARNING);
                    }

                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.WARNING);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);

            }
            return ContainsTxt;
        }

        public bool com_DoesNOTContainsTxt(By elementBy, string CompareValue, String PassMsg, String FailMsg)
        {
            bool ContainsTxt = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {

                    if (!driver.FindElement(elementBy).Text.Contains(CompareValue))
                    {
                        ContainsTxt = true;
                        report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    }
                    else
                    {
                        report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                    }

                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);

            }
            return ContainsTxt;
        }

        public bool com_CompareTwoTxt(string CompareValue1, string CompareValue2, String PassMsg, String FailMsg)
        {
            bool Compare = false;
            try
            {
                if (CompareValue1.Equals(CompareValue2))
                {
                    Compare = true;
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }

            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);

            }
            return Compare;
        }

        public bool isAlertPresent(IWebDriver driver)
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(driver);
            return (alert != null);
        }

        public void com_HandleAlert(bool AlertCommand)
        {
            try
            {
                if (AlertCommand)
                {
                    driver.SwitchTo().Alert().Accept();
                    System.Threading.Thread.Sleep(3000);
                    report.AddReportStep("Clicked OK button in the Alert Box", "Clicked OK button in the Alert Box", StepResult.PASS);
                }
                else
                {
                    driver.SwitchTo().Alert().Dismiss();
                    report.AddReportStep("Clicked CANCEL button in the Alert Box", "Clicked CANCEL button in the Alert Box", StepResult.PASS);
                }

            }
            catch (Exception e)
            {
                report.AddReportStep("No Alert Box is present", "No Alert Box is present", StepResult.PASS);
            }

        }

        public string com_GetWithValueProperty(By elementBy)
        {
            try
            {
                if (com_IsElementPresent(elementBy))
                {

                    return driver.FindElement(elementBy).GetAttribute("value");

                }
                else
                {

                    return string.Empty;

                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }

        public string com_GetSelectedOption(By elementBy)
        {
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    return new SelectElement(driver.FindElement(elementBy)).SelectedOption.Text;

                }
                else
                {

                    return string.Empty;

                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }

        public void elementHighlight(By element)
        {
            var tempelement = driver.FindElement(element);
            for (int i = 0; i < 2; i++)
            {
                var javaScriptDriver = (IJavaScriptExecutor)driver;
                javaScriptDriver.ExecuteScript(@"arguments[0].style.cssText = ""border-width: 5px; border-style: solid; border-color: green"";", new object[] { tempelement });
            }
        }


        public bool com_IsElementPresent(By elementBy)
        {
            bool isElementPresent = false;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

                IWebElement webElement = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(elementBy);
                });

                if (driver.FindElement(elementBy).Displayed)
                    isElementPresent = true;
                if (driver.FindElement(elementBy).Displayed)
                    elementHighlight(elementBy);

            }
            catch (WebDriverException wdEx)
            {
                Console.WriteLine(wdEx.Message);
            }
            return isElementPresent;
        }

        public bool com_ClickOnInvisibleElement(By by, string PassMsg, string failmsg)
        {
            try
            {
                if (com_IsElementPresent(by))
                {

                    IWebElement element = driver.FindElement(by);
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].click();", element);

                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                    return true;
                }
                else
                {
                    report.AddReportStep(failmsg, failmsg, StepResult.FAIL);
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool com_ClickListElement(IWebElement Obj)
        {
            try
            {
                if (Obj.Displayed)
                {

                    if (driver.Url.Contains("bmwgroupretailcareers.co.uk") || driver.Url.Contains("clarityrobotics.jobsattmp.com") || driver.Url.Contains("jobs.progleasing.com") || driver.Url.Contains("jobs.newellbrands.com") || driver.Url.Contains("www.sneakerjobs.com"))
                    {
                        Obj.Click();
                        return true;
                    }
                    else
                    {
                        IWebElement element = Obj;
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        js.ExecuteScript("arguments[0].click();", element);
                        //report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                        return true;
                    }
                }
                else
                {
                    //report.AddReportStep(failmsg, failmsg, StepResult.FAIL);
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool com_IsSelected(By elementBy, String PassMsg, String FailMsg)
        {
            bool IsSelected = false;
            //bool exceptionOccured = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    if (driver.FindElement(elementBy).Selected)
                    {
                        report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                        IsSelected = true;
                    }
                    else
                    {
                        report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                    }


                }
                else
                {
                    report.AddReportStep(elementBy.ToString() + "is not found", elementBy.ToString() + "is not found", StepResult.FAIL);
                }

            }
            catch (Exception ex)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
            }

            return IsSelected;
        }

        public void com_ClearElement(By elementBy)
        {
            try
            {
                driver.FindElement(elementBy).Clear();
            }
            catch (Exception e)
            {

            }
        }

        public bool com_SelectByValue(By elementBy, String label, String PassMsg, String FailMsg)
        {
            bool selectValue = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    new SelectElement(driver.FindElement(elementBy)).SelectByText(label);
                    selectValue = true;
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }

            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
            }
            return selectValue;
        }

        public bool com_SelectByIndex(By elementBy, int index, String PassMsg, String FailMsg)
        {
            bool selectIndex = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    new SelectElement(driver.FindElement(elementBy)).SelectByIndex(index);
                    selectIndex = true;
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);
                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }

            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
            }
            return selectIndex;
        }

        public bool com_SendKeys(By elementBy, string typeValue, String PassMsg, String FailMsg)
        {
            bool sendKeys = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    com_ClearElement(elementBy);
                    driver.FindElement(elementBy).SendKeys(typeValue);
                    sendKeys = true;
                    report.AddReportStep(PassMsg, PassMsg, StepResult.PASS);

                }
                else
                {
                    report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep(FailMsg, FailMsg, StepResult.FAIL);

            }
            return sendKeys;
        }

        public string com_GetText(By elementBy)
        {
            try
            {
                if (com_IsElementPresent(elementBy))
                {

                    return driver.FindElement(elementBy).Text;

                }
                else
                {

                    return string.Empty;

                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }

        public int com_GetListBoxSize(By elementBy)
        {
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    return new SelectElement(driver.FindElement(elementBy)).Options.Count;

                }
                else
                {

                    return 0;

                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public string com_GetTagName(By elementBy)
        {
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    return driver.FindElement(elementBy).TagName;

                }
                else
                {

                    return null;

                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public void app_verifyApplyButton(By applyButton, string expectedURLtext)
        {
            string expectedURL = "";

            //    if (driver.Url.Contains("careers.lilly.com"))
            //  {
            //  com_ClickOnInvisibleElement(applyButton, "clicked on 'Apply' button Successfully", "Unable to click on the 'Apply' button");
            //    Thread.Sleep(2000);
            //com_HandleAlert(false);
            //}
            // else
            com_ClickOnInvisibleElement(applyButton, "clicked on 'Apply' button Successfully", "Unable to click on the 'Apply' button");
            if (isAlertPresent(driver))
            {
                com_HandleAlert(true);

            }
            //Thread.Sleep(5000);
            expectedURL = driver.Url;
            try
            {
                IList<string> OpenWindows = driver.WindowHandles;
                string mainwindow = driver.CurrentWindowHandle;
                if (OpenWindows != null && OpenWindows.Count() > 1)
                {
                    foreach (string Title in OpenWindows)
                    {
                        if (!Title.Equals(mainwindow))
                        {
                            driver.SwitchTo().Window(Title);
                            if (!expectedURL.Contains(expectedURLtext) || expectedURL.Contains(expectedURLtext))
                            {
                                report.AddReportStep("Page is redirected successfully .", "On clicking apply button  page is redirected to the " + expectedURLtext + " page", StepResult.PASS);
                                driver.Navigate().Back();
                            }

                            else
                            {
                                report.AddReportStep("Page is not redirected.", "On clicking apply button  page is not redirected to the " + expectedURLtext + " page", StepResult.FAIL);
                                if (!driver.Url.Contains(""))
                                {
                                    driver.Navigate().Back();
                                }
                            }
                            driver.Close();
                            driver.SwitchTo().Window(mainwindow);
                            break;
                        }
                    }
                }
                else
                {
                    if (!expectedURL.Contains(expectedURLtext) || expectedURL.Contains(expectedURLtext))
                    {
                        report.AddReportStep("Page is redirected successfully .", "On clicking apply button  page is redirected to the " + expectedURLtext + " page", StepResult.PASS);
                        if (!driver.Url.Contains("jobs.holidayinnclub.com"))
                        {
                            driver.Navigate().Back();
                        }
                    }

                    else
                    {
                        report.AddReportStep("Page is not redirected.", "On clicking apply button  page is not redirected to the " + expectedURLtext + " page", StepResult.FAIL);
                        if (!driver.Url.Contains("jobs.holidayinnclub.com"))
                        {
                            driver.Navigate().Back();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("'Jobs By Location' || Expection occured - " + e.ToString(), "'Jobs By Location'  || Expection occured - " + e.ToString(), StepResult.FAIL);
            }
        }

        public void app_verifyTop5Link()
        {
            if (com_VerifyObjPresent(TalentBrewUI.jobsByCategoryLink, "'search jobs by category' is displayed in the L2 Page", "'search jobs by category' is displayed in the L2 Page"))
            {
                for (int i = 0; i < 5; i++)
                {
                    //check with kamesh whether need to hard code the 5 links or we can generate dynamically.
                }
            }
            else
            {
                report.AddReportStep("'search jobs by category' is not displayed in the L2 Page", "'search jobs by category' is not displayed in the L2 Page", StepResult.FAIL);
            }
        }

        public void app_verifyPagination()
        {
            if (com_IsElementPresent(TalentBrewUI.paginationNumber))
            {
                report.AddReportStep("Pagination: Available", "Pagination Available", StepResult.PASS);
                if (com_IsElementPresent(TalentBrewUI.nextPagination))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.nextPagination, "Pagination: 'Next' button is displayed/Clicked successfully", "Pagination: 'Unable to click on the 'Next' button'");
                    Thread.Sleep(4000);
                    if (com_IsElementPresent(TalentBrewUI.previousPagination))
                    {
                        report.AddReportStep("Pagination: works successfully", "'Prev' button is displayed after clicking the 'Next' button.", StepResult.PASS);
                        com_ClickOnInvisibleElement(TalentBrewUI.previousPagination, "clicked on Previous button successfully.", "Unable to click on the previous button.");
                        Thread.Sleep(5000);
                        string value = com_GetWithValueProperty(TalentBrewUI.paginationNumber);
                        if (!String.IsNullOrEmpty(value))
                        {
                            if (value.Contains("1"))
                            {
                                report.AddReportStep("Page is redirected to the previous page successfully.", "'Prev' button works successfully.", StepResult.PASS);
                            }
                            else
                            {
                                report.AddReportStep("Page is not redirected to the previous page.", "'Prev' button is not working.", StepResult.FAIL);
                            }
                        }
                    }
                }
            }
            else
            {
                report.AddReportStep("Pagination: is not Available", "Pagination is not Available", StepResult.WARNING);
            }
        }

        public bool app_verifyJobTitle(By actualElement, By expectedElement)
        {
            bool jobTitle = false;
            try
            {
                string actualTitle = com_GetText(actualElement);
                com_ClickOnInvisibleElement(actualElement, "'job link' is displayed and clicked successfully.", "'Unable to click on the 'Job Link' element.");

                string expectedTitle = com_GetText(expectedElement);

                if (actualTitle.Equals(expectedTitle))
                {
                    report.AddReportStep("'Job Title' matches between the page", "'Job Title' matches between the pages", StepResult.PASS);
                    jobTitle = true;
                }
                else
                {
                    report.AddReportStep("'job link' does not match between the pages.", "'job link' does not match between the pages.", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {

            }

            return jobTitle;
        }

        public void app_SearchjobBy(string clientUrl, string executionStatus, By JobSearchBy, string searchBy, By JobSearchByLink, string ScenarioName)
        {
            string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
            if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
            {
                com_NewLaunchUrl(clientUrl);

                if (driver.Url.Contains("careers.bbcworldwide.com"))
                {
                    app_navigateL2(searchKeyword, clientUrl);
                    com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on expand button to display advance search ", "Problem in clicking on button to expand advance search section");
                    Thread.Sleep(3000);
                }
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (com_IsElementPresent(By.XPath("//button[@id='_evidon-accept-button']")))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@id='_evidon-accept-button']"), "Clicked on Accept button", "Didn't click on Accept button");
                    }
                    ap_searchJobs(clientUrl, searchBy, JobSearchBy, JobSearchByLink, searchKeyword);
                }
                else
                {
                    ap_searchJobs(clientUrl, searchBy, JobSearchBy, JobSearchByLink, searchKeyword);
                }

            }

            else
            {
                report.AddReportStep(searchBy + ": is not applicable.", searchBy + ": is not applicable.", StepResult.WARNING);
            }
        }



        public void switchToNewWindow(string originalHandle)
        {
            try
            {

                // string originalHandle = driver.CurrentWindowHandle;
                report.AddReportStep(" current window size is " + driver.CurrentWindowHandle.Count(), "current window size is " + driver.CurrentWindowHandle.Count(), StepResult.PASS);
                report.AddReportStep("entered into site preview", "entered into site preview", StepResult.PASS);

                IList<string> Allwindows = driver.WindowHandles;
                Console.Write(driver.WindowHandles.Count());

                foreach (string window in Allwindows)
                {
                    if (window != originalHandle)
                    {
                        if (!string.IsNullOrEmpty(window))
                        {
                            driver.SwitchTo().Window(window);

                        }
                    }
                }
            }




            catch (Exception e)
            {
                Console.Write(e.StackTrace);

            }
        }

        private void ap_searchJobsNavWindows(string clientUrl, string ScenarioName, By JobSearchBy, By JobSearchByLink, string searchKeyword)
        {

            string jobCategory = ScenarioName;
            string L2PageURL = "";
            string L1jobTitle = "";
            string L2jobTitle = "";
            string JobLinkText = "";
            //int i = 0;
            int i = 0;

            bool classAttr = false;
            string attr = "";
            try
            {
                //new Actions(driver).moveToElement(element).perform();
                Expand_BrowseJobsBtn();

                if (com_VerifyObjPresent(JobSearchBy, "'" + jobCategory + "'is displayed successfully", "'" + jobCategory + "' is not displayed"))
                {
                    string originalHandle = driver.CurrentWindowHandle;
                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                    Thread.Sleep(3000);
                    switchToNewWindow(originalHandle);

                    IList<IWebElement> tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");

                    if (tempListOfJobLink.Count <= 1)
                    {
                        driver.FindElement(JobSearchBy).Click();
                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                    }
                    //  com_VerifyObjPresent(By.XPath("//*[text()='検索']"), "Found", "not Found");
                    if (tempListOfJobLink != null)
                    {
                        List<IWebElement> topJobLink = FetchTop10links(tempListOfJobLink);

                        var listOfJobLink = topJobLink.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                        report.AddReportStep("Total Jobs available", "Total Jobs under " + jobCategory + " - " + tempListOfJobLink.Count, StepResult.PASS);

                        if (driver.Url.Contains("jobs.pattersoncompanies.com") || driver.Url.Contains("www.kpcareers.org"))
                        {
                            if (listOfJobLink.Count == 0)
                            {
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            }
                        }
                        if (driver.Url.Contains("jobs.newellbrands.com"))
                        {
                            if (listOfJobLink.Count == 0)
                            {
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            }
                        }

                        if (listOfJobLink != null && listOfJobLink.Count > 0)
                        {
                            for (; i < listOfJobLink.Count; i++)
                            {
                                try
                                {
                                    if (listOfJobLink[i].Displayed)
                                    {
                                        //IList<IWebElement> link = listOfJobLink[i].FindElements(By.XPath("//li/a"));
                                        JobLinkText = listOfJobLink[i].Text;
                                        classAttr = com_chkattributePresent("class", listOfJobLink[i], attr);
                                        if (!classAttr)
                                        {
                                            if (!JobLinkText.ToLower().Trim().Equals("jobs by category") && !JobLinkText.ToLower().Trim().Equals("jobs by location") && !JobLinkText.ToLower().Trim().Equals("jobs by group") && !JobLinkText.ToLower().Trim().Equals("browse job groups") && !JobLinkText.ToLower().Trim().Equals("Search by Location") && !JobLinkText.ToLower().Trim().Equals("ou procure por categoria") && !JobLinkText.ToLower().Trim().Equals("búsqueda por categoría") && !JobLinkText.ToLower().Trim().Equals("please click here"))
                                            {
                                                if (driver.Url.Contains("careers.enterprise.com"))
                                                {
                                                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                                                }

                                                com_ClickListElement(listOfJobLink[i]);
                                                Thread.Sleep(2000);

                                                if (JobLinkText.Trim().Equals("View more"))
                                                {
                                                    com_ClickOnInvisibleElement(TalentBrewUI.lilly, "Click on the the first link after clicking on 'view more' link", "Click on the the first link after clicking on 'view more' link");
                                                }
                                                else
                                                {
                                                    waitObj(TalentBrewUI.HomePage);
                                                }
                                                if (com_IsElementPresent(TalentBrewUI.searchResultPage1) || com_IsElementPresent(TalentBrewUI.searchResultPage))
                                                {
                                                    report.AddReportStep(jobCategory + " || on Clicking '" + JobLinkText + " ' Search Result Page is loaded Successfully", jobCategory + " || on Clicking '" + JobLinkText + " ' Search Result Page is loaded Successfully", StepResult.PASS);
                                                    driver.Close();
                                                    driver.SwitchTo().Window(originalHandle);
                                                    com_NewLaunchUrl(clientUrl);
                                                    if (driver.Url.Contains("careers.bbcworldwide.com"))
                                                    {
                                                        app_navigateL2(searchKeyword, clientUrl);
                                                        com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on expand button to display advance search ", "Problem in clicking on button to expand advance search section");
                                                        Thread.Sleep(2000);
                                                    }

                                                    if (driver.Url.Contains("atos.net"))
                                                    {
                                                        WaitForObject(TalentBrewUI.btn_Explore, 100);
                                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                                                        WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                                                        Thread.Sleep(2000);
                                                    }
                                                    //else if (driver.Url.Contains("internalcareers.bbcworldwide.com"))
                                                    //{
                                                    //    com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on button to expand advance search", "Unable to click on the button to expand advance search");
                                                    //    Thread.Sleep(2000);
                                                    //}

                                                    else if (driver.Url.Contains("three.co.uk") || driver.Url.Contains("primark.com") || driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                                                    {
                                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                                                    }

                                                    else if (driver.Url.Contains("job-search.astrazeneca.fr"))
                                                    {
                                                        Thread.Sleep(2000);
                                                    }
                                                    WaitForObject(JobSearchBy, 15);
                                                    com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully", "'" + jobCategory + "'Unable to click on the element");
                                                    Thread.Sleep(3000);
                                                    switchToNewWindow(originalHandle);

                                                    if (driver.Url.Contains("job-search.astrazeneca.fr"))
                                                    {
                                                        Thread.Sleep(2000);
                                                    }
                                                    tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                    if (tempListOfJobLink.Count <= 1)
                                                    {
                                                        driver.FindElement(JobSearchBy).Click();
                                                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                    }

                                                    List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                                    listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                                                    if (driver.Url.Contains("jobs.pattersoncompanies.com") || driver.Url.Contains("jobs.newellbrands.com"))
                                                    {
                                                        if (listOfJobLink.Count == 0)
                                                        {
                                                            Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                            Thread.Sleep(2000);
                                                            Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                            Thread.Sleep(2000);
                                                            List<IWebElement> topJobLink2 = FetchTop10links(tempListOfJobLink);
                                                            listOfJobLink = topJobLink2.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (classAttr)
                                        {
                                            if (driver.Url.Contains("careers.enterprise.com"))
                                            {
                                                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                                            }
                                            tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "li");
                                            if (tempListOfJobLink.Count <= 1)
                                            {
                                                driver.FindElement(JobSearchBy).Click();
                                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "li");
                                            }
                                            List<IWebElement> topJobLink2 = FetchTop10links(tempListOfJobLink);
                                            listOfJobLink = topJobLink2.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                            listOfJobLink[i].FindElement(By.TagName("a")).Click();
                                            Thread.Sleep(2000);

                                            //IList<IWebElement> subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[@class='dropdown-link expandable-childlist-open active']/li/a"));

                                            IList<IWebElement> subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                            string subLinkText;

                                            if (subListLink[0].Displayed)
                                            {
                                                subLinkText = subListLink[0].Text;
                                                com_ClickListElement(subListLink[0]);
                                                Thread.Sleep(3000);
                                                if (driver.Url.Contains("jobs.criver.com"))
                                                {
                                                    com_ClickListElement(subListLink[0]);
                                                }
                                                if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/"))
                                                {
                                                    IList<IWebElement> subListLink1 = listOfJobLink[i].FindElements(By.XPath("//ul/li/ul/li/ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));

                                                    if (subListLink1.Count == 0)
                                                    {
                                                        subListLink1 = listOfJobLink[i].FindElements(By.XPath("//ul/li/ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                                    }
                                                    string subLinkText1;
                                                    if (subListLink1[0].Displayed)
                                                    {
                                                        subLinkText1 = subListLink1[0].Text;
                                                        com_ClickListElement(subListLink1[0]);
                                                    }

                                                    Thread.Sleep(1000);

                                                    if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/"))
                                                    {
                                                        IList<IWebElement> childSubListLink = subListLink[0].FindElements(By.XPath("//ul[@class='expandable-childlist-open']/li/a"));

                                                        //do to 
                                                        if (childSubListLink != null)
                                                        {

                                                            //childSubLinkTraverse

                                                            if (childSubListLink[1].Displayed)
                                                            {
                                                                string childSubListLinkText = childSubListLink[1].Text;
                                                                com_ClickListElement(childSubListLink[1]);
                                                                report.AddReportStep("Clicked on  " + jobCategory + " Child Sub Link List - " + JobLinkText, "Clicked on  " + jobCategory + "Child Sub Link List - " + JobLinkText, StepResult.PASS);

                                                                Thread.Sleep(1000);
                                                            }
                                                        }
                                                    }
                                                }
                                                //}
                                                if (com_IsElementPresent(TalentBrewUI.searchResultPage1))
                                                {
                                                    if (com_VerifyObjPresent(TalentBrewUI.searchResultPage1, jobCategory + " || on Clicking '" + subLinkText + " ' Search Result Page is loaded Successfully", jobCategory + "|| on clicking '" + subLinkText + "' Search Result Page is not loaded."))
                                                    {
                                                        report.AddReportStep("Job Search Page(L2) is displayed successfully", "Job Search Page(L2) is displayed successfully", StepResult.PASS);
                                                        //L2jobTitle = com_GetText(TalentBrewUI.l2_jobTitle);
                                                        //com_CompareTwoTxt(JobLinkText, L2jobTitle, "Job Title matches between Home Page and Search result Page", "Job Title does not matches between Home Page and Search result Page");
                                                    }
                                                }

                                                else if (com_IsElementPresent(TalentBrewUI.searchResultPage))
                                                {
                                                    if (com_VerifyObjPresent(TalentBrewUI.searchResultPage, jobCategory + " || on Clicking '" + subLinkText + " ' Search Result Page is loaded Successfully", jobCategory + "|| on clicking '" + subLinkText + "' Search Result Page is not loaded") || com_VerifyObjPresent(TalentBrewUI.searchResultPage, jobCategory + " || on Clicking '" + subLinkText + " ' Search Result Page is loaded Successfully", jobCategory + "|| on clicking '" + subLinkText + "' Search Result Page is not loaded"))
                                                    {
                                                        report.AddReportStep("Job Search Page(L2) is displayed successfully", "Job Search Page(L2) is displayed successfully", StepResult.PASS);
                                                        //L2jobTitle = com_GetText(TalentBrewUI.l2_jobTitle);
                                                        //com_CompareTwoTxt(JobLinkText, L2jobTitle, "Job Title matches between Home Page and Search result Page", "Job Title does not matches between Home Page and Search result Page");
                                                    }
                                                }
                                                com_NewLaunchUrl(clientUrl);
                                                if (driver.Url.Contains("atos.net"))
                                                {
                                                    WaitForObject(TalentBrewUI.btn_Explore, 100);
                                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                                                    WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                                                }
                                                WaitForObject(JobSearchBy, 15);
                                                com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully.", "'" + jobCategory + "'Unable to click on the element.");

                                                //Blank error
                                                //listOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                if (driver.Url.Contains("jobs.advocatehealth.com"))
                                                {
                                                    new Actions(driver).MoveToElement(driver.FindElement(JobSearchBy)).Perform();
                                                    Thread.Sleep(3000);
                                                }
                                                Thread.Sleep(2000);
                                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                if (tempListOfJobLink.Count <= 1)
                                                {
                                                    driver.FindElement(JobSearchBy).Click();
                                                    tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                }
                                                List<IWebElement> topJobLink3 = FetchTop10links(tempListOfJobLink);
                                                listOfJobLink = topJobLink3.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                            }

                                        }
                                        else
                                        {
                                            report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                                        }
                                    }
                                    else
                                    {
                                        report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.WARNING);
                                    }
                                }
                                catch (Exception e)
                                {
                                    if (i == 0)
                                    {
                                        report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                        }

                        com_LaunchUrl(clientUrl);

                        if (driver.Url.Contains("careers.bbcworldwide.com"))
                        {
                            app_navigateL2(searchKeyword, clientUrl);
                            com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on expand button to display advance search ", "Problem in clicking on button to expand advance search section");
                            Thread.Sleep(3000);
                        }
                        if (driver.Url.Contains("atos.net"))
                        {
                            Thread.Sleep(2000);
                            WaitForObject(TalentBrewUI.btn_Explore, 100);
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                            WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                            Thread.Sleep(3000);
                            WaitForObject(By.XPath("//nav[@id='mainMenu']/ul/li/div"), 30);
                        }

                        else if (driver.Url.Contains("three.co.uk") || driver.Url.Contains("primark.com") || driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                        }
                        if (driver.Url.Contains("job-search.astrazeneca.fr"))
                        {
                            Thread.Sleep(8000);
                        }
                        if (com_VerifyObjPresent(JobSearchBy, "'" + jobCategory + "'is displayed successfully", "'" + jobCategory + "' is not displayed."))
                        {
                            if (driver.Url.Contains("jobs.advocatehealth.com"))
                            {
                                new Actions(driver).MoveToElement(driver.FindElement(JobSearchBy)).Perform();
                                Thread.Sleep(3000);
                            }

                            //if (!driver.Url.Contains("thementornetwork.com") && !driver.Url.Contains("laureate.net") && !driver.Url.Contains("chinajobs.disneycareers.cn"))
                            else
                            {
                                if (driver.Url.Contains("jobs.disneycareers.jp"))
                                {
                                    driver.Close();
                                    driver.SwitchTo().Window(originalHandle);

                                }
                                WaitForObject(JobSearchBy, 15);
                                com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully.", "'" + jobCategory + "'Unable to click on the element.");
                                if (driver.Url.Contains("jobs.disneycareers.jp"))
                                {
                                    switchToNewWindow(originalHandle);
                                }
                                Thread.Sleep(2000);
                            }
                            if (driver.Url.Contains("careers.enterprise.com"))
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            }

                            tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                            if (tempListOfJobLink.Count <= 1)
                            {
                                driver.FindElement(JobSearchBy).Click();
                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                            }
                            List<IWebElement> topJobLink4 = FetchTop10links(tempListOfJobLink);
                            listOfJobLink = topJobLink4.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            //listOfJobLink = tempListOfJobLink.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                            if (driver.Url.Contains("jobs.pattersoncompanies.com") || driver.Url.Contains("jobs.newellbrands.com"))
                            {
                                if (listOfJobLink.Count == 0)
                                {
                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    Thread.Sleep(1000);
                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    Thread.Sleep(1000);
                                    List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                    listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                }
                            }

                            if (listOfJobLink != null && listOfJobLink.Count > 0)
                            {
                                Thread.Sleep(2000);
                                JobLinkText = listOfJobLink[i - 1].Text;
                                Thread.Sleep(1000);
                                com_ClickListElement(listOfJobLink[i - 1]);
                                report.AddReportStep("Clicked on  " + jobCategory + " Link - " + JobLinkText, "Clicked on  " + jobCategory + " Link - " + JobLinkText, StepResult.PASS);
                                Thread.Sleep(3000);
                                if (JobLinkText.Trim().Equals("View more"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.lilly, "Click on the the first link after clicking on 'view more' link", "Click on the the first link after clicking on 'view more' link");
                                }
                                else
                                {
                                    //WaitForObject(TalentBrewUI.searchResultPage);
                                }
                                if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/") || (driver.Url == clientUrl + "locations"))
                                {
                                    IList<IWebElement> subListLinkValue = listOfJobLink[i - 1].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                    com_ClickListElement(subListLinkValue[0]);
                                    report.AddReportStep("Clicked on  " + jobCategory + " Link - " + subListLinkValue[0], "Clicked on  " + jobCategory + " Link - " + subListLinkValue[0], StepResult.PASS);

                                    //to do child traverse
                                    if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/"))
                                    {
                                        IList<IWebElement> childSubListLinkValue = listOfJobLink[i - 1].FindElements(By.XPath("//ul/li/ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                        com_ClickListElement(childSubListLinkValue[1]);
                                        report.AddReportStep("Clicked on  " + jobCategory + " Sub Link List - " + childSubListLinkValue[1], "Clicked on  " + jobCategory + " Sub Link List - " + childSubListLinkValue[1], StepResult.PASS);
                                    }


                                    //report.AddReportStep("Clicked on  " + jobCategory + " Sub Link List - " + JobLinkText, "Clicked on  " + jobCategory + " Sub Link List - " + JobLinkText, StepResult.PASS);
                                }

                                //Thread.Sleep(2000);
                                if (com_VerifyObjPresent(TalentBrewUI.searchResultPage1, "Via " + jobCategory + " ||L2 Page is loaded successfully", "Via " + jobCategory + "||L2 Page is not loaded.") || com_VerifyObjPresent(TalentBrewUI.searchResultPage, "Via " + jobCategory + " ||L2 Page is loaded successfully", "Via " + jobCategory + "||L2 Page is not loaded"))
                                {
                                    //com_ContainsTxt(TalentBrewUI.l2_jobTitle, L2jobTitle, "Job Title Matches between the L1 and L2 Page", "Job Title does not matches between the L1 and L2 page");
                                    app_verifyPagination();

                                    if (com_IsElementPresent(TalentBrewUI.searchResultLink))
                                    {
                                        L2jobTitle = com_GetText(TalentBrewUI.searchResultLink);

                                        if (driver.Url.Contains("recrutement.bpce.fr"))
                                            com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);
                                        else if (driver.Url.Contains("careers.underarmour.com"))
                                            com_ClickOnInvisibleElement(TalentBrewUI.SearchResultLink4, "Search Result || Clicked on the first Job link", "Search Result || Unable to click on the first Job link");
                                        else
                                            com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);

                                        //Thread.Sleep(2000);
                                        Verify_ApplyButtons(clientUrl, jobCategory, L2jobTitle);
                                        //if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton1, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded") || com_VerifyOptionalObjPresent(TalentBrewUI.applyButton2, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded") || com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded"))
                                        //{
                                        //    com_ContainsTxt(TalentBrewUI.l3_jobTitle, L2jobTitle, "Job Title Matches between the Search results and job Details  Page", "Job Title does not matches between the Search results and job Details page");
                                        //    if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton1, "Apply button1 || is displayed", "Apply button1 || is not displayed"))
                                        //    {
                                        //        app_verifyApplyButton(TalentBrewUI.applyButton1, clientUrl);
                                        //    }
                                        //    if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton2, "Apply button2 || is displayed", "Apply button2 || is not displayed"))
                                        //    {
                                        //        app_verifyApplyButton(TalentBrewUI.applyButton2, clientUrl);
                                        //        //for bd
                                        //        com_NewLaunchUrl(clientUrl);
                                        //    }
                                        //    else
                                        //    {
                                        //        com_NewLaunchUrl(clientUrl);
                                        //    }
                                        //}

                                    }

                                    else if (com_IsElementPresent(TalentBrewUI.searchResultLink1))
                                    {
                                        L2jobTitle = com_GetText(TalentBrewUI.searchResultLink1);
                                        com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);
                                        Verify_ApplyButtons(clientUrl, jobCategory, L2jobTitle);
                                    }
                                    else if (com_IsElementPresent(TalentBrewUI.searchResultLink3))
                                    {
                                        L2jobTitle = com_GetText(TalentBrewUI.searchResultLink3);
                                        com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink3, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);
                                        Verify_ApplyButtons(clientUrl, jobCategory, L2jobTitle);
                                    }
                                    else
                                    {
                                        report.AddReportStep("Search Result || 0 Jobs found.", "Search Result || 0 Jobs found.", StepResult.WARNING);
                                    }
                                }
                                else
                                {
                                    report.AddReportStep("Via:" + ScenarioName + ": L2 Page is not Loaded successfully", "Via:" + ScenarioName + ": L2Page is not loaded successfully", StepResult.FAIL);
                                }
                            }
                            else
                            {
                                report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                            }
                        }
                    }
                    else
                    {
                        report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                    }
                }
            }
            catch (Exception e)
            {
                //report.AddReportStep(jobCategory + " || Expection occured - " + e.ToString(), jobCategory + " || Expection occured - " + e.ToString(), StepResult.FAIL);
                report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
            }




        }

        private void ap_searchJobs(string clientUrl, string ScenarioName, By JobSearchBy, By JobSearchByLink, string searchKeyword)
        {

            string jobCategory = ScenarioName;
            string L2PageURL = "";
            string L1jobTitle = "";
            string L2jobTitle = "";
            string JobLinkText = "";
            //int i = 0;
            int i = 0;

            bool classAttr = false;
            string attr = "";
            try
            {
                //new Actions(driver).moveToElement(element).perform();
                Expand_BrowseJobsBtn();
                //    else if (driver.Url.Contains("jobs.solvay.com"))
                //{
                //    com_ClickOnInvisibleElement(By.XPath("//div[@class='browse-by job-location']//h2"), "Clicked on the location drop down", "unable to click on the location drop down");
                //    Thread.Sleep(1000);
                //    com_ClickOnInvisibleElement(TalentBrewUI.Selectlocation, "selected Location from the dropdown", " unable to select the location from the drop down");
                //}

                //if (driver.Url.Contains("primark.com"))
                //{
                //    driver.FindElement(TalentBrewUI.btn_BrowseJobs).Click();
                //}
                WaitForObject(JobSearchBy, 20);

                if (com_VerifyObjPresent(JobSearchBy, "'" + jobCategory + "'is displayed successfully", "'" + jobCategory + "' is not displayed"))
                {
                    if (driver.Url.Contains("jobs.solvay.com") && ScenarioName.Contains("Jobs By Location"))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//div[@class='browse-by job-location']//h2"), "Clicked on the location drop down", "unable to click on the location drop down");
                    }
                    else
                    {
                        Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                    }
                    Thread.Sleep(3000);
                    WaitForObject(JobSearchByLink, 30);
                    IList<IWebElement> tempListOfJobLink = null;
                    if (driver.Url.Contains("boeing.com") && ScenarioName.Equals("Jobs By Location"))
                    {
                        driver.FindElement(By.XPath("//a[text()='Browse by Country']")).Click();
                        tempListOfJobLink = driver.FindElementsByXPath("//ul[@class='expandable-childlist-open']//li//a");
                    }
                    else if (driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                    {
                        //SelectElement tmp = new SelectElement(driver.FindElement(By.Id("category-list-selector")));
                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "option");
                    }
                    else if (driver.Url.Contains("sneakerjobs.com"))
                    {
                        tempListOfJobLink = driver.FindElements(JobSearchByLink);
                    }
                    else if (driver.Url.Contains("jobs.disneycareers.jp") && !ScenarioName.Equals("Jobs By Category"))
                    {

                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                        Thread.Sleep(3000);
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                        Thread.Sleep(3000);
                        driver.Close();
                        Thread.Sleep(3000);
                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                        Thread.Sleep(3000);
                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                    }
                    else if (driver.Url.Contains("jobs.advocatehealth.com") && ScenarioName.Contains("Jobs By Location"))
                    {
                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "h3");

                    }
                    else
                    {
                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");

                    }

                    if (tempListOfJobLink.Count <= 1 && !driver.Url.Contains("jobs.summithealthmanagement.com") && (!driver.Url.Contains("jobs.advocatehealth.com") && !ScenarioName.Contains("Jobs By Location")))
                    {
                        driver.FindElement(JobSearchBy).Click();
                        WaitForObject(JobSearchByLink, 30);
                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                    }


                    if (tempListOfJobLink != null)
                    {
                        List<IWebElement> topJobLink = FetchTop10links(tempListOfJobLink);


                        var listOfJobLink = topJobLink.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                        report.AddReportStep("Total Jobs available", "Total Jobs under " + jobCategory + " - " + tempListOfJobLink.Count, StepResult.PASS);

                        if (driver.Url.Contains("jobs.pattersoncompanies.com") || driver.Url.Contains("www.kpcareers.org") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("jobs.aarons.com") || driver.Url.Contains("jobs.progleasing.com"))
                        {
                            if (listOfJobLink.Count == 0)
                            {
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            }
                        }
                        if (driver.Url.Contains("jobs.adt.com") || driver.Url.Contains("emploi.adt.ca") || driver.Url.Contains("jobs.progleasing.com") || driver.Url.Contains("jobs.progleasing.com"))
                        {
                            if (listOfJobLink.Count == 0)
                            {
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            }
                        }


                        //if (driver.Url.Contains("jobs.adt.com"))
                        //{
                        //    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                        //}
                        if (listOfJobLink != null && listOfJobLink.Count > 0)
                        {
                            int j = 5;
                            if (listOfJobLink.Count < 5)
                            {
                                j = listOfJobLink.Count;
                            }
                            if (driver.Url.Contains("jobs.boeing.com") && !ScenarioName.Equals("Jobs By Location"))
                            {
                                i = 1;
                                j = 6;
                            }

                            if ((driver.Url.Contains("bmwgroupretailcareers.co.uk") || (driver.Url.Contains("jobs.laureate.net"))))
                            {
                                i = 1;
                            }
                            for (; i < j; i++)
                            {
                                try
                                {
                                    if (listOfJobLink[i].Displayed)
                                    {
                                        //IList<IWebElement> link = listOfJobLink[i].FindElements(By.XPath("//li/a"));
                                        JobLinkText = listOfJobLink[i].Text;
                                        classAttr = com_chkattributePresent("class", listOfJobLink[i], attr);
                                        if (!classAttr)
                                        {
                                            if (!JobLinkText.ToLower().Trim().Equals("jobs by category") && !JobLinkText.ToLower().Trim().Equals("jobs by location") && !JobLinkText.ToLower().Trim().Equals("jobs by group") && !JobLinkText.ToLower().Trim().Equals("browse job groups") && !JobLinkText.ToLower().Trim().Equals("Search by Location") && !JobLinkText.ToLower().Trim().Equals("ou procure por categoria") && !JobLinkText.ToLower().Trim().Equals("búsqueda por categoría") && !JobLinkText.ToLower().Trim().Equals("please click here"))
                                            {
                                                if (driver.Url.Contains("careers.enterprise.com"))
                                                {
                                                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                                                }

                                                com_ClickListElement(listOfJobLink[i]);
                                                Thread.Sleep(2000);


                                                if (JobLinkText.Trim().Equals("View more"))
                                                {
                                                    com_ClickOnInvisibleElement(TalentBrewUI.lilly, "Click on the the first link after clicking on 'view more' link", "Click on the the first link after clicking on 'view more' link");
                                                }
                                                else
                                                {
                                                    waitObj(TalentBrewUI.HomePage);
                                                }
                                                if (driver.Url.Contains("jobs.advocatehealth.com"))
                                                {
                                                    if (ScenarioName.Contains("Jobs By Location"))
                                                    {
                                                        com_ClickOnInvisibleElement(By.XPath("//div[@class='container']//div[@class='row']//div[contains(@class, 'col-md')]//img"), "Jobs dispalyed-clicked ", "Jobs dispalyed- not clicked ");
                                                    }
                                                    if (com_IsElementPresent(By.XPath("//a[text()='VIEW JOBS']")))
                                                    {
                                                        com_ClickOnInvisibleElement(By.XPath("//a[text()='VIEW JOBS']"), "View Jobs button clicked", "View jobs button not clicked");
                                                    }
                                                }
                                                if (com_IsElementPresent(TalentBrewUI.searchResultPage1) || com_IsElementPresent(TalentBrewUI.searchResultPage) || com_IsElementPresent(TalentBrewUI.searchResultPage2))
                                                {
                                                    report.AddReportStep(jobCategory + " || on Clicking '" + JobLinkText + " ' Search Result Page is loaded Successfully", jobCategory + " || on Clicking '" + JobLinkText + " ' Search Result Page is loaded Successfully", StepResult.PASS);
                                                    com_NewLaunchUrl(clientUrl);
                                                    if (driver.Url.Contains("careers.bbcworldwide.com"))
                                                    {
                                                        app_navigateL2(searchKeyword, clientUrl);
                                                        com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on expand button to display advance search ", "Problem in clicking on button to expand advance search section");
                                                        Thread.Sleep(2000);
                                                    }

                                                   //if (driver.Url.Contains("atos.net"))
                                                    //{
                                                    //    // WaitForObject(TalentBrewUI.btn_Explore, 100);
                                                    //    // com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                                                    //    WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                                                    //    com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                                                    //    Thread.Sleep(2000);
                                                    //}
                                                    //else if (driver.Url.Contains("internalcareers.bbcworldwide.com"))
                                                    //{
                                                    //    com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on button to expand advance search", "Unable to click on the button to expand advance search");
                                                    //    Thread.Sleep(2000);
                                                    //}

                                                    else if (driver.Url.Contains("three.co.uk") || driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                                                    {
                                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                                                    }

                                                    else if (driver.Url.Contains("job-search.astrazeneca.fr"))
                                                    {
                                                        Thread.Sleep(2000);
                                                    }

                                                    else if (driver.Url.Contains("www.nespressojobs.com"))
                                                    {
                                                        if (com_IsElementPresent(By.XPath("//button[@class='btn-advanced-search']")))
                                                        {
                                                            com_ClickOnInvisibleElement(By.XPath("//button[@class='btn-advanced-search']"), "clicked on Plus icon", "Not Clicked on Plus icon");
                                                        }
                                                    }

                                                    else if (driver.Url.Contains("primark.com"))
                                                    {
                                                        driver.FindElement(TalentBrewUI.btn_BrowseJobs).Click();
                                                    }

                                                    WaitForObject(JobSearchBy, 15);
                                                    //com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully", "'" + jobCategory + "'Unable to click on the element");
                                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                    Thread.Sleep(3000);

                                                    if (driver.Url.Contains("job-search.astrazeneca.fr"))
                                                    {
                                                        Thread.Sleep(2000);
                                                    }
                                                    WaitForObject(JobSearchByLink, 30);
                                                    if (driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                                                    {
                                                        //SelectElement tmp = new SelectElement(driver.FindElement(By.Id("category-list-selector")));
                                                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "option");
                                                    }
                                                    else if (driver.Url.Contains("sneakerjobs.com"))
                                                    {
                                                        tempListOfJobLink = driver.FindElements(JobSearchByLink);
                                                    }
                                                    else if (driver.Url.Contains("boeing.com") && ScenarioName.Equals("Jobs By Location"))
                                                    {
                                                        driver.FindElement(By.XPath("//a[text()='Browse by Country']")).Click();
                                                        tempListOfJobLink = driver.FindElementsByXPath("//ul[@class='expandable-childlist-open']//li//a");
                                                        Thread.Sleep(3000);
                                                    }
                                                    else if (driver.Url.Contains("jobs.disneycareers.jp") && !ScenarioName.Equals("Jobs By Category"))
                                                    {
                                                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                                                        Thread.Sleep(3000);
                                                        driver.SwitchTo().Window(driver.WindowHandles.First());
                                                        Thread.Sleep(3000);
                                                        driver.Close();
                                                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                                                        Thread.Sleep(3000);
                                                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");

                                                    }
                                                    else
                                                    {
                                                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                    }
                                                    if (tempListOfJobLink.Count <= 1 && !driver.Url.Contains("jobs.summithealthmanagement.com"))
                                                    {
                                                        driver.FindElement(JobSearchBy).Click();
                                                        WaitForObject(JobSearchByLink, 30);
                                                        tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                    }
                                                    if ((driver.Url.Contains("jobs.adt.com") && !ScenarioName.Equals("Jobs By Group")) || driver.Url.Contains("emploi.adt.ca") || driver.Url.Contains("careers.enterprise.ca"))
                                                    {

                                                        Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                        Thread.Sleep(1000);
                                                        Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                        Thread.Sleep(1000);

                                                    }


                                                    List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                                    listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                                                    if (driver.Url.Contains("jobs.pattersoncompanies.com") || driver.Url.Contains("jobs.newellbrands.com"))
                                                    {
                                                        if (listOfJobLink.Count == 0)
                                                        {
                                                            Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                            Thread.Sleep(2000);
                                                            Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                            Thread.Sleep(2000);
                                                            List<IWebElement> topJobLink2 = FetchTop10links(tempListOfJobLink);
                                                            listOfJobLink = topJobLink2.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (classAttr)
                                        {
                                            if (driver.Url.Contains("careers.enterprise.com"))
                                            {
                                                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                                            }
                                            WaitForObject(JobSearchByLink, 30);
                                            if (driver.Url.Contains("sneakerjobs.com"))
                                            {
                                                tempListOfJobLink = driver.FindElements(JobSearchByLink);
                                            }
                                            else
                                            {
                                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "li");
                                            }
                                            if (tempListOfJobLink.Count <= 1)
                                            {
                                                driver.FindElement(JobSearchBy).Click();
                                                WaitForObject(JobSearchByLink, 30);
                                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "li");
                                            }
                                            if (driver.Url.Contains("jobs.progleasing.com") || driver.Url.Contains("jobs.aarons.com"))
                                            {

                                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                Thread.Sleep(1000);
                                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                Thread.Sleep(1000);
                                                //List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                                // listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                                            }


                                            List<IWebElement> topJobLink2 = FetchTop10links(tempListOfJobLink);
                                            listOfJobLink = topJobLink2.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                            if (driver.Url.Contains("sneakerjobs.com"))
                                            {
                                                listOfJobLink[i].Click();
                                            }
                                            else
                                            {
                                                //String cText = "";
                                                //bool disp = false;
                                                //IList<IWebElement> ParentLink = driver.FindElements(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/li[1]/a"));
                                                //int ParentElementCount = ParentLink.Count;
                                                //String pText = ParentLink[0].Text;
                                                listOfJobLink[i].FindElement(By.TagName("a")).Click();
                                                //Thread.Sleep(5000);
                                                //if (driver.FindElement(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/li[1]/ul/li[1]/a")).Displayed)
                                                //    disp = true;
                                                //else
                                                //    disp = false;
                                                //IList<IWebElement> ChildLink = driver.FindElements(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/li[1]/ul/li[1]/a"));
                                                //int ChildLinkCount = ParentLink.Count;
                                                //if (ChildLinkCount > 0)
                                                //    cText = ChildLink[0].Text;
                                                //ChildLink[0].Click();
                                                //Thread.Sleep(5000);

                                            }
                                            Thread.Sleep(2000);

                                            //IList<IWebElement> subListLink = null;
                                            ////IList<IWebElement> subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[@class='dropdown-link expandable-childlist-open active']/li/a"));
                                            //if (driver.Url.Contains("jobs.newellbrands.com"))
                                            //{
                                            //Rajesh
                                            /* String cText ="";                                                    
                                             IList<IWebElement> ParentLink = driver.FindElements(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/li[1]/a"));
                                                 int ParentElementCount = ParentLink.Count;
                                                 String pText = ParentLink[0].Text;
                                              IList<IWebElement> ChildLink = driver.FindElements(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/li[1]/ul/li[1]/a"));
                                                 int ChildLinkCount = ParentLink.Count;
                                             if(ChildLinkCount>0)
                                                 cText = ChildLink[0].Text;*/
                                            //----
                                            //    IList<IWebElement> ParentLink = driver.FindElements(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/l"));
                                            //    String pText = ParentLink[i].Text;
                                            //    subListLink = ParentLink[i].FindElements(By.XPath("//*[@id='page']/div[2]/div[4]/div[2]/ul/li[1]/ul/li[1]/a"));
                                            //}
                                            //else
                                            //{

                                            //  IList<IWebElement> subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a | //ul[contains(@class,'expandable-childlist-open')]/li/a"));
                                            //  }
                                            IList<IWebElement> subListLink = null;
                                            if (driver.Url.Contains("jobs.newellbrands.com"))
                                            {
                                                subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[contains(@class,'expandable-childlist-open')]/li/a"));
                                            }
                                            else
                                            {
                                                subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                            }

                                            string subLinkText;



                                            if (subListLink[0].Displayed)
                                            {
                                                subLinkText = subListLink[0].Text;
                                                if (driver.Url.Contains("www.wellsfargojobs.com"))
                                                {
                                                    com_ClickListElement(subListLink[1]);
                                                }
                                                else
                                                {
                                                    com_ClickListElement(subListLink[0]);
                                                }
                                                Thread.Sleep(3000);
                                                if (driver.Url.Contains("jobs.criver.com") || driver.Url.Contains("emplois.criver.com"))
                                                {
                                                    com_ClickListElement(subListLink[0]);
                                                }
                                                //check it here
                                                if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/") || (driver.Url == clientUrl + "sitemap#location" && !driver.Url.Contains("disneycareers")))
                                                {
                                                    IList<IWebElement> subListLink1 = listOfJobLink[i].FindElements(By.XPath("//ul/li/ul/li/ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));

                                                    if (subListLink1.Count == 0)
                                                    {
                                                        subListLink1 = listOfJobLink[i].FindElements(By.XPath("//ul/li/ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                                    }
                                                    string subLinkText1;
                                                    if (subListLink1[0].Displayed)
                                                    {
                                                        subLinkText1 = subListLink1[0].Text;
                                                        com_ClickListElement(subListLink1[0]);
                                                    }

                                                    Thread.Sleep(1000);

                                                    if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/"))
                                                    {
                                                        IList<IWebElement> childSubListLink = subListLink[0].FindElements(By.XPath("//ul[@class='expandable-childlist-open']/li/a"));

                                                        //do to 
                                                        if (childSubListLink != null)
                                                        {

                                                            //childSubLinkTraverse

                                                            if (childSubListLink[1].Displayed)
                                                            {
                                                                string childSubListLinkText = childSubListLink[1].Text;
                                                                com_ClickListElement(childSubListLink[1]);
                                                                report.AddReportStep("Clicked on  " + jobCategory + " Child Sub Link List - " + JobLinkText, "Clicked on  " + jobCategory + "Child Sub Link List - " + JobLinkText, StepResult.PASS);

                                                                Thread.Sleep(1000);
                                                            }
                                                        }
                                                    }
                                                }
                                                //}
                                                if (com_IsElementPresent(TalentBrewUI.searchResultPage1))
                                                {
                                                    if (com_VerifyObjPresent(TalentBrewUI.searchResultPage1, jobCategory + " || on Clicking '" + subLinkText + " ' Search Result Page is loaded Successfully", jobCategory + "|| on clicking '" + subLinkText + "' Search Result Page is not loaded."))
                                                    {
                                                        report.AddReportStep("Job Search Page(L2) is displayed successfully", "Job Search Page(L2) is displayed successfully", StepResult.PASS);
                                                        //L2jobTitle = com_GetText(TalentBrewUI.l2_jobTitle);
                                                        //com_CompareTwoTxt(JobLinkText, L2jobTitle, "Job Title matches between Home Page and Search result Page", "Job Title does not matches between Home Page and Search result Page");
                                                    }
                                                }

                                                else if (com_IsElementPresent(TalentBrewUI.searchResultPage))
                                                {
                                                    if (com_VerifyObjPresent(TalentBrewUI.searchResultPage, jobCategory + " || on Clicking '" + subLinkText + " ' Search Result Page is loaded Successfully", jobCategory + "|| on clicking '" + subLinkText + "' Search Result Page is not loaded") || com_VerifyObjPresent(TalentBrewUI.searchResultPage, jobCategory + " || on Clicking '" + subLinkText + " ' Search Result Page is loaded Successfully", jobCategory + "|| on clicking '" + subLinkText + "' Search Result Page is not loaded"))
                                                    {
                                                        report.AddReportStep("Job Search Page(L2) is displayed successfully", "Job Search Page(L2) is displayed successfully", StepResult.PASS);
                                                        //L2jobTitle = com_GetText(TalentBrewUI.l2_jobTitle);
                                                        //com_CompareTwoTxt(JobLinkText, L2jobTitle, "Job Title matches between Home Page and Search result Page", "Job Title does not matches between Home Page and Search result Page");
                                                    }
                                                }
                                                com_NewLaunchUrl(clientUrl);
                                                //if (driver.Url.Contains("atos.net"))
                                                //{
                                                //    //WaitForObject(TalentBrewUI.btn_Explore, 100);
                                                //    //com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                                                //    WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                                                //    com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                                                //}
                                                WaitForObject(JobSearchBy, 15);

                                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                //com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully.", "'" + jobCategory + "'Unable to click on the element.");

                                                //Blank error
                                                //listOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                if (driver.Url.Contains("jobs.advocatehealth.com"))
                                                {
                                                    new Actions(driver).MoveToElement(driver.FindElement(JobSearchBy)).Perform();
                                                    Thread.Sleep(3000);
                                                }
                                                Thread.Sleep(2000);

                                                WaitForObject(JobSearchByLink, 30);
                                                if (driver.Url.Contains("sneakerjobs.com"))
                                                {
                                                    tempListOfJobLink = driver.FindElements(JobSearchByLink);
                                                }
                                                else
                                                {
                                                    tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                }
                                                if (tempListOfJobLink.Count <= 1)
                                                {
                                                    driver.FindElement(JobSearchBy).Click();
                                                    WaitForObject(JobSearchByLink, 30);
                                                    tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                                                }
                                                if (driver.Url.Contains("jobs.progleasing.com") || driver.Url.Contains("jobs.aarons.com"))
                                                {

                                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                    Thread.Sleep(1000);
                                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                                    Thread.Sleep(1000);
                                                    //List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                                    // listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                                                }


                                                List<IWebElement> topJobLink3 = FetchTop10links(tempListOfJobLink);
                                                listOfJobLink = topJobLink3.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                            }

                                        }
                                        else
                                        {
                                            report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                                        }
                                    }
                                    else
                                    {
                                        report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.WARNING);
                                    }
                                }
                                catch (Exception e)
                                {
                                    if (i == 0)
                                    {
                                        report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                        }

                        //After 5 jobs are verified--closing for loop 
                        com_LaunchUrl(clientUrl);

                        if (driver.Url.Contains("careers.bbcworldwide.com"))
                        {
                            app_navigateL2(searchKeyword, clientUrl);
                            com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on expand button to display advance search ", "Problem in clicking on button to expand advance search section");
                            Thread.Sleep(3000);
                        }
                        //if (driver.Url.Contains("atos.net"))
                        //{
                        //    Thread.Sleep(2000);
                        //    // WaitForObject(TalentBrewUI.btn_Explore, 100);
                        //    // com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                        //    WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                        //    com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                        //    Thread.Sleep(3000);
                        //    WaitForObject(By.XPath("//nav[@id='mainMenu']/ul/li/div"), 30);
                        //}

                        else if (driver.Url.Contains("three.co.uk") || driver.Url.Contains("primark.com") || driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                        }
                        else if (driver.Url.Contains("www.nespressojobs.com"))
                        {
                            if (com_IsElementPresent(By.XPath("//button[@class='btn-advanced-search']")))
                            {
                                com_ClickOnInvisibleElement(By.XPath("//button[@class='btn-advanced-search']"), "clicked on Plus icon", "Not Clicked on Plus icon");
                            }
                        }

                        WaitForObject(JobSearchBy, 60);
                        if (com_VerifyObjPresent(JobSearchBy, "'" + jobCategory + "'is displayed successfully", "'" + jobCategory + "' is not displayed."))
                        {
                            if (driver.Url.Contains("jobs.advocatehealth.com"))
                            {
                                new Actions(driver).MoveToElement(driver.FindElement(JobSearchBy)).Click().Build().Perform();
                                Thread.Sleep(3000);
                            }

                            //if (!driver.Url.Contains("thementornetwork.com") && !driver.Url.Contains("laureate.net") && !driver.Url.Contains("chinajobs.disneycareers.cn"))
                            else
                            {
                                WaitForObject(JobSearchBy, 15);
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                //com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully.", "'" + jobCategory + "'Unable to click on the element.");
                                Thread.Sleep(2000);
                                if (driver.Url.Contains("boeing.com"))
                                {
                                    Thread.Sleep(5000);
                                }
                            }
                            if (driver.Url.Contains("careers.enterprise.com"))
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            }
                            WaitForObject(JobSearchByLink, 30);
                            if (driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                                //SelectElement tmp = new SelectElement(driver.FindElement(By.Id("category-list-selector")));
                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "option");
                            else if (driver.Url.Contains("sneakerjobs.com"))
                                tempListOfJobLink = driver.FindElements(JobSearchByLink);
                            else if (driver.Url.Contains("boeing.com") && (ScenarioName.Equals("Jobs By Location")))
                            {

                                driver.FindElement(By.XPath("//a[text()='Browse by Country']")).Click();
                                tempListOfJobLink = driver.FindElementsByXPath("//ul[@class='expandable-childlist-open']//li//a");

                            }
                            else if (driver.Url.Contains("jobs.disneycareers.jp") && !ScenarioName.Equals("Jobs By Category"))
                            {
                                driver.SwitchTo().Window(driver.WindowHandles.Last());
                                Thread.Sleep(2000);
                                driver.SwitchTo().Window(driver.WindowHandles.First());
                                Thread.Sleep(2000);
                                driver.Close();
                                driver.SwitchTo().Window(driver.WindowHandles.Last());
                                Thread.Sleep(2000);
                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                            }
                            else if (driver.Url.Contains("jobs.advocatehealth.com") && ScenarioName.Contains("Jobs By Location"))
                            {
                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "h3");

                            }
                            else
                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");



                            if (tempListOfJobLink.Count <= 1 && !driver.Url.Contains("jobs.summithealthmanagement.com") && (!driver.Url.Contains("jobs.advocatehealth.com") && !ScenarioName.Contains("Jobs By Location")))
                            {
                                driver.FindElement(JobSearchBy).Click();
                                WaitForObject(JobSearchByLink, 30);
                                tempListOfJobLink = com_getListObjectByTagName(JobSearchByLink, "a");
                            }
                            if ((driver.Url.Contains("jobs.progleasing.com") && !ScenarioName.Equals("Jobs By Group")) || driver.Url.Contains("jobs.aarons.com"))
                            {

                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                Thread.Sleep(1000);
                                //List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                // listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            }

                            List<IWebElement> topJobLink4 = FetchTop10links(tempListOfJobLink);
                            listOfJobLink = topJobLink4.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                            //listOfJobLink = tempListOfJobLink.Where(item => item.Text != string.Empty).ToList<IWebElement>();

                            if (driver.Url.Contains("jobs.pattersoncompanies.com") || driver.Url.Contains("jobs.adt.com") || driver.Url.Contains("emploi.adt.ca"))
                            {
                                if (listOfJobLink.Count == 0)
                                {
                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    Thread.Sleep(1000);
                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    Thread.Sleep(1000);
                                    List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                    listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                }
                            }
                            if (driver.Url.Contains("careers.enterprise.ca"))
                            {
                                if (listOfJobLink.Count == 0)
                                {
                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    Thread.Sleep(1000);
                                    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    Thread.Sleep(1000);
                                    List<IWebElement> topJobLink1 = FetchTop10links(tempListOfJobLink);
                                    listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();
                                }

                            }
                            //if (driver.Url.Contains("jobs.pihhealth.org"))
                            //{
                            //   if(listOfJobLink.Count==1)
                            //   {
                            //       listOfJobLink[0].Click();
                            //      tempListOfJobLink = driver.FindElementsByXPath("//*[contains(@class,'expandable-childlist-open')]//a");
                            //     //  listOfJobLink = topJobLink1.Where(item => item.Text != string.Empty).ToList<IWebElement>();


                            //    }


                            //}



                            if (!driver.Url.Contains("jobs.atos.net"))
                            {
                                if (listOfJobLink != null && listOfJobLink.Count > 0)
                                {
                                    Thread.Sleep(2000);
                                    JobLinkText = listOfJobLink[i - 1].Text;
                                    Thread.Sleep(1000);
                                    com_ClickListElement(listOfJobLink[i - 1]);
                                    report.AddReportStep("Clicked on  " + jobCategory + " Link - " + JobLinkText, "Clicked on  " + jobCategory + " Link - " + JobLinkText, StepResult.PASS);
                                    Thread.Sleep(3000);
                                    //if (driver.Url.Contains("jobs.newellbrands.com"))
                                    //{
                                    //    Click_JobsByCatLocGrp(JobSearchBy, jobCategory);
                                    //    JobLinkText = listOfJobLink[i - 1].Text;
                                    //  //  com_ClickListElement(listOfJobLink[i - 1]);
                                    //    listOfJobLink[i-1].FindElement(By.TagName("a")).Click();

                                    //}
                                    if (JobLinkText.Trim().Equals("View more"))
                                        com_ClickOnInvisibleElement(TalentBrewUI.lilly, "Click on the the first link after clicking on 'view more' link", "Click on the the first link after clicking on 'view more' link");
                                    //else if (JobLinkText.Trim().ToLower().Contains("browse by country"))
                                    //{
                                    //    com_ClickOnInvisibleElement(By.LinkText("Browse by Country"),"Clicked on Browse by country","Problem in clicking on Browse by country");

                                    //}

                                    else
                                        //WaitForObject(TalentBrewUI.searchResultPage);
                                        if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "job-locations") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/") || (driver.Url == clientUrl + "locations") || (driver.Url == clientUrl + "sitemap") || driver.Url == clientUrl + "sitemap#location" || driver.Url == clientUrl + "sitemap#job-location" || (((driver.Url.Contains("oldnavy.com") || (driver.Url.Contains("emplois.gapcanada.ca")) || (driver.Url.Contains("emplois.gapfrance")) || (driver.Url.Contains("jobs.gap.co.uk"))) && driver.Url == clientUrl + "sitemap#location-section")))
                                        {

                                            // IList<IWebElement> subListLink = listOfJobLink[i].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a | //ul[contains(@class,'expandable-childlist-open')]/li/a"));
                                            // IList<IWebElement> subListLinkValue = listOfJobLink[i - 1].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a | //ul[contains(@class,'expandable-childlist-open')]/li/a"));
                                            IList<IWebElement> subListLinkValue = null;
                                            if (driver.Url.Contains("jobs.newellbrands.com"))
                                                subListLinkValue = listOfJobLink[i - 1].FindElements(By.XPath("//ul[contains(@class,'expandable-childlist-open')]/li/a"));
                                            else
                                                subListLinkValue = listOfJobLink[i - 1].FindElements(By.XPath("//ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                            Thread.Sleep(2000);
                                            if (driver.Url.Contains("www.wellsfargojobs.com"))
                                            {
                                                com_ClickListElement(subListLinkValue[1]);
                                            }
                                            else
                                            {
                                                com_ClickListElement(subListLinkValue[0]);
                                            }
                                            report.AddReportStep("Clicked on  " + jobCategory + " Link - " + subListLinkValue[0], "Clicked on  " + jobCategory + " Link - " + subListLinkValue[0], StepResult.PASS);

                                            //to do child traverse
                                            if (driver.Url.Contains("emplois.criver.com"))
                                                com_ClickListElement(subListLinkValue[0]);
                                            if ((driver.Url == clientUrl) || (driver.Url == clientUrl + "job-locations") || (driver.Url == clientUrl + "/") || (driver.Url == clientUrl + "jobs-by-location") || (driver.Url == clientUrl + "jobs-by-group") || (driver.Url == clientUrl + "jobs-by-category") || (driver.Url == clientUrl + "job-category/") || (driver.Url == clientUrl + "job-location/") || (driver.Url == clientUrl + "job-group/"))
                                            {
                                                IList<IWebElement> childSubListLinkValue = listOfJobLink[i - 1].FindElements(By.XPath("//ul/li/ul[@class='expandable-childlist-open' or @class='dropdown-link expandable-childlist-open active']/li/a"));
                                                //Ramya added new
                                                if (driver.Url.Contains("emplois.criver.com") || driver.Url.Contains("clarityrobotics.jobsattmp.com") || driver.Url.Contains("www.sneakerjobs.com") || driver.Url.Contains("www.fmjobs-sa.com"))
                                                {
                                                    com_ClickListElement(childSubListLinkValue[0]);
                                                    report.AddReportStep("Clicked on  " + jobCategory + " Sub Link List - " + childSubListLinkValue[0], "Clicked on  " + jobCategory + " Sub Link List - " + childSubListLinkValue[0], StepResult.PASS);
                                                }
                                                else
                                                {
                                                    Thread.Sleep(2000);
                                                    com_ClickListElement(childSubListLinkValue[1]);
                                                    report.AddReportStep("Clicked on  " + jobCategory + " Sub Link List - " + childSubListLinkValue[1], "Clicked on  " + jobCategory + " Sub Link List - " + childSubListLinkValue[1], StepResult.PASS);
                                                }
                                            }
                                            //report.AddReportStep("Clicked on  " + jobCategory + " Sub Link List - " + JobLinkText, "Clicked on  " + jobCategory + " Sub Link List - " + JobLinkText, StepResult.PASS);
                                        }

                                    //Thread.Sleep(2000);
                                    if (driver.Url.Contains("jobs.advocatehealth.com"))
                                    {
                                        if (ScenarioName.Contains("Jobs By Location"))
                                        {
                                            com_ClickOnInvisibleElement(By.XPath("//div[@class='container']//div[@class='row']//div[contains(@class, 'col-md')]//img"), "Jobs dispalyed-clicked ", "Jobs dispalyed- not clicked ");
                                        }
                                        if (com_IsElementPresent(By.XPath("//a[text()='VIEW JOBS']")))
                                        {
                                            com_ClickOnInvisibleElement(By.XPath("//a[text()='VIEW JOBS']"), "View Jobs button clicked", "View jobs button not clicked");
                                        }
                                    }
                                    if (com_VerifyOptionalObjPresent(TalentBrewUI.searchResultPage1, "Via " + jobCategory + " ||L2 Page is loaded successfully", "Via " + jobCategory + "||L2 Page is not loaded.") || com_VerifyOptionalObjPresent(TalentBrewUI.searchResultPage, "Via " + jobCategory + " ||L2 Page is loaded successfully", "Via " + jobCategory + "||L2 Page is not loaded") || com_VerifyOptionalObjPresent(TalentBrewUI.searchResultPage2, "Via " + jobCategory + " ||L2 Page is loaded successfully", "Via " + jobCategory + "||L2 Page is not loaded."))
                                    {
                                        //com_ContainsTxt(TalentBrewUI.l2_jobTitle, L2jobTitle, "Job Title Matches between the L1 and L2 Page", "Job Title does not matches between the L1 and L2 page");

                                        app_verifyPagination();

                                        if (com_IsElementPresent(TalentBrewUI.searchResultLink))
                                        {
                                            L2jobTitle = com_GetText(TalentBrewUI.searchResultLink);

                                            if (driver.Url.Contains("recrutement.bpce.fr"))
                                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);
                                            else if (driver.Url.Contains("careers.underarmour.com"))
                                                com_ClickOnInvisibleElement(TalentBrewUI.SearchResultLink4, "Search Result || Clicked on the first Job link", "Search Result || Unable to click on the first Job link");
                                            else
                                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);

                                            //Thread.Sleep(2000);
                                            Verify_ApplyButtons(clientUrl, jobCategory, L2jobTitle);
                                            //if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton1, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded") || com_VerifyOptionalObjPresent(TalentBrewUI.applyButton2, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded") || com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded"))
                                            //{
                                            //    com_ContainsTxt(TalentBrewUI.l3_jobTitle, L2jobTitle, "Job Title Matches between the Search results and job Details  Page", "Job Title does not matches between the Search results and job Details page");
                                            //    if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton1, "Apply button1 || is displayed", "Apply button1 || is not displayed"))
                                            //    {
                                            //        app_verifyApplyButton(TalentBrewUI.applyButton1, clientUrl);
                                            //    }
                                            //    if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton2, "Apply button2 || is displayed", "Apply button2 || is not displayed"))
                                            //    {
                                            //        app_verifyApplyButton(TalentBrewUI.applyButton2, clientUrl);
                                            //        //for bd
                                            //        com_NewLaunchUrl(clientUrl);
                                            //    }
                                            //    else
                                            //    {
                                            //        com_NewLaunchUrl(clientUrl);
                                            //    }
                                            //}

                                        }

                                        else if (com_IsElementPresent(TalentBrewUI.searchResultLink1))
                                        {
                                            L2jobTitle = com_GetText(TalentBrewUI.searchResultLink1);
                                            com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);
                                            Verify_ApplyButtons(clientUrl, jobCategory, L2jobTitle);
                                        }
                                        else if (com_IsElementPresent(TalentBrewUI.searchResultLink3))
                                        {
                                            L2jobTitle = com_GetText(TalentBrewUI.searchResultLink3);
                                            com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink3, "Search Result || Clicked on the first Job link - " + L2jobTitle, "Search Result || Unable to click on the first Job link - " + L2jobTitle);
                                            Verify_ApplyButtons(clientUrl, jobCategory, L2jobTitle);
                                        }

                                        else
                                        {
                                            report.AddReportStep("Search Result || 0 Jobs found.", "Search Result || 0 Jobs found.", StepResult.WARNING);
                                        }
                                    }
                                }
                                else
                                {
                                    if (driver.Url.Contains("jobs.atos.net"))
                                    {
                                        report.AddReportStep("No Job details page is displayed for this client", "No Job details page is displayed for this client", StepResult.WARNING);
                                    }
                                    else
                                    {
                                        report.AddReportStep("Via:" + ScenarioName + ": L2 Page is not Loaded successfully", "Via:" + ScenarioName + ": L2Page is not loaded successfully", StepResult.FAIL);
                                    }
                                }
                            }

                        }
                        else
                        {
                            report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                        }

                    }
                    else
                    {
                        report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
                    }
                }

            }
            catch (Exception e)
            {
                //report.AddReportStep(jobCategory + " || Expection occured - " + e.ToString(), jobCategory + " || Expection occured - " + e.ToString(), StepResult.FAIL);
                report.AddReportStep("Under:'" + jobCategory + "' || items is not displayed.", "Under : ' " + jobCategory + "' items is not displayed.", StepResult.FAIL);
            }
        }


        public void sleep(int seconds)
        {
            try
            {
                Thread.Sleep(seconds * 1000);
            }
            catch (Exception e)
            {

            }
        }

        private static List<IWebElement> FetchTop10links(IList<IWebElement> tempListOfJobLink)
        {
            List<IWebElement> topJobLink = new List<IWebElement>();
            foreach (IWebElement tempObj in tempListOfJobLink)
            {
                topJobLink.Add(tempObj);
                //Console.WriteLine(tempObj.Text);
                if (topJobLink.Count > 10)
                {
                    break;
                }
            }
            return topJobLink;
        }

        private void Click_JobsByCatLocGrp(By JobSearchBy, string jobCategory)
        {

            if (driver.Url.Contains("jobs.advocatehealth.com"))
            {
                new Actions(driver).MoveToElement(driver.FindElement(JobSearchBy)).Click().Build().Perform();
                Thread.Sleep(3000);
            }

            else if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("jobs.summithealthmanagement.com") || driver.Url.Contains("jobs.deluxe.com") || driver.Url.Contains("jobs.trocglobal.com"))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(200,300)");
                driver.FindElement(JobSearchBy).Click();
                Thread.Sleep(3000);
            }

            else if (driver.Url.Contains("empleos.scotiabank.com") || driver.Url.Contains("jobs.milfordregional.org") || driver.Url.Contains("www.jobs.libertymutualgroup.com") || driver.Url.Contains("jobs.carnival.com") || driver.Url.Contains("jobs.summitmedicalgroup.com") || driver.Url.Contains("jobs.sleepnumber.com") || driver.Url.Contains("jobs.petsmart.com"))
            {
                WaitForObject(JobSearchBy, 15);
                driver.FindElement(JobSearchBy).Click();
                Thread.Sleep(3000);
            }
            else if (driver.Url.Contains("jobs.pizzahut.com"))
            {
                WaitForObject(JobSearchBy, 15);

                com_ClickOnInvisibleElement(JobSearchBy, "'" + jobCategory + "' is displayed and clicked successfully", "'" + jobCategory + "'Unable to click on the element");
                Thread.Sleep(2000);

            }//Ramya Added new
            else if (driver.Url.Contains("www.aetnacareers.com"))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,3500)");
                driver.FindElement(JobSearchBy).Click();
                Thread.Sleep(3000);
            }
            else if (driver.Url.Contains("jobs.newellbrands.com"))
            {

                //Actions ac = new Actions(driver);
                //IWebElement element = driver.FindElement(JobSearchBy);
                //ac.MoveToElement(element).Perform();
                // IWebElement element = driver.FindElement(by);
                // IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                //// js.ExecuteScript("arguments[0].click();", element);
                //  js.ExecuteScript ("document.getElementByXpat('"++"').focus()") 
                //IWebElement element = driver.FindElement(JobSearchBy);
                //element.SendKeys("");
                Actions action = new Actions(driver);
                IWebElement elem = driver.FindElement(JobSearchBy);
                action.MoveToElement(elem);
                action.Perform();
                this.sleep(2);

            }

            else
            {
                if (driver.Url.Contains("job-search.astrazeneca.fr"))
                {

                    Thread.Sleep(2000);
                }
                else if (driver.Url.Contains("job-search.astrazeneca.com") || driver.Url.Contains("job-search.astrazeneca.se"))
                {
                    WaitForObject(JobSearchBy, 15);
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("arguments[0].click();", driver.FindElement(JobSearchBy));

                }
                else if (driver.Url.Contains("jobs.bd.com"))
                {
                    WaitForObject(JobSearchBy, 15);
                    Thread.Sleep(3000);
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("arguments[0].click();", driver.FindElement(JobSearchBy));
                }
                else
                {
                    WaitForObject(JobSearchBy, 15);
                    driver.FindElement(JobSearchBy).Click();
                }
                Thread.Sleep(2000);
            }
            //driver.FindElement(By.ClassName("category-list")).Click();


        }

        private void Expand_BrowseJobsBtn()
        {
            if (driver.Url.Contains("three.co.uk") || driver.Url.Contains("primark.com") || driver.Url.Contains("bmwgroupretailcareers.co.uk"))
            {
                driver.FindElement(TalentBrewUI.btn_BrowseJobs).Click();
            }
            else if (driver.Url.Contains("job-search.astrazeneca.fr"))
            {
                Thread.Sleep(2000);
            }

            //else if (driver.Url.Contains("internalcareers.bbcworldwide.com"))
            //{
            //    com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on button to expand advance search", "Unable to click on the button to expand advance search");
            //    Thread.Sleep(4000);
            //}
        }

        private void Verify_ApplyButtons(string clientUrl, string jobCategory, string L2jobTitle)
        {
            WaitForObject(TalentBrewUI.applyButton1, 20);
            if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton1, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded.") || com_VerifyOptionalObjPresent(TalentBrewUI.applyButton2, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded.") || com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "Via" + jobCategory + "job Details Page is Loaded successfully", "Via ||" + jobCategory + " || job Details  Page is not loaded."))
            {
                com_ContainsTxt(TalentBrewUI.l3_jobTitle, L2jobTitle, "Job Title Matches between the Search results and job Details  Page", "Job Title does not matches between the Search results and job Details page");
                if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton1, "Apply button1 || is displayed", "Apply button1 || is not displayed"))
                {
                    app_verifyApplyButton(TalentBrewUI.applyButton1, clientUrl);
                }
                if (com_VerifyOptionalObjPresent(TalentBrewUI.applyButton2, "Apply button2 || is displayed", "Apply button2 || is not displayed"))
                {
                    app_verifyApplyButton(TalentBrewUI.applyButton2, clientUrl);
                    //for bd

                    com_NewLaunchUrl(clientUrl);
                }
                else
                {
                    com_NewLaunchUrl(clientUrl);
                }
            }
            else
            {
                report.AddReportStep("Job Details : Does not navigated to Job details page on clicking the Job link in search result page", "Job Details : Does not navigated to Job details page on clicking the Job link in search result page", StepResult.FAIL);
            }
        }

        private bool com_chkattributePresent(string p, IWebElement webElement, string att)
        {
            try
            {
                att = webElement.GetAttribute(p);
                if (!att.Contains("expandable-parent"))
                    //if (att != "expandable-parent")
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public void app_socialMedia(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Social Media");
            try
            {
                //report.AddReportStep
                //com_HandleAlert(true);
                string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    //if(!driver.Url.Contains("careers.duffandphelps"))
                    //    com_NewLaunchUrl(clientUrl);

                    if (ScenarioName.Equals("L1"))
                    {
                        app_verifySocialMedia();
                    }
                    else if (ScenarioName.Equals("L2"))
                    {
                        if (app_navigateL2(searchKeyword, clientUrl))
                        {
                            app_verifySocialMedia();
                        }
                    }
                    else if (ScenarioName.Equals("L3"))
                    {
                        if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                        {
                            app_verifySocialMedia();
                        }
                    }
                }
                else
                {
                    report.AddReportStep("'SocialMedia' is not available", "'SocialMedia' is not available", StepResult.WARNING);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("'SocialMedia' || Expection occured - " + e.ToString(), "'SocialMedia'  || Expection occured - " + e.ToString(), StepResult.FAIL);
            }
        }

        private void app_verifySocialMedia()
        {
            //Rajesh added
            if (driver.Url.Contains("jobs.holidayinnclub.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.btn_ExploreSocialMedia))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_ExploreSocialMedia, "Clicked on Social Media explore button", "Unable to click on the Social Media explore button");
            }

            if (driver.Url.Contains("mcdonalds.co.uk"))
            {
                if (com_VerifyOptionalObjPresent(TalentBrewUI.facebook1, "Social Media || is displayed", "Social Media || is not displayed"))
                {
                    com_VerifyOptionalObjPresent(TalentBrewUI.facebook1, "Social Media ||'Facebook' link is displayed", "Social Media ||'Facebook' link is not displayed");
                }

            }
            else if (driver.Url.Contains("raymondjames.com") || driver.Url.Contains("chinajobs.disneycareers") || driver.Url.Contains("jobs.bd"))
            {
                if (com_IsElementPresent(TalentBrewUI.facebook) || com_IsElementPresent(TalentBrewUI.linkedIn))
                {
                    report.AddReportStep("Social Media", "Social Media || is displayed", StepResult.PASS);
                    com_VerifyOptionalObjPresent(TalentBrewUI.facebook, "Social Media ||'Facebook' link is displayed", "Social Media || 'Facebook' link is not displayed");
                    com_VerifyOptionalObjPresent(TalentBrewUI.twitter, "Social Media ||'Twitter'link is displayed", "Social Media ||'Twitter' link is not displayed");
                    com_VerifyOptionalObjPresent(TalentBrewUI.linkedIn, "Social Media ||'Linkedin'link is displayed", "Social Media ||'Linkedin' link is not displayed");
                    com_VerifyOptionalObjPresent(TalentBrewUI.youTube, "Social Media ||'Youtube'link is displayed", "Social Media ||'Youtube' link is not displayed");
                }
            }

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule))
                SocialMedia(TalentBrewUI.socialMediaModule);

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule1))
                SocialMedia(TalentBrewUI.socialMediaModule1);

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule2))
                SocialMedia(TalentBrewUI.socialMediaModule2);

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule3))
                SocialMedia(TalentBrewUI.socialMediaModule3);

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule4))
                SocialMedia(TalentBrewUI.socialMediaModule4);

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule5))
                SocialMedia(TalentBrewUI.socialMediaModule5);

            else if (com_IsElementPresent(TalentBrewUI.socialMediaModule6))
                SocialMedia(TalentBrewUI.socialMediaModule6);

            else
            {
                if (driver.Url.Contains("deborahcareers"))
                {
                    com_NewLaunchUrl(driver.Url);
                    if (com_IsElementPresent(TalentBrewUI.socialMediaModule))
                        SocialMedia(TalentBrewUI.socialMediaModule);
                }
                else
                    report.AddReportStep("'SocialMedia' is not displayed", "'SocialMedia' is not displayed", StepResult.FAIL);
            }

        }

        private void SocialMedia(By obj)
        {
            if (com_VerifyObjPresent(obj, "Social Media || is displayed", "Social Media || is not displayed"))
            {
                if (driver.Url.Contains("deborahcareers.org"))
                {
                    com_NewLaunchUrl(driver.Url);
                    Thread.Sleep(2000);
                }

                else if (driver.Url.Contains("careers.duffandphelps.jobs"))
                {
                    WaitForObject(TalentBrewUI.socialMediaModule, 70);
                    Thread.Sleep(2000);
                }

                if (com_IsElementPresent(TalentBrewUI.facebook))
                    com_VerifyOptionalObjPresent(TalentBrewUI.facebook, "Social Media ||'Facebook' link is displayed", "Social Media ||'Facebook' link is not displayed");
                else if (com_IsElementPresent(TalentBrewUI.facebook1))
                    com_VerifyOptionalObjPresent(TalentBrewUI.facebook1, "Social Media ||'Facebook' link is displayed", "Social Media ||'Facebook' link is not displayed");
                else if (com_IsElementPresent(TalentBrewUI.facebook2))
                    com_VerifyOptionalObjPresent(TalentBrewUI.facebook2, "Social Media ||'Facebook' link is displayed", "Social Media ||'Facebook' link is not displayed");
                else
                    report.AddReportStep("Social Media || 'Facebook' link is not available for this site", "Social Media || 'Facebook' link is not available for this site", StepResult.WARNING);

                if (com_IsElementPresent(TalentBrewUI.linkedIn))
                    com_VerifyOptionalObjPresent(TalentBrewUI.linkedIn, "Social Media ||'linkedIn' link is displayed", "Social Media ||'LinkedIn' link is not displayed");
                else if (com_IsElementPresent(TalentBrewUI.linkedIn1))
                    com_VerifyOptionalObjPresent(TalentBrewUI.linkedIn1, "Social Media ||'linkedIn' link is displayed", "Social Media ||'LinkedIn' link is not displayed");
                else if (com_IsElementPresent(TalentBrewUI.linkedIn2))
                    com_VerifyOptionalObjPresent(TalentBrewUI.linkedIn2, "Social Media ||'linkedIn' link is displayed", "Social Media ||'LinkedIn' link is not displayed");
                else
                    report.AddReportStep("Social Media || 'Linkedin' link is not available for this site", "Social Media || 'Linkedin' link is not available for this site", StepResult.WARNING);

                if (com_IsElementPresent(TalentBrewUI.twitter))
                    com_VerifyOptionalObjPresent(TalentBrewUI.twitter, "Social Media ||'Twitter' link is displayed", "Social Media ||'Twitter' link is not displayed");
                else if (com_IsElementPresent(TalentBrewUI.twitter1))
                    com_VerifyOptionalObjPresent(TalentBrewUI.twitter1, "Social Media ||'Twitter' link is displayed", "Social Media ||'Twitter' link is not displayed");
                else if (com_IsElementPresent(TalentBrewUI.twitter2))
                    com_VerifyOptionalObjPresent(TalentBrewUI.twitter2, "Social Media ||'Twitter' link is displayed", "Social Media ||'Twitter' link is not displayed");
                else
                    report.AddReportStep("Social Media || 'Twitter' link is not available for this site", "Social Media || 'Twitter' link is not available for this site", StepResult.WARNING);

                if (com_IsElementPresent(TalentBrewUI.youTube))
                    com_VerifyOptionalObjPresent(TalentBrewUI.youTube, "Social Media ||'youTube' link is displayed", "Social Media ||'youTube' link is not displayed");
                else
                    report.AddReportStep("Social Media || 'Youtube' link is not available for this site", "Social Media || 'Youtube' link is not available for this site", StepResult.WARNING);

                if (com_IsElementPresent(TalentBrewUI.pInterest))
                    com_VerifyOptionalObjPresent(TalentBrewUI.pInterest, "Social Media || 'pInterest' link is displayed.", "Social Media || 'pInterest' link is not displayed");
                else
                    report.AddReportStep("Social Media || 'pInterest' link is not available for this site", "Social Media || 'pInterest' link is not available for this site", StepResult.WARNING);

                if (com_IsElementPresent(TalentBrewUI.Instagram))
                    com_VerifyOptionalObjPresent(TalentBrewUI.Instagram, "Social Media || 'Instagram' link is displayed.", "Social Media || 'Instagram' link is not displayed");
                else
                    report.AddReportStep("Social Media || 'Instagram' link is not available for this site", "Social Media || 'Instagram' link is not available for this site", StepResult.WARNING);
            }
        }


        public void app_recentJobs(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Recent Jobs");
            try
            {
                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    com_NewLaunchUrl(clientUrl);
                    string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);

                    if (ScenarioName.Equals("L1"))
                    {
                        if (com_IsElementPresent(TalentBrewUI.recentJobModule))
                        {
                            com_VerifyObjPresent(TalentBrewUI.recentJobModule, "'Recent Jobs'|| module is displayed successfully in the Home Page", "'Recent Jobs'|| module is not displayed in the Home Page");
                            app_verifyRecentJobLink(clientUrl);
                        }

                        else if (com_IsElementPresent(TalentBrewUI.recentJobModule1))
                        {
                            com_VerifyObjPresent(TalentBrewUI.recentJobModule1, "'Recent Jobs'|| module is displayed successfully in the Home Page", "'Recent Jobs'|| module is not displayed in the Home Page");
                            app_verifyRecentJobLink(clientUrl);
                        }

                    }
                    else if (ScenarioName.Equals("L2"))
                    {
                        if (app_navigateL2(searchKeyword, clientUrl))
                        {
                            if (com_IsElementPresent(TalentBrewUI.recentJobModule))
                            {
                                com_VerifyObjPresent(TalentBrewUI.recentJobModule, "'Recent Jobs'|| module is displayed successfully in the Home Page", "'Recent Jobs'|| module is not displayed in the Home Page");
                                app_verifyRecentJobLink(clientUrl);
                            }

                            else if (com_IsElementPresent(TalentBrewUI.recentJobModule1))
                            {
                                com_VerifyObjPresent(TalentBrewUI.recentJobModule1, "'Recent Jobs'|| module is displayed successfully in the Home Page", "'Recent Jobs'|| module is not displayed in the Home Page");
                                app_verifyRecentJobLink(clientUrl);
                            }

                        }
                    }
                    //else if (ScenarioName.Equals("L3"))
                    //{
                    //    if (app_navigateL3(searchKeyword))
                    //    {
                    //        if (com_VerifyObjPresent(TalentBrewUI.recentJobModule, "'Recent Jobs' || module is displayed successfully in Job Detail Page.", "'Recent Jobs' || module is not displayed in Job Detail Page."))
                    //        {
                    //            app_verifyRecentJobLink(clientUrl);
                    //        }

                    //    }
                    //}
                }
                else
                {
                    report.AddReportStep("'Recent Jobs' || module is not available", "'Recent Jobs' || module is not available", StepResult.WARNING);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("'Recent  Jobs' || Expection occured - " + e.ToString(), "'Recent  Jobs'  || Expection occured - " + e.ToString(), StepResult.FAIL);
            }
        }

        private void app_verifyRecentJobLink(string clientUrl)
        {
            try
            {
                //if (driver.Url.Contains("jobs.boystown.org"))
                //{
                //    com_ClickOnInvisibleElement(TalentBrewUI.recentJobModule1, "Recent Jobs || clicked on Recent Job link", "Recent Jobs || Unable to click on Recent Job link");
                //}

                IList<IWebElement> listRecentJobLinks = com_getListObjectByTagName(TalentBrewUI.recentJobLink, "a");
                if (!listRecentJobLinks.Equals(null))
                {
                    for (int i = 0; i < listRecentJobLinks.Count(); i++)
                    {
                        string recentJobLinkText = listRecentJobLinks[i].Text;
                        Thread.Sleep(1000);

                        if (driver.Url.Contains("careers.enterprise.com"))
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,700)");

                        com_ClickListElement(listRecentJobLinks[i]);
                        //com_ClickOnInvisibleElement(recentJobLinkText,"Clicked on the item","Problem in clicking the item");
                        Thread.Sleep(1000);
                        if (com_IsElementPresent(TalentBrewUI.applyButton1))
                            com_VerifyObjPresent(TalentBrewUI.applyButton1, "Recent Jobs || Recent Job link: " + (i + 1) + "'" + recentJobLinkText + "' redirects to L3 page successfully", "Recent Jobs || Recent Job link :" + (i + 1) + "'" + recentJobLinkText + "' does not redirects to L3 page");
                        else if (com_IsElementPresent(TalentBrewUI.applyButton2))
                            com_VerifyObjPresent(TalentBrewUI.applyButton2, "Recent Jobs || Recent Job link: " + (i + 1) + "'" + recentJobLinkText + "' redirects to L3 page successfully", "Recent Jobs || Recent Job link :" + (i + 1) + "'" + recentJobLinkText + "' does not redirects to L3 page");

                        com_NewLaunchUrl(clientUrl);

                        //if (driver.Url.Contains("jobs.boystown.org"))
                        //    com_ClickOnInvisibleElement(TalentBrewUI.recentJobModule1, "Recent Jobs || clicked on Recent Job link", "Recent Jobs || Unable to click on Recent Job link");

                        listRecentJobLinks = com_getListObjectByTagName(TalentBrewUI.recentJobLink, "a");
                    }
                }
                else
                    report.AddReportStep("Recent Job Link is not displayed", "Recent Job Link is not displayed.", StepResult.FAIL);

            }
            catch (Exception e) { }
        }

        public void app_meetUs(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Meet Us");
            try
            {
                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    com_NewLaunchUrl(clientUrl);
                    string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);

                    if (ScenarioName.Equals("L3"))
                    {
                        if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                        {
                            com_VerifyObjPresent(TalentBrewUI.meetUs, "'Meet Us' || module is displayed successfully in Job Detail Page.", "'Meet Us' || module is not displayed in Job Detail.");

                        }
                    }
                }
                else
                {
                    report.AddReportStep("'MeetUs' || module is not available", "'MeetUs' || module is not available", StepResult.WARNING);
                }
            }
            catch (Exception e) { }
        }

        public void app_siteMap(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Site Map");
            string actualTitle = "";
            string expectedTitle = "Sitemap";

            try
            {
                //com_HandleAlert(true);

                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    com_NewLaunchUrl(clientUrl);
                    string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);

                    if (ScenarioName.Equals("L1"))
                    {
                        app_verifySiteMap(clientUrl, actualTitle, expectedTitle);
                    }
                    else if (ScenarioName.Equals("L2"))
                    {
                        if (app_navigateL2(searchKeyword, clientUrl))
                        {
                            app_verifySiteMap(clientUrl, actualTitle, expectedTitle);
                        }
                    }
                    else if (ScenarioName.Equals("L3"))
                    {
                        if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                        {
                            app_verifySiteMap(clientUrl, actualTitle, expectedTitle);
                        }
                    }
                }
                else
                {
                    report.AddReportStep("'SiteMap' || is not available", "'SiteMap' || is not available", StepResult.WARNING);
                }
            }

            catch (Exception e)
            {
                report.AddReportStep("'SiteMap' || Expection occured -" + e.ToString(), "'SiteMap' || Expection occured -" + e.ToString(), StepResult.FAIL);
            }
        }

        internal void app_loading(By obj)
        {
            //Method to wait until the element is not present
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            for (int i = 0; i < 8; i++)
            {
                if (com_IsElementPresent(obj))
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        public void WaitForObject(By Obj, int itr)
        {
            for (int i = 0; i < itr; i++)
            {

                if (waitObj(Obj))
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        public bool waitObj(By Obj)
        {
            bool flag = false;
            try
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                //IWebElement webElement = wait.Until<IWebElement>((d) =>
                //{
                //    return d.FindElement(Obj);
                //});
                flag = driver.FindElement(Obj).Displayed;

            }
            catch (Exception e)
            {

            }

            return flag;
        }

        //private void app_verifySiteMap(string actualTitle, string expectedTitle)
        //{
        //    bool SIteMapWinFound = false;
        //    //By objectBy;
        //    //if(driver.Url.Contains("utcaerospacesystems.com") || driver.Url.Contains("utc.com"))
        //    //    objectBy = TalentBrewUI.siteMap;
        //    //else if (com_IsElementPresent(TalentBrewUI.siteMap2))
        //    //    objectBy = TalentBrewUI.siteMap2;
        //    //else if (com_IsElementPresent(TalentBrewUI.siteMap))
        //    //    objectBy = TalentBrewUI.siteMap;
        //    //else
        //    //    objectBy = TalentBrewUI.siteMap1;

        //    if (driver.Url.Contains("utcaerospacesystems.com") || driver.Url.Contains("careers.hpe.com"))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.SiteMap5);
        //    else if (driver.Url.Contains("jobs.utc.com"))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.SiteMap4);
        //    else if (driver.Url.Contains("jobs.mountcarmelhealth.com") || driver.Url.Contains("jobs.parexel.com"))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.SiteMap3);
        //    else if (driver.Url.Contains("jobs.ccs.utc.com") || driver.Url.Contains("usccareers.usc.edu"))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap);
        //    else if (com_IsElementPresent(TalentBrewUI.siteMap6))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap6);
        //    else if (com_IsElementPresent(TalentBrewUI.siteMap2))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap2);
        //    else if (com_IsElementPresent(TalentBrewUI.siteMap1))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap1);
        //    else if (com_IsElementPresent(TalentBrewUI.siteMap))
        //        SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap);
        //    else
        //        report.AddReportStep("'SiteMap' || Sitemap link is not available", "'SiteMap' || Sitemap link is not available", StepResult.FAIL);
        //}



        //...........................Modified sitemap Functionality--- Krishna--06/09/2018........................//
        private void app_verifySiteMap(string clientUrl, string actualTitle, string expectedTitle)
        {
            bool SIteMapWinFound = false;

            if (driver.Url.Contains("utcaerospacesystems.com") || driver.Url.Contains("careers.hpe.com") || driver.Url.Contains("jobs.utc.com"))
                SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.SiteMap5);
            else if (driver.Url.Contains("jobs.mountcarmelhealth.com") || driver.Url.Contains("jobs.parexel.com"))
                SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.SiteMap3);
            else if (driver.Url.Contains("jobs.ccs.utc.com") || driver.Url.Contains("usccareers.usc.edu"))
                SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap);
            else if (com_IsElementPresent(TalentBrewUI.siteMap6))
                SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap6);
            else if (com_IsElementPresent(TalentBrewUI.siteMap2))
                SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap2);
            else if (com_IsElementPresent(TalentBrewUI.siteMap1))
                SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap1);
            else if (!driver.Url.Contains("jobs.dell.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.siteMap))
                    SiteMap_Verify(actualTitle, expectedTitle, SIteMapWinFound, TalentBrewUI.siteMap);
            }

            else
                SiteMap_AppendURL(clientUrl, actualTitle, expectedTitle, SIteMapWinFound);
        }

        private void SiteMap_Verify(string actualTitle, string expectedTitle, bool SIteMapWinFound, By obj)
        {
            //com_ClickOnInvisibleElement(TalentBrewUI.SiteMap4,"Sitemap || Clicked on sitemap link", "Unable to click on sitemap link");
            if (com_VerifyObjPresent(obj, expectedTitle + "|| link is displayed successfully", expectedTitle + "|| link is not displayed"))
            {
                com_ClickOnInvisibleElement(obj, expectedTitle + "|| link is clicked successfully", "Unable to click on the '" + expectedTitle + "' link");

                Thread.Sleep(3000);
                IList<string> OpenWindows = driver.WindowHandles;
                if (OpenWindows != null && OpenWindows.Count() > 1)
                {
                    actualTitle = driver.CurrentWindowHandle;
                    foreach (string Title in OpenWindows)
                    {
                        driver.SwitchTo().Window(Title);
                        if (driver.Title.ToLower().Contains(expectedTitle.ToLower()) || driver.Title.ToLower().Contains("sitemap") || driver.Title.ToLower().Contains("site map") || driver.Title.ToLower().Contains("site") || driver.Url.ToLower().Contains("sitemap"))
                        {
                            SIteMapWinFound = true;
                            report.AddReportStep(expectedTitle + " page is displayed.", expectedTitle + " page is displayed successfully.", StepResult.PASS);
                            //com_ClickOnInvisibleElement(TalentBrewUI.careerHomePage, "Clicked on 'Career Home Page' Link.", "Unable to Clicked on 'Career Home Page' Link.");
                            //com_VerifyObjPresent(TalentBrewUI.HomePage, "Page is redirected to home page successfully from ", "Page is not redirected to Home Page from SiteMap Page.");
                            break;
                        }
                    }

                    if (!SIteMapWinFound)
                    {
                        report.AddReportStep(expectedTitle + "|| Page is not loaded.", expectedTitle + " || Page is not loaded.", StepResult.FAIL);
                    }
                    driver.Close();

                    driver.SwitchTo().Window(actualTitle);

                }
                else
                {
                    actualTitle = driver.Title;
                    //actualTitle = driver.Url;
                    if (actualTitle.Contains(expectedTitle) || actualTitle.Contains("Site Map") || actualTitle.Contains("sitemap") || actualTitle.Contains("Sitemap") || actualTitle.Contains("Site map") || actualTitle.Contains("site") || actualTitle.Contains("Site") || driver.Url.Contains(expectedTitle) || driver.Url.Contains("Site Map") || driver.Url.Contains("sitemap") || driver.Url.Contains("Sitemap") || driver.Url.Contains("Site map") || driver.Url.Contains("site") || driver.Url.Contains("Site"))
                    {
                        report.AddReportStep(expectedTitle + "is displayed.", expectedTitle + " is displayed successfully.", StepResult.PASS);
                        //com_ClickOnInvisibleElement(TalentBrewUI.careerHomePage, "Clicked on 'Career Home Page' Link.", "Unable to Clicked on 'Career Home Page' Link.");
                        //com_VerifyObjPresent(TalentBrewUI.HomePage, "Page is redirected to home page successfully from ", "Page is not redirected to Home Page from SiteMap Page.");
                    }

                    else
                    {
                        report.AddReportStep(expectedTitle + "|| Page is not loaded.", expectedTitle + " || Page is not loaded.", StepResult.FAIL);
                    }
                }
            }
        }



        private void SiteMap_AppendURL(string clientUrl, string actualTitle, string expectedTitle, bool SIteMapWinFound)
        {
            string actualURL = clientUrl + "sitemap";
            com_NewLaunchUrl(actualURL);
            Thread.Sleep(3000);
            actualTitle = driver.Title;
            if (actualTitle.Contains(expectedTitle) || actualTitle.Contains("Site Map") || actualTitle.Contains("sitemap") || actualTitle.Contains("Sitemap") || actualTitle.Contains("Site map") || actualTitle.Contains("site") || actualTitle.ToLower().Contains("site"))
            {
                SIteMapWinFound = true;
                report.AddReportStep(expectedTitle + "is displayed.", expectedTitle + " is displayed successfully.Append Pass", StepResult.PASS);
                //com_ClickOnInvisibleElement(TalentBrewUI.careerHomePage, "Clicked on 'Career Home Page' Link.", "Unable to Clicked on 'Career Home Page' Link.");
                //com_VerifyObjPresent(TalentBrewUI.HomePage, "Page is redirected to home page successfully from ", "Page is not redirected to Home Page from SiteMap Page.");
            }

            else
            {
                if (com_IsElementPresent(By.XPath("//body[contains(@id,'site')]")))
                    report.AddReportStep("Verifying SiteMap page.", "Sitemap page is loaded successfully. Append Pass", StepResult.PASS);
                else
                    report.AddReportStep(expectedTitle + "|| Page is not loaded. ", expectedTitle + " || Page is not loaded.Append failure", StepResult.FAIL);
            }
        }



        private IList<IWebElement> com_getListObjectByTagName(By Obj, string tagName)
        {
            try
            {
                if (com_IsElementPresent(Obj))
                {
                    IWebElement parentObj = driver.FindElement(Obj);
                    IList<IWebElement> listOfObject = parentObj.FindElements(By.TagName(tagName));
                    return listOfObject;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }


        private IList<IWebElement> com_getListObjectByXpath(By Obj, string tagName)
        {
            try
            {
                if (com_IsElementPresent(Obj))
                {
                    IWebElement parentObj = driver.FindElement(Obj);
                    IList<IWebElement> listOfObject = parentObj.FindElements(By.XPath(tagName));
                    return listOfObject;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        //**************************************************************************************

        public bool com_IsSelected(By elementBy)
        {
            bool IsSelected = false;
            //bool exceptionOccured = false;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    if (driver.FindElement(elementBy).Selected)
                    {
                        IsSelected = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return IsSelected;
        }

        public string com_getobjProperty(By elementBy, string prop)
        {
            string getpropvalue = string.Empty;
            try
            {
                if (com_IsElementPresent(elementBy))
                {
                    getpropvalue = driver.FindElement(elementBy).GetAttribute(prop);
                }
                else
                {
                    report.AddReportStep("Element is not available", "Element is not available", StepResult.WARNING);
                }
            }
            catch (Exception e)
            {

            }
            return getpropvalue;
        }

        public bool app_navigateL2(string searchKeyword, string clientUrl)
        {
            bool navigateL2 = false;
            try
            {              
                clientUrl = clientUrl.Trim();
                //String clientUrl = driver.Url;

                //change for newclient Nestle :
                if (clientUrl.Contains("https://www.nestlewaterscareers.com/search-site"))
                {
                    clientUrl = clientUrl.Replace("/search-site", "");
                }
                else if (clientUrl.Contains("https://jobs.gapinc.com/"))
                {
                    com_NewLaunchUrl("https://jobs.gapinc.com/search-jobs");
                }
                //Ramya Added Latest
                else if (clientUrl.Contains("newsearch.tmp.com") || clientUrl.Contains("www.tmp.com/jobs") || clientUrl.Contains("dc.tmp.com") || clientUrl.Contains("www.aia.co.uk") || clientUrl.Contains("www.fr.tmp.ca/jobs") || clientUrl.Contains("www.tmp.ca/jobs") || clientUrl.Contains("www.tmp.de/jobs") || clientUrl.Contains("www.tmpworldwideindia.com/jobs") || clientUrl.Contains("www.br.tmp.com/jobs"))
                {
                    clientUrl = clientUrl.Replace("/jobs", "/search-jobs");
                    com_NewLaunchUrl(clientUrl);
                }  //Ramya Added New
                else if (clientUrl.Contains("www.tmpw.com.sg/jobs") || clientUrl.Contains("www.br.latam.tmp.com/jobs") || clientUrl.Contains("newsearch.tmp.com") || clientUrl.Contains("www.tmp.com/jobs") || clientUrl.Contains("dc.tmp.com") || clientUrl.Contains("www.aia.co.uk") || clientUrl.Contains("www.fr.tmp.ca/jobs") || clientUrl.Contains("www.tmp.ca/jobs") || clientUrl.Contains("www.tmp.de/jobs") || clientUrl.Contains("www.tmpworldwideindia.com/jobs") || driver.Url.Contains("www.br.tmp.com/jobs"))
                {
                    clientUrl = clientUrl.Replace("/jobs", "/search-jobs");
                    com_NewLaunchUrl(clientUrl);
                }
                //Ramya added new
                else if (clientUrl.Contains("program-arrivals.disneycareers.com"))
                {
                    clientUrl = "https://program-arrivals.disneycareers.com/search-jobs";
                    com_NewLaunchUrl(clientUrl);
                }
                else
                {
                    clientUrl += "search-jobs/";
                    Thread.Sleep(1000);
                    com_NewLaunchUrl(clientUrl);
                    Thread.Sleep(5000);
                }
                if (driver.Url.Contains("search-jobs"))
                {
                    if ((com_IsElementPresent(TalentBrewUI.searchResultLink)) || (com_IsElementPresent(TalentBrewUI.searchResultLink1)) || (com_IsElementPresent(TalentBrewUI.searchResultLink2)) || (com_IsElementPresent(TalentBrewUI.searchResultLink3)) || (com_IsElementPresent(TalentBrewUI.searchResultLink4)) || (com_IsElementPresent(TalentBrewUI.searchResultLink5)))
                    {
                        //com_VerifyObjPresent(TalentBrewUI.searchResultLink, "Navigating to Search result page || Jobs are present in Search result page", "Navigating to Search result page || Jobs are not present in Search result page");
                        report.AddReportStep("Navigated to Search result page", "Navigated to Search result page", StepResult.PASS);
                        navigateL2 = true;
                    }
                    else
                    {
                        report.AddReportStep("Does not Navigated to Search result page", "Does not Navigated to Search result page", StepResult.FAIL);
                    }
                }
                else
                {
                    report.AddReportStep("Does not Navigated to Search result page", "Does not Navigated to Search result page", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Navigating to search results page", "Exception occured! Does not Navigated to Search result page", StepResult.FAIL);
            }
            //}
            return navigateL2;
        }

        public bool app_navigateL3(string searchKeyword, string clientUrl, By obj)
        {
            bool navigateL3 = false;
            //if (com_VerifyObjPresent(TalentBrewUI.txt_keywordSearch, "Navigating to Search Result page || Keyword Search text box is present in Home Page", "Problem in navigating to Search Result page || Keyword Search text box is not present in Home Page") && com_VerifyObjPresent(TalentBrewUI.btn_Search, "Navigating to Search Result page || Search button is present in Home Page", "Problem in navigating to Search Result page || Search button is not present in Home Page"))
            //{
            //    com_SendKeys(TalentBrewUI.txt_keywordSearch, searchKeyword, "Navigating to Search Result page || Entered the keyword in the Keyword textBox - " + searchKeyword, "Problem in navigating to Search Result page || Problem in entering the keyword in the keyword textbox- " + searchKeyword);
            //    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Navigating to Search Result page || Clicked on Search Button", "Problem in navigating to Search Result page || Problem in Clicking on Search Button");

            clientUrl = clientUrl.Trim();
            //change for newclient Nestle :
            //if (clientUrl.Contains("https://www.nestlewaterscareers.com/search-site"))
            //{
            //    com_NewLaunchUrl("https://www.nestlewaterscareers.com" + "/search-jobs/");
            //}

            //change for newclient Nestle :
            if (clientUrl.Contains("https://www.nestlewaterscareers.com/search-site"))
            {
                clientUrl = clientUrl.Replace("/search-site", "");
            }

            //com_NewLaunchUrl(clientUrl);
            com_NewLaunchUrl(clientUrl + "/search-jobs/");
            System.Threading.Thread.Sleep(3000);
            if (driver.Url.Contains("/search-jobs"))
            {
                if (com_VerifyObjPresent(TalentBrewUI.searchResultLink, "Navigating to Job Description page || Jobs are present in Navigating to Search result page || Jobs are present in Search result page", "Problem in navigating to Job Description page || Jobs are not present in Navigating to Search result page || Jobs are present in Search result page"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink, "Navigating to Job Description page || Clicked on the First Job Link", "Problem in navigating to Job Description page || Problem in clicking the frist job link");
                    //if (com_VerifyObjPresent(TalentBrewUI.applyButton1, "Navigated to Job Description page on Clicking First Link in Search result page", "Does not navigated to Job Description page on Clicking First Link in Search result page"))
                    if (com_IsElementPresent(TalentBrewUI.applyButton1) || com_IsElementPresent(TalentBrewUI.applyButton2))
                    {
                        report.AddReportStep("Navigated to Job Description page on Clicking First Link in Search result page", "Navigated to Job Description page on Clicking First Link in Search result page", StepResult.PASS);
                        com_ClearElement(obj);
                        com_ClearElement(TalentBrewUI.txt_LocationSearch);
                        navigateL3 = true;
                    }
                    else if (com_IsElementPresent(TalentBrewUI.applyButtonwithCaps))
                    {
                        report.AddReportStep("Navigated to Job Description page on Clicking First Link in Search result page", "Navigated to Job Description page on Clicking First Link in Search result page", StepResult.PASS);
                        com_ClearElement(obj);
                        com_ClearElement(TalentBrewUI.txt_LocationSearch);
                        navigateL3 = true;
                    }
                    else
                    {
                        report.AddReportStep("Does not navigated to Job Description page on Clicking First Link in Search result page", "Does not navigated to Job Description page on Clicking First Link in Search result page", StepResult.FAIL);
                    }
                }
                else
                {
                    report.AddReportStep("No Jobs present - Does not Navigated to Job Description page", "No Jobs present - Does not Navigated to Job Description page", StepResult.FAIL);
                }
            }
            else
            {
                report.AddReportStep("Does not Navigated to Job Description page", "Does not Navigated to Job Description page", StepResult.FAIL);
            }
            //}
            return navigateL3;
        }

        public void app_KeywordSearch(string searchKeyword, string clientUrl, By obj, By objectby)
        {
            try
            {
                com_ClearElement(obj);
                Thread.Sleep(2000);

                //mani added

                if (clientUrl.Contains("jobs.deluxe.com") | driver.Url.Contains("jobs.chs-ga.org"))
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,300)");

                com_ClearElement(TalentBrewUI.txt_LocationSearch);

                if (com_VerifyObjPresent(obj, "Basic Search || 'Keyword Search' text box is present", "Basic Search || 'Keyword Search' text box is not present") && com_VerifyObjPresent(objectby, "Basic Search || Search button is present", "Basic Search || Search button is not present"))
                {
                    com_SendKeys(obj, searchKeyword, "Basic Search || Entered the keyword in the 'Keyword textBox' - " + searchKeyword, "Basic Search || Problem in entering the keyword in the 'Keyword textbox'- " + searchKeyword);
                    com_ClearElement(TalentBrewUI.txt_LocationSearch);
                    Thread.Sleep(1000);
                    if (com_IsElementPresent(objectby))
                        com_ClickOnInvisibleElement(objectby, "Basic Search || Clicked on Search Button", "Basic Search || Problem in Clicking on Search Button");
                    //Thread.Sleep(3000);

                    WaitForObject(TalentBrewUI.l2_jobTitle3, 100);

                    if (com_IsElementPresent(TalentBrewUI.l2_jobTitle3))
                        com_ContainsTxt(TalentBrewUI.l2_jobTitle3, searchKeyword, "Basic Search || Result Title in Search Result page is matching with Search Keyword", "Basic Search || Result Title in Search Result page is not matching with Search Keyword");

                    else if (com_IsElementPresent(TalentBrewUI.l2_jobTitle))
                        com_ContainsTxt(TalentBrewUI.l2_jobTitle, searchKeyword, "Basic Search || Result Title in Search Result page is matching with Search Keyword", "Basic Search || Result Title in Search Result page is not matching with Search Keyword");
                    else if (com_IsElementPresent(TalentBrewUI.l2_jobtitle1))
                        com_ContainsTxt(TalentBrewUI.l2_jobtitle1, searchKeyword, "Basic Search || Result Title in Search Result page is matching with Search Keyword", "Basic Search || Result Title in Search Result page is not matching with Search Keyword");

                    if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Basic Search || Result panel is displayed for Keyword Search", "Basic Search || Result panel is not displayed for Keyword search"))
                    {
                        string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                        if (!String.IsNullOrEmpty(jobcount))
                            report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);

                        else
                            report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);

                        //com_ContainsTxt(TalentBrewUI.l2_jobTitle,searchKeyword, "Basic Search || Result Title in Search Result page is matching with Search Keyword", "Basic Search || Result Title in Search Result page is not matching with Search Keyword");
                        if (jobcount != "0")
                        {
                            if (driver.Url.Contains("greeneking.co.uk"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.SearchResultLink4, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.SearchResultLink4, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                                //Thread.Sleep(2000);
                            }

                                        //mani added
                            else if (driver.Url.Contains("jobs.deluxe.com") | driver.Url.Contains("parksjobs.disneycareers.com") | driver.Url.Contains("jobs.chs-ga.org") | driver.Url.Contains("emploi.burgerking.fr"))
                            {
                                if (driver.Url.Contains("jobs.chs-ga.org"))
                                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultJobLink, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");

                                if (com_IsElementPresent(TalentBrewUI.searchResultLink1))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                                }
                                else
                                {
                                    report.AddReportStep("No Results are displayed", "No Results are displayed", StepResult.FAIL);
                                }

                            }
                            else if (driver.Url.Contains("sutterhealth"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink1, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                            }
                            //Mani added
                            else if (clientUrl.Contains("chinajobs.disneycareers.com"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink3, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink3, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                            }
                            else if (driver.Url.Contains("careers.enterprise.ca"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink1, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                            }
                            //kk
                            else if (driver.Url.Contains("jobs.hackensackumc.org"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink1, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                            }
                            //kk
                            else if (driver.Url.Contains("jobs.disneycareers.com"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink4, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink4, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                            }
                            //kk
                            else if (driver.Url.Contains("jobs.boystown.org"))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink1, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                            }

                            else if (com_IsElementPresent(TalentBrewUI.searchResultLink))
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.searchResultLink, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                                //Thread.Sleep(2000);
                            }
                            else if (com_IsElementPresent(TalentBrewUI.searchResultLink1))
                            {
                                com_VerifyObjPresent(TalentBrewUI.searchResultLink1, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink1, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                                //Thread.Sleep(2000);
                            }
                            else if (com_IsElementPresent(TalentBrewUI.searchResultLink3))
                            {
                                com_VerifyObjPresent(TalentBrewUI.searchResultLink3, "Basic Search || Jobs are displayed in Job detail page", "Basic Search || No Jobs are displayed in Job detail page");
                                com_ClickOnInvisibleElement(TalentBrewUI.searchResultLink3, "Basic Search || Clicked on First Job Link in Search Result page", "Basic Search || Problem in Clicking the First Job Link in Search Result page");
                                //Thread.Sleep(2000);
                            }
                            else
                                report.AddReportStep("No Jobs are displayed", "No Jobs are present for this search", StepResult.WARNING);

                            if (com_IsElementPresent(TalentBrewUI.applyButton1) || com_IsElementPresent(TalentBrewUI.applyButton2))
                            {
                                if (com_IsElementPresent(TalentBrewUI.applyButton1))
                                    report.AddReportStep("Navigated to Job Description page", "Basic Search || Navigated to Job Description page on Clicking First Link in Search result page", StepResult.PASS);
                                //com_VerifyObjPresent(TalentBrewUI.applyButton1, "Basic Search || Navigated to Job Description page on Clicking First Link in Search result page", "Basic Search || Does not navigated to Job Description page on Clicking First Link in Search result page");

                                else if (com_IsElementPresent(TalentBrewUI.applyButton2))
                                    report.AddReportStep("Navigated to Job Description page", "Basic Search || Navigated to Job Description page on Clicking First Link in Search result page", StepResult.PASS);
                                //com_VerifyObjPresent(TalentBrewUI.applyButton2, "Basic Search || Navigated to Job Description page on Clicking First Link in Search result page", "Basic Search || Does not navigated to Job Description page on Clicking First Link in Search result page");
                                else
                                    report.AddReportStep("Does not Navigated to Job Description page", "Basic Search || Does not Navigated to Job Description page on Clicking First Link in Search result page", StepResult.WARNING);

                                //if (driver.Url.Contains("careers.vmware.com"))
                                //{
                                //    report.AddScenarioHeader("Your Saved Jobs");
                                //    if (com_VerifyObjPresent(TalentBrewUI.Btn_SaveJob, "Your saved jobs|| 'Save This Job' button is available", " Your saved jobs|| 'Save This Job' button is not available"))
                                //    {

                                //        if (com_ClickOnInvisibleElement(TalentBrewUI.Btn_SaveJob, "Your saved jobs || Clicked on 'Save this Job' button", "Save Your Job || Problem in clicking the 'Save this Job' button"))
                                //        {

                                //            com_VerifyObjPresent(TalentBrewUI.Btn_RemoveJob, "Your saved jobs || The job is saved and remove button is enabled", " Save Your Search || The job is not saved and remove button is not enabled");
                                //        }
                                //        com_NewLaunchUrl(clientUrl);
                                //        if (com_VerifyObjPresent(TalentBrewUI.sec_SavedJobs, "Your saved Jobs || Your saved Jobs module is available", "Your saved Jobs || Your saved Jobs module is not available"))
                                //        {
                                //            if (com_VerifyObjPresent(TalentBrewUI.lnk_savedJobs, "Your saved Jobs || Your saved Job is available under the section -'Your saved jobs' in the Home page", "Your saved Jobs are not available under the section -'Your saved jobs' in the Home page"))
                                //            {
                                //                com_ClickOnInvisibleElement(TalentBrewUI.lnk_savedJobs, "Your saved Jobs || Clicked on the Saved Job under the section -'Your saved jobs' in the Home page", "Your saved Jobs || Problem in clicking the Saved Jobs under the section -'Your saved jobs' in the Home page");
                                //                if (com_IsElementPresent(TalentBrewUI.applyButton1) || com_IsElementPresent(TalentBrewUI.applyButton2))
                                //                {
                                //                    report.AddReportStep("Your saved Jobs || Navigated to Job Description page on Clicking the 'Saved Job' in Search result page", "Navigated to Job Description page on Clicking the 'Saved Job' under the section -'Your saved jobs'", StepResult.PASS);
                                //                }
                                //                else
                                //                {
                                //                    report.AddReportStep("Your saved Jobs || Does not navigated to Job Description page on Clicking saved job Link under the section -'Your saved jobs' in the Home page ", "Does not navigated to Job Description page on Clicking saved job Link under the section -'Your saved jobs' in the Home page ", StepResult.FAIL);
                                //                }
                                //                if (com_VerifyObjPresent(TalentBrewUI.Btn_RemoveJob, "Your Saved Jobs || 'Remove Job' button is available in Job description page", "Your Saved Jobs || 'Remove Job' button is not available in Job description page"))
                                //                {
                                //                    if (com_ClickOnInvisibleElement(TalentBrewUI.Btn_RemoveJob, "Your Saved Jobs || Clicked on 'Remove Job' button", "Your Saved Jobs || Problem in clicking the'Remove Job' button"))
                                //                    {
                                //                        com_VerifyObjPresent(TalentBrewUI.Btn_SaveJob, "Your saved jobs || The job is removed and 'Save' button is enabled", " Save Your Search || The job is not removed and 'Save' button is not enabled");
                                //                    }
                                //                    com_NewLaunchUrl(clientUrl);
                                //                    if (com_VerifyObjPresent(TalentBrewUI.sec_SavedJobs, "Your saved Jobs || Your saved Jobs module is available", "Your saved Jobs || Your saved Jobs module is not available"))
                                //                    {
                                //                        if (com_IsElementPresent(TalentBrewUI.lnk_noJobs))
                                //                        {
                                //                            report.AddReportStep("Your Saved Jobs || No jobs is available under the section -'Your saved jobs' in the Home page", "Your Saved Jobs || No jobs are available under the section -'Your saved jobs' in the Home page", StepResult.PASS);
                                //                        }
                                //                        else
                                //                        {
                                //                            report.AddReportStep("Your Saved Jobs || Job is available under the section -'Your saved jobs' in the Home page even after removing the job", "Your Saved Jobs || Job is available under the section -'Your saved jobs' in the Home page even after removing the job", StepResult.FAIL);
                                //                        }

                                //                    }
                                //                }

                                //            }
                                //        }
                                //    }
                                //}
                            }
                            else
                            {
                                com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "Basic Search || Navigated to Job Description page on Clicking First Link in Search result page", "Basic Search || Does not navigated to Job Description page on Clicking First Link in Search result page");
                                report.AddReportStep("Apply button is not present in the Job Description Page", "Apply button is not present in the Job Description Page", StepResult.WARNING);
                            }
                        }
                        else
                            com_NewLaunchUrl(clientUrl);
                    }
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Basic Search || Expection Occured : " + e.ToString(), "Basic Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }

        }

        public void app_basicSearch(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Basic Search");
            try
            {
                string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
                //ramya Added New
                if (clientUrl.Contains("program-arrivals.disneycareers.com"))
                    clientUrl = "https://program-arrivals.disneycareers.com/search-jobs";

                else if (clientUrl.Contains("dpdhl-jobwatch"))
                    clientUrl = "https://www.dpdhl.jobs/";

                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {

                    com_NewLaunchUrl(clientUrl);
                    if (driver.Url.Contains("jobs.mountcarmelhealth.com"))
                    {
                        if (com_IsElementPresent(TalentBrewUI.joinOurTalent_Popup))
                            com_ClickOnInvisibleElement(TalentBrewUI.closeButton_TalentPop, "Clicked on Join Our Talent Pop up close button", "Unable to click on the Join Our Talent Pop up close button");
                    }

                    //mani added
                    if (clientUrl.Contains("parksjobs.disneycareers.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//p[@href='/disneyland-resort']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//p[@href='/disneyland-resort']"), "Redirection link || Clicked on redirection link", "Redirection link || Problem in Clicking on redirection link");
                        }
                    }
                    if (ScenarioName.Equals("L1"))
                    {
                        if (driver.Url.Contains("careers.jobsataramco.eu"))
                            com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                        if (driver.Url.Contains("jobs.sungardas.com"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs3, "Clicked on Search Jobs button", "Unable to click on Search Jobs button");

                        if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                        //Ramya Added latest
                        if (driver.Url.Contains("jobs.cooperhealth.org"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs2, "Clicked on 'External Job Search' button", "Unable to click on 'External Job Search' Button");
                        }


                        //  if (driver.Url.Contains("pattersoncompanies.com"))
                        // com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                        if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("huskyenergy") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                        //Mani added
                        if (driver.Url.Contains("explore.lockheedmartinjobs.com"))
                        {
                            if (com_IsElementPresent(By.XPath("//a[contains(@class,'global-search')]")))
                            {
                                com_ClickOnInvisibleElement(By.XPath("//a[contains(@class,'global-search')]"), "Clicked on Search Button", "Problem in clicking search button");
                            }
                        }


                        if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                        if (driver.Url.Contains("pgcareers.com"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        }
                        //Ramya Added
                        if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                            Thread.Sleep(2000);
                        }
                        if (driver.Url.Contains("jobs.greatwolf.com"))
                        {
                            if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                            {
                                com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                            }
                            Thread.Sleep(2000);
                        }
                        //Ramya Added New
                        if (driver.Url.Contains("jobs.delltechnologies.com"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        }

                        if (driver.Url.Contains("careers.genpact.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch1, TalentBrewUI.btn_Search1);
                        }
                        //Ramya Added New
                        else if (driver.Url.Contains("armatis.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.Keyword_txt_new, TalentBrewUI.btn_Search5);

                        }
                        //Mani added

                        else if (driver.Url.Contains("explore.lockheedmartinjobs.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.keywordBoxNew, TalentBrewUI.searchButtonnew2);
                        }


                      //Ramya Added New
                        else if (driver.Url.Contains("fr.jobs-ups.ca"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs_new_2, "Clicked on 'Explore Jobs' button", "Unable to click on 'Explore Jobs' Button");
                            Thread.Sleep(2000);
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch2, TalentBrewUI.btn_Search);
                        }

                            //Ramya Added New
                        else if (driver.Url.Contains("utc.com"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            Thread.Sleep(2000);
                            if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                            {
                                if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            }
                            Thread.Sleep(2000);
                            //Ramya Added
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search3);
                        }
                        else if (driver.Url.Contains("sutterhealth"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs3, "Clicked on 'Find a career' button", "Unable to click on 'Find a career' Button");
                            Thread.Sleep(2000);
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search);

                        }
                        else if (driver.Url.Contains("jobs.greeneking.co.uk"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Explore Jobs' button", "Unable to click on 'Explore Jobs' Button");
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search);
                        }

                        else if (driver.Url.Contains("capitalonecareers.co.uk"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.btn_search2))
                                app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch, TalentBrewUI.btn_search2);
                        }
                        else if (driver.Url.Contains("jobs.atos.net"))
                        {
                            WaitForObject(TalentBrewUI.btn_Explore, 100);
                            Thread.Sleep(2000);
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_search3);
                        }
                        //Ramya Added
                        else if (driver.Url.Contains("jobs-ups") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("jobs.cornerstonebrands.com"))
                        {

                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Explore Jobs' button", "Unable to click on 'Explore Jobs' Button");
                            Thread.Sleep(2000);
                            //Ramya Added
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch2, TalentBrewUI.btn_Search3);
                        }
                        //Ramya Added
                        else if (driver.Url.Contains("utc.com"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            Thread.Sleep(2000);
                            //Ramya Added Latest
                            if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Explore Jobs' button", "Unable to click on 'Explore Jobs' Button");
                            Thread.Sleep(2000);
                            //Ramya Added
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search3);
                        }
                        //Ramya Added
                        else if (driver.Url.Contains("jobs.santanderbank.com"))
                        {

                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search3);
                        }


                        else if (driver.Url.Contains("jobs.gartner.com"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search);
                        }
                        else if (driver.Url.Contains("jobs.capitalone.co.uk"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch4, TalentBrewUI.btn_search4);
                        }

                        else if (driver.Url.Contains("nespressojobs.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_searchjobs1);
                        }
                        else if (driver.Url.Contains("emploi.thalesgroup.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search1);
                        }
                        else if (driver.Url.Contains("careers.mcafee.com"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                            app_KeywordSearch(searchKeyword, clientUrl, By.XPath(" (//input[@name='k'])[2]"), By.XPath("(//button[contains(@id,'search-submit')])[2]"));
                            Thread.Sleep(2000);

                        }//Ramya Added New
                        else if (driver.Url.Contains("scotiabank.com") || driver.Url.Contains("jobs.cdc.gov") || driver.Url.Contains("jobs.delltechnologies.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search_New);
                        }
                        //Ramya Added New
                        else if (driver.Url.Contains("careers.adeccogroup.com"))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, By.XPath("(//input[@name='k'])[2]"), By.XPath("(//button[contains(@id,'search-submit')])[2]"));
                        }
                        else if (com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search);

                        else if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch1))
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch1, TalentBrewUI.btn_Search);

                        else if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch))
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch, TalentBrewUI.btn_Search);
                        else if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch2))
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch2, TalentBrewUI.btn_Search);
                        else if ((com_IsElementPresent(TalentBrewUI.txt_keywordSearch6)) || (com_IsElementPresent(TalentBrewUI.btn_search3)))
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch6, TalentBrewUI.btn_search3);
                        else
                            report.AddReportStep("Keyword textbox is not available", "Keyword textbox is not available", StepResult.FAIL);
                    }

                    else if (ScenarioName.Equals("L2"))
                    {
                        if (app_navigateL2(searchKeyword, clientUrl))
                        {
                            //MAni added-Chinajobs and chrobsion
                            if (clientUrl.Contains("jobs.nike.com") | clientUrl.Contains("chinajobs.disneycareers.com") | clientUrl.Contains("jobs.chrobinson.com") | clientUrl.Contains("internalcareers.bbcworldwide.com"))
                            {
                                app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordsearch3, TalentBrewUI.btn_Search);
                            }
                            else
                            {
                                app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch, TalentBrewUI.btn_Search);
                            }

                        }

                    }
                    else if (ScenarioName.Equals("L3"))
                    {
                        if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                        {
                            app_KeywordSearch(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch, TalentBrewUI.btn_Search);
                        }
                    }
                }
                else
                {
                    report.AddReportStep("Basic Search is not avaiable", "Basic Search is not avaiable", StepResult.WARNING);
                }

            }
            catch (Exception e)
            {

            }
        }

        public void app_Filter(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Filter Module");
            try
            {
                string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    com_NewLaunchUrl(clientUrl);
                    if (ScenarioName.Equals("L2"))
                    {
                        if (app_navigateL2(searchKeyword, clientUrl))
                        {
                            if (clientUrl.Contains("utc.com") || clientUrl.Contains("jobs.apac.utc") || clientUrl.Contains("jobs.utcaerospacesystems.com"))
                            {
                                if (com_IsElementPresent(TalentBrewUI.Filter_button9))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.Filter_button9, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                                }
                            }
                            else if (driver.Url.Contains("santanderbank"))
                            {
                                Thread.Sleep(3000);
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button1, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                                Thread.Sleep(2000);
                            }
                            else if (driver.Url.Contains("nespressojobs"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button7, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                                Thread.Sleep(2000);
                            }

                            else if (driver.Url.Contains("jobs.lindtusa"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button6, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                                Thread.Sleep(2000);
                            }

                            else if (driver.Url.Contains("jobs.premisehealth"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button5, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                            }

                            else if (driver.Url.Contains("technicolor"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                            }
                            else if (driver.Url.Contains("careers.bbc"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button4, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                                Thread.Sleep(3000);
                            }

                            else if (clientUrl.Contains("jobs.capitalone.co.uk"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button3, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                            }

                            else if (clientUrl.Contains("usa.jobs.scotiabank.com"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button8, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                            }

                            else if (driver.Url.Contains("jobs.mvwcareers.com"))
                            {
                                if (com_IsElementPresent(TalentBrewUI.Accept_cookie))
                                    driver.FindElement(TalentBrewUI.Accept_cookie).Click();
                            }
                            else if (driver.Url.Contains("careers.rgp.com"))
                            {
                                com_ClickOnInvisibleElement(By.Id("filter-slideout-toggle"), "Click on Show Filters button", "Clicked on Show Filters button");
                            }
                            else if (driver.Url.Contains("www.nestleusacareers.com"))
                            {
                                //string a = com_GetText(TalentBrewUI.Filter_toggle);
                                if (!com_IsElementPresent(By.Id("keyword-tag")))
                                    com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                Thread.Sleep(3000);
                                if (!com_IsElementPresent(By.Id("keyword-tag")))
                                    com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                Thread.Sleep(3000);
                            }

                            if (clientUrl.Contains("jobs.deluxe.com") | driver.Url.Contains("jobs.chs-ga.org"))
                                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");

                            //Getting the job count to confirm
                            try
                            {
                                string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");

                                if ((!String.IsNullOrEmpty(jobcount)) && (!jobcount.Equals("0")))
                                {
                                    report.AddReportStep("Job count - " + jobcount, "Job count - " + jobcount + " is displayed", StepResult.PASS);
                                    //Filter module starts
                                    if ((com_IsElementPresent(TalentBrewUI.chk_categoryToggle)) || (com_IsElementPresent(TalentBrewUI.chk_categoryToggle1)) || (com_IsElementPresent(TalentBrewUI.categoryToggleNew)) || (com_IsElementPresent(TalentBrewUI.chk_countryToggle)) || (com_IsElementPresent(TalentBrewUI.chk_institutionToggle)) || (com_IsElementPresent(TalentBrewUI.chk_regionToggle)) || (com_IsElementPresent(TalentBrewUI.chk_cityToggle)) || (com_IsElementPresent(TalentBrewUI.categoryToggle2)) || (com_IsElementPresent(TalentBrewUI.chk_regionToggle1)) || (com_IsElementPresent(TalentBrewUI.chk_cityToggle1)))
                                    {
                                        if ((com_IsElementPresent(TalentBrewUI.chk_categoryToggle)) || (com_IsElementPresent(TalentBrewUI.chk_categoryToggle1)) || (com_IsElementPresent(TalentBrewUI.categoryToggleNew)) || (com_IsElementPresent(TalentBrewUI.categoryToggle2)))
                                        {
                                            if (clientUrl.Contains("jobsatnovanthealth"))
                                                app_FilterSearch("Category", TalentBrewUI.chk_categoryToggle, TalentBrewUI.chk_category_novan);
                                            else if (clientUrl.Contains("jobs.chipotle.com"))
                                                app_FilterSearch("Category", TalentBrewUI.chk_categoryToggle, TalentBrewUI.chk_category2);
                                            else if (clientUrl.Contains("jobs.sleepnumber.com"))
                                                app_FilterSearch("Category", TalentBrewUI.categoryToggle2, TalentBrewUI.chk_category1);
                                            else if (com_IsElementPresent(TalentBrewUI.chk_categoryToggle))
                                                app_FilterSearch("Category", TalentBrewUI.chk_categoryToggle, TalentBrewUI.chk_category1);
                                            else if (com_IsElementPresent(TalentBrewUI.chk_categoryToggle1))
                                                app_FilterSearch("Category", TalentBrewUI.chk_categoryToggle1, TalentBrewUI.chk_category3);

                                            else if (com_IsElementPresent(TalentBrewUI.categoryToggleNew))
                                                app_FilterSearch("Category", TalentBrewUI.categoryToggleNew, TalentBrewUI.newToggleCat1);

                                        }

                                        if (driver.Url.Contains("jobs.sleepnumber.com"))
                                        {
                                            app_FilterSearch("State", TalentBrewUI.chk_regionToggle1, TalentBrewUI.chk_region1);
                                            app_FilterSearch("City", TalentBrewUI.chk_cityToggle1, TalentBrewUI.chk_city1);
                                        }
                                        else
                                            app_FilterSearch("Country", TalentBrewUI.chk_countryToggle, TalentBrewUI.chk_country1);
                                        app_FilterSearch("State", TalentBrewUI.chk_regionToggle, TalentBrewUI.chk_region1);
                                        app_FilterSearch("City", TalentBrewUI.chk_cityToggle, TalentBrewUI.chk_city1);

                                        if (driver.Url.Contains("usccareers.usc.edu"))
                                        {
                                            app_FilterSearch("Institution", TalentBrewUI.chk_institutionToggle, TalentBrewUI.chk_institution1);
                                            app_FilterSearch("Campus", TalentBrewUI.chk_campusToggle, TalentBrewUI.chk_campus1);
                                        }
                                        else { }
                                    }
                                    else
                                    {
                                        report.AddReportStep("Filter section is not available", "Filter section is not available", StepResult.FAIL);
                                    }
                                }
                                else
                                {
                                    report.AddReportStep("Filter || No Jobs are present", "Filter || No Jobs are present", StepResult.WARNING);
                                }
                            }
                            catch (Exception e)
                            {
                                report.AddReportStep("Filter || Problem in getting the jobs list", "Filter || Problem in getting the jobs list", StepResult.WARNING);
                            }


                        }
                    }
                }
                else
                {
                    report.AddReportStep("Filter module", "Filter module  is not avaiable", StepResult.WARNING);
                }

            }
            catch (Exception e) { }
        }

        public void app_AdvanceSearch(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Advanced Search");
            clientUrl = clientUrl.Trim();
            if (clientUrl.Contains("events.memorialhermann.org"))
                clientUrl = "https://jobs.memorialhermann.org/";

            else if (clientUrl.Contains("program-arrivals.disneycareers.com"))
                clientUrl = "https://program-arrivals.disneycareers.com/search-jobs";

            else if (clientUrl.Contains("dpdhl-jobwatch"))
                clientUrl = "https://www.dpdhl.jobs/";

            try
            {
                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
                    string searchLocation = string.Empty;
                    // bool jobsearchresults = app_navigateL2(searchKeyword, clientUrl);
                    //Ramya Added New
                    bool jobsearchresults = false;
                    if (!driver.Url.Contains("jobs.marksandspencer.com"))
                    {
                        jobsearchresults = app_navigateL2(searchKeyword, clientUrl);
                    }
                    else
                    {
                        searchLocation = dataHelper.GetData(DataColumn.JobMatchingLocationSearch);
                    }
                    if (jobsearchresults)
                    {
                        if (clientUrl.Contains("jobs.capitalone.co.uk"))
                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_button3, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");

                        else if (clientUrl.Contains("careers.bbcworldwide.com"))
                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_button4, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");

                        else if (clientUrl.Contains("jobs.chrobinson.com"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_filter, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");

                        else if (clientUrl.Contains("usa.jobs.scotiabank.com"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_button8, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                            Thread.Sleep(2000);
                        }

                        else if (clientUrl.Contains("utc.com") || clientUrl.Contains("jobs.apac.utc") || clientUrl.Contains("jobs.utcaerospacesystems.com"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.Filter_button9))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button9, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                            }
                        }

                        if (com_IsElementPresent(TalentBrewUI.chk_cityToggle))
                        {
                            if (!com_getobjProperty(TalentBrewUI.chk_cityToggle, "class").Contains("child-open"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.chk_cityToggle, "Clicked on city to fetch the city", "Problem in clicking the city to fetch the city");
                                Thread.Sleep(3000);
                            }
                            if (com_IsElementPresent(TalentBrewUI.chk_city1))
                                searchLocation = com_getobjProperty(TalentBrewUI.chk_city1, "data-display");


                            else if (com_IsElementPresent(TalentBrewUI.chk_city2))
                                searchLocation = com_GetText(TalentBrewUI.chk_city2);
                            //Ramya Added
                            else if (com_IsElementPresent(By.XPath("//*[contains(@id, 'city-filter')]")))
                                searchLocation = com_getobjProperty(By.XPath("//*[contains(@id, 'city-filter')]"), "data-display");


                        }
                        else
                        {
                            report.AddReportStep("Advanced search || Cannot select city from filter module hence fetching it from test data sheet", "Advanced search || Cannot select city from filter module hence fetching it from test data sheet", StepResult.WARNING);
                            searchLocation = dataHelper.GetData(DataColumn.JobMatchingLocationSearch);
                        }

                    }
                    //searchLocation = dataHelper.GetData(DataColumn.SearchLocation);

                    if (!string.IsNullOrWhiteSpace(searchLocation))
                    {

                        if (searchLocation.Contains("/"))
                        {
                            searchLocation = dataHelper.GetData(DataColumn.JobMatchingLocationSearch);
                        }
                        else
                        {
                            // searchLocation = searchLocation.Split(',')[0].ToString();
                            String[] splitlocations = null;

                            splitlocations = searchLocation.Split(',');
                            searchLocation = splitlocations[0];

                            if (searchLocation.Contains("‘"))
                            {
                                if (!driver.Url.Contains("sutterhealth"))
                                    searchLocation = dataHelper.GetData(DataColumn.JobMatchingLocationSearch);
                            }
                        }
                        //Ramya Added
                        if (driver.Url.Contains("parexel.co.jp"))
                            searchLocation = searchLocation.Substring(0, 3);
                        else if (driver.Url.Contains("sutterhealth.org"))
                            searchLocation = searchLocation.Substring(1, 3);
                        else if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                            searchLocation = searchLocation.Substring(0, 5);
                        else if (driver.Url.Contains("jobs.aarons.com"))
                            searchLocation = "New York, NY";

                        com_NewLaunchUrl(clientUrl);
                        if (clientUrl.Contains("jobs.nike.com"))
                        {
                            clientUrl = clientUrl + "corporate";
                            com_NewLaunchUrl(clientUrl);
                        }

                        if (ScenarioName.Equals("L1"))
                        {
                            
                           // added Enhance advance feature
                            
                           app_LocationSearch(searchKeyword, searchLocation, clientUrl);

                        }
                        else if (ScenarioName.Equals("L2"))
                        {
                            if (app_navigateL2(searchKeyword, clientUrl))
                                app_LocationSearch(searchKeyword, searchLocation, clientUrl);
                        }
                        else if (ScenarioName.Equals("L3"))
                        {
                            if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                                app_LocationSearch(searchKeyword, searchLocation, clientUrl);
                        }
                    }
                    else
                    {
                        if (jobsearchresults == false && com_IsElementPresent(TalentBrewUI.Noresults_Link))
                        {
                            report.AddReportStep("No jobs are available for this client", "No jobs  are Available for this client", StepResult.FAIL);
                        }
                        else
                        {
                            report.AddReportStep("Cannot fetch the first city value from the filter module- Advance Search cannot be performed", "Cannot fetch the first city value from the filter module- Advance Search cannot be performed", StepResult.FAIL);
                        }
                    }
                }
                else
                {
                    report.AddReportStep("Advance Search is not available", "Advance Search is not available", StepResult.WARNING);
                }

            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
        }

        public void deleteAllCookies()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            report.AddReportStep("Advance Search || Cookies", "Advance Search || Deleted all the cookies", StepResult.PASS);
        }

        public void app_LocationSearch(string searchKeyword, string searchLocation, string clientUrl)
        {
            try
            {
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || (driver.Url.Contains("utc.com") && !driver.Url.Contains("jobs.otis.utc.com")))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                //Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }

                 //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                else if (driver.Url.Contains("careers.mcafee.com"))
                {
                    com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                    Thread.Sleep(2000);
                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                if (driver.Url.Contains(".citi."))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");
                if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("jobs.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);
                if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                Thread.Sleep(3000);
                
                    By obj;
                    if (com_IsElementPresent(TalentBrewUI.txt_LocationSearch) || com_IsElementPresent(TalentBrewUI.txt_locSearch))
                    {
                        if ((com_IsElementPresent(TalentBrewUI.btn_Search)) || com_IsElementPresent(TalentBrewUI.btn_Search7) || (com_IsElementPresent(TalentBrewUI.btn_Search1)) || (com_IsElementPresent(TalentBrewUI.btn_search2)) || (com_IsElementPresent(TalentBrewUI.btn_Search3)) || (com_IsElementPresent(TalentBrewUI.btn_search4)) || (com_IsElementPresent(TalentBrewUI.btn_Search5)) || (com_IsElementPresent(TalentBrewUI.btn_search6)) || (com_IsElementPresent(TalentBrewUI.btn_Search3)))
                        {
                            string tempURL = driver.Url;
                            int radiusSize = 1;
                            if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                            {
                                TalentBrewUI.lst_Radius = By.XPath("(//*[@name='r'])[2]");
                                obj = By.XPath("(//*[@name='k'])[2]");
                            }
                            //Ramya Added                  
                            if (!driver.Url.Contains("jobs.marksandspencer.com") && !driver.Url.Contains("jobs.coop.co.uk") && !driver.Url.Contains("jobs.chla.org") && !driver.Url.Contains("bmwgroupretailcareers.co.uk") && !driver.Url.Contains("www.att.jobs") && !driver.Url.Contains("internalcareers.mnscorp.net") && !driver.Url.Contains("jobs.sanofi.us") && !driver.Url.Contains("ikea.com") && !driver.Url.Contains("chipotle.com") && !driver.Url.Contains("jobs.yp.ca") && !driver.Url.Contains("jobs.memorialcare.org") && !driver.Url.Contains("emploi.burgerking.fr") && !driver.Url.Contains("jobs.memorialhermann.org") && !driver.Url.Contains("connect.att.jobs") && !driver.Url.Contains("carrieres.pj.ca") && !driver.Url.Contains("emc.com") && !driver.Url.Contains("pihhealth.org") && !driver.Url.Contains("jobs.greeneking.co.uk") && !driver.Url.Contains("jobs.three.co.uk") && !driver.Url.Contains("employees.kaiserpermanentejobs.org") && !driver.Url.Contains("www.kaiserpermanentejobs.org") && !driver.Url.Contains("jobs.mcleodhealth.org") && !driver.Url.Contains("interiorhealth.ca") && !driver.Url.Contains("emplois.vinci-energies.com") && !driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                            {
                                //Ramya Added
                                if (!driver.Url.Contains("jobs.ebayinc.com"))
                                {
                                    com_VerifyObjPresent(TalentBrewUI.lst_Radius, "Advance Search || Radius ListBox is present", "Advance Search || Radius ListBox is not present");
                                }
                                radiusSize = app_Location_radius_Search(searchKeyword, searchLocation, tempURL, radiusSize);
                            }
                            else
                                report.AddReportStep("Advance Search || Radius List box is not available", "Advance Search || Radius List box is not available", StepResult.WARNING);

                            if ((com_getobjProperty(TalentBrewUI.txt_keywordSearch, "class")).Contains("keyword"))
                                obj = TalentBrewUI.txt_keywordSearch;

                            else
                                obj = TalentBrewUI.txt_keywordsearch3;

                          app_Key_Loc_Search(searchKeyword, searchLocation, tempURL, obj);                          
                           app_Location_Search(searchLocation, tempURL, obj);
                           //Ramya Added
                           if (!driver.Url.Contains("jobs.marksandspencer.com") && !driver.Url.Contains("jobs.chla.org") && !driver.Url.Contains("bmwgroupretailcareers.co.uk") && !driver.Url.Contains("www.att.jobs") && !driver.Url.Contains("internalcareers.mnscorp.net") && !driver.Url.Contains("jobs.sanofi.us") && !driver.Url.Contains("ikea.com") && !driver.Url.Contains("chipotle.com") && !driver.Url.Contains("jobs.memorialcare.org") && !driver.Url.Contains("jobs.yp.ca") && !driver.Url.Contains("emploi.burgerking.fr") && !driver.Url.Contains("jobs.memorialhermann.org") && !driver.Url.Contains("connect.att.jobs") && !driver.Url.Contains("carrieres.pj.ca") && !driver.Url.Contains("emc.com") && !driver.Url.Contains("pihhealth.org") && !driver.Url.Contains("jobs.three.co.uk") && !driver.Url.Contains("jobs.greeneking.co.uk") && !driver.Url.Contains("employees.kaiserpermanentejobs.org") && !driver.Url.Contains("www.kaiserpermanentejobs.org") && !driver.Url.Contains("jobs.mcleodhealth.org") && !driver.Url.Contains("interiorhealth.ca") && !driver.Url.Contains("emplois.vinci-energies.com") && !driver.Url.Contains("emplois.vinci-construction.com") & !driver.Url.Contains("stellenangebote.vinci-energies.com"))
                            {
                                com_NewLaunchUrl("https://www.google.co.in");
                                com_NewLaunchUrl(tempURL);
                             app_Key_Loc_Radius_Search(searchKeyword, searchLocation, tempURL, radiusSize, obj);
                            }
                            else
                                report.AddReportStep("Advance Search || Radius List box is not available", "Advance Search || Radius List box is not available", StepResult.WARNING);
                            //Mani added--QAA-798
                            app_Key_Loc_SearchNew(tempURL, obj);
                    }
                    }
                    else
                    {
                        if (driver.Url.Contains("careers.bbcworldwide.com"))
                        {
                            app_navigateL2(searchKeyword, clientUrl);
                            com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on Advance Search expand button", "Unable to click on Advance Search expand button");

                        }
                        else if (driver.Url.Contains("jobs.gartner.com"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsAdvance, "clicked on search jobs button", "unable to click search jobs button");

                        app_AdvanceSearchSpl(clientUrl);

                        if (driver.Url.Contains("careers.bbcworldwide.com"))
                        {
                            app_navigateL2(searchKeyword, clientUrl);
                            com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on Advance Search expand button", "Unable to click on Advance Search expand button");
                        }
                        else if (driver.Url.Contains("jobs.gartner.com"))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsAdvance, "clicked on search jobs button", "unable to click search jobs button");
                        else if (driver.Url.Contains("careers.mcafee.com"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                            Thread.Sleep(2000);
                        }

                        if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch))
                            app_AdvanceSearchSpl_key(clientUrl, searchKeyword, TalentBrewUI.txt_keywordSearch, TalentBrewUI.btn_Search);
                        else if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch1))
                            app_AdvanceSearchSpl_key(clientUrl, searchKeyword, TalentBrewUI.txt_keywordSearch1, TalentBrewUI.btn_Search1);
                        //Ramya Added
                        else if ((com_IsElementPresent(TalentBrewUI.Keyword_txt_new)))
                            app_AdvanceSearchSpl_key(clientUrl, searchKeyword, TalentBrewUI.Keyword_txt_new, TalentBrewUI.btn_Search5);

                    }
            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
        }

        private IList<IWebElement> com_getListsOfobjectsTagname(By obj, String childname)
        {
            try
            {
                IList<IWebElement> listOfObject = null;
                IWebElement parentObj = driver.FindElement(obj);
                listOfObject = parentObj.FindElements(By.TagName(childname));
                return listOfObject;


            }
            catch (Exception w)
            {
                return null;
            }
        }

        private void app_AdvanceSearchSpl(string clientURL)
        {
            try
            {
                if (!driver.Url.Contains("careers.bbcworldwide.com") && !driver.Url.Contains(".citi.") && !driver.Url.Contains("utc.com") && !driver.Url.Contains("jobs.gartner.com"))
                    com_NewLaunchUrl(clientURL);
                if (driver.Url.Contains("jobs.gartner.com") || driver.Url.Contains("jobs.memorialhermann.org") || driver.Url.Contains("program-arrivals.disneycareers.com"))
                {
                    TalentBrewUI.txt_keywordSearch = TalentBrewUI.txt_keywordsearch3;
                }
                try
                {
                    // IWebElement parentObj = driver.FindElement(By.XPath("//div[contains(@class,'advanced-search-form-fields')]"));
                    // IList<IWebElement> listOfSelects = parentObj.FindElements(By.TagName("select"));

                    //  IList<IWebElement> listOfSelects = com_getListObjectByTagName(TalentBrewUI.advanceSearchForm, "select");

                    //Ramya Added
                    IList<IWebElement> listOfSelects = null;
                    if (driver.Url.Contains("armatis.com") || driver.Url.Contains("careers.mcafee.com"))
                        listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advancesearchform2, "select");
                    else
                        listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advanceSearchForm, "select");
                    

                        for (int i = 0; i <= listOfSelects.Count - 1; i++)
                        {
                            if (new SelectElement(listOfSelects[i]).Options.Count() > 1)
                                new SelectElement(listOfSelects[i]).SelectByIndex(1);
                            else
                                new SelectElement(listOfSelects[i]).SelectByIndex(0);

                            report.AddReportStep("Select the element from " + i + 1 + " dropdown", "Selected the element from " + i + 1 + " dropdown", StepResult.PASS);
                            Thread.Sleep(1000);

                            SearchResults(clientURL);
                            if (!driver.Url.Contains("utc.com"))
                            {
                                com_NewLaunchUrl(clientURL);
                            }
                            if (driver.Url.Contains("careers.jobsataramco.eu"))
                                com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                            else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                            else if (driver.Url.Contains("pattersoncompanies.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("www.wellsfargojobs.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            else if (driver.Url.Contains("utc.com"))
                            { //Ramya Added Latest
                                if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                                {
                                    if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                }
                            }
                            else if (driver.Url.Contains(".citi."))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                            else if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                            else if (driver.Url.Contains("jobs.atos.net"))
                                WaitForObject(TalentBrewUI.btn_Explore, 100);
                            else if (driver.Url.Contains("jobs.gartner.com"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsAdvance, "clicked on search jobs button", "unable to click search jobs button");
                            }
                            else if (driver.Url.Contains("careers.mcafee.com"))
                            {
                                com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                                Thread.Sleep(2000);

                            }
                        }
                            //Ramya Added
                            // IList<IWebElement> listOfSelects = null;
                            if (driver.Url.Contains("armatis.com") || driver.Url.Contains("careers.mcafee.com"))
                            {
                                listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advancesearchform2, "select");
                            }
                            else
                            {
                                listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advanceSearchForm, "select");
                            }
                    }
                
                catch (Exception e)
                { }

                try
                {
                    //Ramya Added
                    IList<IWebElement> listOfSelects = null;
                    if (driver.Url.Contains("armatis.com") || driver.Url.Contains("careers.mcafee.com"))
                        listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advancesearchform2, "select");
                    else
                        listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advanceSearchForm, "select");
                   
                    for (int i = 0; i <= listOfSelects.Count - 1; i++)
                    {
                        if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch) || com_IsElementPresent(TalentBrewUI.keywordBoxNew) || com_IsElementPresent(TalentBrewUI.txt_keywordSearch1) || com_IsElementPresent(TalentBrewUI.txt_keywordSearch2) || com_IsElementPresent(TalentBrewUI.txt_keywordsearch3) || com_IsElementPresent(TalentBrewUI.txt_keywordsearch4) || com_IsElementPresent(TalentBrewUI.txt_keywordsearch5) || com_IsElementPresent(TalentBrewUI.txt_keywordSearch6))
                        {
                            if (listOfSelects.Count == 0)
                            {
                                if (driver.Url.Contains("careers.jobsataramco.eu"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                                else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                                else if (driver.Url.Contains("pattersoncompanies.com"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                //Ramya Added
                                else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("www.wellsfargojobs.com"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                //Ramya Added
                                if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                //Ramya Added
                                else if (driver.Url.Contains("utc.com"))
                                { //Ramya Added Latest
                                    if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                                    {
                                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                    }
                                }
                                else if (driver.Url.Contains(".citi."))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                                else if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                                else if (driver.Url.Contains("jobs.atos.net"))
                                    WaitForObject(TalentBrewUI.btn_Explore, 100);
                                else if (driver.Url.Contains("careers.mcafee.com"))
                                {
                                    com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                                    Thread.Sleep(2000);

                                }
                            }

                            if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch) || com_IsElementPresent(TalentBrewUI.txt_keywordSearch1) || com_IsElementPresent(TalentBrewUI.txt_keywordSearch2) || com_IsElementPresent(TalentBrewUI.txt_keywordsearch3) || com_IsElementPresent(TalentBrewUI.txt_keywordsearch4) || com_IsElementPresent(TalentBrewUI.txt_keywordsearch5) || com_IsElementPresent(TalentBrewUI.txt_keywordSearch6) || com_IsElementPresent(TalentBrewUI.Keyword_txt_new) || com_IsElementPresent(TalentBrewUI.keywordBoxNew))
                            {
                                if(com_IsElementPresent(TalentBrewUI.txt_keywordSearch))
                                    SendKeyword(TalentBrewUI.txt_keywordSearch);
                                else if(com_IsElementPresent(TalentBrewUI.txt_keywordSearch1))
                                 SendKeyword(TalentBrewUI.txt_keywordSearch1);
                                else if(com_IsElementPresent(TalentBrewUI.txt_keywordSearch2))
                                  SendKeyword(TalentBrewUI.txt_keywordSearch2);
                                 else if(com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                                  SendKeyword(TalentBrewUI.txt_keywordsearch3);
                                 else if(com_IsElementPresent(TalentBrewUI.txt_keywordsearch4))
                                  SendKeyword(TalentBrewUI.txt_keywordsearch4);
                                else if (com_IsElementPresent(TalentBrewUI.txt_keywordsearch5))
                                    SendKeyword(TalentBrewUI.txt_keywordsearch5);
                                else if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch6))
                                    SendKeyword(TalentBrewUI.txt_keywordSearch6);
                                else if (com_IsElementPresent(TalentBrewUI.Keyword_txt_new))
                                    SendKeyword(TalentBrewUI.Keyword_txt_new);
                                else if (com_IsElementPresent(TalentBrewUI.keywordBoxNew))
                                    SendKeyword(TalentBrewUI.keywordBoxNew);




                            }

                            if (new SelectElement(listOfSelects[i]).Options.Count() > 1)
                            {
                                new SelectElement(listOfSelects[i]).SelectByIndex(1);
                            }
                            else
                            {
                                new SelectElement(listOfSelects[i]).SelectByIndex(0);
                            }
                            report.AddReportStep("Select the element from " + i + 1 + " dropdown", "Selected the element from " + i + 1 + " dropdown", StepResult.PASS);
                            Thread.Sleep(3000);
                            SearchResults(clientURL);
                            if (!driver.Url.Contains("utc.com"))
                            {
                                com_NewLaunchUrl(clientURL);
                            }
                            if (driver.Url.Contains("careers.jobsataramco.eu"))
                                com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                            else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                            else if (driver.Url.Contains("pattersoncompanies.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("www.wellsfargojobs.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' buton", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            else if (driver.Url.Contains("utc.com"))
                            { //Ramya Added Latest
                                if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                                {
                                    if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                }
                            }
                            else if (driver.Url.Contains(".citi."))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                            else if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");
                            else if (driver.Url.Contains("jobs.atos.net"))
                                WaitForObject(TalentBrewUI.btn_Explore, 100);

                            else if (driver.Url.Contains("jobs.gartner.com"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsAdvance, "clicked on search jobs button", "unable to click search jobs button");
                            }
                            else if (driver.Url.Contains("careers.mcafee.com"))
                            {
                                com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                                Thread.Sleep(2000);

                            }
                            //Ramya Added
                            //IList<IWebElement> listOfSelects = null;
                            if (driver.Url.Contains("armatis.com"))
                            {
                                listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advancesearchform2, "select");
                            }
                            else
                            {
                                listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advanceSearchForm, "select");
                            }

                        }
                        else
                            report.AddReportStep("Keyword textbox is not available", "Keyword textbox is not available", StepResult.WARNING);
                    }
                }
                catch (Exception e)
                {
                }

                try
                {
                    //Ramya Added
                    IList<IWebElement> listOfSelects = null;
                    if (driver.Url.Contains("armatis.com") || driver.Url.Contains("careers.mcafee.com"))
                    {
                        listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advancesearchform2, "select");
                    }
                    else
                    {
                        listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advanceSearchForm, "select");
                    }


                    for (int i = 0; i <= listOfSelects.Count - 1; i++)
                    {
                        if (listOfSelects.Count == 0)
                        {
                            if (driver.Url.Contains("careers.jobsataramco.eu"))
                                com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                            else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                            else if (driver.Url.Contains("pattersoncompanies.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("www.wellsfargojobs.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                            //Ramya Added
                            else if (driver.Url.Contains("utc.com"))
                            { //Ramya Added Latest
                                if (!com_IsElementPresent(TalentBrewUI.txt_keywordsearch3))
                                {
                                    if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                                }
                            }
                            else if (driver.Url.Contains(".citi."))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");
                            else if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                            else if (driver.Url.Contains("jobs.atos.net"))
                                WaitForObject(TalentBrewUI.btn_Explore, 100);
                            else if (driver.Url.Contains("jobs.gartner.com"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsAdvance, "clicked on search jobs button", "unable to click search jobs button");
                            }
                            else if (driver.Url.Contains("careers.mcafee.com"))
                            {
                                com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                                Thread.Sleep(2000);

                            }
                        }

                        if (new SelectElement(listOfSelects[i]).Options.Count() > 1)
                        {
                            new SelectElement(listOfSelects[i]).SelectByIndex(1);
                        }
                        else
                        {
                            new SelectElement(listOfSelects[i]).SelectByIndex(0);
                        }
                        report.AddReportStep("Select the element from " + i + 1 + " dropdown", "Selected the element from " + i + 1 + " dropdown", StepResult.PASS);
                        Thread.Sleep(3000);

                    }
                }
                catch (Exception e) { }

                if (com_IsElementPresent(TalentBrewUI.txt_keywordSearch))
                {
                    SendKeyword(TalentBrewUI.txt_keywordSearch);
                    SearchResults(clientURL);
                }
                else
                {
                    report.AddReportStep("Keyword textbox is not available", "Keyword textbox is not available", StepResult.WARNING);
                    SearchResults(clientURL);
                }

            }
            catch (Exception e)
            {
            }

        }

        private void SendKeyword(By obj)
        {
            string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
            com_SendKeys(obj, searchKeyword, "Basic Search || Entered the keyword in the 'Keyword textBox' - " + searchKeyword, "Basic Search || Problem in entering the keyword in the 'Keyword textbox'- " + searchKeyword);

        }

        private void SearchResults(string clientURL)
        {
            //Ramya Added
            if (driver.Url.Contains("utc.com") || driver.Url.Contains("jobs.delltechnologies.com") )
            {
                if (com_IsElementPresent(TalentBrewUI.btn_Search3))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
            }
            else
            {
                if (com_IsElementPresent(TalentBrewUI.btn_Search))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                else if (com_IsElementPresent(TalentBrewUI.btn_Search1))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search1, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                //Ramya Added
                else if (com_IsElementPresent(TalentBrewUI.btn_Search5))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search5, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                else if(com_IsElementPresent(By.XPath("(//button[contains(@id,'search-submit')])[2]")))
                    com_ClickOnInvisibleElement(By.XPath("(//button[contains(@id,'search-submit')])[2]"),"Search button","Advance Search || Problem in Clicking on Search Button");
                else if (com_IsElementPresent(By.XPath("(//a[contains(@class,'button button-submit')])[2]")))
                    com_ClickOnInvisibleElement(By.XPath("(//a[contains(@class,'button button-submit')])[2]"), "Search button", "Advance Search || Problem in Clicking on Search Button");
            }
           
            string url = driver.Url;
            //Ramya Added
            if (driver.Url.Contains("search-jobs") || driver.Url.Contains("szukanie-pracy") || driver.Url.Contains("jobs-zoeken") || driver.Url.Contains("jobsuche") || driver.Url.Contains("buscar-trabajo") || driver.Url.Contains("%e6%b1%82%e8%81%8c") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("busca-de-vagas"))
            {
                report.AddReportStep("Advance Search || Navigated to Search result page", "Advance Search || Navigated to Search result page", StepResult.PASS);
                if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search || Result panel is displayed for Advanced Search", "Advance Search || Result panel is not displayed for Advanced search"))
                {
                    string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                    if (!String.IsNullOrEmpty(jobcount))
                    {
                        report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                        //com_NewLaunchUrl(clientURL);
                    }
                    else
                    {
                        report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                    }

                }
            }
            else
            {
                report.AddReportStep("Advance Search || Does not Navigated to Search result page", "Advance Search || Does not Navigated to Search result page", StepResult.FAIL);
            }
        }

        private void app_AdvanceSearchSpl_key(string clientURL, string searchKeyword, By obj, By objectBy)
        {
            try
            {
                if (!driver.Url.Contains("careers.bbcworldwide.com") && !driver.Url.Contains("jobs.gartner.com"))
                    com_NewLaunchUrl(clientURL);

                //com_HandleAlert(true);

                com_ClearElement(obj);
                if (driver.Url.Contains("www.tmp.com") || driver.Url.Contains("jobs.memorialhermann.org") || driver.Url.Contains("newsearch.tmp.com"))
                {
                    obj = TalentBrewUI.txt_keywordsearch3;
                }
                //Ramya Added
                else if (driver.Url.Contains("armatis.com"))
                {
                    obj = TalentBrewUI.Keyword_txt_new;
                }

                com_SendKeys(obj, searchKeyword, "Advance Search || Entered the keyword in the Keyword textBox - " + searchKeyword, "Advance Search || Problem in entering the keyword in the keyword textbox- " + searchKeyword);
                //Ramya Added
                IList<IWebElement> listOfSelects = null;
                if (driver.Url.Contains("armatis.com"))
                {
                    listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advancesearchform2, "select");
                }
                else
                {
                    listOfSelects = com_getListsOfobjectsTagname(TalentBrewUI.advanceSearchForm, "select");
                }
              
                    for (int i = 0; i < listOfSelects.Count; i++)
                    {
                        if (new SelectElement(listOfSelects[i]).Options.Count() > 1)
                        {
                            new SelectElement(listOfSelects[i]).SelectByIndex(1);
                        }
                        else
                        {
                            new SelectElement(listOfSelects[i]).SelectByIndex(0);
                        }
                        report.AddReportStep("Select the element from " + i + 1 + " dropdown", "Selected the element from " + i + 1 + " dropdown", StepResult.PASS);
                        Thread.Sleep(3000);
                    }
              
                com_ClickOnInvisibleElement(objectBy, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                string url = driver.Url;
                Thread.Sleep(5000);
                if (driver.Url.Contains("search-jobs") || driver.Url.Contains("buscar-trabajo") || driver.Url.Contains("%e6%b1%82%e8%81%8c") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("busca-de-vagas"))
                {
                    report.AddReportStep("Advance Search|| Navigated to Search result page", "Advance Search || Navigated to Search result page", StepResult.PASS);
                    if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search || Result panel is displayed for Advanced Search", "Advance Search || Result panel is not displayed for Advanced search"))
                    {
                        string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                        if (!String.IsNullOrEmpty(jobcount))
                        {
                            report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                            com_NewLaunchUrl(clientURL);
                            //com_HandleAlert(true);
                        }
                        else
                        {
                            report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                        }
                    }
                }
                else
                {
                    report.AddReportStep("Advance Search || Does not Navigated to Search result page", "Advance Search || Does not Navigated to Search result page", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
            }
        }

        private int app_Location_radius_Search(string searchKeyword, string searchLocation, string tempURL, int radiusSize)
        {
            bool gotTheLocation = false;
            com_NewLaunchUrl(tempURL);
            By obj;
            if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
            {
                TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                TalentBrewUI.lst_Radius = By.XPath("(//*[@name='r'])[2]");
            }
            if (driver.Url.Contains("jobs.mountcarmelhealth.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.joinOurTalent_Popup))
                    com_ClickOnInvisibleElement(TalentBrewUI.closeButton_TalentPop, "Clicked on Join Our Talent Pop up close button", "Unable to click on the Join Our Talent Pop up close button");
            }
            //Ramya Added
            if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {
                    //Ramya Added
                    //Ramya Added Latest
                    if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                }

            }  //Ramya Added Latest
            if (driver.Url.Contains("fr.jobs-ups.ca"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    Thread.Sleep(1000);
                }
            }
            if (driver.Url.Contains("jobs.delltechnologies.com"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {

                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                }

            }
            if (driver.Url.Contains("jobs.hcr-manorcare.com"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                }
                Thread.Sleep(2000);
            } if (driver.Url.Contains("jobs.greatwolf.com"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {
                    com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                }
                Thread.Sleep(2000);
            }

            //Ramya Added
            else if (driver.Url.Contains("pgcareers.com"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {

                    com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                }

            }
            if (driver.Url.Contains("careers.jobsataramco.eu"))
                com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");


            if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
            {
                if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                }
            }
            if (driver.Url.Contains("jobs.greeneking.co.uk"))
                com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
            if (driver.Url.Contains("jobs.mdanderson.org"))
                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

            if (driver.Url.Contains("jobs.atos.net"))
                WaitForObject(TalentBrewUI.btn_Explore, 100);
           
            //Mani Added-29/10

            if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
            {
                TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                TalentBrewUI.lst_Radius = By.XPath("(//*[@name='r'])[2]");
                TalentBrewUI.txt_keywordSearch = By.XPath("(//input[@name='k'])[2]");
            }
            Thread.Sleep(3000);
            gotTheLocation = GetJobLocationRadiusDropDown(searchLocation);
            if ((com_getobjProperty(TalentBrewUI.txt_keywordSearch, "class")).Contains("keyword"))
            {
                obj = TalentBrewUI.txt_keywordSearch;
            }
            else
            {
                obj = TalentBrewUI.txt_keywordsearch3;
            }


            if (gotTheLocation == false)
            {

                com_ClickOnInvisibleElement(TalentBrewUI.txt_LocationSearch, "Clicked on Location Text Box", "Unable to click on Location Text box");


                com_ClickOnInvisibleElement(obj, "Clicked on Keyword text box", "Unable to click on keyword text box");

                Thread.Sleep(5000);

                //Ramya Added
                if ((com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") == null) || (driver.FindElement(TalentBrewUI.lst_Radius).GetAttribute("disabled")) == null)
                {
                    gotTheLocation = true;
                }
            }

            if (gotTheLocation)
            {

                if (!com_IsElementPresent(TalentBrewUI.lst_Radius))
                {
                    Search_withGotLocation(searchLocation, obj);
                }

                else
                {
                    //Ramya Added
                    if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                    {
                        TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                        TalentBrewUI.lst_Radius = By.XPath("(//*[@name='r'])[2]");
                        TalentBrewUI.txt_keywordSearch = By.XPath("(//input[@name='k'])[2]");
                    }
                    if (driver.Url.Contains("jobs.ebayinc.com") || com_VerifyObjPresent(TalentBrewUI.lst_Radius, "Advance Search || Radius is present", "Advance Search || Radius is not present"))
                    {
                        radiusSize = com_GetListBoxSize(TalentBrewUI.lst_Radius);
                        Thread.Sleep(5000);


                        if ((com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") == null) || (com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") == String.Empty))
                        {
                            //Ramya Added
                            if (!driver.Url.Contains("jobs.ebayinc.com"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.lst_Radius, "Clicked on Radius Listbox", "Problem in clicking in the radius list box");
                            }
                            if (com_SelectByIndex(TalentBrewUI.lst_Radius, (radiusSize - 1), "Advance Search || Selected the radius " + (radiusSize - 1), "Advance Search || Problem in selecting the Radius"))
                            {
                                Thread.Sleep(2000);
                                report.AddReportStep("Advance Search ||Selected Radius - " + com_GetSelectedOption(TalentBrewUI.lst_Radius), "Advance Search ||Selected Radius - " + com_GetSelectedOption(TalentBrewUI.lst_Radius), StepResult.PASS);
                                //Ramya Added
                                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com") || driver.Url.Contains("jobs.santanderbank.com") || driver.Url.Contains("jobs.delltechnologies.com"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                else if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_search6, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                } //Ramya Added New
                                else if (driver.Url.Contains("scotiabank.com"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search_New, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                //Mani added
                                else if (driver.Url.Contains("jobs.cdc.gov"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                else if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search7, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                else
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                Thread.Sleep(2000);
                                //Ramya Added
                                if (driver.Url.Contains("search-jobs") || driver.Url.Contains("vmware.com") || driver.Url.Contains("parexel.co") || driver.Url.Contains("lavori-di-ricerca") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("busca-de-vagas") || driver.Url.Contains("buscar-trabajo") || driver.Url.Contains("%e6%b1%82%e8%81%8c") || driver.Url.Contains("jobsuche") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("jobs-zoeken") || driver.Url.Contains("soeg-jobs") || driver.Url.Contains("%d8%a7%d9%84%d8%a8%d8%ad%d8%ab-%d8%b9%d9%86-%d9%88%d8%b8%d8%a7%d8%a6%d9%81") || driver.Url.Contains("mekl%c4%93t-darbu"))
                                {
                                    report.AddReportStep("Advance Search - Radius & Location search only || Navigated to Search result page", "Advance Search - Radius & Location search only || Navigated to Search result page", StepResult.PASS);
                                    if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search - Radius & Location search only || Result panel is displayed for Keyword Search", "Advance Search - Radius & Location search only || Result panel is not displayed for Keyword search"))
                                    {
                                        string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                                        if (!String.IsNullOrEmpty(jobcount))
                                        {
                                            report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                                            if (!jobcount.Equals("0"))
                                            {
                                                app_VerifyFilter_Radius();
                                            }
                                        }
                                        else
                                        {
                                            report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                                        }
                                    }
                                }
                                else
                                {
                                    report.AddReportStep("Advance Search - Radius & Location search only || Does not Navigated to Search result page", "Advance Search - Radius & Location search only || Does not Navigated to Search result page", StepResult.FAIL);
                                }
                            }
                        }
                        else
                        {
                            report.AddReportStep("Advance Search - Radius || Radius dropdown list box is not enabled on entering the city in location text box ", "Advance Search - Radius || Radius dropdown list box is not enabled on entering the city in location text box ", StepResult.FAIL);

                        }
                    }
                }
            }
            else
            {
                report.AddReportStep("Advance Search || Radius listbox is not enabled while entering the location", "Advance Search || Radius listbox is not enabled while entering the location", StepResult.FAIL);
            }
            return radiusSize;
        }

        private void Search_withGotLocation(string searchLocation, By obj)
        {
            com_ClearElement(obj);
            if (driver.Url.Contains("emplois.enterprise.ca")||driver.Url.Contains("careers.enterprise.ca"))
            {
                TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                TalentBrewUI.txt_keywordSearch = By.XPath("(//input[@name='k'])[2]");

            }
            com_ClearElement(TalentBrewUI.txt_LocationSearch);
            com_SendKeys(TalentBrewUI.txt_LocationSearch, searchLocation, "Advance Search || Entered the Location in the Location textBox - " + searchLocation, "Advance Search || Problem in entering the location in the location textbox- " + searchLocation);
            Thread.Sleep(3000);
            if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink1))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink1, "Advance Search || Clicked on location from the sugesstion", "Advance Search || Problem in Clicking the location from the sugesstion");
            }

            else if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink, "Advance Search || Clicked on location from the sugesstion", "Advance Search || Problem in Clicking the location from the sugesstion");
            }
            else
            {
                report.AddReportStep("Location suggestion is not displayed", "Location suggestion is not displayed", StepResult.FAIL);
            }
            Thread.Sleep(2000);
            com_ClearElement(obj);
            report.AddReportStep("Cleared the keyword text field", "Cleared the keyword text field", StepResult.PASS);
            Thread.Sleep(2000);
        }

        private bool GetJobLocationRadiusDropDown(string searchLocation)
        {
            By obj;
            if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
            {
                TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                TalentBrewUI.lst_Radius = By.XPath("(//input[@name='r'])[2]");
                TalentBrewUI.txt_keywordSearch = By.XPath("(//input[@name='k'])[2]");

            }
            if ((com_getobjProperty(TalentBrewUI.txt_keywordSearch, "class")).Contains("keyword"))
            {
                obj = TalentBrewUI.txt_keywordSearch;
            }
            else
            {
                obj = TalentBrewUI.txt_keywordsearch3;
            }
            
            bool tempGotTheLocation = Radius_ClickLocation(searchLocation, obj);

            if (com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") != null)
            {
                deleteAllCookies();
                if (driver.Url.Contains("jobs.mountcarmelhealth.com"))
                {
                    if (com_IsElementPresent(TalentBrewUI.joinOurTalent_Popup))
                        com_ClickOnInvisibleElement(TalentBrewUI.closeButton_TalentPop, "Clicked on Join Our Talent Pop up close button", "Unable to click on the Join Our Talent Pop up close button");
                }
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                } if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }//Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                //button[contains(@id,'search-submit') or contains(@class,'job-search-submit') or contains(@id,'edit-submit--3') or contains(text(), 'Search Jobs')]
                //Ramya Added
                if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                else if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);

                Thread.Sleep(3000);
                tempGotTheLocation = Radius_ClickLocation(searchLocation, obj);
                if (!tempGotTheLocation.Equals("true"))
                {
                    com_ClearElement(obj);
                    Thread.Sleep(1000);

                   
                    com_ClickOnInvisibleElement(TalentBrewUI.txt_LocationSearch, "Clicked on Location textbox", "Problem in clicking on Location textbox");
                    com_ClearElement(obj);
                    Thread.Sleep(1000);
                    if(com_IsElementPresent(TalentBrewUI.lst_Radius))
                    {
                        bool flag = tillEnabled(TalentBrewUI.lst_Radius, 40);
                    //Ramya Added
                    if (com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") == null || (driver.FindElement(TalentBrewUI.lst_Radius).GetAttribute("disabled")) == null)
                    {
                        tempGotTheLocation = true;
                    }
                    }
                    return tempGotTheLocation;
                }

            }

            return tempGotTheLocation;
        }

        private bool Radius_ClickLocation(string searchLocation, By obj)
        {
            if (driver.Url.Contains("www.tmp.com") || driver.Url.Contains("internaljobs.coxenterprises.com") || driver.Url.Contains("newsearch.tmp.com"))
            {
                obj = TalentBrewUI.txt_keywordsearch3;
            }
            //Mani Added-29/10

            if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
            {
                TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                TalentBrewUI.lst_Radius = By.XPath("(//input[@name='r'])[2]");
                TalentBrewUI.txt_keywordSearch = By.XPath("(//input[@name='k'])[2]");

            }
            bool tempGotTheLocation = false;
            Thread.Sleep(2000);
            com_ClearElement(obj);
            com_ClearElement(TalentBrewUI.txt_LocationSearch);
            Thread.Sleep(3000);
            com_SendKeys(TalentBrewUI.txt_LocationSearch, searchLocation, "Advance Search || Entered the Location in the Location textBox - " + searchLocation, "Advance Search || Problem in entering the location in the location textbox- " + searchLocation);
            Thread.Sleep(4000);

            if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink1))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink1, "Advance Search || Clicked on location from the suggestion", "Advance Search || Problem in Clicking the location from the suggestion");
            }

            else if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink, "Advance Search || Clicked on location from the suggestion", "Advance Search || Problem in Clicking the location from the suggestion");
            }

            Thread.Sleep(4000);
            com_ClickOnInvisibleElement(TalentBrewUI.txt_LocationSearch, "clicked in location search", "unable to click on the location search");


            com_ClearElement(obj);
            Thread.Sleep(2000);

            //Ramya Added
            if(com_IsElementPresent(TalentBrewUI.lst_Radius))
            {
                    if (com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") == null || (driver.FindElement(TalentBrewUI.lst_Radius).GetAttribute("disabled")) == null)
                    {
                        report.AddReportStep("Radius button is disabled", "Radius button is disabled", StepResult.PASS);
                    }
            }
            return tempGotTheLocation = true;
        }

        private void app_Key_Loc_Radius_Search(string searchKeyword, string searchLocation, string tempURL, int radiusSize, By obj)
        {
            bool gotTheLocation = false;
            //deleteAllCookies();
            try
            {
                if (driver.Url.Contains("jobs.mountcarmelhealth.com"))
                {
                    if (com_IsElementPresent(TalentBrewUI.joinOurTalent_Popup))
                        com_ClickOnInvisibleElement(TalentBrewUI.closeButton_TalentPop, "Clicked on Join Our Talent Pop up close button", "Unable to click on the Join Our Talent Pop up close button");
                }
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }//Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                }
                if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                //Ramya Added
                if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                //   if (driver.Url.Contains("pattersoncompanies.com"))
                //   com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs1, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                //Ramya Added
                if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("jobs.nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);

                gotTheLocation = GetJobLocationRadiusDropDown(searchLocation);
                if (driver.Url.Contains("www.tmp.com") || driver.Url.Contains("internaljobs.coxenterprises.com") || driver.Url.Contains("jobs.memorialhermann.org") || driver.Url.Contains("newsearch.tmp.com"))
                {
                    obj = TalentBrewUI.txt_keywordsearch3;
                }
                if (driver.Url.Contains("emplois.enterprise.ca")||driver.Url.Contains("careers.enterprise.ca")||driver.Url.Contains("careers.enterprise.com"))
                {
                    obj = By.XPath("(//input[@name='k'])[2]");
                }

                com_SendKeys(obj, searchKeyword, "Advance Search || Entered the keyword in the Keyword textBox - " + searchKeyword, "Advance Search || Problem in entering the keyword in the keyword textbox- " + searchKeyword);

                if (gotTheLocation)
                {
                    //Ramya Added
                    if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                    {
                        TalentBrewUI.lst_Radius = By.XPath("(//*[@name='r'])[2]");
                    }
                    if (driver.Url.Contains("jobs.ebayinc.com") || com_VerifyObjPresent(TalentBrewUI.lst_Radius, "Advance Search || Radius is present", "Advance Search || Radius is not present"))
                    {
                        //com_ClickOnInvisibleElement(TalentBrewUI.lst_Radius, "Clicked on Radius Listbox", "Problem in clicking in the radius list box");
                        //Ramya Added
                        if (com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") == null || (driver.FindElement(TalentBrewUI.lst_Radius).GetAttribute("disabled")) == null)
                        {
                            //Ramya Added
                            if (!driver.Url.Contains("jobs.ebayinc.com"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.lst_Radius, "Clicked on Radius Listbox", "Problem in clicking in the radius list box");
                            }
                            //Ramya Added
                            if (driver.Url.Contains("jobs.ebayinc.com") || com_SelectByIndex(TalentBrewUI.lst_Radius, (radiusSize - 1), "Advance Search || Selected the radius Option item - " + (radiusSize - 1), "Advance Search || Problem in selecting the Radius"))
                            {
                                Thread.Sleep(3000);
                                report.AddReportStep("Advance Search ||Selected Radius - " + com_GetSelectedOption(TalentBrewUI.lst_Radius), "Advance Search ||Selected Radius - " + com_GetSelectedOption(TalentBrewUI.lst_Radius), StepResult.PASS);
                                //Ramya Added
                                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com") || driver.Url.Contains("jobs.santanderbank.com") || driver.Url.Contains("jobs.delltechnologies.com") || driver.Url.Contains("jobs.cdc.gov"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }//Ramya Added New
                                else if (driver.Url.Contains("scotiabank.com"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search_New, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                else if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_search6, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                else if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search7, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                else
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                                }
                                Thread.Sleep(5000);
                                //Ramya Added
                                if (driver.Url.Contains("search-jobs") || driver.Url.Contains("vmware.com") || driver.Url.Contains("parexel.co") || driver.Url.Contains("lavori-di-ricerca") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("busca-de-vagas") || driver.Url.Contains("buscar-trabajo") || driver.Url.Contains("%e6%b1%82%e8%81%8c") || driver.Url.Contains("jobsuche") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("jobs-zoeken") || driver.Url.Contains("soeg-jobs") || driver.Url.Contains("%d8%a7%d9%84%d8%a8%d8%ad%d8%ab-%d8%b9%d9%86-%d9%88%d8%b8%d8%a7%d8%a6%d9%81") || driver.Url.Contains("mekl%c4%93t-darbu"))
                                {
                                    report.AddReportStep("Advance Search - Keyword & Radius & Location search only || Navigated to Search result page", "Advance Search - Keyword & Radius & Location search only || Navigated to Search result page", StepResult.PASS);
                                    if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search - Keyword & Radius & Location search only || Result panel is displayed for Keyword Search", "Advance Search - Keyword & Radius & Location search only || Result panel is not displayed for Keyword search"))
                                    {
                                        string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                                        if (!String.IsNullOrEmpty(jobcount))
                                        {
                                            report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                                        }
                                        else
                                        {
                                            report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                                        }
                                    }
                                }
                                else
                                {
                                    report.AddReportStep("Advance Search - Keyword & Radius & Location search only || Does not Navigated to Search result page", "Advance Search - Keyword & Radius & Location search only || Does not Navigated to Search result page", StepResult.FAIL);
                                }
                            }
                        }
                        else
                        {
                            report.AddReportStep("Advance Search - Radius || Radius dropdown list box is not enabled on entering the city in location text box ", "Advance Search - Radius || Radius dropdown list box is not enabled on entering the city in location text box ", StepResult.FAIL);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
        }

        private void app_VerifyFilter_Radius()
        {
            try
            {
                if (driver.Url.Contains("jobs.capitalone.com"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.Filter_button2, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
                }
                if (com_IsElementPresent(TalentBrewUI.lst_Filterradius))
                {
                    if (com_VerifyObjPresent(TalentBrewUI.lst_Filterradius, "Radius listbox is present in Filter Module", "Radius listbox is not present in Filter Module"))
                    {
                        com_SelectByIndex(TalentBrewUI.lst_Filterradius, 1, "Advance Search || Selected the radius", "Advance Search || Problem in selecting the Radius");
                        Thread.Sleep(4000);
                        report.AddReportStep("Advance Search ||Selected Radius - " + com_GetSelectedOption(TalentBrewUI.lst_Filterradius), "Advance Search ||Selected Radius - " + com_GetSelectedOption(TalentBrewUI.lst_Filterradius), StepResult.PASS);
                        string radiusSelected = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-distance");
                        string chkRadiusSelected = com_GetSelectedOption(TalentBrewUI.lst_Filterradius);
                        if (chkRadiusSelected == null || chkRadiusSelected == "")
                        {
                            chkRadiusSelected = com_GetSelectedOption(TalentBrewUI.lst_Filterradius1);

                        }
                        if (chkRadiusSelected.Contains(radiusSelected))
                        {
                            report.AddReportStep("Advance Search || Results are updated according to the radius selected from the filter module", "Advance Search || Results are updated according to the radius selected from the filter module", StepResult.PASS);
                        }
                        else
                        {
                            report.AddReportStep("Advance Search || Results are not updated according to the radius selected from the filter module", "Advance Search || Results are not updated according to the radius selected from the filter module", StepResult.FAIL);
                        }
                    }
                }
                else
                    report.AddReportStep("Advance Search || Radius Listbox is not available in Filter module for this site", "Advance Search || Radius Listbox is not available in Filter module for this site", StepResult.WARNING);

            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
            }



        private void app_Key_Loc_Search(string searchKeyword, string searchLocation, string tempURL, By obj)
        {
            try
            {
                com_NewLaunchUrl("https://www.google.co.in");
                com_NewLaunchUrl(tempURL);

                if (driver.Url.Contains("emplois.enterprise.ca")||driver.Url.Contains("careers.enterprise.ca"))
                {
                    obj = By.XPath("(//input[@name='k'])[2]");
                    TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");                   

                }
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }//Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("jobs.nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                else if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                else if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);

                Thread.Sleep(3000);
                com_ClearElement(obj);
                com_ClearElement(TalentBrewUI.txt_LocationSearch);
                com_SendKeys(TalentBrewUI.txt_LocationSearch, searchLocation, "Advance Search || Entered the Location in the Location textBox - " + searchLocation, "Advance Search || Problem in entering the location in the location textbox- " + searchLocation);

                Thread.Sleep(3000);

                if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink1))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink1, "Advance Search || Clicked on location from the sugesstion", "Advance Search || Problem in Clicking the location from the sugesstion");
                }

                else if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink, "Advance Search || Clicked on location from the sugesstion", "Advance Search || Problem in Clicking the location from the sugesstion");
                }
                if (driver.Url.Contains("www.tmp.com") || driver.Url.Contains("internaljobs.coxenterprises.com") || driver.Url.Contains("jobs.memorialhermann.org") || driver.Url.Contains("newsearch.tmp.com"))
                {
                    obj = TalentBrewUI.txt_keywordsearch3;
                }

                com_SendKeys(obj, searchKeyword, "Advance Search || Entered the keyword in the Keyword textBox - " + searchKeyword, "Advance Search || Problem in entering the keyword in the keyword textbox- " + searchKeyword);
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com") || driver.Url.Contains("jobs.santanderbank.com") || driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                }
                else if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_search6, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                }//Ramya Added New
                else if (driver.Url.Contains("scotiabank.com"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search_New, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                }
                //Mani added
                else if (driver.Url.Contains("jobs.cdc.gov"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                }
                else if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search7, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                else
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                }
                Thread.Sleep(5000);
                //Ramya Added
                if (driver.Url.Contains("search-jobs") || driver.Url.Contains("vmware.com") || driver.Url.Contains("parexel.co") || driver.Url.Contains("lavori-di-ricerca") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("busca-de-vagas") || driver.Url.Contains("buscar-trabajo") || driver.Url.Contains("%e6%b1%82%e8%81%8c") || driver.Url.Contains("jobsuche") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("jobs-zoeken") || driver.Url.Contains("soeg-jobs") || driver.Url.Contains("%d8%a7%d9%84%d8%a8%d8%ad%d8%ab-%d8%b9%d9%86-%d9%88%d8%b8%d8%a7%d8%a6%d9%81") || driver.Url.Contains("mekl%c4%93t-darbu"))
                {
                    report.AddReportStep("Advance Search - Keyword & Location search only || Navigated to Search result page", "Advance Search - Keyword & Location search only || Navigated to Search result page", StepResult.PASS);
                    if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search - Keyword & Location search only || Result panel is displayed for Keyword Search", "Advance Search - Keyword & Location search only || Result panel is not displayed for Keyword search"))
                    {
                        string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                        if (!String.IsNullOrEmpty(jobcount))
                        {
                            report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                        }
                        else
                        {
                            report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                        }
                    }
                }
                else
                {
                    report.AddReportStep("Advance Search - Keyword & Location search only || Does not Navigated to Search result page", "Advance Search - Keyword & Location search only || Does not Navigated to Search result page", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
        }

        private void app_Location_Search(string searchLocation, string tempURL, By obj)
        {
            try
            {
                com_NewLaunchUrl(tempURL);
                Thread.Sleep(3000);
                if (driver.Url.Contains("emplois.enterprise.ca")||driver.Url.Contains("careers.enterprise.ca"))
                {
                    obj = By.XPath("(//input[@name='k'])[2]");
                    TalentBrewUI.txt_LocationSearch = By.XPath("(//input[@name='l'])[2]");
                }
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }//Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("jobs.nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                else if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);

                Thread.Sleep(4000);
                if (com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                {
                    com_ClearElement(obj);
                    com_ClearElement(TalentBrewUI.txt_LocationSearch);
                    com_SendKeys(TalentBrewUI.txt_LocationSearch, searchLocation, "Advance Search || Entered the Location in the Location textBox - " + searchLocation, "Advance Search || Problem in entering the location in the location textbox- " + searchLocation);
                    Thread.Sleep(3000);
                    if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink1))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink1, "Advance Search || Clicked on location from the suggestion", "Advance Search || Problem in Clicking the location from the sugesstion");
                    }

                    else if (com_IsElementPresent(TalentBrewUI.lnk_LocationLink))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.lnk_LocationLink, "Advance Search || Clicked on location from the sugesstion", "Advance Search || Problem in Clicking the location from the sugesstion");
                    }

                    Thread.Sleep(1000);
                    //Ramya Added
                    if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com") || driver.Url.Contains("jobs.santanderbank.com") || driver.Url.Contains("jobs.delltechnologies.com"))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                    }
                    else if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_search6, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                    }//Ramya Added New
                    else if (driver.Url.Contains("scotiabank.com"))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_Search_New, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                    }
                    else if (driver.Url.Contains("jobs.cdc.gov"))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_Search3, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                    }
                    else if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_Search7, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                    else
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
                    }

                    Thread.Sleep(5000);
                    //Ramya Added
                    if (driver.Url.Contains("search-jobs") || driver.Url.Contains("vmware.com") || driver.Url.Contains("parexel.co") || driver.Url.Contains("lavori-di-ricerca") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("busca-de-vagas") || driver.Url.Contains("buscar-trabajo") || driver.Url.Contains("%e6%b1%82%e8%81%8c") || driver.Url.Contains("jobsuche") || driver.Url.Contains("recherche-d'offres") || driver.Url.Contains("jobs-zoeken") || driver.Url.Contains("soeg-jobs") || driver.Url.Contains("%d8%a7%d9%84%d8%a8%d8%ad%d8%ab-%d8%b9%d9%86-%d9%88%d8%b8%d8%a7%d8%a6%d9%81") || driver.Url.Contains("mekl%c4%93t-darbu"))
                    {
                        report.AddReportStep("Advance Search - Location search only || Navigated to Search result page", "Advance Search - Location search only || Navigated to Search result page", StepResult.PASS);
                        if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search - Location search only || Result panel is displayed for Location Search", "Advance Search - Location search only || Result panel is not displayed for Location search"))
                        {
                            string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                            if (!String.IsNullOrEmpty(jobcount))
                            {
                                report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                            }
                            else
                            {
                                report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                            }
                            com_ContainsTxt(TalentBrewUI.l2_jobTitle, searchLocation, "Advance Search - Location search only || Result Title in Search Result page is matching with Search Location", "Advance Search - Location search only || Result Title in Search Result page is not matching with Search Location");
                        }
                    }
                    else
                    {
                        report.AddReportStep("Advance Search - Location search only || Does not Navigated to Search result page", "Advance Search - Location search only || Does not Navigated to Search result page", StepResult.FAIL);
                    }
                }
                else
                {
                    report.AddReportStep("Advance Search - Location Search only || Location Search textbox is not present", "Advance Search - Location Search only || Location Search textbox is not present", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
        }

        private void app_Keyword_Search(string searchKeyword, By obj)
        {
            if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
            }

            com_ClearElement(obj);
            com_ClearElement(TalentBrewUI.txt_LocationSearch);
            driver.FindElement(TalentBrewUI.txt_LocationSearch).SendKeys(Keys.Tab);
            Thread.Sleep(1000);
            com_CompareTwoTxt("true", com_getobjProperty(TalentBrewUI.lst_Radius, "disabled"), "Advance Search || Radius List box is disabled", "Advance Search || Radius List box is not disabled");
            if (driver.Url.Contains("www.tmp.com") || driver.Url.Contains("newsearch.tmp.com"))
            {
                obj = TalentBrewUI.txt_keywordsearch3;
            }

            com_SendKeys(obj, searchKeyword, "Advance Search || Entered the keyword in the Keyword textBox - " + searchKeyword, "Advance Search || Problem in entering the keyword in the keyword textbox- " + searchKeyword);
            if (driver.Url.Contains("usa.jobs.scotiabank.com"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.btn_search6, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
            }
            else
                com_ClickOnInvisibleElement(TalentBrewUI.btn_Search, "Advance Search || Clicked on Search Button", "Advance Search || Problem in Clicking on Search Button");
            if (driver.Url.Contains("search-jobs"))
            {
                report.AddReportStep("Advance Search - Keyword search only || Navigated to Search result page", "Advance Search - Keyword search only || Navigated to Search result page", StepResult.PASS);
                if (com_VerifyObjPresent(TalentBrewUI.section_Searchresults, "Advance Search - Keyword search only || Result panel is displayed Keyword Search", "Advance Search - Keyword search only || Result panel is not displayed Keyword search"))
                {
                    string jobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                    if (!String.IsNullOrEmpty(jobcount))
                    {
                        report.AddReportStep("Job count", "Job count is - " + jobcount, StepResult.PASS);
                    }
                    else
                    {
                        report.AddReportStep("Job count", "Job count is not present", StepResult.FAIL);
                    }
                    com_ContainsTxt(TalentBrewUI.l2_jobTitle, searchKeyword, "Advance Search - Keyword search only || Result Title in Search Result page is matching with Search Keyword", "Advance Search - Keyword search only || Result Title in Search Result page is not matching with Search Keyword");
                }
            }
            else
            {
                report.AddReportStep("Advance Search - Keyword search only || Does not Navigated to Search result page", "Advance Search - Keyword search only || Does not Navigated to Search result page", StepResult.FAIL);
            }

        }

        public void app_FilterSearch(string filtertype, By obj_filtertype, By obj_FilterItem)
        {
            bool filtersectionpresent = false;

            string classAttr = string.Empty;
            string itemName = string.Empty;
            if (driver.Url.Contains("www.nestleusacareers.com"))
            {
                //string a = com_GetText(TalentBrewUI.Filter_toggle);
                if (!com_IsElementPresent(By.Id("keyword-tag")))
                    com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                Thread.Sleep(3000);
                if (!com_IsElementPresent(By.Id("keyword-tag")))
                    com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                Thread.Sleep(3000);
            }

            if (com_IsElementPresent(TalentBrewUI.section_Filter) || com_IsElementPresent(TalentBrewUI.Section_filter1))
            {
                filtersectionpresent = true;
                //if (com_VerifyObjPresent(obj_filtertype, filtertype + " || " + filtertype + " is present in filter section", filtertype + " || " + filtertype + " is not present in filter section"))
                if (driver.Url.Contains("www.nestleusacareers.com"))
                {
                    //string a = com_GetText(TalentBrewUI.Filter_toggle);
                    if (!com_IsElementPresent(By.Id("keyword-tag")))
                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                    Thread.Sleep(3000);
                    if (!com_IsElementPresent(By.Id("keyword-tag")))
                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                    Thread.Sleep(3000);
                }


                if (com_IsElementPresent(obj_filtertype))
                {
                    report.AddReportStep(filtertype + " || " + filtertype + " is present in filter section", filtertype + " || " + filtertype + " is present in filter section", StepResult.PASS);
                    classAttr = com_getobjProperty(obj_filtertype, "class");
                    if (classAttr.Contains("child-open"))
                    {
                        report.AddReportStep("Filter module " + filtertype + " || " + filtertype + " is expanded by default", "Filter module " + filtertype + " || " + filtertype + " is expanded by default", StepResult.WARNING);
                        if (com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button"))
                            app_loading(TalentBrewUI.expand_Filtertype);
                        //Thread.Sleep(4000);
                        classAttr = com_getobjProperty(obj_filtertype, "class");
                        if (classAttr.Contains("expandable-parent") || classAttr.Contains("expandable-parent expandable-child-open"))
                        {
                            report.AddReportStep("Filter module " + filtertype + " toggle is collapsed on clicking the collapse button", "Filter module " + filtertype + " toggle is collapsed on clicking the collapse button", StepResult.PASS);
                            if (com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + "expand button", "Filter Module || Problem in clicking the " + filtertype + " expand button"))
                            {
                                WaitForObject(TalentBrewUI.expand_Filtertype, 100);
                                //Thread.Sleep(4000);
                                classAttr = com_getobjProperty(obj_filtertype, "class");
                                if (classAttr.Contains("child-open"))
                                {
                                    report.AddReportStep("Filter module || " + filtertype + "toggle is expanded on clicking the expand button", "Filter module || " + filtertype + "toggle is expanded on clicking the expand button", StepResult.PASS);

                                    //IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                                    //IWebElement firstItem = driver.FindElement(obj_FilterItem);
                                    //String filterLocation = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", firstItem);
                                    //js.ExecuteScript("arguments[0].click();", firstItem);
                                    //String filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", firstItem);
                                    ////string filterjobcount = com_getobjProperty(obj_FilterItem, "data-count");

                                    itemName = com_getobjProperty(obj_FilterItem, "data-display");
                                    string filterjobcount = com_getobjProperty(obj_FilterItem, "data-count");
                                    com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Clicked on the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking the first " + filtertype + " CheckBox");
                                    WaitForObject(TalentBrewUI.section_appliedFilter, 100);
                                    string appliedFilter = com_GetText(TalentBrewUI.section_appliedFilter);
                                    if (com_ContainsTxt(TalentBrewUI.section_appliedFilter, itemName, "Filter By section matches the " + filtertype + " selected " + itemName, "Filter By section does not matches the " + filtertype + " selected "))
                                    {
                                        //string filterjobcount = com_getobjProperty(obj_FilterItem, "data-count");
                                        string alljobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");

                                        if (filterjobcount.Equals(alljobcount))
                                        {
                                            report.AddReportStep("Filter module || " + filtertype + " Job results are displayed as per the filter selected", "Filter module || " + filtertype + " Job results are displayed as per the filter selected", StepResult.PASS);
                                        }
                                        else
                                        {
                                            report.AddReportStep("Filter module || " + filtertype + " Job results are not displayed as per the filter selected", "Filter module || " + filtertype + " Job results are not displayed as per the filter selected", StepResult.WARNING);
                                        }

                                        if (driver.Url.Contains("jobs.cornerstonebrands") || driver.Url.Contains("physicianjobs.interiorhealth") || driver.Url.Contains("careers.rochesterregional") || driver.Url.Contains("cox") || driver.Url.Contains("advocatehealth") || driver.Url.Contains("internaljobs.coxenterprises") || driver.Url.Contains("cliffsnaturalresources.com") || driver.Url.Contains("raymondjames.com") || driver.Url.Contains("columbusregional.com") || driver.Url.Contains("baxter.com") || driver.Url.Contains("jobs.lvhn.org") || driver.Url.Contains("jobs.sungardas.com") || driver.Url.Contains("jobs.ehhi.com") || driver.Url.Contains("jobs.owenscorningcareers.com") || driver.Url.Contains("www.unmhjobs.com") || driver.Url.Contains("jobs.clevelandcliffs.com"))
                                        {
                                            com_ClickOnInvisibleElement(By.LinkText(itemName.ToUpper()), "Filter Module || Clicked on the delete icon for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking the delete icon of the frist " + filtertype + " CheckBox");
                                        }
                                        else
                                        {
                                            com_ClickOnInvisibleElement(By.LinkText(itemName), "Filter Module || Clicked on the delete icon for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking the delete icon of the frist " + filtertype + " CheckBox");

                                        }

                                        if (!driver.Url.Contains("ikea"))
                                            app_loading(TalentBrewUI.section_appliedFilter);
                                        //if (driver.Url.Contains("springhire.lowes.com")||driver.Url.Contains("raymondjames.com"))
                                        //{
                                        //   Thread.Sleep(5000);
                                        //}
                                        //appliedFilter = com_GetText(TalentBrewUI.section_appliedFilter);
                                        if (com_IsElementPresent(TalentBrewUI.section_appliedFilter))
                                        {
                                            com_DoesNOTContainsTxt(TalentBrewUI.section_appliedFilter, itemName, "Filter By section removes the " + filtertype + "-" + itemName + "on clicking the delete icon", "Filter By section does not removes the " + filtertype + "-" + itemName + "on clicking the delete icon");
                                        }
                                        else
                                        {
                                            report.AddReportStep("Filter By section removes the " + filtertype + " on clicking the delete icon -  " + itemName, "Filter By section removes the " + filtertype + " on clicking the delete icon -  " + itemName, StepResult.PASS);
                                        }

                                    }
                                    else
                                    {
                                        com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Unchecked the " + filtertype + " " + itemName, "Filter Module || Problem in  Unchecking the first " + filtertype + " CheckBox");
                                        System.Threading.Thread.Sleep(7000);
                                    }
                                    if (driver.Url.Contains("www.nestleusacareers.com"))
                                    {
                                        //string a = com_GetText(TalentBrewUI.Filter_toggle);
                                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                        Thread.Sleep(3000);
                                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                        Thread.Sleep(3000);
                                    }

                                    com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button");
                                    app_loading(TalentBrewUI.expand_Filtertype);
                                }
                            }

                        }
                        else
                        {
                            report.AddReportStep(filtertype + "toggle is not collapsed on clicking the collapse button", filtertype + "toggle is not collapsed on clicking the collapse button", StepResult.FAIL);
                        }

                    }
                    else
                    {
                        report.AddReportStep(filtertype + " toggle is not expanded by default", filtertype + " toggle is not expanded by default", StepResult.WARNING);
                        if (com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " expand button", "Filter Module || Problem in clicking the " + filtertype + " expand button"))
                        {
                            if (!(driver.Url.Contains("primark.com") || driver.Url.Contains("ikea") || driver.Url.Contains("coop")))
                                WaitForObject(TalentBrewUI.expand_Filtertype, 100);
                            if (driver.Url.Contains("www.nestleusacareers.com"))
                                Thread.Sleep(3000);

                            classAttr = com_getobjProperty(obj_filtertype, "class");
                            if (classAttr.Contains("child-open") || classAttr.Contains("active") || classAttr.Contains("toggle-open"))
                            {
                                report.AddReportStep(filtertype + " toggle is expanded on clicking the expand button", filtertype + " toggle is expanded on clicking the expand button", StepResult.PASS);

                                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                                IWebElement firstItem = driver.FindElement(obj_FilterItem);
                                itemName = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", firstItem);
                                String filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", firstItem);
                                js.ExecuteScript("arguments[0].click();", firstItem);

                                ///string filterjobcount = com_getobjProperty(obj_FilterItem, "data-count");

                                //itemName = com_getobjProperty(obj_FilterItem, "data-display");
                                //string filterjobcount = com_getobjProperty(obj_FilterItem, "data-count");
                                //Thread.Sleep(2000);
                                //com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Clicked on the " + filtertype + " " + itemName, "Filter Module || Problem in  clicking the first " + filtertype + " CheckBox");

                                //Changes for Unchecking check box  stemcell client
                                string appliedFilter = "";
                                //-------------- 

                                if (!(driver.Url.Contains("jobs.allinahealth.org") || driver.Url.Contains("primark.com")))
                                {
                                    WaitForObject(TalentBrewUI.section_appliedFilter, 30);

                                    //Changes for Unchecking check box  stemcell client
                                    if (driver.FindElement(TalentBrewUI.section_appliedFilter).Displayed)
                                    {
                                        report.AddReportStep("Verify Applied fliter is displayed", "Filter Module||Applied Filter is displayed", StepResult.PASS);
                                        appliedFilter = driver.FindElement(TalentBrewUI.section_appliedFilter).Text;
                                    }
                                    else
                                    {

                                        Actions act = new Actions(driver);
                                        act.SendKeys(Keys.Home).Build().Perform();
                                        Thread.Sleep(2000);
                                        if (driver.FindElement(TalentBrewUI.section_appliedFilter).Displayed)
                                        {
                                            report.AddReportStep("Verify Applied fliter is displayed", "Filter Module||Applied Filter is displayed", StepResult.PASS);
                                            appliedFilter = driver.FindElement(TalentBrewUI.section_appliedFilter).Text;
                                        }
                                        else
                                            report.AddReportStep("Verify Applied fliter is displayed", "Filter Module||Applied Filter is not displayed", StepResult.FAIL);
                                    }
                                }

                                System.Threading.Thread.Sleep(3000);
                                //string alljobcount = com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results");
                                string alljobcount = driver.FindElement(TalentBrewUI.section_Searchresults).GetAttribute("data-total-results");

                                //string filterjobcount = com_getobjProperty(obj_FilterItem, "data-count");
                                Thread.Sleep(2000);
                                if (filterjobcount.Equals(alljobcount))
                                    report.AddReportStep(filtertype + " Job results are displayed as per the filter selected", filtertype + " Job results are displayed as per the filter selected", StepResult.PASS);
                                else
                                    report.AddReportStep(filtertype + " Job results are not displayed as per the filter selected", filtertype + " Job results are not displayed as per the filter selected", StepResult.WARNING);

                                if (driver.Url.Contains("primark.com"))
                                {
                                    js = driver as IJavaScriptExecutor;
                                    firstItem = driver.FindElement(obj_FilterItem);
                                    itemName = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", firstItem);
                                    filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", firstItem);
                                    js.ExecuteScript("arguments[0].click();", firstItem);
                                    report.AddReportStep("Uncheck on the item - " + firstItem, "Unchecked on the item - " + firstItem, StepResult.PASS);
                                }

                                //if (com_IsElementPresent(TalentBrewUI.section_appliedFilter))
                                else if (driver.FindElement(TalentBrewUI.section_appliedFilter).Displayed)
                                {
                                    //com_ContainsTxt(TalentBrewUI.section_appliedFilter, itemName, "Filter By section matches the " + filtertype + " selected " + itemName, "Filter By section does not matches the " + filtertype + " selected ");
                                    //string a = com_GetText(TalentBrewUI.section_appliedFilter);
                                    //string b = itemName.Trim();

                                    if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                                    {
                                        driver.FindElement(TalentBrewUI.btn_close).Click();
                                        Thread.Sleep(2000);
                                    }
                                    if (driver.FindElement(TalentBrewUI.section_appliedFilter).Text.ToUpper().Contains(itemName.Trim().ToUpper()))
                                    {
                                        report.AddReportStep("Filter By section matches the " + filtertype + " selected " + itemName, "Filter By section matches the " + filtertype + " selected ", StepResult.PASS);
                                    }
                                    else
                                    {
                                        report.AddReportStep("Filter By section matches the " + filtertype + " selected " + itemName, "Filter By section does not matches the " + filtertype + " selected ", StepResult.FAIL);
                                    }

                                    if (driver.Url.Contains("jobs.cornerstonebrands") || driver.Url.Contains("physicianjobs.interiorhealth") || driver.Url.Contains("careers.rochesterregional") || driver.Url.Contains("cox") || driver.Url.Contains("advocatehealth") || driver.Url.Contains("internaljobs.coxenterprises") || driver.Url.Contains("cliffsnaturalresources.com") || driver.Url.Contains("raymondjames.com") || driver.Url.Contains("columbusregional.com") || driver.Url.Contains("jobs.abbott") || driver.Url.Contains("baxter.com") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("clarityrobotics.jobsattmp.com") || driver.Url.Contains("jobs.sungardas.com") || driver.Url.Contains("careers.aldi.us") || driver.Url.Contains("jobs.ehhi.com") || driver.Url.Contains("jobs.owenscorningcareers.com") || driver.Url.Contains("jobs.ncci.com") || driver.Url.Contains("jobs.conduent.com") || driver.Url.Contains("careers.mmc.com") || driver.Url.Contains("jobs.clevelandcliffs.com") || driver.Url.Contains("recrutement.bpce.fr") || driver.Url.Contains("valleyhealthsystemlv"))
                                        com_ClickOnInvisibleElement(By.LinkText(itemName.ToUpper()), "Filter Module || Clicked on the delete icon for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking over the applied filter link for the the first " + filtertype);
                                    else if (driver.Url.Contains("careers.nationalgridus.com") || driver.Url.Contains("jobs.cvshealth.com") || driver.Url.Contains("jobs.matheny.org") || driver.Url.Contains("job-search.astrazeneca.cn") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("www.unmhjobs.com") || driver.Url.Contains("thalesgroup.com") || driver.Url.Contains("jobs.loram.com") || driver.Url.Contains("eurovia.com") || driver.Url.Contains("emplois.vinci-autoroutes.com") || driver.Url.Contains("job-search.astrazeneca.com") || driver.Url.Contains("www.tmpworldwideindia.com") || driver.Url.Contains("capitalonecareers") || driver.Url.Contains("jobs.spectrum.com") || driver.Url.Contains("brighamandwomensfaulkner") || driver.Url.Contains("premisehealth") || driver.Url.Contains("unifirst") || driver.Url.Contains("disneycareers") || driver.Url.Contains("endurance") || driver.Url.Contains("campus.capitalone") || driver.Url.Contains("www.nestleusacareers") || driver.Url.Contains("careers.nationalgridus") || driver.Url.Contains("jobs.tenethealth.com"))
                                        com_ClickOnInvisibleElement(TalentBrewUI.appliedFilter_Link, "Filter Module || Clicked over the applied filter link for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking over the applied filter link for the the first " + filtertype);
                                    else if (driver.Url.Contains("careers.nyp") || driver.Url.Contains("target.searchgreatcareers.com") || driver.Url.Contains("target.searchgreatcareers"))
                                        com_ClickOnInvisibleElement(By.LinkText(itemName.ToLower()), "Filter Module || Clicked on the delete icon for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking over the applied filter link for the the first " + filtertype);
                                    else if (com_IsElementPresent(By.LinkText(itemName)))
                                        com_ClickOnInvisibleElement(By.LinkText(itemName), "Filter Module || Clicked on the delete icon for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking over the applied filter link for the the first " + filtertype);
                                    else
                                        com_ClickOnInvisibleElement(By.PartialLinkText(itemName), "Filter Module || Clicked on the delete icon for the " + filtertype + " - " + itemName, "Filter Module || Problem in  clicking over the applied filter link for the the first " + filtertype);

                                    app_loading(TalentBrewUI.section_appliedFilter);
                                    //System.Threading.Thread.Sleep(5000);
                                    if (driver.Url.Contains("springhire.lowes.com") || driver.Url.Contains("raymondjames.com"))
                                        Thread.Sleep(5000);
                                    if (driver.Url.Contains("target.searchgreatcareers"))
                                        WaitForObject(TalentBrewUI.section_appliedFilter, 100);
                                    //if (com_IsElementPresent(TalentBrewUI.section_appliedFilter))

                                    if (!(driver.Url.Contains("usa.jobs.scotiabank.com") || driver.Url.Contains("ikea") || driver.Url.Contains("coop") || driver.Url.Contains("sleepnumber")))
                                    {
                                        if (driver.FindElement(TalentBrewUI.section_appliedFilter).Displayed)
                                            com_DoesNOTContainsTxt(TalentBrewUI.section_appliedFilter, itemName, "Filter By section removes the " + filtertype + "-" + itemName + "on clicking the delete icon", "Filter By section does not removes the " + filtertype + "-" + itemName + "on clicking the delete icon");
                                    }
                                    else
                                        report.AddReportStep("Filter By section removes the " + filtertype + " on clicking the delete icon -  " + itemName, "Filter By section removes the " + filtertype + " on clicking the delete icon -  " + itemName, StepResult.PASS);

                                    //com_DoesNOTContainsTxt(TalentBrewUI.section_appliedFilter, itemName, "Filter By section removes the " + filtertype + "-" + itemName + "on clicking the delete icon", "Filter By section does not removes the " + filtertype + "-" + itemName + "on clicking the delete icon");
                                    //com_VerifyObjNOTPresent(TalentBrewUI.section_appliedFilter, "Filter By section removes the " + filtertype + " on clicking the delete icon -  " + itemName, "Filter By section does not removes the " + filtertype + "on clicking the delete icon ");
                                }
                                else
                                {
                                    com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Unchecked the " + filtertype + " " + itemName, "Filter Module || Problem in  Unchecking the first " + filtertype + " CheckBox");
                                    Thread.Sleep(3000);
                                }
                                Thread.Sleep(2000);

                                if (driver.Url.Contains("www.nestleusacareers.com"))
                                {
                                    //string a = com_GetText(TalentBrewUI.Filter_toggle);
                                    if (!com_IsElementPresent(By.Id("keyword-tag")))
                                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                    Thread.Sleep(3000);
                                    if (!com_IsElementPresent(By.Id("keyword-tag")))
                                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                    Thread.Sleep(4000);
                                }
                                if (!(driver.Url.Contains("primark.com") || driver.Url.Contains("ikea") || driver.Url.Contains("coop") || driver.Url.Contains("sleepnumber")))
                                {
                                    if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                                    {
                                        Thread.Sleep(2000);
                                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_button8, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                                    }
                                    com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button");
                                    //app_loading(TalentBrewUI.expand_Filtertype);
                                    Thread.Sleep(3000);
                                }
                            }
                            else
                            {
                                report.AddReportStep(filtertype + " toggle is not expanded on clicking the expand button", filtertype + " toggle is not expanded on clicking the expand button", StepResult.FAIL);
                            }
                        }
                    }

                    //Clear All Button flow
                    if (!driver.Url.Contains("capitalonecareers"))
                    {
                        if (driver.Url.Contains("www.nestleusacareers.com"))
                        {
                            //string a = com_GetText(TalentBrewUI.Filter_toggle);
                            if (!com_IsElementPresent(By.Id("keyword-tag")))
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                            Thread.Sleep(3000);
                            if (!com_IsElementPresent(By.Id("keyword-tag")))
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                            Thread.Sleep(3000);
                        }
                        classAttr = com_getobjProperty(obj_filtertype, "class");
                        if (!classAttr.Contains("child-open"))
                        {
                            com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clear All || Clicked on the " + filtertype + "expand button", "Filter Module || Clear All || Problem in clicking the " + filtertype + " expand button");
                            if (!(driver.Url.Contains("primark.com") || driver.Url.Contains("ikea") || driver.Url.Contains("coop")))
                                WaitForObject(TalentBrewUI.expand_Filtertype, 100);
                            //Thread.Sleep(3000);
                            if (!com_IsSelected(obj_FilterItem))
                            {
                                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                                IWebElement firstItem = driver.FindElement(obj_FilterItem);
                                itemName = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", firstItem);
                                String filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", firstItem);
                                js.ExecuteScript("arguments[0].click();", firstItem);
                                report.AddReportStep("Filter Module || Clear All || check the " + filtertype + " " + itemName, "Filter Module || Clear All ||  checked the " + filtertype + " " + itemName, StepResult.PASS);
                                //com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Clear All ||  checked the " + filtertype + " " + itemName, "Filter Module || Clear All || Problem in  checking the first " + filtertype + " CheckBox");
                                if (!(driver.Url.Contains("allinahealth.org") || driver.Url.Contains("primark.com")))
                                    WaitForObject(TalentBrewUI.section_appliedFilter, 100);
                                else
                                    Thread.Sleep(2000);
                            }
                        }
                        else
                        {
                            Thread.Sleep(2000);
                            if (!com_IsSelected(obj_FilterItem))
                            {
                                com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Clear All ||  checked the " + filtertype + " " + itemName, "Filter Module || Clear All ||  Problem in  checking the first " + filtertype + " CheckBox");
                                WaitForObject(TalentBrewUI.section_appliedFilter, 100);
                                Thread.Sleep(3000);
                            }
                        }
                        if (driver.Url.Contains("www.nestleusacareers.com"))
                        {
                            //string a = com_GetText(TalentBrewUI.Filter_toggle);
                            if (!com_IsElementPresent(By.Id("keyword-tag")))
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                            Thread.Sleep(3000);
                            if (!com_IsElementPresent(By.Id("keyword-tag")))
                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                            Thread.Sleep(5000);
                        }
                        if ((com_IsElementPresent(TalentBrewUI.btn_searchfilterclear) && !driver.Url.Contains("capitalonecareers.com")))
                        {
                            if (com_ClickOnInvisibleElement(TalentBrewUI.btn_searchfilterclear, "Filter Module || Clear All ||  Clicked on the 'Clear All' button", "Filter Module || Clear All ||  Problem in Clicking on the 'Clear All' button"))
                            {
                                Thread.Sleep(1000);
                                //if (!driver.Url.Contains("primark.com"))
                                app_loading(TalentBrewUI.section_appliedFilter);
                                //Waiting till results are loaded
                                if (driver.Url.Contains("target.searchgreatcareers"))
                                {
                                    for (int i = 0; i < 100; i++)
                                    {
                                        if (driver.FindElement(By.XPath("//ul/li/a")).Enabled)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Thread.Sleep(1000);
                                        }
                                    }
                                }

                                if (!com_IsSelected(obj_FilterItem))
                                {
                                    report.AddReportStep("Check Box " + itemName + " is cleared on clicking Clear All button", "Check Box " + itemName + " is cleared on clicking Clear All button", StepResult.PASS);
                                    if (driver.Url.Contains("www.nestleusacareers.com"))
                                    {
                                        //string a = com_GetText(TalentBrewUI.Filter_toggle);
                                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                        Thread.Sleep(3000);
                                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                        Thread.Sleep(3000);
                                    }
                                    if (!(driver.Url.ToLower().Contains("primark.com") || driver.Url.Contains("coop") || driver.Url.Contains("sleepnumber")))
                                        com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clear All || Clicked on the " + filtertype + "collapse button", "Filter Module || Clear All || Problem in clicking the " + filtertype + " collapse button");
                                    //app_loading(TalentBrewUI.expand_Filtertype);
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                    report.AddReportStep("Check Box " + itemName + " is not cleared on clicking Clear All button", "Check Box " + itemName + " is not cleared on clicking Clear All button", StepResult.FAIL);
                                    com_ClickOnInvisibleElement(obj_FilterItem, "Filter Module || Clear All ||  Resetting the " + filtertype + " " + itemName + " because the clear all button is not working", "Filter Module || Clear All ||  Problem in setting the first " + filtertype + " CheckBox because the clear all button is not working");
                                    Thread.Sleep(3000);
                                    com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clear All || Clicked on the " + filtertype + "collapse button", "Filter Module || Clear All || Problem in clicking the " + filtertype + " collapse button");
                                    Thread.Sleep(3000);
                                }
                                if (!(driver.Url.ToLower().Contains("usa.jobs.scotiabank.com") || driver.Url.Contains("ikea") || driver.Url.Contains("sleepnumber")))
                                    com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button");
                            }
                        }
                        else
                        {
                            report.AddReportStep("Filter Module || Clear All || is not available for the client", "Filter Module || Clear All || is not available for the client", StepResult.WARNING);
                        }
                    }
                    else
                    {
                        report.AddReportStep("Filter Module || Clear All || is not available for the client", "Filter Module || Clear All || is not available for the client", StepResult.WARNING);
                    }
                }
                else
                {
                    report.AddReportStep(filtertype + " || " + filtertype + " is not applicable in filter section", filtertype + " || " + filtertype + " is not applicable in filter section", StepResult.WARNING);
                }
            }

            if (filtersectionpresent)
            {
                report.AddReportStep("Filter || Filter section is present in search results page", "Filter || Filter section is present in search results page", StepResult.PASS);
            }
            else
            {
                report.AddReportStep("Filter || Filter section is not present in search results page", "Filter || Filter section is not present in search results page", StepResult.FAIL);
            }

        }




        //*******************************************************************************

        public void app_jobAlerts(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Job Alert");
            if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
            {
                com_NewLaunchUrl(clientUrl);
                string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
                if (ScenarioName.Equals("L2"))
                {

                    if (app_navigateL2(searchKeyword, clientUrl))
                    {
                        app_getFilterCategory();
                        app_getFilterLocation();
                        app_jobAlert(ScenarioName);
                    }

                }
                else if (ScenarioName.Equals("L3"))
                {
                    if (app_navigateL2(searchKeyword, clientUrl))
                    {
                        app_getFilterCategory();
                        app_getFilterLocation();
                    }
                    if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                    {
                        app_jobAlert(ScenarioName);
                    }
                }
                else if (ScenarioName.Equals("L1"))
                {
                    if (app_navigateL2(searchKeyword, clientUrl))
                    {
                        app_getFilterCategory();
                        app_getFilterLocation();
                    }
                    com_NewLaunchUrl(clientUrl);
                    app_jobAlert(ScenarioName);
                }
            }
            else
            {
                report.AddReportStep("Job Alerts is not available", "Job Alerts is not available", StepResult.WARNING);
            }

        }



        private void app_jobAlert(String PageName)
        {
            try
            {
                if (com_VerifyObjPresent(TalentBrewUI.txt_emailAddress, "'Job Alerts' || is displayed Search results page", "'Job Alerts' || is not displayed Search results page"))
                {
                    //string Cat_classAttr = string.Empty;
                    // string Loc_classAttr = string.Empty;
                    String Subscription_emailID = "Selenium" + DateTime.Now.ToString("hhmmss") + "@tmp.com";
                    com_SendKeys(TalentBrewUI.txt_emailAddress, Subscription_emailID, "'Job Alerts' || Email address entered - " + Subscription_emailID, "'Job Alerts' || Not able to enter Email address - " + Subscription_emailID);
                    if (com_VerifyOptionalObjPresent(TalentBrewUI.txt_alertCategory, "'Job Alerts' || Category field is available", "'Job Alerts' || Category field is not available"))
                    {
                        //if(com_GetType(TalentBrewUI.txt_alertCategory).GetElementType().e);
                        if (com_GetTagName(TalentBrewUI.txt_alertCategory).Equals("input"))
                        {
                            com_SendKeys(TalentBrewUI.txt_alertCategory, RunManager.CategoryName, "'Job Alerts' || " + RunManager.CategoryName + " Category entered into Category text box.", "'Job Alerts' || Not able to enter Category into Category text box");
                            Thread.Sleep(3000);
                            IList<IWebElement> CategoriesAvailable = com_getListObjectByTagName(TalentBrewUI.alertCategoryList, "a");
                            if (CategoriesAvailable != null)
                            {
                                try
                                {
                                    CategoriesAvailable[0].Click();
                                    report.AddReportStep(RunManager.CategoryName + "'Job Alerts' || " + RunManager.CategoryName + " Category selected from the dropdown", "'Job Alerts' || " + RunManager.CategoryName + " Category selected from the dropdown", StepResult.PASS);
                                }
                                catch (Exception e)
                                {
                                    report.AddReportStep("'Job Alerts' || Not able to select category from the dropdown", "'Job Alerts' || Not able to select category from the dropdown", StepResult.FAIL);
                                }
                            }
                            else
                            {
                                report.AddReportStep("'Job Alerts' || Not able to select category from the dropdown", "'Job Alerts' || Not able to select category from the dropdown", StepResult.FAIL);
                            }
                        }
                        else
                        {
                            com_SelectByIndex(TalentBrewUI.txt_alertCategory, 1, "Selected the category from the drop down", "Cannot select the category");
                        }


                        com_SendKeys(TalentBrewUI.txt_alertCategory, RunManager.CategoryName, "'Job Alerts' || " + RunManager.CategoryName + " Category entered into Category text box.", "'Job Alerts' || Not able to enter Category into Category text box");

                        Thread.Sleep(3000);


                    }
                    else if (com_VerifyOptionalObjPresent(TalentBrewUI.jobAlertKeyword, "'Job Alerts' || job alert keyword field is available", "'Job Alerts' || job alert keyword field is not available"))
                    {
                        com_SendKeys(TalentBrewUI.jobAlertKeyword, RunManager.CategoryName, "'Job Alerts' || " + RunManager.CategoryName + " Category entered into keyword text box.", "'Job Alerts' || Not able to enter Category into keyword text box");
                    }
                    else
                    {
                        report.AddReportStep("'Job Alerts' || job alert dont have category field", "'Job Alerts' || job alert dont have category field", StepResult.FAIL);
                    }


                    if (com_VerifyOptionalObjPresent(TalentBrewUI.txt_alertLocation, "'Job Alerts' || Location field is available", "'Job Alerts' || Location field is not available"))
                    {
                        if (com_GetTagName(TalentBrewUI.txt_alertLocation).Equals("input"))
                        {

                            com_SendKeys(TalentBrewUI.txt_alertLocation, RunManager.LocationName, "'Job Alerts' || Location entered into Location Text box - " + RunManager.LocationName, "'Job Alerts' || Not able to enter Location into Location text box - " + RunManager.LocationName);



                            //else
                            //{
                            //    report.AddReportStep("Job Alert || Auto complete box was not rendered", "Job Alert || Auto complete box was not rendered ", StepResult.FAIL);
                            //}

                            Thread.Sleep(5000);
                            com_ClickOnInvisibleElement(By.LinkText(RunManager.LocationName), "'Job Alerts' || " + RunManager.LocationName + " - Location selected from the dropdown", "'Job Alerts' || Not able to Select location from dropdown");
                            IList<IWebElement> JobAlertLocationsAvailable = com_getListObjectByTagName(TalentBrewUI.jobAlertAutoPopulateLocation, "a");
                            string AutoLocationText = string.Empty;

                            if (JobAlertLocationsAvailable != null)
                            {
                                if (JobAlertLocationsAvailable[0].Displayed)
                                {
                                    AutoLocationText = JobAlertLocationsAvailable[0].Text;
                                    JobAlertLocationsAvailable[0].Click();

                                    report.AddReportStep("Job Alert || Location" + AutoLocationText + " selected from Auto complete box", "Job Alert || Location" + AutoLocationText + " selected from autocomplete box", StepResult.PASS);
                                    //com_ClickOnInvisibleElement(TalentBrewUI.btn_AddLocation, "Job Matching || Location filter - Add button clicked", "Job Matching || Location filter - Add button not clickable");
                                }
                                else
                                {
                                    report.AddReportStep("Job Alert || Not able to select the Location", "Job Alert || Not able to select the Location ", StepResult.WARNING);
                                }

                            }
                            else
                            {
                                report.AddReportStep("Job Alert || Auto complete box was not rendered", "Job Alert || Auto complete box was not rendered ", StepResult.FAIL);
                            }
                        }
                        else
                        {
                            com_SelectByIndex(TalentBrewUI.txt_alertLocation, 1, "Selected the location from the drop down", "Cannot select the location");

                        }
                    }



                    else
                    {
                        report.AddReportStep("'Job Alerts' || job alert dont have location field", "'Job Alerts' || job alert dont have location field", StepResult.FAIL);
                    }


                    if (com_IsElementPresent(TalentBrewUI.txt_MobilePhone))
                    {
                        com_SendKeys(TalentBrewUI.txt_MobilePhone, "9087834521", "entered the mobile number", "Cannot enter Mobile number");
                    }

                    if (com_IsElementPresent(TalentBrewUI.txt_Company))
                    {
                        com_SendKeys(TalentBrewUI.txt_Company, "Company", "entered the Company Name", "Cannot enter Company Name");
                    }

                    if (com_IsElementPresent(TalentBrewUI.txt_title))
                    {
                        com_SendKeys(TalentBrewUI.txt_title, "Job title", "entered the title", "Cannot enter title");
                    }

                    if (com_IsElementPresent(TalentBrewUI.txt_FirstName))
                    {
                        com_SendKeys(TalentBrewUI.txt_FirstName, "first name", "entered the First name", "Cannot enter First name");
                    }

                    if (com_IsElementPresent(TalentBrewUI.txt_LastName))
                    {
                        com_SendKeys(TalentBrewUI.txt_LastName, "Last name", "entered the last name", "Cannot enter Last name");
                    }



                    com_ClickOnInvisibleElement(TalentBrewUI.btn_add, "'Job Alerts' || Add button clicked", "Not able to click Add button");
                    if (com_VerifyObjPresent(TalentBrewUI.addedKeyword, "'Job Alerts' || Category - Loaction keyword added for alert", "'Job Alerts' || Category - Location keyword not added for alert"))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_alertSubmit, "'Job Alerts' || Submit button clicked", "Not able to click Submit button");
                        com_VerifyObjPresent(TalentBrewUI.SuccessMessage, "'Job Alerts' ||  'Your subscription was submitted successfully ' Displayed", "Your subscription was submitted successfully ' Not Displayed");
                    }

                }

                else
                {
                    report.AddReportStep("'Job Alerts' is not displayed", "'Job Alerts' is not displayed", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("'Job Alerts' encountered execption", "'Job Alerts' encountered execption", StepResult.FAIL);
            }
        }

        private void app_getFilterLocation()
        {
            if (driver.Url.Contains("technicolor"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
            }

            if (driver.Url.Contains("astrazeneca"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button1, "Clicked on Filter module to fetch the filter options", "Problem in clicking the Filter module to fetch the filter options");
            }

            if (driver.Url.Contains("jobs.capitalone.co.uk"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button3, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
            }

            if (driver.Url.Contains("internalcareers.bbcworldwide.com"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.Filter_button4, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                Thread.Sleep(2000);
            }

            //Filter_button4

            string Loc_classAttr = com_getobjProperty(TalentBrewUI.chk_cityToggle, "class");
            //if(!string.IsNullOrEmpty(Loc_classAttr))

            if (Loc_classAttr != null && Loc_classAttr != string.Empty)
            {
                if (!Loc_classAttr.Contains("child-open"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.chk_cityToggle, "Expanded Location list from filter to fetch the location value", "Not able able to expand Location list from filter to fetch the location value");
                }
                //if (driver.Url.Contains("jobsatnovanthealth.org"))
                //{
                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                IWebElement city_object = driver.FindElement(By.Id("city-filter-0"));
                String filterLocation = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", city_object);
                String[] splitLocations = filterLocation.Split(',');
                RunManager.LocationName = splitLocations[0];
                //}
                //else
                //{
                //    String filterLocation = com_getobjProperty(TalentBrewUI.chk_city1, "data-display");
                //    String[] splitLocations = filterLocation.Split(',');
                //    RunManager.LocationName = splitLocations[0];
                //}
            }
            else
            {
                RunManager.LocationName = dataHelper.GetData(DataColumn.JobMatchingLocationSearch);
            }
        }


        private void app_getFilterCategory()
        {
            bool IsCategoryFilter = false;
            string Cat_classAttr = string.Empty;
            if (com_IsElementPresent(TalentBrewUI.chk_categoryToggle))
            {
                Cat_classAttr = com_getobjProperty(TalentBrewUI.chk_categoryToggle, "class");
                IsCategoryFilter = true;

            }
            else if (com_IsElementPresent(TalentBrewUI.categoryToggleNew))
                Cat_classAttr = com_getobjProperty(TalentBrewUI.categoryToggleNew, "class");
            if (IsCategoryFilter)
            {
                if (!Cat_classAttr.Contains("child-open"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.chk_categoryToggle, "Expanded category list from filter to fetch the category value", "Not able able to expand category list from filter to fetch the category value");
                }
                String filterCategory = com_getobjProperty(TalentBrewUI.chk_category1, "data-display");
                String filterCategorySec = com_getobjProperty(TalentBrewUI.chk_category2, "data-display");
                string pattern = @"[A-Za-z]";
                Match match = Regex.Match(filterCategory, pattern);
                //if(.match(filterCategory))
                //{
                //}
                if (!match.Success)
                {
                    RunManager.CategoryName = filterCategorySec;
                    report.AddReportStep("Filter || First Category in the category filter module is Invalid", "Filter || First Category in the category filter module is Invalid", StepResult.WARNING);
                }
                else
                    RunManager.CategoryName = filterCategory;
            }
            else
            {
                if (!Cat_classAttr.Contains("child-open"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.categoryToggleNew, "Expanded category list from filter to fetch the category value", "Not able able to expand category list from filter to fetch the category value");
                }
                String filterCategory = com_getobjProperty(TalentBrewUI.newToggleCat1, "data-display");
                String filterCategorySec = com_getobjProperty(TalentBrewUI.newToggleCat2, "data-display");
                string pattern = @"[A-Za-z]";
                Match match = Regex.Match(filterCategory, pattern);
                //if(.match(filterCategory))
                //{
                //}
                if (!match.Success)
                {
                    RunManager.CategoryName = filterCategorySec;
                    report.AddReportStep("Filter || First Category in the category filter module is Invalid", "Filter || First Category in the category filter module is Invalid", StepResult.WARNING);
                }
                else
                    RunManager.CategoryName = filterCategory;
            }
        }


        //For Radius disable issue
        private void app_getAdvanceSearchLocation()
        {

            //com_SendKeys(TalentBrewUI.txt_LocationSearch, searchLocation, "Advance Search || Entered the Location in the Location textBox - " + searchLocation, "Advance Search || Problem in entering the location in the location textbox- " + searchLocation);



            IList<IWebElement> LocationLists = com_getListObjectByTagName(TalentBrewUI.all_Location_lnk, "li");

            foreach (IWebElement location in LocationLists)
            {
                if (location.FindElement(By.TagName("a")).GetAttribute("data-lt").Equals("4"))
                {

                    string x = location.FindElement(By.TagName("a")).Text;
                    //com_ClickOnInvisibleElement(By.TagName("a"), "Advance Search || Clicked on location from the sugesstion", "Advance Search || Problem in Clicking the location from the sugesstion");
                    location.FindElement(By.TagName("a")).Click();

                    //if (com_getobjProperty(TalentBrewUI.lst_Radius, "disabled") != null)
                    //{
                    //    //Thread.Sleep(2000);
                    //    driver.Navigate().Refresh();
                    //    com_ClearElement(TalentBrewUI.txt_keywordSearch);
                    //    //com_SendKeys(TalentBrewUI.txt_keywordSearch, searchKeyword, "Advance Search || Entered the keyword in the Keyword textBox - " + searchKeyword, "Advance Search || Problem in entering the keyword in the keyword textbox- " + searchKeyword);
                    //    //com_SendKeys(TalentBrewUI.txt_LocationSearch, x, "Advance Search || Entered the keyword in the Keyword textBox - " + searchKeyword, "Advance Search || Problem in entering the keyword in the keyword textbox- " + searchKeyword);
                    //    //driver.Navigate().Refresh();

                    //    Thread.Sleep(2000);
                    //    //com_ClearElement(TalentBrewUI.txt_LocationSearch);
                    //    //driver.FindElement(TalentBrewUI.txt_keywordSearch).SendKeys(Keys.Tab);
                    //    //driver.FindElement(TalentBrewUI.txt_LocationSearch).SendKeys(Keys.Tab);

                    //}

                }
            }
        }


        public void app_RSSFeed(string clientUrl, string executionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Rss Feed");
            try
            {
                if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
                {
                    com_NewLaunchUrl(clientUrl);

                    //com_HandleAlert(true);

                    string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);

                    if (ScenarioName.Equals("L1"))
                    {
                        VerifyRSS();
                        com_NewLaunchUrl(clientUrl);
                    }

                    else if (ScenarioName.Equals("L2"))
                    {
                        if (app_navigateL2(searchKeyword, clientUrl))
                        {
                            VerifyRSS();
                        }
                    }
                    else if (ScenarioName.Equals("L3"))
                    {
                        if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                        {
                            VerifyRSS();
                        }
                    }
                }
                else
                {
                    report.AddReportStep("RSS Feed is not available", "RSS Feed is not available", StepResult.WARNING);
                }
            }

            catch (Exception e)
            {
                report.AddReportStep("'RSS Feed' encountered execption  - " + e.ToString(), "'RSS Feed' encountered execption - " + e.ToString(), StepResult.FAIL);
            }
        }

        //private void VerifyRSS()
        //{
        //    String CurrentUrl = driver.Url;
        //    //change for newclient Nestle :
        //    if (CurrentUrl.Contains("www.nestlewaterscareers.com/search-site"))
        //        CurrentUrl = CurrentUrl.Replace("/search-site", "/");

        //    else if (CurrentUrl.Contains("www.tmp.com/jobs") || CurrentUrl.Contains("newsearch.tmp.com") || CurrentUrl.Contains("www.aia.co.uk/jobs") || CurrentUrl.Contains("dc.tmp.com/jobs") || CurrentUrl.Contains("www.tmpworldwideindia.com/jobs"))
        //        CurrentUrl = CurrentUrl.Replace("/jobs", "/");

        //    else if (CurrentUrl.Contains("www.br.tmp.com/jobs") || CurrentUrl.Contains("newsearch.tmp.com") || CurrentUrl.Contains("www.tmp.ca/jobs") || CurrentUrl.Contains("www.fr.tmp.ca/jobs") || CurrentUrl.Contains("www.tmp.de/jobs") || CurrentUrl.Contains("www.tmpw.com.sg/jobs"))
        //        CurrentUrl = CurrentUrl.Replace("/jobs/", "/");

        //    else if (CurrentUrl.Contains("www.capitalonecareers.co.uk"))
        //        CurrentUrl = CurrentUrl.Replace("http://www.capitalonecareers", "https://jobs.capitalone");

        //    else if (CurrentUrl.Contains("www.utc.com/Careers/Work-With-Us/Pages/default.aspx"))
        //        CurrentUrl = "https://jobs.utc.com/";

        //    else if (CurrentUrl.Contains("https://emplois.gapcanada.ca/home"))
        //        CurrentUrl = CurrentUrl.Replace("/home", "/");

        //    else if (CurrentUrl.Contains("search-jobs") || CurrentUrl.Contains("redirect"))
        //    {
        //        CurrentUrl = CurrentUrl.Split('/')[0] + "/" + CurrentUrl.Split('/')[1] + "/" + CurrentUrl.Split('/')[2] + "/";
        //        //string CurrentUrl1 = CurrentUrl.Split('/')[0];
        //        //string CurrentUrl2 = CurrentUrl.Split('/')[1];
        //        //string CurrentUrl3 = CurrentUrl.Split('/')[2];
        //        //CurrentUrl = CurrentUrl1+"/" + CurrentUrl2 + "/" + CurrentUrl3 + "/";
        //    }

        //    //com_HandleAlert(true);

        //    CurrentUrl += "rss";

        //    com_NewLaunchUrl(CurrentUrl);
        //    Thread.Sleep(2000);
        //    //com_HandleAlert(true);

        //    String JobURl = String.Empty;

        //    try
        //    {
        //        string St = driver.PageSource;
        //        Thread.Sleep(3000);
        //        int pFrom = St.IndexOf("</description><link>") + 20;
        //        int pTo = St.LastIndexOf("</link>");
        //        String result = St.Substring(pFrom, pTo - pFrom);
        //        Thread.Sleep(1000);
        //        JobURl = result.Split('<')[0];
        //        Thread.Sleep(1000);
        //        //int pTo = St.IndexOf("</link><guid");
        //        //String result = St.Substring(pFrom, pTo);
        //        //JobURl = result;
        //    }
        //    catch (Exception e)
        //    {
        //    }

        //    if (!String.IsNullOrEmpty(JobURl))
        //    {
        //        report.AddReportStep("Launching the First Job Link in RSS Page", "Launching the First Job Link in RSS Page", StepResult.PASS);

        //        com_NewLaunchUrl(JobURl);
        //        if (com_IsElementPresent(TalentBrewUI.applyButton1) || com_IsElementPresent(TalentBrewUI.applyButton2))
        //        {

        //            if (com_IsElementPresent(TalentBrewUI.applyButton1))
        //                com_VerifyObjPresent(TalentBrewUI.applyButton1, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
        //            else
        //                com_VerifyObjPresent(TalentBrewUI.applyButton2, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
        //        }
        //        else
        //            com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");

        //    }
        //    else
        //    {
        //        report.AddReportStep("'RSS feed' ||No jobs are displayed in RSS Feed", "'RSS feed' ||No jobs are displayed in RSS Feed", StepResult.FAIL);
        //    }



        //    //WaitForObject(TalentBrewUI.rssFeedHeader);
        //    //Thread.Sleep(2000);
        //    //if (com_VerifyObjPresent(TalentBrewUI.rssFeedHeader, "'RSS feed' || Rss Feed page is opened", "'RSS feed' || Rss Feed page is not opened"))
        //    //{
        //    //    IList<IWebElement> AvialableJobsList = com_getListObjectByTagName(TalentBrewUI.rssJobsList, "a");
        //    //    if (AvialableJobsList != null)
        //    //    {
        //    //        report.AddReportStep("'RSS feed' ||List of available jobs displayed in RSS Feed", "'RSS feed' ||List of available jobs displayed in RSS Feed", StepResult.PASS);
        //    //        com_ClickListElement(AvialableJobsList[0]);
        //    //        WaitForObject(TalentBrewUI.rss_L3page);

        //    //        if (com_IsElementPresent(TalentBrewUI.applyButton1) || com_IsElementPresent(TalentBrewUI.applyButton2))
        //    //        {
        //    //            if (com_IsElementPresent(TalentBrewUI.applyButton1))
        //    //                com_VerifyObjPresent(TalentBrewUI.applyButton1, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
        //    //            else
        //    //                com_VerifyObjPresent(TalentBrewUI.applyButton2, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
        //    //        }
        //    //        else
        //    //            com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
        //    //    }
        //    //    else
        //    //        report.AddReportStep("'RSS feed' ||No jobs are displayed in RSS Feed", "'RSS feed' ||No jobs are displayed in RSS Feed", StepResult.FAIL);
        //    //}
        //    //else
        //    //    report.AddReportStep("RSS Feeds is not opened", "RSS Feeds is not opened", StepResult.FAIL);
        //}

        //............................RSS Fixed-Krishna--------06/09................
        private void VerifyRSS()
        {
            String CurrentUrl = driver.Url;
            //change for newclient Nestle :
            if (CurrentUrl.Contains("www.nestlewaterscareers.com/search-site"))
                CurrentUrl = CurrentUrl.Replace("/search-site", "/");

            else if (CurrentUrl.Contains("www.tmp.com/jobs") || CurrentUrl.Contains("newsearch.tmp.com") || CurrentUrl.Contains("www.aia.co.uk/jobs") || CurrentUrl.Contains("dc.tmp.com/jobs") || CurrentUrl.Contains("www.tmpworldwideindia.com/jobs"))
                CurrentUrl = CurrentUrl.Replace("/jobs", "/");

            else if (CurrentUrl.Contains("www.br.tmp.com/jobs") || CurrentUrl.Contains("newsearch.tmp.com") || CurrentUrl.Contains("www.tmp.ca/jobs") || CurrentUrl.Contains("www.fr.tmp.ca/jobs") || CurrentUrl.Contains("www.tmp.de/jobs") || CurrentUrl.Contains("www.tmpw.com.sg/jobs"))
                CurrentUrl = CurrentUrl.Replace("/jobs/", "/");

            else if (CurrentUrl.Contains("www.capitalonecareers.co.uk"))
                CurrentUrl = CurrentUrl.Replace("http://www.capitalonecareers", "https://jobs.capitalone");

            else if (CurrentUrl.Contains("www.utc.com/Careers/Work-With-Us/Pages/default.aspx"))
                CurrentUrl = "https://jobs.utc.com/";

            else if (CurrentUrl.Contains("https://jobs.gap.co.uk/gap-home"))
                CurrentUrl = "https://jobs.gap.co.uk/";

            else if (CurrentUrl.Contains("https://www.br.latam.tmp.com/jobs/"))
                CurrentUrl = "https://www.br.latam.tmp.com/";

            else if (CurrentUrl.Contains("https://emplois.gapfrance.fr/gap-home"))
                CurrentUrl = "https://emplois.gapfrance.fr/";

            else if (CurrentUrl.Contains("https://emplois.gapcanada.ca/home"))
                CurrentUrl = CurrentUrl.Replace("/home", "/");

            else if (CurrentUrl.Contains("search-jobs") || CurrentUrl.Contains("redirect"))
            {
                CurrentUrl = CurrentUrl.Split('/')[0] + "/" + CurrentUrl.Split('/')[1] + "/" + CurrentUrl.Split('/')[2] + "/";

            }

            //com_HandleAlert(true);

            CurrentUrl += "rss";

            com_NewLaunchUrl(CurrentUrl);
            Thread.Sleep(2000);
            //com_HandleAlert(true);

            String JobURl = String.Empty;

            try
            {
                string St = driver.PageSource;
                Thread.Sleep(3000);
                int pFrom = St.IndexOf("</description><link>") + 20;
                int pTo = St.LastIndexOf("</link>");
                String result = St.Substring(pFrom, pTo - pFrom);
                Thread.Sleep(1000);
                JobURl = result.Split('<')[0];
                Thread.Sleep(1000);

            }
            catch (Exception e)
            {
            }

            if (!String.IsNullOrEmpty(JobURl))
            {
                report.AddReportStep("Launching the First Job Link in RSS Page", "Launching the First Job Link in RSS Page", StepResult.PASS);

                com_NewLaunchUrl(JobURl);
                if (com_IsElementPresent(TalentBrewUI.applyButton1) || com_IsElementPresent(TalentBrewUI.applyButton2))
                {

                    if (com_IsElementPresent(TalentBrewUI.applyButton1))
                        com_VerifyObjPresent(TalentBrewUI.applyButton1, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
                    else
                        com_VerifyObjPresent(TalentBrewUI.applyButton2, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");
                }
                else
                    com_VerifyOptionalObjPresent(TalentBrewUI.applyButtonwithCaps, "'RSS feed' ||Navigated to Job Description page on Clicking First job", "'RSS feed' ||Does not navigated to Job Description page on Clicking First job");

            }
            else
            {
                report.AddReportStep("'RSS feed' ||No jobs are displayed in RSS Feed", "'RSS feed' ||No jobs are displayed in RSS Feed", StepResult.FAIL);
            }
        }



        public void app_JobMatching(string clientUrl, string ExecutionStatus, string ScenarioName)
        {
            report.AddScenarioHeader("Job Matching");
            string actualTitle = "";
            string expectedTitle = "Authorize | LinkedIn";
            String expectedTitle2 = "Sign In to LinkedIn";
            bool JobMatchingWindowFound = false;

            String error = "Error";
            String exception = "Exception";
            String driverTitle = "";
            ReusableComponents rc = new ReusableComponents(driver, report, wdHelper, dataHelper);
            String exec = "";
            try
            {
                if (ExecutionStatus.Contains("Yes") && ExecutionStatus.Contains(ScenarioName))
                {
                    report.AddReportStep("Start Of the Testcase", "Start Of the Testcase", StepResult.PASS);

                    //Latest modified --08-10
                    try
                    {
                        driver.Manage().Cookies.DeleteAllCookies();
                        report.AddReportStep("Clearing cookies", "Cleared cookies successfully", StepResult.PASS);
                        Thread.Sleep(2000);
                    }
                    catch (Exception e)
                    {
                        report.AddReportStep("Clearing cookies", "Problem in clearing cookies", StepResult.WARNING);

                    }

                    //List<Cookie> allCookies = driver.Manage().Cookies.AllCookies.ToList();
                    //foreach (Cookie cookie in allCookies)
                    //{
                    //    driver.Manage().Cookies.DeleteCookieNamed(cookie.Name);
                    //}

                    com_LaunchUrl(clientUrl);

                    //Rajesh--
                    //  deleteCookieNamed(cookie.getName());

                    if (driver.Url.Contains("jobs.catalina.com"))
                    {
                        if (com_IsElementPresent(By.XPath("//div[contains(@class, 'optanon-alert-box-button-middle')]//a[text()='Accept All Cookies']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//div[contains(@class, 'optanon-alert-box-button-middle')]//a[text()='Accept All Cookies']"), "clicked on-Accept All cookies button", "not clicked on-Accept All cookies button");
                            Thread.Sleep(2000);
                        }
                    }
                    else if (driver.Url.Contains("clarityrobotics.jobsattmp.com"))
                    {

                        if (com_IsElementPresent(By.XPath("//*[text()='Welcome to TalentBrew']/..//button[text()='Next']")))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//*[text()='Welcome to TalentBrew']/..//button[text()='Next']"), "Next button-clicked", "Next button-not clicked");
                            Thread.Sleep(2000);
                            if (com_IsElementPresent(By.XPath("//*[text()='Search Capabilities']/..//button[text()='Exit ']")))
                            {
                                com_ClickOnInvisibleElement(By.XPath("//*[text()='Search Capabilities']/..//button[text()='Exit ']"), "Clicked on Exit button", "Not Clicked on EXit Button");
                                Thread.Sleep(2000);
                            }

                        }
                    }
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
                    //------
                    string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
                    if (ScenarioName.Equals("L2"))
                    {

                        bool result = app_navigateL2(searchKeyword, clientUrl);
                        if (result)
                        {
                            report.AddReportStep("loading of the search page", "Job search page is loaded", StepResult.PASS);
                        }
                        else
                        {
                            report.AddReportStep("loading of the  search page", " Job search page is not loaded", StepResult.FAIL);
                        }
                        if (com_IsElementPresent(TalentBrewUI.link_JobMatching3))
                        {
                            if (com_VerifyObjPresent(TalentBrewUI.link_JobMatching3, "Job Matching || Job Matching link present in search page", "Job Matching || Job matching link not present in search page"))
                            {

                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching3, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                        }
                        else
                        {
                            report.AddReportStep("Job Matching || Job Matching link is not available", "Job Matching || Job Matching link is not available not found in search page", StepResult.FAIL);
                        }
                    }
                    else
                    {

                        if (clientUrl.Contains("jobs.greeneking.co.uk") || clientUrl.Contains("internalcareers.bbcworldwide.com") || clientUrl.Contains("www.jobs-ups.com"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.Searchbutton))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.Searchbutton, "clicked on search button", "failed to click on search button");
                                Thread.Sleep(3000);
                            }
                        }
                        else if (driver.Url.Contains("jobs.parexel.com") || driver.Url.Contains("jobs.parexel.co.jp") || driver.Url.Contains("jobs.atos.net") || driver.Url.Contains("job-search.astrazeneca") || driver.Url.Contains("lockheedmartinjobs.com"))
                        {
                            Thread.Sleep(5000);
                        }

                        //Rajesh
                        else if (driver.Url.Contains("jobs-ups") || driver.Url.Contains("campus.capitalone.com") || driver.Url.Contains("utc.com"))
                        {
                            if (!driver.Url.Contains("jobs.otis.utc.com"))
                            {
                                if (com_IsElementPresent(By.XPath("//form/a[contains(text(),'Search Jobs')] | //span[contains(text(),'Search Jobs')] | //button[text()='Search Jobs']")))
                                {
                                    com_ClickOnInvisibleElement(By.XPath("//form/a[contains(text(),'Search Jobs')] | //span[contains(text(),'Search Jobs')] | //button[text()='Search Jobs']"), "clicked on search Jobs Link", "failed to click on search Jobs Link");

                                }
                            }
                        }

                        else if (driver.Url.Contains("nespressojobs.com"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            if (com_IsElementPresent(By.XPath("//button[contains(text(),'Open Addtional Job Search')]")))
                            {
                                com_ClickOnInvisibleElement(By.XPath("//button[contains(text(),'Open Addtional Job Search')]"), "clicked on search Jobs Link", "failed to click on search Jobs Link");

                            }
                        }

                        else if (driver.Url.Contains("jobs.hcr-manorcare"))
                        {
                            if (com_IsElementPresent(By.XPath("//a[contains(@class,'search-wrap-toggle')]")))
                            {
                                com_ClickOnInvisibleElement(By.XPath("//a[contains(@class,'search-wrap-toggle')]"), "clicked on search Jobs Link", "failed to click on search Jobs Link");

                            }
                        }
                        //-----
                        //Fix for Client - events.memorialhermann.org Job Matching Issue --Rajesh
                        else if (driver.Url.Contains("events.memorialhermann.org"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//ul//h2[contains(text(),'Career Areas')]"), "Successfully clicked on Career Areas", "Not able to click on Career Areas");
                        }
                        else if (driver.Url.Contains("jobs.greatwolf.com"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                            Thread.Sleep(2000);
                        }
                        else if (driver.Url.Contains("careers.mcafee.com"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                            Thread.Sleep(2000);

                        }
                        else if (driver.Url.Contains("jobs.deluxe.com"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,500)");
                            com_ClickOnInvisibleElement(By.XPath("//button[@class='toggle-button']"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                            Thread.Sleep(2000);

                        }
                        else if (driver.Url.Contains("www.pgcareers.com"))
                        {
                            com_ClickOnInvisibleElement(By.Id("hero-search"), "Successfully clicked on search opportunities", "Not able to click on search opportunities");
                            Thread.Sleep(2000);
                        }

                        //-------------------------------------------------
                        if (driver.Url.Contains("clarityrobotics.jobsattmp.com"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//a[text()='Match your skills']"), "Successfully clicked on Match your skills", "Not able to click on Match your skills");
                        }
                        else if (driver.Url.Contains("jobs.capitalone.co.uk"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("//div[@class='linkedin-ourjob-home']//a[@data-target='LinkedIn job matching']"), "Successfully clicked on Match your skills", "Not able to click on Match your skills");
                        }
                        else if (driver.Url.Contains("careers.mcafee.com"))
                        {
                            com_ClickOnInvisibleElement(By.XPath("(//*[contains(@data-callout-action,'job matching') ])[2]"), "Successfully clicked on Match your skills", "Not able to click on Match your skills");
                        }

                        else if (driver.Url.Contains("www.tmp.com/jobs") || driver.Url.Contains("jobs.stemcell.com") || driver.Url.Contains("moneygramjobs.com") || driver.Url.Contains("jobs.nbc.ca") || driver.Url.Contains("emplois.bnc.ca") || driver.Url.Contains("jobs.caleres.com") || driver.Url.Contains("unmhjobs.com") || driver.Url.Contains("jobs.premisehealth.com"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.link_JobMatching4))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching4, "clicked on Ok button", "Not clicked on OK button");
                            }
                        }
                        else if (driver.Url.Contains("jobs.delltechnologies.com"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.open_Jobmatching))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.open_Jobmatching, "Successfull clicked on Expand button", "Problem in clicking expand button");
                                Thread.Sleep(3000);
                            }
                            if (com_IsElementPresent(TalentBrewUI.link_JobMatching3))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching3, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                        }
                        else if (driver.Url.Contains("careers.duffandphelps.jobs"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.link_JobMatching))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching, "Successfully clicked Job matching link", "Job Matching not clickable");

                                com_New_WaitForObject(By.XPath("//a[@data-callout-action='job matching']"), "Job Matching Link", 10);
                                if (com_IsElementPresent(By.XPath("//a[@data-callout-action='job matching']")))
                                {
                                    com_ClickOnInvisibleElement(By.XPath("//a[@data-callout-action='job matching']"), "clicked on Ok button", "Not clicked on OK button");
                                }
                            }
                        }
                        else if (driver.Url.Contains("canada-francais.pgcareers.com"))
                        {
                            if (com_IsElementPresent(TalentBrewUI.open_Jobmatching1))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.open_Jobmatching1, "Successfull clicked on Expand button", "Problem in clicking expand button");
                                Thread.Sleep(3000);
                            }
                            if (com_IsElementPresent(TalentBrewUI.link_JobMatching3))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching3, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                        }

                        else if (driver.Url.Contains("jobs.assurant.com"))
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0,600)");
                            Thread.Sleep(3000);
                            if (com_IsElementPresent(TalentBrewUI.link_JobMatching))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                        }



                        else if (com_IsElementPresent(TalentBrewUI.link_JobMatching3))
                        {
                            if (com_VerifyObjPresent(TalentBrewUI.link_JobMatching3, "Job Matching || Job Matching link present on the home page", "Job Matching || Job matching link not present on Home page"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching3, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                        }

                        else if (com_IsElementPresent(TalentBrewUI.link_JobMatching))
                        {
                            if (com_VerifyObjPresent(TalentBrewUI.link_JobMatching, "Job Matching || Job Matching link present on the home page", "Job Matching || Job matching link not present on Home page"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }



                        }


                        else if (com_IsElementPresent(TalentBrewUI.link_JobMatching2))
                        {
                            if (com_VerifyObjPresent(TalentBrewUI.link_JobMatching2, "Job Matching || Job Matching link present on the home page", "Job Matching || Job matching link not present on Home page"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.link_JobMatching2, "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                        }

                        //Fix for Client - jobs.mcleodhealth.org Job Matching Issue --Rajesh
                        else if (driver.Url.Contains("jobs.mcleodhealth.org"))
                        {
                            IList<IWebElement> jobMatch = driver.FindElements(TalentBrewUI.link_JobMatching2);
                            int jobMatchCount = jobMatch.Count;
                            if (jobMatchCount > 1)
                            {
                                if (com_VerifyOptionalObjPresent(By.XPath("(//a[contains(@href, 'job-match')])[" + jobMatchCount + "]"), "Successfully clicked Job matching link", "Job Matching not clickable"))
                                    com_ClickOnInvisibleElement(By.XPath("(//a[contains(@href, 'job-match')])[" + jobMatchCount + "]"), "Successfully clicked Job matching link", "Job Matching not clickable");
                            }
                            else
                            {
                                report.AddReportStep("Job Matching || Job Matching link is not available", "Job Matching || Job Matching link is not available", StepResult.FAIL);
                            }

                        }

                        //--------------------------------------

                        else
                        {
                            report.AddReportStep("Job Matching || Job Matching link is not available", "Job Matching || Job Matching link is not available", StepResult.FAIL);
                        }

                    }


                    if (!com_IsElementPresent(TalentBrewUI.Error_Retriving))
                    {
                        IList<string> OpenWindows = driver.WindowHandles;
                        if (OpenWindows != null && OpenWindows.Count() > 1)
                        {
                            actualTitle = driver.CurrentWindowHandle;
                            foreach (string Title in OpenWindows)
                            {
                                if (!Title.Equals(actualTitle))
                                {
                                    driver.SwitchTo().Window(Title);
                                    //if (driver.Title.Contains(expectedTitle) || driver.Title.Contains(expectedTitle2))
                                    //{
                                    JobMatchingWindowFound = true;
                                    report.AddReportStep("Job Matching || authorization page is displayed", "Job Matching || authorization page is displayed", StepResult.PASS);

                                    if (app_JobMatchingLogin())
                                    {
                                        report.AddReportStep("User successfully logged in to LinkedIn Job Matching", "User successfully logged in to LinkedIn Job Matching", StepResult.PASS);

                                        app_JobMatchLocationFilter();
                                        app_JobMatchingSkillFilter();
                                        app_JobMatchingExperienceFilter();
                                        app_JobMatchingLogout();
                                        //driver.Manage().Cookies.DeleteAllCookies();
                                        //driver.Navigate().Refresh();
                                        //Thread.Sleep(3000);
                                        //List<Cookie> allCookies3 = driver.Manage().Cookies.AllCookies.ToList();
                                        //foreach (Cookie cookie in allCookies3)
                                        //{
                                        //    driver.Manage().Cookies.DeleteCookieNamed(cookie.Name);
                                        //}

                                    }
                                    //}
                                    //else
                                    //{
                                    //    report.AddReportStep("JobMatching || User Login unsuccessful", "JobMatching || User login unsuccessful", StepResult.FAIL);
                                    //}

                                    if (!JobMatchingWindowFound)
                                    {
                                        report.AddReportStep("Job Matching || Authorization Page is not loaded.", "Job Matching || Authorization Page is not loaded.", StepResult.FAIL);
                                    }

                                    driver.Close();
                                    driver.SwitchTo().Window(actualTitle);
                                    driver.Manage().Cookies.DeleteAllCookies();
                                    driver.Navigate().Refresh();
                                    Thread.Sleep(3000);
                                    List<Cookie> allCookies1 = driver.Manage().Cookies.AllCookies.ToList();
                                    foreach (Cookie cookie in allCookies1)
                                    {
                                        driver.Manage().Cookies.DeleteCookieNamed(cookie.Name);
                                    }
                                }
                            }
                        }

                        else
                        {
                            Thread.Sleep(5000);
                            actualTitle = driver.Title;
                            //if (actualTitle.Contains(expectedTitle) || actualTitle.Contains(expectedTitle2))
                            //{
                            report.AddReportStep("Job Matching || authorization page is displayed", "Job Matching || authorization page is displayed", StepResult.PASS);
                            if (app_JobMatchingLogin())
                            {
                                report.AddReportStep("User successfully logged in to LinkedIn Job Matching", "User successfully logged in to LinkedIn Job Matching", StepResult.PASS);
                                app_JobMatchLocationFilter();
                                app_JobMatchingSkillFilter();
                                app_JobMatchingExperienceFilter();
                                app_JobMatchingLogout();
                                //driver.Manage().Cookies.DeleteAllCookies();
                                //driver.Navigate().Refresh();
                                //Thread.Sleep(3000);
                                //List<Cookie> allCookies4 = driver.Manage().Cookies.AllCookies.ToList();
                                //foreach (Cookie cookie in allCookies4)
                                //{
                                //    driver.Manage().Cookies.DeleteCookieNamed(cookie.Name);
                                //}
                            }

                            else
                            {
                                if (com_IsElementPresent(TalentBrewUI.Gateway_Timeout))
                                    report.AddReportStep("Job Matching || candidate cannot log out - due to 504 Gateway Timeout", "Job Matching || candidate cannot log out - due to 504 Gateway Timeout", StepResult.FAIL);

                                else
                                    report.AddReportStep("JobMatching || User Login unsuccessful", "JobMatching || User login unsuccessful", StepResult.FAIL);
                            }
                            // }
                            //else
                            //{
                            //    report.AddReportStep("JobMatching || User Login unsuccessful", "JobMatching || User login unsuccessful", StepResult.FAIL);
                            //}
                        }
                    }
                    else
                        report.AddReportStep("Job Matching || Error occured while logging in ", "Job Matching || Error occured while logging in", StepResult.FAIL);
                }

                else
                    report.AddReportStep("JobMatching || Module not present for the client", "JobMatching || Module is not present for the client", StepResult.WARNING);
            }

            catch (Exception e)
            {
                if (com_IsElementPresent(TalentBrewUI.Gateway_Timeout))
                {
                    report.AddReportStep("Job Matching || candidate cannot log out - due to 504 Gateway Timeout", "Job Matching || candidate cannot log out - due to 504 Gateway Timeout", StepResult.FAIL);
                }
                else
                {
                    //report.AddReportStep("'Job Matching' || Encountered an exception - " + e.ToString(), "'Job Matching' || Encountered an exception - " + e.ToString(), StepResult.FAIL);
                    //driverTitle = driver.Title;
                    //exec = rc.getDataCommon(CommonDataColumn.Common_JobMatching);
                    //if (driverTitle.ToLower().Contains(error.ToLower()) || driverTitle.ToLower().Contains(exception.ToLower()))
                    //{
                    //    app_JobMatching(clientUrl, exec, "L1");
                    //}
                    //else
                    //{
                    report.AddReportStep("'Job Matching' || Encountered an exception - " + e.ToString(), "'Job Matching' || Encountered an exception - " + e.ToString(), StepResult.FAIL);
                    //}
                }
            }

        }


        public bool app_JobMatchingLogin()
        {
            try
            {
                string linkedInEmailAddress = dataHelper.GetData(DataColumn.JobMatchingLoginEmail);
                string linkedInPassword = dataHelper.GetData(DataColumn.JobMatchingLoginPassword);
                Thread.Sleep(5000);
                report.AddReportStep("Job Matching || Linked In Job Matching Login page is opened successfully", "Job Matching || Linked In Job Matching Login page is opened successfully", StepResult.PASS);
                try
                {
                    if (com_IsElementPresent(TalentBrewUI.jobmatching_errorPage))
                    {
                        try
                        {
                            driver.Manage().Cookies.DeleteAllCookies();
                            report.AddReportStep("Clearing cookies", "Cleared cookies successfully", StepResult.PASS);
                            driver.Navigate().Refresh();
                            Thread.Sleep(3000);
                        }
                        catch (Exception e)
                        {
                            report.AddReportStep("Clearing cookies", "Problem in clearing cookies", StepResult.WARNING);

                        }

                    }
                }
                catch (Exception e) { }

                if (com_IsElementPresent(By.XPath("//section[@data-selector-name='jobmatchinglogout']")))
                {
                    return true;
                }
                else
                {
                    if (com_IsElementPresent(By.XPath("//button[@id='oauth__auth-form__submit-btn']")))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@id='oauth__auth-form__submit-btn']"), "Clicked-Allow button of authorization page", "not Clicked-Allow button of authorization page");
                        //  com_New_WaitForObject(By.XPath("//section[@data-selector-name='jobmatchinglogout']"), "job Matching Logout section", 120);
                    }
                    if (com_IsElementPresent(TalentBrewUI.txt_LinkedInEmailAddress2) && com_IsElementPresent(TalentBrewUI.txt_LinkedInPassword2))
                    {
                        com_SendKeys(TalentBrewUI.txt_LinkedInEmailAddress2, linkedInEmailAddress, "Job Matching || Successfully entered Email Address", "Job Matching || Unable to enter email address in the text box");
                        Thread.Sleep(1000);
                        com_SendKeys(TalentBrewUI.txt_LinkedInPassword2, linkedInPassword, "Job Matching || Successfully entered Password", "Job Matching || Unable to enter Password in the text box");
                        Thread.Sleep(2000);
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_LinkedInLogin2, "Job Matching || Successfully clicked sign in button", "Job Matching || Unable to click the sign in button");
                        Thread.Sleep(5000);
                    }

                    else if (com_IsElementPresent(TalentBrewUI.txt_LinkedInEmailAddress1) && com_IsElementPresent(TalentBrewUI.txt_LinkedInPassword1))
                    {
                        com_SendKeys(TalentBrewUI.txt_LinkedInEmailAddress1, linkedInEmailAddress, "Job Matching || Successfully entered Email Address", "Job Matching || Unable to enter email address in the text box");
                        Thread.Sleep(1000);
                        com_SendKeys(TalentBrewUI.txt_LinkedInPassword1, linkedInPassword, "Job Matching || Successfully entered Password", "Job Matching || Unable to enter Password in the text box");
                        Thread.Sleep(2000);
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_LinkedInLogin, "Job Matching || Successfully clicked authorize button", "Job Matching || Unable to click the Authorize button");
                        Thread.Sleep(5000);
                    }
                    else if (com_IsElementPresent(TalentBrewUI.txt_LinkedInEmailAddress) && com_IsElementPresent(TalentBrewUI.txt_LinkedInPassword))
                    {
                        com_SendKeys(TalentBrewUI.txt_LinkedInEmailAddress, linkedInEmailAddress, "Job Matching || Successfully entered Email Address", "Job Matching || Unable to enter email address in the text box");
                        Thread.Sleep(1000);
                        com_SendKeys(TalentBrewUI.txt_LinkedInPassword, linkedInPassword, "Job Matching || Successfully entered Password", "Job Matching || Unable to enter Password in the text box");
                        Thread.Sleep(2000);
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_LinkedInLogin, "Job Matching || Successfully clicked authorize button", "Job Matching || Unable to click the Authorize button");
                        Thread.Sleep(5000);
                    }

                }
                if (com_IsElementPresent(By.XPath("//button[@id='oauth__auth-form__submit-btn']")))
                {
                    com_ClickOnInvisibleElement(By.XPath("//button[@id='oauth__auth-form__submit-btn']"), "Clicked-Allow button of authorization page", "not Clicked-Allow button of authorization page");
                    com_New_WaitForObject(By.XPath("//section[@data-selector-name='jobmatchinglogout']"), "job Matching Logout section", 120);
                }
                Thread.Sleep(5000);

                if (com_IsElementPresent(TalentBrewUI.JobMatchingLogoutLink))
                {
                    return true;
                }

                else if (com_IsElementPresent(TalentBrewUI.Gateway_Timeout))
                {
                    report.AddReportStep("Job Matching || candidate cannot log in - due to 504 Gateway Timeout", "Job Matching || candidate cannot log out - due to 504 Gateway Timeout", StepResult.FAIL);
                }
                else if (com_IsElementPresent(TalentBrewUI.JobMatchingFrame))
                {
                    report.AddReportStep("Job Matching || Logged in but 'Logout' button is not present", "Job Matching || Logged in but 'Logout' button is not present", StepResult.FAIL);
                }

            }
            catch (Exception e)
            {
                if (com_IsElementPresent(TalentBrewUI.Error_Retriving))
                {
                    report.AddReportStep("Job Matching || Error occured while retrieving results ", "Job Matching || Error occured while retrieving results", StepResult.FAIL);
                }

                else if (com_IsElementPresent(TalentBrewUI.Gateway_Timeout))
                {
                    report.AddReportStep("Job Matching || candidate cannot log in - due to 504 Gateway Timeout", "Job Matching || candidate cannot log out - due to 504 Gateway Timeout", StepResult.FAIL);
                }
                else
                    report.AddReportStep("'Job Matching' || Candidate could not login", "'Job Matching' || Candidate could not login", StepResult.FAIL);

            }
            return false;
        }

        public void app_JobMatchLocationFilter()
        {
            try
            {
                //if (driver.Url.Contains("astrazeneca") && !driver.Url.Contains("job-search.astrazeneca.fr") && !driver.Url.Contains("job-search.astrazeneca.cn") && !driver.Url.Contains("job-search.astrazeneca.de") && !driver.Url.Contains("job-search.astrazeneca.com"))
                //{
                //    if (com_IsElementPresent(TalentBrewUI.Edit_btn))
                //    {
                //       com_ClickOnInvisibleElement(TalentBrewUI.Edit_btn, "Job Matching ||  Edit button clicked", "Job Matching || - Edit button not clickable");
                //    }
                //    else
                //        report.AddReportStep("Job Matching || Edit Button is not available", "Job Matching || Edit Button is not available", StepResult.FAIL);
                //}

                if (driver.Url.Contains("marksandspencer.com"))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_Filter, "Job Matching || Clicked on 'Expand filter' button", "Job Matching || Problem in clicking on 'Expand filter' button");
                }


                if (driver.Url.Contains("nestleusacareers.com"))
                {
                    com_ClickOnInvisibleElement(By.Id("filter-toggle"), "Job Matching || Clicked on 'Expand filter' button", "Job Matching || Problem in clicking on 'Expand filter' button");
                }
                //-----

                if (!com_IsElementPresent(TalentBrewUI.txt_AddLocation))
                {
                    string classAttr = string.Empty;

                    if (com_IsElementPresent(TalentBrewUI.btn_Location))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.btn_Location, "class");
                        if (!classAttr.Contains("child-open"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_Location, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");
                        }
                    }
                    else if (com_IsElementPresent(TalentBrewUI.btn_Location1))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.btn_Location1, "class");
                        if (!classAttr.Contains("child-open"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_Location1, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");

                        }
                    }
                    else if (driver.Url.Contains("ar-jobs.about.ikea.com") || driver.Url.Contains("nl-jobs.about.ikea.com") || driver.Url.Contains("da-jobs.about.ikea.com") || driver.Url.Contains("lv-jobs.about.ikea.com"))
                    {
                        if (com_IsElementPresent(TalentBrewUI.btn_Location6))
                        {
                            classAttr = com_getobjProperty(TalentBrewUI.btn_Location6, "class");
                            if (!classAttr.Contains("child-open"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_Location6, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");
                            }
                        }
                    }

                    else if (driver.Url.Contains("jobs.nuance.com"))
                    {
                        if (com_IsElementPresent(TalentBrewUI.btn_Location5))
                        {
                            classAttr = com_getobjProperty(TalentBrewUI.btn_Location5, "class");
                            if (!classAttr.Contains("child-open"))
                            {
                                com_ClickOnInvisibleElement(TalentBrewUI.btn_Location5, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");
                            }
                        }
                    }
                    //Changes for be.emploi.primark.com client ->Rajesh
                    else if (com_IsElementPresent(TalentBrewUI.btn_Location4))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.btn_Location4, "class");
                        if (!classAttr.Contains("child-open"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_Location4, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");

                        }
                    }
                    //-----------------------------

                    else if (com_IsElementPresent(TalentBrewUI.btn_Location2))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.btn_Location2, "class");
                        if (!classAttr.Contains("child-open"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_Location2, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");

                        }
                    }

                    else if (com_IsElementPresent(TalentBrewUI.btn_Location3))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.btn_Location3, "class");
                        if (!classAttr.Contains("child-open"))
                        {
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_Location3, "Job Matching ||  Location button clicked", "Job Matching || - Location button not clickable");

                        }

                    }



                }
                //Rajesh--
                WaitForObject(TalentBrewUI.txt_AddLocation, 5);
                IList<IWebElement> Location = null;
                int LocationCount = 0;
                if (driver.Url.Contains("primark.com"))
                {
                    if (!com_VerifyOptionalObjPresent(TalentBrewUI.txt_AddLocation, "Add button is present", "Add button is NotFiniteNumberException present"))
                    {
                        Location = driver.FindElements(By.XPath("//a[contains(text(),'Localisation')] | //a[contains(text(),'Plaats')] | //a[contains(text(),'地區')] | //a[contains(text(),'Localização')] | //a[contains(text(),'Luogo')] | //a[contains(text(),'Localización')]"));
                        LocationCount = Location.Count;
                        com_ClickOnInvisibleElement(By.XPath("(//a[contains(text(),'Localisation')] | //a[contains(text(),'Plaats')] | //a[contains(text(),'地區')] | //a[contains(text(),'Localização')] | //a[contains(text(),'Luogo')] | //a[contains(text(),'Localización')])[" + LocationCount + "]"), "Job Matching || Clicked on 'Expand filter' button", "Job Matching || Problem in clicking on 'Expand filter' button");
                    }
                }
                //----
                if (com_IsElementPresent(TalentBrewUI.txt_AddLocation))
                {

                    string LocationTosearch = dataHelper.GetData(DataColumn.JobMatchingLocationSearch);

                    //Rajesh
                    com_ClickOnInvisibleElement(TalentBrewUI.txt_AddLocation, "Clicked on Add button", "Not able to Click on Add button");
                    //-----
                    com_SendKeys(TalentBrewUI.txt_AddLocation, LocationTosearch, "Job Matching || The Location to be searched was entered-" + LocationTosearch, "Job Matching || The Location to be searched was entered-" + LocationTosearch);
                    //Thread.Sleep(3000);

                    WaitForObject(By.XPath("//*[contains(@class,'mindreader-results-open') or contains(@id,'job-matching-add-location-mindreader')]//a"), 10);
                    //  By.XPath("//[contains(@class,'mindreader-results') or contains(@id,'job-matching-add-location-mindreader')]")

                    //Rajesh
                    IList<IWebElement> LinkedInLocationsAvailable = null;
                    if (driver.Url.Contains("emplois.bnc.ca") || driver.Url.Contains("baystatehealthjobs.com"))
                    {
                        Thread.Sleep(3000);
                        LinkedInLocationsAvailable = com_getListObjectByTagName(By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]"), "a");
                        //Rajesh
                        if (LinkedInLocationsAvailable == null)
                        {
                            com_ClearElement(TalentBrewUI.txt_AddLocation);
                            com_SendKeys(TalentBrewUI.txt_AddLocation, "B", "Job Matching || The Location to be searched was entered-" + LocationTosearch, "Job Matching || The Location to be searched was entered-" + LocationTosearch);
                            WaitForObject(By.XPath("//*[contains(@class,'mindreader-results-open') or contains(@id,'job-matching-add-location-mindreader')]//a"), 10);
                            LinkedInLocationsAvailable = com_getListObjectByTagName(By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]"), "a");
                        }
                        //--
                    }

                    else if (driver.Url.Contains("jobs.autonation.com") || driver.Url.Contains("jobs.libertymutualgroup.com"))
                    {
                        // Thread.Sleep(3000);
                        LinkedInLocationsAvailable = com_getListObjectByTagName(By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]"), "a");
                        //Rajesh
                        if (LinkedInLocationsAvailable == null)
                        {
                            com_ClearElement(TalentBrewUI.txt_AddLocation);
                            com_SendKeys(TalentBrewUI.txt_AddLocation, "Oo", "Job Matching || The Location to be searched was entered-" + LocationTosearch, "Job Matching || The Location to be searched was entered-" + LocationTosearch);
                            WaitForObject(By.XPath("//*[contains(@class,'mindreader-results-open') or contains(@id,'job-matching-add-location-mindreader')]//a"), 10);
                            LinkedInLocationsAvailable = com_getListObjectByTagName(By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]"), "a");
                        }
                        //--
                    }


                    else
                    {
                        LinkedInLocationsAvailable = com_getListObjectByTagName(TalentBrewUI.autopopulate_Location, "a");

                        //Rajesh--
                        if (LinkedInLocationsAvailable == null || LinkedInLocationsAvailable.Count == 0)
                        {
                            LinkedInLocationsAvailable = null;
                            LinkedInLocationsAvailable = com_getListObjectByTagName(TalentBrewUI.autopopulate_Location2, "a");
                        }

                        if (LinkedInLocationsAvailable == null)
                        {
                            com_ClearElement(TalentBrewUI.txt_AddLocation);
                            com_SendKeys(TalentBrewUI.txt_AddLocation, "B", "Job Matching || The Location to be searched was entered-" + LocationTosearch, "Job Matching || The Location to be searched was entered-" + LocationTosearch);
                            WaitForObject(By.XPath("//*[contains(@class,'mindreader-results-open') or contains(@id,'job-matching-add-location-mindreader')]//a"), 10);

                            LinkedInLocationsAvailable = com_getListObjectByTagName(TalentBrewUI.autopopulate_Location, "a");


                            if (LinkedInLocationsAvailable == null || LinkedInLocationsAvailable.Count == 0)
                            {
                                LinkedInLocationsAvailable = com_getListObjectByTagName(By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]"), "a");
                            }

                            LinkedInLocationsAvailable = com_getListObjectByTagName(By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]"), "a");
                        }
                        //-----
                    }
                    //------
                    string AutoLocationText = string.Empty;

                    if (LinkedInLocationsAvailable != null)
                    {
                        if (LinkedInLocationsAvailable[0].Displayed)
                        {
                            AutoLocationText = LinkedInLocationsAvailable[0].Text;
                            com_ClickListElement(LinkedInLocationsAvailable[0]);
                            //Thread.Sleep(1000);
                            try
                            {
                                for (int i = 1; i < 30; i++)
                                {
                                    if (String.IsNullOrEmpty(com_getobjProperty(By.ClassName("location-add"), "disabled")))
                                        break;
                                }
                            }
                            catch (Exception e) { }


                            report.AddReportStep("Job Matching || Location" + AutoLocationText + " selected from Auto complete box", "Job Matching || Location" + AutoLocationText + " selected from autocomplete box", StepResult.PASS);
                            if (com_IsElementPresent(TalentBrewUI.btn_AddLocation))
                            {
                                //Rajesh--
                                // if (driver.Url.Contains("baystatehealthjobs.com"))
                                if (driver.Url.Contains("baystatehealthjobs.com") || driver.Url.Contains("nespressojobs.com") || driver.Url.Contains("job-search.astrazeneca.se") || driver.Url.Contains("jobs.autonation.com") || driver.Url.Contains("jobs.libertymutualgroup.com"))
                                {
                                    com_ClickOnInvisibleElement(By.XPath("//button[contains(@class,'location-add')]"), "Job Matching || Location filter - Add button clicked", "Job Matching || Location filter - Add button not clickable");
                                }
                                else
                                {
                                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AddLocation, "Job Matching || Location filter - Add button clicked", "Job Matching || Location filter - Add button not clickable");
                                }
                                //----
                            }
                            else
                            {
                                report.AddReportStep("Job Matching || Location filter-Problem in clicking the Add button", "Job Matching || Location filter-Problem in clicking the Add button", StepResult.FAIL);
                            }
                        }
                        else
                        {
                            report.AddReportStep("Job Matching || Auto complete box was not rendered", "Job Matching || Auto complete box was not rendered ", StepResult.FAIL);
                        }

                    }
                    else
                    {
                        report.AddReportStep("Job Matching || Auto complete box was not rendered & Location Add button could not be clicked", "Job Matching || Auto complete box was not rendered & Location Add button could not be clicked", StepResult.FAIL);
                    }


                    IList<IWebElement> LinkedInLocationCheckBoxList = com_getListObjectByTagName(TalentBrewUI.LocationFilterCheckBox, "input");
                    IList<IWebElement> LinkedInLocationFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.LocationFilterCheckBox, "label");
                    app_clickCheckBoxes(LinkedInLocationCheckBoxList, LinkedInLocationFilterCheckBoxLabel, "Location", "Job Matching");
                    Thread.Sleep(2000);


                    try
                    {
                        //Rajesh--
                        if (driver.Url.Contains("primark"))
                            Thread.Sleep(2000);
                        //------
                        IList<IWebElement> DelLocations = com_getListObjectByTagName(TalentBrewUI.LocationFilterCheckBox, "button");
                        if (DelLocations.Count >= 1)
                        {

                            string deletedLocation = LinkedInLocationFilterCheckBoxLabel[DelLocations.Count].Text;
                            com_ClickListElement(DelLocations[DelLocations.Count - 1]);
                            //Rajesh--
                            if (driver.Url.Contains("primark"))
                                Thread.Sleep(2000);
                            //------
                            report.AddReportStep("Job Matching || Deleted " + deletedLocation + " From Location List", "Job Matching || Deleted " + deletedLocation + " From Location List", StepResult.PASS);
                        }
                        else
                        {
                            report.AddReportStep("Job Matching || Newly added locations are removed as remove button is only present and not the check box", "Job Matching || Newly added locations are removed as remove button is only present and not the check box", StepResult.FAIL);
                        }
                    }

                    catch (Exception e)
                    {
                        report.AddReportStep("Job Matching ||  Problem occured while trying to remove the Location", "Job Matching ||  Problem occured while trying to remove the Location", StepResult.FAIL);
                    }
                }
                else
                {
                    report.AddReportStep("Job Matching || Location filter and Filter text box unavailable", "Job Matching || Location filter and Filter text box unavailable", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                if (com_IsElementPresent(TalentBrewUI.Error_Retriving))
                {
                    report.AddReportStep("Job Matching || Error occured while retrieving results ", "Job Matching || Error occured while retrieving results", StepResult.FAIL);
                }
                else
                    report.AddReportStep("Job Matching ||  Unable to Add locations for candidate profile", "Job Matching || Unable to Add locations to candidate profile", StepResult.FAIL);
            }
        }


        public bool com_New_WaitForObject(By Obj, string ObjectName, long iterator)
        {
            bool flag = false;
            for (int i = 0; i < iterator; i++)
            {

                if (waitObj(Obj))
                {
                    flag = true;

                    break;
                }
            }

            if (!flag)
                report.AddReportStep("Verify the element '" + ObjectName + "'is present", "Element '" + ObjectName + "' is not present after " + iterator + " seconds", StepResult.FAIL);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            return flag;
        }

        public void app_JobMatchingSkillFilter()
        {
            //string skillFilterText=null;
            string classAttr = string.Empty;
            try
            {
                if (driver.Url.Contains("parexel.com") || driver.Url.Contains("karriere.disneycareers.com"))
                {
                    if (com_IsElementPresent(TalentBrewUI.link_Skills))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.link_Skills, "class");
                        app_SkillFilter(classAttr, TalentBrewUI.link_Skills);
                    }
                }

                else if (driver.Url.Contains("coxenterprises.com"))
                {
                    if (com_IsElementPresent(TalentBrewUI.link_Skills1))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.link_Skills1, "class");
                        app_SkillFilter(classAttr, TalentBrewUI.link_Skills1);
                    }
                }

                else if (com_IsElementPresent(TalentBrewUI.link_Skills4))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_Skills4, "class");
                    app_SkillFilter(classAttr, TalentBrewUI.link_Skills4);
                }

                else if (com_IsElementPresent(TalentBrewUI.link_SkillsFilter))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_SkillsFilter, "class");
                    app_SkillFilter(classAttr, TalentBrewUI.link_SkillsFilter);
                }

                else if (com_IsElementPresent(TalentBrewUI.link_EndorsedSkillsFilter))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_EndorsedSkillsFilter, "class");
                    app_SkillFilter(classAttr, TalentBrewUI.link_EndorsedSkillsFilter);
                }
                else if (com_IsElementPresent(TalentBrewUI.link_Skills2))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_Skills2, "class");
                    app_SkillFilter(classAttr, TalentBrewUI.link_Skills2);
                }

                //Rajesh--
                else if (com_IsElementPresent(TalentBrewUI.link_Skills3))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_Skills3, "class");
                    app_SkillFilter(classAttr, TalentBrewUI.link_Skills3);
                }

                else if (com_IsElementPresent(TalentBrewUI.SkillFilterCheckBox))
                {
                    classAttr = "child-open";
                    app_SkillFilter(classAttr, TalentBrewUI.SkillFilterCheckBox);
                }

                //---
                else
                {
                    report.AddReportStep("Job Matching || Skill filter section is not available", "Job Matching || skill filter section is not available", StepResult.FAIL);
                }
            }
            catch (Exception e)
            {
                if (com_IsElementPresent(TalentBrewUI.Error_Retriving))
                {
                    report.AddReportStep("Job Matching || Error occured while retrieving results ", "Job Matching || Error occured while retrieving results", StepResult.FAIL);
                }
                else
                {
                    report.AddReportStep("Job Matching || Cannot process Job Matching skill Filter", "Job Matching || Cannot process Job Matching skill Filter", StepResult.FAIL);
                }
            }

        }


        private void app_SkillFilter(string classAttr, By Objskill)
        {
            if (!classAttr.Contains("child-open") || driver.Url.Contains("jobs.ocwen.com"))
            //  if ((!classAttr.Contains("child-open") && !classAttr.Contains("expandable")) || driver.Url.Contains("jobs.ocwen.com"))
            {
                //Rajesh--
                if (driver.Url.Contains("emplois.bnc.ca") || driver.Url.Contains("careers-us.primark.com") || driver.Url.Contains("careers.lpl.com"))
                    com_ClickOnInvisibleElement(By.XPath("//form[@id='job-matching-filters']/fieldset[2]/legend/a"), "Job Matching || Skill filter toggle was clicked", "Job Matching || Skill filter toggle not clickable");
                else
                    com_ClickOnInvisibleElement(Objskill, "Job Matching || Skill filter toggle is clicked", "Job Matching || Skill filter toggle is not Clickable");

                IList<IWebElement> LinkedInSkillsCheckBoxList = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "input");

                //Rajesh
                if (LinkedInSkillsCheckBoxList == null || LinkedInSkillsCheckBoxList.Count == 0)
                {
                    com_ClickOnInvisibleElement(By.XPath("//form[@id='job-matching-filters']/fieldset[2]/legend/a"), "Job Matching || Skill filter toggle was clicked", "Job Matching || Skill filter toggle not clickable");
                    LinkedInSkillsCheckBoxList = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "input");
                }
                //--
                IList<IWebElement> LinkedInSkillFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "label");
                if (LinkedInSkillsCheckBoxList == null && com_IsElementPresent(Objskill))
                {
                    WaitForObject(Objskill, 30);
                    Thread.Sleep(2000);
                    driver.FindElement(Objskill).Click();
                    //Rajesh--
                    WaitForObject(TalentBrewUI.SkillFilterCheckBox, 30);
                    //--------
                    LinkedInSkillsCheckBoxList = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "input");
                    LinkedInSkillFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "label");
                    Thread.Sleep(2000);
                    report.AddReportStep("Job Matching || Skill filter section is expanded by default", "Job Matching || skill filter section is expanded by default", StepResult.PASS);

                }
                app_clickCheckBoxes(LinkedInSkillsCheckBoxList, LinkedInSkillFilterCheckBoxLabel, "Skill", "Job Matching");
            }
            else
            {
                report.AddReportStep("Job Matching || Skill filter section is expanded by default", "Job Matching || skill filter section is expanded by default", StepResult.PASS);
                IList<IWebElement> LinkedInSkillsCheckBoxList = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "input");
                IList<IWebElement> LinkedInSkillFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.SkillFilterCheckBox, "label");
                app_clickCheckBoxes(LinkedInSkillsCheckBoxList, LinkedInSkillFilterCheckBoxLabel, "Skill", "Job Matching");
            }
        }

        public void app_JobMatchingExperienceFilter()
        {
            string classAttr = string.Empty;
            try
            {
                if (driver.Url.Contains("jobs.parexel.com") || driver.Url.Contains("berufe.criver.com"))
                {
                    if (com_IsElementPresent(TalentBrewUI.link_ExperienceFilter2))
                    {
                        classAttr = com_getobjProperty(TalentBrewUI.link_ExperienceFilter2, "class");
                        Experience_Filter(classAttr, TalentBrewUI.link_ExperienceFilter2);
                    }
                    else
                    {
                        report.AddReportStep("Job Matching || Experience filter section not available", "Job Matching || Experience filter section not available", StepResult.FAIL);
                    }
                }

                else if (com_IsElementPresent(TalentBrewUI.link_ExperienceFilter4))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_ExperienceFilter4, "class");
                    Experience_Filter(classAttr, TalentBrewUI.link_ExperienceFilter4);
                }


                else if (com_IsElementPresent(TalentBrewUI.link_ExperienceFilter))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_ExperienceFilter, "class");
                    Experience_Filter(classAttr, TalentBrewUI.link_ExperienceFilter);
                }
                else if (com_IsElementPresent(TalentBrewUI.link_ExperienceFilter2))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_ExperienceFilter2, "class");
                    Experience_Filter(classAttr, TalentBrewUI.link_ExperienceFilter2);
                }
                else if (com_IsElementPresent(TalentBrewUI.link_PositonFilter))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_PositonFilter, "class");
                    Experience_Filter(classAttr, TalentBrewUI.link_PositonFilter);
                }
                else if (com_IsElementPresent(TalentBrewUI.link_ExperienceFilter1))
                {
                    classAttr = com_getobjProperty(TalentBrewUI.link_ExperienceFilter1, "class");
                    Experience_Filter(classAttr, TalentBrewUI.link_ExperienceFilter1);
                }

                //Rajesh---
                else if (com_IsElementPresent(TalentBrewUI.ExperienceFilterCheckBox))
                {
                    classAttr = "child-open";
                    Experience_Filter(classAttr, TalentBrewUI.ExperienceFilterCheckBox);
                }
                //-----

                // Click the Experience check boxes
                //IList<IWebElement> LinkedInExperienceCheckBoxList = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "input");
                //IList<IWebElement> LinkedInExperienceFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "label");

                //app_clickCheckBoxes(LinkedInExperienceCheckBoxList, LinkedInExperienceFilterCheckBoxLabel, "Experience", "Job Matching");
                else
                {
                    report.AddReportStep("Job Matching || Experience filter section not available", "Job Matching || Experience filter section not available", StepResult.FAIL);
                }

            }
            catch (Exception e)
            {
                if (com_IsElementPresent(TalentBrewUI.Error_Retriving))
                {
                    report.AddReportStep("Job Matching || Error occured while retrieving results ", "Job Matching || Error occured while retrieving results", StepResult.FAIL);
                }
                else
                {

                    report.AddReportStep("Job Matching || Cannot process Job Matching Experience Filter", "Job Matching || Cannot process Job Matching Experience Filter", StepResult.FAIL);
                }
            }
        }

        private void Experience_Filter(string classAttr, By objExpFilter)
        {
            if (!classAttr.Contains("child-open"))
            {
                report.AddReportStep("Job Matching || Experience filter section is not expanded by default", "Job Matching || Experience filter section is not expanded by default", StepResult.WARNING);
                if (com_IsElementPresent(objExpFilter))
                {
                    //Rajesh
                    if (driver.Url.Contains("emplois.bnc.ca") || driver.Url.Contains("careers.lpl.com"))
                        com_ClickOnInvisibleElement(By.XPath("//form[@id='job-matching-filters']/fieldset[3]/legend/a"), "Job Matching || Experience filter toggle was clicked", "Job Matching || Experience filter toggle not clickable");
                    else
                        com_ClickOnInvisibleElement(objExpFilter, "Job Matching || Experience filter toggle was clicked", "Job Matching || Experience filter toggle not clickable");
                    //----


                    Thread.Sleep(2000);
                }
                IList<IWebElement> LinkedInExperienceCheckBoxList = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "input");

                //Rajesh--
                if (LinkedInExperienceCheckBoxList == null || LinkedInExperienceCheckBoxList.Count == 0)
                {
                    com_ClickOnInvisibleElement(By.XPath("//form[@id='job-matching-filters']/fieldset[3]/legend/a"), "Job Matching || Experience filter toggle was clicked", "Job Matching || Experience filter toggle not clickable");
                    LinkedInExperienceCheckBoxList = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "input");
                }
                //---
                IList<IWebElement> LinkedInExperienceFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "label");
                //  Console.Write(LinkedInExperienceCheckBoxList.Count);
                try
                {
                    if (LinkedInExperienceCheckBoxList == null && com_IsElementPresent(objExpFilter))
                    {
                        WaitForObject(objExpFilter, 30);
                        Thread.Sleep(2000);
                        driver.FindElement(objExpFilter).Click();
                        LinkedInExperienceCheckBoxList = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "input");
                        LinkedInExperienceFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "label");
                        Thread.Sleep(2000);

                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.StackTrace);
                }
                app_clickCheckBoxes(LinkedInExperienceCheckBoxList, LinkedInExperienceFilterCheckBoxLabel, "Experience", "Job Matching");
            }
            else
            {
                report.AddReportStep("Job Matching || Experience filter section is expanded by default", "Job Matching || Experience filter section is expanded by default", StepResult.PASS);
                IList<IWebElement> LinkedInExperienceCheckBoxList = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "input");
                IList<IWebElement> LinkedInExperienceFilterCheckBoxLabel = com_getListObjectByTagName(TalentBrewUI.ExperienceFilterCheckBox, "label");
                app_clickCheckBoxes(LinkedInExperienceCheckBoxList, LinkedInExperienceFilterCheckBoxLabel, "Experience", "Job Matching");
            }
        }

        public void app_JobMatchingLogout()
        {
            try
            {
                if (com_IsElementPresent(TalentBrewUI.JobMatchingLogoutLink))
                {
                    com_ClickOnInvisibleElement(TalentBrewUI.JobMatchingLogoutLink, "Job Matching || Log out is clicked", "Job Matching || Log out is not clickable");
                    if (com_IsElementPresent(TalentBrewUI.HomePage) || com_IsElementPresent(TalentBrewUI.Homepage2) || com_IsElementPresent(TalentBrewUI.Homepage1) || (driver.Url.ToString().Contains("/search-site")))
                    {
                        report.AddReportStep("Job Matching || User successfully logged out of Job Matching", "Job Matching || User successfully logged out of Job Matching", StepResult.PASS);
                    }
                    else if (com_IsElementPresent(By.XPath("//button[@id='oauth__auth-form__submit-btn']")))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@id='oauth__auth-form__submit-btn']"), "Clicked-Allow button of authorization page", "not Clicked-Allow button of authorization page");
                        report.AddReportStep("Job Matching || User successfully logged out of Job Matching", "Job Matching || User successfully logged out of Job Matching", StepResult.PASS);
                        //com_New_WaitForObject(By.XPath("//section[@data-selector-name='jobmatchinglogout']"), "job Matching Logout section", 120);
                    }
                    else if (com_IsElementPresent(TalentBrewUI.Error_Retriving))
                    {
                        report.AddReportStep("Job Matching || Error occured while logging out ", "Job Matching || Error occured while logging out", StepResult.FAIL);
                    }
                    else if (com_IsElementPresent(TalentBrewUI.Gateway_Timeout))
                    {
                        report.AddReportStep("Job Matching || candidate cannot log out - due to 504 Gateway Timeout", "Job Matching || candidate cannot log out - due to 504 Gateway Timeout", StepResult.FAIL);
                    }
                    else
                    {
                        report.AddReportStep("Job Matching || candidate cannot log out - As log Out button is not available", "Job Matching || candidate cannot log out - As log Out button is not available", StepResult.FAIL);
                    }
                }
                //else
                //{
                //    report.AddReportStep("Job Matching || candidate cannot log out - As log Out button is not available", "Job Matching || candidate cannot log out - As log Out button is not available", StepResult.FAIL);
                //}
                //else if (com_IsElementPresent(TalentBrewUI.JobMatchingLogoutLink2))
                //{
                //com_ClickOnInvisibleElement(TalentBrewUI.JobMatchingLogoutLink2, "Job Matching || Log out is clicked", "Job Matching || Log out is not clickable");
                //    if (com_IsElementPresent(TalentBrewUI.HomePage))
                //    {
                //        report.AddReportStep("Job Matching || User successfully logged out of Job Matching", "Job Matching || User successfully logged out of Job Matching", StepResult.PASS);
                //    }
                //    else
                //    {
                //        report.AddReportStep("Job Matching || candidate cannot log out", "Job Matching || candidate cannot log out", StepResult.FAIL);
                //    }
                //}
            }
            catch (Exception e)
            {
                if (com_IsElementPresent(TalentBrewUI.Error_Retriving))
                {
                    report.AddReportStep("Job Matching || Error occured while logging out ", "Job Matching || Error occured while logging out", StepResult.FAIL);
                }

                else

                    report.AddReportStep("Job Matching || candidate cannot log out", "Job Matching || candidate cannot log out", StepResult.FAIL);

            }

        }

        private void app_clickCheckBoxes(IList<IWebElement> checkBoxList, IList<IWebElement> checkBoxLabel, string section, string Module)
        {
            try
            {

                if (checkBoxList != null && checkBoxList.Count == checkBoxLabel.Count)
                {
                    for (int i = 0; i < checkBoxList.Count; i++)
                    {
                        com_ClickListElement(checkBoxList[i]);

                        report.AddReportStep(Module + " || " + checkBoxLabel[i].Text + " " + section + " Check box is checked/Unchecked", Module + " || " + checkBoxLabel[i].Text + " " + section + " checkbox is checked/unchecked", StepResult.PASS);
                    }
                }
                else
                {
                    report.AddReportStep(Module + " || " + section + " Check box are not checked/Unchecked", Module + " || " + section + " Check box are not checked/Unchecked", StepResult.FAIL);
                }
            }

            catch (Exception e)
            {
                report.AddReportStep(Module + " || " + section + " -checkboxes are missing for newly added locations", Module + " || " + section + " -checkboxes are missing for newly added locations", StepResult.FAIL);

            }
        }

        public bool app_verifyGoogleAnalyticsCode(string value1, string a1, string b1)
        {
            bool isGAelementAvailable = false;
            string pageSource = driver.PageSource;
            if (pageSource.Contains("www.google-analytics.com/analytics.js"))
            {



                //if (com_IsElementPresent(TalentBrewUI.link_googleAnalytics))
                //{
                //    isGAelementAvailable = driver.PageSource.Contains("UA-");
                //}
                //else
                //{
                //    report.AddReportStep("Google Analytics || Google Analytics code is not available", "Google Analytics || Google Analytics code is not available - As Google Analytics javascript is not available", StepResult.FAIL);

                //}

                string a = app_GACodeBetween(value1, a1, b1);

                isGAelementAvailable = true;

            }



            return isGAelementAvailable;

        }

        public string app_GACodeBetween(string value, string a, string b)
        {
            string gaCode;
            string pageSource = driver.PageSource;
            if (pageSource.Contains("www.google-analytics.com/analytics.js"))
            {
                //if (com_VerifyObjPresent((TalentBrewUI.link_googleAnalytics), "Google Analytics || Google Analytics code is available", "Google Analytics || Google Analytics code is not available - As Google Analytics javascript is not available"))
                int posA = value.IndexOf(a);
                int posB = value.LastIndexOf(b);
                if (posA == -1)
                {
                    return "";
                }
                if (posB == -1)
                {
                    return "";
                }
                int adjustedPosA = posA + a.Length;
                if (adjustedPosA >= posB)
                {
                    return "";
                }
                gaCode = value.Substring(adjustedPosA, posB - adjustedPosA);
                //return value.Substring(adjustedPosA, posB - adjustedPosA);
                return gaCode;
                //report.AddReportStep("Under Google Analytics:'" + gaCode + "' || items is displayed.", "Under Google Analytics: ' " + gaCode + "' items is displayed.", StepResult.PASS);
                // report.AddReportStep("Google Analytics || Google Analytics code is not available", "Google Analytics || Google Analytics code is not available - As Google Analytics javascript is not available", StepResult.FAIL);

            }

            else
            {

                return "";
                //report.AddReportStep("Google Analytics || Google Analytics code is available", "Google Analytics || Google Analytics code is not available - As Google Analytics javascript is not available" ,StepResult.FAIL); 
            }
        }

        public void app_VerifyJobsByCategory(string clientUrl, string category, string SearchKeyword)
        {
            if (category.Contains("L1"))
            {
                com_NewLaunchUrl(clientUrl);
            }
            if (driver.Url.Contains("careers.bbcworldwide.com"))
            {
                app_navigateL2(SearchKeyword, clientUrl);
                com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on expand button to display advance search ", "Problem in clicking on button to expand advance search section");
                Thread.Sleep(2000);
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCat1))
                {
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCat1, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L2");
                }
            }
            else if (driver.Url.Contains("jobs.pattersoncompanies.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory15))
                {
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory15, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
                }
            }

            else if (driver.Url.Contains("laureate.net") || driver.Url.Contains("aarons.com") || driver.Url.Contains("jobs.shophomesmart.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory2))
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory2, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }

            else if (driver.Url.Contains("jobs.atos.net"))
            {
                Thread.Sleep(3000);
                // WaitForObject(TalentBrewUI.btn_Explore, 100);
                //com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                //WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                //com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                //Thread.Sleep(2000);
                //if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory6))

                app_SearchjobBy(clientUrl, category, By.XPath("//*[text()='Search by Category']"), "Jobs By Category", By.XPath("//*[text()='Search by Category']/..//ul"), "L1");
            }
            else if (driver.Url.Contains("empleos.oldnavy.com"))
            {
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory6, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }
            else if (driver.Url.Contains("three.co.uk"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory5))
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory5, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }

            else if (driver.Url.Contains("primark.com") || driver.Url.Contains("bmwgroupretailcareers.co.uk"))
            {
                //com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory6, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }
            else if (driver.Url.Contains("careers.jobsataramco.eu"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory3))
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory3, "Jobs By Category", TalentBrewUI.jobsByCategoryLink1, "L1");
            }

            else if (driver.Url.Contains("jobs.adt.com") || driver.Url.Contains("www.takedajobs.com") || driver.Url.Contains("carolinaeasthealth.com") || driver.Url.Contains("progleasing.com") || driver.Url.Contains("jobs.solvay.com") || driver.Url.Contains("emplois.criver.com") || driver.Url.Contains("emploisfr.criver.com") || driver.Url.Contains("berufe.criver.com") || driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("jobs.newellbrands.com") || driver.Url.Contains("empleos.scotiabank.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory3))
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory3, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }

            else if (driver.Url.Contains("job-search.astrazeneca.fr"))
            {
                Thread.Sleep(3000);
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory9))
                {
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory9, "Jobs By Category", TalentBrewUI.jobsByCategoryLink3, "L1");
                }
            }

            else if (driver.Url.Contains("jobs.bidmc.org"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory3))
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory3, "Jobs By Category", TalentBrewUI.jobsByCategoryLink2, "L1");
            }

            else if (driver.Url.Contains("jobs.parexel.com"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory4, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            //else if (driver.Url.Contains("tdameritrade.com"))
            //    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory12, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("carrieres.pj.ca"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory8, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("careers.duffandphelps.jobs") || driver.Url.Contains("www.wellsfargojobs.com"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory9, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("recrutement.bpce.fr"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory10, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("www.baystatehealthjobs.com"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory11, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("jobs.pagny.org"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory13, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("emploi.adt.ca"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory14, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("jobs.communitymedical.org"))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory16, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");

            else if (driver.Url.Contains("emplois.banquescotia.com") || driver.Url.Contains("wfbase.searchgreatcareers.com") || driver.Url.Contains("wfalpha.searchgreatcareers.com") || driver.Url.Contains("jobs.tdameritrade.com") || driver.Url.Contains("jobs.experian.com") || driver.Url.Contains("www.kpcareers.org"))
            {
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory18, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }

            else if (driver.Url.Contains("www.sneakerjobs.com"))
            {
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory1, "Jobs By Category", TalentBrewUI.jobsByCategoryLink5, "L1");
            }
            else if (driver.Url.Contains("jobs.monsanto.com"))
            {
                app_SearchjobBy(clientUrl, category, By.XPath("//*[contains(text(),'Career Areas')]"), "Jobs By Category", By.XPath("//*[contains(text(),'Career Areas')]/../..//ul"), "L1");
            }
            else if (driver.Url.Contains("jobs.marksandspencer.com"))
            {
                app_SearchjobBy(clientUrl, category, By.XPath("//*[text()='Or browse by category']"), "Jobs By Category", By.XPath("//*[text()='Or browse by category']/..//ul"), "L1");
            }

            else if (driver.Url.Contains("petsmart"))
            {
                app_SearchjobBy(clientUrl, category, By.LinkText("Jobs By Category"), "Jobs By Category", By.XPath("//div[contains(@class,'job-category')]"), "L1");
            }
            else if (driver.Url.Contains("job-search.astrazeneca.se"))
            {
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory18, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            }
            else if (clientUrl.Contains("chinajobs.disneycareers.cn"))
            {
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByCategoryLinkAchor, "Jobs by category", TalentBrewUI.jobsByCategoryLink, "L1");
            }
            else if (driver.Url.Contains("disneycareers.cn"))
            {
                app_SearchjobBy(clientUrl, category, By.XPath("//div[@class='job-category']"), "Jobs By Category", By.XPath("//div[@class='job-category']//div//ul"), "L1");

            }
            else if (driver.Url.Contains("jobs.advocatehealth.com"))
            {
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory19, "Jobs By Category", By.XPath("//li[contains(@class,'dropdown open')]//ul"), "L1");
            }
            else if (driver.Url.Contains("jobs.sky.com"))
            {
                app_SearchjobBy(clientUrl, category, By.XPath("//h2[text()='Browse by category']"), "Jobs By Category", By.XPath("(//ul[@class='category-links'])[1]"), "L1");
            }
            else if (driver.Url.Contains("www.nespressojobs.com"))
            {
                if (com_IsElementPresent(By.XPath("//button[@class='btn-advanced-search']")))
                {
                    com_ClickOnInvisibleElement(By.XPath("//button[@class='btn-advanced-search']"), "clicked on Plus icon", "Not Clicked on Plus icon");
                    app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByCategory, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
                }
            }
            else if (driver.Url.Contains("www.aetnacareers.com"))
            {
                app_SearchjobBy(clientUrl, category, By.XPath("//a[text()='Jobs by Category']"), "Jobs By Category", By.XPath("//div[@data-selector-name='jobcategory']//ul"), "L1");
            }



            else if (com_IsElementPresent(TalentBrewUI.searchJobsByCategory))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByCategory, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByCategoryLinkAchor))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByCategoryLinkAchor, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory1))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory1, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkCategory))
                app_SearchjobBy(clientUrl, category, TalentBrewUI.searchJobsByLinkCategory, "Jobs By Category", TalentBrewUI.jobsByCategoryLink, "L1");
            else
                report.AddReportStep("Jobs By Category|| Jobs By Category is not available for this client", "Jobs By Category|| Jobs By Category is not available for this client", StepResult.FAIL);

        }

        public void app_JobsByCategory(string clientUrl, string executionStatus, string ScenarioName)
        {
            string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
            if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
            {
                report.AddScenarioHeader("Jobs By Category");
                if (ScenarioName.Equals("L1"))
                {
                    app_VerifyJobsByCategory(clientUrl, executionStatus, searchKeyword);
                }

                else if (ScenarioName.Equals("L2"))
                {
                    if (app_navigateL2(searchKeyword, clientUrl))
                    {
                        app_VerifyJobsByCategory(clientUrl, executionStatus, searchKeyword);
                    }
                }

                else if (ScenarioName.Equals("L3"))
                {
                    if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                    {
                        app_VerifyJobsByCategory(clientUrl, executionStatus, searchKeyword);
                    }
                }
            }
        }

        public void app_JobsByLocation(string clientUrl, string executionStatus, string ScenarioName)
        {
            string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
            if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
            {
                report.AddScenarioHeader("Jobs By Location");
                if (ScenarioName.Equals("L1"))
                {
                    app_VerifyJobsByLocation(clientUrl, executionStatus, searchKeyword);
                }
                else if (ScenarioName.Equals("L2"))
                {
                    if (app_navigateL2(searchKeyword, clientUrl))
                    {
                        app_VerifyJobsByLocation(clientUrl, executionStatus, searchKeyword);
                    }
                }
                else if (ScenarioName.Equals("L3"))
                {
                    if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                    {
                        app_VerifyJobsByLocation(clientUrl, executionStatus, searchKeyword);
                    }
                }
            }
        }

        public void app_VerifyJobsByLocation(string clientUrl, string Location, string SearchKeyword)
        {
            if (driver.Url.Contains("premisehealthjobs") || driver.Url.Contains("pattersoncompanies.com") || driver.Url.Contains("premerajobs.com") || driver.Url.Contains("jp.takedajobs.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation1))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation1, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }

            else if (driver.Url.Contains("jobs.pagny.org"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation9))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation9, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }


            else if (driver.Url.Contains("jobs.shophomesmart.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation2))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation2, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("jobs.adt.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation3))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation3, "Jobs By Location", TalentBrewUI.jobsByLocationLink1, "L1");
            }
            else if (driver.Url.Contains("www.takedajobs.com") || driver.Url.Contains("carolinaeasthealth.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation3))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation3, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("progleasing.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation3))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation3, "Jobs By Location", TalentBrewUI.jobsByLocationLink2, "L1");
            }
            else if (driver.Url.Contains("jobs.parexel.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation4))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation4, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("three.co.uk"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation3))
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation3, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }

            //else if (driver.Url.Contains("careers.primark.com"))
            //{
            //    com_ClickOnInvisibleElement(TalentBrewUI.btn_BrowseJobs, "Clicked on 'Browse Jobs' button", "Problem in clicking the 'Browse Jobs' button");
            //}

            else if (driver.Url.Contains("jobs.atos.net"))
            {
                Thread.Sleep(3000);
                //  WaitForObject(TalentBrewUI.btn_Explore, 100);
                //  com_ClickOnInvisibleElement(TalentBrewUI.btn_Explore, "Clicked on Explore button", "Problem in clicking on Explore button");
                // WaitForObject(TalentBrewUI.btn_BrowseJobs1, 100);
                //// com_(TalentBrewUI.btn_BrowseJobs1, "Clicked on 'Browse Jobs' button", "Problem in clicking on 'Browse Jobs' button");
                // driver.FindElement(TalentBrewUI.btn_BrowseJobs1).Click();

                // Thread.Sleep(2000);
                // if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation1))
                app_SearchjobBy(clientUrl, Location, By.XPath("//*[text()='Search by Location']"), "Jobs By Location", By.XPath("//*[text()='Search by Location']/..//ul"), "L1");
            }

            else if (driver.Url.Contains("careers.enterprise.com"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink3, "L1");

            else if (driver.Url.Contains("newellbrands.com"))
                app_SearchjobBy(clientUrl, Location, By.XPath("//div/h2[contains(text(),'Search Jobs By Location') or contains(text(),'Search By Location')]"), "Jobs By Location", By.XPath("//*[contains(@class,'job-category dropdown') or contains(@class,'job-location')]| //*[@id='category-list-selector']/option"), "L1");

            else if (driver.Url.Contains("careers.duffandphelps.jobs") || driver.Url.Contains("carrieres.pj.ca") || driver.Url.Contains("internalcareers.bbcworldwide.com") || driver.Url.Contains("jobs.interiorhealth.ca"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation6, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");

            else if (driver.Url.Contains("recrutement.bpce.fr"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation7, "Jobs By Location", TalentBrewUI.jobsByLocationLink5, "L1");

            else if (driver.Url.Contains("www.baystatehealthjobs.com"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation8, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");

            else if (driver.Url.Contains("emploi.adt.ca"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation10, "Jobs By Location", TalentBrewUI.jobsByLocationLink1, "L1");

            else if (driver.Url.Contains("internalcareers.bbcworldwide.com"))
            {
                com_ClickOnInvisibleElement(TalentBrewUI.advanceSearch_Button, "Clicked on button to expand advance search", "Unable to click on the button to expand advance search");
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation11, "Jobs By Location", TalentBrewUI.jobsByLocationLink1, "L1");
            }

            else if (driver.Url.Contains("jobs.experian.com") || driver.Url.Contains("emplois.banquescotia.com"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation12, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }

            else if (driver.Url.Contains("www.kpcareers.org"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation12, "Jobs By Location", TalentBrewUI.jobsByLocationLink7, "L1");
            }

            else if (driver.Url.Contains("careers.enterprise.ca"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink6, "L1");


            else if (driver.Url.Contains("job-search.astrazeneca.fr"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation13, "Jobs By Location", TalentBrewUI.jobsByLocationLink7, "L1");
            else if (driver.Url.Contains("primark.com"))
                app_SearchjobBy(clientUrl, Location, By.XPath("//a[contains(@href, 'jobsbycountry')]"), "Jobs By Location", By.XPath("//div[@id='jobsbycountry']//ul"), "L1");

            else if (driver.Url.Contains("bmwgroupretailcareers.co.uk"))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");

            else if (driver.Url.Contains("jobs.solvay.com"))
            {
                com_ClickOnInvisibleElement(By.XPath("//div[@class='browse-by job-location']//h2"), "Clicked on the location drop down", "unable to click on the location drop down");
                Thread.Sleep(1000);
                com_ClickOnInvisibleElement(TalentBrewUI.Selectlocation, "selected Location from the dropdown", " unable to select the location from the drop down");
                app_SearchjobBy(clientUrl, Location, By.XPath("//div[@class='browse-by job-location']//h2"), "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");

            }
            else if (driver.Url.Contains("emplois.criver.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobslistByLocation2))
                {
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobslistByLocation2, "Jobs By Location", TalentBrewUI.JobsByLocationLink_NEW2, "L1");
                }

            }

            else if (driver.Url.Contains("sneakerjobs.com"))
            {

                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocation, "Jobs By Location", By.XPath("//*[@id='page']/header/nav/div/div/div[2]/ul/li/a"), "L1");

            }

            else if (driver.Url.Contains("emplois.enterprise.ca"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocation, "Jobs By Location", TalentBrewUI.JobsByLocationLink8, "L1");
            }

            else if (driver.Url.Contains("wfalpha.searchgreatcareers.com") || driver.Url.Contains("wfbase.searchgreatcareers.com"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation14, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("careers.lpl.com"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.SearchJobsLocationNEW, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("jobs.aarons.com"))
            {
                app_SearchjobBy(clientUrl, Location, By.XPath("//*[text()='Jobs By Location']"), "Jobs By Location", By.XPath("//*[text()='Jobs By Location']/../..//ul"), "L1");
            }
            else if (driver.Url.Contains("jobs.sky.com"))
            {
                app_SearchjobBy(clientUrl, Location, By.XPath("//*[text()='Browse by Location']"), "Jobs By Location", By.XPath("//*[text()='Browse by Location']/..//ul"), "L1");
            }
            else if (driver.Url.Contains("jobs.tenethealth.com"))
            {
                app_SearchjobBy(clientUrl, Location, By.XPath("//*[text()='Search Jobs by Location']"), "Jobs By Location", By.XPath("//*[text()='Search Jobs by Location']/..//ul"), "L1");
            }
            else if (driver.Url.Contains("jobs.bd.com"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobslistByLocation2, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("job-search.astrazeneca.com") || driver.Url.Contains("job-search.astrazeneca.se"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobslistByLocation2, "Jobs By Location", TalentBrewUI.jobsByLocationLink10, "L1");
            }
            else if (driver.Url.Contains("jobs.advocatehealth.com"))
            {
                app_SearchjobBy(clientUrl, Location, By.XPath("//a[@title='LOCATIONS']"), "Jobs By Location", By.XPath("//div[@class='container']//div[@class='row']//div[contains(@class, 'col-md')]"), "L1");
            }
            else if (driver.Url.Contains("www.wellsfargojobs.com"))
            {
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation6, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            }
            else if (driver.Url.Contains("www.nespressojobs.com"))
            {
                if (com_IsElementPresent(By.XPath("//button[@class='btn-advanced-search']")))
                {
                    com_ClickOnInvisibleElement(By.XPath("//button[@class='btn-advanced-search']"), "clicked on Plus icon", "Not Clicked on Plus icon");
                    app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink3, "L1");

                }
            }
            else if (driver.Url.Contains("jobs.screwfix.com"))
            {

                app_SearchjobBy(clientUrl, Location, By.XPath("//div[@class='job-location']"), "Jobs By Location", By.XPath("//div[@class='job-location']//ul"), "L1");


            }
            else if (driver.Url.Contains("www.aetnacareers.com"))
            {

                app_SearchjobBy(clientUrl, Location, By.XPath("//a[text()='Jobs by Location']"), "Jobs By Location", By.XPath("//div[@data-selector-name='joblocation']//ul"), "L1");


            }
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByLocation))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByLocationLinkAchor))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLocationLinkAchor, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkLocation))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobsByLinkLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobslistByLocation))
                app_SearchjobBy(clientUrl, Location, TalentBrewUI.searchJobslistByLocation, "Jobs By Location", TalentBrewUI.jobsByLocationLink_NEW, "L1");


            else
                report.AddReportStep("Jobs By Location|| Jobs By Location is not available for this client", "Jobs By Location|| Jobs By Location is not available for this client", StepResult.FAIL);
        }

        public void app_JobsByGroup(string clientUrl, string executionStatus, string ScenarioName)
        {
            string searchKeyword = dataHelper.GetData(DataColumn.SearchKeyword);
            if (executionStatus.Contains("Yes") && executionStatus.Contains(ScenarioName))
            {
                report.AddScenarioHeader("Jobs By Group");
                if (ScenarioName.Equals("L1"))
                {
                    app_VerifyJobsByGroup(clientUrl, executionStatus, searchKeyword);
                }
                else if (ScenarioName.Equals("L2"))
                {
                    if (app_navigateL2(searchKeyword, clientUrl))
                    {
                        app_VerifyJobsByGroup(clientUrl, executionStatus, searchKeyword);
                    }
                }
                else if (ScenarioName.Equals("L3"))
                {
                    if (app_navigateL3(searchKeyword, clientUrl, TalentBrewUI.txt_keywordSearch))
                    {
                        app_VerifyJobsByGroup(clientUrl, executionStatus, searchKeyword);
                    }
                }
            }
        }

        public void app_VerifyJobsByGroup(string clientUrl, string Group, string SearchKeyword)
        {
            if (driver.Url.Contains("aarons.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkGroup1))
                    app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByLinkGroup1, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            }

            else if (driver.Url.Contains("jobs.lvhn.org"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkGroup2))
                    app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByLinkGroup2, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            }
            else if (driver.Url.Contains("careers.lahey.org"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkGroup2))
                    app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByLinkGroup2, "Jobs By Group", TalentBrewUI.jobsByGroupLink1, "L1");
            }


            else if (driver.Url.Contains("jobs.experian.com") || driver.Url.Contains("jobs.scotiabank.com") || driver.Url.Contains("empleos.scotiabank.com") || driver.Url.Contains("emplois.banquescotia.com") || driver.Url.Contains("jobs.cunamutual.com") || driver.Url.Contains("jobs.advocatehealth.com") || driver.Url.Contains("jobs.tenethealth.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.searchJobsLinkGroup3))
                    app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsLinkGroup3, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            }

            else if (driver.Url.Contains("jobs.barnabashealthcareers.org") || driver.Url.Contains("progleasing.com") || driver.Url.Contains("emplois.criver.com") || driver.Url.Contains("emploisfr.criver.com") || driver.Url.Contains("berufe.criver.com"))
            {
                if (com_IsElementPresent(TalentBrewUI.secJobByGroup))
                    app_SearchjobBy(clientUrl, Group, TalentBrewUI.secJobByGroup, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            }
            else if (driver.Url.Contains("jobs.summithealthmanagement.com"))
            {
                app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByGroup, "Jobs By Group", By.XPath("//*[contains(@class,'job-keyword') or contains(@data-selector-name,'jobkeyword') or contains(@class,'job-groups') or contains(@class,'job-hierarchy') or contains(@id,'ui-id-6') or contains(@class,'col-1')]//ul"), "L1");
            }
            //else if (driver.Url.Contains("jobs.progleasing.com"))
            //{
            //    if (com_IsElementPresent(TalentBrewUI.SearchByGroupsLinkNew))
            //        app_SearchjobBy(clientUrl, Group, TalentBrewUI.SearchByGroupsLinkNew, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            //}
            else if (driver.Url.Contains("jobs.bd.com"))
            {
                app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsLinkGroup3, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            }
            else if (driver.Url.Contains("www.aetnacareers.com"))
            {
                app_SearchjobBy(clientUrl, Group, By.XPath("//a[text()='Jobs by Group']"), "Jobs By Group", By.XPath("//div[@data-selector-name='jobkeyword']//ul"), "L1");
            }
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByGroup))
                app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByGroup, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByGroupLinkAchor))
                app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByGroupLinkAchor, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            else if (com_IsElementPresent(TalentBrewUI.searchJobsByLinkGroup))
                app_SearchjobBy(clientUrl, Group, TalentBrewUI.searchJobsByLinkGroup, "Jobs By Group", TalentBrewUI.jobsByGroupLink, "L1");
            else
                report.AddReportStep("Jobs By Group|| Jobs By Group is not available for this client", "Jobs By Group|| Jobs By Group is not available for this client", StepResult.FAIL);
        }


        //Get job count ---Mani added
        public void app_getJobCount(string clientUrl, By SearchButton,By keywordTxtBox)
        {
            try
            {

                string tempUrl = "https://www.google.co.in";
                com_LaunchUrl(tempUrl);
                Thread.Sleep(5000);
                com_LaunchUrl(clientUrl);
                Thread.Sleep(4000);
               
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || (driver.Url.Contains("utc.com") && !driver.Url.Contains("jobs.otis.utc.com")))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                //Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }

                 //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                else if (driver.Url.Contains("careers.mcafee.com"))
                {
                    com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                    Thread.Sleep(2000);
                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                if (driver.Url.Contains(".citi."))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");
                if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("jobs.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);
                if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                Thread.Sleep(3000);

                com_New_WaitForObject(SearchButton, "Search button", 60);
                if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                {
                    TalentBrewUI.txt_locationSearch = By.XPath("(//input[@name='l'])[2]");
                }
                if (driver.Url.Contains("sonicdrivein.com"))
                {
                    TalentBrewUI.txt_locationSearch = By.XPath("//input[@name='l']");
                }
                if (com_IsElementPresent(SearchButton))
                {
                    com_ClearElement(TalentBrewUI.txt_locationSearch);
                    com_ClearElement(keywordTxtBox);
                   com_ClickOnInvisibleElement(SearchButton, "Clicked on Search jobs", "Problem in clicking search Jobs");
                    com_New_WaitForObject(TalentBrewUI.section_Searchresults, "Search results section", 60);
                   
                    if (com_IsElementPresent(TalentBrewUI.section_Searchresults))
                    {
                        RunManager.totalJobCount = Convert.ToInt32(com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results"));
                        report.AddReportStep("Getting job count", "Total job count - " +RunManager.totalJobCount, StepResult.PASS);
                        String filterName="";

                        if (com_IsElementPresent(TalentBrewUI.chk_countryToggle))
                        {
                            filterName = com_GetText(TalentBrewUI.chk_countryToggle);
                            report.AddScenarioHeader("Enhanced advance search - " + filterName);
                            String countryName = app_ApplyFilter(filterName, TalentBrewUI.chk_countryToggle, TalentBrewUI.chk_country1);
                          app_enhancedAdvanceSearch(clientUrl, countryName, SearchButton, keywordTxtBox);
                        }
                        if (com_IsElementPresent(TalentBrewUI.chk_regionToggle))
                        {
                            filterName=com_GetText(TalentBrewUI.chk_regionToggle);
                            report.AddScenarioHeader("Enhanced advance search - " + filterName);
                            String stateName = app_ApplyFilter(filterName, TalentBrewUI.chk_regionToggle, TalentBrewUI.chk_region1);
                            app_enhancedAdvanceSearch(clientUrl, stateName, SearchButton, keywordTxtBox);
                        }
                        if (com_IsElementPresent(TalentBrewUI.chk_cityToggle))
                        {

                            filterName = com_GetText(TalentBrewUI.chk_cityToggle);
                            report.AddScenarioHeader("Enhanced advance search - " + filterName);
                            String cityName = app_ApplyFilter(filterName, TalentBrewUI.chk_cityToggle, TalentBrewUI.chk_city1);
                            app_enhancedAdvanceSearch(clientUrl, cityName, SearchButton, keywordTxtBox);
                        }
                    }
                    else
                    {
                        report.AddReportStep("Verify L2 page/Search results section", "Problem in navigating to search results section/L2 page", StepResult.WARNING);
                    }
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Verify Enhanced advanced search", "Exception occured - Enhanced advanced search" + e.ToString(), StepResult.FAIL);
            
            }
        }

        public void app_enhancedAdvanceSearch(String clientUrl, String countryName, By SearchButton,By keywordTxtBox)
        {
            try
            {
                string tempUrl = "https://www.google.co.in";
                com_LaunchUrl(tempUrl);
                Thread.Sleep(5000);
                com_LaunchUrl(clientUrl);
                if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                {
                    TalentBrewUI.txt_locationSearch = By.XPath("(//input[@name='l'])[2]");
                }
                if (driver.Url.Contains("sonicdrivein.com"))
                {
                    TalentBrewUI.txt_locationSearch = By.XPath("//input[@name='l']");
                }
                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || (driver.Url.Contains("utc.com") && !driver.Url.Contains("jobs.otis.utc.com")))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                //Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }

                 //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                else if (driver.Url.Contains("careers.mcafee.com"))
                {
                    com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                    Thread.Sleep(2000);
                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                if (driver.Url.Contains(".citi."))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");
                if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("jobs.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);
                if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                Thread.Sleep(3000);

                com_New_WaitForObject(SearchButton, "Search jobs button", 60);
                string[] Location = countryName.Split(',');
                if (Location.Count() > 1)
                {
                    countryName = Location[0];
                }

                com_SendKeys(TalentBrewUI.txt_locationSearch, countryName, "The value " + countryName + " - is entered in location textbox", "Problem in entering - " + countryName + "in location textbox");
                Thread.Sleep(4000);
                if (com_IsElementPresent(By.XPath("(//ul[contains(@id,'search-location')]//li//a[contains(text(),'" + countryName + "')])[1]")))
                {
                    com_ClickOnInvisibleElement(By.XPath("(//ul[contains(@id,'search-location')]//li//a[contains(text(),'" + countryName + "')])[1]"), "Clicked from suggestion-" + countryName, "Problem in clicking - " + countryName + " from suggestion");
                }
                com_ClickOnInvisibleElement(SearchButton, "Clicked on search jobs button", "Problem in clicking the search jobs button");
                com_New_WaitForObject(TalentBrewUI.section_Searchresults, "Search results section", 60);

                if (com_IsElementPresent(TalentBrewUI.section_Searchresults))
                {
                    RunManager.locJobCount = Convert.ToInt32(com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results"));
                    report.AddReportStep("Filter job count value", "Filter job count value is - " + RunManager.locJobCount, StepResult.PASS);
                    if (RunManager.locJobCount < RunManager.filterJobCount)
                        report.AddReportStep("Verify location search job count is equal to filter job count", "Location search job count - " + RunManager.locJobCount + "is lesser than filter job count - " + RunManager.filterJobCount, StepResult.WARNING);
                    else if (RunManager.locJobCount > RunManager.filterJobCount)
                        report.AddReportStep("Verify location search job count is equal to filter job count", "Location search job count - " + RunManager.locJobCount + " is greater than filter job count - " + RunManager.filterJobCount, StepResult.WARNING);
                    else if (RunManager.locJobCount == RunManager.filterJobCount)
                        report.AddReportStep("Verify location search job count is equal to filter job count", "Location search job count - " + RunManager.locJobCount + " is equal to filter job count - " + RunManager.filterJobCount, StepResult.PASS);

                }
              
                com_LaunchUrl(clientUrl);

                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || (driver.Url.Contains("utc.com") && !driver.Url.Contains("jobs.otis.utc.com")))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                //Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }

                 //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                else if (driver.Url.Contains("careers.mcafee.com"))
                {
                    com_ClickOnInvisibleElement(By.XPath("(//a[@href='advanced_fields'])[2]"), "Successfully clicked on plus icon", "Not able to click on plus icon");
                    Thread.Sleep(2000);
                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("usccareers.usc.edu") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                if (driver.Url.Contains(".citi."))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");
                if (driver.Url.Contains("emplois.vinci-energies.com") || driver.Url.Contains("jobs.vinci-energies.com") || driver.Url.Contains("emplois.vinci-construction.com") || driver.Url.Contains("stellenangebote.vinci-energies.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_AdvanceSearch1, "Clicked on 'Advance Search' link", "Unable to click on 'Advance Search' link");

                if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);
                if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }
                Thread.Sleep(2000);
                com_New_WaitForObject(SearchButton, "Search jobs btn", 60);
                com_ClearElement(TalentBrewUI.txt_locationSearch);
                Thread.Sleep(1000);
                com_SendKeys(keywordTxtBox, countryName, "The value " + countryName + " - is entered in keyword textbox", "Problem in entering - " + countryName + "in keyword textbox");
                Thread.Sleep(1000);
                com_ClickOnInvisibleElement(SearchButton, "Clicked on search jobs button", "Problem in clicking the search jobs button");
                com_New_WaitForObject(TalentBrewUI.section_Searchresults, "Search results section", 80);
                if (com_IsElementPresent(TalentBrewUI.section_Searchresults))
                {
                    RunManager.keywordJobCOunt = Convert.ToInt32(com_getobjProperty(TalentBrewUI.section_Searchresults, "data-total-results"));
                    report.AddReportStep("Filter job count value", "Filter job count value is - " + RunManager.keywordJobCOunt, StepResult.PASS);
                    if (RunManager.keywordJobCOunt > RunManager.filterJobCount)
                        report.AddReportStep("Verify keyword search job count is greater than or equal to filter job count", "Keyword job count - " + RunManager.keywordJobCOunt + " is greater than filter job count - " + RunManager.filterJobCount, StepResult.PASS);
                    else if (RunManager.keywordJobCOunt < RunManager.filterJobCount)
                        report.AddReportStep("Verify keyword search job count is greater than or equal to filter job count", "Keyword job count - " + RunManager.keywordJobCOunt + " is lesser than filter job count - " + RunManager.filterJobCount, StepResult.WARNING);
                    else if (RunManager.keywordJobCOunt == RunManager.filterJobCount)
                        report.AddReportStep("Verify keyword search job count is greater than or equal to filter job count", "Keyword job count - " + RunManager.keywordJobCOunt + " is equal to filter job count - " + RunManager.filterJobCount, StepResult.PASS);
                }
            }
            catch (Exception e)
            {
                report.AddReportStep("Enhanced advanced search- comparing job count values", "Exception occured - Enhanced advanced search- comparing job cout values" + e.ToString(), StepResult.FAIL);
            }
                  
        }

        public string app_ApplyFilter(string filtertype, By obj_filtertype, By obj_FilterItem)       
        {
            string classAttr = string.Empty;
            string itemName = string.Empty;
            try
            {

                if (driver.Url.Contains("www.nestleusacareers.com"))
                {
                    //string a = com_GetText(TalentBrewUI.Filter_toggle);
                    if (!com_IsElementPresent(By.Id("keyword-tag")))
                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                    Thread.Sleep(3000);
                    if (!com_IsElementPresent(By.Id("keyword-tag")))
                        com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                    Thread.Sleep(3000);
                }

                if (com_IsElementPresent(TalentBrewUI.section_Filter) || com_IsElementPresent(TalentBrewUI.Section_filter1))
                {
                    if (driver.Url.Contains("www.nestleusacareers.com"))
                    {
                        //string a = com_GetText(TalentBrewUI.Filter_toggle);
                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                        Thread.Sleep(3000);
                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                        Thread.Sleep(3000);
                    }


                    if (com_IsElementPresent(obj_filtertype))
                    {
                        report.AddReportStep(filtertype + " || " + filtertype + " is present in filter section", filtertype + " || " + filtertype + " is present in filter section", StepResult.PASS);
                        classAttr = com_getobjProperty(obj_filtertype, "class");
                        if (classAttr.Contains("child-open"))
                        {
                            report.AddReportStep("Filter module " + filtertype + " || " + filtertype + " is expanded by default", "Filter module " + filtertype + " || " + filtertype + " is expanded by default", StepResult.WARNING);
                            if (com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button"))
                                app_loading(TalentBrewUI.expand_Filtertype);
                            //Thread.Sleep(4000);
                            classAttr = com_getobjProperty(obj_filtertype, "class");
                            if (classAttr.Contains("expandable-parent") || classAttr.Contains("expandable-parent expandable-child-open"))
                            {
                                report.AddReportStep("Filter module " + filtertype + " toggle is collapsed on clicking the collapse button", "Filter module " + filtertype + " toggle is collapsed on clicking the collapse button", StepResult.PASS);
                                if (com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + "expand button", "Filter Module || Problem in clicking the " + filtertype + " expand button"))
                                {
                                    WaitForObject(TalentBrewUI.expand_Filtertype, 100);
                                    //Thread.Sleep(4000);
                                    classAttr = com_getobjProperty(obj_filtertype, "class");
                                    if (classAttr.Contains("child-open"))
                                    {
                                        report.AddReportStep("Filter module || " + filtertype + "toggle is expanded on clicking the expand button", "Filter module || " + filtertype + "toggle is expanded on clicking the expand button", StepResult.PASS);
                                        itemName = com_getobjProperty(obj_FilterItem, "data-display");
                                        report.AddReportStep("Select first" + filtertype + "value", "First " + filtertype + " value - " + itemName + "- is selected", StepResult.PASS);
                                        if (com_IsElementPresent(obj_FilterItem))
                                        {
                                            RunManager.filterJobCount = Convert.ToInt32(com_getobjProperty(obj_FilterItem, "data-count"));
                                        }
                                        else if (com_IsElementPresent(TalentBrewUI.chk_city4))
                                        {
                                            obj_FilterItem = TalentBrewUI.chk_city4;
                                            RunManager.filterJobCount = Convert.ToInt32(com_getobjProperty(obj_FilterItem, "data-count"));
                                        }

                                        if (RunManager.filterJobCount < RunManager.totalJobCount)
                                        {
                                            report.AddReportStep("Verify filter job count is lesser than  total job count", "Filter job count - " + RunManager.filterJobCount + " is lesser than total job count- " + RunManager.totalJobCount, StepResult.PASS);
                                        }
                                        else if (RunManager.filterJobCount == RunManager.totalJobCount)
                                        {
                                            report.AddReportStep("Verify filter job count is lesser than  total job count", "Filter job count - " + RunManager.filterJobCount + " is equal total job count- " + RunManager.totalJobCount, StepResult.WARNING);
                                        }
                                        else
                                        {
                                            report.AddReportStep("Verify filter job count is lesser than  total job count", "Filter job count - " + RunManager.filterJobCount + " is greater than total job count- " + RunManager.totalJobCount, StepResult.WARNING);
                                        }

                                        if (driver.Url.Contains("www.nestleusacareers.com"))
                                        {
                                            //string a = com_GetText(TalentBrewUI.Filter_toggle);
                                            if (!com_IsElementPresent(By.Id("keyword-tag")))
                                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                            Thread.Sleep(3000);
                                            if (!com_IsElementPresent(By.Id("keyword-tag")))
                                                com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                            Thread.Sleep(3000);
                                        }

                                        com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button");
                                        app_loading(TalentBrewUI.expand_Filtertype);
                                    }
                                }

                            }
                            else
                            {
                                report.AddReportStep(filtertype + "toggle is not collapsed on clicking the collapse button", filtertype + "toggle is not collapsed on clicking the collapse button", StepResult.FAIL);
                            }

                        }
                        else
                        {
                            report.AddReportStep(filtertype + " toggle is not expanded by default", filtertype + " toggle is not expanded by default", StepResult.WARNING);
                            if (com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " expand button", "Filter Module || Problem in clicking the " + filtertype + " expand button"))
                            {
                                if (!(driver.Url.Contains("primark.com") || driver.Url.Contains("ikea") || driver.Url.Contains("coop")))
                                    WaitForObject(TalentBrewUI.expand_Filtertype, 100);
                                if (driver.Url.Contains("www.nestleusacareers.com"))
                                    Thread.Sleep(3000);

                                classAttr = com_getobjProperty(obj_filtertype, "class");
                                if (classAttr.Contains("child-open") || classAttr.Contains("active") || classAttr.Contains("toggle-open"))
                                {
                                    report.AddReportStep(filtertype + " toggle is expanded on clicking the expand button", filtertype + " toggle is expanded on clicking the expand button", StepResult.PASS);

                                    IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                                    // IWebElement firstItem   = driver.FindElement(obj_FilterItem);
                                    String filterjobcount = "";

                                    if (com_IsElementPresent(obj_FilterItem))
                                    {
                                        IWebElement firstItem = driver.FindElement(obj_FilterItem);
                                        itemName = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", firstItem);
                                        report.AddReportStep("Select first" + filtertype + "value", "First " + filtertype + " value - " + itemName + "- is selected", StepResult.PASS);
                                        filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", firstItem);
                                    }

                                    else if (com_IsElementPresent(TalentBrewUI.chk_city4))
                                    {
                                        IWebElement firstItem1 = driver.FindElement(TalentBrewUI.chk_city4);
                                        itemName = (String)js.ExecuteScript("return arguments[0].getAttribute('data-display');", firstItem1);
                                        report.AddReportStep("Select first" + filtertype + "value", "First " + filtertype + " value - " + itemName + "- is selected", StepResult.PASS);
                                        filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", firstItem1);
                                    }

                                    else if (com_IsElementPresent(TalentBrewUI.chk_city2))
                                    {
                                        IWebElement firstItem1 = driver.FindElement(TalentBrewUI.chk_city2);
                                        itemName = com_GetText(TalentBrewUI.chk_city2);
                                        report.AddReportStep("Select first" + filtertype + "value", "First " + filtertype + " value - " + itemName + "- is selected", StepResult.PASS);
                                        filterjobcount = driver.FindElement(By.XPath("//label[contains(@for,'city-filter-0')]//b[2]")).Text.ToString();
                                        IWebElement item2;
                                        if (filterjobcount.Equals(""))
                                        {
                                            item2 = driver.FindElementById("city-filter-0");
                                            filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", item2);

                                        }
                                    }

                                    else if (com_IsElementPresent(By.XPath("//label[contains(@for,'region-filter-0')]/b")))
                                    {
                                        IWebElement firstItem1 = driver.FindElement(By.XPath("//label[contains(@for,'region-filter-0')]/b[1]"));
                                        itemName = com_GetText(By.XPath("//label[contains(@for,'region-filter-0')]/b[1]"));
                                        report.AddReportStep("Select first" + filtertype + "value", "First " + filtertype + " value - " + itemName + "- is selected", StepResult.PASS);
                                        IWebElement item2=driver.FindElementByXPath("//label[contains(@for,'region-filter-0')]/b[2]");
                                        filterjobcount = driver.FindElement(By.XPath("//label[contains(@for,'region-filter-0')]/b[2]")).Text.ToString();
                                     
                                        if (filterjobcount.Equals(""))
                                        {
                                            item2 = driver.FindElementById("region-filter-0");
                                            filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", item2);

                                        }
                                    }
                                    else if (com_IsElementPresent(By.XPath("//label[contains(@for,'country-filter-0')]/b")))
                                    {
                                        IWebElement firstItem1 = driver.FindElement(By.XPath("//label[contains(@for,'country-filter-0')]/b[1]"));
                                        itemName = com_GetText(By.XPath("//label[contains(@for,'country-filter-0')]/b[1]"));
                                        report.AddReportStep("Select first" + filtertype + "value", "First " + filtertype + " value - " + itemName + "- is selected", StepResult.PASS);
                                        filterjobcount = com_GetText(By.XPath("//label[contains(@for,'country-filter-0')]/b[2]"));
                                        IWebElement item2;
                                        if (filterjobcount.Equals(""))
                                        {
                                            item2 = driver.FindElementById("country-filter-0");
                                            filterjobcount = (String)js.ExecuteScript("return arguments[0].getAttribute('data-count');", item2);

                                        }
                                    }
                                    System.Threading.Thread.Sleep(3000);

                                    if (driver.Url.Contains("www.nestleusacareers.com"))
                                    {
                                        //string a = com_GetText(TalentBrewUI.Filter_toggle);
                                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                        Thread.Sleep(3000);
                                        if (!com_IsElementPresent(By.Id("keyword-tag")))
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_toggle, " Clicked on show filters button", "Unable to click on show filters button");
                                        Thread.Sleep(4000);
                                    }
                                    if (!(driver.Url.Contains("primark.com") || driver.Url.Contains("ikea") || driver.Url.Contains("coop") || driver.Url.Contains("sleepnumber")))
                                    {
                                        if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                                        {
                                            Thread.Sleep(2000);
                                            com_ClickOnInvisibleElement(TalentBrewUI.Filter_button8, "Clicked on Filter button to expand filter section", "Problem in clicking the filter button to expand filter section");
                                        }
                                        // com_ClickOnInvisibleElement(obj_filtertype, "Filter Module || Clicked on the " + filtertype + " collapse button", "Filter Module || Problem in  clicking the " + filtertype + " collapse button");
                                        //app_loading(TalentBrewUI.expand_Filtertype);
                                        Thread.Sleep(3000);
                                    }
                                    RunManager.filterJobCount = Convert.ToInt32(filterjobcount);

                                    if (RunManager.filterJobCount < RunManager.totalJobCount)
                                    {
                                        report.AddReportStep("Verify filter job count is lesser than  total job count", "Filter job count - " + RunManager.filterJobCount + " is lesser than total job count- " + RunManager.totalJobCount, StepResult.PASS);
                                    }
                                    else if (RunManager.filterJobCount == RunManager.totalJobCount)
                                    {
                                        report.AddReportStep("Verify filter job count is lesser than  total job count", "Filter job count - " + RunManager.filterJobCount + " is equal total job count- " + RunManager.totalJobCount, StepResult.WARNING);
                                    }
                                    else
                                    {
                                        report.AddReportStep("Verify filter job count is lesser than  total job count", "Filter job count - " + RunManager.filterJobCount + " is greater than total job count- " + RunManager.totalJobCount, StepResult.WARNING);
                                    }

                                }
                                else
                                {
                                    report.AddReportStep(filtertype + " toggle is not expanded on clicking the expand button", filtertype + " toggle is not expanded on clicking the expand button", StepResult.FAIL);
                                }
                            }

                        }

                    }
                    else
                    {
                        report.AddReportStep(filtertype + " || " + filtertype + " is not applicable in filter section", filtertype + " || " + filtertype + " is not applicable in filter section", StepResult.WARNING);
                    }
                }

            }
            catch (Exception e)
            {
                report.AddReportStep("Enhanced advanced search-Getting filter values", "Exception occured - Enhanced advanced search- Getting filter values" + e.ToString(), StepResult.FAIL);
            }
            return itemName;
        }


//-------------------------------Mani added new method---------
        private void app_Key_Loc_SearchNew(string tempURL, By obj)
        {
            try
            {
                com_NewLaunchUrl(tempURL);
                By obj2;
                if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                {
                    obj = By.XPath("(//input[@name='k'])[2]");
                   
                }
              

                if (driver.Url.Contains("careers.jobsataramco.eu"))
                    com_ClickOnInvisibleElement(TalentBrewUI.search_Apply, "Clicked on Search and Apply Button", "Unable to click on Search and Apply Button");

                else if (driver.Url.Contains("careers.vmware.com") || driver.Url.Contains("interiorhealth.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.huskyenergy.com") || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        //Ramya Added
                        //Ramya Added Latest
                        if (com_IsElementPresent(TalentBrewUI.btn_searchjobs))
                            com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }//Ramya Added Latest
                if (driver.Url.Contains("fr.jobs-ups.ca"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                        Thread.Sleep(1000);
                    }
                } if (driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }

                }
                if (driver.Url.Contains("jobs.hcr-manorcare.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(TalentBrewUI.btn_searchjobs, "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                if (driver.Url.Contains("jobs.greatwolf.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {
                        com_ClickOnInvisibleElement(By.XPath("//button[@class='search-block__content-toggle']"), "Clicked on search jobs", "Not Clicked on search jobs");
                    }
                    Thread.Sleep(2000);
                }
                //Ramya Added
                else if (driver.Url.Contains("pgcareers.com"))
                {
                    if (!com_IsElementPresent(TalentBrewUI.txt_LocationSearch))
                    {

                        com_ClickOnInvisibleElement(TalentBrewUI.Searchopportunities_btn, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                    }
                }

                if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("jobs.mdanderson.org") || driver.Url.Contains("jobs.mcleodhealth.org") || driver.Url.Contains("jobs.nike.com") || driver.Url.Contains("careers.kumandgo.com") || driver.Url.Contains("www.wellsfargojobs.com"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_searchJobs2, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                //Ramya Added
                else if (driver.Url.Contains("fr.wellsfargojobs.ca"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobsNew, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");
                else if (driver.Url.Contains("jobs.greeneking.co.uk"))
                    com_ClickOnInvisibleElement(TalentBrewUI.btn_SearchJobs4, "Clicked on 'Search Jobs' button", "Unable to click on 'Search Jobs' Button");

                else if (driver.Url.Contains("jobs.atos.net"))
                    WaitForObject(TalentBrewUI.btn_Explore, 100);


                //Ramya Added
                if ((driver.Url.Contains("jobs-ups") && !driver.Url.Contains("fr.jobs-ups.ca")) || driver.Url.Contains("jobs.cornerstonebrands.com") || driver.Url.Contains("utc.com") || driver.Url.Contains("jobs.santanderbank.com") || driver.Url.Contains("jobs.delltechnologies.com"))
                {
                    obj2 = TalentBrewUI.btn_Search3;
                }
                else if (driver.Url.Contains("usa.jobs.scotiabank.com"))
                {
                    obj2 = TalentBrewUI.btn_search6;
                }
                //Ramya Added New
                else if (driver.Url.Contains("scotiabank.com"))
                {
                    obj2 = TalentBrewUI.btn_Search_New;
                }
                //Mani added
                else if (driver.Url.Contains("jobs.cdc.gov"))
                {

                    obj2 = TalentBrewUI.btn_Search3;
                }
                else if (driver.Url.Contains("emplois.enterprise.ca") || driver.Url.Contains("careers.enterprise.ca") || driver.Url.Contains("careers.enterprise.com"))
                    obj2 = TalentBrewUI.btn_Search7;
                else
                {
                    obj2 = TalentBrewUI.btn_Search;
                }
                Thread.Sleep(2000);

                app_getJobCount(tempURL, obj2, obj);
            }
            catch (Exception e)
            {
                report.AddReportStep("Advance Search || Expection Occured : " + e.ToString(), "Advance Search || Expection Occured : " + e.ToString(), StepResult.FAIL);
            }
        }

//----------------End------------------------------

    }
}
