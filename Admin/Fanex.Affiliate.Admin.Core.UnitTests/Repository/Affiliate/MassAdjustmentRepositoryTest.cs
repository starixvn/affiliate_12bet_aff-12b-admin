using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Affiliate
{
    public class MassAdjustmentRepositoryTest
    {
        [Fact]
        public void UpdateMassAdjusment_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var repository = new Mock<MassAdjustmentRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Adjustment_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateMassAdjusment(new MassAdjustmentParam());

            // Assert
            Assert.True(result.Result.IsSuccessful);
        }

        [Fact]
        public void UpdateMassAdjusment_WhenCall_ExcuteFailed()
        {
            // Arrange
            var repository = new Mock<MassAdjustmentRepository> { CallBase = true };

            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(Code.GeneralErrorCode);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);

            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);
            ReaderHelper.MapReaderData(dataReader, GetErrorList());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Adjustment_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateMassAdjusment(new MassAdjustmentParam());

            // Assert
            Assert.False(result.Result.IsSuccessful);
            Assert.Single(result.ErroredAdjustmentInfoList);
        }

        [Fact]
        public void UpdateMassAdjusment_WhenCallWithValidAmount_ExcuteFailed()
        {
            // Arrange
            var repository = new Mock<MassAdjustmentRepository> { CallBase = true };

            var objectDb = new Mock<IObjectDb>();
            objectDb.Setup(obj => obj.ExecuteNonQuery(null, null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(Code.GeneralErrorCode);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);

            var dataReader = new Mock<IDataReader>();
            dataReader.SetupSequence(d => d.Read()).Returns(true).Returns(false);
            ReaderHelper.MapReaderData(dataReader, GetErrorListHasValidAmount());
            objectDb.Setup(obj => obj.ExecuteReader(null, null)).Returns(dataReader.Object);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_Adjustment_Upd", 120)).Returns(objectDb.Object);

            // Action
            var result = repository.Object.UpdateMassAdjusment(new MassAdjustmentParam());

            // Assert
            Assert.False(result.Result.IsSuccessful);
            Assert.Single(result.ErroredAdjustmentInfoList);
        }

        private IDictionary<string, object> GetErrorList()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Username"] = string.Empty,
                ["Amount"] = string.Empty,
                ["ErrorMsg"] = "Username and Amount can not blank"
            };
        }

        private IDictionary<string, object> GetErrorListHasValidAmount()
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["Username"] = string.Empty,
                ["Amount"] = 123,
                ["ErrorMsg"] = "Username can not blank"
            };
        }
    }
}
