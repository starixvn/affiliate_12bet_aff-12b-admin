<%@ Page Language="C#" MasterPageFile="~/Master2.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUS" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<%@ Register Src="Controls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" Runat="Server">
    <title>12BET Affiliate Program | Contact Us</title>
    <meta name="description" content="12BET - Sports Betting and Online Casino Affiliate Program: Customer Support and Contact Details" />
    <meta name="keywords" content="12BET, 12BET Affiliate Program, Sports Betting Affiliate, Casino Affiliate, betting affiliate programs, Contact Details, top affiliate programs, asian casino affiliate programs, Asian betting affiliate programs, high payout affiliate programs" />
    <meta name="robots" content="index,follow,archive" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
  <div id="contents">        
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="144" class="in_top">
                            <%=Resources.Lang.ContactUs%>
                        </td>
                        <td class="in_right">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="padd15">
                <div id="addressForm">
                    <div class="contact-icon">&nbsp;</div>
                    <div class="contact-list">
                        <ul>
                            <li><span><%=Resources.Lang.EnglishSupport %>:</span><a style="color:Black;" href="mailto:aff12b_eng@hotmail.com">aff12b_eng@hotmail.com</a></li>
                            <li><span><%=Resources.Lang.ThaiSupport %>:   </span><a style="color:Black;" href="mailto:csd12bthai_affiliate@hotmail.com">csd12bthai_affiliate@hotmail.com</a></li>
                            <li><span><%=Resources.Lang.VietnameseSupport %>:</span><a style="color:Black;" href="mailto:aff12b_viet@yahoo.com">aff12b_viet@yahoo.com</a></li>
                        </ul>
                    </div>
                    <div class="clear"></div>
                </div>
                <br />
                <uc1:ContactUs ID="ContactUs1" runat="server" />
            </div>
        </div>
    </div>  
</asp:Content>