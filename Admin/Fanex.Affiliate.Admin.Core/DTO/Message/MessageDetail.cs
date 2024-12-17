using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.Message
{
    public class MessageDetail
    {
        public int MessageId { get; set; }

        public bool Important { get; set; }

        [JsonIgnore]
        public string Message_En { get; set; }

        public string MessageContenten => Message_En;

        [JsonIgnore]
        public string Message_Cn { get; set; }

        public string MessageContentcs => Message_Cn;

        [JsonIgnore]
        public string Message_Th { get; set; }

        public string MessageContentth => Message_Th;

        [JsonIgnore]
        public string Message_Vi { get; set; }

        public string MessageContentvn => Message_Vi;

        [JsonIgnore]
        public string Message_Id { get; set; }

        public string MessageContentid => Message_Id;

        [JsonIgnore]
        public string Message_Hi { get; set; }

        public string MessageContenthi => Message_Hi;

        [JsonIgnore]
        public string Message_Ms { get; set; }

        public string MessageContentms => Message_Ms;

        [JsonIgnore]
        public string Message_Bn { get; set; }

        public string MessageContentbn => Message_Bn;

        [JsonIgnore]
        public string Message_Kh { get; set; }

        public string MessageContentkh => Message_Kh;
    }
}
