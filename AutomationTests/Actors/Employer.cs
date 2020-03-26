﻿using System;
using ScenarioBuilder.Helpers;

namespace AutomationTests.Actors
{
    public class Employer
    {
        public long AccountId { get; private set; }
        public string EncodedAccountId => HashingHelper.Encode(AccountId);
        public long AccountLegalEntityId { get; private set; }
        public string EncodedAccountLegalEntityId => HashingHelper.EncodeAccountLegalEntityId(AccountLegalEntityId);
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsLevyPayer { get; private set; }
        /// <summary>
        /// Indicates an encoded transfer sender id for use for this employer. Used due to simulation of transfer selection in Add journey
        /// </summary>
        public string TransferSenderId { get; private set; }

        public static Employer Create(EmployerActor actorType)
        {
            Console.WriteLine($"Creating Employer: {actorType}");

            switch (actorType)
            {
                case EmployerActor.NonLevyEmployer:
                    return new Employer
                    {
                        AccountId = 30060,
                        AccountLegalEntityId = 645,
                        Username = "chrisfoster186+test2@googlemail.com",
                        Password = "DevTestingPassword123",
                        IsLevyPayer = false,
                        TransferSenderId = "7YRV9B"
                    };
                case EmployerActor.LevyEmployer:
                    return new Employer
                    {
                        AccountId = 8194,
                        AccountLegalEntityId = 2818,
                        Username = "foster186@hotmail.com",
                        Password = "DevTestingPassword123",
                        IsLevyPayer = true,
                        TransferSenderId = ""
                    };
                default:
                    throw new ArgumentException();
            }
        }
    }
}
