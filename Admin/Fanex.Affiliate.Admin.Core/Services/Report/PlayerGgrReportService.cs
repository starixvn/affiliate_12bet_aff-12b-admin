using Fanex.Affiliate.Admin.Core.Criterias.Report;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using Newtonsoft.Json.Linq;
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

                    var (dataItemObjects, dataTotalObjects) = DynamicRepository.FetchMultiple<JObject, JObject>(criteria);

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

        private static List<PlayerGgrReport> BuildDataItemList(IEnumerable<JObject> dataItemObjects, string[] selectedProductIdList)
        {
            var dataList = new List<PlayerGgrReport>();

            foreach (var dataItem in dataItemObjects)
            {
                dataList.Add(BuildDataItem(dataItem, selectedProductIdList));
            }

            return dataList;
        }

        private static PlayerGgrReport BuildDataItem(JObject dataItem, string[] selectedProductIdList)
        {
            var playerGgrReport = BuildTotalDataItem(dataItem, selectedProductIdList);

            playerGgrReport.AffName = dataItem.Value<string>("AffUsername");
            playerGgrReport.PlayerName = dataItem.Value<string>("UserName");
            playerGgrReport.RegistrationDate = dataItem.Value<DateTime?>("RegisteredDate");
            playerGgrReport.FdmDate = dataItem.Value<DateTime?>("FirstDepositDate");

            return playerGgrReport;
        }

        private static PlayerGgrReport BuildTotalDataItem(JObject dataItem, string[] selectedProductIdList)
        {
            return new PlayerGgrReport
            {
                TotalTurnover = ConvertToDecimal(dataItem.Value<decimal?>("Total_Turnover")),
                TotalGgr = ConvertToDecimal(dataItem.Value<decimal?>("Total_GGR")),
                ProductReportList = BuildProductReportList(dataItem, selectedProductIdList),
            };
        }

        private static Dictionary<string, PlayerGgrProductInfo> BuildProductReportList(JObject dataItem, string[] selectedProductIdList)
        {
            var productReportList = new Dictionary<string, PlayerGgrProductInfo>();

            foreach (var productId in selectedProductIdList)
            {
                var item = new PlayerGgrProductInfo
                {
                    Turnover = ConvertToDecimal(dataItem.Value<decimal?>($"{productId}_Turnover")),
                    Ggr = ConvertToDecimal(dataItem.Value<decimal?>($"{productId}_GGR"))
                };

                productReportList.Add(productId, item);
            }

            return productReportList;
        }

        private static decimal ConvertToDecimal(decimal? data)
        {
            return data ?? 0;
        }
    }
}
