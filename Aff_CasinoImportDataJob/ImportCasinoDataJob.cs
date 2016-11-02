// -----------------------------------------------------------------------
// <copyright file="ImportCasinoDataJob.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Aff_CasinoImportDataJob
{
    using System;
    using System.Xml;
    using AffCasinoImportDataAccess;
    using DNAide.Web;
    using Fanex.MailDeliver.MailClient.API;
    using Fanex.MailDeliver.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImportCasinoDataJob
    {
        public void DoWork(object state)//Check data and send email here
        {
            try
            {
                DateTime now = DateTime.Now;

                //Run Yesterday Data at the beginning and the end of today
                if (now.Hour == Common.CheckingTime1 || now.Hour == Common.CheckingTime2 || Common.ForTest)
                {
                    UploadCasinoData(Common.DateImport.Yesterday, "Yesterday - Last Time Run: " + now.ToLongDateString());
                }

                //Import data for today
                UploadCasinoData(Common.DateImport.Today, "Today - Last Time Run: " + now.ToLongDateString());
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message);
                SendEmail(ex.Message);
                //LogService.Error(ex.Message);
            }
        }

        private void UploadCasinoData(Common.DateImport importDate, string logMessage)
        {
            int errCode = 0;
            string filePath = string.Empty;
            string errMsg = string.Empty;

            DateTime date = (importDate == Common.DateImport.Yesterday ? DateTime.Now.AddDays(-1) : DateTime.Now);
            Common.UploadFileExcel(importDate, ref filePath);

            XmlDocument docXML = Common.ConvertCSVToXML(filePath);

            DataAccess.ImportCasinoData(Common.ForSystem, Common.AdminID, docXML.OuterXml, date, ref errCode, ref errMsg);

            DNAideLog.Write(logMessage);
            if (errCode != 0)
            {
                DNAideLog.Write(errMsg);
                SendEmail(errMsg);
            }
        }

        private void SendEmail(string Content)
        {
            //SendMailService.SendMail2(Common.MailServer, Common.ToMail, Common.FromMail, Common.CCMail, Common.BCCMail, Common.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]), Common.PasswordMail);
            MailRecord mailItem = new MailRecord
            {
                MailTo = Common.ToMail,
                Subject = Common.SubjectMailError,
                Title = Common.SubjectMailError,
                Body = Content, //IsBodyHtml ready
                MailBcc = Common.BCCMail,
                MailCc = Common.CCMail
            };
            using (MailDeliver client = new MailDeliver())
            {
                // call send mail service
                client.SendMail(mailItem);
            }
        }
    }
}