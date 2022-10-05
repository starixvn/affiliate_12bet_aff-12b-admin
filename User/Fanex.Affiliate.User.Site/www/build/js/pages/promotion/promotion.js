"use strict";

var Promotion = (function ($) {
    var DEFAULT_ACTION_OF_PROMOTION = 'New';
    var allowedActionListOfPromotion = ["New", "Existing"];

    var _me = {
        initialize: function initialize() {
            var hash = window.location.hash;
            var actionUrl = hash.split('#')[1];

            _me.initEvent();
            _me.renderView(actionUrl);
        },
        initEvent: function initEvent() {
            $('#promotion-menu').on('click', '.promotion-menu-item', function () {
                var name = $(this).data('name');
                history.pushState({
                    actionName: name
                }, null, "#" + name);

                _me.renderView(name);
            });
        },
        activeMenu: function activeMenu(actionName) {
            var activeClassName = "active";
            $(".promotion-menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },
        renderView: function renderView(actionName) {
            if (allowedActionListOfPromotion.indexOf(actionName) === -1) {
                actionName = DEFAULT_ACTION_OF_PROMOTION;
            }

            _me.activeMenu(actionName);

            $.ajax({
                type: "POST",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("Promotion/" + actionName),
                success: function success(response) {
                    $("#promotion-content").html(response);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        }
    };

    return _me;
})(jQuery);

Promotion.initialize();

