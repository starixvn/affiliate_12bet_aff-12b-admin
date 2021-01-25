using DNAide.Utils;
using ExcelDataReader;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Resources;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Utilities.MassAdjustment
{
    public static class MassAdjustmentHelper
    {
        public static bool IsValidCellNumber(int rowNumber, int columnNumber)
        {
            return rowNumber < Validation.MaxRow || columnNumber > Validation.MinColumn;
        }

        public static bool HasMergeCell(IExcelDataReader reader)
        {
            var mergeCells = reader.MergeCells;

            return mergeCells != null;
        }

        public static List<ErroredAdjustmentInfo> CheckDataOfRow(DataRowCollection rows)
        {
            List<ErroredAdjustmentInfo> erroredAdjustmentInfoList = new List<ErroredAdjustmentInfo>();

            //First line of file must be title line. Therefore, system shall not check this line.
            for (int i = 1; i < rows.Count; i++)
            {
                var rowData = rows[i];
                var itemArray = rowData.ItemArray;
                var userName = itemArray[Validation.UserNameColumnIndex].ToString().Trim();
                var amount = itemArray[Validation.AmountColumnIndex].ToString().Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    erroredAdjustmentInfoList.Add(new ErroredAdjustmentInfo
                    {
                        Username = userName,
                        Amount = FormatAmount(amount),
                        Reason = "Username cannot be blank."
                    });

                    continue;
                }

                var validateDuplicateUserNameResult = ValidateDuplicateUserName(rows, userName.ToLowerInvariant(), i);

                if (!validateDuplicateUserNameResult.IsSuccessful)
                {
                    erroredAdjustmentInfoList.Add(new ErroredAdjustmentInfo
                    {
                        Username = userName,
                        Amount = FormatAmount(amount),
                        Reason = validateDuplicateUserNameResult.Message
                    });

                    continue;
                }

                var validateAmountResult = ValidateAmount(amount);

                if (!validateAmountResult.IsSuccessful)
                {
                    erroredAdjustmentInfoList.Add(new ErroredAdjustmentInfo
                    {
                        Username = userName,
                        Amount = FormatAmount(amount),
                        Reason = validateAmountResult.Message
                    });
                }
            }

            return erroredAdjustmentInfoList;
        }

        private static ResultInfo ValidateDuplicateUserName(DataRowCollection rows, string userName, int index)
        {
            //First line of file must be title line. Therefore, system shall not check this line.
            for (int i = 1; i < rows.Count; i++)
            {
                var rowData = rows[i];
                var itemArray = rowData.ItemArray;
                var userNameToLower = itemArray[Validation.UserNameColumnIndex].ToString().Trim().ToLowerInvariant();

                if (userNameToLower.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && i != index)
                {
                    return new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = "This username is duplicate."
                    };
                }
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateAmount(string amount)
        {
            if (!decimal.TryParse(amount, out decimal amountNumber))
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "The amount is invalid."
                };
            }

            if (amountNumber < Money.MinValue || amountNumber > Money.MaxValue)
            {
                return new ResultInfo
                {
                    Code = Code.Adjustment,
                    Message = "The amount is too big."
                };
            }

            return new ResultInfo();
        }

        private static string FormatAmount(string amount)
        {
            if(string.IsNullOrEmpty(amount))
            {
                return amount;
            }

            if (decimal.TryParse(amount, out decimal amountNumber))
            {
                return NumberFormat.DecFormat(amountNumber, Money.DecDigits);
            }

            return amount;
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
