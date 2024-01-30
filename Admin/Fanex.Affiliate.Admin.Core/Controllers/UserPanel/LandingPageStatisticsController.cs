using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.CustomerMenu)]
    public class LandingPageStatisticsController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemServive;
        private readonly ILandingPageRepository _landingPageRepository;
        private readonly ILandingPageStatisticsRepository _landingPageStatisticsRepository;

        public LandingPageStatisticsController(
            ISessionService sessionService,
            ISystemService systemServive,
            ILandingPageRepository landingPageRepository,
            ILandingPageStatisticsRepository landingPageStatisticsRepository) : base(sessionService)
        {
            _systemServive = systemServive;
            _landingPageRepository = landingPageRepository;
            _landingPageStatisticsRepository = landingPageStatisticsRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetPageList()
        {
            return new JsonNetResult
            {
                Data = _landingPageRepository.GetLandingPageList(SessionService.UserId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageStatisticsList(LandingPageStatisticsParam param)
        {
            if (ModelState.IsValid)
            {
                if(!string.Equals("Country", param.GroupBy, StringComparison.InvariantCultureIgnoreCase))
                {
                    param.LanguageCode = GetShortedCode(_systemServive.GetAllInChangeMarkets(), param.LanguageCode);
                }

                return new JsonNetResult
                {
                    Data = _landingPageStatisticsRepository.GetLandingPageStatisticsList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageStatisticsDetailList(LandingPageStatisticsDetailParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _landingPageStatisticsRepository.GetLandingPageStatisticsDetailList(param)
                };
            }

            return new JsonNetResult();
        }

        private static string GetShortedCode(IEnumerable<Language> languageList, string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                return string.Join(",", languageList.Select(l => l.ShortedCode));
            }

            var shortedCode = languageList.FirstOrDefault(l => l.ShortedCode.MatchWith(languageCode))?.ShortedCode ?? LanguageSystem.ShortedDefaultCode;

            return shortedCode;
        }
    }
}
