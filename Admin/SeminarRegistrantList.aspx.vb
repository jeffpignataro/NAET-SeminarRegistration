Imports System.Linq
Imports System.Web.Services
Imports System.IO
Imports System.ComponentModel
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common

Public Class SeminarRegistrantList
    Inherits Page

    Private Sub SeminarRegistrantList_Init(sender As Object, e As EventArgs) Handles Me.Init
        AuthenticationHelper.ValidateAdminWithRedirect(Session("username"), Session("password"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            BindDropDownLists()
        End If
    End Sub

    Private Sub BindDropDownLists()

        ClearDropDownListItems(ddlCourseType)
        ddlCourseType.DataSource = EnumHelper.EnumToDictionary(GetType(EnumHelper.CourseType))
        ddlCourseType.DataTextField = "value"
        ddlCourseType.DataValueField = "key"
        ddlCourseType.DataBind()

        ddlRegistrantStatusFilter.DataSource = EnumHelper.EnumToDictionary(GetType(EnumHelper.RegistrantStatus))
        ddlRegistrantStatusFilter.DataTextField = "value"
        ddlRegistrantStatusFilter.DataValueField = "key"
        ddlRegistrantStatusFilter.DataBind()

        Dim course As New Course
        Dim getCourses As List(Of Course) = course.GetCourses().Where(Function(c) c.DateTime > Now.AddMonths(-2)).ToList()
        ddlCourseDate.DataSource = getCourses.OrderByDescending(Function(c) c.DateTime)
        ddlCourseDate.DataValueField = "Id"
        ddlCourseDate.DataTextField = "FormattedDateTime"
        ddlCourseDate.DataBind()
    End Sub

    Protected Sub ddlCourseType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseType.SelectedIndexChanged
        Dim course As New Course
        Dim getCourses As New List(Of Course)
        If (ddlCourseType.SelectedValue <> 0) Then
            getCourses = course.GetCoursesByType(ddlCourseType.SelectedValue)
        Else
            'If no course type is selected than get all course
            getCourses = course.GetCourses().Where(Function(c) c.DateTime > Now.AddMonths(-2)).ToList()
        End If
        ClearDropDownListItems(ddlCourseDate)
        ddlCourseDate.DataSource = getCourses.OrderByDescending(Function(c) c.DateTime)
        ddlCourseDate.DataValueField = "Id"
        ddlCourseDate.DataTextField = "FormattedDateTime"
        ddlCourseDate.DataBind()
    End Sub

    Public Sub ClearDropDownListItems(ByVal dropDownList As DropDownList)
        dropDownList.Items.Clear()
        dropDownList.Items.Add(New ListItem("Select One", "0"))
    End Sub

    Protected Sub ddlCourseDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseDate.SelectedIndexChanged
        BindCourseRegistrantsGridView()
    End Sub

    Protected Sub ddlRegistrantStatusFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegistrantStatusFilter.SelectedIndexChanged
        BindCourseRegistrantsGridView()
    End Sub

    Private Sub BindCourseRegistrantsGridView()
        gvCourseRegistrants.DataSource = GetSeminarRegistrationBySeminarId()
        gvCourseRegistrants.DataBind()
    End Sub

    Protected Function GetSeminarRegistrationBySeminarId() As List(Of SeminarRegistration)
        Dim seminarRegistration As New SeminarRegistration
        Dim seminarRegistrationBySeminarId As List(Of SeminarRegistration) = seminarRegistration.GetSeminarRegistrationBySeminarId(ddlCourseDate.SelectedValue)
        If Not (ddlRegistrantStatusFilter.SelectedValue = 0) Then
            seminarRegistrationBySeminarId = seminarRegistrationBySeminarId.Where(Function(seminarReg) seminarReg.RegistrantStatusId = ddlRegistrantStatusFilter.SelectedValue).ToList()
        Else
            seminarRegistrationBySeminarId = seminarRegistrationBySeminarId.Where(Function(seminarReg) seminarReg.RegistrantStatusId <> EnumHelper.RegistrantStatus.Cancelled).ToList()
        End If
        Return seminarRegistrationBySeminarId
    End Function

    Private Sub gvCourseRegistrants_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCourseRegistrants.RowCommand
        If (e.CommandName = "btnUpdateLineItem") Then
            Dim row As GridViewRow = gvCourseRegistrants.Rows(Convert.ToInt32(e.CommandArgument))
            UpdateRegistrant(row)
        End If
        If (e.CommandName = "btnEditLineItem") Then
            Dim seminarRegistrationId As Integer = gvCourseRegistrants.DataKeys(Convert.ToInt32(e.CommandArgument)).Item("SeminarRegistrationId")
            Response.Redirect("SeminarRegistrantEdit.aspx?Id=" + seminarRegistrationId.ToString())
        End If
        BindCourseRegistrantsGridView()
    End Sub

    Protected Sub gvCourseRegistrants_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCourseRegistrants.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim registrantStatusId As Integer = gvCourseRegistrants.DataKeys(e.Row.RowIndex).Item("RegistrantStatusId")
            Dim ddlRegistrantStatusId As DropDownList = e.Row.FindControl("ddlRegistrantStatusId")
            ddlRegistrantStatusId.DataSource = EnumHelper.EnumToDictionary(GetType(EnumHelper.RegistrantStatus))
            ddlRegistrantStatusId.DataTextField = "value"
            ddlRegistrantStatusId.DataValueField = "key"
            ddlRegistrantStatusId.DataBind()
            ddlRegistrantStatusId.SelectedValue = registrantStatusId
        End If
    End Sub

    Protected Sub lnkMarkAllUnapproved_Click(sender As Object, e As EventArgs) Handles lnkMarkAllUnapproved.Click
        UpdateAllDropdowns(EnumHelper.RegistrantStatus.Unapproved)
    End Sub

    Protected Sub lnkMarkAllApproved_Click(sender As Object, e As EventArgs) Handles lnkMarkAllApproved.Click
        UpdateAllDropdowns(EnumHelper.RegistrantStatus.Approved)
    End Sub

    Protected Sub lnkMarkAllAttended_Click(sender As Object, e As EventArgs) Handles lnkMarkAllAttended.Click
        UpdateAllDropdowns(EnumHelper.RegistrantStatus.Attended)
    End Sub

    Protected Sub btnUpdateBulk_Click(sender As Object, e As EventArgs) Handles btnUpdateBulk.Click
        For Each row As GridViewRow In gvCourseRegistrants.Rows
            UpdateRegistrant(row)
        Next
    End Sub

    Private Sub UpdateRegistrant(ByVal row As GridViewRow)
        Dim registrant As New Registrant
        Dim ddlRegistrantStatusId As DropDownList = row.FindControl("ddlRegistrantStatusId")
        Dim seminarRegistrantId As Integer = gvCourseRegistrants.DataKeys(row.RowIndex).Item("SeminarRegistrantId")
        Dim doctorId As Integer = gvCourseRegistrants.DataKeys(row.RowIndex).Item("DoctorId")
        Dim seminarId As Integer = gvCourseRegistrants.DataKeys(row.RowIndex).Item("SeminarId")
        registrant.UpdateRegistrant(seminarRegistrantId, ddlRegistrantStatusId.SelectedValue)
        registrant.AddRegistrantStatusAudit(seminarRegistrantId, ddlRegistrantStatusId.SelectedValue, Session("username"), Now)
        If (ddlRegistrantStatusId.SelectedValue = EnumHelper.RegistrantStatus.Attended) Then
            Doctor.AddAttendeeAddRegistration(doctorId, seminarId)
        End If
    End Sub

    Private Sub UpdateAllDropdowns(ByVal registrantStatus As EnumHelper.RegistrantStatus)
        For Each row As GridViewRow In gvCourseRegistrants.Rows
            Dim ddlRegistrantStatusId As DropDownList = row.FindControl("ddlRegistrantStatusId")
            ddlRegistrantStatusId.SelectedValue = registrantStatus
        Next
    End Sub

    Public Sub ExportGridToExcel(ByVal gv As GridView)
        Dim filename As String = "export_" & DateTime.Now.ToString("MMddyyyy_HHMMs")
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As New StringWriter()
        Dim ht As New HtmlTextWriter(sw)
        gv.AllowPaging = False
        BindCourseRegistrantsGridView()
        gv.Columns(gv.Columns.Count - 1).Visible = False
        gv.Columns(gv.Columns.Count - 2).Visible = False
        gv.Columns(0).ItemStyle.Width = "100"
        gv.Columns(1).ItemStyle.Width = "175"
        gv.Columns(2).ItemStyle.Width = "175"
        gv.Columns(3).ItemStyle.Width = "100"
        gv.RenderControl(ht)
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub

    Protected Sub btnExportExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportExcel.Click
        'ExportGridToExcel(gvCourseRegistrants)
        Dim seminarRegistration As New SeminarRegistration
        Dim seminarRegistrationBySeminarId As List(Of SeminarRegistration) = GetSeminarRegistrationBySeminarId()
        Dim colsToHide As ArrayList = GetColsToHide()
        ExportListToExcel(seminarRegistrationBySeminarId, colsToHide)
    End Sub

    Private Function GetColsToHide() As ArrayList

        Dim colsToHide As New ArrayList
        'Uncomment colsToHide.Add(n) to hide the column on export


        'Property SeminarRegistrantId() As Integer
        colsToHide.Add(0)
        'Property SeminarRegistrationId() As Integer
        colsToHide.Add(1)
        'Property SeminarDate() As DateTime
        colsToHide.Add(2)
        'Property SeminarId() As Integer
        colsToHide.Add(3)
        'Property FirstName() As String
        'colsToHide.Add(4)
        'Property LastName() As String
        'colsToHide.Add(5)
        'Property Degree() As String
        'colsToHide.Add(6)
        'Property YearGraduated() As String
        colsToHide.Add(7)
        'Property LicenseNumber() As String
        colsToHide.Add(8)
        'Property YearAttendedNAETBasic() As String
        colsToHide.Add(9)
        'Property YearAttendedNAETAdv1() As String
        colsToHide.Add(10)
        'Property Member() As String
        colsToHide.Add(11)
        'Property Referrer() As String
        colsToHide.Add(12)
        'Property Address() As String
        'colsToHide.Add(13)
        'Property City() As String
        'colsToHide.Add(14)
        'Property State() As String
        'colsToHide.Add(15)
        'Property Zip() As String
        'colsToHide.Add(16)
        'Property Country() As String
        'colsToHide.Add(17)
        'Property Phone() As String
        'colsToHide.Add(18)
        'Property Mobile() As String
        'colsToHide.Add(19)
        'Property Fax() As String
        'colsToHide.Add(20)
        'Property Email() As String
        'colsToHide.Add(21)
        'Property SpecialConsiderations() As String
        colsToHide.Add(22)
        'Property PaymentMethod() As String
        'colsToHide.Add(23)
        'Property CreditCardNum() As String
        'colsToHide.Add(24)
        'Property ExpDate() As String
        'colsToHide.Add(25)
        'Property CSCCode() As String
        'colsToHide.Add(26)
        'Property TotalAmount() As String
        'colsToHide.Add(27)
        'Property TransactionId() As String
        colsToHide.Add(28)
        'Property Status() As String
        colsToHide.Add(29)
        'Property AuthCode() As String
        colsToHide.Add(30)
        'Property ResponseStr() As String
        colsToHide.Add(31)
        'Property PNRef() As String
        colsToHide.Add(32)
        'Property DoctorId() As Integer
        'colsToHide.Add(33)
        'Property RegistrationDate() As DateTime
        'colsToHide.Add(34)
        'Property StudentAgreementFormFileName() As String
        colsToHide.Add(35)
        'Property StudentAgreementFormFileType() As String
        colsToHide.Add(36)

        Return colsToHide
    End Function

    Private Sub ExportListToExcel(ByVal seminarRegistrations As List(Of SeminarRegistration), Optional columnsToHideByIndex As ArrayList = Nothing)
        Dim filename As String = "export_" & DateTime.Now.ToString("MMddyyyy_HHMMs") & ".xls"
        Dim tw = New StringWriter()
        Dim hw = New HtmlTextWriter(tw)
        Dim gv As GridView = New GridView()

        gv.DataSource = seminarRegistrations
        gv.DataBind()
        If Not (IsNothing(columnsToHideByIndex)) Then
            For Each i As Integer In columnsToHideByIndex
                gv.HeaderRow.Cells(i).Visible = False
            Next
            For Each gridViewRow As GridViewRow In gv.Rows
                For Each i As Integer In columnsToHideByIndex
                    gridViewRow.Cells(i).Visible = False
                Next
            Next
        End If
        'Get the HTML for the control.
        gv.RenderControl(hw)
        'Write the HTML back to the browser.
        Response.ContentType = "application/vnd.ms-excel"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "")
        Response.Output.Write(tw.ToString())
        Response.End()
    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        'For the excel RenderControl
    End Sub

End Class