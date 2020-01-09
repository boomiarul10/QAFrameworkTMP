using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Drawing;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
namespace TB2
{
    public class Driver
    {
        public static RemoteWebDriver driver;
        public RemoteWebDriver GetWebDriver(TestCase currentTestCase)
        {
            if (!prevbrowser.browser.Equals(currentTestCase.Browser.ToString()))
            {
                if (!prevbrowser.browser.Equals(""))
                {
                    Driver.QuitDriver(driver);
                }
                switch (currentTestCase.Browser)
                {
                    case Browser.FireFox:
                        try
                        {
                            string path = @"D:\TB2.ProfileF";
                            //string path = @"D:\TB2.Profile3";
                            //string path = @"D:\JobIDProfile";
                            //string path = @"D:\TB2.Part3";
                            //string path = @"C:\DONOT_Delete_MindTree_Automation_Scripts\TB2.ProfileN";
                            //string path = @"C:\DONOT_Delete_MindTree_Automation_Scripts\TB2.ProfileF";
                            //string path = @"C:\DONOT_Delete_MindTree_Automation_Scripts\TB2.ProfileL";


                           
                            FirefoxProfile ffprofile = new FirefoxProfile(path);
                            driver = new FirefoxDriver(ffprofile);

                            //"D:/Autoamtion Framework/TB2/Newly created_First/Drivers/FirefoxPortable.exe"
                            //FirefoxProfile ffprofile = new FirefoxProfileManager().GetProfile("FirefoxPortable");
                            //var firefoxBinary = new FirefoxBinary(HelperClass.driversPath+"/FirefoxPortable.exe");
                            //driver = new FirefoxDriver(firefoxBinary, ffprofile);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            goto default;
                        }
                        break;
                    case Browser.Chrome:
                        try
                        {
                           // driver = new ChromeDriver(HelperClass.driversPath);
                            ChromeOptions options = new ChromeOptions();
                            options.AddArguments("disable-infobars");
                            options.AddUserProfilePreference("credentials_enable_service", false);
                            options.AddUserProfilePreference("profile.password_manager_enabled", false);
                            driver = new ChromeDriver(HelperClass.driversPath, options);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            goto default;
                        }
                        break;
                    case Browser.IE:
                        try
                        {
                            /* DriverService service = InternetExplorerDriverService.CreateDefaultService(HelperClass.driversPath);
                             var options = new InternetExplorerOptions();
                             options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                             driver = new InternetExplorerDriver(service, options, TimeSpan.FromSeconds(60));
                            driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), DesiredCapabilities.InternetExplorer());*/

                            driver = new InternetExplorerDriver(HelperClass.driversPath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            goto default;
                        }
                        break;
                    case Browser.Safari:
                        try
                        {
                            driver = new SafariDriver();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            goto default;
                        }
                        break;
                    default:
                        switch (HelperClass.DefaultBrowser)
                        {
                            case Browser.FireFox:
                                driver = new FirefoxDriver();
                                currentTestCase.Browser = Browser.FireFox;
                                break;
                            case Browser.Chrome:
                                driver = new ChromeDriver(HelperClass.driversPath);
                                currentTestCase.Browser = Browser.Chrome;
                                break;
                            case Browser.IE:
                                DriverService service = InternetExplorerDriverService.CreateDefaultService(HelperClass.driversPath);
                                var options = new InternetExplorerOptions();
                                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                                // driver = new InternetExplorerDriver(service, options, TimeSpan.FromSeconds(60));
                                currentTestCase.Browser = Browser.IE;
                                break;
                            case Browser.Safari:
                                driver = new SafariDriver();
                                currentTestCase.Browser = Browser.Safari;
                                break;
                        }
                        break;
                }
            }
            prevbrowser.browser = currentTestCase.Browser.ToString();
            driver.Manage().Timeouts().SetPageLoadTimeout(System.TimeSpan.FromSeconds(180));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

         //   driver.Manage().Window.Size = new Size(1600, 900);
          driver.Manage().Window.Maximize();
            return (RemoteWebDriver)driver;

        }

        public static void QuitDriver(RemoteWebDriver driver)
        {
            driver.Quit();
        }
    }
}
