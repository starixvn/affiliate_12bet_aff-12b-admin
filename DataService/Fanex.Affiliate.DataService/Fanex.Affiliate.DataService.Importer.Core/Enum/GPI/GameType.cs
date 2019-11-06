using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Enum.GPI
{
    public enum GameType
    {
        [Description("keno")]
        Keno,
        [Description("pk10")]
        Pk10,
        [Description("ladder")]
        Ladder,
        [Description("rockpaperscissors")]
        RockPaperScissors,
        [Description("thailottery")]
        ThaiLottery,
        [Description("fast3")]
        Fast3,
        [Description("thor")]
        Thor,
        [Description("sode")]
        Sode
    }
}
