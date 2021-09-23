using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class MessagePage : PageObject
    {
        public MessagePage(Page page) : base(page)
        { }

        public override string PageTitle => "Message for your training provider";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/message";


        public async Task ClickSend() //todo: confirmation page?
        {
            await Page.ClickOn(SendButton);
        }

        public async Task EnterMessage(string value)
        {
            await Page.TypeInputAsync(Message, value);
        }

        private static string Message = "#Message";
        private static string SendButton = "#continue-button";
    }
}
