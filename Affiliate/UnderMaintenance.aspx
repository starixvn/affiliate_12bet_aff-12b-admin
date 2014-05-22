<%@ Page Language="C#" AutoEventWireup="false" EnableViewState="false" CodeFile="UnderMaintenance.aspx.cs"
    Inherits="UnderMaintenance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Under Maintenance</title>
    <meta http-equiv="refresh" content="30">
    <script src="App_Js/Common.js" type="text/javascript"></script>

    <script src="App_Js/prototype.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>
    
    <style type="text/css">
        body
        {
            background-image: url(App_Themes/Default/Images/L_bg.gif);
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            background-repeat: repeat-x;
            background-color: #6d0001;
        }
        #footer
        {
            position: relative;
            z-index: 1;
            left: 20px;
            width: 200px;
            top: 20px;
        }
        .txt
        {
            font-size: 11px;
            color: #9f7070;
            font-family: Arial, Helvetica, sans-serif;
        }
        .bg
        {
            background-repeat: repeat-x;
        }
        a:link
        {
            color: #ff5a00;
        }
        a:visited
        {
            color: #ff5a00;
        }
        a:hover
        {
            color: #002aa0;
        }
        h1
        {
            font-family: Arial, Helvetica;
            font-size: 18px;
            font-weight: bold;
            color: #ff5a00;
            _padding-top: 20px; *padding-top:20px;line-height:25px;
        }
        .cont
        {
            font-size: 14px;
            color: #000000;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            line-height: 20px;
        }
        .cont2
        {
            font-family: Arial, Helvetica, "新細明體";
            font-size: 14px;
            font-weight: bold;
            color: #ff5a00;
        }
    </style>
</head>
<body>
    <table width="702" align="center" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <img src="App_Themes/Default/images/um_head.gif" width="702" height="90" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#fd0000" height="5">
                </td>
            </tr>
            <tr>
                <td style="padding: 20px;" bgcolor="#e4e4e4">
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        Under Maintenance</h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    We are having server maintenance, website will be unavailable until <span class="cont2">
                                        <%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    Please accept our apologies for the inconvenience caused!
                                    <br>
                                    To reach customer service, please <a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=English&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">click here</a> or email to

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr style="border-top: 1px dotted rgb(153, 153, 153);" noshade="noshade" size="1">
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        伺服器维护</h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    本公司正进行伺服器维护，时间至 <span class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    所有网页将无法登入，不便之处有请见谅！<br>
                                    如需要在线帮助，有请<a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=Chinese&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">点击这里</a>或邮件至

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr style="border-top: 1px dotted rgb(153, 153, 
153);" noshade="noshade" size="1">
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        伺服器維護</h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    本公司正進行伺服器維護，時間至 <span class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    所有網頁將無法登入，不便之處 有請見諒！<br>
                                    如需要在線幫助，有請<a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=Chinese&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">點擊這裡</a>或郵件至

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr style="border-top: 1px dotted rgb(153, 153, 153);" noshade="noshade" size="1">
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        ปรับปรุงระบบ
                                    </h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    ทางเวบไซด์มีการปรับปรุงระบบขณะนี้ และไม่สามารถเข้าชมหรือใช้งานได้ไปจนถึงเวลา <span
                                        class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    ขออภัยในความไม่สะดวก คะ!
                                    <br>
                                    ติอต่อฝ่ายบริการลูกค้าเราได้ที่ <a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=Thai&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">ห้องสนทนาสด</a> หรือ อีเมลล์

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr style="border-top: 1px dotted rgb(153, 153, 153);" noshade="noshade" size="1">
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        Hệ thống bảo trì
                                    </h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    Hệ thống công ty chúng tôi đang trong thời gian bảo trì đến <span class="cont2">
                                        <%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    Thành thật xin lỗi những bất tiện gây ra. 
                                    <br>
                                    Yêu cầu dịch vụ khách hàng, xin vui lòng <a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=English&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">Nhấp vào đây</a> hoặc gửi Email đến

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr style="border-top: 1px dotted rgb(153, 153, 153);" noshade="noshade" size="1">

                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        Bajo Mantenimiento</h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    Tenemos mantenimiento de servidor, el sitio web estará indisponible hasta que <span
                                        class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    Acepta por favor nuestras disculpas para el inconveniente causado"!
                                    <br>
                                    Para alcanzar servicio de atención al cliente, por favor <a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=English&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">clic aquí</a> o correo electrónico a

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr style="border-top: 1px dotted rgb(153, 153, 
153);" noshade="noshade" size="1">
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="55">
                                    <img src="App_Themes/Default/Images/note_icon.gif" width="46" height="46" />
                                </td>
                                <td>
                                    <h1>
                                        Under Maintenance
                                    </h1>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="cont">
                                    Kami sedang melakukan server maintenance, website tidak tersedia hingga pukul <span
                                        class="cont2"><%= UMTime_To%> (HK Time GMT +8).</span><br>
                                    Mohon maaf untuk ketidaknyamanan ini!<br>
                                    Untuk menghubungi Customer Service, mohon <a href="http://server.iad.liveperson.net/hc/69174729/?cmd=file&amp;file=visitorWantsToChat&amp;site=69174729&amp;SESSIONVAR%21skill=Indonesia&amp;imageUrl=http://server.iad.liveperson.net/hcp/Gallery/ChatButton-Gallery/English/General/1a&amp;referrer=http%3A//www.12bet.com/homeleft.aspx"
                                        target="_blank">klik disini</a> atau email ke

                                    <a href="mailto:support@12CSD.com?subject=Enquiries%20sent%20from%2012BET%20website">
                                        support@12CSD.com</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br>
                </td>
            </tr>
            <tr>
                <td class="txt" align="center" height="40">
                    © Copyright <%= DateTime.Now.Year %> Pacific Sea Invests
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
