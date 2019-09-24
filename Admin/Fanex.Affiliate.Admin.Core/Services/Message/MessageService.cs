using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Repository;
using System.Data;
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Services.Message
{
    public class MessageService : BaseRepository, IMessageService
    {
        public EntityResult<MessageInfo> GetMessageList(MessageParam param)
        {
            string SpKey = "Aff_HotNewsSel";
            var result = new EntityResult<MessageInfo>();
            var dataList = new List<MessageInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Important", DbType.Int32, param.Important);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new MessageInfo
                        {
                            MessageId = Convert.ToInt32(reader["msgid"]),
                            Important = Convert.ToBoolean(reader["Important"]),
                            Creator = Convert.ToString(reader["creatorname"]),
                            PostedDate = Convert.ToDateTime(reader["PostedDate"]),
                            UpdatedDate = reader["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]),
                            UpdatedBy = reader["updatename"] == DBNull.Value ? string.Empty : Convert.ToString(reader["updatename"]),
                            Message = Convert.ToString(reader["message_en"])
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public MessageDetail GetMessageDetail(int selectedMessageId)
        {
            string SpKey = "Aff_HotNewsDetail";
            MessageDetail item = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("MsgID", DbType.Int32, selectedMessageId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new MessageDetail
                        {
                            MessageId = Convert.ToInt32(reader["msgid"]),
                            Important = Convert.ToBoolean(reader["Important"]),
                            MessageContentEn = Convert.ToString(reader["message_en"]),
                            MessageContentCn = Convert.ToString(reader["message_cn"]),
                            MessageContentTw = Convert.ToString(reader["message_tw"]),
                            MessageContentEs = Convert.ToString(reader["message_es"]),
                            MessageContentTh = Convert.ToString(reader["message_th"]),
                            MessageContentVi = Convert.ToString(reader["message_vi"]),
                            MessageContentId = Convert.ToString(reader["message_id"]),
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
            const string SpKey = "Aff_HotNewsIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter("id", DbType.Int32, 4);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateMessage(InsertUpdateMessageParam param)
        {
            const string SpKey = "Aff_HotNewsUpd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("id", DbType.Int32, param.MessageId);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteMessages(DeleteMessageParam param)
        {
            const string SpKey = "Aff_HotNewsDel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("listmsgid", DbType.String, param.MessageIdList);
                objectDb.AddInParameter("userid", DbType.Int32, param.UserId);
                objectDb.AddInParameter("ip", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateMessageParam param)
        {
            objectDb.AddInParameter("important", DbType.Boolean, param.Important);
            objectDb.AddInParameter("msgen", DbType.String, param.MessageContentEn);
            objectDb.AddInParameter("msgcn", DbType.String, param.MessageContentCn);
            objectDb.AddInParameter("msgtw", DbType.String, param.MessageContentTw);
            objectDb.AddInParameter("msges", DbType.String, param.MessageContentEs);
            objectDb.AddInParameter("msgth", DbType.String, param.MessageContentTh);
            objectDb.AddInParameter("msgvi", DbType.String, param.MessageContentVi);
            objectDb.AddInParameter("msgid", DbType.String, param.MessageContentId);
            objectDb.AddInParameter("msgkr", DbType.String, param.MessageContentKr);
            objectDb.AddInParameter("msgjp", DbType.String, param.MessageContentJp);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }
    }
}
