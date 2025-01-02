namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using MetalModels.UserPanel;
    using Utilities;

    public class NewsService : BaseDynamicService, INewsService
    {
        private readonly IMemoryCacheService _cacheService;

        private const string MessagePrefix = "message_";
        private const string ImportantPropName = "important";

        public NewsService(IDynamicRepository dynamicRepository, IMemoryCacheService cacheService) : base(dynamicRepository)
        {
            _cacheService = cacheService;
        }

        public LatestHotNews GetLatestHotNews(bool isInternal)
        {
            var hotNews = _cacheService.Get<LatestHotNews>(CacheKey.LatestHotNews);

            if (hotNews == null)
            {
                var result = GetTData<dynamic>(new GetLastestHotNewsCriteria(isInternal));

                hotNews = GetMessageData(result);

                if (hotNews != null)
                {
                    _cacheService.Set(CacheKey.LatestHotNews, hotNews);
                }
            }

            return hotNews ?? new LatestHotNews { HotNewsList = new Dictionary<string, string>() };
        }

        public IEnumerable<MessageHistory> GetMessageList(MessageHistoryParam param)
        {
            return FetchTDataWithPaging<MessageHistory>(param.MapTo<GetMessageListCriteria>(), param);
        }

        private static LatestHotNews GetMessageData(dynamic obj)
        {
            if (obj is null)
            {
                return null;
            }

            var result = new LatestHotNews();

            var languagePropertyMapping = new Dictionary<string, string>
            {
                ["en-US"] = Convert.ToString(obj[$"{MessagePrefix}en"]),
                ["bn-BD"] = Convert.ToString(obj["Message_Bn"]),
                ["th-TH"] = Convert.ToString(obj[$"{MessagePrefix}th"]),
                ["vi-VN"] = Convert.ToString(obj[$"{MessagePrefix}vi"]),
                ["id-ID"] = Convert.ToString(obj[$"{MessagePrefix}id"]),
                ["km-KH"] = Convert.ToString(obj["Message_Kh"]),
                ["zh-CN"] = Convert.ToString(obj[$"{MessagePrefix}cn"]),
                ["hi-IN"] = Convert.ToString(obj[$"{MessagePrefix}hi"])
            };

            result.HotNewsList = languagePropertyMapping;
            result.IsImportant = Convert.ToBoolean(obj[ImportantPropName]);

            return result;
        }

    }
}
