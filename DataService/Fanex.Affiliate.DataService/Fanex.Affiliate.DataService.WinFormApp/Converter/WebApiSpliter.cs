using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.WinFormApp.Converter
{
    public class WebApiSpliter : IDataSpliter
    {
        public IEnumerable<string> Split(string data)
        {
            var rawDatas = data.Split(new[] { "-----------------------------------------" }, StringSplitOptions.RemoveEmptyEntries);
            return rawDatas;
        }
    }
}
