var Banner = (function () {
    var sliderTimeout = null;
    var slideIndex = 1;
    showSlides(slideIndex);

    function showSlides(n) {
        var i;
        var slides = document.getElementsByClassName("mySlides");
        if (n > slides.length) {
            slideIndex = 1;
        }
        else if (n < 1) {
            slideIndex = slides.length
        }
        else {
            slideIndex = n;
        }

        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }
        slides[slideIndex - 1].style.display = "block";

        clearTimeout(sliderTimeout);
        sliderTimeout = setTimeout(function () { showSlides(slideIndex + 1); }, 7000);
    }

    var _me = {
        handleMouseOver: function () {
            clearTimeout(sliderTimeout);
        },
        handleMouseOut: function () {
            sliderTimeout = setTimeout(function () { showSlides(slideIndex + 1); }, 7000);
        },
        // Next/previous controls
        plusSlides: function (n) {
            showSlides(slideIndex += n);
        }
    }

    return _me;
})()