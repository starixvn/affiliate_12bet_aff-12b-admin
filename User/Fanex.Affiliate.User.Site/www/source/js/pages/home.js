var Home = (function ($) {
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
    function partnershipBanner() {
        var swiper = new Swiper(".partnership-banner", {
            slidesPerView: 3,
            spaceBetween: 100,
            centeredSlides: true,
            loop: true,
            speed: 300,
            autoplay: {
                delay: 3500,
                disableOnInteraction: false,
            },
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            },
            breakpoints: {
                640: {
                    slidesPerView: 3,
                    spaceBetween: 11
                },
                768: {
                    slidesPerView: 3,
                    spaceBetween: 100,
                },
                1024: {
                    slidesPerView: 3,
                    spaceBetween: 100,
                },
            },
        });
    }

    function miniBlockBanner() {
        var swiper = new Swiper(".mini-block-banner", {
            slidesPerView: 3,
            spaceBetween: 75,
            slidesPerGroup: 3,
            loop: true,
            speed: 700,
            autoplay: {
                delay: 3500,
                disableOnInteraction: false,
            },
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            },
            breakpoints: {
                640: {
                    slidesPerView: 2,
                    spaceBetween: 12,
                    slidesPerGroup: 2,
                },
                768: {
                    slidesPerView: 3,
                    spaceBetween: 35,
                },
                1024: {
                    slidesPerView: 3,
                    spaceBetween: 75,
                },
            },
        });
    }

    var _me = {
        initialize: function () {
/*            topBanner();*/
            partnershipBanner();
            miniBlockBanner();
            _me.initEvent();
        },
        initEvent: function () {
            $('.home-banner-tracking').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForHomeBanner();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            })

            $('.group-faq:not(:first-of-type) .anwser').css("display", "none");
            $('.group-faq:first-of-type .button-toggle').addClass("open");
            $('.button-toggle').click(function () {
                $('.open').not(this).removeClass('open').parent().next().slideUp(300);
                $(this).toggleClass('open').parent().next().slideToggle(300);
            });
        }
    };

    return _me;
})(jQuery);
Home.initialize();