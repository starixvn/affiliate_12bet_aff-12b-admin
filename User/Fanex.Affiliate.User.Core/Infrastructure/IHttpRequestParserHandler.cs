using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Infrastructure
{
    public interface IHttpRequestParserHandler
    {
        QueryStringResult TryParseQueryString(string encryptedQuery);
    }
}
