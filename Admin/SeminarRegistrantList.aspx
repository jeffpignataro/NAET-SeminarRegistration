<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="SeminarRegistrantList.aspx.vb" Inherits="SeminarRegistrationForm.SeminarRegistrantList" EnableEventValidation="false" %>

<%@ Import Namespace="SeminarRegistrationForm" %>
<%@ Register Src="~/usercontrols/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".SeminarRegistrantList").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <uc1:AdminMenu runat="server" id="AdminMenu" />
    <div id="wrapper">
        <div id="Content">
            <div class="fr" style="text-align: right;">
                Mark all as:<br />
                <asp:LinkButton runat="server" ID="lnkMarkAllUnapproved" Text="Unapproved" /><br />
                <asp:LinkButton runat="server" ID="lnkMarkAllApproved" Text="Approved" /><br />
                <asp:LinkButton runat="server" ID="lnkMarkAllAttended" Text="Attended" /><br />
                <asp:Button runat="server" ID="btnUpdateBulk" Text="Update All Items" />
            </div>
            <div>
                Course type:
                    <asp:DropDownList runat="server" ID="ddlCourseType" AppendDataBoundItems="True" AutoPostBack="True">
                        <Items>
                            <asp:ListItem Text="Select One" Value="0" />
                        </Items>
                    </asp:DropDownList>
            </div>
            <div>
                <asp:UpdatePanel runat="server" ID="upDdlCourseDate">
                    <ContentTemplate>
                        Course date:
                            <asp:DropDownList runat="server" ID="ddlCourseDate" AppendDataBoundItems="true" AutoPostBack="True">
                                <Items>
                                    <asp:ListItem Text="Select One" Value="0" />
                                </Items>
                            </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div>
                <asp:UpdatePanel runat="server" ID="upDdlRegistrantStatusFilter">
                    <ContentTemplate>
                        Show only:
                            <asp:DropDownList runat="server" ID="ddlRegistrantStatusFilter" AppendDataBoundItems="true" AutoPostBack="True">
                                <Items>
                                    <asp:ListItem Text="All" Value="0" />
                                </Items>
                            </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="exportExcel">
                <span>Export:</span><asp:ImageButton runat="server" ImageUrl="~/images/excel-export.png" ID="btnExportExcel"/>
            </div>
            <div>
                <asp:UpdatePanel runat="server" ID="upRegistrantList">
                    <ContentTemplate>
                        <asp:GridView ID="gvCourseRegistrants" runat="server" AutoGenerateColumns="False" DataKeyNames="SeminarRegistrantId, SeminarRegistrationId, RegistrantStatusId, SeminarId, DoctorId" Width="100%">
                            <AlternatingRowStyle BackColor="LightGrey" />
                            <Columns>
                                <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                <asp:BoundField DataField="TotalAmount" HeaderText="Amount Paid" DataFormatString="${0:#,0}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlRegistrantStatusId" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Edit" runat="server" ID="btnEditLineItem" CommandName="btnEditLineItem" CommandArgument='<%# Container.DataItemIndex%>' />
                                        <asp:Button Text="Update" runat="server" ID="btnUpdateLineItem" CommandName="btnUpdateLineItem" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
