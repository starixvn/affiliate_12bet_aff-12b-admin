<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs"
    Inherits="UserInfo_Customers" %>
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../App_Js/Calendar2/calendar-normal.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/date.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/Lang/calendar_<%=AppSettings.Session.Language%>.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/calendar-setup.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>    
    <link href="../App_Js/Calendar2/Skin/calendar-blue.css?vers=<%Response.Write(AppSettings.JsVersion);%>" media="all" rel="stylesheet" type="text/css" title="Blue" />


    <script type="text/javascript">
        function NavigatePage(curPage) {
            location.href = "Customers.aspx?page=" + curPage;
        }
        // used in Common.js/LoadData()
        function ValidData(id) {
            if ($("getall").checked)
                return true;   
            var fdate = new Date($("fdate").value);
            var tdate = new Date($("tdate").value);
            if (fdate > tdate) {
                alert('<%=Resources.Lang.DateRangeValid %>');
                return false;
            }
            return true;
        }
       
        function EnableCalendar(isEnable) {
            
            if (isEnable) {
                $("fdate").style.backgroundColor = "";
                $("tdate").style.backgroundColor = "";
                $("fdateTrigger").disabled = false;
                $("tdateTrigger").disabled = false;

                Calendar.setup({
                    inputField: "fdate",
                    ifFormat: "%m/%d/%Y",
                    button: "fdateTrigger",
                    singleClick: true,
                    showsTime: false,
                    showOthers: true,
                    step: 1
                });

                Calendar.setup({
                    inputField: "tdate",
                    ifFormat: "%m/%d/%Y",
                    button: "tdateTrigger",
                    singleClick: true,
                    showsTime: false,
                    showOthers: true,
                    step: 1
                });
            }
            else {
                $("fdate").style.backgroundColor = "#ECE9D8";
                $("tdate").style.backgroundColor = "#ECE9D8";
                $("fdateTrigger").disabled = true;
                $("tdateTrigger").disabled = true;
                $("fdateTrigger").onclick = "";
                $("tdateTrigger").onclick = "";
            }
        }
        window.onload = function () {

            $("getall").onchange = function () {
                EnableCalendar(!this.checked);
            };

            EnableCalendar(!$("getall").checked);
        }
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="170" class="in_top"><asp:Literal runat="server" ID="ltrStaHis"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
