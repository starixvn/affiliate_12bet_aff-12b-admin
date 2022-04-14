using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class PlayerDetailService : BaseRepository, IPlayerDetailService
    {
        public PlayerDetailResult GetPlayerDetailList(PlayerDetailParam param)
        {
            const string SpKey = "Aff_Admin_PlayersDetail_Sel";
            var result = new PlayerDetailResult();
            var dataList = new List<PlayerDetail>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(param, objectDb);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(CreatePlayerDetailItem(reader));
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        private static PlayerDetail CreatePlayerDetailItem(IDataReader reader)
        {
            return new PlayerDetail
            {
                IbcUserName = Convert.ToString(reader["IbcUserName"]),
                UserName = Convert.ToString(reader["UserName"]),
                RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"]),
                Deposit = Convert.ToDecimal(reader["Deposit"]),
                Withdrawal = Convert.ToDecimal(reader["Withdrawal"]),
                BankingFee = Convert.ToDecimal(reader["BankingFee"]),
                Bonus = Convert.ToDecimal(reader["Bonus"]),
                Turnover = Convert.ToDecimal(reader["Turnover"]),
                Winloss = Convert.ToDecimal(reader["Winloss"])
            };
        }

        private static void AssignParam(PlayerDetailParam param, IObjectDb objectDb)
        {
            objectDb.SetParameterValue("AffId", param.AffId);
            objectDb.SetParameterValue("ProductId", param.ProductId);
            objectDb.SetParameterValue("FromDate", param.FromDate);
            objectDb.SetParameterValue("ToDate", param.ToDate);
            objectDb.SetParameterValue("PageIndex", param.PageIndex);
            objectDb.SetParameterValue("PageSize", param.PageSize);
            objectDb.SetParameterValue("IsExportExcel", param.IsExportExcel);
            objectDb.SetParameterValue("IsSortAsc", param.IsSortAsc);
            objectDb.SetParameterValue("SortById", param.SortById);

            objectDb.SetParameterValue("NRows", DefaultValue.Int);
        }
    }
}
