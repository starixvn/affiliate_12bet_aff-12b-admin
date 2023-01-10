"use strict";

var Promotion = (function ($) {
    var DEFAULT_ACTION_OF_PROMOTION = 'New';
    var allowedActionListOfPromotion = ["New", "Existing"];
    var alertPromotionBlockRemoveHeight = 15;

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
                _me.handleShowAlert();
            });

            $('#alertPromotionBlockRemove').on('click', function (event) {
                event.preventDefault();
                $("#alertPromotion").hide();
                $(document.body).css('overflow', 'auto');
                window.removeEventListener("resize", _me.adjustAlertPromotionBlockRemovePosition);
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
        },
        handleShowAlert: function handleShowAlert() {
            $("#alertPromotion").show();
            $(document.body).css('overflow', 'hidden');
            _me.adjustAlertPromotionBlockRemovePosition();
            window.addEventListener("resize", _me.adjustAlertPromotionBlockRemovePosition);
        },

        adjustAlertPromotionBlockRemovePosition: function adjustAlertPromotionBlockRemovePosition() {
            var alertPromotionBlockHeight = $('#alertPromotionBlock').outerHeight();
            var alertPromotionBlockWidth = $('#alertPromotionBlock').outerWidth();
            $('#alertPromotionBlock').css({
                top: "calc(50% - " + alertPromotionBlockHeight / 2 + "px)",
                right: "calc(50% - " + alertPromotionBlockWidth / 2 + "px)"
            });

            var alertPromotionBlockOffset = $('#alertPromotionBlock').position();
            var alertPromotionBlockTop = alertPromotionBlockOffset.top;
            var alertPromotionBlockLeft = alertPromotionBlockOffset.left;

            $('#alertPromotionBlockRemove').css({
                top: alertPromotionBlockTop - alertPromotionBlockRemoveHeight + 'px',
                right: "calc(100% - " + (alertPromotionBlockLeft + alertPromotionBlockWidth + alertPromotionBlockRemoveHeight) + "px)"
            });
        }
    };

    return _me;
})(jQuery);

Promotion.initialize();

