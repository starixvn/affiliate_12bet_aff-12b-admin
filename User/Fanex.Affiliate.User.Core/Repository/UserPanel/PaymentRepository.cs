using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IObjectDBBuilder _builder;

        public PaymentRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<Payment> GetPaymentList(PaymentParam paymentParam)
        {
            const string uSpKey = "Aff_User_PaymentRequest_Sel";
            var dbMode = paymentParam.IsInternal ? DBMode.View : DBMode.Main;

            var paymentList = new List<Payment>();

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                AssignPaymentParam(db, paymentParam);

                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paymentList.Add(BuildPaymentItem(reader));
                    }
                }

                var balBroughtForward = db.GetParameterValue("BalBroughtForward");
                var preferredPayment = db.GetParameterValue("PreferredPayment");
                var minAmount = db.GetParameterValue("MinAmount");

                paymentParam.PreferredPayment = preferredPayment == DBNull.Value ? string.Empty : Convert.ToString(preferredPayment);
                paymentParam.BalBroughtForward = balBroughtForward == DBNull.Value ? 0m : Convert.ToDecimal(balBroughtForward);
                paymentParam.MinAmount = minAmount == DBNull.Value ? 0m : Convert.ToDecimal(minAmount);
            }

            return paymentList;
        }

        public ResultInfo CancelPaymentRequest(PaymentCancelParam cancelParam)
        {
            const string uSpKey = "Aff_User_PaymentRequest_Upd";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(cancelParam);
            }

            return new ResultInfo
            {
                IsOk = cancelParam.ErrCode == 0,
                Message = cancelParam.ErrMsg
            };
        }

        public ResultInfo CreatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            const string uSpKey = "Aff_PaymentRequestIns";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(paymentCreationParam);
            }

            return new ResultInfo
            {
                IsOk = paymentCreationParam.ErrCode == 0,
                Message = paymentCreationParam.ErrMsg
            };
        }

        private static void AssignPaymentParam(IObjectDb db, PaymentParam param)
        {
            db.AddInParameter("AffID", SqlDbType.Int, param.AffId);
            db.AddInParameter("Year", SqlDbType.Int, param.Year);

            db.AddOutParameter("PreferredPayment", SqlDbType.VarChar, DbParam.PreferPaymentSize);
            db.AddOutParameter("BalBroughtForward", SqlDbType.Money, DbParam.MoneySize);
            db.AddOutParameter("MinAmount", SqlDbType.Money, DbParam.MoneySize);
        }

        private static Payment BuildPaymentItem(IDataReader reader)
        {
            return new Payment
            {
                TransId = Convert.ToInt64(reader["TransID"]),
                RefId = Convert.ToInt64(reader["RefID"]),
                TransDate = Convert.ToDateTime(reader["TransDate"]),
                CompletedDate = Convert.ToDateTime(reader["CompletedDate"]),
                Status = Convert.ToString(reader["Status"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                AffDesc = Convert.ToString(reader["AffDesc"])
            };
        }
    }
}