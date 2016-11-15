using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using GenerateHTML;

namespace AffiliateAdmin.Business.Services
{
    public class CommGroupUpdService
    {
        public void CommGroupUpd(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                try
                {
                    AntiForgeryHelper.ValidateXsrfToken(context);
                }
                catch
                {
                    context.Response.Write(Resources.Lang.Err500);
                    return;
                }

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
                                Common.SendEmailApproved(obj.id, obj.language, obj.fName, obj.affName, obj.email, obj.password, Common.EmailSuffix(MailOption));
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
                Fanex.Logging.LogService.Error(e.Message);
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
    }
}