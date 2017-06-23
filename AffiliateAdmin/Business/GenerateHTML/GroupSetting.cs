using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

namespace GenerateHTML
{
    /// <summary>
    /// Generate content for Group Setting
    /// Written by : Tong Hoang Dung on 24/03/2009
    /// </summary>
    public class DeductionTypeObj
    {
        public int id;
        public string name;
        public bool isCheck;
        public double deductedPercentage;

        public static DeductionTypeObj GetInstance()
        {
            return new DeductionTypeObj();
        }
    }

    public class SportSettingObj
    {
        public int id;
        public string sportName;
        public bool isEnabled;
        public IList<DeductionTypeObj> deductions = new List<DeductionTypeObj>();
        public IList<Int64> minAmounts = new List<Int64>();
        public IList<Int64> maxAmounts = new List<Int64>();
        public IList<int> minQMs = new List<int>();
        public IList<int> maxQMs = new List<int>();
        public IList<string> commissions = new List<string>();

        public static SportSettingObj GetInstance()
        {
            return new SportSettingObj();
        }
    }

    public class GroupSettingObj
    {
        /*Approve session */
        public bool isApproved = false;
        public string fName = string.Empty;
        public string email = string.Empty;
        public string affName = string.Empty;
        public string language = string.Empty;
        public string password = string.Empty;
        /*Approve session */
        public bool isEdited = true;
        public int id;
        public int copySportID = -1;
        public string groupName;
        public string description;
        public IList<SportSettingObj> sportSetting = new List<SportSettingObj>();

        public static GroupSettingObj GetInstance()
        {
            return new GroupSettingObj();
        }
    }

    public class SportStatusColor
    {
        public string Enabled = "enableSportColor";
        public string Disabled = "disableSportColor";

        public static SportStatusColor GetInstance()
        {
            return new SportStatusColor();
        }
    }

    public class IconSportStatus
    {
        public string Enabled = "i_enableSport";
        public string Disabled = "i_disableSport";

        public static IconSportStatus GetInstance()
        {
            return new IconSportStatus();
        }
    }

    public class SportTabSwitchIcon
    {
        public string Expand = "expand";
        public string Collapse = "collapse";

        public static SportTabSwitchIcon GetInstance()
        {
            return new SportTabSwitchIcon();
        }
    }

    public class SportTabSwitch
    {
        public string Expand = "";
        public string Collapse = "none";

        public static SportTabSwitch GetInstance()
        {
            return new SportTabSwitch();
        }
    }

    public class CheckboxStatus
    {
        public string OnEnable = "chkbox_on";
        public string OffEnable = "chkbox_off";
        public string OnDisable = "chkbox_on_disable";
        public string OffDisable = "chkbox_off_disable";

        public static CheckboxStatus GetInstance()
        {
            return new CheckboxStatus();
        }
    }

    public class IconEdit
    {
        public string Enabled = "icedit";
        public string Disabled = "icedit_disable";

        public static IconEdit GetInstance()
        {
            return new IconEdit();
        }
    }

    public class IconDelete
    {
        public string Enabled = "icdelete";
        public string Disabled = "icdelete_disable";

        public static IconDelete GetInstance()
        {
            return new IconDelete();
        }
    }

    public class IconAddTier
    {
        public string Enabled = "btnAdd";
        public string Disabled = "btnAdd_disabled";

        public static IconAddTier GetInstance()
        {
            return new IconAddTier();
        }
    }

    public class IconSave
    {
        public string Enabled = "btnSave";
        public string Disabled = "btnSave_disable";

        public static IconSave GetInstance()
        {
            return new IconSave();
        }
    }

    public class GroupSetting
    {
        public const int MAXQM = 1000000000;
        public const Int64 MAXAMOUNT = 100000000000000;

        public static string GetMaxQMDisplay(object max)
        {
            if (Convert.ToInt32(max) == MAXQM)
            {
                return "∞";
            }
            return Common.FormatNumber1(Convert.ToInt32(max).ToString(), false);
        }

        public static string GetMaxAmountDisplay(object max)
        {
            if (Convert.ToInt64(max) == MAXAMOUNT)
            {
                return "∞";
            }
            return Common.FormatNumber1(Convert.ToInt64(max).ToString(), true);
        }

        public static string GetCommissionOptionList(decimal selectedCommission, string htmlTemplate)
        {
            StringBuilder list = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                string option = htmlTemplate;
                decimal val = Convert.ToDecimal(i) / 100;
                TemplateHandler.Assign(ref option, "option_value", DNAide.Utils.NumberFormat.DecFormat(val, 2));
                if (val == selectedCommission)
                {
                    TemplateHandler.Assign(ref option, "selected_val", "selected");
                }
                else
                {
                    TemplateHandler.Assign(ref option, "selected_val", string.Empty);
                }
                list.Append(option);
            }
            return list.ToString();
        }

        public static string GetCashOutWLShareOptionList(double selectedCashOutWLShare, string htmlTemplate)
        {
            StringBuilder list = new StringBuilder();
            for (int i = 0; i <= 100; i++)
            {
                string option = htmlTemplate;
                double val = Convert.ToDouble(i) / 100;
                TemplateHandler.Assign(ref option, "option_value", DNAide.Utils.NumberFormat.DecFormat(val, 2));
                if (val == selectedCashOutWLShare)
                {
                    TemplateHandler.Assign(ref option, "selected_val", "selected");
                }
                else
                {
                    TemplateHandler.Assign(ref option, "selected_val", string.Empty);
                }
                list.Append(option);
            }
            return list.ToString();
        }

