using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public class PaymentRequestRepository : BaseRepository, IPaymentRequestRepository
    {
        public EntityResult<PaymentRequest> GetPaymentRequestList(PaymentRequestParam param)
        {
            const string SpKey = "Aff_Admin_AffiliatePaymentRequest_Sel";
            var result = new EntityResult<PaymentRequest>();
            var dataList = new List<PaymentRequest>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("SearchById", param.SearchBy);
                objectDb.SetParameterValue("Username", param.Username);
                objectDb.SetParameterValue(Field.Status, param.Status);
                objectDb.SetParameterValue("FromDate", param.FromDate);
                objectDb.SetParameterValue("ToDate", param.ToDate);
                objectDb.SetParameterValue("IsGetAll", param.IsGetAll);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new PaymentRequest
                        {
                            TransId = Convert.ToInt64(reader["Transid"]),
                            TransDate = Convert.ToDateTime(reader["Transdate"]),
                            Status = Convert.ToString(reader[Field.Status]),
                            AffName = Convert.ToString(reader["Username"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            CompletedDate = Convert.ToDateTime(reader["Completeddate"]),
                            Creator = Convert.ToString(reader["Creator"])
                        });
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public PaymentRequestDetail GetPaymentRequestDetail(long transId)
        {
            const string SpKey = "Aff_Admin_AffiliatePaymentRequest_Detail_Sel";
            PaymentRequestDetail result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("TransID", transId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new PaymentRequestDetail
                        {
                            TransId = Convert.ToInt64(reader["Transid"]),
                            AffId = Convert.ToInt32(reader["AffId"]),
                            AffName = Convert.ToString(reader["username"]),
                            TransDate = Convert.ToDateTime(reader["Transdate"]),
                            Email = Convert.ToString(reader["email"]),
                            FirstName = HttpUtility.HtmlDecode(Convert.ToString(reader["firstname"])),
                            LastName = HttpUtility.HtmlDecode(Convert.ToString(reader["lastname"])),
                            PaymentMethod = Convert.ToString(reader["PreferredPayment"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Status = Convert.ToString(reader[Field.Status]),
                            PlayerUsername = Convert.ToString(reader["Username2"]),
                            CompletedDate = Convert.ToDateTime(reader["Completeddate"]),
                            Creator = Convert.ToString(reader["adminname"]),
                            AdminDesc = Convert.ToString(reader["AdminDesc"]),
                            AffDesc = Convert.ToString(reader["AffDesc"])
                        };
                    }
                }
            }

            return result;
        }

        public ResultInfo UpdatePaymentRequest(PaymentRequestUpdateParam param)
        {
            const string SpKey = "Aff_Admin_AffiliatePaymentRequest_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.Status, param.Status);
                objectDb.SetParameterValue("TransID", param.TransId);
                objectDb.SetParameterValue("AdminDesc", param.AdminDesc);
                objectDb.SetParameterValue("AffDesc", param.AffDesc);
                objectDb.SetParameterValue("UserID", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }
    }
}
