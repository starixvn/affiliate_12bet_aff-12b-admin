using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using Fanex.MailDeliver.Model;
using Fanex.MailDeliver.MailClient.API;

namespace AffCheckSum
{
    public partial class Service1 : ServiceBase
    {
        private Timer serviceTimer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                TimerCallback timerDelegate = new TimerCallback(DoWork);
                //call Dowork after 1 hour
                serviceTimer = new Timer(timerDelegate, null, int.Parse(Common.DueTime), int.Parse(Common.TimeInterval));
                
            }
            catch (Exception ex)
            {
                DNAide.Web.DNAideLog.Write(ex.Message);
            }
        }

        private void DoWork(object state)//Check data and send email here
        {
            try
            {
                //decide time to compare data(check data 2 times: first at 11:00-12:00, second at 23:00-24:00)
                if (DateTime.Now.Hour == Common.CheckingTime || Common.ForTest)
                {
                    //Call SPs check Data here
                    DateTime date = DateTime.Now.AddDays(Common.ForDate);
                    bool isMatch = false;

                    DbResult rs = DAL.DataAccess.CheckSumMemberDaily(date, out isMatch);

                    //if failed: send email to IT team
                    if (!isMatch)
                    {
                        //SendMailService.SendMail2(Common.MailServer, Common.ToMail, Common.FromMail, Common.CCMail, Common.BCCMail, Common.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]), Common.PasswordMail);
                        SendEmail(Common.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]));

                    }

                    DNAide.Web.DNAideLog.Write("AffCheckSum run for date: " + DateTime.Now.AddDays(Common.ForDate).ToShortDateString() + "; IsMatch: " + isMatch);
                    isMatch = false;
                    DbResult rs1 = DAL.DataAccess.IA_CheckSumMemberDaily(date, out isMatch);
                    if (!isMatch)
                    {
                        //SendMailService.SendMail2(Common.MailServer, Common.ToMail, Common.FromMail, Common.CCMail, Common.BCCMail, Common.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]), Common.PasswordMail);
                        SendEmail(Common.SubjectMailError, IA_BuildBodyEmail(rs1.ResultSet.Tables[0]));

                    }

                    DNAide.Web.DNAideLog.Write("Income Access check sum run for date: " + DateTime.Now.AddDays(Common.ForDate).ToShortDateString() + "; IsMatch: " + isMatch);
                }
            }
            catch (Exception ex)
            {
                DNAide.Web.DNAideLog.Write(ex.Message);
                SendEmail(Common.SubjectMailError, ex.Message);
                //SendMailService.SendMail2(Common.MailServer, Common.ToMail, Common.FromMail, Common.CCMail, Common.BCCMail, Common.SubjectMailError, ex.Message, Common.PasswordMail);
                //SendMailError(termId, ex.Message);
            }
        }
        protected override void OnStop()
        {
        }

        string buildBodyEmail(DataTable dtb)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("This is an auto-mail. Please do not reply. <br /><br />");
            strBuild.Append("There is an error occur when checking sum data between <b>Bettrans and MemberDaily in " + DateTime.Now.AddDays(Common.ForDate).ToLongDateString() + "</b>.<br /><br />");
            strBuild.Append("<table width=\"100%\" border=\"1px\">");
            strBuild.Append(string.Format("<tr bgcolor=\"#e1ebf2\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
                                "Deposit", "Withdrawal", "Bonus", "Adjustment", "Turnover", "Winlost", "Description"));

            foreach (DataRow dr in dtb.Rows)
            {
                strBuild.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
                               NumberFormat.DecFormat(dr["Deposit"], 2), NumberFormat.DecFormat(dr["Withdrawal"], 2), NumberFormat.DecFormat(dr["Bonus"], 2), NumberFormat.DecFormat(dr["Adjustment"], 2), NumberFormat.DecFormat(dr["Turnover"], 2), NumberFormat.DecFormat(dr["Winlost"], 2), dr["Description"]));
            }
            strBuild.Append("</table>");
            strBuild.Append("<br /><br /><b>Auto Email Checksum.</b>");
            return strBuild.ToString();
        }

        string IA_BuildBodyEmail(DataTable dtb)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("This is an auto-mail. Please do not reply. <br /><br />");
            strBuild.Append("There is an error occur when checking sum data between <b>Bettrans and MemberDailyIA in " + DateTime.Now.AddDays(Common.ForDate).ToLongDateString() + "</b>.<br /><br />");
            strBuild.Append("<table width=\"100%\" border=\"1px\">");
            strBuild.Append(string.Format("<tr bgcolor=\"#e1ebf2\"><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td><td>{12}</td></tr>",
                                "Deposit", "Withdrawal", "Bonus", "Adjustment", "Turnover", "Outstanding", "Winlost", "DrawGame", "TurnoverHR", "OutstandingHR", "WinlostHR", "DrawGameHR", "Description"));

            foreach (DataRow dr in dtb.Rows)
            {
                strBuild.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td><td>{12}</td></tr>",
                               NumberFormat.DecFormat(dr["Deposit"], 2), NumberFormat.DecFormat(dr["Withdrawal"], 2), NumberFormat.DecFormat(dr["Bonus"], 2), NumberFormat.DecFormat(dr["Adjustment"], 2), NumberFormat.DecFormat(dr["Turnover"], 2), NumberFormat.DecFormat(dr["Outstanding"], 2), NumberFormat.DecFormat(dr["Winlost"], 2)
                               , NumberFormat.DecFormat(dr["DrawGame"], 2), NumberFormat.DecFormat(dr["TurnoverHR"], 2), NumberFormat.DecFormat(dr["OutstandingHR"], 2), NumberFormat.DecFormat(dr["WinlostHR"], 2), NumberFormat.DecFormat(dr["DrawGameHR"], 2), dr["Description"]));
            }
            strBuild.Append("</table>");
            strBuild.Append("<br /><br /><b>Auto Email Checksum.</b>");
            return strBuild.ToString();
        }

        void SendEmail(string Title, string Content)
        {
                        //SendMailService.SendMail2(Common.MailServer, Common.ToMail, Common.FromMail, Common.CCMail, Common.BCCMail, Common.SubjectMailError, buildBodyEmail(rs.ResultSet.Tables[0]), Common.PasswordMail);
            MailRecord mailItem = new MailRecord
            {
                MailTo = Common.ToMail,
                Subject = Title,
                Title = Title,
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
