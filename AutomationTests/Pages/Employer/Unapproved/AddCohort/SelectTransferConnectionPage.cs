using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class SelectTransferConnectionPage : PageObject
    {
        public SelectTransferConnectionPage(Page page) : base(page)
        { }

        public override string PageTitle => "Do you want to use transfer funds to pay for this training?";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/transferConnection/create";

        public async Task SelectNo()
        {
            await Page.ClickOn(NoTransferConnection);
        }

        public async Task SelectYes()
        {
            await Page.ClickOn(TransferConnection);
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            await Page.ClickOn(ContinueButton);
            return PageObjectFactory.CreatePage<T>(Page);
        }

        private static string ContinueButton = "#submit-transfer-connection";

        private static string NoTransferConnection = "#TransferConnection-None";

        private static string TransferConnection = "#TransferConnection-7YRV9B";
    }
}
