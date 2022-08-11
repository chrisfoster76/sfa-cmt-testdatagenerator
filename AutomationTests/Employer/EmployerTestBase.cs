using System;
using System.Threading.Tasks;
using AutomationTests.Actors;
using AutomationTests.Pages.Employer;
using NUnit.Framework;
using PuppeteerSharp;
using ScenarioBuilder.Helpers;

namespace AutomationTests.Employer
{
    public class EmployerTestBase
    {
        protected Browser Browser { get; set; }
        protected Page Page { get; set; }

        [SetUp]
        public async Task Setup()
        {
            ConfigurationHelper.Initialise(TestContext.CurrentContext.TestDirectory);
            DbHelper.ClearDb();
            IdentityHelpers.ClearAll();

            var employerActor = (EmployerActor)TestContext.CurrentContext.Test.Arguments.GetValue(0);

            var employer = Actors.Employer.Create(employerActor);

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            var launchOptions = new LaunchOptions
            {
                Headless = false,
                IgnoreHTTPSErrors = true,
                SlowMo = 0
            };

            Console.WriteLine("Launching browser...");

            Browser = await Puppeteer.LaunchAsync(launchOptions);

            var context = Browser.DefaultContext;

            Page = await context.NewPageAsync();

            var url = LaunchPage.Url.Replace("{accountId}", employer.EncodedAccountId);
            Console.WriteLine($"Opening {url}");
            var response  = await Page.GoToAsync(url);

            //await Page.TypeInputAsync(LaunchPage.EmailAddress, employer.Username);
            //await Page.TypeInputAsync(LaunchPage.Password, employer.Password);
            Console.Write("Signing in... ");
            await Page.ClickOn($"{LaunchPage.SignInPrefx}{employer.Username}");
            await Page.WaitForNavigationAsync();
            Console.WriteLine("Complete");
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
            await Browser.CloseAsync();
        }
    }
}
