'use strict';

var NewPromotion = (function ($) {
    var _me = {
        initialize: function initialize() {
            _me.initEvent();
        },
        initEvent: function initEvent() {
            $('.btn-join-affiliate').on('click', function (event) {
                event.preventDefault();
                PageTracking.addTrackingForSignUpFromPromotion();
                var redirectLink = $(this).attr('href');

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            });
        }
    };

    return _me;
})(jQuery);

NewPromotion.initialize();

