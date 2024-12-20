using Fanex.Affiliate.Admin.Core.Criterias.Report;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public class TransferReportService : BaseDynamicService, ITransferReportService
    {
        public TransferReportService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<TransferReport> GetTransferReportData(TransferReportParam param)
        {
            if (DateTimeHelper.IsValidDateTime(param.FromDate, param.ToDate))
            {
                var criteria = GetTransferReportCriteria(param);

                var entityResult = FetchEntityResult<TransferReport>(criteria);

                if (criteria.TotalBasedAmount != null)
                {
                    entityResult.Total = new TransferReport { BasedAmount = criteria.TotalBasedAmount.Value };
                }

                return entityResult;
            }

            return new EntityResult<TransferReport> { MainData = new List<TransferReport>() };
        }

        private static TransferReportCriteria GetTransferReportCriteria(TransferReportParam param)
        {
            return new TransferReportCriteria
            {
                TransId = param.TransId,
                AffName = param.AffName,
                PlayerName = param.PlayerName,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                IsExportExcel = param.IsExportExcel,
                BaseCurrency = param.BaseCurrencyId,
            };
        }
    }
}
