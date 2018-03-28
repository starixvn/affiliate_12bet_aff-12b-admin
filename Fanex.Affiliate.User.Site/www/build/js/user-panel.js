"use strict";

var UserPanel = (function ($) {
    var allowedActionList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest", "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics"];

    function renderView() {
        var hash = window.location.hash;
        var action = hash.split('#')[1];

        if (!hash || !action || allowedActionList.indexOf(action) === -1) {
            action = "AccountOverView";
        }

        var url = "/UserPanel/" + action;

        $.ajax({
            type: "GET",
            dataType: "html",
            cache: false,
            url: url,
            success: function success(response) {
                $("#mainPanel").html(response);
            }
        });
    }

    return {
        initialize: function initialize() {
            renderView();
            UserPanel.initEvent();
        },

        initEvent: function initEvent() {
            $(window).on('hashchange', function () {
                renderView();
            });

            $("#mainMenu").on("click", ".menu-item", function () {
                var activeClassName = "active";
                $(".menu-item").removeClass(activeClassName);
                $(this).addClass(activeClassName);
            });
        }
    };
})(jQuery);

UserPanel.initialize();

