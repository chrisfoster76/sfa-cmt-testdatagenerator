using System;

namespace ScenarioBuilder.Models
{
    public class ChangeOfPartyRequest
    {
        public long Id { get; set; }
        public long ApprenticeshipId { get; set; }
        public ChangeOfPartyRequestType ChangeOfPartyType { get; set; }
        public Party OriginatingParty { get; set; }
        public long? AccountLegalEntityId { get; set; }
        public long? ProviderId { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public ChangeOfPartyRequestStatus Status { get; set; }
    }
}
