﻿using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class SelectStandardPage : PageObject
    {
        public SelectStandardPage(Page page) : base(page)
        { }

        public override string PageTitle => "Select standard";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/add/select-course";


        public async Task<T> ClickContinue<T>() where T:PageObject
        {
            await Page.ClickOn(ContinueButton);
            return PageObjectFactory.CreatePage<T>(Page) ;
        }

        private static string ContinueButton = "#continue-button";
    }
}
