namespace Fanex.Affiliate.Admin.Core.MetaModels.Report
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RedepositReportParam
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DealName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsExportExcel { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
