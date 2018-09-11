﻿using System.Collections.Generic;

namespace CommitmentsDataGen.Generator
{
    public static class Scenarios
    {
        private static readonly List<Scenario> _scenarios;

        static Scenarios()
        {
            _scenarios = new List<Scenario>
            {
                new Scenario("Empty database", Generator.Scenario_Empty_Db),
                new Scenario("Scenario_Cohort_Sent_to_new_Provider", Generator.Scenario_Cohort_Sent_to_new_Provider),
                new Scenario("Scenario_Cohort_Sent_to_existing_Provider", Generator.Scenario_Cohort_Sent_to_existing_Provider),
                new Scenario("Transfer Draft Cohort with Employer", Generator.Scenario_Transfer_Cohort_Employer_Draft),
                new Scenario("Transfer Cohort Rejected by Sender", Generator.Scenario_Transfer_Cohort_Rejected_By_Sender),
                new Scenario("Transfer Cohort Pending with Sender", Generator.Scenario_Transfer_Cohort_Pending_With_Sender),
                new Scenario("Scenario_Transfer_Cohort_Pending_With_Sender_With_FundingBands", Generator.Scenario_Transfer_Cohort_Pending_With_Sender_With_FundingBands),
                new Scenario("Fully Approved Cohort", Generator.Scenario_Fully_Approved_Cohort),
                new Scenario("Fully Approved Transfers Cohort", Generator.Scenario_Fully_Approved_Transfer_Cohort),
                new Scenario("Approved Apprentices (Live and Waiting to Start) with Data Lock success", Generator.Scenario_Fully_Approved_Apprentices_With_DataLock_Success),
                new Scenario("Approved Apprentices (Live and Waiting to Start) pending Data Lock success", Generator.Scenario_Fully_Approved_Apprentices_Pending_DataLock_Success),
                new Scenario("Approved Transfer Apprentices (Live and Waiting to Start) with Data Lock success", Generator.Scenario_Fully_Approved_Transfer_Apprentices_With_DataLock_Success),
                new Scenario("Approved Transfer Apprentices (Live and Waiting to Start) pending Data Lock success", Generator.Scenario_Fully_Approved_Transfer_Apprentices_Pending_DataLock_Success),
                new Scenario("Scenario_Fully_Approved_Cohort_With_Provider_Removed_From_ROATP", Generator.Scenario_Fully_Approved_Cohort_With_Provider_Removed_From_ROATP),
                new Scenario("PriceDataLock", Generator.PriceDataLock),
                new Scenario("CourseDataLock", Generator.CourseDataLock),
                new Scenario("MulitplePriceDataLocks", Generator.MulitplePriceDataLocks),
                new Scenario("Price Change through DataLock", Generator.DataLockDueToPriceChangeMidway),
                new Scenario("Reusing a ULN", Generator.ReusingUln),
                new Scenario("Reusing a ULN when it is pending with transfer sender", Generator.ReusingUlnWhenPendingWithSender),
                new Scenario("Two Approved Apprenticeships With Same Uln", Generator.TwoApprovedApprenticeshipsWithSameUln),
                new Scenario("A great many approved cohorts", Generator.ManyApproved),
                new Scenario("A very large cohort ready for provider to approve", Generator.Scenario_Very_Large_Cohort),
                new Scenario("A very large cohort ready for employer to approve", Generator.Scenario_Very_Large_Cohort_Ready_For_Employer),
                new Scenario("Scenario_Multiple_Approved_Apprenticeships_Employers_And_Providers", Generator.Scenario_Multiple_Approved_Apprenticeships_Employers_And_Providers),
                new Scenario("ManyApproved_And_Very_Large_Cohort_Ready_For_Employer", Generator.ManyApproved_And_Very_Large_Cohort_Ready_For_Employer),
                new Scenario("Investigating end date bug", Generator.Investiating_EndDate_Bug)
            };
        }

        public static List<Scenario> GetScenarios()
        {
            return _scenarios;
        }
    }
}
