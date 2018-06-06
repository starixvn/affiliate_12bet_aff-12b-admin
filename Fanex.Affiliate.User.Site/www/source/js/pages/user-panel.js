var UserPanel = (function ($) {
    var allowedActionList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest",
        "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics",
        "DailyEarning", "DailyEarningDetail", "BetList"];

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

            var queryString = _me.getQueryString();

            var url = Site.resolveRelativeUrl("UserPanel/" + action + queryString);

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

            var queryStringIndexSign = action.indexOf('?');
            if (queryStringIndexSign !== -1) {
                action = action.substring(0, queryStringIndexSign);
            }

            if (!hash || !action || allowedActionList.indexOf(action) === -1) {
                action = "AccountOverView";
            }

            return action;
        },

        getQueryString: function () {
            var url = window.location.href;
            var hasQueryString = url.indexOf("?") !== -1;

            if (hasQueryString) {
                return "?" + url.split('?')[1];
            }

            return "";
        }
    };

    return _me;
})(jQuery);

UserPanel.initialize();