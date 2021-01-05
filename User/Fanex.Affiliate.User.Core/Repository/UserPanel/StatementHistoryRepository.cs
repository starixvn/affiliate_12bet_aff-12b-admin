using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class StatementHistoryRepository : IStatementHistoryRepository
    {
        private readonly IObjectDBBuilder _builder;

        public StatementHistoryRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam)
        {
            const string uSpKey = "Aff_User_StatementHistory_Sel";
            var dbMode = statementHistoryParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<StatementHistory>(statementHistoryParam);
            }
        }

        public IEnumerable<ProductItem> GetProductList(bool isInternal)
        {
            const string uSpKey = "Aff_ProductList_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<ProductItem>();
            }
        }

        public DailyEarningResult GetDailyEarning(DailyEarningParam dailyEarningParam)
        {
            const string uSpKey = "Aff_DailyEarning_Sel";
            var dbMode = dailyEarningParam.IsInternal ? DBMode.View : DBMode.Main;

            var dailyEarningResult = new DailyEarningResult();

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                using (var multipleReader = db.QueryMultiple(dailyEarningParam))
                {
                    dailyEarningResult.MainDataList = multipleReader.Read<DailyEarning>();
                    dailyEarningResult.Total = multipleReader.ReadFirstOrDefault<DailyEarning>();
                }
            }

            return dailyEarningResult;
        }

        public ExtraUserInfo GetExtraUserInfo(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_AffiliateUser_ExtraUserInfoSel";
            var mode = isInternal ? DBMode.View : DBMode.Main;
            var result = new ExtraUserInfo();

            using (var db = _builder.Build(uSpKey, mode))
            {
                db.AddInParameter("AffId", System.Data.SqlDbType.Int, affId);
                result = db.QueryEntity<ExtraUserInfo>() ?? result;
            }

            return result;
        }

        public IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam earningDetailParam)
        {
            const string uSpKey = "Aff_DailyEarning_Detail_Sel";
            var mode = earningDetailParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                return db.Query<DailyEarningDetail>(earningDetailParam);
            }
        }
    }
}