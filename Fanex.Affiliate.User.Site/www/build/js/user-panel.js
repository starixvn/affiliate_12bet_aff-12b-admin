"use strict";

var UserPanel = (function ($) {
    var allowedActionList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest", "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics"];

    var _me = {
        initialize: function initialize() {
            _me.renderView();
            UserPanel.initEvent();
        },

        initEvent: function initEvent() {
            $(window).on('hashchange', function () {
                _me.renderView();
            });
        },

        activeMenu: function activeMenu(actionName) {
            var activeClassName = "active";
            $("#mainMenu .menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderView: function renderView() {
            var action = _me.getActionName();
            _me.activeMenu(action);

            var url = Site.resolveRelativeUrl("/UserPanel/" + action);

            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: url,
                success: function success(response) {
                    $("#mainPanel").html(response);
                }
            });
        },

        getActionName: function getActionName() {
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

