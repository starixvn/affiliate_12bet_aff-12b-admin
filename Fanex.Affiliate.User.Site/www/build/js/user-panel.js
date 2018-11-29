"use strict";

var UserPanel = (function ($) {
    var allowedActionList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest", "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics", "DailyEarning", "DailyEarningDetail", "BetList", "MessageHistory", "CommissionReport"];

    var _ajaxRequest = null;

    var _me = {
        datepicker: [],
        initialize: function initialize() {
            _me.renderView();
            UserPanel.initEvent();
            $('#commissionNewsPopup').modal({
                backdrop: 'static',
                show: false
            });
            _me.getCommissionNewsPopup();
        },

        initEvent: function initEvent() {
            $(window).on('hashchange', function (event) {
                event.stopPropagation();
                _me.renderView();
                if (_me.canShowCommissionNewsPopup()) {
                    $('#commissionNewsPopup').modal('show');
                }
            });

            $("#mainMenu").on("click", ".menu-item", function (event) {
                event.stopPropagation();
                var currentPathWithHash = window.location.pathname + window.location.hash;
                var hrefMenu = $(this).attr("href");
                if (currentPathWithHash === hrefMenu) {
                    _me.renderView();
                }
            });

            $('#commissionNewsPopup').on('hidden.bs.modal', function (e) {
                _me.updateVisitedPage();
            });
        },

        activeMenu: function activeMenu(actionName) {
            var activeClassName = "active";
            $("#mainMenu .menu-item").removeClass(activeClassName);
            $("#menu" + actionName).addClass(activeClassName);
        },

        renderView: function renderView() {
            _me.clearEventListener();
            $("#mainPanel").html("");
            var action = _me.getActionName();
            _me.activeMenu(action);

            var queryString = _me.getQueryString();

            var url = Site.resolveRelativeUrl("UserPanel/" + action + queryString);

            if (_ajaxRequest !== null) {
                _ajaxRequest.abort();
            }

            _ajaxRequest = $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: url,
                success: function success(response) {
                    $("#mainPanel").html(response);
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },

        clearEventListener: function clearEventListener() {
            $("#mainPanel *").off();
            if (_me.datepicker) {
                for (var i = 0; i < _me.datepicker.length; i++) {
                    if (_me.datepicker[i]) {
                        _me.datepicker[i].destroy();
                    }
                }
                _me.datepicker = [];
            }
        },

        getActionName: function getActionName() {
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

        getQueryString: function getQueryString() {
            var url = window.location.href;
            var hasQueryString = url.indexOf("?") !== -1;

            if (hasQueryString) {
                return "?" + url.split('?')[1];
            }

            return "";
        },

        getCommissionNewsPopup: function getCommissionNewsPopup() {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: Site.resolveRelativeUrl("UserPanel/Main/CommissionNewsPopup"),
                success: function success(response) {
                    $('#commissionNewsPopup').html(response);
                    if (_me.canShowCommissionNewsPopup()) {
                        $('#commissionNewsPopup').modal('show');
                    }
                }
            });
        },

        canShowCommissionNewsPopup: function canShowCommissionNewsPopup() {
            var action = _me.getActionName();

            return action === 'AccountOverView' && $('#IsVisitedAccountOverviewPage').val() === 'False' || action === 'CommissionReport' && $('#IsVisitedCommissionReportPage').val() === 'False';
        },

        updateVisitedPage: function updateVisitedPage() {
            var action = _me.getActionName();
            if (action === 'AccountOverView') {
                $('#IsVisitedAccountOverviewPage').val('True');
            } else {
                $('#IsVisitedCommissionReportPage').val('True');
            }
        }
    };

    return _me;
})(jQuery);

UserPanel.initialize();

