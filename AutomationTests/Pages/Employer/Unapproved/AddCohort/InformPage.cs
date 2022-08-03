using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class InformPage : PageObject
    {
        public InformPage(Page page) : base(page)
        { }

        public override string PageTitle => "Add an apprentice";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/inform";

        public async Task GoToPage(string accountId)
        {
            var url = Url.Replace("{accountId}", accountId);

            await Page.GoToAsync(url);
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            await Page.ClickOn(ContinueButton);
            return PageObjectFactory.CreatePage<T>(Page);
        }

        private static string ContinueButton = "#continue-button";
    }
}