        public static string GetGroupSettingList(string groupName, object fromdate, object todate, int curpage, ref int minNo, ref int maxNo, ref int totalRows, int pagesize, ref int totalpage)
        {
            DbResult result = null;
            int nRows = 0;
            int totalOfNoneGroup = 0;
            result = DataHandler.GroupSetting.CommGroupSel(groupName, fromdate, todate, pagesize, curpage);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            totalOfNoneGroup = CastAide.AsInt32(result.Outputs["totalnonegroup"]);
            totalRows = nRows;
            int cols = 18;
            minNo = 0;
            maxNo = 0;
            if (totalOfNoneGroup > 0 || nRows > 0)
            {
                string groupSettingContent = TemplateHandler.LoadTemplate("GroupSetting/GroupSetting.htm");
                string masterRow = TemplateHandler.CopyBlock(groupSettingContent, "MasterRow");
                string policyDetailRow = TemplateHandler.CopyBlock(groupSettingContent, "PolicyDetailRow");
                string nextSportTypeMasterRow = TemplateHandler.CopyBlock(groupSettingContent, "NextSportTypeMasterRow");
                string deductionOption = TemplateHandler.CopyBlock(masterRow, "DeductionOption");

                StringBuilder strBuild = new StringBuilder();
                if (totalOfNoneGroup > 0)
                {
                    string noneGroupRow = TemplateHandler.CopyBlock(groupSettingContent, "NoneGroupRow");
                    TemplateHandler.Assign(ref noneGroupRow, "No_val", 0);
                    TemplateHandler.Assign(ref noneGroupRow, "GroupName_val", "[None]");
                    TemplateHandler.Assign(ref noneGroupRow, "Description_val", "All private groups for affiliates");
                    TemplateHandler.Assign(ref noneGroupRow, "CreatedDate_val", string.Empty);
                    TemplateHandler.Assign(ref noneGroupRow, "Creator_val", string.Empty);
                    TemplateHandler.Assign(ref noneGroupRow, "NoOfAffiliate_val", totalOfNoneGroup.ToString(Common.NumberFormatPlaceholder));

                    TemplateHandler.Assign(ref noneGroupRow, "GroupID_val", 0);
                    TemplateHandler.Assign(ref noneGroupRow, "color", Common.RowBgEven);
                    strBuild.Append(noneGroupRow);
                }
                if (nRows <= 0)
                    return strBuild.ToString();

                DataTable tblDeductionType = result.ResultSet.Tables[0];
                DataTable tblDeductionSetting = result.ResultSet.Tables[1];
                DataTable tblGroup = result.ResultSet.Tables[2];
                DataTable tblGroupDetail = result.ResultSet.Tables[3];
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                int intNo = 0;
                minNo = (curpage - 1) * pagesize + 1;
                int totalSport = 0;
                foreach (DataRow dr in tblGroup.Rows)
                {
                    string strTmp = masterRow;
                    intNo++;
                    totalSport++;
                    int no = (curpage - 1) * pagesize + intNo;
                    TemplateHandler.Assign(ref strTmp, "No_val", no);
                    TemplateHandler.Assign(ref strTmp, "GroupName_val", Common.CutLongText(dr["GroupName"].ToString(), 20));
                    TemplateHandler.Assign(ref strTmp, "Description_val", Common.CutLongText(dr["Description"].ToString(), 50));
                    TemplateHandler.Assign(ref strTmp, "CreatedDate_val", Common.IBCDateTimeFormat(dr["createddate"], DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "Creator_val", dr["Creator"].ToString());

                    int noOfAffiliate = Convert.ToInt32(dr["NoOfAffiliate"]);
                    if (noOfAffiliate > 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "NoOfAffiliate_val", noOfAffiliate.ToString(Common.NumberFormatPlaceholder));
                    }
                    else
                    {
                        TemplateHandler.ReplaceBlock(ref strTmp, "link", noOfAffiliate.ToString(Common.NumberFormatPlaceholder));
                    }

                    TemplateHandler.Assign(ref strTmp, "App_Themes", AppSettings.ThemeDir);
                    //if(string.Compare(dr["GroupName"].ToString(), "NoCommission", true) == 0)
                    //TemplateHandler.Assign(ref strTmp, "GroupID_val", dr["GroupID"].ToString() + "_NoComm");
                    //else
                    //TemplateHandler.Assign(ref strTmp, "GroupID_val", dr["GroupID"]);
                    TemplateHandler.Assign(ref strTmp, "GroupID_val", dr["GroupID"]);

                    if (Convert.ToInt32(dr["NoOfAffiliate"]) > 0 || string.Compare(dr["GroupName"].ToString(), "Default", true) == 0 || string.Compare(dr["GroupName"].ToString(), "NoCommission", true) == 0/*|| !Convert.ToBoolean(dr["IsAllowedToDelete"])*/)
                    {
                        TemplateHandler.Assign(ref strTmp, "id_val", string.Empty);
                        TemplateHandler.Assign(ref strTmp, "btnDeleteClass_val", "icdelete1");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "id_val", "id='" + dr["GroupID"].ToString() + "_" + no.ToString() + "'");
                        TemplateHandler.Assign(ref strTmp, "btnDeleteClass_val", "icdelete");
                    }

                    if ((totalSport % 2) == 0)
                        TemplateHandler.Assign(ref strTmp, "SportsClass_val", "SportEven");
                    else
                        TemplateHandler.Assign(ref strTmp, "SportsClass_val", "SportOdd");

                    if ((intNo % 2) == 0)
                        TemplateHandler.Assign(ref strTmp, "color", Common.RowBgEven);
                    else
                        TemplateHandler.Assign(ref strTmp, "color", Common.RowBgOdd);

                    DataRow[] rows = tblGroupDetail.Select("GroupID=" + dr["GroupID"].ToString());
                    TemplateHandler.Assign(ref strTmp, "TotalRowOfGroup", rows.Length);
                    int lastSportType = -1;
                    int firstSportType = -1;
                    int totalRowOfFirstSportType = 0;
                    StringBuilder subRows = new StringBuilder();
                    foreach (DataRow row in rows)
                    {
                        if (lastSportType == -1) //First Sport Type
                        {
                            lastSportType = Convert.ToInt32(row["SportType"]);
                            firstSportType = lastSportType;
                            totalRowOfFirstSportType++;
                            TemplateHandler.Assign(ref strTmp, "SportName_val", row["SportName"]);
                            TemplateHandler.Assign(ref strTmp, "Tier_val", row["TierID"]);
                            TemplateHandler.Assign(ref strTmp, "MinAmt_val", Common.FormatNumber1(Convert.ToInt64(row["MinAmount"]), true));
                            TemplateHandler.Assign(ref strTmp, "MaxAmt_val", GetMaxAmountDisplay(row["MaxAmount"]));
                            TemplateHandler.Assign(ref strTmp, "MinQM_val", Common.FormatNumber1(Convert.ToInt32(row["MinQM"]), false));
                            TemplateHandler.Assign(ref strTmp, "MaxQM_val", GetMaxQMDisplay(row["MaxQM"]));
                            TemplateHandler.Assign(ref strTmp, "Commission_val", DNAide.Utils.NumberFormat.DecFormat(Convert.ToDecimal(row["Commission"]), 2));

                            //Load Deduction Settings
                            DataRow[] deductions = tblDeductionType.Select("SportID=" + row["SportType"]);
                            StringBuilder deductionList = new StringBuilder();
                            foreach (DataRow deduction in deductions)
                            {
                                string _deductionOption = deductionOption;
                                TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", deduction["dtypedesc"]);
                                TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", deduction["dtypename"]);

                                DataView dv = tblDeductionSetting.DefaultView;
                                dv.RowFilter = "GroupID=" + dr["GroupID"];

                                if (dv.ToTable().Select("DeductionID=" + deduction["dtypeid"]).Length > 0)
                                {
                                    TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OnDisable);
                                }
                                else
                                {
                                    TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffDisable);
                                }
                                deductionList.Append(_deductionOption);
                            }
                            TemplateHandler.ReplaceBlock(ref strTmp, "DeductionOption", deductionList.ToString());
                        }
                        else
                        {
                            if (firstSportType == Convert.ToInt32(row["SportType"]))
                                totalRowOfFirstSportType++;
                            if (lastSportType == Convert.ToInt32(row["SportType"])) //raise a sub policyrow
                            {
                                string p = policyDetailRow;
                                TemplateHandler.Assign(ref p, "Tier_val", row["TierID"]);
                                TemplateHandler.Assign(ref p, "MinAmt_val", Common.FormatNumber1(Convert.ToInt64(row["MinAmount"]), true));
                                TemplateHandler.Assign(ref p, "MaxAmt_val", GetMaxAmountDisplay(row["MaxAmount"]));
                                TemplateHandler.Assign(ref p, "MinQM_val", Common.FormatNumber1(Convert.ToInt32(row["MinQM"]), false));
                                TemplateHandler.Assign(ref p, "MaxQM_val", GetMaxQMDisplay(row["MaxQM"]));
                                TemplateHandler.Assign(ref p, "Commission_val", DNAide.Utils.NumberFormat.DecFormat(Convert.ToDecimal(row["Commission"]), 2));
                                if ((totalSport % 2) == 0)
                                    TemplateHandler.Assign(ref p, "SportsClass_val", "SportEven");
                                else
                                    TemplateHandler.Assign(ref p, "SportsClass_val", "SportOdd");
                                subRows.Append(p);
                            }
                            else //Next Sport Type Rows
                            {
                                totalSport++;
                                lastSportType = Convert.ToInt32(row["SportType"]);
                                string nextSport = nextSportTypeMasterRow;
                                TemplateHandler.Assign(ref nextSport, "SportName_val", row["SportName"]);
                                TemplateHandler.Assign(ref nextSport, "Tier_val", row["TierID"]);
                                TemplateHandler.Assign(ref nextSport, "MinAmt_val", Convert.ToInt64(row["MinAmount"]));
                                TemplateHandler.Assign(ref nextSport, "MaxAmt_val", GetMaxAmountDisplay(row["MaxAmount"]));
                                TemplateHandler.Assign(ref nextSport, "MinQM_val", Convert.ToInt32(row["MinQM"]));
                                TemplateHandler.Assign(ref nextSport, "MaxQM_val", GetMaxQMDisplay(row["MaxQM"]));
                                TemplateHandler.Assign(ref nextSport, "Commission_val", DNAide.Utils.NumberFormat.DecFormat(Convert.ToDecimal(row["Commission"]), 2));

                                DataRow[] deductions = tblDeductionType.Select("SportID=" + row["SportType"]);
                                StringBuilder deductionList = new StringBuilder();
                                foreach (DataRow deduction in deductions)
                                {
                                    string _deductionOption = deductionOption;
                                    TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", deduction["dtypedesc"]);
                                    TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", deduction["dtypename"]);

                                    DataView dv = tblDeductionSetting.DefaultView;
                                    dv.RowFilter = "GroupID=" + dr["GroupID"];

                                    if (dv.ToTable().Select("DeductionID=" + deduction["dtypeid"]).Length > 0)
                                    {
                                        TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OnDisable);
                                    }
                                    else
                                    {
                                        TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffDisable);
                                    }
                                    deductionList.Append(_deductionOption);
                                }
                                TemplateHandler.ReplaceBlock(ref nextSport, "DeductionOption", deductionList.ToString());

                                if ((totalSport % 2) == 0)
                                    TemplateHandler.Assign(ref nextSport, "SportsClass_val", "SportEven");
                                else
                                    TemplateHandler.Assign(ref nextSport, "SportsClass_val", "SportOdd");
                                DataRow[] temp = tblGroupDetail.Select("GroupID=" + dr["GroupID"].ToString() + " and Sporttype=" + row["SportType"]);
                                TemplateHandler.Assign(ref nextSport, "TotalRowOfSportType", temp.Length);
                                subRows.Append(nextSport);
                            }
                        }
                    }
                    TemplateHandler.Assign(ref strTmp, "TotalRowOfFirstSportType", totalRowOfFirstSportType);
                    strBuild.Append(strTmp);
                    strBuild.Append(subRows.ToString());
                }
                maxNo = (curpage - 1) * pagesize + intNo;
                return strBuild.ToString();
            }
            return Common.NoInfo(cols);
        }

        public static string GetGroupSettingForEdit(int groupID)
        {
            //DbResult commonSetting = null;
            //commonSetting = DataHandler.GroupSetting.CommGroupSelCommonSetting();
            DbResult setting = null;
            setting = DataHandler.GroupSetting.CommGroupSelDetail(groupID);
            if (setting.ResultSet.Tables.Count < 5 || setting.ResultSet.Tables[0].Rows.Count <= 0)
            {
                return "There is no information available";
            }
            string settingTemplate = TemplateHandler.LoadTemplate("GroupSetting/GroupSettingEdit.htm");
            if (groupID < 1000)
            {
                TemplateHandler.RemoveBlock(ref settingTemplate, "PrivateGroup");
                TemplateHandler.Assign(ref settingTemplate, "AffName_val", string.Empty);
            }
            else
            {
                TemplateHandler.RemoveBlock(ref settingTemplate, "NormalGroup");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();

            TemplateHandler.Assign(ref settingTemplate, "XsrfTokenFormValue", AntiForgeryHelper.GenerateXsrfFormToken(HttpContext.Current));
            TemplateHandler.Assign(ref settingTemplate, "SportStatusColor_val", js.Serialize(SportStatusColor.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconSportStatus_val", js.Serialize(IconSportStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "SportTabSwitchIcon_val", js.Serialize(SportTabSwitchIcon.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "SportTabSwitch_val", js.Serialize(SportTabSwitch.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "CheckboxStatus_val", js.Serialize(CheckboxStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconEdit_val", js.Serialize(IconEdit.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconDelete_val", js.Serialize(IconDelete.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconAddTier_val", js.Serialize(IconAddTier.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconSave_val", js.Serialize(IconSave.GetInstance()));

            TemplateHandler.Assign(ref settingTemplate, "RequiedExplain_val", Resources.Lang.RequiedExplain);
            TemplateHandler.Assign(ref settingTemplate, "EnableSportExplainTpl_val", Resources.Lang.EnableSportExplain);
            TemplateHandler.Assign(ref settingTemplate, "DisableSportExplainTpl_val", Resources.Lang.DisableSportExplain);
            TemplateHandler.Assign(ref settingTemplate, "noInfo_val", Common.NoInfo(7));

            TemplateHandler.Assign(ref settingTemplate, "maxlengthGroupName_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthDescription_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthAmount_val", 19);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthQM_val", 13);
            TemplateHandler.Assign(ref settingTemplate, "maximumQMNumber_val", MAXQM);
            TemplateHandler.Assign(ref settingTemplate, "maximumAmountNumber_val", MAXAMOUNT);

            string sportSetting = TemplateHandler.CopyBlock(settingTemplate, "SportSetting");

            TemplateHandler.Assign(ref settingTemplate, "GroupName", Resources.Lang.GroupName);
            TemplateHandler.Assign(ref settingTemplate, "Description", Resources.Lang.Description);
            TemplateHandler.Assign(ref settingTemplate, "ActiveAll", Resources.Lang.ActiveAll);
            TemplateHandler.Assign(ref settingTemplate, "Save", Resources.Lang.Save);

            TemplateHandler.Assign(ref sportSetting, "Deductions", Resources.Lang.Deductions);
            TemplateHandler.Assign(ref sportSetting, "CashOutDeductionNote", Resources.Lang.CashOutDeductionNote);
            TemplateHandler.Assign(ref sportSetting, "TierSetting", Resources.Lang.TierSetting);
            TemplateHandler.Assign(ref sportSetting, "Tier", Resources.Lang.Tier);
            TemplateHandler.Assign(ref sportSetting, "MinAmount", Resources.Lang.MinAmount);
            TemplateHandler.Assign(ref sportSetting, "MaxAmount", Resources.Lang.MaxAmount);
            TemplateHandler.Assign(ref sportSetting, "MinQM", Resources.Lang.MinQM);
            TemplateHandler.Assign(ref sportSetting, "MaxQM", Resources.Lang.MaxQM);
            TemplateHandler.Assign(ref sportSetting, "Commission", Resources.Lang.Comm);
            TemplateHandler.Assign(ref sportSetting, "Action", Resources.Lang.Action);
            TemplateHandler.Assign(ref sportSetting, "AddTier", Resources.Lang.AddTier);
            TemplateHandler.Assign(ref sportSetting, "Edit", Resources.Lang.Edit);
            TemplateHandler.Assign(ref sportSetting, "Delete", Resources.Lang.Delete);
            TemplateHandler.Assign(ref sportSetting, "MinQMExplain", Resources.Lang.MinQMExplain);
            TemplateHandler.Assign(ref sportSetting, "MaxQMExplain", Resources.Lang.MaxQMExplain);
            TemplateHandler.Assign(ref sportSetting, "MinAmountExplain", Resources.Lang.MinAmountExplain);
            TemplateHandler.Assign(ref sportSetting, "MaxAmountExplain", Resources.Lang.MaxAmountExplain);

            string deductionOption = TemplateHandler.CopyBlock(sportSetting, "DeductionType");
            string cashOutWLOption = TemplateHandler.CopyBlock(sportSetting, "CashOutWinLoss");
            string cashOutWLShareOption = TemplateHandler.CopyBlock(cashOutWLOption, "CashOutWLShareOption");
            string tierRow = TemplateHandler.CopyBlock(sportSetting, "TierRow");
            string slOption = TemplateHandler.CopyBlock(tierRow, "SelectOption");

            string tierRowTpl = tierRow;
            TemplateHandler.ReplaceBlock(ref tierRowTpl, "SelectOption", GetCommissionOptionList(0, slOption));
            TemplateHandler.Assign(ref settingTemplate, "TierRowTpl_val", js.Serialize(tierRowTpl));

            DataTable SportType = setting.ResultSet.Tables[3];
            DataTable DeductionType = setting.ResultSet.Tables[4];
            DataTable CommGroup = setting.ResultSet.Tables[0];
            DataTable GroupSetting = setting.ResultSet.Tables[1];
            DataTable DeductionSetting = setting.ResultSet.Tables[2];

            TemplateHandler.Assign(ref settingTemplate, "GroupName_val", CommGroup.Rows[0]["GroupName"]);
            TemplateHandler.Assign(ref settingTemplate, "Description_val", CommGroup.Rows[0]["Description"]);
            if (string.Compare(CommGroup.Rows[0]["GroupName"].ToString(), "Default", true) == 0)
            {
                TemplateHandler.Assign(ref settingTemplate, "txtGroupName_disabled_val", "disabled");
            }
            else {
                TemplateHandler.Assign(ref settingTemplate, "txtGroupName_disabled_val", string.Empty);
            }

            GroupSettingObj obj = GroupSettingObj.GetInstance();
            obj.id = groupID;
            obj.description = CommGroup.Rows[0]["Description"].ToString();
            obj.groupName = CommGroup.Rows[0]["GroupName"].ToString();

            StringBuilder result = new StringBuilder();
            IList<int> sportIDArr = new List<int>();
            bool isFirstSportActive = true;
            int activeSports = 0, noSports = 0;
            foreach (DataRow sport in SportType.Rows)
            {
                SportSettingObj sportSettingObj = SportSettingObj.GetInstance();
                sportSettingObj.id = Convert.ToInt32(sport["SportID"]);
                sportSettingObj.sportName = sport["SportName"].ToString();

                sportIDArr.Add(Convert.ToInt32(sport["SportID"]));
                string _sportSetting = sportSetting;
                TemplateHandler.Assign(ref _sportSetting, "SportName", sport["SportName"]);
                string copyText = Resources.Lang.CopySettingText;
                TemplateHandler.Assign(ref copyText, "SportName_val", sport["SportName"]);
                TemplateHandler.Assign(ref _sportSetting, "CopyAllText", copyText);
                TemplateHandler.Assign(ref _sportSetting, "SportID_val", sport["SportID"]);

                noSports++;

                DataRow[] drDeductionType = DeductionType.Select("SportID=" + sport["SportID"]);

                StringBuilder deductionList = new StringBuilder();
                StringBuilder tierList = new StringBuilder();
                //Sport is enabled
                if (GroupSetting.Select("SportType=" + sport["SportID"]).Length >= 1)
                {
                    sportSettingObj.isEnabled = true;
                    activeSports++;
                    TemplateHandler.Assign(ref _sportSetting, "sport_color_val", SportStatusColor.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _sportSetting, "icon_enable_val", IconSportStatus.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _sportSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffEnable);
                    string iconExplain = Resources.Lang.DisableSportExplain;
                    TemplateHandler.Assign(ref iconExplain, "SportName_val", sport["SportName"]);
                    TemplateHandler.Assign(ref _sportSetting, "icon_enable_explain_val", iconExplain);

                    if (isFirstSportActive)
                    {
                        isFirstSportActive = false;
                        TemplateHandler.Assign(ref _sportSetting, "collapse_expand_val", SportTabSwitchIcon.GetInstance().Expand);
                        TemplateHandler.Assign(ref _sportSetting, "tab_content_display_val", SportTabSwitch.GetInstance().Expand);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref _sportSetting, "collapse_expand_val", SportTabSwitchIcon.GetInstance().Collapse);
                        TemplateHandler.Assign(ref _sportSetting, "tab_content_display_val", SportTabSwitch.GetInstance().Collapse);
                    }
                    //Load Deduction Settings
                    foreach (DataRow dtype in drDeductionType)
                    {
                        DeductionTypeObj deductionObj = DeductionTypeObj.GetInstance();
                        deductionObj.id = Convert.ToInt32(dtype["dtypeid"]);
                        deductionObj.name = dtype["dtypename"].ToString();

                        string _deductionOption = deductionObj.name == "Cash Out Win Loss Share" ? cashOutWLOption : deductionOption;

                        TemplateHandler.Assign(ref _deductionOption, "SportID_val", sport["SportID"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionID_val", dtype["dtypeid"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", dtype["dtypedesc"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", dtype["dtypename"]);

                        if (DeductionSetting.Select("DeductionID=" + dtype["dtypeid"]).Length > 0)
                        {
                            TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OnEnable);
                            deductionObj.isCheck = true;
                            deductionObj.deductedPercentage = CastAide.AsDouble(DeductionSetting.Select("DeductionID=" + dtype["dtypeid"])[0]["deductedpercentage"]);
                        }
                        else
                        {
                            TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffEnable);
                            deductionObj.isCheck = false;
                            deductionObj.deductedPercentage = CastAide.AsDouble(dtype["deductedpercentage"]);
                        }
                        if (deductionObj.name == "Cash Out Win Loss Share")
                        {
                            TemplateHandler.ReplaceBlock(ref _deductionOption, "CashOutWLShareOption", GetCashOutWLShareOptionList(deductionObj.deductedPercentage, cashOutWLShareOption));
                            TemplateHandler.Assign(ref _deductionOption, "Deduction_val", deductionObj.deductedPercentage);
                        }
                        deductionList.Append(_deductionOption);
                        sportSettingObj.deductions.Add(deductionObj);
                    }
                    //Load Tier Setting
                    DataRow[] Tiers = GroupSetting.Select("SportType=" + sport["SportID"]);

                    bool isFirstTier = true;
                    foreach (DataRow tier in Tiers)
                    {
                        string _tierRow = tierRow;
                        sportSettingObj.minAmounts.Add(Convert.ToInt64(tier["MinAmount"]));
                        sportSettingObj.maxAmounts.Add(Convert.ToInt64(tier["MaxAmount"]));
                        sportSettingObj.minQMs.Add(Convert.ToInt32(tier["MinQM"]));
                        sportSettingObj.maxQMs.Add(Convert.ToInt32(tier["MaxQM"]));
                        sportSettingObj.commissions.Add(DNAide.Utils.NumberFormat.DecFormat(tier["Commission"], 2));

                        TemplateHandler.Assign(ref _tierRow, "SportID_val", sport["SportID"]);
                        //Only allow to delete last tier

                        TemplateHandler.Assign(ref _tierRow, "tierID_val", tier["tierid"]);
                        TemplateHandler.Assign(ref _tierRow, "txtMinAmount_val", Common.FormatNumber1(Convert.ToInt64(tier["MinAmount"]), true));
                        TemplateHandler.Assign(ref _tierRow, "txtMaxAmount_val", GetMaxAmountDisplay(tier["MaxAmount"]));
                        TemplateHandler.Assign(ref _tierRow, "txtMinQM_val", Common.FormatNumber1(Convert.ToInt32(tier["MinQM"]), false));
                        TemplateHandler.Assign(ref _tierRow, "txtMaxQM_val", GetMaxQMDisplay(tier["MaxQM"]));
                        TemplateHandler.ReplaceBlock(ref _tierRow, "SelectOption", GetCommissionOptionList(Convert.ToDecimal(tier["Commission"]), slOption));
                        if (GetMaxAmountDisplay(tier["MaxAmount"]) == "∞")
                        {
                            TemplateHandler.Assign(ref _tierRow, "chkMaxClass_val", CheckboxStatus.GetInstance().OnEnable);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxQM_val", "disabled");
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxAmount_val", "disabled");
                            TemplateHandler.Assign(ref _tierRow, "icon_delete_status_val", IconDelete.GetInstance().Enabled);
                        }
                        else {
                            TemplateHandler.Assign(ref _tierRow, "chkMaxClass_val", CheckboxStatus.GetInstance().OffDisable);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxQM_val", string.Empty);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMaxAmount_val", string.Empty);
                            TemplateHandler.Assign(ref _tierRow, "icon_delete_status_val", IconDelete.GetInstance().Disabled);
                        }

                        if (isFirstTier)
                        {
                            isFirstTier = false;
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinQM_val", string.Empty);
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinAmount_val", string.Empty);
                        }
                        else
                        {
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinQM_val", "disabled");
                            TemplateHandler.Assign(ref _tierRow, "disabled_txtMinAmount_val", "disabled");
                        }
                        tierList.Append(_tierRow);
                    }
                }
                else //Sport is disabled
                {
                    sportSettingObj.isEnabled = false;
                    TemplateHandler.Assign(ref _sportSetting, "sport_color_val", SportStatusColor.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _sportSetting, "icon_enable_val", IconSportStatus.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _sportSetting, "collapse_expand_val", SportTabSwitchIcon.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _sportSetting, "tab_content_display_val", SportTabSwitch.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _sportSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffDisable);

                    string iconExplain = Resources.Lang.EnableSportExplain;
                    TemplateHandler.Assign(ref iconExplain, "SportName_val", sport["SportName"]);
                    TemplateHandler.Assign(ref _sportSetting, "icon_enable_explain_val", iconExplain);

                    //Load Deduction Settings
                    foreach (DataRow dtype in drDeductionType)
                    {
                        DeductionTypeObj deductionObj = DeductionTypeObj.GetInstance();
                        deductionObj.id = Convert.ToInt32(dtype["dtypeid"]);
                        deductionObj.name = dtype["dtypename"].ToString();
                        deductionObj.isCheck = false;
                        deductionObj.deductedPercentage = CastAide.AsDouble(dtype["deductedpercentage"]);
                        sportSettingObj.deductions.Add(deductionObj);

                        string _deductionOption;
                        if (deductionObj.name == "Cash Out Win Loss Share")
                        {
                            _deductionOption = cashOutWLOption;
                            TemplateHandler.ReplaceBlock(ref _deductionOption, "CashOutWLShareOption", GetCashOutWLShareOptionList(deductionObj.deductedPercentage, cashOutWLShareOption));
                            TemplateHandler.Assign(ref _deductionOption, "Deduction_val", deductionObj.deductedPercentage);
                        }
                        else
                        {
                            _deductionOption = deductionOption;
                        }
                        TemplateHandler.Assign(ref _deductionOption, "SportID_val", sport["SportID"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionID_val", dtype["dtypeid"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", dtype["dtypedesc"]);
                        TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", dtype["dtypename"]);
                        TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffEnable);
                        deductionList.Append(_deductionOption);
                    }
                    //Load TierList=no info
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", Common.NoInfo(7));
                    //Disable Button AddTier
                    TemplateHandler.Assign(ref _sportSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);
                }

                if (deductionList.Length == 0)
                {
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "Deduction", string.Empty);
                }
                else {
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "DeductionOption", deductionList.ToString());
                }
                if (tierList.Length == 0)
                {
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", Common.NoInfo(7));
                    TemplateHandler.Assign(ref _sportSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", tierList.ToString());
                    TemplateHandler.Assign(ref _sportSetting, "btnAddStatus_val", IconAddTier.GetInstance().Disabled);
                }

                result.Append(_sportSetting);
                obj.sportSetting.Add(sportSettingObj);
            }

            if (activeSports != 0 && activeSports == noSports)
            {
                TemplateHandler.Assign(ref settingTemplate, "chkActiveAllClass_val", CheckboxStatus.GetInstance().OnEnable);
            }
            else
            {
                TemplateHandler.Assign(ref settingTemplate, "chkActiveAllClass_val", CheckboxStatus.GetInstance().OffEnable);
            }

            TemplateHandler.ReplaceBlock(ref settingTemplate, "SportSetting", result.ToString());
            TemplateHandler.Assign(ref settingTemplate, "GroupSetting_val", js.Serialize(obj));

            //string t = js.Serialize(obj);

            return settingTemplate;
        }

        public static string GetGroupSettingForAddNew(int groupID, bool isApproved, string fName, string affName, string language, string email)
        {
            DbResult commonSetting = null;
            commonSetting = DataHandler.GroupSetting.CommGroupSelCommonSetting();

            if (commonSetting.ResultSet.Tables.Count < 2 || commonSetting.ResultSet.Tables[0].Rows.Count <= 0)
            {
                return "There is no information available";
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            GroupSettingObj obj = GroupSettingObj.GetInstance();
            obj.isEdited = false;
            obj.id = groupID;
            if (isApproved)
            {
                obj.isApproved = true;
                obj.fName = fName;
                obj.affName = affName;
                obj.language = language;
                obj.email = email;
            }

            string settingTemplate = TemplateHandler.LoadTemplate("GroupSetting/GroupSettingEdit.htm");
            if (groupID > 0)
            {
                TemplateHandler.RemoveBlock(ref settingTemplate, "NormalGroup");
                obj.description = "[None]";
                obj.groupName = "[None]";
            }
            else
            {
                obj.description = string.Empty;
                obj.groupName = string.Empty;
            }

            TemplateHandler.Assign(ref settingTemplate, "XsrfTokenFormValue", AntiForgeryHelper.GenerateXsrfFormToken(HttpContext.Current));
            TemplateHandler.Assign(ref settingTemplate, "SportStatusColor_val", js.Serialize(SportStatusColor.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconSportStatus_val", js.Serialize(IconSportStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "SportTabSwitchIcon_val", js.Serialize(SportTabSwitchIcon.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "SportTabSwitch_val", js.Serialize(SportTabSwitch.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "CheckboxStatus_val", js.Serialize(CheckboxStatus.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconEdit_val", js.Serialize(IconEdit.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconDelete_val", js.Serialize(IconDelete.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconAddTier_val", js.Serialize(IconAddTier.GetInstance()));
            TemplateHandler.Assign(ref settingTemplate, "IconSave_val", js.Serialize(IconSave.GetInstance()));

            TemplateHandler.Assign(ref settingTemplate, "RequiedExplain_val", Resources.Lang.RequiedExplain);
            TemplateHandler.Assign(ref settingTemplate, "EnableSportExplainTpl_val", Resources.Lang.EnableSportExplain);
            TemplateHandler.Assign(ref settingTemplate, "DisableSportExplainTpl_val", Resources.Lang.DisableSportExplain);
            TemplateHandler.Assign(ref settingTemplate, "noInfo_val", Common.NoInfo(7));

            TemplateHandler.Assign(ref settingTemplate, "maxlengthGroupName_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthDescription_val", 100);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthAmount_val", 19);
            TemplateHandler.Assign(ref settingTemplate, "maxlengthQM_val", 13);
            TemplateHandler.Assign(ref settingTemplate, "maximumQMNumber_val", MAXQM);
            TemplateHandler.Assign(ref settingTemplate, "maximumAmountNumber_val", MAXAMOUNT);

            string sportSetting = TemplateHandler.CopyBlock(settingTemplate, "SportSetting");

            TemplateHandler.Assign(ref settingTemplate, "GroupName", Resources.Lang.GroupName);
            TemplateHandler.Assign(ref settingTemplate, "Description", Resources.Lang.Description);
            TemplateHandler.Assign(ref settingTemplate, "ActiveAll", Resources.Lang.ActiveAll);
            TemplateHandler.Assign(ref settingTemplate, "Save", Resources.Lang.Add);

            TemplateHandler.Assign(ref sportSetting, "Deductions", Resources.Lang.Deductions);
            TemplateHandler.Assign(ref sportSetting, "CashOutDeductionNote", Resources.Lang.CashOutDeductionNote);
            TemplateHandler.Assign(ref sportSetting, "TierSetting", Resources.Lang.TierSetting);
            TemplateHandler.Assign(ref sportSetting, "Tier", Resources.Lang.Tier);
            TemplateHandler.Assign(ref sportSetting, "MinAmount", Resources.Lang.MinAmount);
            TemplateHandler.Assign(ref sportSetting, "MaxAmount", Resources.Lang.MaxAmount);
            TemplateHandler.Assign(ref sportSetting, "MinQM", Resources.Lang.MinQM);
            TemplateHandler.Assign(ref sportSetting, "MaxQM", Resources.Lang.MaxQM);
            TemplateHandler.Assign(ref sportSetting, "Commission", Resources.Lang.Comm);
            TemplateHandler.Assign(ref sportSetting, "Action", Resources.Lang.Action);
            TemplateHandler.Assign(ref sportSetting, "AddTier", Resources.Lang.AddTier);
            TemplateHandler.Assign(ref sportSetting, "Edit", Resources.Lang.Edit);
            TemplateHandler.Assign(ref sportSetting, "Delete", Resources.Lang.Delete);
            TemplateHandler.Assign(ref sportSetting, "MinQMExplain", Resources.Lang.MinQMExplain);
            TemplateHandler.Assign(ref sportSetting, "MaxQMExplain", Resources.Lang.MaxQMExplain);
            TemplateHandler.Assign(ref sportSetting, "MinAmountExplain", Resources.Lang.MinAmountExplain);
            TemplateHandler.Assign(ref sportSetting, "MaxAmountExplain", Resources.Lang.MaxAmountExplain);

            string deductionOption = TemplateHandler.CopyBlock(sportSetting, "DeductionType");
            string cashOutWLOption = TemplateHandler.CopyBlock(sportSetting, "CashOutWinLoss");
            string cashOutWLShareOption = TemplateHandler.CopyBlock(cashOutWLOption, "CashOutWLShareOption");
            string tierRow = TemplateHandler.CopyBlock(sportSetting, "TierRow");
            string slOption = TemplateHandler.CopyBlock(tierRow, "SelectOption");

            string tierRowTpl = tierRow;
            TemplateHandler.ReplaceBlock(ref tierRowTpl, "SelectOption", GetCommissionOptionList(0, slOption));
            TemplateHandler.Assign(ref settingTemplate, "TierRowTpl_val", js.Serialize(tierRowTpl));

            DataTable SportType = commonSetting.ResultSet.Tables[0];
            DataTable DeductionType = commonSetting.ResultSet.Tables[1];

            TemplateHandler.Assign(ref settingTemplate, "GroupName_val", string.Empty);
            TemplateHandler.Assign(ref settingTemplate, "Description_val", string.Empty);

            StringBuilder result = new StringBuilder();
            IList<int> sportIDArr = new List<int>();
            bool IsFirstSport = true;

            foreach (DataRow sport in SportType.Rows)
            {
                SportSettingObj sportSettingObj = SportSettingObj.GetInstance();
                sportSettingObj.id = Convert.ToInt32(sport["SportID"]);
                sportSettingObj.sportName = sport["SportName"].ToString();

                sportIDArr.Add(Convert.ToInt32(sport["SportID"]));
                string _sportSetting = sportSetting;
                TemplateHandler.Assign(ref _sportSetting, "SportName", sport["SportName"]);
                string copyText = Resources.Lang.CopySettingText;
                TemplateHandler.Assign(ref copyText, "SportName_val", sport["SportName"]);
                TemplateHandler.Assign(ref _sportSetting, "CopyAllText", copyText);
                TemplateHandler.Assign(ref _sportSetting, "SportID_val", sport["SportID"]);

                DataRow[] drDeductionType = DeductionType.Select("SportID=" + sport["SportID"]);

                StringBuilder deductionList = new StringBuilder();

                if (IsFirstSport)
                {
                    sportSettingObj.isEnabled = true;
                    IsFirstSport = false;
                    TemplateHandler.Assign(ref _sportSetting, "sport_color_val", SportStatusColor.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _sportSetting, "icon_enable_val", IconSportStatus.GetInstance().Enabled);
                    TemplateHandler.Assign(ref _sportSetting, "collapse_expand_val", SportTabSwitchIcon.GetInstance().Expand);
                    TemplateHandler.Assign(ref _sportSetting, "tab_content_display_val", SportTabSwitch.GetInstance().Expand);
                    TemplateHandler.Assign(ref _sportSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffEnable);
                }
                else
                {
                    sportSettingObj.isEnabled = false;
                    TemplateHandler.Assign(ref _sportSetting, "sport_color_val", SportStatusColor.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _sportSetting, "icon_enable_val", IconSportStatus.GetInstance().Disabled);
                    TemplateHandler.Assign(ref _sportSetting, "collapse_expand_val", SportTabSwitchIcon.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _sportSetting, "tab_content_display_val", SportTabSwitch.GetInstance().Collapse);
                    TemplateHandler.Assign(ref _sportSetting, "chkbox_copy_val", CheckboxStatus.GetInstance().OffDisable);
                }

                string iconExplain = Resources.Lang.EnableSportExplain;
                TemplateHandler.Assign(ref iconExplain, "SportName_val", sport["SportName"]);
                TemplateHandler.Assign(ref _sportSetting, "icon_enable_explain_val", iconExplain);

                //Load Deduction Settings
                foreach (DataRow dtype in drDeductionType)
                {
                    DeductionTypeObj deductionObj = DeductionTypeObj.GetInstance();
                    deductionObj.id = Convert.ToInt32(dtype["dtypeid"]);
                    deductionObj.name = dtype["dtypename"].ToString();
                    deductionObj.isCheck = false;
                    deductionObj.deductedPercentage = CastAide.AsDouble(dtype["deductedpercentage"]);
                    sportSettingObj.deductions.Add(deductionObj);

                    string _deductionOption;
                    if (deductionObj.name == "Cash Out Win Loss Share")
                    {
                        _deductionOption = cashOutWLOption;
                        TemplateHandler.ReplaceBlock(ref _deductionOption, "CashOutWLShareOption", GetCashOutWLShareOptionList(0, cashOutWLShareOption));
                        TemplateHandler.Assign(ref _deductionOption, "Deduction_val", 0);
                    }
                    else
                    {
                        _deductionOption = deductionOption;
                    }
                    TemplateHandler.Assign(ref _deductionOption, "SportID_val", sport["SportID"]);
                    TemplateHandler.Assign(ref _deductionOption, "DeductionID_val", dtype["dtypeid"]);
                    TemplateHandler.Assign(ref _deductionOption, "DeductionDescription_val", dtype["dtypedesc"]);
                    TemplateHandler.Assign(ref _deductionOption, "DeductionName_val", dtype["dtypename"]);
                    TemplateHandler.Assign(ref _deductionOption, "chkActiveDeductionClass_val", CheckboxStatus.GetInstance().OffEnable);
                    TemplateHandler.ReplaceBlock(ref tierRowTpl, "SelectOption", GetCommissionOptionList(0, slOption));
                    deductionList.Append(_deductionOption);
                }
                //Load TierList=no info
                TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", Common.NoInfo(7));
                //Disable Button AddTier
                TemplateHandler.Assign(ref _sportSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);

                if (deductionList.Length == 0)
                {
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "Deduction", string.Empty);
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref _sportSetting, "DeductionOption", deductionList.ToString());
                }
                TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", Common.NoInfo(7));
                TemplateHandler.Assign(ref _sportSetting, "btnAddStatus_val", IconAddTier.GetInstance().Enabled);

                result.Append(_sportSetting);
                obj.sportSetting.Add(sportSettingObj);
            }

            TemplateHandler.Assign(ref settingTemplate, "chkActiveAllClass_val", CheckboxStatus.GetInstance().OffEnable);

            TemplateHandler.ReplaceBlock(ref settingTemplate, "SportSetting", result.ToString());
            TemplateHandler.Assign(ref settingTemplate, "GroupSetting_val", js.Serialize(obj));

            //string t = js.Serialize(obj);

            return settingTemplate;
        }

        public static string GetAssignmentForm(string affName, int affID, bool isApproved, string MailOption)
        {
            string assignTemplate = TemplateHandler.LoadTemplate("GroupSetting/GroupAssignment.htm");
            TemplateHandler.Assign(ref assignTemplate, "XsrfTokenFormValue", AntiForgeryHelper.GenerateXsrfFormToken(HttpContext.Current));
            TemplateHandler.Assign(ref assignTemplate, "Assignee", Resources.Lang.Assignee);
            TemplateHandler.Assign(ref assignTemplate, "AssignCommGroup", Resources.Lang.AssignCommGroup);
            TemplateHandler.Assign(ref assignTemplate, "AddNew", Resources.Lang.AddNew);
            TemplateHandler.Assign(ref assignTemplate, "Save", Resources.Lang.Save);
            TemplateHandler.Assign(ref assignTemplate, "CommGroup", Resources.Lang.CommGroup);
            TemplateHandler.Assign(ref assignTemplate, "ListAff_val", affName);
            TemplateHandler.Assign(ref assignTemplate, "mailoption", MailOption);
            TemplateHandler.Assign(ref assignTemplate, "affid", affID);

            if (!isApproved)
            {
                TemplateHandler.RemoveBlock(ref assignTemplate, "ApproveAffiliate");
                TemplateHandler.Assign(ref assignTemplate, "IsApproved_val", "false");
            }
            else
            {
                TemplateHandler.Assign(ref assignTemplate, "IsApproved_val", "true");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            TemplateHandler.Assign(ref assignTemplate, "CheckboxStatus_val", js.Serialize(CheckboxStatus.GetInstance()));
            string optionTemplate = TemplateHandler.CopyBlock(assignTemplate, "SelectOption");
            StringBuilder list = new StringBuilder();
            DbResult result = DataHandler.GroupSetting.CommGroupSelListNormal(affID);
            int groupid = CastAide.AsInt32(result.Outputs["groupid"], -1);
            TemplateHandler.Assign(ref assignTemplate, "OldGroupID_val", groupid);

            foreach (DataRow row in result.ResultSet.Tables[0].Rows)
            {
                string option = optionTemplate;

                if (groupid != -1 && groupid == CastAide.AsInt32(row["groupid"]))
                {
                    TemplateHandler.Assign(ref option, "selected_val", "selected");
                }
                else
                {
                    TemplateHandler.Assign(ref option, "selected_val", string.Empty);
                }
                TemplateHandler.Assign(ref option, "option_value", row["groupid"]);
                TemplateHandler.Assign(ref option, "option_name", row["groupname"]);
                list.Append(option);
            }
            if (affID > 0)
            {
                if (groupid > 1000)
                {
                    TemplateHandler.Assign(ref assignTemplate, "slCommGroupDisabled_val", "disabled");
                    TemplateHandler.Assign(ref assignTemplate, "chkBoxClass_val", CheckboxStatus.GetInstance().OnEnable);
                }
                else
                {
                    TemplateHandler.Assign(ref assignTemplate, "slCommGroupDisabled_val", string.Empty);
                    TemplateHandler.Assign(ref assignTemplate, "chkBoxClass_val", CheckboxStatus.GetInstance().OffEnable);
                }
            }
            else
            {
                TemplateHandler.Assign(ref assignTemplate, "slCommGroupDisabled_val", string.Empty);
                TemplateHandler.RemoveBlock(ref assignTemplate, "PrivateGroup");
            }
            if (groupid > 1000)
            {
                TemplateHandler.Assign(ref assignTemplate, "isUsePrivateGroup_val", "true");
            }
            else
            {
                TemplateHandler.Assign(ref assignTemplate, "isUsePrivateGroup_val", "false");
            }
            TemplateHandler.ReplaceBlock(ref assignTemplate, "SelectOption", list.ToString());
            return assignTemplate;
        }
    }
}