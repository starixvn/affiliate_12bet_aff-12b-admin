var Home = (function ($) {
    var alertHomeBlockRemoveHeight = 15;

    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.handleShowAlert();
        },
        initEvent: function () {
            $('#euroBanner').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForSignUpPage();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            })

            $('#sportBanner').on('click', function (event) {
                event.preventDefault();
                var redirectLink = $(this).attr('href');
                PageTracking.addTrackingForSignUpPage();

                setTimeout(function () {
                    Site.redirect(redirectLink);
                }, 10);
            })

            $('#casinoBanner').on('click', function (event) {
                event.preventDefault();
                var $this = $(this);
                var isLoggedIn = !$this.hasClass('mediaLoginLink');

                if (isLoggedIn) {
                    PageTracking.addTrackingForMediaPage();

                    setTimeout(function () {
                        Site.redirect($this.attr('href'));
                    }, 10);
                }
            })

            $('#alertHomeBlockRemove').on('click', function (event) {
                event.preventDefault();
                $("#alertHome").hide();
                $(document.body).css('overflow', 'auto');
                window.removeEventListener("resize", _me.adjustAlertHomeBlockRemovePosition);
            })
        },
        handleShowAlert: function () {
            if ($("#alertHome").css("display") === "block") {
                $(document.body).css('overflow', 'hidden');
                _me.adjustAlertHomeBlockRemovePosition();
                window.addEventListener("resize", _me.adjustAlertHomeBlockRemovePosition);
            }
        },
        adjustAlertHomeBlockRemovePosition: function () {
            var alertHomeBlockHeight = $('#alertHomeBlock').outerHeight();
            var alertHomeBlockWidth = $('#alertHomeBlock').outerWidth();
            $('#alertHomeBlock').css({
                top: `calc(50% - ${alertHomeBlockHeight / 2}px)`,
                right: `calc(50% - ${alertHomeBlockWidth / 2}px)`
            });

            var alertHomeBlockOffset = $('#alertHomeBlock').position();
            var alertHomeBlockTop = alertHomeBlockOffset.top;
            var alertHomeBlockLeft = alertHomeBlockOffset.left;

            $('#alertHomeBlockRemove').css({
                top: (alertHomeBlockTop - alertHomeBlockRemoveHeight) + 'px',
                right: `calc(100% - ${alertHomeBlockLeft + alertHomeBlockWidth + alertHomeBlockRemoveHeight}px)`
            });
        }
    };

    return _me;
})(jQuery);

Home.initialize();