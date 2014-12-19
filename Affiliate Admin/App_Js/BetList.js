/* global AFFILIATE, QueryString */
/* exported showCombMP, showCombMPDetail, showMP, ViewResult */
"use strict";

var BetList = (function ($) {
    return {
        initialize: function () {
        },

        getMixParlayResultUrl: function (refno, winlostdate, bettype, username) {
            var url = "BetListResults.aspx?";
            url += "refno=" + refno;
            url += "&winlossdate=" + winlostdate;
            url += "&bettype=" + bettype;
            url += "&matchid=" + 1;
            url += "&username=" + username;

            return url;
        },

        getSportsbookResultUrl: function (matchid, bettype, raceNo, sporttype, league, isOutright, username, betid) {
            var url = "BetListResults.aspx?";
            url += "matchid=" + matchid;
            url += "&bettype=" + bettype;
            url += "&raceno=" + raceNo;
            url += "&sporttype=" + sporttype;
            url += "&isoutright=" + isOutright;
            url += "&username=" + username;
            url += "&betid=" + betid;

            return url;
        },

        showResult: function (matchid, raceNo, bettype, sporttype, refno, username, winlostdate, refnoMixParlay, league, isOutright, betid) {
            var url = bettype === 9 || bettype === 29
                    ? BetList.getMixParlayResultUrl(refnoMixParlay, winlostdate, bettype, username)
                    : BetList.getSportsbookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betid);

            var widthDialog = 720;
            var heightDialog = 400;
            DoCalFull(url, 'Results', widthDialog, heightDialog, 150, 0);
        },

        showSystemParlayDetail: function (transId, refNo, betId, custId, betTeam, baseCurrency) {
            if (transId === 0) {
                return;
            }

            var url = "BetListParlayDetail.aspx?";
            url += "transId=" + transId;
            url += "&refno=" + refNo;
            url += "&betid=" + betId;
            url += "&custid=" + custId;
            url += "&betteam=" + betTeam;
            url += "&basecurrency=" + baseCurrency;
            var widthDialog = 760;
            var heightDialog = 450;
            DoCalFull(url, 'System Parlay Detail', widthDialog, heightDialog, 150, 0);
        },

        ViewCasinoHandHistory: function (refNo) {
            var url = AFFILIATE.getAbsoluteUrl('Account/RNGCasinoData');
            var urlParam = $("#casino-hand-history-" + refNo).attr('url-param');

            url += urlParam;
            var title = "Results";
            AFFILIATE.openDialogFrame(url, title, { width: 950, height: 500 });
        }
    };
})(jQuery);

var ViewResult = BetList.showResult;

function showMP(transId) {
    var divEvent = document.getElementById('divEvent_' + transId);
    divEvent.className = "mx-parlay"; //to make line-through for void-parlay-ticket
    
    // Manage biz
    if (divEvent.style.display === 'none') {
        $.ajax({
            type: "GET",
            dataType: "text",
            cache: false,
            url: "BetListParlay.ashx?transId=" + transId,
            error: function () {
            },
            success: function (responseData) {
                divEvent.innerHTML = responseData;
                divEvent.style.display = "";
            },
            beforeSend: function () {

            },
            complete: function () {
            }
        });
    }
    else {
        divEvent.style.display = "none";
        divEvent.innerHTML = "";
    }
}

function showCombMP(transId, index) {
    var divEvent = document.getElementById('divEvent_' + transId + "" + index);
    divEvent.className = "mx-parlay"; //to make line-through for void-parlay-ticket

    // Manage biz
    if (divEvent.style.display === 'none') {
        $.ajax({
            type: "GET",
            dataType: "text",
            cache: false,
            url: "BetListParlay.ashx?transId=" + transId,
            error: function () {
            },
            success: function (responseData) {
                divEvent.innerHTML = responseData;
                divEvent.style.display = "";
            },
            beforeSend: function () {

            },
            complete: function () {
            }
        });
    }
    else {
        divEvent.style.display = "none";
        divEvent.innerHTML = "";
    }
};

var showCombMPDetail = BetList.showSystemParlayDetail;