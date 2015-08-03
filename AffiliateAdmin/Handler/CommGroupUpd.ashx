<%@ WebHandler Language="C#" Class="CommGroupUpd" %>

using System;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using GenerateHTML;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

public class CommGroupUpd : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string groupSetting = context.Request.Params["params"];
            GroupSettingObj obj = GetInstanceFromJSON(groupSetting);

            DbResult result;
            try
            {
               
                if (obj.isEdited)
                {
                    result = DataHandler.GroupSetting.CommGroupUpd(obj, context.Request.UserHostAddress);
                }
                else
                {
                    if (obj.isApproved)
                    {
                        obj.password = DNAide.Utils.Encryption.RandomString(6);
                        result = DataHandler.GroupSetting.CommGroupIns(obj, context.Request.UserHostAddress);
                        if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                        {
                            bool MailOption = CastAide.AsBoolean(result.Outputs["mailoption"], true);//Send relevant email format
                            Common.SendEmailApproved(obj.id, obj.language, obj.fName, obj.affName, obj.email,obj.password, Common.EmailSuffix(MailOption));                            
                        }

                    }
                    else
                        result = DataHandler.GroupSetting.CommGroupIns(obj, context.Request.UserHostAddress);  
                    
                }
                context.Response.Write(result.Outputs["errmsg"]);
                
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);              
#if DEBUG
                throw;

#else
                DNAide.Web.DNAideLog.Write(e.Message);
#endif

            }
           
            
        }   
      
    }
    public GroupSettingObj GetInstanceFromJSON(string json)
    {

        using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GroupSettingObj));

            GroupSettingObj obj = (GroupSettingObj)serializer.ReadObject(memoryStream);

            memoryStream.Close();

            return (GroupSettingObj)obj;

        }

    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
