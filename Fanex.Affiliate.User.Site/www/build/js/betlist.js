"use strict";

var BetList = (function () {
    var BETTYPE_CASHOUT = 911;
    var BETTYPE_BOUGHT = 912;

    function isMixParlay(bettype) {
        return bettype === 9 || bettype === 29;
    }

    var _me = {
        initialize: function initialize() {
            $("#betlistDialog").modal({ backdrop: "static", show: false });

            window.ViewResult = _me.ViewResult;
            window.showCombMPDetail = _me.showCombMPDetail;
            window.showNewCombMP = _me.showNewCombMP;
            window.ViewCashOutHistory = _me.ViewCashOutHistory;
            window.showMP = _me.showMP;
            window.showCombMP = _me.showCombMP;
        },

        getMixParlayResultUrl: function getMixParlayResultUrl(refno, winlostdate, bettype, username) {
            return {
                refno: refno,
                winlossdate: winlostdate,
                bettype: bettype,
                matchid: 1,
                username: username
            };
        },

        getSportBookResultUrl: function getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betidOrHdp1, hdp2) {
            var params = {
                matchid: matchid,
                bettype: bettype,
                raceno: raceNo,
                sporttype: sporttype,
                isoutright: isOutright,
                username: username,
                betid: betidOrHdp1
            };

            if (hdp2) {
                params.handicap2 = hdp2;
            }

            return params;
        },

        ViewResult: function ViewResult(matchid, raceNo, bettype, sporttype, refno, username, winlostdate, refnoMixParlay, league, isOutright, betidOrHdp1, hdp2) {
            var url = Site.resolveRelativeUrl("UserPanel/BetList/Result");
            var params = isMixParlay(bettype) ? _me.getMixParlayResultUrl(refnoMixParlay, winlostdate, bettype, username) : _me.getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betidOrHdp1, hdp2);

            _me.showPopup(url, params, resourceLang.result);
        },

        showPopup: function showPopup(url, params, title, onSuccess) {
            $.ajax({
                type: "POST",
                dataType: "html",
                contentType: "application/json; charset=UTF-8",
                cache: false,
                url: url,
                data: JSON.stringify(params),
                success: function success(response) {
                    $("#betListDisalogTitle").html(title || "");
                    $("#betlistDialogContent").html(response);
                    $("#betlistDialog").modal("show");

                    onSuccess && onSuccess();
                },
                beforeSend: function beforeSend() {
                    Site.showLoader();
                    $("#betlistDialogContent").html("");
                },
                complete: function complete() {
                    Site.hideLoader();
                }
            });
        },

        showMP: function showMP(transId) {
            var $divEvent = $('#divEvent_' + transId);
            $divEvent.addClass("mx-parlay"); //to make line-through for void-parlay-ticket

            _me.showOldMixParlayDetail(transId, $divEvent);
        },

        showCombMP: function showCombMP(transId, index) {
            var $divEvent = $('#divEvent_' + transId + "" + index);
            $divEvent.addClass("mx-parlay"); //to make line-through for void-parlay-ticket

            _me.showOldMixParlayDetail(transId, $divEvent);
        },

        showCombMPDetail: function showCombMPDetail(transId, refNo, betId, custId, betTeam, baseCurrency) {
            if (transId === 0) {
                return;
            }

            var url = Site.resolveRelativeUrl("UserPanel/BetList/ParlayDetail");
            var params = {
                transId: transId,
                refno: refNo,
                betid: betId,
                custid: custId,
                betteam: betTeam,
                basecurrency: baseCurrency
            };

            _me.showPopup(url, params, betTeam);
        },

        showNewCombMP: function showNewCombMP(transId, transIndex, refNo, handicap1, handicap2, betcheck, betteam) {
            var $detaillDiv = $('#divEvent_' + transId + "" + transIndex);
            var $nameDiv = $('#combinationOfferName_' + transId + transIndex);
            $detaillDiv.addClass("mx-parlay"); //to make line-through for void-parlay-ticket

            if ($detaillDiv.is(':hidden')) {
                var url = Site.resolveRelativeUrl("UserPanel/BetList/NewParlay");
                var params = {
                    transId: transId,
                    refno: refNo,
                    handicap1: handicap1,
                    handicap2: handicap2,
                    betcheck: betcheck,
                    betteam: betteam
                };

                $.ajax({
                    type: "POST",
                    dataType: "html",
                    contentType: "application/json; charset=UTF-8",
                    cache: false,
                    url: url,
                    data: JSON.stringify(params),
                    success: function success(response) {
                        $detaillDiv.html(response).show();
                        $nameDiv.hide();
                    },
                    beforeSend: function beforeSend() {
                        Site.showLoader();
                    },
                    complete: function complete() {
                        Site.hideLoader();
                    }
                });
            } else {
                $detaillDiv.hide().html("");
                $nameDiv.show();
            }
        },

        showOldMixParlayDetail: function showOldMixParlayDetail(transId, $divEvent) {
            var url = Site.resolveRelativeUrl("UserPanel/BetList/OldParlay");
            if ($divEvent.is(':hidden')) {
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    cache: false,
                    url: url,
                    data: { transId: transId },
                    success: function success(response) {
                        $divEvent.html(response).show();
                    },
                    beforeSend: function beforeSend() {
                        Site.blockUI();
                    },
                    complete: function complete() {
                        Site.unBlockUI();
                    }
                });
            } else {
                $divEvent.html("").hide();
            }
        },

        ViewCashOutHistory: function ViewCashOutHistory(transId, refNo, betType, betdaqId) {
            var isCashOut = betType === BETTYPE_CASHOUT || betdaqId < 1 && betType !== BETTYPE_BOUGHT;

            var url = Site.resolveRelativeUrl("UserPanel/BetList/CashOutDetail");
            var params = {
                transId: transId,
                refno: refNo,
                betType: betType,
                betdaqId: betdaqId,
                isCashOut: isCashOut
            };

            _me.showPopup(url, params, isCashOut ? 'Cash Out Details' : 'Bought Details');
        }
    };

    return _me;
})(jQuery);

BetList.initialize();

