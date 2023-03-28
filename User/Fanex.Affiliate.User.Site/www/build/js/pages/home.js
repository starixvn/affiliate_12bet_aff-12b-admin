'use strict';

var Home = (function ($) {

    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $('.home-banner-tracking').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForSignUpPage();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            });

            $('.media-link-tracking').on('click', function (event) {
                event.preventDefault();
                var $this = $(this);
                var isLoggedIn = !$this.hasClass('mediaLoginLink');

                if (isLoggedIn) {
                    PageTracking.addTrackingForMediaPage();

                    setTimeout(function () {
                        Site.redirect($this.attr('href'));
                    }, 10);
                }
            });

            $('.home-banner-tracking-flat').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForHomeBanAFFFlat50();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            });

            $('.home-banner-tracking-smart').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForSmartBanAFFFlat50();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            });
        }
    };

    return _me;
})(jQuery);

Home.initialize();

