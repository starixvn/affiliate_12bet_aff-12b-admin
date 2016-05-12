/* global AFFILIATE, QueryString */
/* exported showCombMP, showCombMPDetail, showMP, ViewResult */
"use strict";

var BetList = (function ($) {
    return {
        initialize: function () {
        },

        getMixParlayResultUrl: function (refno, winlostdate, bettype, username, affid) {
            var url = "BetListResults.aspx?";
            url += "refno=" + refno;
            url += "&winlossdate=" + winlostdate;
            url += "&bettype=" + bettype;
            url += "&matchid=" + 1;
            url += "&username=" + username;
            url += "&affid=" + affid;
            return url;
        },

        getSportsbookResultUrl: function (matchid, bettype, raceNo, sporttype, league, isOutright, username, betid, affid) {
            var url = "BetListResults.aspx?";
            url += "matchid=" + matchid;
            url += "&bettype=" + bettype;
            url += "&raceno=" + raceNo;
            url += "&sporttype=" + sporttype;
            url += "&isoutright=" + isOutright;
            url += "&username=" + username;
            url += "&betid=" + betid;
            url += "&affid=" + affid;

            return url;
        },

        showResult: function (matchid, raceNo, bettype, sporttype, refno, username, winlostdate, refnoMixParlay, league, isOutright, betid) {
            var affid = $("#affid").val();
            var url = bettype === 9 || bettype === 29
                    ? BetList.getMixParlayResultUrl(refnoMixParlay, winlostdate, bettype, username, affid)
                    : BetList.getSportsbookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betid, affid);

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

        showNewParlayDetail: function (transId, transIndex, refNo, handicap1, handicap2, betcheck, betteam) {
            var $detaillDiv = $('#divEvent_' + transId + "" + transIndex);
            var $nameDiv = $('#combinationOfferName_' + transId + transIndex);
            $detaillDiv.addClass("mx-parlay"); //to make line-through for void-parlay-ticket

            if ($detaillDiv.is(':hidden')) {
                var url = "BetListNewParlay.ashx?";
                url += "transId=" + transId;
                url += "&refno=" + refNo;
                url += "&handicap1=" + handicap1;
                url += "&handicap2=" + handicap2;
                url += "&betcheck=" + betcheck;
                url += "&betteam=" + betteam;
                $.ajax({
                    type: "GET",
                    contentType: 'application/json',
                    dataType: "text",
                    cache: false,
                    url: url,
                    error: function () {
                    },
                    success: function (response) {
                        $detaillDiv.html(response).show();
                        $nameDiv.hide();
                    },
                    beforeSend: function () {
                    },
                    complete: function () {
                    }
                });
            }
            else {
                $detaillDiv.hide().html("");
                $nameDiv.show();
            }
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
var showNewCombMP = BetList.showNewParlayDetail;