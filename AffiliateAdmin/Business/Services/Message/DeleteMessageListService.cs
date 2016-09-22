using System.Text;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Message
{
    public class DeleteMessageListService
    {
        public ResultInfo DeleteMessageList(string idList)
        {
            return new ResultInfo { Message = DataHandler.HotNews.DeleteHotNews(idList) };
        }
    }
}