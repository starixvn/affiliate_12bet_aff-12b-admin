using ExcelDataReader;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.MassAdjustment;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MassAdjustment
{
    public class MassAdjustmentService : IMassAdjustmentService
    {
        private static readonly string[] ValidExtensions = {
            ".xlsx",
            ".xls"
        };

        private readonly ISystemFile _systemFile;
        private readonly IMassAdjustmentRepository _massAdjustmentRepository;

        public MassAdjustmentService(IMassAdjustmentRepository massAdjustmentRepository,
            ISystemFile systemFile)
        {
            _massAdjustmentRepository = massAdjustmentRepository;
            _systemFile = systemFile;
        }

        public AdjustmentResultInfo UpdateMassAdjusment(HttpPostedFileBase file, MassAdjustmentParam param)
        {
            using (var reader = ExcelReaderFactory.CreateReader(file.InputStream))
            {
                var hasMergeCell = MassAdjustmentHelper.HasMergeCell(reader);
                var dataSet = reader.AsDataSet();
                var firstSheet = dataSet.Tables[0];

                var validateFileResult = ValidateFile(firstSheet, file.FileName, hasMergeCell);

                if (validateFileResult.Result.IsSuccessful)
                {
                    param.XmlData = GetXmlData(firstSheet.Rows);

                    return _massAdjustmentRepository.UpdateMassAdjusment(param);
                }

                return validateFileResult;
            }
        }

        private AdjustmentResultInfo ValidateFile(DataTable data, string fileName, bool hasMergeCell)
        {
            var isExcelFile = IsExcelFile(fileName);

            var isValidCellNumber = MassAdjustmentHelper.IsValidCellNumber(data.Rows.Count, data.Columns.Count);

            if (!isExcelFile || hasMergeCell || !isValidCellNumber)
            {
                return MassAdjustmentHelper.CreateErrorResult();
            }

            return new AdjustmentResultInfo();
        }

        private bool IsExcelFile(string fileName)
        {
            var extension = _systemFile.GetExtension(fileName);

            return ValidExtensions.Contains(extension.ToLowerInvariant());
        }

        private static string GetXmlData(DataRowCollection rows)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            //First line of file must be title line. Therefore, system shall not check this line.
            for (int i = 1; i < rows.Count; i++)
            {
                var rowData = rows[i];
                var itemArray = rowData.ItemArray;
                var userName = itemArray[Validation.UserNameColumnIndex].ToString().Trim();
                var amount = itemArray[Validation.AmountColumnIndex].ToString().Trim();
                var reason = itemArray[Validation.ReasonColumnIndex].ToString().Trim();

                var row = $"<r Username=\"{WebUtility.HtmlEncode(userName)}\" Amount=\"{WebUtility.HtmlEncode(amount)}\" Description=\"{WebUtility.HtmlEncode(reason)}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}
