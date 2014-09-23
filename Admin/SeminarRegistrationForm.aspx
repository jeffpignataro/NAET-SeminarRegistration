<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="SeminarRegistrationForm.aspx.vb" Inherits="SeminarRegistrationForm.AdminSeminarRegistrationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.maskedinput.min.js" type="text/javascript"></script>
    <script src="../Scripts/handlebars-v1.3.0.js"></script>
    <script type="text/javascript">
        function ValidateTextBox(source, args) {
            //Disable payment method and CC num validators if Manual or Check payment methods are chosen
            var paymentType = $($("#<%=rblPaymentMethod.ClientID%>")).find("input:radio:checked")[0].value.toLowerCase();
            if (paymentType == "manual" || paymentType == "check") {
                ValidatorEnable($("#<%=CustomValidator12.ClientID%>")[0], false);
                //ValidatorEnable($("#<%'=CustomValidator13.ClientID%>")[0], false);
            }


            var is_valid = $("#" + source.controltovalidate).val() != "";
            $("#" + source.controltovalidate).css("border-color", is_valid ? "#eeeeee" : "red");
            args.IsValid = is_valid;
        }

        function OptionHighlight() {
            $(".optionHighlight").parent().css("background-color", "#ccc");
        }

        function closeModal() {
            $("#dialog-modal").dialog("close");
        }

        function closeInnerModal() {
            $("#dialog-search-modal").dialog("close");
        }

        function setRefresherCheckBoxList() {
            var list = $("#<%= rblBasicSeminar.ClientID%>");
            var rdbtnLstValues = list.find("input");
            for (var i = 0; i < rdbtnLstValues.length; i++) {
                if (rdbtnLstValues[i].value == "refresh") {
                    $(rdbtnLstValues[i]).attr("checked", "checked");
                } else {
                    rdbtnLstValues[i].removeAttribute("checked");
                }
            }
            $("#rblBasicSeminarContainer").hide();
        }

        $(function () {
            $("#<%= tbYearAttendedBasic.ClientID%>").mask("9999");
            $("#<%= tbYearAttendedAdv1.ClientID%>").mask("9999");
            $("#<%= tbYearGraduated.ClientID%>").mask("9999");
            $("#<%= tbPhone.ClientID%>").mask("(999) 999-9999");
            $("#<%= tbMobile.ClientID%>").mask("(999) 999-9999");
            $("#<%= tbFax.ClientID%>").mask("(999) 999-9999");
            <%-- Removed mask because of non-US zip/postal codes
            $("#<%= tbZip.ClientID%>").mask("99999");--%>
            $("#<%= tbCreditCard.ClientID%>").mask("9999-9999-9999-999?9");

            $("#dialog-modal").dialog({
                height: 310,
                width: 250,
                modal: true,
                open: function () {
                    $('.ui-widget-overlay').addClass('custom-overlay');
                },
                close: function () {
                    $('.ui-widget-overlay').removeClass('custom-overlay');
                }
            });

            OptionHighlight();

            $('#btnDoctorLogin').click(function () {
                var multiParams = JSON.stringify({
                    doctorId: $("#tbDoctorId").val(),
                    lastName: $("#tbDoctorLastName").val()
                });

                $.ajax({
                    type: "POST",
                    url: 'SeminarRegistrationForm.aspx/FindDoctor',
                    async: false,
                    data: multiParams,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var json = $.parseJSON(data.d);
                        if (json.Error) {
                            //Failed
                            $("#msgLogin").text(json.ErrorMsg);
                            $("#msgLogin").show();
                            return;
                        }
                        //Success
                        $("#msgLogin").hide();
                        $("#loginSection").hide();
                        if (json.length > 1) {
                            var source = $("#entry-template").html();
                            var template = Handlebars.compile(source);
                            var context = { doctors: json };
                            var html = template(context);
                            $("#dialog-search-modal").append(html);

                            $('.btnSelectDoctor').click(function () {
                                var docKey = this.id;
                                $.ajax({
                                    type: "POST",
                                    url: 'SeminarRegistrationForm.aspx/FindDoctor',
                                    async: false,
                                    data: JSON.stringify({
                                            doctorId: docKey,
                                            lastName: ''
                                          }),
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (innerData) {
                                        var innerJson = $.parseJSON(innerData.d);
                                        if (innerJson.Error) {
                                            //Failed
                                            $("#msgLogin").text(json.ErrorMsg);
                                            $("#msgLogin").show();
                                            return;
                                        }
                                        //Success
                                        $("#msgLogin").hide();
                                        $("#loginSection").hide();
                                        $(".fName").val(innerJson.FirstName);
                                        $(".lName").val(innerJson.LastName);
                                        $(".Phone").val(innerJson.Phone);
                                        $(".Fax").val(innerJson.Fax);
                                        $(".Email").val(innerJson.Email);
                                        $(".DoctorKey").val(innerJson.DoctorKey);
                                    },
                                    fail: function () {
                                        alert("fail");
                                    }
                                });
                                closeInnerModal();
                            });

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
                        else {
                            //Returned json was not a list
                            //Should be a single record
                            //Empty resultset is disallowed in codebehind
                            $(".fName").val(json.FirstName);
                            $(".lName").val(json.LastName);
                            $(".Email").val(json.Email);
                            $(".DoctorKey").val(json.DoctorKey);
                            $(".Address").val(json.DoctorLocation.Address1);
                            $(".City").val(json.DoctorLocation.City);
                            $(".State").val(json.DoctorLocation.State);
                            $(".Zip").val(json.DoctorLocation.Zip);
                            if (json.DoctorLocation.Country == 'USA') {
                                $(".Country").val('UNITED STATES');

                            } else {
                                $(".Country").val(json.DoctorLocation.Country.toUpperCase);

                            }
                            //Phone and Fax fields are not formatted properly
                            //They contain more information than just phone numbers
                            //$(".Phone").val(json.DoctorLocation.Phone);
                            //$(".Fax").val(json.DoctorLocation.Fax);
                        }
                        closeModal();
                        setRefresherCheckBoxList();
                    },
                    fail: function () {
                        alert("fail");
                    }
                });
                //Clear textboxes after attempting login
                $("#tbDoctorId").val("");
                $("#tbDoctorLastName").val("");
            });
        });
    </script>
    <script id="entry-template" type="text/x-handlebars-template">
        <ul class="doctorList">
            {{#each doctors}}
                <li>
                    <button id="{{DoctorKey}}" class="btnSelectDoctor">Select</button>
                    {{DoctorKey}} - {{Username}} - {{FirstName}} {{LastName}} {{Title}}
                </li>
            {{/each}}
        </ul>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div id="wrapper">
        <div id="symheader">
            <h1>NAET Seminar Application Form</h1>
        </div>
        <div id="Content">
            <div id="dialog-modal" title="Doctor Search">
                <span id="loginSection">
                    <div class="row">
                        <span>Find Doctor by ID or Last Name</span>
                    </div>
                    <div class="row">
                        <span class="col1">Doctor ID</span>
                        <input type="text" id="tbDoctorId" placeholder="Doctor ID" class="col1" />
                    </div>
                    <div class="row">
                        <span class="col1">Last Name</span>
                        <input type="text" id="tbDoctorLastName" placeholder="Last Name" class="col1" />
                    </div>
                    <div class="row">
                        <span class="col1">
                            <!-- Spacer for Column 1 -->
                        </span>
                        <input type="button" id="btnDoctorLogin" value="Search" />
                        <div id="msgLogin" style="color: red; display: none;"></div>
                    </div>
                </span>
            </div>
            <div id="dialog-search-modal" title="Doctor Search">
            </div>
            <div class="row">
                <h4>Personal Information</h4>
                <span class="col1">First Name <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbfName" placeholder="Name" CssClass="col1 fName" />
                <asp:CustomValidator ID="CustomValidator1" runat="server"
                    ControlToValidate="tbfName" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="First Name"
                    ErrorMessage="First name is required." Display="None" />
                <span class="col2">Last Name <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tblName" placeholder="Name" CssClass="col2 lName" />
                <asp:CustomValidator ID="CustomValidator8" runat="server"
                    ControlToValidate="tblName" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Last Name"
                    ErrorMessage="Last name is required." Display="None" />
            </div>
            <div class="row">
                <span class="col1">Degree or Diploma <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbDegree" placeholder="Degree or Diploma" CssClass="col1" />
                <asp:CustomValidator ID="CustomValidator2" runat="server"
                    ControlToValidate="tbDegree" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Degree or Diploma"
                    ErrorMessage="Degree is required." Display="None" />
                <span class="col2">Year Graduated <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbYearGraduated" placeholder="Year Graduated" CssClass="col2" />
                <asp:CustomValidator ID="CustomValidator3" runat="server"
                    ControlToValidate="tbYearGraduated" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Year Graduated"
                    ErrorMessage="Year graduated is required." Display="None" />
            </div>
            <div class="row">
                <span class="col1">Copy of License<span class="req">*</span></span>
                <asp:FileUpload runat="server" ID="fuLicenseFile" />
                <div>
                    <asp:RequiredFieldValidator runat="server" ID="rfvLicenseFile" ControlToValidate="fuLicenseFile"
                        ErrorMessage="License is required." ForeColor="Red" ValidationGroup="SeminarRegistration" />
                </div>
            </div>
            <div class="row">
                <h4>NAET Information</h4>
                <span class="col1">Year Attended NAET Basic</span>
                <asp:TextBox runat="server" ID="tbYearAttendedBasic" placeholder="Year Attended NAET Basic" CssClass="col1" />
                <span class="col2">Year Attended NAET ADV-1</span>
                <asp:TextBox runat="server" ID="tbYearAttendedAdv1" placeholder="Year Attended" CssClass="col2" />
            </div>
            <div class="row">
                <span class="col1">Are you a NAET/NARF Member?</span>
                <asp:RadioButtonList runat="server" ID="rblNAETMember" RepeatDirection="Horizontal" CssClass="col1" Width="204px">
                    <Items>
                        <asp:ListItem Text="Yes" Value="true" Selected="True" />
                        <asp:ListItem Text="No" Value="false" />
                    </Items>
                </asp:RadioButtonList>
                <span class="col2">Who Referred You?</span>
                <asp:TextBox runat="server" ID="tbReferred" placeholder="Referrer" CssClass="col2" />
            </div>
            <div class="row">
                <h4>Address</h4>
                <span class="colFull">Business Address <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbAddress" placeholder="Business Address" CssClass="colFull Address" />
                <asp:CustomValidator ID="CustomValidator5" runat="server"
                    ControlToValidate="tbAddress" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Business Address"
                    ErrorMessage="Address is required." Display="None" />
            </div>
            <div class="row">
                <span class="col1">City <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbCity" placeholder="City" CssClass="col1 City" />
                <asp:CustomValidator ID="CustomValidator6" runat="server"
                    ControlToValidate="tbCity" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="City"
                    ErrorMessage="City is required." Display="None" />
                <span class="col2">State <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbState" placeholder="State" CssClass="col2 State" />
                <asp:CustomValidator ID="CustomValidator7" runat="server"
                    ControlToValidate="tbState" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="State"
                    ErrorMessage="State is required." Display="None" />
            </div>
            <div class="row">
                <span class="col1">Zip <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbZip" placeholder="Zip" CssClass="col1 Zip" />
                <asp:CustomValidator ID="CustomValidator4" runat="server"
                    ControlToValidate="tbZip" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Zip"
                    ErrorMessage="Zip is required." Display="None" />
                <span class="col2">Country <span class="req">*</span></span>
                <asp:DropDownList runat="server" ID="ddlCountry" CssClass="col2 Country" />

            </div>
            <div class="row">
                <span class="col1">Phone <span class="req">*</span></span>
                <asp:TextBox runat="server" ID="tbPhone" placeholder="Phone" CssClass="col1 Phone" />
                <asp:CustomValidator ID="CustomValidator10" runat="server"
                    ControlToValidate="tbPhone" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Phone"
                    ErrorMessage="Phone is required." Display="None" />
                <span class="col2">Mobile</span>
                <asp:TextBox runat="server" ID="tbMobile" placeholder="Mobile" CssClass="col2" />
            </div>
            <div class="row">
                <span class="col1">Fax</span>
                <asp:TextBox runat="server" ID="tbFax" placeholder="Fax" CssClass="col1 Fax" />
                <span class="col2">Email <span class="req">*</span>
                    <asp:RegularExpressionValidator runat="server" ID="RegExValidator1" ControlToValidate="tbEmail"
                        ValidationExpression="[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?" ErrorMessage="Invalid Email" ValidationGroup="SeminarRegistration" /></span>
                <asp:TextBox runat="server" ID="tbEmail" placeholder="Email" CssClass="col2 Email" />
                <asp:CustomValidator ID="CustomValidator11" runat="server"
                    ControlToValidate="tbEmail" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Email"
                    ErrorMessage="Email is required." Display="None" />
            </div>
            <div class="row">
                <span class="colFull">Special Considerations</span>
                <asp:TextBox runat="server" ID="tbSpecialConsiderations" TextMode="MultiLine" CssClass="colFull" />
            </div>
            <div class="row">
                <h4>Select all the seminars you would like to attend</h4>
            </div>
            <div class="row listContainer">
                <span class="col1">
                    <b>Basic Seminars</b>
                    <br />
                    <span class="req">Student agreement<br />
                        form required</span>
                    <br />
                    <br />
                    <div id="rblBasicSeminarContainer">
                        <asp:UpdatePanel runat="server" ID="upBasicSeminarPricing">
                            <ContentTemplate>
                                <asp:RadioButtonList runat="server" ID="rblBasicSeminar" AutoPostBack="True">
                                    <asp:ListItem Text="1st time: $660.00" Value="first" Selected="True" />
                                    <asp:ListItem Text="Refresher: $390.00" Value="refresh" />
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <strong>Student agreement form</strong>
                    <asp:FileUpload ID="fuBasicSeminar" runat="server" Width="180px" />
                    <asp:UpdatePanel runat="server" ID="upBasicSeminarFileUpload">
                        <ContentTemplate>
                            <asp:RequiredFieldValidator ID="rfvBasicSeminarFileUpload" runat="server" ControlToValidate="fuBasicSeminar" ErrorMessage="Student agreement form is required." ForeColor="Red" Enabled="False" ValidationGroup="SeminarRegistration" CssClass="colThreeQuarter" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
                <asp:UpdatePanel runat="server" ID="upBasicSeminar">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblBasicSeminar" Text="None currently Avaialable" Visible="False" CssClass="col1" />
                        <asp:CheckBoxList runat="server" ID="cblBasicSeminar" CssClass="col1" AutoPostBack="True" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <span class="col2">
                    <b>Advanced Level 1 Seminars</b>
                    <br />
                    <br />
                    <asp:UpdatePanel runat="server" ID="upAdvancedLevel1Pricing">
                        <ContentTemplate>
                            <asp:RadioButtonList runat="server" ID="rblAdvancedLevel1" AutoPostBack="True">
                                <asp:ListItem Text="1st time: $660.00" Value="first" Selected="True" />
                                <asp:ListItem Text="Refresher: $390.00" Value="refresh" />
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
                <asp:UpdatePanel runat="server" ID="upAdvancedLevel1">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblAdvancedLevel1" Text="None currently Avaialable" Visible="False" CssClass="col2" />
                        <asp:CheckBoxList runat="server" ID="cblAdvancedLevel1" CssClass="col2" AutoPostBack="True" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="row listContainer">
                <span class="col1">
                    <b>Advanced Level 2 and Case Management Seminars</b>
                    <br />
                    <br />
                    Both Days: $450.00
                    <br />
                    Single Day: $225.00
                </span>
                <asp:UpdatePanel runat="server" ID="upAdvancedLevel2AndCaseManage" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblAdvancedLevel2AndCaseManage" Text="None currently Avaialable" Visible="False" CssClass="colThreeQuarter" />
                        <asp:CheckBoxList runat="server" ID="cblAdvancedLevel2AndCaseManage" CssClass="colThreeQuarter" AutoPostBack="True" RepeatColumns="3" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="row listContainer">
                <span class="col1">
                    <b>NST Instructor’s class</b>
                    <br />
                    <br />
                    $210.00
                </span>
                <asp:UpdatePanel runat="server" ID="upNst">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="lblNst" Text="None currently Avaialable" Visible="False" CssClass="col2" />
                        <asp:CheckBoxList runat="server" ID="cblNst" CssClass="col2" AutoPostBack="True" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <h4>Payment Information</h4>
                <span class="col1">Payment Method <span class="req">*</span>
                </span>
                <asp:RadioButtonList runat="server" ID="rblPaymentMethod" CssClass="col1">
                    <Items>
                        <asp:ListItem Text="Check" Value="Check" Selected="True" />
                        <asp:ListItem Text="Manual" Value="Manual" />
                        <asp:ListItem Text="Visa" Value="Visa" />
                        <asp:ListItem Text="MasterCard" Value="MasterCard" />
                        <asp:ListItem Text="American Express" Value="AmericanExpress" />
                        <asp:ListItem Text="Discover" Value="Discover" />
                    </Items>
                </asp:RadioButtonList>
                <span class="col2">Credit Card # <span class="req">*</span>
                </span>
                <asp:TextBox runat="server" ID="tbCreditCard" placeholder="Credit Card Number" CssClass="col2" />
                <asp:CustomValidator ID="CustomValidator12" runat="server"
                    ControlToValidate="tbCreditCard" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Credit Card Number"
                    ErrorMessage="Credit card number is required." Display="None" />
                <span class="col2">Exp. Date <span class="req">*</span>
                </span>
                <asp:DropDownList runat="server" ID="ddlExpDateMonth" CssClass="col2Small">
                    <Items>
                        <asp:ListItem Text="01" Value="01" />
                        <asp:ListItem Text="02" Value="02" />
                        <asp:ListItem Text="03" Value="03" />
                        <asp:ListItem Text="04" Value="04" />
                        <asp:ListItem Text="05" Value="05" />
                        <asp:ListItem Text="06" Value="06" />
                        <asp:ListItem Text="07" Value="07" />
                        <asp:ListItem Text="08" Value="08" />
                        <asp:ListItem Text="09" Value="09" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="11" Value="11" />
                        <asp:ListItem Text="12" Value="12" />
                    </Items>
                </asp:DropDownList>
                <asp:DropDownList runat="server" ID="ddlExpDateYear" CssClass="col2Small" />
                <span class="col2">CSC Code <%--<span class="req">*</span>--%>
                </span>
                <asp:TextBox runat="server" ID="tbCCV" placeholder="CSC Code" CssClass="col2" />
                <%--<asp:CustomValidator ID="CustomValidator13" runat="server"
                    ControlToValidate="tbCCV" ClientValidationFunction="ValidateTextBox"
                    ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="CSC Code"
                    ErrorMessage="CSC code is required." Display="None" />--%>
            </div>
            <div class="row">
                <span class="col1">
                    <!-- Spacer for Column 1 -->
                </span>
                <span class="col1">
                    <!-- Spacer for Column 1 -->
                </span>
                <span class="col2"><b>Total</b>
                </span>
                <b>
                    <asp:UpdatePanel runat="server" ID="upTotal">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="lblTotal" CssClass="col2" Text="$0.00" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </b>
            </div>
            <div class="row">
                <asp:Label runat="server" ID="lblErr" ForeColor="Red" CssClass="colThreeQuarter" />
                <asp:ValidationSummary runat="server" ID="validationSummary" HeaderText="The following errors occurred:" DisplayMode="List" CssClass="colThreeQuarter" ValidationGroup="SeminarRegistration" ShowSummary="True" ShowMessageBox="False" />
                <asp:Button runat="server" ID="btnSubmit" Text="Pay Now" ValidationGroup="SeminarRegistration" CssClass="SubmitBtn col2" />
                <asp:TextBox runat="server" ID="tbDoctorKey" CssClass="DoctorKey" Style="display: none;" />
            </div>
            <div class="row">
                <!-- Spacer to force footer to the bottom of page -->
            </div>
        </div>
    </div>
</asp:Content>
