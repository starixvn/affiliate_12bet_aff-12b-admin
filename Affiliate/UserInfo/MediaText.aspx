<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="MediaText.aspx.cs" Inherits="UserInfo_MediaText" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type ="text/javascript">
    var jQ = jQuery.noConflict();

    jQ.fn.serializeObject = function () {
        "use strict";
        var o = {};

        var a = this.serializeArray();

        jQ.each(a, function () {
            if (o[this.name] !== undefined) {
                o[this.name] = o[this.name] + ',' + this.value;
            } else {
                o[this.name] = this.value || '';
            }
        });

        return o;
    };

    window.onload=function() {
        EnableDisableInputTrackingCode();

        jQ('#slDevice').change(function () {
            var obj = jQ('#frmData').serializeObject();
            delete obj['target'];
            delete obj['slDomain'];
            var url = location.href;
            var posQuestion = url.indexOf('?');

            if (posQuestion > 0) {
                url = url.substring(0, posQuestion);
            }

            location.href = url + "?" + jQ.param(obj);
        });
    };
    function NavigatePage(curPage) {
        GetDataBannerList(curPage,false);      
    }
    function GetDataBannerList(page, isGetTextLink) {
        var param = 'cate=' + $('slCategory').value + '&lang=' + $('slLanguage').value + '&currency=' + $('slCurrency').value + '&type=' + $('slImageType').value + "&size=" + $('slSize').value + "&page=" + page;      
       
        var ajax = new Ajax.Request("../Handler/GetDataBanner.ashx", {
            method: 'post',
            parameters: param,
            
            onComplete: function(data) {
            ProcessResult(data.responseText);
                
            }
        });
    }

    function ProcessResult(data) {
        try {
            var arr = data.split("$$$");            
            var sXmlBody = "<table><tbody id='result'>" + arr[0] + "</tbody></table>";
            var tempDiv = document.createElement("DIV");
            tempDiv.innerHTML = sXmlBody;
            $("tblResult").removeChild($("result"));
            $("tblResult").appendChild(tempDiv.firstChild.firstChild);
            if (arr.length > 1) {
                $("textlink").innerHTML = arr[1];
            }           
        }
        catch (e) {
            //alert(e);
        }
    }
    function EnableDisableInputTrackingCode() {        
        
        clock();
        setInterval("clock();", 1000);
    }
    function checkData()
    {
    }
</script>
<%=htmlContent %>
</asp:Content>
