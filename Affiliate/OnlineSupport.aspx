<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="OnlineSupport.aspx.cs" Inherits="OnlineSupport" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="350" class="in_top">
                            <%=Resources.Lang.OnlineSupport%>
                        </td>
                        <td class="in_right">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="in_conts">
                <div class="text">
                    <%=Resources.Lang.OnlineSupportDetail%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

