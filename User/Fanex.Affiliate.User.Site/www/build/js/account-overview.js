var AccountOverview = (function ($) {
    var _ajaxRequest = null;

    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.renderView("Summary");
        },

        initEvent: function () {
            $('#account-overview-menu').on('click', '.account-overview-menu', function () {
                var name = $(this).data('name');
                _me.renderView(name);
            });
        },

        activeMenu: function (actionName) {
            var activeClassName = "active";
            $(".account-overview-menu").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderView: function (actionName) {
            _me.activeMenu(actionName);

            if (_ajaxRequest !== null) {
                _ajaxRequest.abort();
            }

            _ajaxRequest = $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/AccountOverView/" + actionName),
                success: function (response) {
                    $("#account-overview-content").html(response);
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

AccountOverview.initialize();