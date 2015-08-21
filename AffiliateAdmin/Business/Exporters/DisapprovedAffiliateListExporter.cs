using System;
using System.Data;
using AffiliateAdmin.Business.DataAccess;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Exporters
{
    public class DisapprovedAffiliateListExporter : PendingAffiliateListExporter
    {
        public override void CreateHeader(HSSFWorkbook workbook, ISheet sheet)
        {
            base.CreateHeader(workbook, sheet);

            var dataRow = sheet.GetRow(0);

            dataRow.GetCell(5).SetCellValue(Resources.Lang.disapprovaldate);
        }

        public override void CreateBody(DbResult result, HSSFWorkbook workbook, ISheet sheet)
        {
            if (result.ResultSet.Tables.Count <= 0)
                return;

            DataTable dtbResult = result.ResultSet.Tables[0];
            int currentRow = 0;
            int count = 0;

            CreateSamplePalettes(workbook);

            int currentSheet = 1;
            const int colNo = 6;

            foreach (DataRow dr in dtbResult.Rows)
            {
                if (currentRow >= Int16.MaxValue - 1)
                {
                    for (int i = 0; i < colNo; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    currentSheet++;
                    sheet = workbook.CreateSheet("Sheet" + currentSheet);
                    CreateHeader(workbook, sheet);
                    currentRow = 0;
                }

                count++;
                currentRow++;

                SetValueToRow(sheet, dr, currentRow, count);
            }

            for (int i = 0; i < colNo; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        private void SetValueToRow(ISheet sheet, DataRow dr, int currentRow, int count)
        {
            var dataRow = sheet.CreateRow(currentRow);
            var selectedPalette = GetPalette(currentRow);

            dataRow.CreateCell(0).SetCellValue(count);
            dataRow.GetCell(0).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.Center);

            dataRow.CreateCell(1).SetCellValue(dr["username"].ToString());
            dataRow.GetCell(1).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.Left);

            dataRow.CreateCell(2).SetCellValue(dr["firstname"].ToString());
            dataRow.GetCell(2).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.Left);

            dataRow.CreateCell(3).SetCellValue(dr["lastname"].ToString());
            dataRow.GetCell(3).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.Left);

            dataRow.CreateCell(4).SetCellValue(Common.GerFullTextLang(dr["preferredlanguage"]));
            dataRow.GetCell(4).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.Left);

            dataRow.CreateCell(5).SetCellValue(Common.IBCDateTimeFormat(dr["ApprovedDate"], DTFormatType.DT24h));
            dataRow.GetCell(5).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.Center);
        }
    }
}