using System;
using System.Threading.Tasks;
using AutomationTests.Actors;
using AutomationTests.Pages.Employer.Unapproved;
using AutomationTests.Pages.Employer.Unapproved.AddCohort;
using NUnit.Framework;

namespace AutomationTests.Employer.Cohort
{
    [TestFixture]
    public class CreateCohortTests : EmployerTestBase
    {
        [TestCase(EmployerActor.NonLevyEmployer, false)]
        [TestCase(EmployerActor.NonLevyEmployer, true)]
        [TestCase(EmployerActor.LevyEmployer, false)]

        public async Task CreateCohort(EmployerActor employerActor, bool withTransferSender)
        {
            var employer = Actors.Employer.Create(employerActor);

            //Journey begins at legal entity selection page (for all intents and purposes)
            var chooseOrganisationPage = new ChooseOrganisationPage(Page);
            await chooseOrganisationPage.GoToPage(employer.EncodedAccountId);

            SelectProviderPage selectProviderPage;

            if (employer.HasMultipleLegalEntities)
            {
                await chooseOrganisationPage.SelectLegalEntity();
                selectProviderPage = await chooseOrganisationPage.ClickContinue<SelectProviderPage>();
            }
            else
            {
                //if only one legal entity, system will have automatically redirected to select provider page
                selectProviderPage = await chooseOrganisationPage.Redirect<SelectProviderPage>();
            }

            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectIWillAddApprentices();

            ApprenticePage apprentice;

            //Non-levy must select a reservation at this point, unless they have a transfer sender
            if (!employer.IsLevyPayer && !withTransferSender)
            {
                var selectReservation = await assign.ClickContinue<SelectReservationPage>();
                apprentice = await selectReservation.ClickContinue<ApprenticePage>();
            }
            else
            {
                apprentice = await assign.ClickContinue<ApprenticePage>();
            }

            //Apprentice
            await apprentice.EnterFirstName("Chris");
            await apprentice.EnterLastName("Foster");
            await apprentice.ClickContinue<CohortDetailsPage>();


            //Assertions
        }

        [TestCase(EmployerActor.NonLevyEmployer, false)]
        [TestCase(EmployerActor.NonLevyEmployer, true)]
        [TestCase(EmployerActor.LevyEmployer, false)]
        public async Task CreateCohortWithProvider(EmployerActor employerActor, bool withTransferSender)
        {
            var employer = Actors.Employer.Create(employerActor);

            //Journey begins in v1, but jumps to v2 select provider page, so let's start here
            var selectProviderPage = new SelectProviderPage(Page);
            await selectProviderPage.GoToPage(employer.EncodedAccountId, employer.EncodedAccountLegalEntityId,
                withTransferSender ? employer.TransferSenderId : "");

            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectProviderWillAddApprentices();
            var message = await assign.ClickContinue<MessagePage>();
            
            //Apprentice
            await message.EnterMessage("Hey there!");
            await message.ClickSend();

            //Assertions
        }
    }

}
