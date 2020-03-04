var AccountOverview = (function ($) {
    var _me = {
        initialize: function () {
            _me.initEvent();
            _me.renderView("Summary");
        },

        initEvent: function () {
            $('#account-overview-menu').on('click', '.account-overview-menu-item', function () {
                var name = $(this).data('name');
                _me.renderView(name);
            });
        },

        activeMenu: function (actionName) {
            var activeClassName = "active";
            $(".account-overview-menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderView: function (actionName) {
            _me.activeMenu(actionName);
            window.removeEventListener("scroll", handleScroll);
            window.removeEventListener("resize", handleResize);

            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/" + actionName),
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