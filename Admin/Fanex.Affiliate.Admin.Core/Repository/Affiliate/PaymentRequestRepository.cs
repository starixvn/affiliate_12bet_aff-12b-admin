using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using System;
using System.Collections.Generic;
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

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("TransID", transId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PaymentRequestDetail
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

            return null;
        }

        public PaymentUpdateResult UpdatePaymentRequest(PaymentUpdateParam param)
        {
            const string SpKey = "Aff_Admin_AffiliatePaymentRequest_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignResultOutputParam(objectDb);

                objectDb.SetParameterValue("DetailXml", DefaultValue.String);
                objectDb.SetParameterValue("OldData", DefaultValue.String);
                objectDb.SetParameterValue("NewData", DefaultValue.String);

                objectDb.ExecuteNonQuery(param);

                var resultInfo = GetResultInfo(objectDb);

                return new PaymentUpdateResult
                {
                    Code = resultInfo.Code,
                    Message = resultInfo.Message,
                    DetailXml = Convert.ToString(objectDb.GetParameterValue("DetailXml")),
                    OldData = Convert.ToString(objectDb.GetParameterValue("OldData")),
                    NewData = Convert.ToString(objectDb.GetParameterValue("NewData"))
                };
            }
        }

        public SiteDBConfigInfo GetSiteDBConfigInfoByCustId(string playerIbcCustId)
        {
            const string SpKey = "Acc_SiteDBConfigSel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.QueryEntity<SiteDBConfigInfo>( new { Custid = playerIbcCustId });
            }
        }

        public ResultInfo ApprovePaymentRequest(PaymentApproveParam param)
        {
            using (var objectDb = GetObjectDb(param.SpKey))
            {
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery(param);

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo RollbackApprovePaymentRequest(ResultInfo approveResult, string detailXml)
        {
            const string SpKey = "Aff_Admin_AffiliatePaymentRequest_Upd_Rollback";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("DetailXml", detailXml);
                objectDb.SetParameterValue("ErrCode", approveResult.Code);
                objectDb.SetParameterValue("ErrMsg", approveResult.Message);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public void WriteLogWhenUpdatePaymentRequestSuccessfully(PaymentUpdateLogParam param)
        {
            const string SpKey = "Aff_UserLog_RunSP_Detail_WriteLog";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.ExecuteNonQuery(param);
            }
        }
    }
}
