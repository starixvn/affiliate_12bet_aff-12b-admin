<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Account_Overview.aspx.cs"
    Inherits="Account_Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" language="javascript" src="../App_Js/swfobject.js"></script>
    <script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
    <script src="../App_Js/IBCWnd/IBCWnd.js" language="javascript" type="text/javascript"></script>    
    <script type="text/javascript" language="javascript">

        function HidePopup() {
            var p = document.getElementById("pop_up");
            p.style.display = "none";
            //set cookie according to checkbox status
        }

        function SetPopupShow() {
            if ($("dontshow").checked)
                setCookie("dontshow" + "$" + $("custid").value, "true", 30);
            else
                setCookie("dontshow" + "$" + $("custid").value, "false", 30);
        }
    
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">
                            &nbsp;
                        </td>
                        <td width="210" class="in_top">
                            <asp:Literal runat="server" ID="ltrAccOverview"></asp:Literal>
                        </td>
                        <td class="in_right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <form action="" id="Register_form" name="Register_form" method="get">
            <div class="in_conts">
                <div class="box_conts_header">
                    <div class="flright">
                        <%= slMonth%>&nbsp;&nbsp;
                        <select id="slYear" name="slYear">
                            <option value="2007">2007</option>
                            <option value="2008">2008</option>
                            <option value="2009">2009</option>
                            <option value="2010">2010</option>
                            <option value="2011">2011</option>
                            <option value="2012">2012</option>
                            <option value="2013">2013</option>
                            <option value="2014">2014</option>
                            <option value="2015">2015</option>
                            <option value="2016">2016</option>
                            <option value="2017">2017</option>
                            <option value="2018">2018</option>
                            <option value="2019">2019</option>
                            <option value="2020">2020</option>
                        </select>&nbsp;&nbsp;<input value="<%=Resources.Lang.Submit %>" class="button" type="submit" />&nbsp;
                    </div>
                    <asp:Literal runat="server" ID="ltrStatic"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="5" style="margin-bottom: 5px">
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td style="width: 210px;">
                            <asp:Literal runat="server" ID="ltrNewSignup"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal runat="server" ID="ltrSignup"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTotalActive"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalActiveVal"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <%--<%=Resources.Lang.TotalQualifiedMembers %>:--%>
                            <%=Resources.Lang.Tqacsbc%>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalQualifiedMembers"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTotalDeposit"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalDepositVal"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <%=Resources.Lang.Turnover %>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTurnoverMonth"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrStaticUp"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="5" style="margin-bottom: 5px">
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td style="width: 190px;">
                            <asp:Literal runat="server" ID="ltrTotalPlay"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal ID="ltrTotalPlayer" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTurnover"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal ID="ltrTurnoverVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <asp:Literal runat="server" ID="ltrDepositUp"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal ID="ltrDepositUpVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td style="width: 190px;">
                            <asp:Literal runat="server" ID="BalanceBroughtText"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal ID="BalanceBroughtVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrComm"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <div class="tbl-box">
                    <%=strTierSetting %>
                </div>
            </div>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        $("slMonth").value = "<%=strMonths%>";
        $("slYear").value = "<%=strYears%>";
    </script>
    <input type="hidden" name="flashpath" id="flashpath" value="<%=flashPath %>" />
    <input type="hidden" name="showpopup" id="showpopup" value="<%=showPopup %>" />
    <input type="hidden" name="custid" id="custid" value="<%=AppSettings.Session.CustID %>" />
    <%--<script type="text/javascript">
        var so = new SWFObject(""+$("flashpath").value, "flCommission", "420", "270", "9", "#000000");
        so.addVariable("afflink", "../ProgramDetails.aspx");
        so.write("fl");
    </script>
    --%>
    <script type="text/javascript">
        var Init = function () {
            var username = getCookie("Username");
            var dontShowMessage = getCookie(username + '_DontShowMessage');

            if (dontShowMessage !== 'true') {
                var height = document.body.clientHeight;
                var width = document.body.clientWidth;
                var widthDialog = 450;
                var heightDialog = 220;
                var top = (height - heightDialog) / 2;
                var left = (width - widthDialog) / 2 - 180;
                DoCalFull('Announcement.aspx', '', widthDialog, heightDialog, top, left);

                setTimeout(function () {
                    var ibcTable = document.getElementsByClassName('IBCWndTable')[0];
                    var iconTitle = ibcTable.getElementsByClassName('iconpopup')[0];
                    iconTitle.style.display = 'none';
                }, 200);
            }
        };

        window.onload = Init;
    </script>
</asp:Content>
