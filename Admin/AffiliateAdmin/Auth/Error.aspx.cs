using System;

public partial class Error : System.Web.UI.Page
{
    protected string errorStr = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string errCode = Request["err"];

        //switch (errCode)
        //{
        //    case "403":
        //        errorStr = Resources.Lang.HttpError_403;
        //        break;
        //    case "404":
        //        errorStr = Resources.Lang.HttpError_404;
        //        break;
        //    default:
        //        errorStr = Resources.Lang.HttpError_Default;
        //        break;
        //}
        //if(string.IsNullOrEmpty(errorStr))
        //    errorStr = Resources.Lang.HttpError_Default;
        //Logs.Write("System Error - "+DateTime.Now.ToString()+":"+Request["aspxerrorpath"]);
    }
}
