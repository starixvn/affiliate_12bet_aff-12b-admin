using Fanex.Affiliate.Admin.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.UserAccount;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class MarketingAccountService : BaseDynamicService, IMarketingAccountService
    {
        public MarketingAccountService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public ResultInfo CreateMarketingAccount(CreateMarketingAccountModel model)
        {
            var command = model.CreateCommand();
            return ExecuteReturnResultInfo(command);
        }

        public MarketingAccount GetMarketingAccountInfo(int affId)
        {
            var criteria = new UserAccountCriteria { AffId = affId };

            return GetTData<MarketingAccount>(criteria);
        }

    }
}
