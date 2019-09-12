using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository.Message
{
    public class MessageRepository : BaseRepository, IMessageRepository
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
                            UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]),
                            UpdatedBy = Convert.ToString(reader["updatename"]),
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
    }
}
