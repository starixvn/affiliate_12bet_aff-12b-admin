using DNAide.Utils;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public class MassAdjustmentRepository : BaseRepository, IMassAdjustmentRepository
    {
        public AdjustmentResultInfo UpdateMassAdjusment(MassAdjustmentParam param)
        {
            const string SpKey = "Aff_Admin_Adjustment_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("XmlData", DbType.String, param.XmlData);
                objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();
                var result = GetResultInfo(objectDb);

                if(!result.IsSuccessful)
                {
                    using (var reader = objectDb.ExecuteReader())
                    {
                        var errorList = new List<ErroredAdjustmentInfo>();

                        while (reader.Read())
                        {
                            errorList.Add(new ErroredAdjustmentInfo
                            {
                                Username = Convert.ToString(reader["Username"]),
                                Amount = NumberFormat.DecFormat(Convert.ToDecimal(reader["Amount"]), Money.DecDigits),
                                Reason = Convert.ToString(reader["ErrorMsg"]),
                            });
                        }

                        return new AdjustmentResultInfo
                        {
                            Result = result,
                            ErroredAdjustmentInfoList = errorList
                        };
                    }
                }

                return new AdjustmentResultInfo
                {
                    Result = result
                };
            }
        }
    }
}
