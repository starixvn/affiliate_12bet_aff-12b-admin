using System;
using System.Text;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Message
{
    public class UpdateMessageService
    {
        public ResultInfo Update(string data)
        {
            string[] param = data.Split(new string[] { "$$$" }, 11, StringSplitOptions.None);
            bool important = false;
            int id = -1;
            string msgEN = string.Empty, msgCN = string.Empty, msgTW = string.Empty, msgTH = string.Empty, msgES = string.Empty, msgVI = string.Empty, msgID = string.Empty, msgKR = string.Empty, msgJP = string.Empty; ;
            StringBuilder result = ValidateData(param, ref id, out important, ref msgEN, ref msgCN, ref msgTW, ref msgTH, ref msgES, ref msgVI, ref msgID, ref msgKR, ref msgJP);
            var resultMessage = result.ToString();

            if (string.IsNullOrEmpty(resultMessage))
            {
                resultMessage = DataHandler.HotNews.UpdateHotNews(id, msgEN, msgCN, msgTW, msgTH, msgES, msgVI, msgID, msgKR, msgJP, important);
            }

            return new ResultInfo { Message = resultMessage };
        }

        private StringBuilder ValidateData(string[] param, ref int msgid, out bool important, ref string msgEN, ref string msgCN, ref string msgTW, ref string msgTH, ref string msgES, ref string msgVI, ref string msgID, ref string msgKR, ref string msgJP)
        {
            StringBuilder err = new StringBuilder("");
            important = false;
            try
            {
                if (param.Length == 11)
                {
                    msgid = Convert.ToInt32(param[0].Trim());
                    important = Convert.ToBoolean(param[1].Trim());
                    msgEN = param[2].Trim();
                    msgCN = param[3].Trim();
                    msgTW = param[4].Trim();
                    msgTH = param[5].Trim();
                    msgES = param[6].Trim();
                    msgVI = param[7].Trim();
                    msgID = param[8].Trim();
                    msgKR = param[9].Trim();
                    msgJP = param[10].Trim();
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