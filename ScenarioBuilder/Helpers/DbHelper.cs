using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using Dapper;
using Newtonsoft.Json;
using ScenarioBuilder.Models;

namespace ScenarioBuilder.Helpers
{
    public static class DbHelper
    {
        private static DbConnection GetConnection()
        {
            var connectionString = ConfigurationHelper.Configuration.DbConnectionString;
            return new SqlConnection(connectionString);
        }

        public static void ClearDb()
        {
            var connection = GetConnection();

            connection.Execute("delete from DataLockStatus");
            connection.Execute("delete from ApprenticeshipUpdate");
            connection.Execute("delete from TransferRequest");
            connection.Execute("delete from CustomProviderPaymentPriority");
            connection.Execute("delete from History");
            connection.Execute("delete from PriceHistory");
            connection.Execute("delete from Message");
            connection.Execute("delete from ChangeOfPartyRequest");
            connection.Execute("delete from Apprenticeship");
            connection.Execute("delete from Commitment");
            connection.Execute("delete from OutboxData");
            connection.Execute("delete from ClientOutboxData");

            connection.Execute("DBCC CHECKIDENT ('[Commitment]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[Apprenticeship]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[DataLockStatus]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[TransferRequest]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[ApprenticeshipUpdate]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[PriceHistory]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[Message]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[ChangeOfPartyRequest]', RESEED, 0);");
        }

        public static void SaveCommitment(Commitment cohort)
        {
            var connection = GetConnection();
            var query = FileHelper.GetSql("SaveCommitment");
            var result = connection.Execute(query, cohort);
        }

        public static void SaveApprenticeship(Apprenticeship apprenticeship)
        {
            var connection = GetConnection();

            var query = "insert into Apprenticeship " +
                        "([CommitmentId],[FirstName],[LastName],[ULN],[TrainingType],[TrainingCode],[TrainingName],[Cost]" +
                        ",[StartDate],[EndDate],[AgreementStatus],[PaymentStatus],[DateOfBirth],[NINumber],[EmployerRef]" +
                        ",[ProviderRef],[CreatedOn],[AgreedOn],[PaymentOrder],[StopDate],[PauseDate],[HasHadDataLockSuccess],[PendingUpdateOriginator],[ReservationId],[CompletionDate])" +
                        "VALUES(" +
                        "@CommitmentId,@FirstName,@LastName, @ULN, @TrainingType, @TrainingCode,@TrainingName, @Cost,@StartDate," +
                        "@EndDate,@AgreementStatus,@PaymentStatus,@DateOfBirth,@NINumber, @EmployerRef, @ProviderRef, @CreatedOn, @AgreedOn, " +
                        "@PaymentOrder, @StopDate, @PauseDate, @HasHadDataLockSuccess,@PendingUpdateOriginator,@ReservationId,@CompletionDate)";

            var result = connection.Execute(query, apprenticeship);

            //price history
            if (apprenticeship.PaymentStatus != PaymentStatus.PendingApproval)
            {
                var query2 = "insert into PriceHistory ([ApprenticeshipId],[Cost],[FromDate]) " +
                        "VALUES (@ApprenticeshipId,@Cost,@FromDate)";

                var result2 = connection.Execute(query2, new
                {
                    ApprenticeshipId = apprenticeship.Id,
                    Cost = apprenticeship.Cost,
                    FromDate = apprenticeship.StartDate
                });
            }
        }

        //public static void CalculatePaymentOrders(long accountId)
        //{
        //    var connection = GetConnection();
        //    connection.Execute("exec [SetPaymentOrder] @accountId", new {accountId});
        //}

        public static void CreateTransferRequest(Commitment commitment)
        {
            var connection = GetConnection();

            var query = "insert into TransferRequest " +
                         "(CommitmentId, TrainingCourses, Cost, [Status], [TransferApprovalActionedByEmployerName]," +
                        "[TransferApprovalActionedByEmployerEmail],[TransferApprovalActionedOn],[CreatedOn])" +
                         "VALUES(@CommitmentId, @TrainingCourses, @Cost, @Status, @TransferApprovalActionedByEmployerName," +
                        "@TransferApprovalActionedByEmployerEmail,@TransferApprovalActionedOn,@CreatedOn" + ")";

            var courseGroups = commitment.Apprenticeships.GroupBy(x => x.TrainingName);

            var courses = courseGroups.Select(x => new
            {
                CourseTitle = x.Key,
                ApprenticeshipCount = x.Count()
            });

            var courseText = JsonConvert.SerializeObject(courses);

            var result = connection.Execute(query, new
            {
                CommitmentId = commitment.Id,
                TrainingCourses = courseText,
                Cost = commitment.Apprenticeships.Sum(x => x.Cost),
                Status = commitment.TransferApprovalStatus,
                TransferApprovalActionedByEmployerName = "Person",
                TransferApprovalActionedByEmployerEmail = "person@mail.com",
                TransferApprovalActionedOn = default(DateTime?),
                CreatedOn = DateTime.Now
            });
        }

