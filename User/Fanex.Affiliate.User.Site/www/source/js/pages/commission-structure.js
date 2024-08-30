var CommissionStructure = (function ($) {
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
/*            topBanner();*/
            _me.initEvent();
        },
        initEvent: function () {
            $('.commission-structure-banner-tracking').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForHomeBanner();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            });

            $('.group-faq:first-of-type .button-toggle').addClass("open");
            $('.block-content .button-toggle').addClass("open");
            $('.button-toggle').click(function () {
                $(this).toggleClass('open').parent().next().slideToggle(300);
            });

        }
    };

    return _me;
})(jQuery);

CommissionStructure.initialize();