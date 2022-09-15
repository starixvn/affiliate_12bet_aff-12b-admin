using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class StatisticsHelper
    {
        public static IEnumerable<SelectListItem> GetCountryList(IEnumerable<Country> countryList)
        {
            var dataList = countryList.Select(c => new SelectListItem
            {
                Value = c.CountryId,
                Text = Lang.ResourceManager.GetString(c.CountryName.Replace(" ", "")) ?? c.CountryName,
                Selected = true
            });

            return dataList;
        }
    }
}
