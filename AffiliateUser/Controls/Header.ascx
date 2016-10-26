<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Header" Codebehind="Header.ascx.cs" %>

<script type="text/javascript" language="javascript">  
    var gmt_offset = '<%=gmt %>';
    var year = <%=year %>;
    var month = <%=month %>;
    var day = <%=day %>;
    var hrs = <%=hour %>;
    var min = <%=minute %>;
    var sec = <%=second %>;
    var strMonth = new Array('<%=Resources.Lang.Jan %>', '<%=Resources.Lang.Feb %>', '<%=Resources.Lang.Mar %>', '<%=Resources.Lang.Apr %>', '<%=Resources.Lang.May_short %>', '<%=Resources.Lang.Jun %>', '<%=Resources.Lang.Jul %>', '<%=Resources.Lang.Aug %>', '<%=Resources.Lang.Sept %>', '<%=Resources.Lang.Oct %>', '<%=Resources.Lang.Nov %>', '<%=Resources.Lang.Dec %>');
    var lang = {
        home:'<%=Resources.Lang.Home %>',
        signup:'<%=Resources.Lang.SignUp %>',
        prodetail:'<%=Resources.Lang.ProgDetails %>',
        faqs:'<%=Resources.Lang.Faqs %>',
        contact:'<%=Resources.Lang.Contact %>',
        aboutus:'<%=Resources.Lang.AboutUs %>'
    };
    menuItem = '';  
    <%=strErrMsg %>

    function PreInit() {
        clock();
        setInterval(clock, 1000);
        <%=strLogin %>
    }

    setTimeout(function() {
        PreInit();
    }, 1000);
   
    function isDate(TDY, TDM, TDD) 
    {
        var BegDate = new Date(parseFloat(TDY) + "/" + TDM + "/" + TDD);
        if (BegDate.getYear() < 200)
            var TBegYear = BegDate.getYear() + 1900
        else var TBegYear = BegDate.getYear()
        if (TBegYear != parseFloat(TDY) || BegDate.getMonth() + 1 != parseFloat(TDM) || BegDate.getDate() != parseFloat(TDD))
            return false;
        else return true;
    }

    function clock() 
    {
        sec++;
        if (sec == 60) {
            sec = 0;
            min++;
        }
        if (min == 60) {
            min = 0;
            hrs++;
        }
        if (hrs == 24) {
            hrs = 0;
            sec = 0;
            min = 0;
            day++;
        }
        if (min < 10) {
            strmin = "0" + min;
        }
        else {
            strmin = min;
        }
        if (sec < 10) {
            strsec = "0" + sec;
        }
        else {
            strsec = sec;
        }

        if (!isDate(year, month, day))   
        {
            month++;
            day = 1;
        }
        if (!isDate(year, month, day))   
        {
            year++;
            month = 1;
            day = 1;
        }

        if (hrs < 10) {
            strhrs = "0" + hrs;
        }
        else {
            strhrs = hrs;
        }
        var str = strhrs + ":" + strmin + ":" + strsec + " " +  day +  "-" + strMonth[month - 1] + "-" + year + gmt_offset;
        $('lblDateTime').innerHTML = str;
    }
    function Goto(url)
    {
        document.location.href = url;
    }
    
    var flagSrc = '';
    function ChangeLanguage(InLang)
    {
        var lang = 'en-US';
        if (InLang!=null){
            lang = InLang;
            Common.setCookie("afffavoritelang",lang, 30);
        }
        
        var d=new Date();
        var _ajax= new Ajax.Request(ErrMsg.RootUrl + "/Handler/SetLanguage.ashx",{
            asynchronous:true,
            method:'get',        
            parameters:'lang=' + lang + '&d=' + d.getMilliseconds(),//add "d" param to prevent cache in IE
            onComplete:UpdateLangSuccess            
        });           
        
    }
    
    function UpdateLangSuccess(data)
    {
        var val = data.responseText;  
        if(val != "")
        {  
            var url = document.location.href;
            if(url.indexOf("#") >= 0)
                url = url.substr(0, url.indexOf("#"));
            document.location.href = url;
        }        
    }
    //Kick out
    function KickedOut() {
       
       var ajax= new Ajax.Request(ErrMsg.RootUrl + "/Authorization/ReCheckIn.ashx",{
                method:'get',              
                onComplete:function(datastran)
                {
                    if(datastran.responseText=='Expired')
                    {
                        alert("Another session has logged in or your session is expired. Force to logout!");
                        window.location.href = ErrMsg.RootUrl + "/Authorization/Logout.ashx";
                    }
                    else if(datastran.responseText=='UM')
                    {
                        window.location.href = ErrMsg.RootUrl + "/UnderMaintenance.aspx";
                    }
                }
            }); 
    }
    function ChangeCapchaCode(img){
        var d=new Date();
        var t_date = d.getDate();      // Returns the day of the month
        var t_mon = d.getMonth();      // Returns the month as a digit
        var t_year = d.getFullYear();  // Returns 4 digit year
        var t_hour = d.getHours();     // Returns hours
        var t_min = d.getMinutes();    // Returns minutes
        var t_sec = d.getSeconds();    // Returns seocnds
        var t_mil = d.getMilliseconds();  
        img.src='Authorization/LoginCaptcha.ashx?v=' + t_date + t_mon + t_year + t_hour + t_min + t_sec + t_mil;       
    }
    
    function ShowLangBox(){
        $("langList").style.display="";
        $("arrow").className="arrDown";
    }
    function HideLangBox(){
        $("langList").style.display="none";
        $("arrow").className="arrUp";
    }

