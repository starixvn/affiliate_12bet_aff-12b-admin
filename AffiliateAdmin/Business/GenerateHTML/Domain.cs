using System;
using System.Collections.Generic;
using System.Data;
using AffiliateAdmin.Business.DataAccess;
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
            var markets = new Dictionary<string, string>();

            if (hasAll)
            {
                markets.Add(string.Empty, "All");
            }

            foreach (DataRow row in rs.ResultSet.Tables[0].Rows)
            {
                markets.Add(row["MarketId"].ToString(), row["MarketName"].ToString());
            }

            return Common.GenerateDropdownHtml("slMarket", markets, selectedMarket);
        }

        public static string GenAffDomainList(string selectedDomain, bool hasAll = false)
        {
            DbResult rs = DataHandler.Domain.DomainAffSel();
            var domains = new Dictionary<string, string>();
            if (hasAll)
            {
                domains.Add(string.Empty, "All");
            }
            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                domains.Add(dr["DomainId"].ToString(), dr["DomainName"].ToString());
            }
            return Common.GenerateDropdownHtml("slAffDomain", domains, selectedDomain);
        }

        public static string Gen12BetDomainList(string deviceType, string selectedDomain, bool hasAll = false)
        {
            DbResult rs = DataHandler.Domain.GetList12BetDomain(deviceType);
            var domains = new Dictionary<string, string>();

            if (rs.ResultSet.Tables[0].Rows.Count == 0)
            {
                return Lang.None;
            }

            if (hasAll)
            {
                domains.Add(string.Empty, "All");
            }

            foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
            {
                domains.Add(dr["DomainId"].ToString(), dr["DomainName"].ToString());
            }

            return Common.GenerateDropdownHtml("sl12BetDomain", domains, selectedDomain);
        }

        public static string GenDeviceList(string id, string selectedDevice = "0", bool hasAll = false)
        {
            var table = new DataTable("table");
            table.Columns.Add(new DataColumn("value", Type.GetType("System.String")));
            table.Columns.Add(new DataColumn("name", Type.GetType("System.String")));

            var newRow = table.NewRow();
            newRow["value"] = "0";
            newRow["name"] = Lang.PCDevice;
            table.Rows.Add(newRow);

            newRow = table.NewRow();
            newRow["value"] = "1";
            newRow["name"] = Lang.MobileDevice;
            table.Rows.Add(newRow);

            return Common.GenDropDownFromTable(table, "value", "name", id, ref selectedDevice, hasAll);
        }
    }
}