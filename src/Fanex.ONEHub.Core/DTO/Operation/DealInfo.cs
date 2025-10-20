namespace Fanex.Affiliate.Admin.Core.DTO.Operation
{
    using System;

    public class DealInfo
    {
        public int RowNum { get; set; }

        public int GroupId { get; set; }

        public string DealName { get; set; }

        public string DealType { get; set; }

        public int NoAff { get; set; }

        public string Description { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedDateStr => UpdatedDate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}