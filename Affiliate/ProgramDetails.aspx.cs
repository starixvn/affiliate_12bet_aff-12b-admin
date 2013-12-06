using System;
using System.Web;
using DataHandler;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Text;
using System.Data;
public partial class ProgramDetails : RootPage
{
    public string mail = "";
    public StringBuilder tableBody = new StringBuilder();
    public const int MAXQM = 1000000000;
    public const Int64 MAXAMOUNT = 100000000000000;

    protected void Page_Load(object sender, EventArgs e)
    {
        mail = "<a href='mailto:{mail}'>{mail}</a>".Replace("{mail}", AppSettings.MailContact);

        string tplTier = "<tr>" +
                            "<td bgcolor=\"#EEEAEA\" style=\"padding-left:10px\" align=\"left\">" +
                                "{minam-val}  {maxam-val}" +
                            "</td>" +
                            "<td bgcolor=\"#EEEAEA\" style=\"padding-left:10px\" align=\"left\">" +
                                "{minqm-val} {maxqm-val}" +
                            "</td>" +
                            "<td bgcolor=\"#EEEAEA\" style=\"padding-left:10px\" align=\"left\">" +
                                "{commission-val}" +
                            "</td>" +
                        "</tr>";
        
        DbResult result = CommGroup.GetCommGroupSettingByGroupId(101);
        if (result.ResultSet.Tables.Count < 5 || result.ResultSet.Tables[0].Rows.Count <= 0)
        {
            tableBody.Append("<tr colspan='3'>There is no information available</tr>");
        }


        DataTable SportType = result.ResultSet.Tables[3];
        DataTable commGroup = result.ResultSet.Tables[0];
        DataTable GroupSetting = result.ResultSet.Tables[1];

        DataRow[] Tiers = GroupSetting.Select("SportType=1");//1 is the identity of sportbook 

        int noTier = 0;
        int totalShownTier = Tiers.Length;
        foreach (DataRow tier in Tiers)
        {
            if (CastAide.AsDouble(tier["Commission"]) != 0)
            {
                string _tierRow = tplTier;
                //Only allow to delete last tier

                TemplateHandler.Assign(ref _tierRow, "minam-val", Common.FormatNumber(Convert.ToInt64(tier["MinAmount"])));//String.Format("{0:0.00}", Convert.ToInt64(tier["MinAmount"])));
                TemplateHandler.Assign(ref _tierRow, "minqm-val", Convert.ToInt32(tier["MinQM"]));
                if (noTier++ < totalShownTier - 1)
                {
                    TemplateHandler.Assign(ref _tierRow, "maxam-val", " - " + Common.FormatNumber(Convert.ToInt64((tier["MaxAmount"])) + 0.99));
                    TemplateHandler.Assign(ref _tierRow, "maxqm-val", " - " + GetMaxQMDisplay(tier["MaxQM"]));
                }
                else
                {
                    TemplateHandler.Assign(ref _tierRow, "maxam-val", GetMaxAmountDisplay(tier["MaxAmount"]));
                    TemplateHandler.Assign(ref _tierRow, "maxqm-val", GetMaxQMDisplay(tier["MaxQM"]));
                }
                TemplateHandler.Assign(ref _tierRow, "commission-val", (Convert.ToDouble(tier["Commission"]) * 100).ToString() + "%");
                tableBody.Append(_tierRow);
            }
            else
            {
                totalShownTier--;
            }
        }

    }

    public static string GetMaxQMDisplay(object max)
    {
        if (Convert.ToInt32(max) >= MAXQM)
        {
            return "+";
        }
        return Convert.ToInt32(max).ToString();
    }

    public static string GetMaxAmountDisplay(object max)
    {
        if (Convert.ToInt64(max) >= MAXAMOUNT)
        {
            return "+";
        }
        return String.Format("{0:0.00}", Convert.ToDouble(max) + 0.99);
    }
}
