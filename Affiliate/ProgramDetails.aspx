<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ProgramDetails.aspx.cs"
    Inherits="ProgramDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">
                            &nbsp;
                        </td>
                        <td width="170" class="in_top">
                            <%=Resources.Lang.ProgDetails %>
                        </td>
                        <td class="in_right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="in_conts">
                <div class="text">
                    <%=Resources.Lang.As12BETAffiliates%>
                    <p>
                        <%=Resources.Lang.HowMuchYouEarn%>
                    </p>
                </div>
                <div class="box_conts_header">
                    <span id="h1">
                        <%=Resources.Lang.AffiliateEarnings%></span> <a class="b_movetop_<%=AppSettings.Session.Flag %>"
                            href="#">&nbsp;</a>
                </div>
                <div class="box_conts_line">
                </div>
                <div class="text">                    
                    <p>
                        <b><%=Resources.Lang.TC_RevenueShareModel%></b>
                        <br />
                        <%=Resources.Lang.TC_RevenueShareModel_Content%>
                        <br />
                    </p>
                    
                    <p>
                        <b><%=Resources.Lang.Sportbook%></b>
                        <br />
                        <%=Resources.Lang.TC_Sportbook_Content%>
                        <br />
                    </p>
                    <div align="center">
                        <div style="border: 1px solid #d7cfcf; width: 550px;">
                            <table width="550" border="0" cellspacing="1" cellpadding="0" align="center">
                                <tr>
                                    <td style="width: 40px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.Tier.ToUpper() %>
                                    </td>
                                    <td style="width: 250px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.NetWinLoss.ToUpper() %>
                                    </td>
                                    <td style="width: 170px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.QualifyingMem.ToUpper() %>
                                    </td>
                                    <td style="width: 90px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.Comm.ToUpper() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEAEA" align="center">
                                        1
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        $1 - $2,999
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        6 - 10
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        20%
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEAEA" align="center">
                                        2
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        $3,000 - $45,000
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        11 - 50
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        30%
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEAEA" align="center">
                                        3
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        $45,000+
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        51+
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        40%
                                    </td>
                                </tr>
                                <%--<%=tableBody.ToString() %>--%>
                            </table>
                        </div>
                    </div>
                                        
                    <p>
                        <b><%=Resources.Lang.Casino%></b>
                        <br />
                        <%=Resources.Lang.TC_Casino_Content1%>
                        <br />
                    </p>
                    <div align="center">
                        <div style="border: 1px solid #d7cfcf; width: 550px;">
                            <table width="550" border="0" cellspacing="1" cellpadding="0" align="center">
                                <tr>
                                    <td style="width: 40px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.Tier.ToUpper() %>
                                    </td>
                                    <td style="width: 250px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.NetWinLoss.ToUpper() %>
                                    </td>
                                    <td style="width: 170px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.QualifyingMem.ToUpper() %>
                                    </td>
                                    <td style="width: 90px" bgcolor="#C3C3C3" align="center">
                                        <%= Resources.Lang.Comm.ToUpper() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEAEA" align="center">
                                        1
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        $1 - $2,999
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        6 - 10
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        20%
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEAEA" align="center">
                                        2
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        $3,000 - $45,000
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        11 - 50
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        30%
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEAEA" align="center">
                                        3
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        $45,000+
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        51+
                                    </td>
                                    <td bgcolor="#EEEAEA" align="center">
                                        40%
                                    </td>
                                </tr>
                                <%--<%=tableBody.ToString() %>--%>
                            </table>
                        </div>
                    </div>
                    <p>
                        <%=Resources.Lang.TC_Casino_Content2%>
                        <br />
                    </p>

                </div>
                <div class="box_conts_header">
                    <span id="h2">
                        <%=Resources.Lang.NegativeBalance%></span> <a class="b_movetop_<%=AppSettings.Session.Flag %>"
                            href="#">&nbsp;</a>
                </div>
                <div class="box_conts_line">
                </div>
                <div class="text">
                    <%=Resources.Lang.AnyNegativeBalance%>
                </div>
                <div class="box_conts_header">
                    <span id="h3">
                        <%=Resources.Lang.GettingPaid%></span> <a class="b_movetop_<%=AppSettings.Session.Flag %>"
                            href="#">&nbsp;</a>
                </div>
                <div class="box_conts_line">
                </div>
                <div class="text">
                    <%=Resources.Lang.YouCanRequestPayout%>
                    <p>
                    </p>
                    <%=Resources.Lang.WeOfferSeveralMethods%>
                    <br />
                    <div class="padd15">
                        1.
                        <%=Resources.Lang.ToYour12BET%><br />
                        2.
                        <%=Resources.Lang.Moneybookers%>
                        *<br />
                        3.
                        <%=Resources.Lang.Neteller%>
                        *<br />
                        4.
                        <%=Resources.Lang.WireTransfer%>
                        *
                    </div>
                    <p>
                    </p>
                    <span id="h4">*
                        <%=Resources.Lang.AffilatesAreOnlyAllowed%></span>
                    <br />
                    <%=Resources.Lang.TheMinimumWithdrawal%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
