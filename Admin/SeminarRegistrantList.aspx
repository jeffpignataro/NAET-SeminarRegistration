<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="SeminarRegistrantList.aspx.vb" Inherits="SeminarRegistrationForm.SeminarRegistrantList" EnableEventValidation="false" %>

<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Register Src="~/usercontrols/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/handlebars-v1.3.0.js"></script>
    <script type="text/javascript">
        //Global Vars
        var seminarRegistrationId;
        var senderId;

        //*****************************
        //Start document-wide functions
        //*****************************
        $(function () {
            $(".SeminarRegistrantList").addClass("active");
        });

        $(document).on("click", ".btnSelectDoctor", function () {
            var existingDoctorRecordId = $(this).attr("id");
            applyDoctorToExistingRecord(existingDoctorRecordId, seminarRegistrationId);
        });
        //***************************
        //End document-wide functions
        //***************************

        //***********************
        //Start utility functions
        //***********************
        function cleanString(str) {
            //May need other characters cleaned
            return str.replace("'", "");
        }

        function showDialog() {
            $("#dialog-search-modal").dialog({
                height: 'auto',
                width: 'auto',
                modal: true,
                open: function () {
                    $('.ui-widget-overlay').addClass('custom-overlay');
                },
                close: function () {
                    $('.ui-widget-overlay').removeClass('custom-overlay');
                }
            });
        }
        function closeDialog() {
            $("#dialog-search-modal").dialog("close");
        }
        //*********************
        //End utility functions
        //*********************

        //********************
        //Start AJAX functions
        //********************
        function CheckForDoctorRecord(sender, json) {
            senderId = $(sender).attr("id");

            seminarRegistrationId = $(sender).next("input[type=hidden]").val();

            $.ajax({
                type: "POST",
                url: "Ajax.aspx/CheckForDoctorRecord",
                data: "{'jsonString':'" + cleanString(json) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: CheckForDoctorRecordSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function applyDoctorToExistingRecord(existingDoctorId, seminarRegistrationId) {
            //alert("Existing Doc ID:" + existingDoctorId);
            //alert("Seminar Reg ID:" + seminarRegistrationId);
            $.ajax({
                type: "POST",
                url: "Ajax.aspx/UpdateDoctorIdInSeminarRegistration",
                data: "{'doctorId':'" + existingDoctorId + "', 'seminarRegistrationId' : " + seminarRegistrationId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: applyDoctorToExistingRecordSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function addDoctorAsNew() {
            //alert("Seminar Reg ID:" + seminarRegistrationId);
            $.ajax({
                type: "POST",
                url: "Ajax.aspx/AddDoctorAsNew",
                data: "{'seminarRegistrationId' : " + seminarRegistrationId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    addDoctorAsNewSuccess(response, seminarRegistrationId);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        //******************
        //End AJAX functions
        //******************


        //****************************
        //Start AJAX Success functions
        //****************************
        function applyDoctorToExistingRecordSuccess(response) {
            $("#" + senderId).hide();
            //__doPostBack(senderId, '');
            closeDialog();
        }
        function CheckForDoctorRecordSuccess(response) {
            var json = response.d;
            if (json == null) {
                $.when(
                    addDoctorAsNew()
                ).done(function () {
                    $("#dialog-search-modal").html("<p>Doctor record successfully added.</p><p style='text-align:center;'><button onClick='closeDialog();'>Close</button></p>");
                    showDialog();
                });
                return false;
            }
            var source = $("#entry-template").html();
            var template = Handlebars.compile(source);
            var context = { doctors: $.parseJSON(json) };
            var html = template(context);
            $("#dialog-search-modal").html(html);
            showDialog();
        }
        function addDoctorAsNewSuccess(response, seminarRegistrationId) {
            var json = $.parseJSON(response.d);
            applyDoctorToExistingRecord(json.doctorId, seminarRegistrationId);
            $("#" + senderId).hide();
        }
        //**************************
        //End AJAX Success functions
        //**************************
    </script>
    <script id="entry-template" type="text/x-handlebars-template">
        After searching the doctor name there are possible duplicate doctor records.
        <br />
        Please review the list below and select the appropriate record.
        <ul class="doctorList">
            {{#each doctors}}
                <li>
                    <button id="{{DoctorKey}}" class="btnSelectDoctor">Select</button>
                    {{DoctorKey}} - {{Username}} - {{FirstName}} {{LastName}} {{Title}}
                </li>
            {{/each}}
        </ul>
        Didn't find the doctor record you are looking for?<br />
        <button id="btnAddNewDoctor">Create New Doctor Record</button>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <uc1:AdminMenu runat="server" ID="AdminMenu" />
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
                <asp:HiddenField runat="server" ID="hdnDdlCourseType" />
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
                        <asp:HiddenField runat="server" ID="hdnDdlCourseDate" />
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
                        <asp:HiddenField runat="server" ID="hdnDdlRegistrantStatusFilter" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="exportExcel">
                <span>Export:</span><asp:ImageButton runat="server" ImageUrl="~/images/excel-export.png" ID="btnExportExcel" />
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
                                        <asp:Button Text="Add"
                                            runat="server"
                                            ID="btnAddLineItem"
                                            CommandName="btnAddLineItem"
                                            CommandArgument='<%# Container.DataItemIndex %>'
                                            Visible="False"
                                            CssClass="btnAddLineItem"
                                            OnClientClick='<%# String.Format("CheckForDoctorRecord(this, ""{0}""); return false;", HttpUtility.JavaScriptStringEncode(JsonConvert.SerializeObject(Container.DataItem)))%>' />
                                        <asp:HiddenField runat="server" ID="hdnDoctorKey" Value='<%# Bind("SeminarRegistrationId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="dialog-search-modal" title="Doctor Search"></div>
</asp:Content>
