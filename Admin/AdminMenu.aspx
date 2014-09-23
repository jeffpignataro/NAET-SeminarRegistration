<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="AdminMenu.aspx.vb" Inherits="SeminarRegistrationForm.AdminMenu"
    Title="Payments Admin" %>

<%@ Register Src="~/usercontrols/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>


<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:AdminMenu runat="server" ID="AdminMenu" />
    <div id="wrapper">
        <div id="Content">
            <div style="float: left;">
                <asp:Button ID="btnListPayments" runat="server" Text="List Payments" />
            </div>
            <div style="float: left;">
                <asp:Button ID="btnListCourses" runat="server" Text="List Registrants by Course" />
            </div>
            <div style="float: left;">
                <asp:Button ID="btnRegForm" runat="server" Text="Registration Form" />
            </div>
        </div>
    </div>
</asp:Content>
