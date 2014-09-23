Public Partial Class AdminMenu
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        AuthenticationHelper.ValidateAdminWithRedirect(Session("username"), Session("password"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpHelper.RedirectSsl()
    End Sub

    Private Sub btnListPayments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListPayments.Click
        Response.Redirect("~/Admin/SeminarPaymentList.aspx")
    End Sub

    Protected Sub btnListCourses_Click(sender As Object, e As EventArgs) Handles btnListCourses.Click
        Response.Redirect("~/Admin/SeminarRegistrantList.aspx")
    End Sub

    Protected Sub btnRegForm_Click(sender As Object, e As EventArgs) Handles btnRegForm.Click
        Response.Redirect("~/Admin/SeminarRegistrationForm.aspx")
    End Sub
End Class