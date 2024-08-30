var Products = (function ($) {
    function handleScroll() {
        $('html, body').stop().animate({
            scrollTop: $('html, body').scrollTop() - $('.header').outerHeight()
        }, 400);
    }
    function productsSlider() {
        let swiperSetting = {
            slidesPerView: 3,
            spaceBetween: 12,
            slidesPerGroup: 3,
            watchOverflow: true,
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
                    spaceBetween: 15,
                    slidesPerGroup: 2,
                    watchOverflow: true
                },
                768: {
                    slidesPerView: 2,
                    spaceBetween: 10,
                    slidesPerGroup: 2,
                    watchOverflow: true
                },
                1024: {
                    slidesPerView: 3,
                    spaceBetween: 12,
                    slidesPerGroup: 3,
                    watchOverflow: true
                },
            },
        };
        
        var manualSwiper = new Swiper(".manual-slider", swiperSetting);

        var autoSwiper = new Swiper(".auto-slider", Object.assign({
            loop: true,
            speed: 700,
            autoplay: {
                delay: 3500,
                disableOnInteraction: false
            }
        }, swiperSetting));
    }
    var _me = {
        initialize: function () {
            productsSlider();
            _me.initEvent();
            $(window).ready(handleScroll);
        },
        initEvent: function () {
        }
    };

    return _me;
})(jQuery);

Products.initialize();