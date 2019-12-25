using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Repository;

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

                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
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
            objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
            objectDb.AddInParameter("ProductId", DbType.Byte, param.ProductId);
            objectDb.AddInParameter("MonthDate", DbType.DateTime, param.MonthDate);
            objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
            objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);
            objectDb.AddInParameter("IsExportExcel", DbType.Boolean, param.IsExportExcel);
            objectDb.AddInParameter("IsSortAsc", DbType.Boolean, param.IsSortAsc);
            objectDb.AddInParameter("SortById", DbType.Int32, param.SortById);

            objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
        }
    }
}
