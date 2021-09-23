using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PuppeteerSharp;

namespace AutomationTests.Pages
{
    public abstract class PageObject
    {
        protected Page Page { get; set; }
        public abstract string PageTitle { get; }
        public abstract string Url { get; }

        protected PageObject(Page page)
        {
            Page = page;
        }

        #region Common properties and methods
        private readonly string BackLink = "#back-link";
        public virtual async Task<T> ClickBack<T>() where T : PageObject
        {
            return await Click<T>(BackLink);
        }

        public async Task<T> Click<T>(string selector) where T : PageObject
        {
            await Page.ClickOn(selector);
            var result = PageObjectFactory.CreatePage<T>(Page);

            await VerifyPageTitle(result);
            
            //todo: assert url is as expected (excluding tokens, but capture those in a dictionary for assessment in the test)
            Console.WriteLine($"{Page.Url} => {result.Url}");

            return result;
        }

        public virtual async Task<T> Redirect<T>() where T : PageObject
        {
            var result = PageObjectFactory.CreatePage<T>(Page);
            await VerifyPageTitle(result);
            return result;
        }

        #endregion

        private async Task VerifyPageTitle(PageObject result)
        {
            if (!string.IsNullOrWhiteSpace(result.PageTitle))
            {
                var actualTitle = await Page.GetTitleAsync();

                if (Constants.TreatPageTitleMismatchAsError)
                {
                    Assert.AreEqual(result.PageTitle, actualTitle, $"Page title did not match - {result.GetType().Name}");
                }
                else
                {
                    if (result.PageTitle != actualTitle)
                    {
                        Console.WriteLine($"Warning: Page title did not match - {result.GetType().Name}");
                    }
                }
            }
        }
    }
}
