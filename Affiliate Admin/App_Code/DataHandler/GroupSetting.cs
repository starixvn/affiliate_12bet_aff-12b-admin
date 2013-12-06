using System;
using System.Collections;
using System.Data;
using DNAide.Data;
using GenerateHTML;
using System.Text;
using System.Web;
namespace DataHandler
{
    
    public class GroupSetting
    {
        public static DbResult CommGroupSel(string strGroupName,object fromdate, object todate, int pagesize, int pageindex)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("groupname", strGroupName);
             if (fromdate != null && todate != null)
            {
                hsParams.Add("fromdate", fromdate);
                hsParams.Add("todate", Convert.ToDateTime(todate).AddDays(1).AddMilliseconds(-3));
            }
             hsParams.Add("totalnonegroup", 0);
             hsParams.Add("pagesize", Convert.ToInt32(pagesize));
             hsParams.Add("pageindex", pageindex);
             hsParams.Add("totalpages", 0);
             hsParams.Add("nrows", 0);
            return DbHelper.Execute("Aff_CommGroupSel", hsParams, DBType.AFFILIATE_12BET);
        }
        public static DbResult CommGroupDel(int groupID,string ip)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", AppSettings.Session.CustID);
            hsParams.Add("groupid", groupID);
            hsParams.Add("ip",ip );
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupDel", hsParams, DBType.AFFILIATE_12BET);
        }
        //public static DbResult CommGroupSelAll(string strGroupName)
        //{
        //    return DbHelper.Execute("Aff_CommGroupSelAll", null, DBType.AFFILIATE_12BET);
        //}
        public static DbResult CommGroupSelDetail(int groupID)
        {
            Hashtable hsParams = new Hashtable();            
            hsParams.Add("groupid", groupID);            
            return DbHelper.Execute("Aff_CommGroupSelDetail", hsParams, DBType.AFFILIATE_12BET);
        }
        public static DbResult CommGroupSelCommonSetting()
        {            
            return DbHelper.Execute("Aff_CommGroupSelCommonSetting", null, DBType.AFFILIATE_12BET);
        }
        
        public static DbResult CommGroupIns(GroupSettingObj obj, string ip)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", AppSettings.Session.CustID);
           
            hsParams.Add("groupid", obj.id);
            if (obj.isApproved)
            {
                hsParams.Add("isapproved", 1);
                hsParams.Add("password", obj.password);
            }
           
            hsParams.Add("groupname", HttpContext.Current.Server.HtmlEncode(obj.groupName));
            hsParams.Add("description", HttpContext.Current.Server.HtmlEncode(obj.description));
            StringBuilder sportIDList = new StringBuilder(), deductionList = new StringBuilder(), deductedPercentageList = new StringBuilder(), tierList = new StringBuilder(), minAmountList = new StringBuilder()
                              , maxAmountList = new StringBuilder(), minQMList = new StringBuilder(), maxQMList = new StringBuilder(), commissionList = new StringBuilder();
            SportSettingObj copyFromSport = GetCopySportSetting(obj);
            foreach (SportSettingObj s in obj.sportSetting)
            {
                if (s.isEnabled)
                {
                    var t = s;
                    if (copyFromSport != null)
                    {
                        t = copyFromSport;
                    }
                    sportIDList.Append(s.id.ToString());

                    sportIDList.Append(";");
                    tierList.Append(t.commissions.Count.ToString());
                    tierList.Append(";");
                    foreach (DeductionTypeObj d in s.deductions)
                    {
                        if (d.isCheck)
                        {
                            deductionList.Append(d.id.ToString());
                            deductionList.Append(",");
                            deductedPercentageList.Append(d.deductedPercentage.ToString());
                            deductedPercentageList.Append(",");
                        }
                    }
                    for (int i = 0; i < t.commissions.Count; i++)
                    {
                        minAmountList.Append(t.minAmounts[i].ToString());
                        minAmountList.Append(",");
                        maxAmountList.Append(t.maxAmounts[i].ToString());
                        maxAmountList.Append(",");
                        minQMList.Append(t.minQMs[i].ToString());
                        minQMList.Append(",");
                        maxQMList.Append(t.maxQMs[i].ToString());
                        maxQMList.Append(",");
                        commissionList.Append(t.commissions[i]);
                        commissionList.Append("@");
                    }
                    minAmountList.Remove(minAmountList.Length - 1, 1);
                    maxAmountList.Remove(maxAmountList.Length - 1, 1);
                    minQMList.Remove(minQMList.Length - 1, 1);
                    maxQMList.Remove(maxQMList.Length - 1, 1);
                    commissionList.Remove(commissionList.Length - 1, 1);
                    minAmountList.Append(";");
                    maxAmountList.Append(";");
                    minQMList.Append(";");
                    maxQMList.Append(";");
                    commissionList.Append(";");

                }
            }
            sportIDList.Remove(sportIDList.Length - 1, 1);
            tierList.Remove(tierList.Length - 1, 1);
            if (deductionList.Length > 0)
            {
                deductionList.Remove(deductionList.Length - 1, 1);
                deductedPercentageList.Remove(deductedPercentageList.Length - 1, 1);
            }
            minAmountList.Remove(minAmountList.Length - 1, 1);
            maxAmountList.Remove(maxAmountList.Length - 1, 1);
            minQMList.Remove(minQMList.Length - 1, 1);
            maxQMList.Remove(maxQMList.Length - 1, 1);
            commissionList.Remove(commissionList.Length - 1, 1);

            hsParams.Add("sportidlist", sportIDList.ToString());
            hsParams.Add("deductionlist", deductionList.ToString());
            hsParams.Add("deductedpercentagelist", deductedPercentageList.ToString());
            hsParams.Add("tierlist", tierList.ToString());
            hsParams.Add("minamountlist", minAmountList.ToString());
            hsParams.Add("maxamountlist", maxAmountList.ToString());
            hsParams.Add("minqmlist", minQMList.ToString());
            hsParams.Add("maxqmlist", maxQMList.ToString());
            hsParams.Add("commissionlist", commissionList.ToString());
            hsParams.Add("mailoption", false);
            hsParams.Add("ip", ip);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupIns", hsParams, DBType.AFFILIATE_12BET);
        }
        public static SportSettingObj GetCopySportSetting(GroupSettingObj obj)
        {
            if (obj.copySportID != -1)
            {
                foreach (SportSettingObj s in obj.sportSetting)
                {
                    if (s.isEnabled && s.id == obj.copySportID)
                        return s;
                }
            }
            return null;
        }
        public static DbResult CommGroupUpd(GroupSettingObj obj, string ip)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", AppSettings.Session.CustID);
            hsParams.Add("groupid", obj.id);
            hsParams.Add("groupname", HttpContext.Current.Server.HtmlEncode(obj.groupName));
            hsParams.Add("description", HttpContext.Current.Server.HtmlEncode(obj.description));
            StringBuilder sportIDList = new StringBuilder(), deductionList = new StringBuilder(), deductedPercentageList = new StringBuilder(), tierList = new StringBuilder(), minAmountList = new StringBuilder()
                              , maxAmountList = new StringBuilder(), minQMList = new StringBuilder(), maxQMList = new StringBuilder(), commissionList = new StringBuilder();
            SportSettingObj copyFromSport = GetCopySportSetting(obj);
            foreach (SportSettingObj s in obj.sportSetting)
            {
                if (s.isEnabled)
                {
                    var t = s;
                    if (copyFromSport != null)
                    {
                        t = copyFromSport;
                    }
                    sportIDList.Append(s.id.ToString());

                    sportIDList.Append(";");
                    tierList.Append(t.commissions.Count.ToString());
                    tierList.Append(";");
                    foreach (DeductionTypeObj d in s.deductions)
                    {
                        if (d.isCheck)
                        {
                            deductionList.Append(d.id.ToString());
                            deductionList.Append(",");
                            deductedPercentageList.Append(d.deductedPercentage.ToString());
                            deductedPercentageList.Append(",");
                        }
                    }
                    for (int i = 0; i < t.commissions.Count; i++)
                    {
                        minAmountList.Append(t.minAmounts[i].ToString());
                        minAmountList.Append(",");
                        maxAmountList.Append(t.maxAmounts[i].ToString());
                        maxAmountList.Append(",");
                        minQMList.Append(t.minQMs[i].ToString());
                        minQMList.Append(",");
                        maxQMList.Append(t.maxQMs[i].ToString());
                        maxQMList.Append(",");
                        commissionList.Append(t.commissions[i]);
                        commissionList.Append("@");
                    }
                    minAmountList.Remove(minAmountList.Length - 1, 1);
                    maxAmountList.Remove(maxAmountList.Length - 1, 1);
                    minQMList.Remove(minQMList.Length - 1, 1);
                    maxQMList.Remove(maxQMList.Length - 1, 1);
                    commissionList.Remove(commissionList.Length - 1, 1);
                    minAmountList.Append(";");
                    maxAmountList.Append(";");
                    minQMList.Append(";");
                    maxQMList.Append(";");
                    commissionList.Append(";");

                }
            }
            sportIDList.Remove(sportIDList.Length - 1, 1);
            tierList.Remove(tierList.Length - 1, 1);
            if (deductionList.Length > 0)
            {
                deductionList.Remove(deductionList.Length - 1, 1);
                deductedPercentageList.Remove(deductedPercentageList.Length - 1, 1);
            }
            minAmountList.Remove(minAmountList.Length - 1, 1);
            maxAmountList.Remove(maxAmountList.Length - 1, 1);
            minQMList.Remove(minQMList.Length - 1, 1);
            maxQMList.Remove(maxQMList.Length - 1, 1);
            commissionList.Remove(commissionList.Length - 1, 1);

            hsParams.Add("sportidlist", sportIDList.ToString());
            hsParams.Add("deductionlist", deductionList.ToString());
            hsParams.Add("deductedpercentagelist", deductedPercentageList.ToString());
            hsParams.Add("tierlist", tierList.ToString());
            hsParams.Add("minamountlist", minAmountList.ToString());
            hsParams.Add("maxamountlist", maxAmountList.ToString());
            hsParams.Add("minqmlist", minQMList.ToString());
            hsParams.Add("maxqmlist", maxQMList.ToString());
            hsParams.Add("commissionlist", commissionList.ToString());
            hsParams.Add("ip", ip);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupUpd", hsParams, DBType.AFFILIATE_12BET);
        }

        public static DbResult CommGroupSelListNormal(int affID)
        {
            Hashtable hsParams = new Hashtable();
            if (affID != -1)
            {
                hsParams.Add("affid", affID);
            }
            hsParams.Add("groupid", -1);
            return DbHelper.Execute("Aff_CommGroupSelListNormal", hsParams, DBType.AFFILIATE_12BET);
        }
        public static DbResult CommGroupAssign(int groupID,string affIDList,string ip)
        {
            Hashtable hsParams = new Hashtable();            
            hsParams.Add("userid", AppSettings.Session.CustID);
            hsParams.Add("groupid",groupID);
            hsParams.Add("affidlist", affIDList);
            hsParams.Add("ip", ip);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupAssign", hsParams, DBType.AFFILIATE_12BET);
        }
        
    }
}