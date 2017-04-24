using System.Collections;
using AffiliateAdmin.Business.DataAccess;
using Criterias;

namespace DataHandler
{
    public class Report
    {
        public static DbResult SBPlayerTurnoverReport(SBPlayerTurnoverReportCriteria criteria)
        {
            var hsParam = new Hashtable();
            hsParam.Add("username", criteria.Username);
            hsParam.Add("fromdate", criteria.DtmFDate);
            hsParam.Add("todate", criteria.DtmTDate);
            hsParam.Add("preferedlanguage", criteria.PreferredLanguage);
            hsParam.Add("isgetall", criteria.IsGetAll);

            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("nrows", 0);
            return DbHelper.Execute(criteria.SpName, DBType.AFFILIATE_12BET, hsParam);
        }

        public static DbResult CasinoPlayerTurnoverReport(CasinoPlayerTurnoverReportCriteria criteria)
        {
            var hsParam = new Hashtable();
            hsParam.Add("username", criteria.Username);
            hsParam.Add("fromdate", criteria.DtmFDate);
            hsParam.Add("todate", criteria.DtmTDate);
            hsParam.Add("preferedlanguage", criteria.PreferredLanguage);
            hsParam.Add("isgetall", criteria.IsGetAll);

            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("nrows", 0);
            return DbHelper.Execute(criteria.SpName, DBType.AFFILIATE_12BET, hsParam);
        }
    }
}

