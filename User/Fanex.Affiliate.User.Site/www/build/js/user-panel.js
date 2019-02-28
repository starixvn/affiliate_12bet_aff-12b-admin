"use strict";

var UserPanel = (function ($) {
    var DEFAULT_ACTION = 'AccountOverView';
    var allowedActionList = ["AccountOverView", "StatementHistory", "MyAccount", "PaymentRequest", "MediaGallery", "TextBox", "TrackingStatistics", "ReferrersStatistics", "LandingPageStatistics", "DailyEarning", "DailyEarningDetail", "BetList", "MessageHistory", "CommissionReport"];

    var _ajaxRequest = null;

    var _me = {
        datepicker: [],
        initialize: function initialize() {
            _me.initializeCommissionNewsPopup();
            _me.renderView();
            UserPanel.initEvent();
        },

        initEvent: function initEvent() {
            $(window).on('hashchange', function (event) {
                event.stopPropagation();
                _me.renderView();
                if (_me.canShowCommissionNewsPopup()) {
                    $('#commissionNewsPopup').modal('show');
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
            var actionUrl = _me.getActionUrl(false);
            var actionName = actionUrl.split('?')[0];
            _me.activeMenu(actionName);

            var url = Site.resolveRelativeUrl("UserPanel/" + actionUrl);

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

        getActionUrl: function getActionUrl(onlyAction) {
            var hash = window.location.hash;
            var actionUrl = hash.split('#')[1];
            if (!actionUrl) {
                return DEFAULT_ACTION;
            }

            var urlPartList = actionUrl.split('?');
            var actionName = urlPartList[0];

            if (allowedActionList.indexOf(actionName) === -1) {
                actionName = DEFAULT_ACTION;
            }

            var queryString = !onlyAction && urlPartList[1] ? '?' + urlPartList[1] : '';

            return actionName + queryString;
        },

        canShowCommissionNewsPopup: function canShowCommissionNewsPopup() {
            var action = _me.getActionUrl(true);

            return action === DEFAULT_ACTION && $('#IsVisitedAccountOverviewPage').val() === 'False' || action === 'CommissionReport' && $('#IsVisitedCommissionReportPage').val() === 'False';
        },

        updateVisitedPage: function updateVisitedPage() {
            var action = _me.getActionUrl(true);
            if (action === DEFAULT_ACTION) {
                $('#IsVisitedAccountOverviewPage').val('True');
            } else {
                $('#IsVisitedCommissionReportPage').val('True');
            }
        },

        initializeCommissionNewsPopup: function initializeCommissionNewsPopup() {
            $('#commissionNewsPopup').modal({
                backdrop: 'static',
                show: false
            });

            if (_me.canShowCommissionNewsPopup()) {
                $('#commissionNewsPopup').modal('show');
            }
        }
    };

    return _me;
})(jQuery);

UserPanel.initialize();

