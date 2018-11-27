using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSIS.Entity
{
    public static class Webber
    {
        public static ChromeDriver ChromeBrowser = new ChromeDriver();

        public static string GetMStarOwnershipInfo(string symbol, string exch)
        {
            string retValue = string.Empty;
            //using (var driver = new ChromeDriver())
            {
                ChromeBrowser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                // Go to the home page
                string url = @"https://www.morningstar.com/stocks/";
                ChromeBrowser.Navigate().GoToUrl(url + GetMStarEx(exch.Trim()) + symbol.Trim() + @"/quote.html");
                var ow = ChromeBrowser.FindElementsByClassName("msiip-application-navigation__list-item");
                foreach (var o in ow)
                {
                    if (o.Text == "Ownership")
                    {
                        var wait = new WebDriverWait(ChromeBrowser, new TimeSpan(0, 0, 30));
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementTo‌BeClickable(o)).Click();
                        retValue += (o.Text + Environment.NewLine);
                        o.Click();
                        Thread.Sleep(4000);

                        var links = ChromeBrowser.FindElements(By.ClassName("ng-binding"));
                        retValue += ("Number of links: " + links.Count.ToString());


                        IWebElement inst = null;
                        foreach (var c in links)
                        {
                            try
                            {
                                {
                                    if (c.Text.Trim() != string.Empty)
                                    {
                                        retValue += ("link: " + c.Text + Environment.NewLine);
                                        if (c.Text.ToLower().Trim() == "institutions")
                                        {
                                            inst = c;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)

                            {
                                retValue += (ex.ToString() + Environment.NewLine);
                            }
                        }

                        if (inst != null)
                        {
                            retValue += (">> Institutions << " + Environment.NewLine);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementTo‌BeClickable(inst)).Click();
                            Thread.Sleep(4000);
                            links = ChromeBrowser.FindElements(By.ClassName("ng-binding"));
                            retValue += ("Number of links: " + links.Count.ToString());


                            inst = null;
                            foreach (var c in links)
                            {
                                try
                                {
                                    if(c.Text.Trim()!=string.Empty)
                                        retValue += ("link: " + c.Text + Environment.NewLine);
                                }
                                catch (Exception ex)
                                {
                                    retValue += (ex.ToString() + Environment.NewLine);
                                }
                            }
                        }
                    }
                }
                return retValue;
            }
        }

        public static string GetMStarEx(string ex)
        {
            if (ex.ToUpper() == "X" || ex.ToUpper() == "T")
            {
                return "XTSE/";
            }
            else if (ex.ToUpper() == "A")
            {
                return "XASE/";
            }
            else if (ex.ToUpper() == "N")
            {
                return "XNYS/";
            }
            else if (ex.ToUpper() == "Q")
            {
                return "XNAS/";
            }
            else
                return string.Empty;
        }
    }
}
