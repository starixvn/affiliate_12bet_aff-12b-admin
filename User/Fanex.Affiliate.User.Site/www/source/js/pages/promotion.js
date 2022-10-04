var Promotion = (function ($) {
    let DEFAULT_ACTION_OF_PROMOTION = 'New';
    let allowedActionListOfPromotion = ["New", "Existing"];

    var _me = {
        initialize: function () {
            let hash = window.location.hash;
            let actionUrl = hash.split('#')[1];

            _me.initEvent();
            _me.renderView(actionUrl);
        },
        initEvent: function () {
            $('.btn-join-affiliate').on('click', function (event) {
                event.preventDefault();

                PageTracking.addTrackingForSignUpFromPromotion();

                setTimeout(function () {
                    Site.redirect($(this).attr('href'));
                }, 10);
            }),
            $('#promotion-menu').on('click', '.promotion-menu-item', function () {
                var name = $(this).data('name');
                history.pushState({
                    actionName: name
                }, null, `#${name}`);

                _me.renderView(name);
            })
        },
        activeMenu: function (actionName) {
            var activeClassName = "active";
            $(".promotion-menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },
        renderView: function (actionName) {
            if (allowedActionListOfPromotion.indexOf(actionName) === -1) {
                actionName = DEFAULT_ACTION_OF_PROMOTION;
            }

            _me.activeMenu(actionName);

            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("Promotion/" + actionName),
                success: function (response) {
                    $("#promotion-content").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },
    };

    return _me;
})(jQuery);

Promotion.initialize();