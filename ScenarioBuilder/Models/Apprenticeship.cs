using System;
using System.Collections.Generic;

namespace ScenarioBuilder.Models
{

    public class Apprenticeship
    {
        public long Id { get; set; }

        public long CommitmentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string ULN { get; set; }
        public int TrainingType { get; set; }
        public string TrainingCode { get; set; }
        public string TrainingName { get; set; }
        public decimal Cost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public AgreementStatus AgreementStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string NINumber { get; set; }
        public string EmployerRef { get; set; }
        public string ProviderRef { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? AgreedOn { get; set; }
        public int? PaymentOrder { get; set; }
        public DateTime? StopDate { get; set; }
        public DateTime? PauseDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool HasHadDataLockSuccess { get; set; }

        public List<DataLock> DataLocks { get; set; }
        public List<ChangeOfPartyRequest> ChangeOfPartyRequests { get; set; }
        public bool HasChangeOfCircumstances { get; set; }
        public Originator ChangeOfCircumstancesOriginator { get; set; }

        public Originator? PendingUpdateOriginator
        {
            get
            {
                if (!HasChangeOfCircumstances) return null;
                return ChangeOfCircumstancesOriginator;
            }
        }

        public Guid? ReservationId { get; set; }

        public Apprenticeship()
        {
            DataLocks = new List<DataLock>();
            ChangeOfPartyRequests = new List<ChangeOfPartyRequest>();
        }
    }
}