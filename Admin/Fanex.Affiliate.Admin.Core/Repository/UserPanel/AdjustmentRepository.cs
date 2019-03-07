
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class AdjustmentRepository : BaseRepository, IAdjustmentRepository
    {
        public decimal GetBalanceBroughtForward(int affId)
        {
            const string SpKey = "Aff_CustomerSelBalBF";

            decimal BalanceBf;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, affId);

                objectDb.AddOutParameter("BalBroughtForward", DbType.Decimal, 16);

                objectDb.ExecuteNonQuery();

                var BalBroughtForward = objectDb.GetParameterValue("BalBroughtForward");
                BalanceBf = BalBroughtForward == DBNull.Value ? 0 : Convert.ToInt32(BalBroughtForward);
            }

            return BalanceBf;
        }

        public ResultInfo InsertAdjustment(AdjustmentParam param)
        {
            const string SpKey = "Aff_AdjustmentIns";
            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("AdminID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Amount", DbType.Decimal, param.Amount);
                objectDb.AddInParameter("transdesc", DbType.String, param.Description);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                objectDb.AddOutParameter("transid", DbType.UInt64, 8);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}
