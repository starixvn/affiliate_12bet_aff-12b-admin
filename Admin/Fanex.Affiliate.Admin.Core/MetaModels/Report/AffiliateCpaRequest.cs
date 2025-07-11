using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using Fanex.Affiliate.Admin.Core.Criterias.Report;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Report
{
    public class AffiliateCpaRequest : PaginationRequest
    {
        [MaxLength(Validation.MaxLengthInputString, ErrorMessage = "Affiliate must shorter than 30 characters!")]
        public string Username { get; set; }

        [MaxLength(Validation.Hundred, ErrorMessage = "Player must shorter than 100 characters!")]
        public string DealName { get; set; }

        public short ViewBy { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public short SortType { get; set; }

        public bool SortDirectionType { get; set; }

        public AffiliateCpaReportCriteria CreateCriteria()
        {
            return new AffiliateCpaReportCriteria
            {
                Username = Username,
                DealName = DealName,
                FromDate = FromDate,
                ToDate = ToDate,
                PageIndex = PageIndex,
                PageSize = PageSize,
                SortType = SortType,
                SortDirectionType = SortDirectionType,
                ViewBy = ViewBy
            };
        }
    }
}
