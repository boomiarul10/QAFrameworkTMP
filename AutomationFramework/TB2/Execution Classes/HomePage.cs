using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using TB2.DAO;
using TB2.UIStore;
using System.Threading;

namespace TB2
{
    class HomePage : BaseExecutionClass
    {
        ReusableComponents rc;
        WebDriverHelper wdHelper;

        string category;
        string Location;
        string Group;
        string AdvancedSearch;
        string BasicSearch;
        string FilterModule;
        string RecentJobs;
        string SocialMedia;
        string JobAlert;
        string SiteMap;
        string RSSFeed;
        string MeetUs;
        string clientUrl;
        string JobMatching;
        //new mani added--22/10
        string enhancedAdvanceSearch;


        public HomePage(TestCase currentTestCase)
            : base(currentTestCase)
        {
            wdHelper = new WebDriverHelper(driver);
            //UI = new HDCUIStore();            
            rc = new ReusableComponents(driver, report, wdHelper, dataHelper);
        }


        public void xx()
        {

        }

        public void setUp()
        {
            try
            {
                category = rc.getDataCommon(CommonDataColumn.Common_Category);
                Location = rc.getDataCommon(CommonDataColumn.Common_Location);
                Group = rc.getDataCommon(CommonDataColumn.Common_Group);
                AdvancedSearch = rc.getDataCommon(CommonDataColumn.Common_AdvancedSearch);
                BasicSearch = rc.getDataCommon(CommonDataColumn.Common_BasicSearch);
                FilterModule = rc.getDataCommon(CommonDataColumn.Common_FilterModule);
                RecentJobs = rc.getDataCommon(CommonDataColumn.Common_RecentJobs);
                SocialMedia = rc.getDataCommon(CommonDataColumn.Common_SocialMedia);
                JobAlert = rc.getDataCommon(CommonDataColumn.Common_JobAlert);
                SiteMap = rc.getDataCommon(CommonDataColumn.Common_SiteMap);
                RSSFeed = rc.getDataCommon(CommonDataColumn.Common_RSSFeed);
                MeetUs = rc.getDataCommon(CommonDataColumn.Common_MeetUs);
                JobMatching = rc.getDataCommon(CommonDataColumn.Common_JobMatching);
                enhancedAdvanceSearch = rc.getDataCommon(CommonDataColumn.Common_enhancedAdvanceSearch);


                clientUrl = dataHelper.GetCommonData("EnvironmentUrl");
            }
            catch (Exception e)
            {

            }
        }

        public void deleteAllCookies()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
        }

        public void verifyHomePage()
        {
            try
            {
                setUp();
                report.AddReportStep("Start Of the Testcase", "Start Of the Testcase", StepResult.PASS);
                deleteAllCookies();
                rc.com_NewLaunchUrl(clientUrl);
                if (driver.Url.Contains("jobs.mountcarmelhealth.com"))
                {
                    if (rc.com_IsElementPresent(TalentBrewUI.joinOurTalent_Popup))
                        rc.com_ClickOnInvisibleElement(TalentBrewUI.closeButton_TalentPop, "Clicked on Join Our Talent Pop up close button", "Unable to click on the Join Our Talent Pop up close button");
                }

                if (rc.com_IsElementPresent(TalentBrewUI.HomePage) || rc.com_IsElementPresent(TalentBrewUI.Homepage4) || rc.com_IsElementPresent(TalentBrewUI.Homepage3) || rc.com_IsElementPresent(TalentBrewUI.Homepage1) || rc.com_IsElementPresent(TalentBrewUI.Homepage2) || (driver.Url.ToString().Contains("/search-site")) || rc.com_IsElementPresent(TalentBrewUI.Homepage5) || rc.com_IsElementPresent(TalentBrewUI.Homepage6))
                //if (rc.com_IsElementPresent(TalentBrewUI.HomePage) || (driver.Url.ToString().Contains("/search-site")))
                {
                    report.AddReportStep("Home Page is loaded successfully.", "Home Page is loaded Successfully.", StepResult.PASS);
                    report.AddScenarioHeader("Verify Google Analytics");
                    //if (rc.com_VerifyObjPresent((TalentBrewUI.link_googleAnalytics), "Google Analytics || Google Analytics code is available", "Google Analytics || Google Analytics code is not available - As Google Analytics javascript is not available"))
                    //rc.app_verifyGoogleAnalyticsCode(driver.PageSource, "ga('create',", "'auto');");

                    string gaCode = rc.app_GACodeBetween(driver.PageSource, "ga('create',", "'auto');");
                    if (gaCode != null)
                    {
                        report.AddReportStep("Google Analytics || Veirfy Google Analytics code is available", "Google Analytics code is available: ' " + gaCode + "", StepResult.PASS);
                    }
                    else
                    {
                        report.AddReportStep("Google Analytics || Veirfy Google Analytics code is available", "Google Analytics || Google Analytics code is not available", StepResult.FAIL);
                    }

                    rc.app_JobsByCategory(clientUrl, category, "L1");
                    rc.app_JobsByLocation(clientUrl, Location, "L1");
                    rc.app_JobsByGroup(clientUrl, Group, "L1");

                    if (driver.Url.Contains("jobs.atos.net"))
                    {
                        Thread.Sleep(3000);
                    }

                    rc.app_recentJobs(clientUrl, RecentJobs, "L1");

                    rc.app_socialMedia(clientUrl, SocialMedia, "L1");

                    rc.app_siteMap(clientUrl, SiteMap, "L1");

                    //rc.app_jobAlerts(clientUrl, JobAlert, "L1");

                    rc.app_basicSearch(clientUrl, BasicSearch, "L1");

                 rc.app_AdvanceSearch(clientUrl, AdvancedSearch, "L1");

                

                    rc.app_RSSFeed(clientUrl, RSSFeed, "L1");

                    rc.app_JobMatching(clientUrl, JobMatching, "L1");
                }

                else
                {
                    report.AddReportStep("Home Page is not loaded successfully", driver.Title + ": Page is loaded", StepResult.FAIL);
                }

                report.AddReportStep("End of the Test Case", "End Of the Testcase", StepResult.PASS);
                //driver.Manage().Cookies.DeleteAllCookies();
                //deleteAllCookies();
            }
            catch (Exception e) { }
        }

