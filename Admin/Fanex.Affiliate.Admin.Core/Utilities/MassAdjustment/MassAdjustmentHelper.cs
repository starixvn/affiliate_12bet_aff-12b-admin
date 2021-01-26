using ExcelDataReader;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Resources;

namespace Fanex.Affiliate.Admin.Core.Utilities.MassAdjustment
{
    public static class MassAdjustmentHelper
    {
        public static bool IsValidCellNumber(int rowNumber, int columnNumber)
        {
            return rowNumber < Validation.MaxRow && columnNumber >= Validation.MinColumn;
        }

        public static bool HasMergeCell(IExcelDataReader reader)
        {
            var mergeCells = reader.MergeCells;

            return mergeCells != null;
        }

        public static AdjustmentResultInfo CreateErrorResult()
        {
            return new AdjustmentResultInfo
            {
                Result = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidAdjustmentFile
                }
            };
        }
    }
}
