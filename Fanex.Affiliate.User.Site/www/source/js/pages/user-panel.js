var UserPanel = (function ($) {
    var allowedActionList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest",
        "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics"];

    var _me = {
        initialize: function () {
            _me.renderView();
            UserPanel.initEvent();
        },

        initEvent: function () {
            $(window).on('hashchange', function () {
                _me.renderView();
            });
        },

        activeMenu: function (actionName) {
            var activeClassName = "active";
            $("#mainMenu .menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderView: function () {
            var action = _me.getActionName();
            _me.activeMenu(action);

            var url = Site.resolveRelativeUrl("UserPanel/" + action);

            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: url,
                success: function (response) {
                    $("#mainPanel").html(response);
                },
                beforeSend: function () {
                    Site.showLoader();
                },
                complete: function () {
                    Site.hideLoader();
                }
            });
        },

        getActionName: function () {
            var hash = window.location.hash;
            var action = hash.split('#')[1];

            if (!hash || !action || allowedActionList.indexOf(action) === -1) {
                action = "AccountOverView";
            }

            return action;
        }
    };

    return _me;
})(jQuery);

UserPanel.initialize();