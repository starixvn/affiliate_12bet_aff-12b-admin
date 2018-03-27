namespace Fanex.Affiliate.User.Core.DTO
{
    using Newtonsoft.Json;

    public class JResult
    {
        public bool IsOk { get; set; }

        [JsonProperty("Msg")]
        public bool Message { get; set; }
    }
}