using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class AssignPage : PageObject
    {
        public AssignPage(Page page) : base(page)
        { }

        public override string PageTitle => "Start adding apprentices";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/assign";
        
        public async Task SelectIWillAddApprentices()
        {
            await Page.ClickOn(IWillAddApprenticesOption);
        }

        public async Task SelectProviderWillAddApprentices()
        {
            await Page.ClickOn(ProviderWillAddApprentices);
        }

        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(ContinueButton);
        }

        private static string IWillAddApprenticesOption = "#WhoIsAddingApprentices";
        private static string ProviderWillAddApprentices = "#WhoIsAddingApprentices-Provider";
        private static string ContinueButton = "#continue-button";
    }
}
