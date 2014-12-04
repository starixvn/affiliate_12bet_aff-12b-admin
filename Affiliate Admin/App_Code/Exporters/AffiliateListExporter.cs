using System.Collections.Generic;
using System.Linq;
using DNAide.Data;
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
            style.BorderBottom = CellBorderType.DASHED;
            style.BottomBorderColor = IndexedColors.BLACK.Index;
            style.BorderLeft = CellBorderType.DASHED;
            style.LeftBorderColor = IndexedColors.BLACK.Index;
            style.BorderRight = CellBorderType.DASHED;
            style.RightBorderColor = IndexedColors.BLACK.Index;
            style.BorderTop = CellBorderType.DASHED;
            style.TopBorderColor = IndexedColors.BLACK.Index;
        }

        protected ICellStyle GetHeaderStyle(HSSFWorkbook workbook)
        {
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = IndexedColors.PALE_BLUE.Index;
            headerStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            headerStyle.Alignment = HorizontalAlignment.CENTER;
            headerStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            BorderCellStyle(headerStyle);

            
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.BOLD; 
            headerStyle.SetFont(font);

            return headerStyle;
        }

        protected void CreateSamplePalettes(HSSFWorkbook workbook)
        {
            var oddStyle = workbook.CreateCellStyle();
            oddStyle.FillForegroundColor = IndexedColors.GREY_25_PERCENT.Index;
            oddStyle.FillPattern = FillPatternType.BRICKS;
            oddStyle.FillBackgroundColor = IndexedColors.WHITE.Index;
            BorderCellStyle(oddStyle);

            var evenStyle = workbook.CreateCellStyle();
            evenStyle.FillForegroundColor = IndexedColors.WHITE.Index;
            evenStyle.FillPattern = FillPatternType.BRICKS;
            evenStyle.FillBackgroundColor = IndexedColors.WHITE.Index;
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
            style.Alignment = HorizontalAlignment.LEFT;
            palette.Add(style);

            style = workbook.CreateCellStyle();
            style.CloneStyleFrom(sampleStyle);
            style.Alignment = HorizontalAlignment.CENTER;
            palette.Add(style);

            style = workbook.CreateCellStyle();
            style.CloneStyleFrom(sampleStyle);
            style.Alignment = HorizontalAlignment.RIGHT;
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