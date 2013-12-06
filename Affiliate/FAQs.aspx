<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="FAQs.aspx.cs"
    Inherits="FAQs" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="144" class="in_top"><%=Resources.Lang.Faqs%></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">                
                <div class="box_conts_header">
                  <span><%=Resources.Lang.General%></span>
                  <a class="b_movetop_<%=AppSettings.Session.Flag %>" href="#">&nbsp;</a> 
                </div>
                <div class="box_conts_line"></div>
                <div class="text">
                    <b id="f1"><%=Resources.Lang.WhatIs12BETAffiliates%></b>
                    <br />
                    <%=Resources.Lang.BETAffiliatesProgram%>
                    <p></p>
                    <b id="f2"><%=Resources.Lang.HowDoISignUp%></b>
                    <br />
                    <%=Resources.Lang.The12BETAffiliates.Replace("{AppForm}", AppForm)%>
                    <p></p>                    
                    <b id="f3"><%=Resources.Lang.WhatIfIDontHave%></b>
                    <br />
                    <%=Resources.Lang.ToParticipateIn12BET.Replace("{mail}", MailContact)%>
                    <p></p>
                </div>                
                <div class="box_conts_header">
                  <span><%=Resources.Lang.MarketingTracking%></span>
                  <a class="b_movetop_<%=AppSettings.Session.Flag %>" href="#">&nbsp;</a> 
                </div>
                <div class="box_conts_line"></div>
                    <div class="text">
                        <b id="f4"><%=Resources.Lang.WhereDoIGetBanners%></b>
                        <br />
                        <%=Resources.Lang.ByAccessingOurMediaGallery%> 
                        <p></p>                       
                        <b id="f5"><%=Resources.Lang.HowDoesTheSystem%></b>
                        <br />                        
                        <%=Resources.Lang.WeEmploys%>
                        <p></p>                        
                        <b><%=Resources.Lang.WillYouTrackAndGiveMeCredit%></b>
                        <br />                        
                        <%=Resources.Lang.WeAreAbleToTrackVisitors%>
                        <p></p>                        
                        <b><%=Resources.Lang.WhatIsACookie%></b>
                        <br />
                        <%=Resources.Lang.CookiesAreAGeneralMechanism%>
                        <p></p>                       
                        <b><%=Resources.Lang.DoYouRewardTheFirstAffiliate%></b>
                        <br />                        
                        <%=Resources.Lang.TheCookiesAreOverwritten%>
                        <p></p>                        
                        <b id="f6"><%=Resources.Lang.HowCanICheck%></b>
                        <br />                        
                        <%=Resources.Lang.YouCanLoginToYour12BET%>
                        <p></p>                        
                        <b><%=Resources.Lang.WhatIsYourPolicy%></b>
                        <br />                        
                        <%=Resources.Lang.WeDoNotTolerateSpamming%>
                    </div>
                <div class="box_conts_header">
                    <span><%=Resources.Lang.GettingPaid%></span>
                    <a class="b_movetop_<%=AppSettings.Session.Flag %>" href="#">&nbsp;</a> 
                </div>
                <div class="box_conts_line"></div>
                    <div class="text">
                        <b id="f7"><%=Resources.Lang.WhatAreTheWithdrawal%></b>
                        <br />                        
                        <%=Resources.Lang.YouCanRequestForAWithdrawal%>
                        <p></p>                        
                        <b><%=Resources.Lang.CanIRequest%></b>
                        <br />                        
                        <%=Resources.Lang.TheCurrenciesOfferedAre%>
                        <p></p>                        
                        <b id="f8"><%=Resources.Lang.WhatIsTheMinimumWithdrawal%></b>
                        <br />                        
                        <%=Resources.Lang.TheMinimumWithdrawal%>
                        <p></p>                        
                        <b><%=Resources.Lang.DoesNegativeBalance%></b>
                        <br />                        
                        <%=Resources.Lang.WeRolloverNegative%>
                        <p></p>                        
                        <b id="h4"><%=Resources.Lang.HowOftenCanIRequest%></b>
                        <br />                        
                        <%=Resources.Lang.AMaximumOfOneWithdrawal%> 
                        <p></p>
                    </div>                   
                    <div id="divJoin" runat="server" class="c" style="padding-left: 135px;">
                        <a class="signup" href="Signup.aspx">
                            <span><%=Resources.Lang.JoinThe12BetAffiliate%></span>
                        </a>
                    </div>
                    <div class="clear"></div>
                </div>
        </div>
    </div>
</asp:Content>
