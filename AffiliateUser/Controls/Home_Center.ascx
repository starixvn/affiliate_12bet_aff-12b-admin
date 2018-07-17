<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Home_Center" CodeBehind="Home_Center.ascx.cs" %>
<link href="Content/_banner.min.css" rel="stylesheet" />

<div id="banner_main">
    <!-- Slideshow container -->
    <div class="slideshow-container">
        <!-- Full-width images with number and caption text -->
        <div class="mySlides fade" onmouseover="Banner.handleMouseOver()" onmouseout="Banner.handleMouseOut()">
            <a href="javacript:;" id="bannerSignup">
                <img src="<%=Common.GetBannerFlash(1) %>" style="width: 100%">
            </a>
        </div>

        <div class="mySlides fade" onmouseover="Banner.handleMouseOver()" onmouseout="Banner.handleMouseOut()">
            <a href="javascript:;" id="bannerMedia">
                <img src="<%=Common.GetBannerFlash(2) %>" style="width: 100%">
            </a>
        </div>

        <!-- Next and previous buttons -->
        <a class="prev" onclick="Banner.plusSlides(-1)">&#10094;</a>
        <a class="next" onclick="Banner.plusSlides(1)">&#10095;</a>
    </div>
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
                            href="ProgramDetails.aspx#h1"><%=Resources.Lang.More %>...</a>
                    </div>
                </div>
            </td>
            <td style="width: 2%;" />
            <td style="width: 49%;">
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.NegativeBalance %>
                    </div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/negative.png" />
                        <%=Common.CutLongText(Resources.Lang.AnyNegativeBalance, 130)%>
                        <a class="more_button"
                            href="ProgramDetails.aspx#h2"><%=Resources.Lang.More %>...</a>
                    </div>
                </div>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.WideRange %>
                    </div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/payment.png" />
                        <%=Common.CutLongText(Resources.Lang.YouCanRequestPayout, 130)%>
                        <a class="more_button"
                            href="ProgramDetails.aspx#h3"><%=Resources.Lang.More %>...</a>
                    </div>
                </div>
            </td>
            <td />
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.PromptMonthly %>
                    </div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/prompt.png" />
                        <%=Common.CutLongText(Resources.Lang.AMaximumOfOneWithdrawal, 130)%>
                        <a class="more_button" href="FAQs.aspx#h4"><%=Resources.Lang.More %>...</a>
                    </div>
                </div>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.DetailedStas %>
                    </div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/tracking.png" />
                        <%=Common.CutLongText(Resources.Lang.WeEmploys, 130) %>
                        <a class="more_button" href="FAQs.aspx#f5"><%=Resources.Lang.More %>...</a>
                    </div>
                </div>
            </td>
            <td />
            <td>
                <div class="box_conts">
                    <div class="box_conts_header">
                        <%=Resources.Lang.OnlineSupport %>
                    </div>
                    <div class="box_conts_line"></div>
                    <div class="box_conts_detail">
                        <img height="71" width="78" align="left" class="img_conts" src="<%=AppSettings.ThemeDir %>/images/support.png" />
                        <%=Common.CutLongText(Resources.Lang.OnlineSupportDetail, 130)%><a class="more_button"
                            href="OnlineSupport.aspx"><%=Resources.Lang.More %>...</a>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>

<script src="App_Js/banner.min.js"></script>
