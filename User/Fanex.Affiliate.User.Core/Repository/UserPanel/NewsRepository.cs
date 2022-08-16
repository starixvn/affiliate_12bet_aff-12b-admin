using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class NewsRepository : INewsRepository
    {
        private readonly IObjectDBBuilder _builder;

        private const string MessagePrefix = "message_";

        public NewsRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public LatestHotNews GetLastestHotNews(bool isInternal)
        {
            const string uSpKey = "Aff_HotNewsDetail";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            var result = new LatestHotNews();

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.HotNewsList = GetMessageData(reader);

                        result.IsImportant = Convert.ToBoolean(reader["Important"]);
                    }
                }
            }

            return result;
        }

        public IEnumerable<MessageHistory> GetMessageList(MessageHistoryParam param)
        {
            const string uSpKey = "Aff_HotNewsSel";
            var mode = param.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                return db.Query<MessageHistory>(param);
            }
        }

        private static IDictionary<string, string> GetMessageData(IDataReader reader)
        {
            return new Dictionary<string, string>
            {
                ["en-US"] = Convert.ToString(reader[MessagePrefix + "en"]),
                ["zh-CN"] = Convert.ToString(reader[MessagePrefix + "cn"]),
                ["zh-TW"] = Convert.ToString(reader[MessagePrefix + "tw"]),
                ["es-ES"] = Convert.ToString(reader[MessagePrefix + "es"]),
                ["th-TH"] = Convert.ToString(reader[MessagePrefix + "th"]),
                ["vi-VN"] = Convert.ToString(reader[MessagePrefix + "vi"]),
                ["id-ID"] = Convert.ToString(reader[MessagePrefix + "id"]),
                ["km-KH"] = Convert.ToString(reader[MessagePrefix + "kh"]),
                ["ko-KR"] = Convert.ToString(reader[MessagePrefix + "kr"])
            };
        }
    }
}