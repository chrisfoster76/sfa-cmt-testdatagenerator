using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class SelectProviderPage : PageObject
    {
        public SelectProviderPage(Page page) : base(page)
        { }

        public override string PageTitle => "Add training provider details";
        public override string Url => Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/select-provider?AccountLegalEntityHashedId={accountLegalEntityId}&transferSenderId={transferSenderId}";
        private static string ProviderId = "[name=\"ProviderId\"]";
        private static string ContinueButton = "#continue-button";

        public async Task GoToPage(string accountId, string accountLegalEntityId, string transferSenderId)
        {
            var url = Url
                .Replace("{accountId}", accountId)
                .Replace("{accountLegalEntityId}", accountLegalEntityId)
                .Replace("{transferSenderId}", transferSenderId);

            await Page.GoToAsync(url);
        }

        public async Task EnterProviderId(string value)
        {
            await Page.TypeInputAsync(ProviderId, value);
        }

        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(ContinueButton);
        }
    }
}
