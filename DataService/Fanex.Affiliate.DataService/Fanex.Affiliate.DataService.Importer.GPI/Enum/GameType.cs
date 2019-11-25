using System.ComponentModel;

namespace Fanex.Affiliate.DataService.Importer.Gpi.Enum
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
