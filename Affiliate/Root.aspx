<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Root.aspx.cs" Inherits="Root" %>

<%@ Register Src="Controls/Home_Center.ascx" TagName="Home_Center" TagPrefix="uc3" %>

<%@ Register Src="Controls/Home_Left.ascx" TagName="Home_Left" TagPrefix="uc2" %>

<%@ Register Src="Controls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <uc3:Home_Center ID="Home_Center" runat="server" />
    </div>        
</asp:Content>


