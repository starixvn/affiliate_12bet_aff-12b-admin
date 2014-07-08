using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using Resources;

/// <summary>
/// Summary description for Domain
/// </summary>
namespace AffAdmin.GenerateHTML
{
    public class Domain
    {
	    public Domain()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        public static string GenMarketList(string selectedMarket, bool hasAll = false)
        {
            DbResult rs = DataHandler.Domain.MarketSel();
            ArrayList arrText = new ArrayList();
            ArrayList arrValue = new ArrayList();

            if (hasAll)
            {
                arrText.Add("All");
                arrValue.Add("");
            }

            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                arrText.Add(dr["MarketName"]);
                arrValue.Add(dr["MarketId"]);
            }
            return Common.GenerateSelectObj("slMarket", arrValue, arrText, selectedMarket);
        }

        public static string GenAffDomainList(string selectedDomain, bool hasAll = false)
        {
            DbResult rs = DataHandler.Domain.DomainAffSel();
            ArrayList arrText = new ArrayList();
            ArrayList arrValue = new ArrayList();
            
            if (hasAll)
            {
                arrText.Add("All");
                arrValue.Add("");
            }
            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                arrText.Add(dr["DomainName"]);
                arrValue.Add(dr["DomainId"]);
            }
            return Common.GenerateSelectObj("slAffDomain", arrValue, arrText, selectedDomain);
        }

        public static string Gen12BetDomainList(string deviceType, string selectedDomain, bool hasAll = false)
        {
            DbResult rs = DataHandler.Domain.GetList12BetDomain(deviceType);
            var arrText = new ArrayList();
            var arrValue = new ArrayList();

            if (rs.ResultSet.Tables[0].Rows.Count == 0)
            {
                return Lang.None;
            }

            if (hasAll)
            {
                arrText.Add("All");
                arrValue.Add("");
            }

            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                arrText.Add(dr["DomainName"]);
                arrValue.Add(dr["DomainId"]);
            }

            return Common.GenerateSelectObj("sl12BetDomain", arrValue, arrText, selectedDomain);
        }

        public static string GenDeviceList(string selectedDevice = "0", bool hasAll = false)
        {
            var arrText = new ArrayList();
            var arrValue = new ArrayList();

            if (hasAll)
            {
                arrText.Add("All");
                arrValue.Add("");
            }

            arrText.Add("PC / Laptop");
            arrValue.Add("0");
            arrText.Add("Mobile");
            arrValue.Add("1");

            return Common.GenerateSelectObj("slDevice", arrValue, arrText, selectedDevice);
        }
    }
}