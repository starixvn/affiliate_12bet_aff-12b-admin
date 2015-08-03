<%@ WebHandler Language="C#" Class="AddMessage" %>

using System;
using System.Web;
using DNAide.Data;
using DNAide.Utils;
using DNAide.Core;
using System.Data;
using System.Web.Security;
using System.Web.SessionState;
using DataHandler;
using System.Text;
using System.Text.RegularExpressions;

public class AddMessage : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.UrlReferrer != null)
        {

            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string t = context.Request.Params["params"];
            string[] param = t.Split(new string[] { "$$$" }, 10, StringSplitOptions.None);
            bool important = false;
            string msgEN = string.Empty, msgCN = string.Empty, msgTW = string.Empty, msgTH = string.Empty, msgES = string.Empty, msgVI = string.Empty, msgID = string.Empty, msgKR = string.Empty, msgJP = string.Empty;
            StringBuilder result = ValidateData(param, out important, ref msgEN, ref msgCN, ref msgTW, ref msgTH, ref msgES, ref msgVI, ref msgID, ref msgKR, ref msgJP);
            if (string.IsNullOrEmpty(result.ToString()))
            {
                result.Append(DataHandler.HotNews.AddHotNews(msgEN, msgCN, msgTW, msgTH, msgES, msgVI, msgID, msgKR, msgJP, important));

            }
            context.Response.Write(result.ToString());


        }

    }

    private StringBuilder ValidateData(string[] param, out bool important, ref string msgEN, ref string msgCN, ref string msgTW, ref string msgTH, ref string msgES, ref string msgVI, ref string msgID, ref string msgKR, ref string msgJP)
    {

        StringBuilder err = new StringBuilder("");
        important = false;
        try
        {
            if (param.Length == 10)
            {
                important = Convert.ToBoolean(param[0].Trim());
                msgEN = param[1].Trim();
                msgCN = param[2].Trim();
                msgTW = param[3].Trim();
                msgTH = param[4].Trim();
                msgES = param[5].Trim();
                msgVI = param[6].Trim();
                msgID = param[7].Trim();
                msgKR = param[8].Trim();
                msgJP = param[9].Trim();
            }

        }
        catch (Exception e)
        {
            return err.Append(e.Message);

        }
        return err;

    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}
