
namespace Fanex.Affiliate.Admin.Core.DTO.NewViewLog
{
    public class NewViewLogDetailInfo : NewViewLogBasicInfo
    {
        public int Id { get; set; }

        public string OldData { get; set; }

        public string NewData { get; set; }

        public string Ip { get; set; }
    }
}
