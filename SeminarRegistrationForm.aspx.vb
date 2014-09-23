Imports System.Xml
Imports System.Linq
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Newtonsoft.Json

<ScriptService()>
Public Class SeminarRegistrationForm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            'HttpHelper.RedirectSsl()
            BindCountry()
            BindDdlExpDateYear()
            BindCheckBoxLists()
        End If

        'Reapply the option hightlight javascript on partial postback
        ScriptManager.RegisterClientScriptBlock(Page, Me.GetType(), "OptionHighlight", "OptionHighlight();", True)
        lblErr.Text = ""
    End Sub

    Private Sub BindDdlExpDateYear()
        For i As Integer = 0 To 10
            ddlExpDateYear.Items.Add(Now.Year + i)
        Next

    End Sub

    Private Sub BindCountry()
        Dim CCodes As New XmlDocument
        CCodes.Load(Server.MapPath("") & "\XMLCountryCodes.xml")
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
        Me.ddlCountry.DataSource = arCCodes
        Me.ddlCountry.DataBind()
        Try
            Me.ddlCountry.Items.FindByText("UNITED STATES").Selected = True
        Catch
        End Try
    End Sub

    Private Sub CalculateTotel()
        Dim currentTotal As Decimal = 0
        Dim selectedItemCount As Integer = 0
        Dim BasicVal As Decimal = Seminar.BasicFirst
        Dim Adv1Val As Decimal = Seminar.Adv1First
        Dim Adv2CaseManageVal As Decimal = Seminar.Adv2CaseManage
        'Dim Adv2Val As Decimal = Adv2
        Dim NstVal As Decimal = Seminar.Nst

        If (rblBasicSeminar.SelectedValue = "refresh") Then
            BasicVal = Seminar.BasicRefresh
        End If
        If (rblAdvancedLevel1.SelectedValue = "refresh") Then
            Adv1Val = Seminar.Adv1Refresh
        End If

        selectedItemCount = cblBasicSeminar.Items.Cast(Of ListItem)().Count(Function(li) (li.Selected))
        currentTotal = currentTotal + selectedItemCount * BasicVal
        selectedItemCount = cblAdvancedLevel1.Items.Cast(Of ListItem)().Count(Function(li) (li.Selected))
        currentTotal = currentTotal + selectedItemCount * Adv1Val
        selectedItemCount = cblAdvancedLevel2AndCaseManage.Items.Cast(Of ListItem)().Count(Function(li) (li.Selected))
        currentTotal = currentTotal + selectedItemCount * Adv2CaseManageVal
        'selectedItemCount = cblAdvancedLevel2.Items.Cast(Of ListItem)().Count(Function(li) (li.Selected))
        'currentTotal = currentTotal + selectedItemCount * Adv2Val
        selectedItemCount = cblNst.Items.Cast(Of ListItem)().Count(Function(li) (li.Selected))
        currentTotal = currentTotal + selectedItemCount * NstVal
        lblTotal.Text = FormatCurrency(currentTotal)
    End Sub

    Private Sub BindCheckBoxLists()
        Dim course As Course = New Course()
        cblBasicSeminar.DataSource = course.GetCoursesByType(EnumHelper.CourseType.Basic).Where(Function(c As Course) c.DateTime >= Now)
        cblBasicSeminar.DataValueField = "Id"
        cblBasicSeminar.DataTextField = "FormattedDateTimeWithSecondDay"
        cblBasicSeminar.DataBind()
        If (cblBasicSeminar.Items.Count = 0) Then
            lblBasicSeminar.Visible = True
        End If

        cblAdvancedLevel1.DataSource = course.GetCoursesByType(EnumHelper.CourseType.AdvancedI).Where(Function(c As Course) c.DateTime >= Now)
        cblAdvancedLevel1.DataValueField = "Id"
        cblAdvancedLevel1.DataTextField = "FormattedDateTimeWithSecondDay"
        cblAdvancedLevel1.DataBind()
        If (cblAdvancedLevel1.Items.Count = 0) Then
            lblAdvancedLevel1.Visible = True
        End If

        cblAdvancedLevel2AndCaseManage.DataSource = course.GetCoursesBySpecialization().Where(Function(c As Course) c.DateTime >= Now)
        cblAdvancedLevel2AndCaseManage.DataValueField = "Id"
        cblAdvancedLevel2AndCaseManage.DataTextField = "FormattedDatetimeWithSpecialization"
        cblAdvancedLevel2AndCaseManage.DataBind()
        If (cblAdvancedLevel2AndCaseManage.Items.Count = 0) Then
            lblAdvancedLevel2AndCaseManage.Visible = True
        End If

        cblNst.DataSource = course.GetCoursesByType(EnumHelper.CourseType.Nst).Where(Function(c As Course) c.DateTime >= Now)
        cblNst.DataValueField = "Id"
        cblNst.DataTextField = "FormattedDateTime"
        cblNst.DataBind()
        If (cblNst.Items.Count = 0) Then
            lblNst.Visible = True
        End If
    End Sub

    Protected Sub cblBasicSeminar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblBasicSeminar.SelectedIndexChanged
        ShowFileUpload()
        CalculateTotel()
    End Sub

    Protected Sub cblAdvancedLevel1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblAdvancedLevel1.SelectedIndexChanged
        CalculateTotel()
    End Sub

    Protected Sub cblAdvancedLevel2AndCaseManage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblAdvancedLevel2AndCaseManage.SelectedIndexChanged
        CalculateTotel()
    End Sub

    'Protected Sub cblAdvancedLevel2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblAdvancedLevel2.SelectedIndexChanged
    '    CalculateTotel()
    'End Sub

    Protected Sub cblNst_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cblNst.SelectedIndexChanged
        CalculateTotel()
    End Sub

    Protected Sub rblBasicSeminar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblBasicSeminar.SelectedIndexChanged
        CalculateTotel()
    End Sub

    Protected Sub rblAdvancedLevel1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblAdvancedLevel1.SelectedIndexChanged
        CalculateTotel()
    End Sub

    Private Sub ShowFileUpload()
        For Each li As ListItem In cblBasicSeminar.Items
            If (li.Selected) Then
                rfvBasicSeminarFileUpload.Enabled = True
                Exit Sub
            Else
                rfvBasicSeminarFileUpload.Enabled = False
            End If
        Next
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim payment As New Payment
        Dim transaction As New Transaction
        Dim memberInfo As New MemberInfo
        Dim billingAddress As New BillingAddress
        Dim contactAddress As New ContactAddress
        Dim license As New License
        With memberInfo
            .Degree = tbDegree.Text
            .YearGraduated = tbYearGraduated.Text
            .LicenseNumber = "" 'tbLicenseNumber.Text REPLACED BY FILE UPLOAD
            .YearAttendedNaetBasic = tbYearAttendedBasic.Text
            .YearAttendedNaetAdv1 = tbYearAttendedAdv1.Text
            .Member = CType(rblNAETMember.SelectedValue, Boolean)
            .Referrer = tbReferred.Text
            .SpecialConsiderations = tbSpecialConsiderations.Text
            .StudentAgreementForm = FileHelper.BinaryReader(fuBasicSeminar.PostedFile.InputStream)
            .DoctorId = TypeHelper.ConvertStringtoInt(tbDoctorKey.Text)
            .StudentAgreementFormFileName = fuBasicSeminar.PostedFile.FileName
            .StudentAgreementFormFileType = fuBasicSeminar.PostedFile.ContentType
        End With
        With billingAddress
            .FirstName = tbfName.Text
            .LastName = tblName.Text
            .Address = tbAddress.Text
            .City = tbCity.Text
            .State = tbState.Text
            .Zip = tbZip.Text
            .Country = ddlCountry.SelectedValue
            .Phone = tbPhone.Text
            .Mobile = tbMobile.Text
            .Fax = tbFax.Text
            .Email = tbEmail.Text
        End With
        With contactAddress
            .FirstName = tbfName.Text
            .LastName = tblName.Text
            .Address = tbAddress.Text
            .City = tbCity.Text
            .State = tbState.Text
            .Zip = tbZip.Text
            .Country = ddlCountry.SelectedValue
            .Phone = tbPhone.Text
            .Mobile = tbMobile.Text
            .Fax = tbFax.Text
            .Email = tbEmail.Text
        End With
        With license
            .FileContent = FileHelper.BinaryReader(fuLicenseFile.PostedFile.InputStream)
            .FileName = fuLicenseFile.PostedFile.FileName
            .FileType = fuLicenseFile.PostedFile.ContentType
        End With
        With transaction
            .MemberInfo = memberInfo
            .BillingAddress = billingAddress
            .ContactAddress = contactAddress
            .License = license
            .CardNum = tbCreditCard.Text
            .CardExpMo = ddlExpDateMonth.SelectedValue
            .CardExpYear = ddlExpDateYear.SelectedValue
            .CscCode = tbCCV.Text
            .CardType = rblPaymentMethod.SelectedValue
            .Notes = tbSpecialConsiderations.Text
            .TransactionId = 0
            .TotalAmount = lblTotal.Text
            .Status = 0 'Status always starts as 0
            .Mode = EnumHelper.TransactionMode.Live
            If (ConfigurationManager.AppSettings("devmode") = "yes") Then
                'TEST gateway URL isn't working for some reason...
                .Mode = EnumHelper.TransactionMode.Dev
            End If
        End With
        'Add Transaction to the DB 
        '   Status = 0
        Try
            Dim addTransaction As DataTable = transaction.AddTransaction(transaction)
            For Each dr As DataRow In addTransaction.Rows
                'Return SeminarRegistrationId PriKey from DB
                transaction.TransactionId = dr(0)
                transaction.License.SeminarRegistrationId = transaction.TransactionId
            Next
            license.AddLicense(transaction.License)
        Catch ex As Exception
            lblErr.Text = "There was an error saving your transaction to the database. Please confirm all fields are filled out and formatted correctly. If you continue to experience an error please contact NAET at webmaster@naet.com."
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Exit Sub
        End Try
        Dim returnPayment As Boolean = False
        Try
            'Authorize Payment
            '   Set Transaction Return Values: AuthNetCode, AuthNetTransId, RtnMesg
            If (transaction.Mode = EnumHelper.TransactionMode.Dev) Then
                transaction.AuthNetCode = "12345A"
                transaction.AuthNetTransId = Guid.NewGuid().ToString()
                transaction.RtnMesg = ""
                transaction.PNRef = "1234567890"
            Else
                returnPayment = payment.AuthorizePayment(transaction)
            End If
        Catch ex As Exception
            lblErr.Text = "There was an error processing your transaction. Your credit card was not charged. Please confirm all fields are correct. If you continue to experience an error please contact NAET at webmaster@naet.com."
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            Exit Sub
        End Try
        Try
            If Not (returnPayment) AndAlso Not (transaction.Mode = EnumHelper.TransactionMode.Dev) Then
                lblErr.Text = transaction.RtnMesg
            Else
                transaction.RtnMesg = "Success"
                'If Payment Succeeds
                '   Status = 1
                transaction.Status = 1
                'Update Transaction Return Values in SeminarRegistration Table
                transaction.UpdateTransaction(transaction)
                Dim seminarDates As List(Of Seminar) = GetSeminars()
                'If Payment Succeeds
                '   Get Seminar Dates from ASPX
                '   Add Seminar Dates to DB with SeminarRegistrationId
                Seminar.AddSeminar(transaction, seminarDates)
                'Send confirmation email.
                Dim sb As StringBuilder = BuildEmailBody(transaction, seminarDates)
                EmailHelper.SendEmail(sb.ToString(), "NAET Seminar Registration", transaction.BillingAddress.Email, ConfigurationManager.AppSettings("EmailNotifyFrom"))
                'Transaction complete. Proceed to Success Page.
                Response.Redirect("SeminarRegistrationSuccess.aspx")
            End If
        Catch ex As Exception
            lblErr.Text = "There was an error saving your seminar dates. Your payment has been posted, but please contact NAET at webmaster@naet.com to confirm your seminar date registration."
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        End Try

    End Sub

    Private Function BuildEmailBody(ByVal transaction As Transaction, ByVal seminarDates As List(Of Seminar)) As StringBuilder
        Dim sb As New StringBuilder
        sb.AppendFormat("Dear {0} {1},", transaction.BillingAddress.FirstName, transaction.BillingAddress.LastName)
        sb.Append(vbCrLf & vbCrLf) '"<br>")
        sb.Append("Thank you for registering for the following NAET seminars: " & vbCrLf) '<br>")
        For Each seminar As Seminar In seminarDates
            'sb.AppendFormat("{0} - {1}<br>", EnumHelper.GetDescriptionFromEnumValue(seminar.Type), seminar.SeminarDate.ToShortDateString())
            sb.AppendFormat("{0} - {1}" & vbCrLf, EnumHelper.GetDescriptionFromEnumValue(seminar.Type), seminar.SeminarDate.ToShortDateString())
        Next
        Return sb
    End Function

    Private Function GetSeminars() As List(Of Seminar)
        Dim returnList As New List(Of Seminar)
        'Basic Seminar
        returnList.AddRange(PopulateSeminarList(cblBasicSeminar, EnumHelper.SeminarType.Basic))
        'Adv1 Seminar
        returnList.AddRange(PopulateSeminarList(cblAdvancedLevel1, EnumHelper.SeminarType.Adv1))
        'Adv2 Seminar
        'returnList.AddRange(PopulateSeminarList(cblAdvancedLevel2, EnumHelper.SeminarType.Adv2))
        'Adv2WCaseManage Seminar
        returnList.AddRange(PopulateSeminarListWithSpecialization(cblAdvancedLevel2AndCaseManage, EnumHelper.SeminarType.Adv2WithCaseManage))
        'Nst Seminar
        returnList.AddRange(PopulateSeminarList(cblNst, EnumHelper.SeminarType.Nst))
        Return returnList
    End Function

    Private Shared Function PopulateSeminarList(ByVal checkBoxList As CheckBoxList, ByVal seminarType As EnumHelper.SeminarType) As IEnumerable(Of Seminar)
        Dim enumerable As IEnumerable(Of Seminar)
        Select Case seminarType
            Case EnumHelper.SeminarType.Basic
                enumerable = From li As ListItem In checkBoxList.Items Where li.Selected Select New Seminar(li.Value, ManipulateDate(li.Text), seminarType)
            Case EnumHelper.SeminarType.Adv1
                enumerable = From li As ListItem In checkBoxList.Items Where li.Selected Select New Seminar(li.Value, ManipulateDate(li.Text), seminarType)
            Case EnumHelper.SeminarType.Adv2
                enumerable = From li As ListItem In checkBoxList.Items Where li.Selected Select New Seminar(li.Value, li.Text, seminarType)
            Case EnumHelper.SeminarType.Adv2WithCaseManage
                enumerable = From li As ListItem In checkBoxList.Items Where li.Selected Select New Seminar(li.Value, li.Text, seminarType)
            Case EnumHelper.SeminarType.Nst
                enumerable = From li As ListItem In checkBoxList.Items Where li.Selected Select New Seminar(li.Value, li.Text, seminarType)
            Case Else
                enumerable = From li As ListItem In checkBoxList.Items Where li.Selected Select New Seminar(li.Value, li.Text, seminarType)
        End Select
        Return enumerable
    End Function

    Private Shared Function ManipulateDate(ByVal text As String) As Date
        dim dayAfter As Date = Date.Parse(text.Substring(9))
        Return Date.Parse(dayAfter.AddDays(-1))
    End Function

    Private Shared Function PopulateSeminarListWithSpecialization(ByVal checkBoxList As CheckBoxList, ByVal seminarType As EnumHelper.SeminarType) As IEnumerable(Of Seminar)
        For Each li As ListItem In checkBoxList.Items
            li.Text = RemoveSpecialization(li.Text)
        Next
        Return PopulateSeminarList(checkBoxList, seminarType)
    End Function

    Private Shared Function RemoveSpecialization(ByVal text As String) As String
        Return text.Substring(0, text.Length - 4) 'Remove " (X)" from the date
    End Function

    <WebMethod()>
    Public Shared Function DoctorLogin(ByVal username As String, ByVal password As String) As String
        Return UserLogin(username, password)
    End Function

    Protected Shared Function UserLogin(ByVal sLogin As String, ByVal sPW As String) As String
        Dim oData As New objData
        Dim sEPW As String = ""
        sEPW = EncryptPWForMatch(sLogin, sPW)

        Dim sqlCommand As New SqlCommand
        sqlCommand.CommandType = CommandType.StoredProcedure

        Dim param1 As New SqlParameter
        param1.DbType = DbType.String
        param1.ParameterName = "@ipUserName"
        param1.Value = sLogin

        Dim param2 As New SqlParameter
        param2.DbType = DbType.String
        param2.ParameterName = "@ipPassword"
        param2.Value = sEPW

        sqlCommand.CommandText = "sp_ValidateUserEPW"
        sqlCommand.Parameters.Add(param1)
        sqlCommand.Parameters.Add(param2)

        Dim dt As DataTable = oData.GetDataTableSP(sqlCommand)

        If dt.Rows.Count > 0 Then
            Dim sDocKey As String = dt.Rows(0).Item(0).ToString
            Dim _doctor As New Doctor()
            Dim doctor As Doctor = _doctor.GetDoctorByDoctorId(sDocKey)
            If Not (IsNothing(doctor)) Then
                'return doctor info to client
                Return JsonConvert.SerializeObject(doctor)
            End If
            'Something broke...
            Return String.Empty
        Else
            Dim anonError = New With {
                                        .Error = True,
                                        .ErrorMsg = "The username/password combination was not found."
                                     }
            Return JsonConvert.SerializeObject(anonError)
        End If
    End Function

    Shared Function CreateDBSession(ByVal sDocKey As String, ByVal sGUID As String) As Boolean
        Dim bln As Boolean = True
        Dim oData As New objData
        Dim sqlCommand As New SqlCommand
        sqlCommand.CommandType = CommandType.StoredProcedure
        Dim sResponse As String = ""

        Dim param1 As New SqlParameter
        param1.DbType = DbType.Int32
        param1.ParameterName = "@ipDoctorKey"
        param1.Value = sDocKey

        Dim param2 As New SqlParameter
        param2.DbType = DbType.String
        param2.ParameterName = "@ipSessionGUID"
        param2.Value = sGUID

        sqlCommand.CommandText = "sp_InsertDoctorLoginSession"
        sqlCommand.Parameters.Add(param1)
        sqlCommand.Parameters.Add(param2)

        sResponse = oData.InsertUpdateDataNonQuery(sqlCommand)
        If sResponse <> "Success" Then
            bln = False
        End If
        Return bln
    End Function

    Protected Shared Sub AccessLog(ByVal sDocKey As String)
        Dim oData As New objData
        Dim sqlCommand As New SqlCommand
        sqlCommand.CommandType = CommandType.StoredProcedure
        Dim sResponse As String = ""

        Dim sIP As String = ""
        Dim request As HttpRequest = HttpContext.Current.Request
        sIP = request.ServerVariables("REMOTE_ADDR").ToString

        Dim iDocKey As Integer = 0
        Try
            iDocKey = CType(sDocKey, Integer)
        Catch ex As Exception

        End Try

        Dim param1 As New SqlParameter
        param1.DbType = DbType.Int32
        param1.ParameterName = "@ipDoctorKey"
        param1.Value = iDocKey

        Dim param2 As New SqlParameter
        param2.DbType = DbType.String
        param2.ParameterName = "@ipLoginIPD"
        param2.Value = sIP

        sqlCommand.CommandText = "sp_AddDoctorLoginEvent"
        sqlCommand.Parameters.Add(param1)
        sqlCommand.Parameters.Add(param2)
        sResponse = oData.InsertUpdateDataNonQuery(sqlCommand)
    End Sub

    Shared Function EncryptPWForMatch(ByVal sLogin As String, ByVal sPW As String) As String
        Dim sEPW As String = ""
        Dim oData As New objData
        Dim sqlCommand As New SqlCommand
        sqlCommand.CommandType = CommandType.StoredProcedure
        Dim param1 As New SqlParameter
        param1.DbType = DbType.String
        param1.ParameterName = "@ipUserName"
        param1.Value = sLogin
        sqlCommand.CommandText = "sp_GetPWSalt"
        sqlCommand.Parameters.Add(param1)
        Dim dt As DataTable = oData.GetDataTableSP(sqlCommand)
        If dt.Rows.Count > 0 Then
            Dim sSalt As String = dt.Rows(0).Item(0).ToString
            sEPW = EncryptString(sPW, "SHA1", sSalt)
        End If
        Return sEPW
    End Function

    Shared Function EncryptString(ByVal plainText As String, ByVal hashAlgorithm As String, ByVal sSalt As String) As String
        ' Convert plain text into a byte array.
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(plainText)

        Dim saltBytes As Byte()
        saltBytes = Encoding.UTF8.GetBytes(sSalt)


        ' Because we support multiple hashing algorithms, we must define
        ' hash object as a common (abstract) base class. We will specify the
        ' actual hashing algorithm class later during object creation.
        Dim hash As HashAlgorithm
        ' Make sure hashing algorithm name is specified.
        If (hashAlgorithm Is Nothing) Then
            hashAlgorithm = ""
        End If

        ' Initialize appropriate hashing algorithm class.
        Select Case hashAlgorithm.ToUpper()

            Case "SHA1"
                hash = New SHA1Managed()

            Case "SHA256"
                hash = New SHA256Managed()

            Case "SHA384"
                hash = New SHA384Managed()

            Case "SHA512"
                hash = New SHA512Managed()

            Case Else
                hash = New MD5CryptoServiceProvider()

        End Select

        ' Allocate array, which will hold plain text and salt.
        Dim plainTextWithSaltBytes() As Byte = _
            New Byte(plainTextBytes.Length + saltBytes.Length - 1) {}

        ' Copy plain text bytes into resulting array.
        Dim I As Integer
        For I = 0 To plainTextBytes.Length - 1
            plainTextWithSaltBytes(I) = plainTextBytes(I)
        Next I

        ' Append salt bytes to the resulting array.
        For I = 0 To saltBytes.Length - 1
            plainTextWithSaltBytes(plainTextBytes.Length + I) = saltBytes(I)
        Next I

        ' Compute hash value of our plain text with appended salt.
        Dim hashBytes As Byte()
        hashBytes = hash.ComputeHash(plainTextWithSaltBytes)

        ' Create array which will hold hash and original salt bytes.
        Dim hashWithSaltBytes() As Byte = New Byte(hashBytes.Length + saltBytes.Length - 1) {}

        ' Copy hash bytes into resulting array.
        For I = 0 To hashBytes.Length - 1
            hashWithSaltBytes(I) = hashBytes(I)
        Next I

        ' Append salt bytes to the result.
        For I = 0 To saltBytes.Length - 1
            hashWithSaltBytes(hashBytes.Length + I) = saltBytes(I)
        Next I

        ' Convert result into a base64-encoded string.
        Dim hashValue As String
        hashValue = Convert.ToBase64String(hashWithSaltBytes)


        Return hashValue
    End Function

End Class