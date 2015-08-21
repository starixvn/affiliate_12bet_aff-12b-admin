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
    DoCalFull(url, 'Results', widthDialog, heightDialog, 150, 290);
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
    DoCalFull(url, 'System Parlay Detail', widthDialog, heightDialog, 150, 290);
}