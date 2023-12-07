var Products = (function ($) {
    function productsSlider() {
        var swiper = new Swiper(".products-slider", {
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
                    spaceBetween: 5,
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
        });
    }
    var _me = {
        initialize: function () {
            productsSlider();
            _me.initEvent();
        },
        initEvent: function () {
            
        }
    };

    return _me;
})(jQuery);

Products.initialize();