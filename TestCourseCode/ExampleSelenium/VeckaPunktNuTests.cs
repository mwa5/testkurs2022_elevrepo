using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ExampleSelenium
{
    public class VeckaPunktNuTests
    {
        [Fact]
        public void TestSkaLäsaNuvarandeVeckaFrånVeckaPunktNu()
        {

                /*ChromeOptions options = new ChromeOptions();

                options.AddArguments(new List<string>()
                {
                    "--silent-launch",
                    "--no-startup-window",
                    "no-sandbox",
                    "headless"
                });*/
                IWebDriver driver = new ChromeDriver();
                String veckonummer = "";
                driver.Close();
                Assert.Equal("39", veckonummer);

        }
    }
}

