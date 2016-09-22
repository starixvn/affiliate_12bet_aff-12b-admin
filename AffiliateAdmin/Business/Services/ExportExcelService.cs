using System.IO;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using Criterias;
using DNAide.Core;
using Exporters;
using NPOI.HSSF.UserModel;

namespace AffiliateAdmin.Business.Services
{
    public class ExportExcelService
    {
        public void ExportExcel(HttpContext context)
        {
            var criteria = GetCritera(context);

            byte[] reportDoc = GetExportExcel(context, criteria);

            context.Response.ContentType = "application/vnd.ms-excel";

            var fileName = GetExportFileName(criteria);

            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            context.Response.BinaryWrite(reportDoc);
        }

        private static string GetExportFileName(AffiliateListCriteria criteria)
        {
            var suffix = string.Empty;
            if (criteria.IsFavorite)
            {
                suffix = "MyFavorite";
            }
            else if (criteria.IsClosed)
            {
                suffix = "closed";
            }
            else
            {
                suffix = criteria.Status;
            }

            return string.Format("AffiliateList_{0}.xls", suffix);
        }

        private AffiliateListCriteria GetCritera(HttpContext context)
        {
            string strStatus = CastAide.AsString(context.Request["status"], Common.Approved);

            int intID;

            try
            {
                intID = CastAide.AsInt32(context.Request["txtID"], -1);
            }
            catch
            {
                intID = -2;
            }

            string strUserName = CastAide.AsString(context.Request["txtUsername"], string.Empty);
            string strEmail = CastAide.AsString(context.Request["txtEmail"], string.Empty);
            int slCommGroup = CastAide.AsInt32(context.Request["slCommGroup"], -1);
            var slLanguage = CastAide.AsString(context.Request["slLanguage"], string.Empty);
            var isFavor = CastAide.AsInt32(context.Request["favor"], 0) == 1;
            var isClosed = CastAide.AsBoolean(context.Request["isClosed"], false);

            strUserName = ((string.Compare(strUserName, Resources.Lang.Username, true) == 0 ? string.Empty : strUserName));
            strEmail = ((string.Compare(strEmail, Resources.Lang.Email, true) == 0 ? string.Empty : strEmail));

            var criteria = new AffiliateListCriteria
            {
                GroupId = slCommGroup,
                Username = strUserName,
                Email = strEmail,
                AffId = intID,
                Status = strStatus,
                IsClosed = isClosed,
                IsAll = true,
                IsFavorite = isFavor,
                Language = string.IsNullOrEmpty(slLanguage) ? null : slLanguage,
            };

            if (isFavor)
            {
                criteria.PageIndex = 1;
                criteria.PageSize = int.MaxValue - 1;
            }

            return criteria;
        }

        private byte[] GetExportExcel(HttpContext context, AffiliateListCriteria criteria)
        {
            const string templatePath = @"..\ExcelFiles\AffiliateList\AffiliateList.xls";
            var fs = new FileStream(context.Server.MapPath(templatePath), FileMode.Open, FileAccess.Read);

            var workbook = new HSSFWorkbook(fs, true);
            var sheet = workbook.GetSheet("Sheet1");

            DbResult result = null;
            if (criteria.AffId != -2)
            {
                if (!criteria.IsFavorite)
                    result = DataHandler.Affiliate.ListAff(criteria);
                else
                    result = DataHandler.Affiliate.ListAffFavorie(criteria);

                criteria.NRows = CastAide.AsInt32(result.Outputs["nrows"]);
            }

            var exporter = ExporterFactory.GetExporter(criteria.Status);

            exporter.CreateHeader(workbook, sheet);

            exporter.CreateBody(result, workbook, sheet);

            var ms = new MemoryStream();

            workbook.Write(ms);

            return ms.ToArray();
        }
    }
}