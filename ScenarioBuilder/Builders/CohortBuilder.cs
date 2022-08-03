using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ScenarioBuilder.Helpers;
using ScenarioBuilder.Models;

namespace ScenarioBuilder.Builders
{
    public class CohortBuilder
    {
        public long Id => _commitment.Id;
        public bool IsPaidByTransfer => _commitment.TransferSenderId.HasValue;
        public bool HasFundingCapWarning { get; set; }

        public AgreementStatus AgreementStatus { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public DeliveryModel DeliveryModel { get; private set; } = DeliveryModel.Regular;
        public string TrainingCourse { get; private set; }
        public bool HasReservations { get; private set; } = true;
        public int Messages { get; private set; }
        public DateTime? AgreedOnDate { get; private set; }
        
        private readonly Commitment _commitment;
        private readonly List<ApprenticeshipBuilder> _apprenticeshipBuilders;

        public CohortBuilder()
        {
            var id = IdentityHelpers.GetNextCohortId();

            Messages = 1;

            //todo: most of this should be pushed into WithXXX methods
            _commitment = new Commitment
            {
                Id = id,
                Reference = HashingHelper.Encode(id),
                CommitmentStatus = CommitmentStatus.Active,
                EditStatus = EditStatus.Employer,
                CreatedOn = System.DateTime.Now,
                LastAction = LastAction.None,
                TransferSenderId = null,
                TransferSenderName = "",
                TransferApprovalStatus = null,
                LastUpdatedByEmployerName = "",
                LastUpdatedByEmployerEmail = "",
                LastUpdatedByProviderName = "",
                LastUpdatedByProviderEmail = ""
            };

            _apprenticeshipBuilders = new List<ApprenticeshipBuilder>();
        }

        public CohortBuilder WithDefaultEmployerProvider()
        {
            _commitment.EmployerAccountId = 8194;
            _commitment.LegalEntityId = "736281";
            _commitment.LegalEntityName = "MegaCorp Pharmaceuticals";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "XEGE5X";
            _commitment.ApprenticeshipEmployerTypeOnApproval = ApprenticeshipEmployerType.Levy;
            _commitment.AccountLegalEntityId = 2817;
            _commitment.ProviderId = 10005077;
            _commitment.ProviderName = "Train-U-Good Corporation";

            return this;
        }

        public CohortBuilder WithDefaultEmployer()
        {
            _commitment.EmployerAccountId = 8194;
            _commitment.LegalEntityId = "736281";
            _commitment.LegalEntityName = "MegaCorp Pharmaceuticals";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "XEGE5X";
            _commitment.AccountLegalEntityId = 2817;
            _commitment.ApprenticeshipEmployerTypeOnApproval = ApprenticeshipEmployerType.Levy;
            return this;
        }

        public CohortBuilder WithNonLevyEmployer()
        {
            _commitment.EmployerAccountId = 30060;
            _commitment.LegalEntityId = "06344082";
            _commitment.LegalEntityName = "Rapid Logistics Co Ltd";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "X9JE72";
            _commitment.AccountLegalEntityId = 645;
            _commitment.ApprenticeshipEmployerTypeOnApproval = ApprenticeshipEmployerType.NonLevy;
            return this;
        }

        public CohortBuilder WithFlexiJobAgencyEmployer()
        {
            _commitment.EmployerAccountId = 36853;
            _commitment.LegalEntityId = "70110101";
            _commitment.LegalEntityName = "Positivity Ltd (FJAA)";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "XKD5Z2";
            _commitment.AccountLegalEntityId = 701;
            _commitment.ApprenticeshipEmployerTypeOnApproval = ApprenticeshipEmployerType.NonLevy;
            return this;
        }

        public CohortBuilder WithDefaultProvider()
        {
            _commitment.ProviderId = 10005077;
            _commitment.ProviderName = "Train-U-Good Corporation";
            return this;
        }

        public CohortBuilder WithEmployer(long accountId, string legalEntityId, string name, string accountLegalEntityPublicHashedId, long accountLegalEntityId, ApprenticeshipEmployerType apprenticeshipEmployerType)
        {
            _commitment.EmployerAccountId = accountId;
            _commitment.LegalEntityId = legalEntityId;
            _commitment.LegalEntityName = name;
            _commitment.LegalEntityAddress = "Some address";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = accountLegalEntityPublicHashedId;
            _commitment.AccountLegalEntityId = accountLegalEntityId;
            _commitment.ApprenticeshipEmployerTypeOnApproval = apprenticeshipEmployerType;
            return this;
        }

        public CohortBuilder WithProvider(int providerId, string providerName)
        {
            _commitment.ProviderId = providerId;
            _commitment.ProviderName = providerName;
            return this;
        }

        public CohortBuilder WithParty(Party assignedParty)
        {
            _commitment.WithParty = assignedParty;

            if (assignedParty == Party.Employer)
            {
                _commitment.EditStatus = EditStatus.Employer;
            }
            else if(assignedParty == Party.Provider)
            {
                _commitment.EditStatus = EditStatus.Provider;
            }
            else
            {
                _commitment.EditStatus = EditStatus.Both;
            }

            return this;
        }

        public CohortBuilder WithTransferSender(long transferSenderId, string transferSenderName,
            TransferApprovalStatus? transferApprovalStatus, int? pledgeApplicationId = null)
        {
            _commitment.TransferSenderId = transferSenderId;
            _commitment.TransferSenderName = transferSenderName;
            _commitment.TransferApprovalStatus = transferApprovalStatus;
            _commitment.PledgeApplicationId = pledgeApplicationId;

            return this;
        }

        public CohortBuilder WithApprenticeships(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _apprenticeshipBuilders.Add(new ApprenticeshipBuilder(this));
            }
            return this;
        }

