<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs"
    Inherits="AboutUs" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" cellspacing="0" cellpadding="0" style="margin-bottom:5px">
        <tr style="height: 34px">
            <td class="box-topleft">
                &nbsp;
            </td>
            <td class="box-topcenter">
                <div class="ContentHeader">
                    <span>About Affiliate 12bet.com</span> <span class="titleBG">&nbsp;</span>
                </div>
            </td>
            <td class="box-topright">
            </td>
        </tr>
        <tr>
            <td class="box-left">
            </td>
            <td valign="top" bgcolor="#fdfdfd">
                <div id="para_content">
                    <p class="justify">
                        The <a href="<%=Common.RootURL() %>">
                            <%=Common.RootURL() %>
                        </a>Partnership Gateway gives you access to earning exciting revenues from Pacific
                        Sea Invests SA.
                    </p>
                    <p class="justify">
                        Pacific Sea Invests SA is an Asian-based, remote gambling operator focused primarily
                        on offering sports betting products and services to certain European and Asia Pacific
                        markets through several brands.
                    </p>
                    <p class="justify">
                        Pacific Sea Invests SA operates this website - www.12BET.com.
                    </p>
                    <p class="justify">
                        The products and services offered through this website are licensed and regulated
                        by First Cagayan Leisure and Resort Corporation (www.firstcagayan.com) for the Cagayan
                        Economic Zone Authority of the Philippines government. A copy of our company's license
                        may be viewed <a href="http://www.12bet.com/template/deposit/en/popup/images/Pacific.jpg" target="_blank">here</a>.
                    </p>
                    <p class="justify">
                        Our philosophy is to grow our business through seeking to deliver to sports betting
                        customers the best overall experience in the market.
                    </p>
                    <p class="justify">
                        You'll also enjoy the following benefits when you join in as an affiliate:
                    </p>
                    <div id="para" class="pad15">
                        <ul>
                            <li>The most complete affiliate commission structure on the Internet.</li>
                            <li>Payments delivered to you on time, every month.</li>
                            <li>A choice of convenient payment solutions.</li>
                            <li>Accurate, up-to-date, statistical affiliate reporting.</li>
                            <li>The most robust affiliate system on the web.</li>
                            <li>Great customer service - 15 minute response times during office hours.</li>
                        </ul>
                    </div>
                    <p class="justify">
                        Join now and be part of the most competitive affiliate commission structure, view
                        accurate statistical reports and receive rewarding on time monthly payments.
                    </p>
                    <p class="justify">
                        <b>E-mail:</b> <a href="mailto:<%=AppSettings.MailContact %>"><%=AppSettings.MailContact %></a>
                        <br />
                        <b>Telephone:</b> <%=AppSettings.PhoneContact%>
                    </p>
                </div>
            </td>
            <td class="box-right">
            </td>
        </tr>
        <tr>
            <td class="box-bottomleft" />
            <td class="box-bottomcenter" />
            <td class="box-bottomright" />
        </tr>
    </table>
</asp:Content>
