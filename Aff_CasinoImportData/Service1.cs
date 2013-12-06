using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using log4net;
using DNAide.Utils;
using DNAide.Web;
//using Fanex.Logging;
using Fanex.MailDeliver.Model;
using Fanex.MailDeliver.MailClient.API;


namespace Aff_CasinoImportData
{
    public partial class Service1 : ServiceBase
    {
        private Timer serviceTimer;
        //private ILog log;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                TimerCallback timerDelegate = new TimerCallback(DoWork);
                //call Dowork after the time is configured as TimeInterval in web.config
                serviceTimer = new Timer(timerDelegate, null, int.Parse(Common.DueTime), int.Parse(Common.TimeInterval));
            }
            catch (Exception ex)
            {
                DNAideLog.Write(ex.Message);
            }
        }

        private void DoWork(object state)//Check data and send email here
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
        protected override void OnStop()
        {
        }

        void UploadCasinoData(Common.DateImport importDate, string logMessage)
        {
            int errCode = 0;
            string filePath = string.Empty;
            string errMsg = string.Empty;

            DateTime date = (importDate == Common.DateImport.Yesterday ? DateTime.Now.AddDays(-1) : DateTime.Now);
            Common.UploadFileExcel(importDate, ref filePath);

            XmlDocument docXML = Common.ConvertCSVToXML(filePath);
            
            DAL.DataAccess.CasinoDataUpdate(docXML.OuterXml, date, ref errCode, ref errMsg);

            DNAideLog.Write(logMessage);
            if (errCode != 0)
            {
                DNAideLog.Write(errMsg);
                SendEmail(errMsg);
            }
        }

        void SendEmail(string Content)
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
