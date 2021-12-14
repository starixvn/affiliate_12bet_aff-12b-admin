using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface INewsLetterRepository
    {
        IEnumerable<NewsLetter> GetNewsLettersByDate(DateTime date);

        NewsLetter GetNewsLetterById(int id);
    }
}
