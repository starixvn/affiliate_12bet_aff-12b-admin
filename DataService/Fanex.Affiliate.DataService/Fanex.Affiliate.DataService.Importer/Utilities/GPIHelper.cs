using Fanex.Affiliate.DataService.Importer.Enum;
using Fanex.Affiliate.DataService.Importer.Enum.GPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class GpiHelper
    {

        public static string GetTransactionStatusName(TransactionStatus status)
        {
            string result = status.ToString();
            return result;
        }

        public static string GetGameTypeName(GameType gameType)
        {
            return gameType.ToString().ToLower();
        }

        public static GameType ConvertStringToGameType(string game)
        {
            var lowerCaseGameName = game.ToLower();
            var gpiGameTypes = System.Enum.GetNames(typeof(GameType));
            var result = GameType.Keno;
            foreach (var gameType in gpiGameTypes)
            {
                if (lowerCaseGameName.Equals(gameType.ToLower()))
                    return System.Enum.TryParse(gameType)
            }
        }
    }
}
