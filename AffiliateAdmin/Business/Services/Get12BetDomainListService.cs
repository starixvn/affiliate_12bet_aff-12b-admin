using System;
using System.Text;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class Get12BetDomainListService
    {
        public void Get12BetDomainList(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                var deviceType = context.Request.Params["deviceType"];
                var redirectDomain = context.Request.Params["redirectDomain"];
                var result = AffAdmin.GenerateHTML.Domain.Gen12BetDomainList(deviceType, redirectDomain);

                context.Response.Write(result);
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
    }
}