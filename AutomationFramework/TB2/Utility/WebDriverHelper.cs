using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TB2.UIStore;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;

namespace TB2
{

    public class WebDriverHelper
    {
        WebDriverHelper wdHelper;
        public RemoteWebDriver driver { get; set; }
      

        public WebDriverHelper(RemoteWebDriver driver)
           
        {
            this.driver = driver;
        }

     

        public bool VerifyObjPresent(By elementBy)
        {
            bool verifyObj = false;
            try
            {

                if (IsElementPresent(elementBy))
                {

                    verifyObj = true;
                }
            }
            catch (Exception e)
            {
               
            }
            return verifyObj;
        }

        public bool IsElementPresent(By elementBy)
        {
            bool isElementPresent = false;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                IWebElement webElement = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(elementBy);
                });

                isElementPresent = driver.FindElement(elementBy).Displayed;

            }
            catch (WebDriverException wdEx)
            {
                Console.WriteLine(wdEx.Message);
            }
            return isElementPresent;
        }

        public bool IsTextPresent(string verifyText)
        {
            bool isTextPresent = false;
            isTextPresent = GetText(By.CssSelector("BODY")).Contains(verifyText);
            return isTextPresent;
        }

        //public bool IsElementPresent(IWebElement element, By elementBy)
        //{
        //    return element.FindElement(elementBy).Displayed;
        //}

        public bool ClickElement(By elementBy)
        {
            bool clickElement = false;
            //bool exceptionOccured = false;
            try
            {
                if (IsElementPresent(elementBy))
                {
                    driver.FindElement(elementBy).Click();
                    clickElement = true;
                }
                //   else
                //{
                //    Exception ex = new Exception("Click element not found");
                //    throw ex;
                //}
                //if (exceptionOccured)
                //{
                //    try
                //    {
                //        JSClick(driver.FindElement(elementBy));
                //        clickElement = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        // Logger.Log(ex.ToString());
                //        Console.WriteLine(ex.ToString() + " Exception occured while clicking element using JavaScript method");
                //    }
                //}
            }
            catch (Exception ex)
            {
                //  exceptionOccured = true;
                //Console.WriteLine(ex.ToString() + " Exception occured while clicking element using driver click method");
                //  Logger.Log(ex.ToString());                   
            }

            return clickElement;
        }

        //public void ClickElement(IWebElement element, By elementBy)
        //{
        //    if (IsElementPresent(element,elementBy))
        //        element.FindElement(elementBy).Click();
        //}




        //public void MouseOver(IWebElement element)
        //{
        //    String code = "var fireOnThis = arguments[0];"
        //                 + "var evObj = document.createEvent('MouseEvents');"
        //                 + "evObj.initEvent( 'mouseover', true, true );"
        //                 + "fireOnThis.dispatchEvent(evObj);";
        //    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
        //    executor.ExecuteScript(code, element);
        //}

        //public void JSClick(IWebElement element)
        //{
        //    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
        //    executor.ExecuteScript("arguments[0].click();", element);
        //}

        public void ClearElement(By elementBy)
        {
            driver.FindElement(elementBy).Clear();
        }

        public bool SelectByValue(By elementBy, String label, String PassMsg, String FailMsg)
        {
            bool selectValue = false;
            try
            {
                new SelectElement(driver.FindElement(elementBy)).SelectByText(label);
                selectValue = true;
              
            }
            catch (Exception e)
            {
            }
            return selectValue;
        }

        public bool SelectByIndex(By elementBy, int index, String PassMsg, String FailMsg)
        {
            bool selectIndex = false;
            try
            {
                new SelectElement(driver.FindElement(elementBy)).SelectByIndex(index);
                selectIndex = true;
              
            }
            catch (Exception e)
            {
             
            }
            return selectIndex;
        }


        //public void WaitForValue(By elementBy, String value)
        //{
        //    for (int second = 0; ; second++)
        //    {
        //        if (second >= 60) break; //ASSERT Fail("Timeout") ; 
        //            try
        //            {
        //                if (value == driver.FindElement(elementBy).GetAttribute("value")) break;
        //            }
        //            catch (Exception)
        //            { }

        //            Thread.Sleep(1000);
        //    }
        //}

        //public void WaitForElement(By elementBy)
        //{
        //    for (int second = 0; ; second++)
        //    {
        //        if (second >= 60) break; // Assert.Fail("timeout");
        //        try
        //        {
        //            if (IsElementPresent(elementBy)) break;
        //        }
        //        catch (Exception)
        //        { }
        //        Thread.Sleep(1000);
        //    }
        //}

        public bool SendKeys(By elementBy, string typeValue, String PassMsg, String FailMsg)
        {
            bool sendKeys = false;
            try
            {
                if (IsElementPresent(elementBy))
                {
                    ClearElement(elementBy);
                    driver.FindElement(elementBy).SendKeys(typeValue);
                    sendKeys = true;
                    
                }

            }
            catch (Exception e)
            {

                
            }
            return sendKeys;
        }

        public string GetText(By elementBy)
        {
            try
            {
                if (IsElementPresent(elementBy))
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

        //   public string GetText(IWebElement element, By elementBy)
        //   {
        //       if (IsElementPresent(element, elementBy))
        //           return element.FindElement(elementBy).Text;
        //       else
        //           return string.Empty;
        //   }
        //}
    }
}