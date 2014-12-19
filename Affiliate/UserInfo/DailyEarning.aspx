<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DailyEarning.aspx.cs" Inherits="DailyEarning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
     <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="144" class="in_top"><asp:Literal runat="server" ID="ltrDailyEarning"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <div style="height: 5px;"></div>
                <b><a href="../UserInfo/StatementHistory.aspx"><%= Resources.Lang.StatementHistory%></a></b>
                <hr style="display: block; height: 1px; border: 0; border-top: 1px solid #ccc;" />
                <form method="get" id="frmData" name="frmData">
                    <%=strListSports %>                    
                    <input type="hidden" id="monthdate" name="monthdate" value="<%=Request["monthdate"] %>" />
                </form>
                <asp:Literal runat="server" ID="ltrViewDaily"></asp:Literal>
            </div>
        </div>
    </div>    
</asp:Content>
