namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Enum;
    using Extensions;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Statistics;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult TrackingStatistics()
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingCodeList = _statisticsRepository.GetTrackingCode(affId, isInternal).Where(c => !c.MatchWith("[None]")),
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatistics(affId, null, isInternal),
                IsDetail = false
            };

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTrackingStatistics(string trackingCode)
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatistics(affId, trackingCode, isInternal),
                IsDetail = false
            };

            return PartialView("~/Views/UserPanel/Statistics/_TrackingStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam)
        {
            trackingStatisticsDetailParam.AffId = SessionService.UserSession.AffId;
            trackingStatisticsDetailParam.IsInternal = SessionService.IsInternal;

            var viewModel = new TrackingStatisticsViewModel
            {
                TrackingStatisticsList = _statisticsRepository.GetTrackingStatisticsDetail(trackingStatisticsDetailParam),
                IsDetail = true
            };

            return PartialView("~/Views/UserPanel/Statistics/_TrackingStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult ReferrersStatistics()
        {
            var now = DateTime.Now;
            var referrerStatisticsParam = new ReferrerStatisticsParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now,
                DeviceType = (byte)DeviceType.PCDevice
            };

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam),
                FromDate = referrerStatisticsParam.FromDate,
                ToDate = referrerStatisticsParam.ToDate
            };

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam)
        {
            referrerStatisticsParam.AffId = SessionService.UserSession.AffId;
            referrerStatisticsParam.IsInternal = SessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetReferrerStatistics(referrerStatisticsParam)
            };

            return PartialView("~/Views/UserPanel/Statistics/_ReferrersStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam)
        {
            statisticsDetailParam.AffId = SessionService.UserSession.AffId;
            statisticsDetailParam.IsInternal = SessionService.IsInternal;

            var viewModel = new ReferrerStatisticsViewModel
            {
                DataList = _statisticsRepository.GetStatisticsDetailBySite(statisticsDetailParam),
                IsDetail = true,
                RowNumber = statisticsDetailParam.NRows
            };

            return PartialView("~/Views/UserPanel/Statistics/_ReferrersStatisticsContent.cshtml", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult LandingPageStatistics()
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var userHasCasino = _accountRepository.UserHasCasino(userSession.UserName, isInternal);
            var memberSitePage = new MemberSitePage(userHasCasino);

            var now = DateTime.Now;
            var landingPageParam = new LandingPageStatisticsParam
            {
                AffId = userSession.AffId,
                IsInternal = isInternal,
                FromDate = new DateTime(now.Year, now.Month, 1),
                ToDate = now
            };

            var viewModel = new LandingPageStatisticsViewModel
            {
                LanguageList = GetLanguageList(),
                MemberSitePageList = memberSitePage.PageList,
                FromDate = landingPageParam.FromDate,
                ToDate = landingPageParam.ToDate,
                DataList = GetLandingPageStatisticsData(landingPageParam),
                IsDetail = false
            };

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetLandingPageStatistics(LandingPageStatisticsParam landingPageParam)
        {
            landingPageParam.AffId = SessionService.UserSession.AffId;
            landingPageParam.IsInternal = SessionService.IsInternal;

            var selectedLanguage = landingPageParam.LanguageCode;
            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                landingPageParam.LanguageCode = SystemLanguage.LanguageList
                                                .FirstOrDefault(l => l.Code.Equals(selectedLanguage))
                                                ?.ShortedCode ?? SystemLanguage.ShortedDefaultCode;
            }

            var viewModel = new LandingPageStatisticsViewModel
            {
                DataList = GetLandingPageStatisticsData(landingPageParam),
                IsDetail = false
            };

            return PartialView("~/Views/UserPanel/Statistics/_LandingPageStatisticsContent.cshtml", viewModel);
        }

        private IEnumerable<SelectListItem> GetLanguageList()
        {
            var languageList = _marketingService.GetBannerTypeList(SessionService.IsInternal)
                                    .Where(t => t.Type.MatchWith(BannerTypeName.Language));

            var dataList = new List<SelectListItem>();
            foreach (var item in languageList)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(item.Value));
                if (language != null)
                {
                    dataList.Add(new SelectListItem
                    {
                        Value = language.Code,
                        Text = SystemLanguage.GetDisplayText(item.Value),
                        Selected = language.Code.MatchWith(SessionService.UserSession.PreferredLanguage)
                    });
                }
            }

            return dataList;
        }

        private IEnumerable<LandingPageStatistics> GetLandingPageStatisticsData(LandingPageStatisticsParam landingPageParam)
        {
            var dataList = _statisticsRepository.GetLandingPageStatistics(landingPageParam);

            foreach (var item in dataList)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(p => p.ShortedCode == item.LanguageCode);

                item.LanguageName = language == null ? item.LanguageCode : language.NativeName;
            }

            return dataList;
        }
    }
}