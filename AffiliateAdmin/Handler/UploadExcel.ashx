<%@ WebHandler Language="C#" Class="UploadExcel" %>

using System;
using System.Xml;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;
using DNAide.Core;
using System.IO;

public class UploadExcel : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        
        IBCHandler.InitializeCulture(HttpContext.Current);
        context.Response.ContentType = "text/html";
        DateTime date = CastAide.AsDateTime(context.Request["fdate"]);
        //if (context.Request["hdHasData"] == "0")
        //{
        //    if (DataHandler.Casino.CasinoHasData(date))//Confirm user replace data or not
        //    {
        //        context.Response.Write("<script type=\"text/javascript\">parent.confirmUpdate('" + context.Request["fdate"] + "');</script>");
        //        return;
        //    }
        //}
        HttpPostedFile fileUpload = context.Request.Files[0];

        string filePath = context.Server.MapPath("../") + "ExcelFiles\\Casino\\" + date.ToString("yyyy") + "\\" + date.ToString("MMM") + "\\";
        string strDst = string.Empty;
        int errcode = 0;
        string fileName = fileUpload.FileName;// "Casino Stat " + date.ToString("yyyyMMMdd");


        Common.UploadFileExcel(filePath, fileUpload, ref fileName, ref errcode);

        if (errcode == 1)
        {
            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ExcelErrmsgEmpty + "');</script>");
            return;
        }
        else if (errcode == 2)
        {
            context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ExcelErrmsgFile + "');</script>");
            return;
        }
        /*Read file excel and insert to database*/
        ReadFileExcel(context, fileName, date);
    }
    public void ReadFileExcel(HttpContext context, string fileName, DateTime date)
    {
        try
        {
            fileName = fileName.Replace("\\", "/");

            string ext = fileName.Substring(fileName.LastIndexOf('.') + 1, fileName.Length - fileName.LastIndexOf('.') - 1);
            XmlDocument doc = new XmlDocument();

            //Update database here
            DbResult rs = DataHandler.Casino.CasinoDataUpdate(doc.OuterXml, true, date);//Alway replace old data
            //DbResult rs = DataHandler.Casino.CasinoDataUpdate(doc.OuterXml, (context.Request["hdHasData"] == "1"), date);        

            if (CastAide.AsInt32(rs.Outputs["errcode"]) == 0)//Show list success trans, error trans here
            {
                if (rs.ResultSet.Tables.Count > 0)
                {
                    string strTemp = TemplateHandler.LoadTemplate("Casino/UploadExcel.htm");
                    TemplateHandler.Assign(ref strTemp, "date", Resources.Lang.Date);
                    TemplateHandler.Assign(ref strTemp, "username", Resources.Lang.Username);
                    TemplateHandler.Assign(ref strTemp, "currency", Resources.Lang.Currency);
                    TemplateHandler.Assign(ref strTemp, "turnover", Resources.Lang.Turnover);
                    TemplateHandler.Assign(ref strTemp, "winlost", Resources.Lang.Winlost);
                    TemplateHandler.Assign(ref strTemp, "deposit", Resources.Lang.Deposit);
                    TemplateHandler.Assign(ref strTemp, "bonus", Resources.Lang.Bonus);

                    context.Response.Write("<script type=\"text/javascript\">parent.UpdateSucc('");
                    context.Response.Write("<div style=\"float: left; width: 530px; display: inline; padding-right:20px;\">" + buildTableHtml(rs.ResultSet.Tables[0], strTemp, true) + "</div>");
                    if (rs.ResultSet.Tables[1].Rows.Count > 0)
                        context.Response.Write("<div style=\"display: inline;\">" + buildTableHtml(rs.ResultSet.Tables[1], strTemp, false) + "</div>");
                    context.Response.Write("');</script>");
                }
            }
            else//Show error message here
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + rs.Outputs["errmsg"] + "\");</script>");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + ex.Message + "\");</script>");
        }
        
    }

    string buildTableHtml(DataTable dtb, string strTpl, bool isSuccess)
    {
        string strResult = TemplateHandler.CopyBlock(strTpl, "hasdata");
        TemplateHandler.Assign(ref strResult, "listtranstext", isSuccess ? "List Success Transactions" : "List Error Transactions");
        TemplateHandler.Assign(ref strResult, "cssmsg", isSuccess ? "tbl_htext" : "cont12oe");
        string rowTpl = TemplateHandler.CopyBlock(strResult, "content");

        StringBuilder strBuild = new StringBuilder();
        int i = 0;
        foreach (DataRow dr in dtb.Rows)
        {
            string rowHTML = rowTpl;
            i++;
            TemplateHandler.Assign(ref rowHTML, "no", i);
            TemplateHandler.Assign(ref rowHTML, "color", (i % 2 == 0 ? Common.RowBgEven : Common.RowBgOdd));
            TemplateHandler.Assign(ref rowHTML, "date-val", CastAide.AsDateTime(dr["DateImport"]).ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref rowHTML, "username-val", dr["username"].ToString().Replace("12bet$", string.Empty));
            TemplateHandler.Assign(ref rowHTML, "currency-val", dr["currency"]);
            TemplateHandler.Assign(ref rowHTML, "turnover-val", Common.FormatNumber(dr["turnover"]));
            TemplateHandler.Assign(ref rowHTML, "winlost-val", Common.FormatNumber(dr["winlost"]));
            TemplateHandler.Assign(ref rowHTML, "deposit-val", Common.FormatNumber(dr["deposit"]));
            TemplateHandler.Assign(ref rowHTML, "bonus-val", Common.FormatNumber(dr["bonus"]));
            strBuild.Append(rowHTML);
        }
        TemplateHandler.ReplaceBlock(ref strResult, "content", strBuild.ToString());

        return strResult;
    }

    
    public bool IsReusable {
        get {
            return false;
        }
    }

}