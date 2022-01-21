
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Repository.ProcessingRequest;
using Fanex.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Repository.Affiliate
{
    public class ProcessingRequestRepositoryTest
    {
        [Fact]
        public void AddProcessingRequest_WhenCall_AddSuccessfully()
        {
            //Arrange
            var (objectDb, repository) = GetObjectDbAndRepository();
            objectDb.Setup(obj => obj.ExecuteNonQuery(It.IsAny<object>(), null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(re => re.GetObjectDb("Aff_Admin_ProcessingRequest_Ins", 120)).Returns(objectDb.Object);

            //Action
            var result = repository.Object.AddProcessingRequest(new ProcessingRequestInsParam());

            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteProcessingRequest_WhenCall_DeleteSuccesslly()
        {
            //Arrange
            var (objectDb, repository) = GetObjectDbAndRepository();
            objectDb.Setup(obj => obj.ExecuteNonQuery(It.IsAny<object>(), null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_ProcessingRequest_Del", 120)).Returns(objectDb.Object);

            //Action
            var result = repository.Object.DeleteProcessingRequest(new ProcessingRequestDelParam());

            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetProcessingRequestDetail_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var (objectDb, repository) = GetObjectDbAndRepository();
            objectDb.Setup(obj => obj.QueryEntity<ProcessingRequestDetailInfo>(It.IsAny<object>(), It.IsAny<DbTransaction>())).Returns(new ProcessingRequestDetailInfo());
            repository.Setup(r => r.GetObjectDb("Aff_Admin_ProcessingRequestDetail_Sel", 120)).Returns(objectDb.Object);

            //Action
            var result = repository.Object.GetProcessingRequestDetail(123);

            //Assert
            Assert.NotNull(result);
        }

        //[Fact]
        //public void GetProcessingRequestList_WhenCall_ReturnCorrectData()
        //{
        //    //Arrange
        //    var reader = new Mock<GridReader>();
        //    reader.Setup(r => r.Read<ProcessingRequestBasicInfo>(It.IsAny<bool>())).Returns(new List<ProcessingRequestBasicInfo> { new ProcessingRequestBasicInfo() });
        //    reader.Setup(r => r.Read<ProcessingRequestSummaryInfo>(true)).Returns(new List<ProcessingRequestSummaryInfo> { new ProcessingRequestSummaryInfo() });
        //    var (objectDb, repository) = GetObjectDbAndRepository();
        //    objectDb.Setup(obj => obj.QueryMultiple(It.IsAny<object>(), It.IsAny<IDbTransaction>(), 120)).Returns(reader.Object);
        //    objectDb.Setup(obj => obj.GetParameterValue("NRows")).Returns("1");
        //    repository.Setup(r => r.GetObjectDb("Aff_Admin_ProcessingRequest_Sel", 120)).Returns(objectDb.Object);

        //    //Action
        //    var result = repository.Object.GetProcessingRequestList(new ProcessingRequestSelParam());

        //    //Assert
        //    Assert.Single(result.MainData);
        //    Assert.NotNull(result.Summary);
        //    Assert.Equal(1, result.TotalItem);
        //}


        [Fact]
        public void UpdateProcessingRequest_WhenCall_UpdateSuccesslly()
        {
            //Arrange
            var (objectDb, repository) = GetObjectDbAndRepository();
            objectDb.Setup(obj => obj.ExecuteNonQuery(It.IsAny<object>(), null)).Returns(0);
            objectDb.Setup(obj => obj.GetParameterValue("ErrCode")).Returns(DBNull.Value);
            objectDb.Setup(obj => obj.GetParameterValue("ErrMsg")).Returns(string.Empty);
            repository.Setup(r => r.GetObjectDb("Aff_Admin_ProcessingRequest_Upd", 120)).Returns(objectDb.Object);

            //Action
            var result = repository.Object.UpdateProcessingRequest(new ProcessingRequestUpdParam());

            //Assert
            Assert.True(result.IsSuccessful);
        }

        private (Mock<IObjectDb>, Mock<ProcessingRequestRepository>) GetObjectDbAndRepository()
        {
            var repository = new Mock<ProcessingRequestRepository> { CallBase = true };
            var objectDb = new Mock<IObjectDb>();

            return (objectDb, repository);
        }
    }
}
