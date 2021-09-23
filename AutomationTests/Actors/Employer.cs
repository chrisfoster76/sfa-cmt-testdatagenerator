using System;
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
        public bool IsLevyPayer { get; private set; }
        public bool HasMultipleLegalEntities { get; private set; }
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
                        Username = "employer-nonlevy-user",
                        IsLevyPayer = false,
                        TransferSenderId = "7YRV9B"
                    };
                case EmployerActor.LevyEmployer:
                    return new Employer
                    {
                        AccountId = 8194,
                        AccountLegalEntityId = 2818,
                        Username = "employer-user",
                        IsLevyPayer = true,
                        TransferSenderId = "",
                        HasMultipleLegalEntities = true
                    };
                default:
                    throw new ArgumentException();
            }
        }
    }
}
