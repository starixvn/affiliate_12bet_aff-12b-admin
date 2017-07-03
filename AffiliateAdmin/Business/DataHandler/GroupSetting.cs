using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using GenerateHTML;

namespace DataHandler
{
    public class GroupSetting
    {
        public static DbResult CommGroupSel(string strGroupName, object fromdate, object todate, int pagesize, int pageindex)
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
            return DbHelper.Execute("Aff_CommGroupSel_xtest", DBType.AFFILIATE_12BET, hsParams);
        }

        public static DbResult CommGroupDel(int groupID, string ip)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", AppSettings.Session.CustID);
            hsParams.Add("groupid", groupID);
            hsParams.Add("ip", ip);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupDel", DBType.AFFILIATE_12BET, hsParams);
        }

        //public static DbResult CommGroupSelAll(string strGroupName)
        //{
        //    return DbHelper.Execute("Aff_CommGroupSelAll", DBType.AFFILIATE_12BET);
        //}
        public static DbResult CommGroupSelDetail(int groupID)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("groupid", groupID);
            return DbHelper.Execute("Aff_CommGroupSelDetail_xtest", DBType.AFFILIATE_12BET, hsParams);
        }

        public static DbResult CommGroupSelCommonSetting()
        {
            return DbHelper.Execute("Aff_CommGroupSelCommonSetting_xtest", DBType.AFFILIATE_12BET);
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
            StringBuilder sportIDList = new StringBuilder(), deductionList = new StringBuilder("<root>"), tierList = new StringBuilder(), minAmountList = new StringBuilder()
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

                    deductionList.Append(BuildDeductionListXml(s.deductions.Where(x => x.isCheck).ToList()));

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
            deductionList.Append("</root>");

            sportIDList.Remove(sportIDList.Length - 1, 1);
            tierList.Remove(tierList.Length - 1, 1);
            minAmountList.Remove(minAmountList.Length - 1, 1);
            maxAmountList.Remove(maxAmountList.Length - 1, 1);
            minQMList.Remove(minQMList.Length - 1, 1);
            maxQMList.Remove(maxQMList.Length - 1, 1);
            commissionList.Remove(commissionList.Length - 1, 1);

            hsParams.Add("sportidlist", sportIDList.ToString());
            hsParams.Add("deductionlist", deductionList.ToString());
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
            return DbHelper.Execute("Aff_CommGroupIns_xtest", DBType.AFFILIATE_12BET, hsParams);
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
            StringBuilder sportIDList = new StringBuilder(), deductionList = new StringBuilder("<root>"), tierList = new StringBuilder(), minAmountList = new StringBuilder()
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

                    deductionList.Append(BuildDeductionListXml(s.deductions.Where(x => x.isCheck || x.KeepCurrentValue).ToList()));

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
            deductionList.Append("</root>");

            sportIDList.Remove(sportIDList.Length - 1, 1);
            tierList.Remove(tierList.Length - 1, 1);
            minAmountList.Remove(minAmountList.Length - 1, 1);
            maxAmountList.Remove(maxAmountList.Length - 1, 1);
            minQMList.Remove(minQMList.Length - 1, 1);
            maxQMList.Remove(maxQMList.Length - 1, 1);
            commissionList.Remove(commissionList.Length - 1, 1);

            hsParams.Add("sportidlist", sportIDList.ToString());
            hsParams.Add("deductionlist", deductionList.ToString());
            hsParams.Add("tierlist", tierList.ToString());
            hsParams.Add("minamountlist", minAmountList.ToString());
            hsParams.Add("maxamountlist", maxAmountList.ToString());
            hsParams.Add("minqmlist", minQMList.ToString());
            hsParams.Add("maxqmlist", maxQMList.ToString());
            hsParams.Add("commissionlist", commissionList.ToString());
            hsParams.Add("ip", ip);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupUpd_xtest", DBType.AFFILIATE_12BET, hsParams);
        }

        public static DbResult CommGroupSelListNormal(int affID)
        {
            Hashtable hsParams = new Hashtable();
            if (affID != -1)
            {
                hsParams.Add("affid", affID);
            }
            hsParams.Add("groupid", -1);
            return DbHelper.Execute("Aff_CommGroupSelListNormal", DBType.AFFILIATE_12BET, hsParams);
        }

        public static DbResult CommGroupAssign(int groupID, string affIDList, string ip)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("userid", AppSettings.Session.CustID);
            hsParams.Add("groupid", groupID);
            hsParams.Add("affidlist", affIDList);
            hsParams.Add("ip", ip);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_CommGroupAssign", DBType.AFFILIATE_12BET, hsParams);
        }

        private static string BuildDeductionListXml(List<DeductionTypeObj> deductionSettings)
        {
            if (!deductionSettings.Any())
            {
                return string.Empty;
            }

            var xmlContent = new StringBuilder();
            foreach (var deduction in deductionSettings)
            {
                xmlContent.Append($"<row id='{deduction.id}'"
                    + (deduction.deductedPercentage.HasValue ? $" percent='{deduction.deductedPercentage.Value}'" : string.Empty)
                    + (deduction.nextDeductedPercentage.HasValue && deduction.isCheck ? $" nextpercent='{deduction.nextDeductedPercentage.Value}'" : string.Empty)
                    + " />");
            }

            return xmlContent.ToString();
        }
    }
}