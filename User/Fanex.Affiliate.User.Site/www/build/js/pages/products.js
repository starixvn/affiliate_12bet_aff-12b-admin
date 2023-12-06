"use strict";

var Products = (function ($) {
    function productsSlider() {
        var swiper = new Swiper(".products-slider", {
            slidesPerView: 3,
            spaceBetween: 12,
            loop: true,
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev"
            },
            pagination: {
                el: ".swiper-pagination",
                clickable: true
            },
            breakpoints: {
                640: {
                    slidesPerView: 2,
                    spaceBetween: 5
                },
                768: {
                    slidesPerView: 2,
                    spaceBetween: 5
                },
                1024: {
                    slidesPerView: 3,
                    spaceBetween: 12
                }
            }
        });
    }
    var _me = {
        initialize: function initialize() {
            productsSlider();
            _me.initEvent();
        },
        initEvent: function initEvent() {}
    };

    return _me;
})(jQuery);

Products.initialize();

