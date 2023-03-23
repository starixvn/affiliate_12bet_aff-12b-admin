using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Services.Message
{
    public class MessageService : BaseRepository, IMessageService
    {
        public EntityResult<MessageInfo> GetMessageList(MessageParam param)
        {
            const string SpKey = "Aff_Admin_Message_Sel";
            var result = new EntityResult<MessageInfo>();
            var dataList = new List<MessageInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("SearchById", param.SearchById);
                objectDb.SetParameterValue("IsGetAll", param.IsGetAll);
                objectDb.SetParameterValue("FromDate", param.FromDate);
                objectDb.SetParameterValue("ToDate", param.ToDate);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new MessageInfo
                        {
                            MessageId = Convert.ToInt32(reader[Field.MessageId]),
                            Important = Convert.ToBoolean(reader["Important"]),
                            Creator = Convert.ToString(reader["creatorname"]),
                            PostedDate = reader["PostedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["PostedDate"]),
                            UpdatedDate = reader["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]),
                            UpdatedBy = Convert.ToString(reader["UpdatedName"]),
                            Message = Convert.ToString(reader["message_en"])
                        });
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public MessageDetail GetMessageDetail(int selectedMessageId)
        {
            const string SpKey = "Aff_Admin_Message_Sel";
            MessageDetail item = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.MessageId, selectedMessageId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new MessageDetail
                        {
                            MessageId = Convert.ToInt32(reader[Field.MessageId]),
                            Important = Convert.ToBoolean(reader["Important"]),
                            MessageContentEn = Convert.ToString(reader["message_en"]),
                            //MessageContentBn = Convert.ToString(reader["message_bn"]),
                            MessageContentCn = Convert.ToString(reader["message_cn"]),
                            MessageContentTw = Convert.ToString(reader["message_tw"]),
                            MessageContentEs = Convert.ToString(reader["message_es"]),
                            MessageContentTh = Convert.ToString(reader["message_th"]),
                            MessageContentVi = Convert.ToString(reader["message_vi"]),
                            MessageContentId = Convert.ToString(reader["message_id"]),
                            MessageContentKh = Convert.ToString(reader["message_kh"]),
                            MessageContentKr = Convert.ToString(reader["message_kr"]),
                            MessageContentJp = Convert.ToString(reader["message_jp"])
                        };
                    }
                }
            }

            return item;
        }

        public ResultInfo InsertMessage(InsertUpdateMessageParam param)
        {
            const string SpKey = "Aff_Admin_Message_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateMessage(InsertUpdateMessageParam param)
        {
            const string SpKey = "Aff_Admin_Message_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.MessageId, param.MessageId);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteMessages(DeleteMessageParam param)
        {
            const string SpKey = "Aff_Admin_Message_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("MessageIds", param.MessageIdList);
                objectDb.SetParameterValue("userid", param.UserId);
                objectDb.SetParameterValue("ip", param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateMessageParam param)
        {
            objectDb.SetParameterValue("IsImportant", param.Important);
            objectDb.SetParameterValue("msgen", param.MessageContentEn);
            //objectDb.SetParameterValue("msgbn", param.MessageContentBn);
            objectDb.SetParameterValue("msgcn", param.MessageContentCn);
            objectDb.SetParameterValue("msgtw", param.MessageContentTw);
            objectDb.SetParameterValue("msges", param.MessageContentEs);
            objectDb.SetParameterValue("msgth", param.MessageContentTh);
            objectDb.SetParameterValue("msgvi", param.MessageContentVi);
            objectDb.SetParameterValue("msgid", param.MessageContentId);
            objectDb.SetParameterValue("msgkh", param.MessageContentKh);
            objectDb.SetParameterValue("msgkr", param.MessageContentKr);
            objectDb.SetParameterValue("msgjp", param.MessageContentJp);
            objectDb.SetParameterValue("UserID", param.UserId);
            objectDb.SetParameterValue("IP", param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
