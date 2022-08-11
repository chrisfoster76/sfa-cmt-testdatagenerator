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


            var informPage = new InformPage(Page);
            await informPage.GoToPage(employer.EncodedAccountId);

            SelectProviderPage selectProviderPage = new SelectProviderPage(Page); //todo: should not have to set this, should always be set below
            SelectTransferConnectionPage transferConnectionPage;


            if (withTransferSender)
            {
                transferConnectionPage = await informPage.ClickContinue<SelectTransferConnectionPage>();
                await transferConnectionPage.SelectYes();

                if (employer.HasMultipleLegalEntities)
                {
                    var chooseOrganisationPage = await transferConnectionPage.ClickContinue<ChooseOrganisationPage>();
                    await chooseOrganisationPage.SelectLegalEntity();
                    selectProviderPage = await chooseOrganisationPage.ClickContinue<SelectProviderPage>();
                }
                else
                {
                    //if only one legal entity, system will have automatically redirected to select provider page
                    var chooseOrganisationPage = await transferConnectionPage.ClickContinue<ChooseOrganisationPage>();
                    selectProviderPage = await chooseOrganisationPage.Redirect<SelectProviderPage>();
                }
            }
            

            if(!employer.IsLevyPayer && !withTransferSender)
            {
                transferConnectionPage = await informPage.ClickContinue<SelectTransferConnectionPage>();
                await transferConnectionPage.SelectNo();

                if (employer.HasMultipleLegalEntities)
                {
                    var chooseOrganisationPage = await transferConnectionPage.ClickContinue<ChooseOrganisationPage>();
                    await chooseOrganisationPage.SelectLegalEntity();
                    selectProviderPage = await chooseOrganisationPage.ClickContinue<SelectProviderPage>();
                }
                else
                {
                    //if only one legal entity, system will have automatically redirected to select provider page
                    var chooseOrganisationPage = await transferConnectionPage.ClickContinue<ChooseOrganisationPage>();
                    selectProviderPage = await chooseOrganisationPage.Redirect<SelectProviderPage>();
                }

            }


            if (employer.IsLevyPayer)
            {
                if (employer.HasMultipleLegalEntities)
                {
                    var chooseOrganisationPage = await informPage.ClickContinue<ChooseOrganisationPage>();
                    await chooseOrganisationPage.SelectLegalEntity();
                    selectProviderPage = await chooseOrganisationPage.ClickContinue<SelectProviderPage>();
                }
                else
                {
                    //if only one legal entity, system will have automatically redirected to select provider page
                    var chooseOrganisationPage = await informPage.ClickContinue<ChooseOrganisationPage>();
                    selectProviderPage = await chooseOrganisationPage.Redirect<SelectProviderPage>();
                }
            }


            
            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectIWillAddApprentices();

            SelectStandardPage selectStandard;

            //Non-levy must select a reservation at this point, unless they have a transfer sender
            if (!employer.IsLevyPayer && !withTransferSender)
            {
                var selectReservation = await assign.ClickContinue<SelectReservationPage>();
                selectStandard = await selectReservation.ClickContinue<SelectStandardPage>();
            }
            else
            {
                selectStandard = await assign.ClickContinue<SelectStandardPage>();
            }

            await selectStandard.SelectStandard();
            var apprentice = await selectStandard.ClickContinue<ApprenticePage>();

            //Apprentice
            await apprentice.EnterFirstName("Chris");
            await apprentice.EnterLastName("Foster");
            await apprentice.ClickContinue<CohortDetailsPage>();
        }

        [TestCase(EmployerActor.LevyEmployer, false)]
        public async Task CreateCohortWithProvider(EmployerActor employerActor, bool withTransferSender)
        {
            var employer = Actors.Employer.Create(employerActor);

            var informPage = new InformPage(Page);
            await informPage.GoToPage(employer.EncodedAccountId);

            var chooseOrganisationPage = await informPage.ClickContinue<ChooseOrganisationPage>();

            await chooseOrganisationPage.SelectLegalEntity();
            var selectProviderPage = await chooseOrganisationPage.ClickContinue<SelectProviderPage>();

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
        }


        [TestCase(EmployerActor.NonLevyFlexiJobAgencyEmployer)]
        public async Task FlexiJobAgencyCreateCohort(EmployerActor employerActor)
        {
            var employer = Actors.Employer.Create(employerActor);


            var informPage = new InformPage(Page);
            await informPage.GoToPage(employer.EncodedAccountId);

            //if only one legal entity, system will have automatically redirected to select provider page
            var selectProviderPage = await informPage.ClickContinue<SelectProviderPage>();

            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectIWillAddApprentices();

            //Non-levy must select a reservation at this point, unless they have a transfer sender
            var selectReservation = await assign.ClickContinue<SelectReservationPage>();
            var selectStandard = await selectReservation.ClickContinue<SelectStandardPage>();

            await selectStandard.SelectStandard();
            var deliveryModelPage = await selectStandard.ClickContinue<DeliveryModelPage>();

            await deliveryModelPage.SelectFlexiJobAgencyDeliveryModel();
            var apprentice = await deliveryModelPage.ClickContinue<ApprenticePage>();

            //Apprentice
            await apprentice.EnterFirstName("Chris");
            await apprentice.EnterLastName("Foster");
            await apprentice.ClickContinue<CohortDetailsPage>();
        }
    }

}
