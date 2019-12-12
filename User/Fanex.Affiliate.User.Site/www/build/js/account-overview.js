var AccountOverview = (function ($) {
    var _me = {
        initialize: function () {
            //_me.initEvent();
            //_me.renderView("Summary");
        },

        //initEvent: function () {
        //    $('#account-overview-menu').on('click', '.account-overview-menu', function () {
        //        var name = $(this).data('name');
        //        _me.renderView(name);
        //    });
        //},

        //activeMenu: function (actionName) {
        //    var activeClassName = "active";
        //    $(".account-overview-menu").removeClass(activeClassName);
        //    $("#menu" + actionName).addClass(activeClassName);
        //},

        //renderView: function (actionName) {
        //    _me.activeMenu(actionName);

        //    $.ajax({
        //        type: "POST",
        //        dataType: "html",
        //        cache: false,
        //        data: JSON.stringify(null),
        //        url: Site.resolveRelativeUrl("UserPanel/AccountOverView/" + actionName),
        //        contentType: "application/json; charset=UTF-8",
        //        success: function (response) {
        //            $("#account-overview-content").html(response);
        //        },
        //        beforeSend: function () {
        //            Site.showLoader();
        //        },
        //        complete: function () {
        //            Site.hideLoader();
        //        }
        //    });
        //},
    };

    return _me;
})(jQuery);

AccountOverview.initialize();