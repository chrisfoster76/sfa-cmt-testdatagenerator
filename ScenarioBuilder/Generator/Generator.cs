using System;
using ScenarioBuilder.Builders;
using ScenarioBuilder.Helpers;
using ScenarioBuilder.Models;

namespace ScenarioBuilder.Generator
{
    public static class Generator
    {
        public static void Scenario_Empty_Db()
        {
        }

        public static void Scenario_Cohort_Sent_to_new_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.Provider)
                .WithLastAction(LastAction.Amend)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Cohort_Sent_to_existing_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.Provider)
                .WithLastAction(LastAction.Amend)
                .WithLastUpdatedByEmployer("Chris", "chrisfoster186@googlemail.com")
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Cohort_Approved_And_Sent_to_existing_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.Provider)
                .WithApprovals(Party.Employer)
                .WithLastAction(LastAction.Approve)
                .WithLastUpdatedByEmployer("Chris", "chrisfoster186@googlemail.com")
                .WithApprenticeships(1000);
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_Employer_Draft()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Employer)
                .WithLastAction(LastAction.None)
                .WithTransferSender(8194, "Mega Corp", null)
                .WithApprenticeships(10);
            builder.Build();
        }

        public static void Scenario_Cohort_Employer_Draft()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithDefaultEmployer()
                .WithParty(Party.Employer)
                .WithLastAction(LastAction.None)
                .WithApprenticeships(10);
            builder.Build();
        }

        public static void Scenario_Cohort_NonLevyEmployer_ReadyForApproval()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Employer)
                .WithApprovals(Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();
        }

        public static void Scenario_Cohort_Provider_ReadyForApproval()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithDefaultEmployer()
                .WithParty(Party.Provider)
                .WithApprovals(Party.Employer)
                .WithLastAction(LastAction.Amend)
                .WithApprenticeships(10);
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_Provider()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Provider)
                .WithApprovals(Party.Employer)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", null)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Pledge_Transfer_Cohort_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Provider)
                .WithApprovals(Party.Employer)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", null,1)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_NonLevy_Employer()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Employer)
                .WithApprovals(Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", null)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_NonLevy_Employer_Many_Cohorts()
        {
            CohortBuilder builder;

            for (var i = 0; i < 100; i++)
            {
                builder = new CohortBuilder();

                builder
                    .WithDefaultProvider()
                    .WithNonLevyEmployer()
                    .WithParty(Party.Employer)
                    .WithApprovals(Party.Provider)
                    .WithLastAction(LastAction.Approve)
                    .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder))
                    .WithMessages(10);
                builder.Build();
            }
        }

        public static void Scenario_Cohort_With_NonLevy_Employer_Draft()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Employer)
                .WithLastAction(LastAction.None)
                .WithApprenticeships(10);
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_Rejected_By_Sender()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.Employer)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Rejected)
                .WithApprenticeships(10);
            builder.Build();

        }

        public static void Scenario_Transfer_Cohort_Pending_With_Sender()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.TransferSender)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval, DateTime.UtcNow)
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Pending)
                .WithLastUpdatedByEmployer("Chris Foster Employer User", "chrisfoster186@googlemail.com")
                .WithLastUpdatedByProvider("Chris Foster Provider User", "chrisfoster186@googlemail.com")
                .WithApprenticeships(10);
            builder.Build();

        }

        public static void Scenario_Transfer_Cohort_Pending_With_Sender_With_FundingBands()
        {
            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithParty(Party.TransferSender)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp",
                    TransferApprovalStatus
                        .Pending) //todo: refactor this so that status is based on being with transfer sender
                .WithApprenticeships(10)
                .WithFundingCapWarning();
            builder.Build();
        }

        public static void Scenario_Fully_Approved_Cohort()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(1);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Cohort_FlexiJobAgency()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithFlexiJobAgencyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeshipDeliveryModel(DeliveryModel.FlexiJobAgency)
                .WithApprenticeships(1);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Cohort_Portable()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithDefaultEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeshipDeliveryModel(DeliveryModel.PortableFlexiJob)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder).WithPortableTrainingCourse());
            builder.Build();

        }


        public static void Scenario_Fully_Approved_Cohort_NonLevy_WithReservation()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithReservations()
                .WithApprenticeships(1);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Transfer_Cohort()
        {

            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider | Party.TransferSender)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Cohort_With_Provider_Removed_From_ROATP()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithProvider(99999999, "Bad Provider")
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Apprentices_With_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }


        public static void Scenario_Fully_Approved_Apprentices_Pending_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }




        public static void Scenario_Fully_Approved_Transfer_Apprentices_With_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }


        public static void Scenario_Fully_Approved_Transfer_Apprentices_Pending_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }

        public static void PriceDataLock()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        //.WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Price)
                );
            builder.Build();

        }

        public static void MulitplePriceDataLocks()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.MultiPrice)
                );
            builder.Build();

        }


        public static void CourseDataLock()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Course)
                );
            builder.Build();

        }

        public static void CourseDataLockWithoutDataLockSuccess()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Course)
                );
            builder.Build();

        }

        public static void DataLockDueToPriceChangeMidway()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.PriceChangeMidway)
                );
            builder.Build();

        }

        public static void PriceDataLockAndPendingChangeOfCircumstances()
        {
            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        //.WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Price)
                        .WithChangeOfCircumstances(Originator.Employer)
                );
            builder.Build();
        }

        public static void NonLevy_PendingChangeOfCircumstances_EmployerOriginated()
        {

            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithChangeOfCircumstances(Originator.Employer)
                );
            builder.Build();
        }

        public static void NonLevy_PendingChangeOfCircumstances_ProviderOriginated()
        {

            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithChangeOfCircumstances(Originator.Provider)
                );
            builder.Build();
        }


        public static void ReusingUln()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                        .WithEndOption(new DateTime(2019, 6, 23))
                        .WithStopOption(new DateTime(2018, 9, 12))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 9, 1))
                );
            builder.Build();
        }

        public static void TwoApprovedApprenticeshipsWithSameUln()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                        .WithEndOption(new DateTime(2019, 6, 1))
                        .WithStopOption(new DateTime(2018, 7, 1))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 9, 1))
                );
            builder.Build();
        }



        public static void ReusingUlnWhenPendingWithSender()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Pending)
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.Employer)
                .WithLastAction(LastAction.None)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 9, 1))
                );
            builder.Build();
        }

        public static void SingleApproved()
        {
            var builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder).WithStartOption(new DateTime(2018, 9, 1)));

            builder.Build();
        }
        
        public static void Single_Stopped_NonLevy()
        {
            var builder = new CohortBuilder();
            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithIsDraft(false)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2019, 6, 1))
                        .WithStopOption(new DateTime(2020, 03, 01)));

            builder.Build();
        }

        public static void Single_Complete_NonLevy()
        {
            var builder = new CohortBuilder();
            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithIsDraft(false)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Completed)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2019, 2, 1))
                        .WithCompletionOption(new DateTime(2020, 04, 01)));

            builder.Build();
        }

        public static void ManyApproved()
        {
            for (var i = 0; i < 500; i++)
            {
                var builder = new CohortBuilder();

                if (RandomHelper.GetRandomNumber(10) > 8)
                {
                    builder
                        .WithDefaultEmployerProvider()
                        .WithParty(Party.None)
                        .WithApprovals(Party.Employer | Party.Provider)
                        .WithLastAction(LastAction.Approve)
                        .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                        .WithApprenticeships(1);
                }
                else
                {
                    DataLockType datalockType = DataLockType.Course;
                    var r = RandomHelper.GetRandomNumber(3);
                    if (r == 1)
                    {
                        datalockType = DataLockType.Price;
                    }

                    if (r == 2)
                    {
                        datalockType = DataLockType.MultiPrice;
                    }

                    builder
                        .WithDefaultEmployerProvider()
                        .WithParty(Party.None)
                        .WithApprovals(Party.Employer | Party.Provider)
                        .WithLastAction(LastAction.Approve)
                        .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                        .WithApprenticeship(cohort =>
                            new ApprenticeshipBuilder(builder)
                                .WithDataLock(datalockType));
                }

                builder.Build();
            }
        }

        public static void ManyApprovedNonLevy()
        {
            for (var i = 0; i < 500; i++)
            {
                var builder = new CohortBuilder();

                if (RandomHelper.GetRandomNumber(10) > 8)
                {
                    builder
                        .WithNonLevyEmployer()
                        .WithDefaultProvider()
                        .WithParty(Party.None)
                        .WithApprovals(Party.Employer | Party.Provider)
                        .WithLastAction(LastAction.Approve)
                        .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                        .WithApprenticeships(1);
                }
                else
                {
                    DataLockType datalockType = DataLockType.Course;
                    var r = RandomHelper.GetRandomNumber(3);
                    if (r == 1)
                    {
                        datalockType = DataLockType.Price;
                    }

                    if (r == 2)
                    {
                        datalockType = DataLockType.MultiPrice;
                    }

                    builder
                        .WithNonLevyEmployer()
                        .WithDefaultProvider()
                        .WithParty(Party.None)
                        .WithApprovals(Party.Employer | Party.Provider)
                        .WithLastAction(LastAction.Approve)
                        .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                        .WithApprenticeship(cohort =>
                            new ApprenticeshipBuilder(builder)
                                .WithDataLock(datalockType));
                }

                builder.Build();
            }
        }


        public static void Apprentice_Stopped_And_Restarted_NonLevy()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                        .WithStopOption(new DateTime(2018, 6, 1))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                );
            builder.Build();
        }

        public static void Scenario_Very_Large_Cohort()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.Provider)
                .WithApprovals(Party.Employer)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(500);
            builder.Build();
        }

        public static void Scenario_Very_Large_Cohort_Ready_For_Employer()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithParty(Party.Employer)
                .WithApprovals(Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(500);
            builder.Build();
        }

        public static void ManyApproved_And_Very_Large_Cohort_Ready_For_Employer()
        {
            for (var i = 0; i < 15; i++)
            {
                var builder = new CohortBuilder();

                builder
                    .WithDefaultEmployerProvider()
                    .WithParty(Party.None)
                    .WithApprovals(Party.Employer | Party.Provider)
                    .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                    .WithApprenticeships(1000);
                builder.Build();

            }

            var builder2 = new CohortBuilder();

            builder2
                .WithDefaultEmployerProvider()
                .WithParty(Party.Employer)
                .WithApprovals(Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder2.Build();
        }


        public static void Scenario_Multiple_Approved_Apprenticeships_Employers_And_Providers()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithDefaultProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithProvider(10005077, "Train-U-Good Corporation")
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();
            builder
                .WithEmployer(30060, "06344082", "Rapid Logistics Co Ltd", "7EKPG7", 645, ApprenticeshipEmployerType.NonLevy)
                .WithDefaultProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();
            builder
                .WithEmployer(30060, "06344082", "Rapid Logistics Co Ltd", "7EKPG7", 645, ApprenticeshipEmployerType.NonLevy)
                .WithProvider(10005077, "Train-U-Good Corporation")
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();
        }

        public static void EmployerWithMultipleProviders()
        {
            CreateAFewApprovedApprenticesForProvider(10005077, "Train-U-Good Corporation");
            CreateAFewApprovedApprenticesForProvider(10005078, "Amazing Training Ltd");
            CreateAFewApprovedApprenticesForProvider(10005079, "Like a Pro Education Inc.");
        }

        public static void EmployerWithMultipleProvidersV2()
        {
            CreateAFewApprovedApprenticesForProvider(10005078, "Amazing Training Ltd");
            CreateAFewApprovedApprenticesForProvider(10005079, "Like a Pro Education Inc.");
        }

        private static void CreateAFewApprovedApprenticesForProvider(int providerId, string providerName)
        {
            var approvalDateTime = DateTime.UtcNow.Date.AddDays(-100);

            for (var i = 0; i < 3; i++)
            {
                var builder = new CohortBuilder();
                builder
                    .WithDefaultEmployer()
                    .WithProvider(providerId, providerName)
                    .WithParty(Party.None)
                    .WithApprovals(Party.Employer | Party.Provider)
                    .WithLastAction(LastAction.Approve)
                    .WithApprenticeshipPaymentStatus(PaymentStatus.Active, DataHelper.GetRandomDateTime())
                    .WithApprenticeships(3);
                builder.Build();
            }
        }


        public static void Apprentice_Stopped_NonLevy()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2019, 6, 1))
                        .WithStopOption(new DateTime(2020, 3, 1))
                );
            builder.Build();
        }

        public static void Apprentice_Stopped_NonLevy_Expired()
        {
            var course = new TrainingCourse { Id = "403-2-1", Title = "Food and Drink: Meat and Poultry Industry Skills" };

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2017, 5, 1))
                        .WithEndOption(new DateTime(2021, 1, 1))
                        .WithStopOption(new DateTime(2019, 1, 1))
                        .WithTrainingCourse(course)
                );
            builder.Build();
        }

        public static void Apprentice_Stopped_NonLevy_FundingCapChange()
        {
            var course = new TrainingCourse {Id = "176", Title = "Accident Repair Technician"};

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2018, 1, 1))
                        .WithStopOption(new DateTime(2020, 3, 1))
                        .WithTrainingCourse(course)
                        .WithCost(10000)
                );
            builder.Build();
        }


        public static void Apprentice_Stopped_NonLevy_Transfer()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider | Party.TransferSender)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2019, 6, 1))
                        .WithStopOption(new DateTime(2020, 3, 1))
                );
            builder.Build();
        }

        public static void Apprentice_Stopped_WithChangeOfEmployerRequest_NonLevy()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2019, 6, 1))
                        .WithStopOption(new DateTime(2020, 3, 1))
                        .WithChangeOfPartyRequest(ChangeOfPartyRequestType.ChangeEmployer)
                        
                );
            builder.Build();
        }

        public static void Apprentice_Stopped_Portable_WithChangeOfEmployerRequest_NonLevy()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Stopped)
                .WithApprenticeshipDeliveryModel(DeliveryModel.PortableFlexiJob)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(new DateTime(2019, 6, 1))
                        .WithStopOption(new DateTime(2020, 3, 1))
                        .WithChangeOfPartyRequest(ChangeOfPartyRequestType.ChangeEmployer)

                );
            builder.Build();
        }
    }
}
