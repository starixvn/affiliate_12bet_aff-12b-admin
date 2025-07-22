using Fanex.Affiliate.Admin.Core.Criterias.Report;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Report
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Affiliate.Admin.Core.Constants;

    public class PlayerCpaReportRequest : PaginationRequest
    {
        [MaxLength(Validation.Hundred, ErrorMessage = "Affiliate must shorter than 100 characters!")]
        public string UserName { get; set; }

        [MaxLength(Validation.Hundred, ErrorMessage = "Player must shorter than 100 characters!")]
        public string CustName { get; set; }

        [MaxLength(Validation.Hundred, ErrorMessage = "Deal Name must shorter than 100 characters!")]
        public string DealName { get; set; }
        
        public short StatusId { get; set; }

        public short ViewBy { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SortColumn { get; set; }

        public bool IsAscending { get; set; }

        public bool IsExportExcel { get; set; }

        public PlayerCpaReportCriteria CreateCriteria()
        {
            return new PlayerCpaReportCriteria
            {
                UserName = UserName,
                CustName = CustName,
                DealName = DealName,
                StatusId = StatusId,
                ViewBy = ViewBy,
                FromDate = FromDate,
                ToDate = ToDate,
            };
        }
        
    }
}
