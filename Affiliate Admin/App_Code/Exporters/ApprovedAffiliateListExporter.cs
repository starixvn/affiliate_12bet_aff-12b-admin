using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DNAide.Core;
using DNAide.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Exporters
{
    public class ApprovedAffiliateListExporter : AffiliateListExporter
    {
        public override void CreateHeader(HSSFWorkbook workbook, ISheet sheet)
        {
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = IndexedColors.LIGHT_BLUE.Index;
            headerStyle.FillPattern = FillPatternType.BRICKS;
            headerStyle.FillBackgroundColor = IndexedColors.WHITE.Index;
            headerStyle.Alignment = HorizontalAlignment.CENTER;

            var dataRow = sheet.CreateRow(0);
            dataRow.CreateCell(0);
            dataRow.CreateCell(1).SetCellValue(Resources.Lang.Username);
            
            dataRow.CreateCell(2).SetCellValue(Resources.Lang.FirstName);
            dataRow.CreateCell(3).SetCellValue(Resources.Lang.LastName);
            dataRow.CreateCell(4).SetCellValue(Resources.Lang.Language);
            dataRow.CreateCell(5).SetCellValue(Resources.Lang.approvaldate);
            dataRow.CreateCell(6).SetCellValue(Resources.Lang.CasinoDate);
            dataRow.CreateCell(7).SetCellValue(Resources.Lang.CommGroup);
            dataRow.CreateCell(8).SetCellValue(Resources.Lang.BalanceForward);
            
            dataRow.CreateCell(9).SetCellValue(Resources.Lang.Member);

            const int colNo = 10;

            for (int i = 0; i < colNo; i++)
            {
                dataRow.GetCell(i).CellStyle = headerStyle;
            }
        }

        public override void CreateBody(DbResult result, HSSFWorkbook workbook, ISheet sheet)
        {
            if (result.ResultSet.Tables.Count <= 0)
                return;

            DataTable dtbResult = result.ResultSet.Tables[0];
            int currentRow = 0;
            int count = 0;

            CreateSamplePalettes(workbook);

            var maxValue = 128;
            int currentSheet = 1;
            const int colNo = 10;
            
            foreach (DataRow dr in dtbResult.Rows)
            {
                if (currentRow >= maxValue - 1)
                {
                    for (int i = 0; i < colNo; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }

                    currentSheet ++;
                    sheet = workbook.CreateSheet("Sheet" + currentSheet);
                    CreateHeader(workbook, sheet);
                    currentRow = 0;
                }

                count++;
                currentRow++;

                SetValueToRow(workbook, sheet, dr, currentRow, count);
            }

            for (int i = 0; i < colNo; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        private void SetValueToRow(HSSFWorkbook workbook, ISheet sheet, DataRow dr, int currentRow, int count)
        {
            var dataRow = sheet.CreateRow(currentRow);

            var selectedPalette = GetPalette(currentRow);

            var isClosed = CastAide.AsBoolean(dr["isclosed"]);

            if (isClosed)
            {
                selectedPalette = GetClosedPalette(workbook);
            }

            dataRow.CreateCell(0).SetCellValue(count);
            dataRow.GetCell(0).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.CENTER);

            dataRow.CreateCell(1).SetCellValue(dr["username"].ToString());
            dataRow.GetCell(1).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.LEFT);

            dataRow.CreateCell(2).SetCellValue(dr["firstname"].ToString());
            dataRow.GetCell(2).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.LEFT);

            dataRow.CreateCell(3).SetCellValue(dr["lastname"].ToString());
            dataRow.GetCell(3).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.LEFT);

            dataRow.CreateCell(4).SetCellValue(Common.GerFullTextLang(dr["preferredlanguage"]));
            dataRow.GetCell(4).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.LEFT);

            dataRow.CreateCell(5).SetCellValue(Common.IBCDateTimeFormat(dr["ApprovedDate"], DTFormatType.DT24h));
            dataRow.GetCell(5).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.CENTER);

            dataRow.CreateCell(6).SetCellValue(Common.FormatDateNoHour(dr["CasinoJoinedDate"]));
            dataRow.GetCell(6).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.CENTER);

            dataRow.CreateCell(7).SetCellValue(Common.CutLongText(dr["groupname"].ToString(), 20));
            dataRow.GetCell(7).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.CENTER);

            dataRow.CreateCell(8).SetCellValue(Common.FormatNumber(dr["balancebf"]));
            dataRow.GetCell(8).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.RIGHT);

            dataRow.CreateCell(9).SetCellValue(CastAide.AsInt32(dr["noofmember"]).ToString());
            dataRow.GetCell(9).CellStyle = GetCellStyleFromPalette(selectedPalette, HorizontalAlignment.CENTER);
        }

        private IList<ICellStyle> GetClosedPalette(HSSFWorkbook workbook)
        {
            var closedStyle = workbook.CreateCellStyle();
            closedStyle.FillForegroundColor = IndexedColors.GREY_50_PERCENT.Index;
            closedStyle.FillPattern = FillPatternType.BRICKS;
            closedStyle.FillBackgroundColor = IndexedColors.WHITE.Index;

            return CreateStylePalette(workbook, closedStyle);
        }
    }
}