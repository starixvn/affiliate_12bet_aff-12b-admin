using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Enum.GPI
{
    public enum GameType
    {
        [Display(Name ="keno")]
        Keno,
        Pk10,
        Ladder,
        RockPaperScissors,
        ThaiLottery,
        Fast3,
        Thor,
        Sode
    }
}
