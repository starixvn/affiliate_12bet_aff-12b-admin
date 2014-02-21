<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Home_Center.ascx.cs" Inherits="Controls_Home_Center" %>
<div id="banner_main">
    <object height="275" width="646" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
      <param value="Flash/<%=Common.GetLanguage() %>/home_banner.swf?v=<%=AppSettings.JsVersion %>" name="movie"/>
      <param value="high" name="quality"/>
      <embed height="275" width="646" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high" src="<%=Common.GetBannerFlash() %>?v=<%=AppSettings.JsVersion %>"/>
    </object>
</div>
<div id="contents">
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr valign="top">
            <td style="width: 49%;">
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.Earn30 %>
                    </div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/earn.png" />
                        <%=Common.CutLongText(Resources.Lang.DontSettleFor2ndBest, 130)%>
                        <a class="more_button"
                            href="ProgramDetails.aspx#h1"><%=Resources.Lang.More %>...</a></div>
                </div>
            </td>
            <td  style="width: 2%;" />
            <td style="width: 49%;">
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.NegativeBalance %></div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/negative.png" />
                        <%=Common.CutLongText(Resources.Lang.AnyNegativeBalance, 130)%>
                        <a class="more_button"
                            href="ProgramDetails.aspx#h2"><%=Resources.Lang.More %>...</a></div>
                </div>               
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.WideRange %></div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/payment.png" />
                        <%=Common.CutLongText(Resources.Lang.YouCanRequestPayout, 130)%>
                        <a class="more_button"
                            href="ProgramDetails.aspx#h3"><%=Resources.Lang.More %>...</a></div>
                </div>
            </td>
            <td />
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.PromptMonthly %></div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/prompt.png" />
                        <%=Common.CutLongText(Resources.Lang.AMaximumOfOneWithdrawal, 130)%>
                        <a class="more_button" href="FAQs.aspx#h4"><%=Resources.Lang.More %>...</a></div>
                </div>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.DetailedStas %></div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/tracking.png" />
                        <%=Common.CutLongText(Resources.Lang.WeEmploys, 130) %>
                        <a class="more_button" href="FAQs.aspx#f5"><%=Resources.Lang.More %>...</a></div>
                </div>
            </td>
            <td />
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.OnlineSupport %></div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/support.png" />
                        <%=Common.CutLongText(Resources.Lang.OnlineSupportDetail, 130)%><a class="more_button"
                            href="OnlineSupport.aspx"><%=Resources.Lang.More %>...</a></div>
                </div>
            </td>
        </tr>
    </table>
</div>
