using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.ViewModels.Report
{
    public class AffiliateReportViewModel : ReportCommonViewModel
    {
        public IEnumerable<Language> LanguageList { get; set; }

        public IEnumerable<Currency> CurrencyList { get; set; }

        public IEnumerable<Currency> PaymentCurrencyList { get; set; }

        public IEnumerable<NormalCommGroup> CommGroupList { get; set; }

        public IEnumerable<CommGroupType> CommGroupTypes { get; set; }

        public DateTime ReportCutOffDate { get; set; }
    }
}
