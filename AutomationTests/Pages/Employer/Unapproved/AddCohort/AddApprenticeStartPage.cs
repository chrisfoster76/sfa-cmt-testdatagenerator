using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class AddApprenticeStartPage : PageObject
    {
        public AddApprenticeStartPage(Page page) : base(page)
        { }

        private static string StartNowButton = "#continue-button";

        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(StartNowButton);
        }

        public override string PageTitle => "Add an apprentice";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add";
    }
}
