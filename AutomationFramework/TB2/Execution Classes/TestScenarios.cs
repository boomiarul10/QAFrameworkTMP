using TB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TB2
{
   public class TestScenarios:BaseExecutionClass
    {
       ReusableComponents rc;
       WebDriverHelper wdHelper;

       public TestScenarios(TestCase currentTestCase)
           : base(currentTestCase)
       {
           wdHelper = new WebDriverHelper(driver);
           rc = new ReusableComponents(driver,report,wdHelper, dataHelper);           
       }

       public void Test_MainMenu()
       {
           rc.OpenHomePage();
           rc.ClickMainMenu();
       }

       public void Test_SubMenu()
       {
           rc.OpenHomePage();
           rc.ClickSubMenu();
       }

    }
}
