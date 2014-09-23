Imports System.Linq

Public Class SeminarPaymentList
    Inherits System.Web.UI.Page

    ReadOnly Property TransactionStatus() As Integer
        Get
            Return ddlStatus.SelectedValue
        End Get
    End Property

    Dim _transaction As New Transaction


    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        AuthenticationHelper.ValidateAdminWithRedirect(Session("username"), Session("password"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpHelper.RedirectSsl()
        If Not (Page.IsPostBack) Then
            BindCalendars()
            BindTransactionGrid()
        End If
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        BindTransactionGrid()
    End Sub

    Private Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles GridView1.SelectedIndexChanging
        Dim seminarRegistrationId As Integer = GridView1.DataKeys(e.NewSelectedIndex).Value
        Response.Redirect("SeminarPaymentDetail.aspx?Id=" & seminarRegistrationId)
    End Sub

    Private Sub ImageButton1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton1.Click
        Cal1.Visible = Not Cal1.Visible
    End Sub

    Private Sub ImageButton2_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton2.Click
        Cal2.Visible = Not Cal2.Visible
    End Sub

    Private Sub Cal1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cal1.SelectionChanged
        Cal1.Visible = False
        lblStartDate.Text = Cal1.SelectedDate.Month & "/" & Cal1.SelectedDate.Day & "/" & Cal1.SelectedDate.Year

    End Sub

    Private Sub Cal2_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Cal2.SelectionChanged
        Cal2.Visible = False
        lblEndDate.Text = Cal2.SelectedDate.Month & "/" & Cal2.SelectedDate.Day & "/" & Cal2.SelectedDate.Year
    End Sub

    Protected Sub btnDateRange_Click(sender As Object, e As EventArgs) Handles btnDateRange.Click
        BindTransactionGrid()
    End Sub

    Private Sub BindTransactionGrid()
        lblErr.Text = ""

        Dim getTransactions As List(Of Transaction) = _transaction.GetTransactions()

        GridView1.DataSource = getTransactions.Where(Function(transaction) transaction.Status = TransactionStatus AndAlso transaction.RegistrationDate >= Cal1.SelectedDate AndAlso transaction.RegistrationDate <= Cal2.SelectedDate).OrderByDescending(Function(transaction) transaction.RegistrationDate)
        GridView1.DataBind()

        If (GridView1.Rows.Count = 0) Then
            lblErr.Text = "No registrants found."
        End If
    End Sub

    Private Sub BindCalendars()
        Dim dtToday As Date, dtStart As Date, dtend As Date
        dtToday = Now.Month & "/" & Now.Day & "/" & Now.Year & " 00:00:01"
        dtStart = Now.AddDays(-14)
        dtend = Now.AddDays(1)
        lblStartDate.Text = dtStart.Month & "/" & dtStart.Day & "/" & dtStart.Year
        Cal1.SelectedDate = dtStart

        lblEndDate.Text = dtend.Month & "/" & dtend.Day & "/" & dtend.Year
        Cal2.SelectedDate = dtend
    End Sub

End Class