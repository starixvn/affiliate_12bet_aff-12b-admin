using System;
using System.Runtime.Serialization;

namespace Fanex.Affiliate.DataService.Importer.Core.Exceptions
{
    public class ThirdPartyProductException : Exception
    {
        public ThirdPartyProductException()
        {
        }

        public ThirdPartyProductException(string message)
            : base(message)
        {
        }

        public ThirdPartyProductException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected ThirdPartyProductException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
