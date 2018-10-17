using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSIS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormSQL());
        }
    }
}
    /*
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                // Go to the home page
                driver.Navigate().GoToUrl("https://www.morningstar.com/stocks/xtse/lmc/quote.html");
                var ow = driver.FindElementsByClassName ("msiip-application-navigation__list-item");
                foreach (var o in ow)
                {
                    if (o.Text == "Ownership")
                    {
                        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementTo‌BeClickable(o)).Click();
                        txtLog.AppendText(o.Text + Environment.NewLine);
                        o.Click();
                        Thread.Sleep(2000);

                        var links = driver.FindElements(By.ClassName("ng-binding"));
                        txtLog.AppendText("Number of links: " + links.Count.ToString());


                        IWebElement inst=null;
                        foreach (var c in links)
                        {
                            try
                            {
                                {
                                    txtLog.AppendText("link: " + c.Text + Environment.NewLine);
                                    if (c.Text.ToLower().Trim() == "institutions")
                                    {
                                        inst = c;
                                    }
                                }
                            }
                            catch (Exception ex)

                            {
                                txtLog.AppendText(ex.ToString() + Environment.NewLine);
                            }
                            
                        }

                        if(inst!=null)
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementTo‌BeClickable(inst)).Click();

                        //    o.Click();
                        //    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40000));

                        //    // Test the autocomplete response - Explicit Wait
                        //    IWebElement autocomplete = wait.Until(x => x.FindElement(By.Id("sal-components-ownership")));

                        //    Debug.WriteLine(autocomplete.Text);
                        //    txtLog.AppendText(autocomplete.Text);

                    }
                }

               
            }
     * 
     */
