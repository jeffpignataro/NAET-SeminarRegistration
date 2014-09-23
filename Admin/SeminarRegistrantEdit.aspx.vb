Imports System.Xml

Public Class SeminarRegistrantEdit
    Inherits Page

    ReadOnly Property SeminarRegistrationId() As Integer
        Get
            Try
                If Not (IsNothing(Request.QueryString("Id"))) Then
                    Return Request.QueryString("Id")
                Else
                    Response.Redirect("SeminarRegistrantList.aspx")
                End If
            Catch ex As Exception
                Response.Redirect("SeminarRegistrantList.aspx")
            End Try
        End Get
    End Property

    ReadOnly _seminarRegistration As SeminarRegistration = New SeminarRegistration()

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        AuthenticationHelper.ValidateAdminWithRedirect(Session("username"), Session("password"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Dim seminarRegistration As SeminarRegistration = _seminarRegistration.GetSeminarRegistrationById(SeminarRegistrationId)
            Dim list As New List(Of SeminarRegistration) From {seminarRegistration}
            fvSeminarRegistration.DataSource = list
            fvSeminarRegistration.DataBind()
            BindCountry()
            'This is a hack because the dropdown can't be populated with values until after the databind
            'But it can't be set during the databind because it has no values.
            'This would probably be better done during the OnDataBound or OnDataBinding events
            'But this works as well
            SetCountryDropdown(seminarRegistration.Country)
        End If
    End Sub

    Private Sub SetCountryDropdown(ByVal country As String)
        Dim dropDownList As DropDownList = CType(fvSeminarRegistration.FindControl("ddlCountry"), DropDownList)
        dropDownList.SelectedValue = country
    End Sub

    Private Sub BindCountry()
        Dim CCodes As New XmlDocument
        CCodes.Load(Server.MapPath("~") & "\XMLCountryCodes.xml")
        'Dim CCodeNames As XmlNodeList
        Dim root As XmlNode = CCodes.DocumentElement
        Dim onodelist As XmlNodeList = root.ChildNodes
        Dim arCCodes As New ArrayList
        For Each ocurrentnode As XmlNode In onodelist
            Dim namenode As XmlNode = ocurrentnode.SelectSingleNode("name")
            Dim mcCode As XmlNode = ocurrentnode.SelectSingleNode("MCCode")
            Dim vcode As XmlNode = ocurrentnode.SelectSingleNode("VCode")
            arCCodes.Add(namenode.InnerXml)
        Next
        '            For I As Integer = 0 To CCodeNames.Count
        '            arCCodes.Add(CCodeNames(I).InnerXml)
        '            Next
        Dim dropDownList As DropDownList = CType(fvSeminarRegistration.FindControl("ddlCountry"), DropDownList)
        dropDownList.DataSource = arCCodes
        dropDownList.DataBind()
        Try
            dropDownList.Items.FindByText("UNITED STATES").Selected = True
        Catch
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim seminarRegistration As SeminarRegistration = _seminarRegistration.GetSeminarRegistrationById(SeminarRegistrationId)
        seminarRegistration = UpdateSeminarRegistrationFromFormValues(seminarRegistration)
        _seminarRegistration.UpdateSeminarRegistrationById(seminarRegistration)
        Response.Redirect("SeminarRegistrantList.aspx")
    End Sub

    Private Function UpdateSeminarRegistrationFromFormValues(ByVal seminarRegistration As SeminarRegistration) As SeminarRegistration
        Dim returnVal As SeminarRegistration = seminarRegistration
        returnVal.FirstName = CType(fvSeminarRegistration.FindControl("tbfName"), TextBox).Text
        returnVal.LastName = CType(fvSeminarRegistration.FindControl("tblName"), TextBox).Text
        returnVal.Address = CType(fvSeminarRegistration.FindControl("tbAddress"), TextBox).Text
        returnVal.City = CType(fvSeminarRegistration.FindControl("tbCity"), TextBox).Text
        returnVal.State = CType(fvSeminarRegistration.FindControl("tbState"), TextBox).Text
        returnVal.Zip = CType(fvSeminarRegistration.FindControl("tbZip"), TextBox).Text
        returnVal.Country = CType(fvSeminarRegistration.FindControl("ddlCountry"), DropDownList).SelectedValue
        returnVal.Phone = CType(fvSeminarRegistration.FindControl("tbPhone"), TextBox).Text
        returnVal.Mobile = CType(fvSeminarRegistration.FindControl("tbMobile"), TextBox).Text
        returnVal.Fax = CType(fvSeminarRegistration.FindControl("tbFax"), TextBox).Text
        returnVal.Email = CType(fvSeminarRegistration.FindControl("tbEmail"), TextBox).Text
        Return returnVal
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("SeminarRegistrantList.aspx")
    End Sub
End Class