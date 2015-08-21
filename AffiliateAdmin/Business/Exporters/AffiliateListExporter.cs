using System.Collections.Generic;
using System.Linq;
using AffiliateAdmin.Business.DataAccess;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Exporters
{
    public abstract class AffiliateListExporter
    {

        public abstract void CreateHeader(HSSFWorkbook workbook, ISheet sheet);

        public abstract void CreateBody(DbResult result, HSSFWorkbook workbook, ISheet sheet);

        protected IList<ICellStyle>[] samplePalettes;

        protected IList<ICellStyle> GetPalette(int currentRow)
        {
            return samplePalettes[currentRow % 2];
        }

        protected void BorderCellStyle(ICellStyle style)
        {
            style.BorderBottom = BorderStyle.Dashed;
            style.BottomBorderColor = IndexedColors.Black.Index;
            style.BorderLeft = BorderStyle.Dashed;
            style.LeftBorderColor = IndexedColors.Black.Index;
            style.BorderRight = BorderStyle.Dashed;
            style.RightBorderColor = IndexedColors.Black.Index;
            style.BorderTop = BorderStyle.Dashed;
            style.TopBorderColor = IndexedColors.Black.Index;
        }

        protected ICellStyle GetHeaderStyle(HSSFWorkbook workbook)
        {
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = IndexedColors.PaleBlue.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            BorderCellStyle(headerStyle);

            
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold; 
            headerStyle.SetFont(font);

            return headerStyle;
        }

        protected void CreateSamplePalettes(HSSFWorkbook workbook)
        {
            var oddStyle = workbook.CreateCellStyle();
            oddStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            oddStyle.FillPattern = FillPattern.Bricks;
            oddStyle.FillBackgroundColor = IndexedColors.White.Index;
            BorderCellStyle(oddStyle);

            var evenStyle = workbook.CreateCellStyle();
            evenStyle.FillForegroundColor = IndexedColors.White.Index;
            evenStyle.FillPattern = FillPattern.Bricks;
            evenStyle.FillBackgroundColor = IndexedColors.White.Index;
            BorderCellStyle(evenStyle);

            samplePalettes = new[]
                {
                    CreateStylePalette(workbook, oddStyle),
                    CreateStylePalette(workbook, evenStyle),
                };
        }

        protected IList<ICellStyle> CreateStylePalette(HSSFWorkbook workbook, ICellStyle sampleStyle)
        {
            var palette = new List<ICellStyle>();
            var style = workbook.CreateCellStyle();
            style.CloneStyleFrom(sampleStyle);
            style.Alignment = HorizontalAlignment.Left;
            palette.Add(style);

            style = workbook.CreateCellStyle();
            style.CloneStyleFrom(sampleStyle);
            style.Alignment = HorizontalAlignment.Center;
            palette.Add(style);

            style = workbook.CreateCellStyle();
            style.CloneStyleFrom(sampleStyle);
            style.Alignment = HorizontalAlignment.Right;
            palette.Add(style);

            return palette;
        }

        protected ICellStyle GetCellStyleFromPalette(IList<ICellStyle> palette, HorizontalAlignment alignment)
        {
            foreach (var cellStyle in palette)
            {
                if (cellStyle.Alignment == alignment)
                {
                    return cellStyle;
                }
            }

            return palette.FirstOrDefault();
        }
    }
}