<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="MediaText.aspx.cs" Inherits="UserInfo_MediaText" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type ="text/javascript">
    var jQ = jQuery.noConflict();
    window.onload=function() {
        EnableDisableInputTrackingCode();

        jQ('#slDevice').change(function () {
            var deviceType = jQ(this).val();
            jQ('#btnSearch').attr("disabled", "disabled");
            var ajax = new Ajax.Request("../Handler/GetDomainList.ashx", {
                method: 'post',
                parameters: 'deviceType=' + deviceType,
                onComplete: function (data) {
                    var domainRegion = document.getElementById("domain-region");
                    domainRegion.innerHTML = data.responseText;
                }
            });

            var ajax1 = new Ajax.Request("../Handler/GetPageList.ashx", {
                method: 'post',
                parameters: 'deviceType=' + deviceType,
                onComplete: function (data) {
                    var domainRegion = document.getElementById("target-domain");
                    domainRegion.innerHTML = data.responseText;
                    jQ('#btnSearch').removeAttr("disabled");
                }
            });
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
