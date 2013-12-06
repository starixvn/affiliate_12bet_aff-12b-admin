using System;
using System.Collections;
using System.Configuration;
using DNAide.Data;
/*
 * Class to handle Clicks Tracking
 * Author: Peter
 * Date : May 05,2009
 */
namespace DataHandler
{
    public class UserLog
    {
        public static DbResult LogTypeSel()
        {            
            return DbHelper.Execute("Aff_AffiliateSelLogType", new Hashtable(), DBType.AFFILIATE_12BET);
        }
        public static DbResult LogSel(int affiliateID, int logType, DateTime dateFrom, DateTime dateTo, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("logtype", logType);
            hsParam.Add("datefrom", dateFrom);
            hsParam.Add("dateto", dateTo);
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pagecount", 0);
            hsParam.Add("rowcount", 0);
            return DbHelper.Execute("Aff_AffiliateSelLog", hsParam, DBType.AFFILIATE_12BET);
        }
    }
}

