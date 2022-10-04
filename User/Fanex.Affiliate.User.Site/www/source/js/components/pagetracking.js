var PageTracking = (function ($) {
    var _me = {
        addTrackingForMediaPage: function () {
            if (window.gtag) {
                var shortedLangCode = $('#shortedLangCode').val();
                var trackingAction = 'AffNewSeasonMedia-' + shortedLangCode;
                window.gtag('event', trackingAction, {
                    'event_category': 'AFFiliatePage',
                    'event_label': 'AFFBigBanner'
                });
            }
        },
        addTrackingForSignUpPage: function () {
            if (window.gtag) {
                var shortedLangCode = $('#shortedLangCode').val();
                var actionName = 'AffJoinJuly40Com-' + shortedLangCode;
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
                var shortedLangCode = $('#shortedLangCode').val();
                var actionName = 'PreAFFSignUpNewPage-' + shortedLangCode;
                window.gtag('event', actionName, {
                    'event_category': 'LandingPage',
                    'event_label': 'NavigationButton'
                });
            }
        }
    }

    return _me;
})(jQuery)