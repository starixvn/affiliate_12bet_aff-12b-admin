using System.Collections;
using AffiliateUser.Business.DataAccess;

/// <summary>
/// Summary description for Payment
/// </summary>
namespace DataHandler
{
    public static class Payment
    {
        public static Hashtable MinWithdraw()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");

            return DbHelper.Execute("Aff_MinAmountSel", hsParam, DBType.AFFILIATE_12BET).Outputs;
        }
    }
}
