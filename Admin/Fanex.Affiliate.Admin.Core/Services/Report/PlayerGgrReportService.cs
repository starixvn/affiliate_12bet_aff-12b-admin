using Fanex.Affiliate.Admin.Core.Criterias.Report;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public class PlayerGgrReportService : BaseDynamicService, IPlayerGgrReportService
    {
        public PlayerGgrReportService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<PlayerGgrReport> GetPlayerGgrReportData(PlayerGgrReportParam param)
        {
            if (DateTimeHelper.IsValidDateRange(param.FromDate, param.ToDate))
            {
                try
                {
                    var criteria = GetPlayerGgrReportCriteria(param);

                    var (dataItemObjects, dataTotalObjects) = DynamicRepository.FetchMultiple<dynamic, dynamic>(criteria);

                    var selectedProductIdList = param.ProductIds.Split(',');

                    return new EntityResult<PlayerGgrReport>
                    {
                        MainData = BuildDataItemList(dataItemObjects, selectedProductIdList),
                        TotalItem = criteria.TotalItem,
                        Total = dataTotalObjects.Any() ? BuildTotalDataItem(dataTotalObjects.First(), selectedProductIdList) : null,
                    };
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex, "Cannot build PlayerGgr Report!");

                    return new EntityResult<PlayerGgrReport> { MainData = new List<PlayerGgrReport>() };
                }
            }

            return new EntityResult<PlayerGgrReport> { MainData = new List<PlayerGgrReport>() };
        }

        private static PlayerGgrReportCriteria GetPlayerGgrReportCriteria(PlayerGgrReportParam param)
        {
            return new PlayerGgrReportCriteria
            {
                AffUsername = param.AffName,
                Username = param.PlayerName,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                ProductIds = param.ProductIds,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                IsExportExcel = param.IsExportExcel,
                BaseCurrency = param.BaseCurrencyId,
            };
        }

        private static List<PlayerGgrReport> BuildDataItemList(IEnumerable<dynamic> dataItemObjects, string[] selectedProductIdList)
        {
            var dataList = new List<PlayerGgrReport>();

            foreach (var dataItem in dataItemObjects)
            {
                dataList.Add(BuildDataItem((IDictionary<string, object>)dataItem, selectedProductIdList));
            }

            return dataList;
        }

        private static PlayerGgrReport BuildDataItem(IDictionary<string, object> dataItem, string[] selectedProductIdList)
        {
            var playerGgrReport = BuildTotalDataItem(dataItem, selectedProductIdList);

            playerGgrReport.AffName = Convert.ToString(dataItem["AffUsername"]);
            playerGgrReport.PlayerName = Convert.ToString(dataItem["UserName"]);
            playerGgrReport.RegistrationDate = dataItem["RegisteredDate"] is null ? default(DateTime?) : Convert.ToDateTime(dataItem["RegisteredDate"]);
            playerGgrReport.FdmDate = dataItem["FirstDepositDate"] is null ? default(DateTime?) : Convert.ToDateTime(dataItem["FirstDepositDate"]);

            return playerGgrReport;
        }

        private static PlayerGgrReport BuildTotalDataItem(IDictionary<string, object> dataItem, string[] selectedProductIdList)
        {
            return new PlayerGgrReport
            {
                TotalTurnover = Convert.ToDecimal(dataItem["Total_Turnover"]),
                TotalGgr = Convert.ToDecimal(dataItem["Total_GGR"]),
                ProductReportList = BuildProductReportList(dataItem, selectedProductIdList),
            };
        }

        private static Dictionary<string, PlayerGgrProductInfo> BuildProductReportList(IDictionary<string, object> dataItem, string[] selectedProductIdList)
        {
            var productReportList = new Dictionary<string, PlayerGgrProductInfo>();

            foreach (var productId in selectedProductIdList)
            {
                var item = new PlayerGgrProductInfo
                {
                    Turnover = Convert.ToDecimal(dataItem[$"{productId}_Turnover"]),
                    Ggr = Convert.ToDecimal(dataItem[$"{productId}_GGR"])
                };

                productReportList.Add(productId, item);
            }

            return productReportList;
        }
    }
}
