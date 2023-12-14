var Promotion = (function ($) {
    function topBanner() {
        var swiper = new Swiper(".top-banner", {
            loop: true,
            speed: 900,
            autoplay: {
                delay: 4500,
                disableOnInteraction: false,
            },
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            }
        });
    }
    var _me = {
        initialize: function () {
            topBanner();
            _me.initEvent();
        },
        initEvent: function () {
            $('.btn-join-affiliate').on('click', function (event) {
                event.preventDefault();
                PageTracking.addTrackingForSignUpFromPromotion();
                var redirectLink = $(this).attr('href');

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            });

            $('.block-content .button-toggle').addClass("open");
            $('.button-toggle').click(function () {
                $(this).toggleClass('open').parent().next().slideToggle(300);
            });
        }
    };

    return _me;
})(jQuery);

Promotion.initialize();