using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class ConfirmProviderPage : PageObject
    {
        public ConfirmProviderPage(Page page) : base(page)
        { }

        public override string PageTitle => "Confirm training provider";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/confirm-provider";

        public async Task SelectConfirmationOption()
        {
            await Page.ClickOn(UseThisProviderOption);
        }

        public async Task SelectChangeProviderOption()
        {
            await Page.ClickOn(ChangeProviderOption);
        }
    
        public async Task<AssignPage> ClickContinue()
        {
            await Page.ClickOn(ContinueButton);
            return PageObjectFactory.CreatePage<AssignPage>(Page);
        }

        private static string UseThisProviderOption = "#UseThisProvider";
        private static string ChangeProviderOption = "#UseThisProvider-no";
        private static string ContinueButton = "#continue-button";
    }
}
