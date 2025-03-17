using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Message
{
    public class InsertUpdateMessageParam : UserInfoParam
    {
        public int MessageId { get; set; }

        public bool Important { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message_en { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message_cs { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message_th { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message_vn { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message_id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Message_ko { get; set; }

        public bool IsValid()
        {
            if (!(string.IsNullOrEmpty(Message_en) && string.IsNullOrEmpty(Message_cs) && string.IsNullOrEmpty(Message_ko)))
            {
                return true;
            }

            return !(string.IsNullOrEmpty(Message_th) && string.IsNullOrEmpty(Message_vn) && string.IsNullOrEmpty(Message_id));
        }
    }
}
