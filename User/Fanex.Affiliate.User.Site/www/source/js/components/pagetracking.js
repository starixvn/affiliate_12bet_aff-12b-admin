var PageTracking = (function ($) {
    const _defaultShortedLanguageCode = "EN"
    const _shortedLanguageCodes = {
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

    function _getShortedLangCodeUpperCase() {
        var shortedLangCode = $('#shortedLangCode').val();

        return _shortedLanguageCodes[shortedLangCode] || _defaultShortedLanguageCode;
    }

    var _me = {
        addTrackingForMediaPage: function () {
            if (window.gtag) {
                var trackingAction = 'AffNewSeasonMedia-' + _getShortedLangCode();
                window.gtag('event', trackingAction, {
                    'event_category': 'AFFiliatePage',
                    'event_label': 'AFFBigBanner'
                });
            }
        },
        addTrackingForSignUpPage: function () {
            if (window.gtag) {
                var actionName = 'AffJoinJuly40Com-' + _getShortedLangCode();
                window.gtag('event', actionName, {
                    'event_category': 'AFFiliatePage',
                    'event_label': 'AFFBigBanner'
                });
            }
        },
        addTrackingForSignUpFromPromotion: function () {
            //analytics.js
            //_gaq.push(['_trackEvent', eventCategory, eventAction, eventLabel, eventValue]);

            //gtag.js
            //gtag('event', <event_action>, {
            //  'event_category': <event_category>,
            //  'event_label': <event_label>,
            //  'value': <value>
            //});
            if (window.gtag) {
                var actionName = 'PreAFFSignUpNewPage' + _getShortedLangCode();
                window.gtag('event', actionName, {
                    'event_category': 'AFFiliatePage',
                    'event_label': 'NavigationButton'
                });
            }
        },
        addTrackingForHomeBanAFFFlat50: function () {
            if (window.gtag) {
                var trackingAction = 'HomeBanAFFFlat50-' + _getShortedLangCodeUpperCase();
                window.gtag('event', trackingAction, {
                    'event_category': 'HomePage',
                    'event_label': 'Banner'
                });
            }
        },
        addTrackingForSmartBanAFFFlat50: function () {
            if (window.gtag) {
                var trackingAction = 'SmartBanAFFFlat50-' + _getShortedLangCodeUpperCase();
                window.gtag('event', trackingAction, {
                    'event_category': 'HomePage',
                    'event_label': 'Banner'
                });
            }
        }
    }

    return _me;
})(jQuery)