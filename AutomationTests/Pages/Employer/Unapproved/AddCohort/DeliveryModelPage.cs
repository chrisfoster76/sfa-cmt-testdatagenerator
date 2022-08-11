using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class DeliveryModelPage : PageObject
    {
        public DeliveryModelPage(Page page) : base(page)
        { }

        public override string PageTitle => "Select delivery model";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/select-delivery-model";
        
        public async Task SelectFlexiJobAgencyDeliveryModel()
        {
            await Page.ClickOn(FlexiJobAgencyOption);
        }

        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(ContinueButton);
        }

        private static string FlexiJobAgencyOption = "#DeliveryModelFjaa";
        private static string ContinueButton = "#continue-button";
    }
}
