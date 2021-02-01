using Fanex.Affiliate.Admin.Core.Utilities.MassAdjustment;
using Moq;
using Xunit;
using ExcelDataReader;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Utilities
{
    public class MassAdjustmentHelperTest
    {
        [Fact]
        public void IsValidCellNumber_WhenCallWithValidCell_ReturnTrue()
        {
            // Arrange

            // Action
            var result = MassAdjustmentHelper.IsValidCellNumber(123, 3);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValidCellNumber_WhenCallWithInvalidCell_ReturnFalse()
        {
            // Arrange

            // Action
            var result = MassAdjustmentHelper.IsValidCellNumber(123, 2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasMergeCell_WhenCall_ReturnTrue()
        {
            // Arrange
            var reader = new Mock<IExcelDataReader>();
            CellRange[] cellRange = { };
            reader.Setup(r => r.MergeCells).Returns(cellRange);
            // Action
            var result = MassAdjustmentHelper.HasMergeCell(reader.Object);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CreateErrorResult_WhenCall_ReturnCorrect()
        {
            // Arrange

            // Action
            var result = MassAdjustmentHelper.CreateErrorResult();

            // Assert
            Assert.Equal(Code.GeneralErrorCode, result.Result.Code);
        }
    }
}