        public CohortBuilder WithApprenticeship(Func<CohortBuilder,ApprenticeshipBuilder> apprenticeshipBuilder)
        {
            var builder = apprenticeshipBuilder.Invoke(this);
            _apprenticeshipBuilders.Add(builder);
            return this;
        }

        public CohortBuilder WithFundingCapWarning()
        {
            HasFundingCapWarning = true;
            return this;
        }

        public CohortBuilder WithReservations()
        {
            HasReservations = true;
            return this;
        }

        public CohortBuilder WithoutReservations()
        {
            HasReservations = false;
            return this;
        }

        public CohortBuilder WithApprenticeshipPaymentStatus(PaymentStatus status, DateTime? approvalDate = null)
        {
            PaymentStatus = status;
            AgreedOnDate = approvalDate.HasValue ? approvalDate.Value : default(DateTime?);
            return this;
        }

        public CohortBuilder WithApprenticeshipDeliveryModel(DeliveryModel deliveryModel)
        {
            DeliveryModel = deliveryModel;
            return this;
        }

        public Commitment Build()
        {
            //todo: why hold a list?
            _commitment.Apprenticeships = new List<Apprenticeship>();

            foreach (var apprenticeshipBuilder in _apprenticeshipBuilders)
            {
                var apprenticeship = apprenticeshipBuilder.Build();
                _commitment.Apprenticeships.Add(apprenticeship);
            }

            DbHelper.SaveCommitment(_commitment);

            foreach (var apprenticeship in _commitment.Apprenticeships)
            {
                DbHelper.SaveApprenticeship(apprenticeship);


                foreach (var datalock in apprenticeship.DataLocks)
                {
                    DbHelper.SaveDataLock(apprenticeship, datalock);
                }

                if (apprenticeship.HasChangeOfCircumstances)
                {
                    DbHelper.CreateChangeOfCircumstances(apprenticeship, apprenticeship.ChangeOfCircumstancesOriginator);
                }

                foreach (var cop in apprenticeship.ChangeOfPartyRequests)
                {
                    DbHelper.SaveChangeOfPartyRequest(cop);
                }
            }

            if (_commitment.TransferApprovalStatus.HasValue)
            {
                DbHelper.CreateTransferRequest(_commitment);
            }

            if (Messages > 0)
            {
                DbHelper.CreateMessages(_commitment, Messages);
            }

            return _commitment;
        }

        public CohortBuilder WithLastAction(LastAction lastAction)
        {
            _commitment.LastAction = lastAction;
            _commitment.IsDraft = lastAction == LastAction.None;
            return this;
        }

        public CohortBuilder WithIsDraft(bool isDraft)
        {
            _commitment.IsDraft = isDraft;
            _commitment.LastAction = LastAction.Amend; //arbitrary value
            return this;
        }

        public CohortBuilder WithLastUpdatedByProvider(string providerUser, string providerUserEmail)
        {
            _commitment.LastUpdatedByProviderName = providerUser;
            _commitment.LastUpdatedByProviderEmail = providerUserEmail;
            return this;
        }

        public CohortBuilder WithLastUpdatedByEmployer(string employerUser, string employerUserEmail)
        {
            _commitment.LastUpdatedByEmployerName = employerUser;
            _commitment.LastUpdatedByEmployerEmail = employerUserEmail;
            return this;
        }

        public CohortBuilder WithMessages(int numberOfMessages)
        {
            Messages = numberOfMessages;
            return this;
        }

        public CohortBuilder WithApprovals(Party approvingParties)
        {
            _commitment.Approvals = approvingParties;

            if (approvingParties.HasFlag(Party.Employer) && approvingParties.HasFlag(Party.Provider))
            {
                _commitment.EmployerAndProviderApprovedOn = DateTime.UtcNow;
            }

            if (approvingParties.HasFlag(Party.Employer) && approvingParties.HasFlag(Party.Provider))
            {
                _commitment.EditStatus = EditStatus.Both;
            }

            return this;
        }
    }
}
