using System;
using System.Collections.Generic;

namespace ScenarioBuilder.Models
{
    public class Commitment
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public long EmployerAccountId { get; set; }
        public long AccountLegalEntityId { get; set; }
        public string LegalEntityId { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityAddress { get; set; }
        public int LegalEntityOrganisationType { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public CommitmentStatus CommitmentStatus { get; set; } = CommitmentStatus.Active;
        public EditStatus EditStatus { get; set; }
        public Party WithParty { get; set; }
        public Party Approvals { get; set; }
        public DateTime? EmployerAndProviderApprovedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public LastAction LastAction { get; set; } = LastAction.Amend;
        public bool IsDraft { get; set; }
        public string LastUpdatedByEmployerName { get; set; }
        public string LastUpdatedByEmployerEmail { get; set; }
        public string LastUpdatedByProviderName { get; set; }

        public string LastUpdatedByProviderEmail { get; set; }
        public long? TransferSenderId { get; set; }
        public string TransferSenderName { get; set; }
        public TransferApprovalStatus? TransferApprovalStatus { get; set; }
        public int? PledgeApplicationId { get; set; }
        public DateTime? TransferApprovalActionedOn { get; set; }
        public string AccountLegalEntityPublicHashedId { get; set; }

        public ApprenticeshipEmployerType? ApprenticeshipEmployerTypeOnApproval { get; set; } //todo: this gets set for pre-approval records, which does not simulate reality

        public List<Apprenticeship> Apprenticeships { get; set; }
        public List<Message> Messages { get; set; }
    }
}
