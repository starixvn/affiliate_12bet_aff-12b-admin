using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.WinFormApp.Converter
{
    public class WebApiSpliter : IDataSpliter
    {
        public IEnumerable<string> Split(string data)
        {
            var rawDatas = data.Split(new[] { "\r\n-----------------------------------------\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return rawDatas;
        }
    }
}
