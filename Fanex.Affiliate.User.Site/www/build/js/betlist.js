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
        },

        getMixParlayResultUrl: function getMixParlayResultUrl(refno, winlostdate, bettype, username) {
            var queryString = "refno=" + refno + "&winlossdate=" + winlostdate + "&bettype=" + bettype + "&matchid=" + 1 + "&username=" + username;
            return Site.resolveRelativeUrl("BetList/Result?" + queryString);
        },

        getSportBookResultUrl: function getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betidOrHdp1, hdp2) {
            var queryString = "matchid=" + matchid + "&bettype=" + bettype + "&raceno=" + raceNo + "&sporttype=" + sporttype + "&isoutright=" + isOutright + "&username=" + username + "&betid=" + betidOrHdp1;

            if (hdp2) {
                queryString += "&handicap2=" + hdp2;
            }

            return Site.resolveRelativeUrl("BetList/Result?" + queryString);
        },

        ViewResult: function ViewResult(matchid, raceNo, bettype, sporttype, refno, username, winlostdate, refnoMixParlay, league, isOutright, betidOrHdp1, hdp2) {
            var url = isMixParlay(bettype) ? _me.getMixParlayResultUrl(refnoMixParlay, winlostdate, bettype, username) : _me.getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betidOrHdp1, hdp2);

            _me.showPopup(url, resourceLang.result);
        },

        showPopup: function showPopup(url, title, onSuccess) {
            $.ajax({
                type: "GET",
                dataType: "html",
                cache: false,
                url: url,
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
            var divEvent = document.getElementById('divEvent_' + transId);
            divEvent.className = "mx-parlay"; //to make line-through for void-parlay-ticket

            // Manage biz
            if (divEvent.style.display === 'none') {
                $j.ajax({
                    method: 'post',
                    dataType: 'text',
                    parameters: 'transId=' + transId,
                    onComplete: function onComplete(responseData) {
                        divEvent.innerHTML = responseData.responseText;
                    },
                    type: "GET",
                    cache: false,
                    url: "BetListParlay.ashx?transId=" + transId,
                    error: function error() {},
                    success: function success(responseData) {
                        divEvent.innerHTML = responseData;
                        divEvent.style.display = "";
                    },
                    beforeSend: function beforeSend() {},
                    complete: function complete() {}
                });
            } else {
                divEvent.style.display = "none";
                divEvent.innerHTML = "";
            }
        },

        showCombMP: function showCombMP(transId, index) {
            var divEvent = document.getElementById('divEvent_' + transId + "" + index);
            divEvent.className = "mx-parlay"; //to make line-through for void-parlay-ticket

            // Manage biz
            if (divEvent.style.display === 'none') {
                $j.ajax({
                    method: 'post',
                    parameters: 'transId=' + transId,
                    onComplete: function onComplete(responseData) {
                        divEvent.innerHTML = responseData.responseText;
                    },
                    type: "GET",
                    dataType: "text",
                    cache: false,
                    url: "BetListParlay.ashx?transId=" + transId,
                    error: function error() {},
                    success: function success(responseData) {
                        divEvent.innerHTML = responseData;
                        divEvent.style.display = "";
                    },
                    beforeSend: function beforeSend() {},
                    complete: function complete() {}
                });
            } else {
                divEvent.style.display = "none";
                divEvent.innerHTML = "";
            }
        },

        showCombMPDetail: function showCombMPDetail(transId, refNo, betId, custId, betTeam, baseCurrency) {
            if (transId === 0) {
                return;
            }

            var queryString = "transId" + transId + "&refno=" + refNo + "&betid=" + betId + "&custid=" + custId + "&betteam=" + betTeam + "&basecurrency=" + baseCurrency;
            var url = Site.resolveRelativeUrl("BetList/ParlayDetail?" + queryString);

            _me.showPopup(url, betTeam);
        },

        showNewCombMP: function showNewCombMP(transId, transIndex, refNo, handicap1, handicap2, betcheck, betteam) {
            var $detaillDiv = $('#divEvent_' + transId + "" + transIndex);
            var $nameDiv = $('#combinationOfferName_' + transId + transIndex);
            $detaillDiv.addClass("mx-parlay"); //to make line-through for void-parlay-ticket

            if ($detaillDiv.is(':hidden')) {
                var queryString = "transId=" + transId + "&refno=" + refNo + "&handicap1=" + handicap1 + "&handicap2=" + handicap2 + "&betcheck=" + betcheck + "&betteam=" + betteam;
                var url = Site.resolveRelativeUrl("BetList/NewParlay?" + queryString);

                $.ajax({
                    type: "GET",
                    dataType: "html",
                    cache: false,
                    url: url,
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

        ViewCashOutHistory: function ViewCashOutHistory(transId, refNo, betType, betdaqId) {
            var isCashOut = betType === BETTYPE_CASHOUT || betdaqId < 1 && betType !== BETTYPE_BOUGHT;

            var queryString = "transId=" + transId + "&refno=" + refNo + "&betType=" + betType + "&betdaqId=" + betdaqId + "&isCashOut=" + isCashOut;
            var url = Site.resolveRelativeUrl("BetList/CashOutDetail?" + queryString);

            _me.showPopup(url, isCashOut ? 'Cash Out Details' : 'Bought Details');
        }
    };

    return _me;
})(jQuery);

BetList.initialize();

