using System.Collections.Generic;
using System.Linq;
using DNAide.Data;
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

        protected void CreateSamplePalettes(HSSFWorkbook workbook)
        {
            var oddStyle = workbook.CreateCellStyle();
            oddStyle.FillForegroundColor = IndexedColors.GREY_25_PERCENT.Index;
            oddStyle.FillPattern = FillPatternType.BRICKS;
            oddStyle.FillBackgroundColor = IndexedColors.WHITE.Index;

            var evenStyle = workbook.CreateCellStyle();
            evenStyle.FillForegroundColor = IndexedColors.WHITE.Index;
            evenStyle.FillPattern = FillPatternType.BRICKS;
            evenStyle.FillBackgroundColor = IndexedColors.WHITE.Index;

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