using Fanex.Affiliate.DataService.Importer.BigGaming;
using Fanex.Affiliate.DataService.Importer.BTi;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Jasper;
using Fanex.Affiliate.DataService.Importer.Playtech;
using Fanex.Affiliate.DataService.Importer.Skywind;
using Fanex.Affiliate.DataService.Importer.Sunstone;
using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.ThirdPartyProduct.Constants
{
    internal static class SeviceSystem
    {
        internal static IDictionary<int, Lazy<IImporterService>> ServiceList
        {
            get
            {
                return new Dictionary<int, Lazy<IImporterService>>
                {
                    { Products.BTiId, new Lazy<IImporterService>(() => new BTiService()) },
                    { Products.SkywindId, new Lazy<IImporterService>(() => new SkywindService()) },
                    { Products.SunstoneId, new Lazy<IImporterService>(() => new SunstoneService()) },
                    { Products.JasperId, new Lazy<IImporterService>(() => new JasperService()) },
                    { Products.PlaytechId, new Lazy<IImporterService>(() => new PlaytechService()) },
                    { Products.BgFishId, new Lazy<IImporterService>(() => new BgFishService()) },
                    { Products.BgLiveCasinoId, new Lazy<IImporterService>(() => new BgLiveCasinoService()) },
                };
            }
        }
    }
}
