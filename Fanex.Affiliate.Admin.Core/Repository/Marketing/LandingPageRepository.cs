using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Marketing;

namespace Fanex.Affiliate.Admin.Core.Repository.Marketing
{
    public class LandingPageRepository : ILandingPageRepository
    {
        public IEnumerable<LandingPage> GetLandingPageList()
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            throw new NotImplementedException();
        }
    }
}
