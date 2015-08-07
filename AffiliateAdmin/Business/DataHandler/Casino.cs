using System.Collections;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using System;

namespace DataHandler
{
    public class Casino
    {
        public static bool CasinoHasData(DateTime date)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("hascasinodata", 0);
            hsParam.Add("date", date);
            DbResult rs = DbHelper.Execute("Aff_CasinoHasData", DBType.AFFILIATE_12BET, hsParam);
            return CastAide.AsBoolean(rs.Outputs["hascasinodata"]);
        }

        public static DbResult CasinoSel(DateTime date)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("date", date);
            return DbHelper.Execute("Aff_CasinoSel", DBType.AFFILIATE_12BET, hsParam);
        }

        public static DbResult CasinoDataUpdate(string xmlData, bool isReplaceData, DateTime importDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("adminid", AppSettings.Session.CustID);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("isreplacedata", isReplaceData);
            hsParam.Add("importdate", importDate);            
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CasinoImportData", DBType.AFFILIATE_12BET, hsParam);
        }
    }
}
