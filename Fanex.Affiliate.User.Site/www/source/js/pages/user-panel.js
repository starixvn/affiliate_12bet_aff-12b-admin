var UserPanel = (function ($) {
    var allowedControllerList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest",
        "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics"];

    function renderView() {
        var hash = window.location.hash;
        console.log(hash);
        var controller = hash.split('#')[1];

        if (!hash || !controller || allowedControllerList.indexOf(controller) === -1) {
            controller = "AccountOverView";
        }

        console.log(controller);

        var url = "/" + controller;

        $.ajax({
            type: "GET",
            dataType: "html",
            cache: false,
            url: url,
            success: function (response) {
                $("#mainPanel").html(response);
            }
        });
    }

    return {
        initialize: function () {
            renderView();
            UserPanel.initEvent();
        },

        initEvent: function () {
            $(window).on('hashchange', function () {
                renderView();
            });
        }
    };
})(jQuery);

UserPanel.initialize();