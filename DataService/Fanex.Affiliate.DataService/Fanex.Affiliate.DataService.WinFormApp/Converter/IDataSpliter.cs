using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.WinFormApp.Converter
{
    public interface IDataSpliter
    {
        IEnumerable<string> Split(string data);
    }
}
