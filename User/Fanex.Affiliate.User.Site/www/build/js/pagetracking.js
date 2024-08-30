"use strict";

var PageTracking = (function ($) {
    var _defaultShortedLanguageCode = "EN";
    var _shortedLanguageCodes = {
        "bn": "BN",
        "en": "EN",
        "cs": "CS",
        "ch": "CH",
        "hi": "HIEN",
        "id": "ID",
        "kh": "KH",
        "ko": "KO",
        "jp": "JP",
        "th": "TH",
        "vn": "VN"
    };

    function _getShortedLangCode() {
        return $('#shortedLangCode').val() || _defaultShortedLanguageCode;
    }

    var _me = {
        addTrackingForLogin: function addTrackingForLogin() {
            if (window.gtag) {
                var trackingAction = 'AffNewSeasonMedia-' + _getShortedLangCode();
                window.gtag('event', trackingAction, {
                    'send_to': 'UA-5160706-44',
                    'event_category': 'AFFiliateLogin',
                    'event_label': 'AFFLogin'
                });
            }
        },
        addTrackingForHomeBanner: function addTrackingForHomeBanner() {
            if (window.gtag) {
                var trackingAction = 'AffHomeBanner-' + _getShortedLangCode();
                window.gtag('event', trackingAction, {
                    'send_to': 'UA-5160706-44',
                    'event_category': 'AFFiliatePage',
                    'event_label': 'AFFHomeBanner'
                });
            }
        },
        addTrackingForCommissionBanner: function addTrackingForCommissionBanner() {
            if (window.gtag) {
                var trackingAction = 'AffCommissionStructureBanner-' + _getShortedLangCode();
                window.gtag('event', trackingAction, {
                    'send_to': 'UA-5160706-44',
                    'event_category': 'AFFiliatePage',
                    'event_label': 'AFFCommissionStructureBanner'
                });
            }
        },

        addTrackingForPromotionBanner: function addTrackingForPromotionBanner() {
            //analytics.js
            //_gaq.push(['_trackEvent', eventCategory, eventAction, eventLabel, eventValue]);

            //gtag.js
            //gtag('event', <event_action>, {
            //  'event_category': <event_category>,
            //  'event_label': <event_label>,
            //  'value': <value>
            //});
            if (window.gtag) {
                var actionName = 'AffPromotionBanner' + _getShortedLangCode();
                window.gtag('event', actionName, {
                    'send_to': 'UA-5160706-44',
                    'event_category': 'AFFiliatePage',
                    'event_label': 'AffPromotionBanner'
                });
            }
        }
    };

    return _me;
})(jQuery);

