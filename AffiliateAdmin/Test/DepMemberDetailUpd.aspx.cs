using System;
using DNAide.Core;

public partial class DepMemberDetailUpd : System.Web.UI.Page
{
    protected string DepositUsername = string.Empty;
    protected string IBCUsername = string.Empty;
    protected int IBCCustId = 0;
    protected int AffId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string DepositUsername = Request["txtDepUsername"];
        string IBCUsername = Request["txtIBCUsername"];
        string ErrMsg = string.Empty;
        int ErrCode = 0;
        int IBCCustId = CastAide.AsInt32(Request["txtIBCCustId"]);
        int AffId = CastAide.AsInt32(Request["txtAffId"]);

        if (IBCCustId == 0 || AffId == 0)
            return;

        DataHandler.Affiliate.Update12BetPlayerStatusToDeposit(AffId, DepositUsername, IBCCustId, IBCUsername, out ErrCode, out ErrMsg);
        if (ErrCode != 0)
        {
            Response.Write("<script type='text/javascript'>alert('" + ErrMsg.Replace("'", string.Empty) + "')</script>");
        }
    }
}