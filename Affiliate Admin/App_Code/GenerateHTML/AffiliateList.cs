using System;
using DNAide.Web;
using DNAide.Core;
using System.Text;
using DNAide.Data;
using System.Data;
namespace AffAdmin.GenerateHTML
{
    /// <summary>
    /// Summary description for AffiliateList
    /// </summary>

    public static partial class AffiliateList
    {
        #region Aff List content
        public static string GetAffList(int groupID,ref string groupName,string username, string email, int custid,string status, bool isClosed, int curpage,int pagesize,out int nummem, ref int totalpage, bool isFavor)
        {
                nummem = 0;
                DbResult result=null;
                if (custid != -2)
                {
                    if(!isFavor)

                        result = DataHandler.Affiliate.ListAff(groupID, groupName, username, email, custid, status, isClosed, curpage, pagesize);
                    else
                        result = DataHandler.Affiliate.ListAffFavorie(groupID, groupName, username, email, custid, status, curpage, pagesize);
                    nummem = CastAide.AsInt32(result.Outputs["nrows"]);
                }
                int cols = 11;
                string strTemplate = TemplateHandler.LoadTemplate("UserInfo/AffList.htm");
                if(string.IsNullOrEmpty(groupName))
                {
                    groupName=string.Empty;
                }

                if (status.ToLower() == "pending" || status.ToLower() == "disapproved")
                {
                    TemplateHandler.RemoveBlock(ref strTemplate, "approved1");
                    cols = 5;
                }
                if (nummem > 0)
                {
                    groupName=CastAide.AsString(result.Outputs["groupname"],string.Empty);
                    TemplateHandler.Assign(ref strTemplate, "Reset", Resources.Lang.Reset);
                    TemplateHandler.Assign(ref strTemplate, "view", Resources.Lang.View);
                    TemplateHandler.Assign(ref strTemplate, "Edit", Resources.Lang.Edit);

                    string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
                    DataTable dtbResult = result.ResultSet.Tables[0]; 
                    totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                    int intNo = 0;
                    StringBuilder strBuild = new StringBuilder();
                    decimal dcCurrentBalance = 0;
                    int intMemNum = 0;
                    TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string strTmp = strContent;
                        intNo++;
                        string strStatus = status;// dr["status"].ToString();
                        bool isClose = CastAide.AsBoolean(dr["isclosed"]);
                        if (isClose)
                        {
                            strStatus = "Closed";
                            TemplateHandler.Assign(ref strTmp, "color", Common.RowBgClosed);
                        }
                        TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                        dcCurrentBalance += CastAide.AsDecimal(dr["balancebf"]);

                        intMemNum = CastAide.AsInt32(dr["noofmember"]);
                        if(intMemNum == 0)
                            TemplateHandler.ReplaceBlock(ref strTmp, "nummem", intMemNum.ToString());
                        TemplateHandler.Assign(ref strTmp, "no",(curpage-1) * pagesize + intNo);
                        
                        TemplateHandler.Assign(ref strTmp, "custid", dr["AffID"].ToString());
                        TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                        if(status=="pending")
                            TemplateHandler.Assign(ref strTmp, "userdate-val", Common.IBCDateTimeFormat(dr["registereddate"], DTFormatType.DT24h));
                        else
                            TemplateHandler.Assign(ref strTmp, "userdate-val", Common.IBCDateTimeFormat(dr["ApprovedDate"], DTFormatType.DT24h));
                        TemplateHandler.Assign(ref strTmp, "casinodate-val", Common.FormatDateNoHour(dr["CasinoJoinedDate"]));
                        TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"].ToString());
                        TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"].ToString());
                        TemplateHandler.Assign(ref strTmp, "language-val", Common.GerFullTextLang(dr["preferredlanguage"])); 
                       
                        TemplateHandler.Assign(ref strTmp, "member-val", intMemNum);
                        TemplateHandler.Assign(ref strTmp, "favorcss", CastAide.AsInt32(dr["favorite"]) == 0 ? "favor-uncheck" : "favor-check");
                       
                        if (string.Compare(strStatus, "Pending", true) != 0 && string.Compare(strStatus, "Disapproved", true) != 0)
                        {
                            TemplateHandler.Assign(ref strTmp, "balancebroughtforward-val", Common.FormatNumber(dr["balancebf"]));
                        }

                        if (string.Compare(strStatus, Status.Approved, true) != 0)
                        {
                            TemplateHandler.ReplaceBlock(ref strTmp, "no-pass", string.Empty);
                        }
                        if (status.ToLower() == "approved")
                        {
                            TemplateHandler.Assign(ref strTmp, "groupid-val", dr["groupid"].ToString());
                            if (Convert.ToInt32(dr["groupid"]) > 1000)
                            {
                                TemplateHandler.RemoveBlock(ref strTmp, "PublicGroup");
                            }
                            else
                            {
                                TemplateHandler.RemoveBlock(ref strTmp, "PrivateGroup");
                            }
                            TemplateHandler.Assign(ref strTmp, "group-val",Common.CutLongText(dr["groupname"].ToString(),20));

                        }
                        TemplateHandler.Assign(ref strTmp, "isAdmin", "0");
                        strBuild.Append(strTmp);
                        
                    }

                    return strBuild.ToString();
                }
                else
                {
                    return Common.NoInfo(cols);                    
                }
        }
        #endregion
        
    }
}


