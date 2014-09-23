Imports System.Linq
Imports System.IO

Public Class SeminarPaymentDetail
    Inherits System.Web.UI.Page

    ReadOnly Property SeminarRegistrationId() As Integer
        Get
            Return Request.QueryString("Id")
        End Get
    End Property

    Dim _transaction As New Transaction
    Dim seminar As New Seminar

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        AuthenticationHelper.ValidateAdminWithRedirect(Session("username"), Session("password"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpHelper.RedirectSsl()
        If (String.IsNullOrEmpty(SeminarRegistrationId) Or SeminarRegistrationId = 0) Then
            Response.Redirect("SeminarPaymentList.aspx")
        End If
        Dim transaction As Transaction = _transaction.GetTransaction(SeminarRegistrationId)

        Dim ds As New List(Of Transaction)
        ds.Add(transaction)

        FormView1.DataSource = ds
        FormView1.DataBind()

        Dim seminarList As List(Of Seminar) = seminar.GetSeminars(SeminarRegistrationId)

        rptSeminars.DataSource = seminarList.OrderBy(Function(seminar) seminar.SeminarDate)
        rptSeminars.DataBind()
    End Sub

    Protected Sub btnViewLicense_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim license As New License
        license = license.GetLicense(SeminarRegistrationId)
        Response.Buffer = True
        Response.Charset = ""
        Response.Clear()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = license.FileType
        Response.BinaryWrite(license.FileContent)
        Response.Flush()
        Response.End()
    End Sub

    Protected Sub btnViewStudentAgreementForm_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim memberInfo As New MemberInfo
        memberInfo = memberInfo.GetStudentAgreementForm(SeminarRegistrationId)
        If (memberInfo Is Nothing) Then
            lblStudentAgreementFormError.Text = "No Student Agreement Form was found."
            Return
        End If
        Response.Buffer = True
        Response.Charset = ""
        Response.Clear()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = memberInfo.StudentAgreementFormFileType
        Response.BinaryWrite(memberInfo.StudentAgreementForm)
        Response.Flush()
        Response.End()
    End Sub
End Class