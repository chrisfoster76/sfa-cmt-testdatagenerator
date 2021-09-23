using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class ApprenticePage : PageObject
    {
        public ApprenticePage(Page page) : base(page)
        { }

        public override string PageTitle => "Add apprentice details";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/apprentice";


        public async Task<T> ClickContinue<T>() where T:PageObject
        {
            await Page.ClickOn(ContinueButton);
            return PageObjectFactory.CreatePage<T>(Page) as T;
        }

        public async Task EnterFirstName(string value)
        {
            await Page.OvertypeInputAsync(FirstName, value);
        }

        public async Task EnterLastName(string value)
        {
            await Page.OvertypeInputAsync(LastName, value);
        }

        private static string FirstName = "#FirstName";
        private static string LastName = "#LastName";
        private static string ContinueButton = "#continue-button";
    }
}
