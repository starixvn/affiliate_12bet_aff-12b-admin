using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.BetList.Core.Entities;

/// <summary>
/// Summary description for TicketResult
/// </summary>
public class TicketResult
{
	public TicketResult()
	{
        MainTicketList = new List<AffTicket>();
        TicketDataList = new List<ITicketData>();
	}

    public List<AffTicket> MainTicketList { get; set; }

    public List<ITicketData> TicketDataList { get; set; }

    // Sum value
    public int TotalRows { get; set; }

    public decimal SumWinLoss { get; set; }

    public decimal SumLicenseeWl { get; set; }

    public decimal SumComm { get; set; }

    public decimal SumPlayerComm { get; set; }

    public decimal SumStake { get; set; }
}