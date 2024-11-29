namespace Fanex.Affiliate.User.Core.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; } = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Error,
            ContractResolver = new DefaultContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JSON GET is not allowed");
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var scriptSerializer = JsonSerializer.Create(Settings);
            scriptSerializer.Serialize(response.Output, Data);
        }
    }
}