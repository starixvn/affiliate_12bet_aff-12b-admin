using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface INewsLetterRepository
    {
        IEnumerable<NewsLetter> GetNewsLettersByDate(DateTime date);

        NewsLetter GetNewsLetterById(int id);
    }
}
