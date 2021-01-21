using ExcelDataReader;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MassAdjustment
{
    public class MassAdjustmentService : IMassAdjustmentService
    {
        private const int MaxRow = 501;
        private const int MinColumn = 2;
        private const int UserNameColumnIndex = 0;
        private const int AmountColumnIndex = 1;
        private const int ReasonColumnIndex = 2;
        private static readonly string[] ValidExtensions = {
            ".xlsx",
            ".xls"
        };

        private readonly IMassAdjustmentRepository _massAdjustmentRepository;
        private readonly ISystemFile _systemFile;

        public MassAdjustmentService(IMassAdjustmentRepository massAdjustmentRepository,
            ISystemFile systemFile)
        {
            _massAdjustmentRepository = massAdjustmentRepository;
            _systemFile = systemFile;
        }

        public AdjustmentResultInfo InsertMassAdjusment(HttpPostedFileBase file)
        {
            var validateFileResult = ValidateFile(file);

            if (validateFileResult.Result.IsSuccessful)
            {
                return _massAdjustmentRepository.InsertMassAdjusment();
            }

            return validateFileResult;
        }

        public AdjustmentResultInfo ValidateFile(HttpPostedFileBase file)
        {
            using (var reader = ExcelReaderFactory.CreateReader(file.InputStream))
            {
                var isExcelFile = IsExcelFile(file.FileName);
                var hasMergeCell = HasMergeCell(reader);

                var dataSet = reader.AsDataSet();
                var firstSheet = dataSet.Tables[0];

                var isValidCellNumber = IsValidCellNumber(firstSheet.Rows.Count, firstSheet.Columns.Count);

                if (!isExcelFile || hasMergeCell || !isValidCellNumber)
                {
                    return CreateErrorResult();
                }

                var result = CheckDataOfRow(firstSheet.Rows);

                return new AdjustmentResultInfo
                {
                    ErroredAdjustmentInfoList = result
                };
            }
        }

        public bool IsExcelFile(string fileName)
        {
            var extension = _systemFile.GetExtension(fileName);

            return ValidExtensions.Contains(extension.ToLowerInvariant());
        }

        public static bool IsValidCellNumber(int rowNumber, int columnNumber)
        {
            return rowNumber < MaxRow || columnNumber < MinColumn;
        }

        public static bool HasMergeCell(IExcelDataReader reader)
        {
            var mergeCells = reader.MergeCells;

            return mergeCells != null;
        }

        public static List<ErroredAdjustmentInfo> CheckDataOfRow(DataRowCollection rows)
        {
            List<ErroredAdjustmentInfo> erroredAdjustmentInfoList = new List<ErroredAdjustmentInfo>();

            for (int i = 0; i < rows.Count; i++)
            {
                var rowData = rows[i];

                var itemArray = rowData.ItemArray;
                var userName = itemArray[UserNameColumnIndex].ToString();
                var amount = itemArray[AmountColumnIndex].ToString();

                var isValidUserName = IsValidUserName(userName);
                var isValidAmount = IsValidAmount(amount);
                var isDuplicateUserName = IsDuplicateUserName(rows, userName.ToLowerInvariant(), i);

                if(!isDuplicateUserName.IsSuccessful || !isValidUserName.IsSuccessful || !isValidAmount.IsSuccessful)
                {
                    erroredAdjustmentInfoList.Add(new ErroredAdjustmentInfo {
                        Username = userName,
                        Amount = amount,
                        Reason = isDuplicateUserName.Message + isValidUserName.Message + isValidAmount.Message
                    });
                }
            }

            return erroredAdjustmentInfoList;
        }

        public static ResultInfo IsDuplicateUserName(DataRowCollection rows, string userName, int index)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                var rowData = rows[i];
                var itemArray = rowData.ItemArray;
                var userNameToLower = itemArray[UserNameColumnIndex].ToString().ToLowerInvariant();

                if(userName.Equals(userNameToLower, StringComparison.InvariantCultureIgnoreCase) && i != index)
                {
                    return new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = "This username is duplicate in Import file. "
                    };
                }
            }

            return new ResultInfo();
        }

        public static ResultInfo IsValidUserName(string userName)
        {
            if(string.IsNullOrEmpty(userName))
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "This username does not exist. "
                };
            }

            return new ResultInfo();
        }

        public static ResultInfo IsValidAmount(string amount)
        {
            if (string.IsNullOrEmpty(amount) || !decimal.TryParse(amount, out decimal amountNumber))
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
                    Code = Code.GeneralErrorCode,
                    Message = "The amount is too big."
                };
            }

            return new ResultInfo();
        }

        private static AdjustmentResultInfo CreateErrorResult()
        {
            return new AdjustmentResultInfo {
                Result = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidAdjustmentFile
                },
                ErroredAdjustmentInfoList = new List<ErroredAdjustmentInfo>
                {
                    new ErroredAdjustmentInfo
                    {
                        Username = "Test01",
                        Amount = "1",
                        Reason = "Invalid username"
                    }
                }
            };
        }
    }
}
