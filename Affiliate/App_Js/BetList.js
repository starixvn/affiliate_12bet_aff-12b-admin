/* global AFFILIATE, QueryString */
/* exported showCombMP, showCombMPDetail, showMP, ViewResult */
"use strict";

function getMixParlayResultUrl(refno, winlostdate, bettype, username) {
    var url = "BetListResults.aspx?";
    url += "refno=" + refno;
    url += "&winlossdate=" + winlostdate;
    url += "&bettype=" + bettype;
    url += "&matchid=" + 1;
    url += "&username=" + username;

    return url;
}

function getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betid) {
    var url = "BetListResults.aspx?";
    url += "matchid=" + matchid;
    url += "&bettype=" + bettype;
    url += "&raceno=" + raceNo;
    url += "&sporttype=" + sporttype;
    url += "&isoutright=" + isOutright;
    url += "&username=" + username;
    url += "&betid=" + betid;

    return url;
}
        
//var ViewResult = BetList.showResult;
function ViewResult(matchid, raceNo, bettype, sporttype, refno, username, winlostdate, refnoMixParlay, league, isOutright, betid)
{
    var url = bettype === 9 || bettype === 29
                    ? getMixParlayResultUrl(refnoMixParlay, winlostdate, bettype, username)
                    : getSportBookResultUrl(matchid, bettype, raceNo, sporttype, league, isOutright, username, betid);

    var widthDialog = 720;
    var heightDialog = 400;
    DoCalFull(url, 'Results', widthDialog, heightDialog, 150, 290);
}

function showMP(transId) {
    var divEvent = document.getElementById('divEvent_' + transId);
    divEvent.className = "mx-parlay"; //to make line-through for void-parlay-ticket

    // Manage biz
    if (divEvent.style.display === 'none') {
        jQuery.ajax({
            method: 'post',
            dataType: 'text',
            parameters: 'transId=' + transId,
            onComplete: function (responseData) {
                divEvent.innerHTML = responseData.responseText;
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
        jQuery.ajax({
            method: 'post',
            dataType: 'text',
            parameters: 'transId=' + transId,
            onComplete: function (responseData) {
                divEvent.innerHTML = responseData.responseText;
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
    DoCalFull(url, 'System Parlay Detail', widthDialog, heightDialog, 150, 290);
}