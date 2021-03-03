using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface INewsLetterService
    {
        Task<string> GetCurrentContent();

        Task<string> GetContentById(int id);

        bool HasNewCurrentTime();
    }
}
