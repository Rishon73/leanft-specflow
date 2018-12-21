using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Diagnostics;

using HP.LFT.SDK;
using HP.LFT.SDK.Web;
using HP.LFT.Verifications;
using HP.LFT.Report;

namespace leanft_specflow
{
    public partial class AdvantageOnlineFeature : UnitTestClassBase
    { }

    [Binding]
    public class AdvantageOnlineSteps
    {
        private static IBrowser browser;
        private static AOSAppModel model;

        [BeforeTestRun]
        public static void setup()
        {
            browser = BrowserFactory.Launch(BrowserType.Chrome);
            model = new AOSAppModel(browser);
        }

        [Given(@"I am in the site")]
        public void GivenIAmInTheSite()
        {
            //browser.Navigate("http://nimbusserver:8080/#/");
            browser.Navigate("http://www.advantageonlineshopping.com/#/");
        }

        [AfterFeature]
        public static void TearDown()
        {
            browser.Close();
        }

        [Given(@"I select the Mice category")]
        public void GivenISelectTheMiceCategory()
        {
            /* The below code was left so you can easily see the difference between
            descriptive programming and using the Application Models
            ---------------------------------------------------------------------------------------------------------
            var mICEShopNowLink = browser.Describe<ILink>(new LinkDescription {
				InnerText = @"MICE Shop Now ",
				TagName = @"DIV"
			});
            */

            model.AdvantageShoppingPage.MICEShopNowLink.Click();
        }
        
        [When(@"I filter by ""(.*)"" color")]
        public void WhenIFilterByColor(string strSelectColor)
        {
            model.AdvantageShoppingPage.COLORLink.Click();
            System.Threading.Thread.Sleep(1000);

            String strHexColor = null;
            switch (strSelectColor.ToLower())
            {
                case "white":
                    strHexColor = "FFFFFF";
                    break;
                case "black":
                    strHexColor = "414141";
                    break;
                case "blue":
                    strHexColor = "3683D1";
                    break;
                case "gray":
                    strHexColor = "C3C3C3";
                    break;
                case "purple":
                    strHexColor = "545195";
                    break;
                case "red":
                    strHexColor = "DD3A5B";
                    break;
                case "yellow":
                    strHexColor = "FCC23D";
                    break;

            }

            var mICEShopNowLink = browser.Describe<ILink>(new LinkDescription
            {
                CSSSelector = @"div#micesImg",
                XPath = @"//DIV[@id=""micesImg""]"
            });

            IWebElement colorPicker = browser.Describe<IWebElement>(new WebElementDescription
            {
                AccessibilityName = string.Empty,
                ClassName = @"productColor ",
                Id = @"productsColors"+ strHexColor,
                Index = 3,
                InnerText = string.Empty,
                TagName = @"A"
            });
            colorPicker.Highlight();
            colorPicker.Click();

        }
        
        [Then(@"the mouse price is ""(.*)""")]
        public void ThenTheMousePriceIs(string price)
        {
            System.Threading.Thread.Sleep(2000);

            IWebElement webElement = browser.Describe<IWebElement>(
                new CSSDescription ( 
                    @"html > body > div:nth-child(8) > section > article > div:nth-child(4) > div > div > div:nth-child(2) > ul > li:nth-child(1) > p:nth-child(5) > a"
                ));
            webElement.Highlight();
            String webPrice;
            webPrice = webElement.InnerText;

            
            if (price != webPrice.Trim())
                Assert.Fail();

        }
    }
}