        public static void CreateMessages(Commitment commitment, int messageCount)
        {
            var connection = GetConnection();

            short createdBy = 0;

            for (var i = 0; i < messageCount; i++)
            {
                var message = new Message
                {
                    CommitmentId = commitment.Id,
                    Author = "Test",
                    CreatedBy = createdBy,
                    Text = TextHelper.GetRandomText(RandomHelper.GetRandomNumber(6))
                };

                var query = "insert into [Message] " +
                            "([CommitmentId],[Text],[CreatedDateTime],[Author],[CreatedBy])" +
                            "VALUES(" +
                            "@CommitmentId, @Text, GETDATE(), @Author, @CreatedBy)";

                connection.Execute(query, message);

                createdBy++;
                if (createdBy > 1)
                {
                    createdBy = 0;
                }
            }
        }

        public static void SaveDataLock(Apprenticeship apprenticeship, DataLock datalock)
        {
            var connection = GetConnection();

            var query = "insert into DataLockStatus (DataLockEventId, DataLockEventDatetime, PriceEpisodeIdentifier," +
                        "ApprenticeshipId, IlrTrainingCourseCode, IlrTrainingType, IlrActualStartDate, IlrEffectiveFromDate, " +
                        "IlrTotalCost, ErrorCode, Status, TriageStatus, IsResolved, EventStatus) " +
                        "VALUES (@DataLockEventId, @DataLockEventDatetime, @PriceEpisodeIdentifier," +
                        "@ApprenticeshipId, @IlrTrainingCourseCode, @IlType, @IlrActualStartDate, @IlrEffectiveFromDate," +
                        "@IlrTotalCost, @ErrorCode, @Status, @TriageStatus,  @IsResolved, @EventStatus)";

            var result = connection.Execute(query, new
            {
                DataLockEventId = IdentityHelpers.GetNextDataLockStatusId(),
                DataLockEventDatetime = DateTime.Now,
                PriceEpisodeIdentifier = datalock.PriceEpisodeIdentifier,
                ApprenticeshipId = apprenticeship.Id,
                IlrTrainingCourseCode = datalock.IlrTrainingCourseCode,
                IlType = datalock.IlrTrainingType,
                IlrActualStartDate = apprenticeship.StartDate.Value,
                IlrEffectiveFromDate = datalock.IlrEffectiveFromDate,
                IlrTotalCost = datalock.IlrTotalCost,
                ErrorCode = datalock.ErrorCode,
                Status = datalock.Status,
                TriageStatus = 0,
                IsResolved = false,
                EventStatus = 1
            });
        }

        public static void CreateChangeOfCircumstances(Apprenticeship apprenticeship, Originator originator = Originator.Employer)
        {
            //todo: allow different originators

            var connection = GetConnection();

            var query =
                @"insert into ApprenticeshipUpdate(ApprenticeshipId, Originator, Status, FirstName) values(@ApprenticeshipId, @Originator, @Status, @FirstName);";

            var result = connection.Execute(query, new
            {
                ApprenticeshipId = apprenticeship.Id,
                Originator = originator,
                Status = 0,
                FirstName = "Jonny Boo"
            });
        }

        public static void SaveChangeOfPartyRequest(ChangeOfPartyRequest changeOfPartyRequest)
        {
            var connection = GetConnection();

            var query = @"insert into ChangeOfPartyRequest([ApprenticeshipId]
           ,[ChangeOfPartyType]
           ,[OriginatingParty]
           ,[AccountLegalEntityId]
           ,[ProviderId]
           ,[Price]
           ,[StartDate]
           ,[EndDate]
           ,[CreatedOn]
           ,[Status]) values
            (@ApprenticeshipId
            ,@ChangeOfPartyType
            ,@OriginatingParty
           ,@AccountLegalEntityId
           ,@ProviderId
           ,@Price
           ,@StartDate
           ,@EndDate
           ,@CreatedOn
           ,@Status)";

            connection.Execute(query, changeOfPartyRequest);
        }
    }
}
