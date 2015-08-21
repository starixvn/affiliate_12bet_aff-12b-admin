namespace AdminAff12Bet.UI._BetList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI;    
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;    
    using Sunplus.Agent.Model.BL.Definitions;
    using Sunplus.Agent.Model.BL.Forecast;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Utilities;
    using Fanex.BetList.Age.BetListCmd;
    using Fanex.BetList.Age.Definition;
    using Fanex.BetList.Age.DTO;
    using Fanex.BetList.Age.ExtTemplate;
    using Fanex.BetList.Age.BetListExcelBuilderExt;
    using Fanex.BetList.Age.BetListHTMLBuilderExt;
    using Fanex.BetList.Core.TicketHelper;

    public partial class BetList : Fanex.Age.UI.Reports.WinlossReports_Page
    {
        /*protected void Page_Load(object sender, EventArgs e)
        {

        }*/

        string type = string.Empty;
        string title = string.Empty;
        int custId;
        int bettype;
        DateTime dateFrom, dateTo;

        bool exportExcel = false;
        IBetListCmd command = null;
        DataTable commandData;

        public override void RenderContent(HtmlTextWriter writer)
        {


            UserTypes userType = UserTypes.Member;
            BetListType betlistType = BetListType.Winlost;
            DataTable betListData = View as DataTable;

            GetCachePropertyById funcGetOddsTypeName = CacheRef.GetOddsTypeName;
            ITicketHelper ticketHelper = new TicketHelper();
            ticketHelper.CurrentLanguage = UserSession.Current.LangKey;

            IList<ITicket> tickets = new List<ITicket>();

            string backupKey = "IP";
            string backupValue = string.Empty;

            if (TicketMapper.dict.ContainsKey(backupKey))
            {
                backupValue = TicketMapper.dict[backupKey];
                TicketMapper.dict.Remove(backupKey);
            }

            if (betListData != null)
            {
                tickets = TicketTransformer.Transform(betListData);
                betListData.Dispose();
            }
            var ticketData = new List<ITicketData>();

            if (commandData != null)
            {
                ticketData = TicketDataTransformer.Transform(commandData);
                commandData.Dispose();
            }

            if (!string.IsNullOrEmpty(backupValue) && !TicketMapper.dict.ContainsKey(backupKey))
            {
                TicketMapper.dict.Add(backupKey, backupValue);
            }

            bool isAdminCanViewVNIP = false;
            int index = 0;

            // generate HTML only, no need ExportEXCEL option
            BetListTemplate tpl = new BetListTemplate();
            StringBuilder content = new StringBuilder();

            tpl.title = "Member Bet List " + dateFrom.ToShortDateString();

            OpenBetList_Template betlistOpenTag = new OpenBetList_Template();
            content.Append(betlistOpenTag);
            Header1_Template betlistHeader = new Header1_Template();
            betlistHeader.Member.Hide();
            betlistHeader.Agent.Hide();
            betlistHeader.Super.Hide();
            betlistHeader.Master.Hide();

            content.Append(betlistHeader);
            content.Append("<tbody>");

            bool isShowScoremap = false;

            BetListHTMLFactory factory = new BetListHTMLFactory();
            BetListHTMLBuilder builder = factory.CreateBuilder();

            foreach (ITicket ticket in tickets)
            {
                index++;
                ticketHelper.Index = index;

                builder.Append("<tr>")
                       .AddNo(index, "<td class='w-order'>", "</td>")
                       .AddTrans(ticket, "<td class='c'>", "</td>")
                       .AddChoice(ticket, ticketHelper, ticketData, "<td class='r bl_evt'><div>", "</div></td>", isShowScoremap)
                       .AddOdds(ticket, ticketData, "<td class='bl_underdog'>", "</td>", funcGetOddsTypeName)
                       .AddStake(ticket, "<td class='bl_underdog'>", "</td>")
                       .AddWinLoss(ticket, "<td class='r' style=\"width: 50px;\">", "</td>")
                       .AddStatus(ticket, ticketHelper, ticketData, "<td class='c'>", "</td>", isAdminCanViewVNIP)
                       .Append("</tr>");
            }

            builder.AddFooter(tickets, string.Empty, string.Empty, userType, (Fanex.BetList.Age.Definition.BetListType)betlistType);
            content.Append(builder.GetBetList());
            content.Append("</tbody></table>");

            tpl.content = content.ToString();
            tpl.ToString(writer);
        }

        public override object InvokeData(Hashtable hash)
        {
            DataTable dtResult = null;

            command = BetListFactory.CreateBetList(type, CurrentUserSession.NetSessionId, _permission);
            if (command != null)
            {
                //MixParlay list
                if (type.Equals("MixParlay"))
                {
                    hash.Add("bettype", 9);
                }
                //Combination MP list
                else if (type.Equals("CombinationMixParlay"))
                {
                    hash.Add("bettype", 29);
                }
                else
                {
                    hash.Add("custid", custId);
                    hash.Add("fdate", dateFrom);
                    hash.Add("tdate", dateTo);
                    hash.Add("bettype", bettype);
                }

                command.SetParams(hash);
                dtResult = command.Execute() as DataTable;
            }
            return dtResult;
        }

        public override void ProcessRequest(ref Hashtable hash)
        {
            base.ProcessRequest(ref hash);

            type = Request["type"];

            if (!type.Equals("MixParlay") && !type.Equals("CombinationMixParlay"))
            {
                if (Request["fdate"] == null || Request["tdate"] == null)
                {
                    hash.Add("DateIsNull", true);
                }

                int.TryParse(Request["custid"] + string.Empty, out custId);
                int.TryParse(Request["bettype"] + string.Empty, out bettype);
                dateFrom = Cast.AsDateTime(Request["fdate"]);
                dateTo = Cast.AsDateTime(Request["tdate"]);

                hash.Add("language", Request["lang"] + string.Empty);
                hash.Add("username", Request["username"] + string.Empty);
                hash.Add("race", Request["race"] + string.Empty);
                hash.Add("matchid", Request["matchid"] != null ? int.Parse(Request["matchid"]) : 0);
                hash.Add("leagueid", Request["leagueid"] != null ? int.Parse(Request["leagueid"]) : 0);
                hash.Add("islive", Request["islive"] != null ? int.Parse(Request["islive"]) : 0);
                hash.Add("transtype", Request["transtype"] != null ? int.Parse(Request["transtype"]) : 0);
                hash.Add("betteam", Request["betteam"] + string.Empty);
            }
            else
            {
                dateFrom = DateTime.Now;
                dateTo = DateTime.Now;
            }

            exportExcel = false;
        }
    }
}