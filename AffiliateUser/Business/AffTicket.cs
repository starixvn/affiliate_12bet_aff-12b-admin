using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AffTicket
/// </summary>
public class AffTicket : Fanex.BetList.Core.Entities.Ticket
{
    public string ExternalId { get; set; }

    public decimal LicenseeWl { get; set; }

    public int CurrencyId { get; set; }
}