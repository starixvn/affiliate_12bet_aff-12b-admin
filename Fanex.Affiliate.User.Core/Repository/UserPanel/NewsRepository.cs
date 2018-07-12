using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class NewsRepository : INewsRepository
    {
        private const string MessagePrefix = "message_";

        public LatestHotNews GetLastestHotNews(bool isInternal)
        {
            const string uSpKey = "Aff_HotNewsDetail";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            var result = new LatestHotNews();

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                using (var reader = objectDb.DB.ExecuteReader())
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

            using (var objectDb = new ObjectDBBuilder(uSpKey, mode))
            {
                return objectDb.DB.Query<MessageHistory>(param);
            }
        }

        private static IDictionary<string, string> GetMessageData(IDataReader reader)
        {
            return new Dictionary<string, string>
            {
                ["en-US"] = reader[MessagePrefix + "en"]?.ToString(),
                ["zh-CN"] = reader[MessagePrefix + "cn"]?.ToString(),
                ["zh-TW"] = reader[MessagePrefix + "tw"]?.ToString(),
                ["es-ES"] = reader[MessagePrefix + "es"]?.ToString(),
                ["th-TH"] = reader[MessagePrefix + "th"]?.ToString(),
                ["vi-VN"] = reader[MessagePrefix + "vi"]?.ToString(),
                ["id-ID"] = reader[MessagePrefix + "id"]?.ToString(),
                ["ko-KR"] = reader[MessagePrefix + "kr"]?.ToString()
            };
        }
    }
}