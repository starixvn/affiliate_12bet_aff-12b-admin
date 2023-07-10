using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Affiliate
{
    public class PaymentRequestRepositoryTest
    {
        [Fact]
        public void GetPaymentRequestList_WhenCall_ReturnCorrectEmailInfoList()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(1);
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedPaymentRequestList());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePaymentRequest_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPaymentRequestList(new PaymentRequestParam());
            var item = result.MainData.SingleOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("04/15/2020 00:00:00", item.TransDateText);
            Assert.Equal("04/15/2020 00:00:00", item.CompletedDateText);
        }

        [Fact]
        public void GetPaymentRequestList_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns(DBNull.Value);
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePaymentRequest_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPaymentRequestList(new PaymentRequestParam());

            // Assert
            Assert.Empty(result.MainData);
        }

        [Fact]
        public void GetPaymentRequestDetail_WhenCall_ReturnCorrectEmailInfo()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);

            ReaderHelper.MapReaderData(dataReader, GetExpectedPaymentRequestDetail());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePaymentRequest_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPaymentRequestDetail(123);

            // Assert
            Assert.Equal(123, result.TransId);
            Assert.Equal("04/15/2020 00:00:00 AM", result.TransDateText);
            Assert.Equal("04/15/2020 00:00:00 AM", result.CompletedDateText);
        }

        [Fact]
        public void GetPaymentRequestDetail_WhenCall_ReturnNull()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();
            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(false).Returns(false);

            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePaymentRequest_Detail_Sel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetPaymentRequestDetail(123);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdatePaymentRequest_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePaymentRequest_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdatePaymentRequest(new PaymentUpdateParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetSiteDBConfigInfoByCustId_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            repository.Setup(re => re.GetObjectDb("Acc_SiteDBConfigSel", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.GetSiteDBConfigInfoByCustId("123");

            // Assert
            objectDb.Verify(obj => obj.QueryEntity<SiteDBConfigInfo>(It.IsAny<object>(), null), Times.Once);
        }

        [Fact]
        public void ApprovePaymentRequest_WhenCall_ExcuteSucceed()
        {
            // Arrange
            const string SpKey = "Aff_Admin_AffiliatePaymentRequest_Approve";
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb(SpKey, 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.ApprovePaymentRequest(new PaymentApproveParam { SpKey = SpKey });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void RollbackApprovePaymentRequest_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_AffiliatePaymentRequest_Upd_Rollback", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.RollbackApprovePaymentRequest(new ResultInfo(), "");

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void WriteLogWhenUpdatePaymentRequestSuccessfully_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var param = new PaymentUpdateLogParam();
            var repository = new Mock<PaymentRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            repository.Setup(re => re.GetObjectDb("Aff_UserLog_RunSP_Detail_WriteLog", 120)).Returns(objectDb.Object);

            // Action
            repository.Object.WriteLogWhenUpdatePaymentRequestSuccessfully(param);

            // Assert
            Assert.NotNull(JsonConvert.SerializeObject(param));
            objectDb.Verify(obj => obj.ExecuteNonQuery(It.IsAny<object>(), null), Times.Once);
        }

        private IDictionary<string, object> GetExpectedPaymentRequestList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Username"] = "12BetAff226",
                ["Transid"] = 123,
                ["Status"] = "Pending",
                ["Amount"] = 100m,
                ["Transdate"] = new DateTime(2020, 04, 15),
                ["Completeddate"] = new DateTime(2020, 04, 15),
                ["Creator"] = "abc"
            };
        }

        private IDictionary<string, object> GetExpectedPaymentRequestDetail()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["username"] = "12BetAff226",
                ["Transid"] = 123,
                ["Status"] = "Pending",
                ["Amount"] = 100m,
                ["Transdate"] = new DateTime(2020, 04, 15),
                ["Completeddate"] = new DateTime(2020, 04, 15),
                ["Creator"] = "abc",
                ["email"] = "abc@gmail.com",
                ["AffId"] = 1271808,
                ["firstname"] = "abc",
                ["lastname"] = "123",
                ["PreferredPayment"] = "vi-VN",
                ["Username2"] = "xyz",
                ["adminname"] = "abc123",
                ["Username2"] = "",
                ["adminname"] = ""
            };
        }
    }
}