</script>

<div id="topPan" style="z-index: 101">
    <div class="logo_<%=AppSettings.Session.Language %>">
        <a href="#"></a>
    </div>
    <div class="lang">
        <div id="language">
            <div style="float: right; margin-top: 3px;" onmouseover="ShowLangBox()" onmouseout="HideLangBox()">
                <div id="langSelectLink">
                    <div id="box">
                        <span class="arrUp" id="arrow">&nbsp;</span> <span class="<%=langClass %>" id="selectedLang">
                            <%=selectedLang %></span>
                    </div>
                </div>
                <div id="langList" style="background-position: 0pt 0pt; display: none;">
                    <div class="langListTop">
                    </div>
                    <div class="langListRp">
                        <li><a href="javascript:ChangeLanguage('en-US');" class="fEng">English</a></li>
                        <li><a href="javascript:ChangeLanguage('zh-CN');" class="fChinese">中文简体</a></li>
                        <li><a href="javascript:ChangeLanguage('zh-TW');" class="fChinese">中文繁体</a></li>
                        <li><a href="javascript:ChangeLanguage('id-ID');" class="fIndo">Indonesia</a></li>
                        <li><a href="javascript:ChangeLanguage('vi-VN');" class="fTV">Tiếng Việt</a></li>
                        <li><a href="javascript:ChangeLanguage('th-TH');" class="fThai">ภาษาไทย</a></li>
                        <li><a href="javascript:ChangeLanguage('ko-KR');" class="fKr">한국어</a></li>
                        <li><a href="javascript:ChangeLanguage('es-ES');" class="fSpain">Español</a></li>
                        <li><a href="javascript:ChangeLanguage('ja-JP');" class="fJapan">日本</a></li>
                        <li><a href="javascript:ChangeLanguage('hi-IN');" class="fIndia">Republic of India</a></li>
                    </div>
                    <div class="langListBottom">
                    </div>
                </div>
            </div>
            <table style="float: right; padding-right: 5px; margin-top: 3px; display: <%=LogoutDisplay%>"
                cellpadding="0" cellspacing="0">
                <td class="logoutl">
                    &nbsp;
                </td>
                <td class="logoutr">
                    <a class="logout" id="A1" runat="server" href="~/Authorization/LogOut.ashx">
                        <%=Resources.Lang.Logout%></a>
                </td>
            </table>
        </div>
        <div id="datetime">
            <div class="clock" id="lblDateTime">
            </div>
        </div>
        <asp:Literal ID="MainNavHtml" runat="server"></asp:Literal>
    </div>
</div>
<div class="red_line">
</div>
<div class="clear">
</div>
<div class="hotNews">
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td width="36" class="hotNewHeader">
                <div class="icons">
                    &nbsp;</div>
            </td>
            <td width="100" class="hotNewHeader">
                <span>
                    <%=Resources.Lang.HotNews%></span>
            </td>
            <td class="leftHN" width="20">
                &nbsp;
            </td>
            <td class="rpHN">
                <div style="overflow: hidden; position: relative; width: 680px;" id="text_news">
                    <div style="position: relative; left: 750px; white-space: nowrap;" id="scroller">
                        <%=hotnews%></div>
                </div>
            </td>
            <td class="rightHN" width="36">
            </td>
            <td width="100" class="hotNewHeader">
                <span><a href="<%=Page.ResolveUrl("~/UserInfo/Message.aspx") %>" style="color:White"><%=Resources.Lang.ViewMore %></a></span>
            </td>
            <td width="20" class="hotNewHeader">
                <div class="bullet">
                    &nbsp;</div>
            </td>
        </tr>
    </table>
</div>
<div class="clear">
</div>


