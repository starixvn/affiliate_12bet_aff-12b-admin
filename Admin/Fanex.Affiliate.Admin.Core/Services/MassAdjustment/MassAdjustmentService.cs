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
        private const int MaxRows = 500;
        private const int UserNameColumnIndex = 1;
        private const int AmountColumnIndex = 2;
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

                if (!isExcelFile || hasMergeCell)
                {
                    return CreateErrorResult();
                }

                var hasRowIsNull = HasRowIsNull(reader);
            }

            return new AdjustmentResultInfo();
        }

        public bool IsExcelFile(string fileName)
        {
            var extension = _systemFile.GetExtension(fileName);

            return ValidExtensions.Contains(extension.ToLowerInvariant());
        }

        public static bool HasMergeCell(IExcelDataReader reader)
        {
            var mergeCells = reader.MergeCells;

            return mergeCells != null;
        }

        public static bool IsValidRow(DataRow row)
        {
            return !row.IsNull(UserNameColumnIndex) && !row.IsNull(AmountColumnIndex);
        }

        public static bool HasRowIsNull(IExcelDataReader reader)
        {
            var dataSet = reader.AsDataSet();
            var table = dataSet.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                var isValidRow = IsValidRow(row);

                if(!isValidRow)
                {
                    return isValidRow;
                }
            }

            return true;
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
                        Amount = 1,
                        Reason = "Invalid username"
                    }
                }
            };
        }
    }
}
