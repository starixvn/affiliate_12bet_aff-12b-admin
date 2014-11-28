using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BetListViewModel
/// </summary>
public class BetListViewModel
{
    public List<TicketText> TicketTextList { get; set; }

    public TicketResult TicketResult { get; set; }

    public byte[] TicketResultBytes { get; set; }

    public bool IsShowSystemIdAndExternalId { get; set; }

    public bool IsShowWinLoss { get; set; }

    public decimal ExchangeRate { get; set; }
}