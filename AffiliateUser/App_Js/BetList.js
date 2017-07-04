/* global AFFILIATE, QueryString */
/* exported showCombMP, showCombMPDetail, showMP*/
"use strict";

function getMixParlayResultUrl(refno, winlostdate, bettype, username, affid) {
    var url = "BetListResults.aspx?";
    url += "refno=" + refno;
    url += "&winlossdate=" + winlostdate;
    url += "&bettype=" + bettype;
    url += "&matchid=" + 1;
    url += "&username=" + username;
    url += "&affid=" + affid;

    return url;
}

function getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betid, affid) {
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
}
        
//var ViewResult = BetList.showResult;
function ViewResult(matchid, raceNo, bettype, sporttype, refno, username, winlostdate, refnoMixParlay, league, isOutright, betid) {
    var affid = $j('#affid').val();

    var url = bettype === 9 || bettype === 29
                    ? getMixParlayResultUrl(refnoMixParlay, winlostdate, bettype, username, affid)
                    : getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betid, affid);

    var widthDialog = 720;
    var heightDialog = 400;
    Common.openPopup(url, 'Results', widthDialog, heightDialog, 150, 290);
}

function showMP(transId) {
    var divEvent = document.getElementById('divEvent_' + transId);
    divEvent.className = "mx-parlay"; //to make line-through for void-parlay-ticket

    // Manage biz
    if (divEvent.style.display === 'none') {
        $j.ajax({
            method: 'post',
            dataType: 'text',
            parameters: 'transId=' + transId,
            onComplete: function(responseData) {
                divEvent.innerHTML = responseData.responseText;
            },
            type: "GET",
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
        $j.ajax({
            method: 'post',
            parameters: 'transId=' + transId,
            onComplete: function(responseData) {
                divEvent.innerHTML = responseData.responseText;
            },
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

function showCombMPDetail(transId, refNo, betId, custId, betTeam, baseCurrency)
{
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
    Common.openPopup(url, 'System Parlay Detail', widthDialog, heightDialog, 150, 290);
}

function showNewCombMP(transId, transIndex, refNo, handicap1, handicap2, betcheck, betteam) {
    var $detaillDiv = $j('#divEvent_' + transId + "" + transIndex);
    var $nameDiv = $j('#combinationOfferName_' + transId + transIndex);
    $detaillDiv.addClass("mx-parlay"); //to make line-through for void-parlay-ticket

    if ($detaillDiv.is(':hidden')) {
        var url = "BetListNewParlay.ashx?";
        url += "transId=" + transId;
        url += "&refno=" + refNo;
        url += "&handicap1=" + handicap1;
        url += "&handicap2=" + handicap2;
        url += "&betcheck=" + betcheck;
        url += "&betteam=" + betteam;
        $j.ajax({
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
}

var BETTYPE_CASHOUT = 911;
var BETTYPE_BOUGHT = 912;

function ViewCashOutHistory(transId, refNo, betType, betdaqId) {
    var isCashOut = betType === BETTYPE_CASHOUT || (betdaqId < 1 && betType !== BETTYPE_BOUGHT);

    var url = "BetListCashOutDetail.aspx?";
    url += "transId=" + transId;
    url += "&refno=" + refNo;
    url += "&betType=" + betType;
    url += "&betdaqId=" + betdaqId;
    url += "&isCashOut=" + isCashOut;
    var widthDialog = 850;
    var heightDialog = 450;
    Common.openPopup(url, isCashOut ? 'Cash Out Details' : 'Bought Details', widthDialog, heightDialog, window.innerWidth > widthDialog ? (window.innerWidth - widthDialog)/2 : 0, 290);
}