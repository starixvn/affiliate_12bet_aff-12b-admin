using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using System.Collections.Generic;
using System.Data;
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.Payment
{
    public class PaymentRequestRepository : BaseRepository, IPaymentRequestRepository
    {
        public EntityResult<PaymentRequest> GetPaymentRequestList(PaymentRequestParam param)
        {
            const string SpKey = "Aff_PaymentRequestSelList";
            var result = new EntityResult<PaymentRequest>();
            var dataList = new List<PaymentRequest>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Username", DbType.String, param.Username);
                objectDb.AddInParameter("Status", DbType.String, param.Status);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("GetAll", DbType.Boolean, param.IsGetAll);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new PaymentRequest
                        {
                            TransId = Convert.ToInt64(reader["Transid"]),
                            TransDate = Convert.ToDateTime(reader["Transdate"]),
                            Status = Convert.ToString(reader["Status"]),
                            AffName = Convert.ToString(reader["Username"]),
                            Amount = Convert.ToDecimal(reader["amt"]),
                            CompletedDate = Convert.ToDateTime(reader["Completeddate"]),
                            Creator = Convert.ToString(reader["Creator"])
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public PaymentRequestDetail GetPaymentRequestDetail(long transId)
        {
            const string SpKey = "Aff_PaymentRequestSelDetail";
            PaymentRequestDetail result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("TransID", DbType.Int32, transId);

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
                            FirstName = Convert.ToString(reader["firstname"]),
                            LastName = Convert.ToString(reader["lastname"]),
                            PaymentMethod = Convert.ToString(reader["PreferredPayment"]),
                            Amount = Convert.ToDecimal(reader["amt"]),
                            Status = Convert.ToString(reader["Status"]),
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

        public ResultInfo ApprovePaymentRequest(PaymentRequestUpdateParam param)
        {
            const string SpKey = "Aff_PaymentRequestApprove";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo RejectPaymentRequest(PaymentRequestUpdateParam param)
        {
            const string SpKey = "Aff_PaymentRequestUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Status", DbType.String, param.Status);
                AssignCommonParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignCommonParam(IObjectDb objectDb, PaymentRequestUpdateParam param)
        {
            objectDb.AddInParameter("TransID", DbType.Int64, param.TransId);
            objectDb.AddInParameter("AdminDesc", DbType.String, param.AdminDesc);
            objectDb.AddInParameter("AffDesc", DbType.String, param.AffDesc);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);
        }
    }
}
