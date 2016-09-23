using System;
using System.Text;
using System.Web;

namespace AffiliateUser.Business.Services
{
    public class GetMessageListService
    {
        public void GetMessageList(HttpContext context)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string[] param = context.Request.Params["params"].Split(";".ToCharArray());

            int important = -1, curpage = 1;
            object fromdate = null, todate = null;
            StringBuilder result = ValidateData(param, ref important, ref fromdate, ref todate, ref curpage);

            context.Response.Write(result.ToString());
        }

        private StringBuilder ValidateData(string[] param, ref int important, ref object fromdate, ref object todate, ref int curPage)
        {
            StringBuilder err = new StringBuilder("");

            try
            {
                if (param.Length == 4)
                {
                    important = Convert.ToInt32(param[0].Trim());
                    if (param[1].Trim() != "")
                    {
                        fromdate = param[1].Trim();
                    }
                    if (param[2].Trim() != "")
                    {
                        todate = param[2].Trim();
                    }
                    curPage = Convert.ToInt32(param[3].Trim());
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