        public void verifyJobSearchPage()
        {
            try
            {
                setUp();
                report.AddReportStep("Start Of the Testcase", "Start Of the Testcase", StepResult.PASS);
                //deleteAllCookies();
                rc.com_NewLaunchUrl(clientUrl);
                if (driver.Url.Contains("jobs.mountcarmelhealth.com"))
                {
                    if (rc.com_IsElementPresent(TalentBrewUI.joinOurTalent_Popup))
                        rc.com_ClickOnInvisibleElement(TalentBrewUI.closeButton_TalentPop, "Clicked on Join Our Talent Pop up close button", "Unable to click on the Join Our Talent Pop up close button");
                }
                if (rc.com_IsElementPresent(TalentBrewUI.HomePage) || rc.com_IsElementPresent(TalentBrewUI.Homepage4) || rc.com_IsElementPresent(TalentBrewUI.Homepage3) || rc.com_IsElementPresent(TalentBrewUI.Homepage1) || rc.com_IsElementPresent(TalentBrewUI.Homepage2) || (driver.Url.ToString().Contains("/search-site")) || rc.com_IsElementPresent(TalentBrewUI.Homepage5) || rc.com_IsElementPresent(TalentBrewUI.Homepage6))
                //if (rc.com_IsElementPresent(TalentBrewUI.HomePage) || (driver.Url.ToString().Contains("/search-site")))
                {
                    report.AddReportStep("Home Page is loaded successfully", "Home Page is loaded Successfully", StepResult.PASS);

                    rc.app_JobsByLocation(clientUrl, Location, "L2");

                    rc.app_JobsByCategory(clientUrl, category, "L2");

                    rc.app_JobsByGroup(clientUrl, Group, "L2");

                    rc.app_recentJobs(clientUrl, RecentJobs, "L2");

                    rc.app_AdvanceSearch(clientUrl, AdvancedSearch, "L2");

                    rc.app_basicSearch(clientUrl, BasicSearch, "L2");

                    rc.app_socialMedia(clientUrl, SocialMedia, "L2");

                    rc.app_siteMap(clientUrl, SiteMap, "L2");

                    rc.app_Filter(clientUrl, FilterModule, "L2");

                    //rc.app_jobAlerts(clientUrl, JobAlert, "L2");

                    rc.app_RSSFeed(clientUrl, RSSFeed, "L2");

                    rc.app_JobMatching(clientUrl, JobMatching, "L2");
                }
                else
                {
                    report.AddReportStep("Home Page is not loaded.", driver.Title + ": Page is loaded.", StepResult.FAIL);
                }

                report.AddReportStep("End of the Test Case", "End Of the Testcase", StepResult.PASS);
                //deleteAllCookies();

            }
            catch (Exception e)
            {

            }
        }

        public void verifyJobDetailPage()
        {
            try
            {
                setUp();
                report.AddReportStep("Start Of the Testcase", "Start Of the Testcase", StepResult.PASS);
                //deleteAllCookies();
                rc.com_NewLaunchUrl(clientUrl);

                if (rc.com_IsElementPresent(TalentBrewUI.HomePage) || rc.com_IsElementPresent(TalentBrewUI.Homepage4) || rc.com_IsElementPresent(TalentBrewUI.Homepage3) || rc.com_IsElementPresent(TalentBrewUI.Homepage1) || rc.com_IsElementPresent(TalentBrewUI.Homepage2) || (driver.Url.ToString().Contains("/search-site")) || rc.com_IsElementPresent(TalentBrewUI.Homepage5) || rc.com_IsElementPresent(TalentBrewUI.Homepage6))
                {

                    rc.app_socialMedia(clientUrl, SocialMedia, "L3");

                    rc.app_siteMap(clientUrl, SiteMap, "L3");

                    rc.app_meetUs(clientUrl, MeetUs, "L3");


                    //rc.app_jobAlerts(clientUrl, JobAlert, "L3");

                    rc.app_RSSFeed(clientUrl, RSSFeed, "L3");

                    rc.app_recentJobs(clientUrl, RecentJobs, "L3");
                }

                else
                {
                    report.AddReportStep("Home Page is not loaded.", driver.Title + ": Page is loaded.", StepResult.FAIL);
                }

                report.AddReportStep("End of the Test Case", "End Of the Testcase", StepResult.PASS);
                deleteAllCookies();
            }
            catch (Exception e)
            {

            }
        }

    }
}
