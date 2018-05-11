using System;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountRepository : IAccountRepository
    {
        public AccountInformationViewModel GetAccountInfo(int affId)
        {
            const string uSpKey = "Aff_AffiliateSelEditInfo";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                var parameterSet = new { affId };

                return objectDb.DB.QueryEntity<AccountInformationViewModel>(parameterSet);
            }
        }

        public bool IsValidDepositPlayer(string playerUserName)
        {
            const string uSpKey = "Deposit_GetCustDataForCreateIBCACByUserName";
            var isValid = false;

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                var parameterSet = new { Deposit_Username = playerUserName };

                var dataTable = objectDb.DB.ExecuteDataSet(parameterSet).Tables[0];

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    isValid = Convert.ToInt32(dataTable.Rows[0]["Success"]) == 0;
                }
            }

            return isValid;
        }

        public ResultInfo UpdateAccountInformation(AccountUpdateParam accountUpdateParam)
        {
            const string uSpKey = "Aff_AffiliateUpdInfo";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                objectDb.DB.ExecuteNonQuery(accountUpdateParam);
            }

            return new ResultInfo
            {
                IsOk = accountUpdateParam.ErrCode == 0,
                Message = accountUpdateParam.ErrMsg
            };
        }
    }
}