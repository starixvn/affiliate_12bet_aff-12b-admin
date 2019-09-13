using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Message
{
    public class InsertUpdateMessageParam: UserInfoParam
    {
        public int MessageId { get; set; }

        public bool Important { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentEn { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentCn { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentTw { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentEs { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentTh { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentVi { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentKr { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MessageContentJp { get; set; }

        public bool IsValid() {
            if (!(string.IsNullOrEmpty(MessageContentEn) && string.IsNullOrEmpty(MessageContentCn) && string.IsNullOrEmpty(MessageContentTw) && string.IsNullOrEmpty(MessageContentEs)))
            {
                return true;
            }

            if(!(string.IsNullOrEmpty(MessageContentTh) && string.IsNullOrEmpty(MessageContentVi) && string.IsNullOrEmpty(MessageContentId) && string.IsNullOrEmpty(MessageContentKr)))
            {
                return true;
            }

            return !string.IsNullOrEmpty(MessageContentJp);
        }
    }
}
