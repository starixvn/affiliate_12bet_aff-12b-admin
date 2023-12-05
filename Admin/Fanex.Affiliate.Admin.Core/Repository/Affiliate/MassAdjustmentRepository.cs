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
            var errorList = new List<ErroredAdjustmentInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("XmlData", param.XmlData);
                objectDb.SetParameterValue("UserID", param.UserId);
                objectDb.SetParameterValue("Ip", param.Ip);
                AssignResultOutputParam(objectDb);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        errorList.Add(new ErroredAdjustmentInfo
                        {
                            Username = Convert.ToString(reader["Username"]),
                            Amount = FormatAmount(Convert.ToString(reader["Amount"])),
                            Reason = Convert.ToString(reader["ErrorMsg"]),
                        });
                    }
                }

                var result = GetResultInfo(objectDb);

                return new AdjustmentResultInfo
                {
                    Result = result,
                    ErroredAdjustmentInfoList = errorList
                };
            }
        }

        private static string FormatAmount(string amount)
        {
            if (decimal.TryParse(amount, out decimal amountNumber))
            {
                return NumberFormat.DecFormat(amountNumber, Money.DecDigits);
            }

            return amount;
        }
    }
}
