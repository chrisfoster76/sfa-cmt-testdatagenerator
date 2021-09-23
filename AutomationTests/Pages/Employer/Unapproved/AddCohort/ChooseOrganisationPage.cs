using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class ChooseOrganisationPage : PageObject
    {
        public ChooseOrganisationPage(Page page) : base(page)
        { }

        public override string PageTitle => "Choose organisation";
        public override string Url => Constants.EmployerBaseUrl + "/{accountId}/unapproved/legalEntity/create";
        private static string LegalEntityOption1 = "#LegalEntityId-7262";
        private static string ContinueButton = "#submit-legal-entity";

        public async Task GoToPage(string accountId)
        {
            var url = Url.Replace("{accountId}", accountId);

            await Page.GoToAsync(url);
        }

        public async Task SelectLegalEntity()
        {
            await Page.ClickOn(LegalEntityOption1);
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            return await Click<T>(ContinueButton);
        }
    }
}
