var Banner = (function () {
    var INTERVAL_SLIDE = 5000;
    var sliderTimeout = null;
    var slideIndex = 1;

    function showSlides(n) {
        var i;
        var slides = document.getElementsByClassName("mySlides");
        if (n > slides.length) {
            slideIndex = 1;
        }
        else if (n < 1) {
            slideIndex = slides.length;
        }
        else {
            slideIndex = n;
        }

        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }
        slides[slideIndex - 1].style.display = "block";

        clearTimeout(sliderTimeout);
        sliderTimeout = setTimeout(function () { showSlides(slideIndex + 1); }, INTERVAL_SLIDE);
    }

    var _me = {
        initialize: function(){
            showSlides(slideIndex);
            _me.initEvent();
        },
        initEvent: function(){
            var bannerMedia = document.getElementById("bannerMedia");
            bannerMedia.addEventListener("click", function () {
                document.getElementById("isCLickedBanner").value = 1;
                window.alert(resource.LoginNotification);
            });
        },
        handleMouseOver: function () {
            clearTimeout(sliderTimeout);
        },
        handleMouseOut: function () {
            sliderTimeout = setTimeout(function () { showSlides(slideIndex + 1); }, INTERVAL_SLIDE);
        },
        // Next/previous controls
        plusSlides: function (n) {
            showSlides(slideIndex += n);
        }
    };

    return _me;
})();

Banner.initialize();