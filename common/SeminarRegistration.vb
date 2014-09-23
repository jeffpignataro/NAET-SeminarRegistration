Imports System.Data.SqlClient
Imports System.Linq

Public Class SeminarRegistration

    Property SeminarRegistrantId() As Integer
    Property SeminarRegistrationId() As Integer
    Property SeminarType() As EnumHelper.SeminarType
    Property SeminarDate() As DateTime
    Property SeminarId() As Integer
    Property RegistrantStatusId() As EnumHelper.RegistrantStatus
    Property FirstName() As String
    Property LastName() As String
    Property Degree() As String
    Property YearGraduated() As String
    Property LicenseNumber() As String
    Property YearAttendedNAETBasic() As String
    Property YearAttendedNAETAdv1() As String
    Property Member() As String
    Property Referrer() As String
    Property Address() As String
    Property City() As String
    Property State() As String
    Property Zip() As String
    Property Country() As String
    Property Phone() As String
    Property Mobile() As String
    Property Fax() As String
    Property Email() As String
    Property SpecialConsiderations() As String
    Property PaymentMethod() As String
    Property CreditCardNum() As String
    Property ExpDate() As String
    Property CSCCode() As String
    Property TotalAmount() As String
    Property TransactionId() As String
    Property Status() As String
    Property AuthCode() As String
    Property ResponseStr() As String
    Property PNRef() As String
    Property DoctorId() As Integer
    Property RegistrationDate() As DateTime
    Property StudentAgreementForm() As Byte()
    Property StudentAgreementFormFileName() As String
    Property StudentAgreementFormFileType() As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal seminarRegistrantId As Integer, ByVal seminarRegistrationId As Integer, ByVal seminarType As EnumHelper.SeminarType, ByVal seminarDate As Date, ByVal seminarId As Integer, ByVal registrantStatusId As EnumHelper.RegistrantStatus, ByVal firstName As String, ByVal lastName As String, ByVal degree As String, ByVal yearGraduated As String, ByVal licenseNumber As String, ByVal yearAttendedNaetBasic As String, ByVal yearAttendedNaetAdv1 As String, ByVal member As String, ByVal referrer As String, ByVal address As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal country As String, ByVal phone As String, ByVal mobile As String, ByVal fax As String, ByVal email As String, ByVal specialConsiderations As String, ByVal paymentMethod As String, ByVal creditCardNum As String, ByVal expDate As String, ByVal cscCode As String, ByVal totalAmount As String, ByVal transactionId As Integer, ByVal status As String, ByVal authCode As String, ByVal responseStr As String, ByVal pnRef As String, ByVal doctorId As Integer, ByVal registrationDate As Date, ByVal studentAgreementForm As Byte(), ByVal studentAgreementFormFileName As String, ByVal studentAgreementFormFileType As String)
        Me.SeminarRegistrantId = seminarRegistrantId
        Me.SeminarRegistrationId = seminarRegistrationId
        Me.SeminarType = seminarType
        Me.SeminarDate = seminarDate
        Me.SeminarId = seminarId
        Me.RegistrantStatusId = registrantStatusId
        Me.FirstName = firstName
        Me.LastName = lastName
        Me.Degree = degree
        Me.YearGraduated = yearGraduated
        Me.LicenseNumber = licenseNumber
        Me.YearAttendedNAETBasic = yearAttendedNaetBasic
        Me.YearAttendedNAETAdv1 = yearAttendedNaetAdv1
        Me.Member = member
        Me.Referrer = referrer
        Me.Address = address
        Me.City = city
        Me.State = state
        Me.Zip = zip
        Me.Country = country
        Me.Phone = phone
        Me.Mobile = mobile
        Me.Fax = fax
        Me.Email = email
        Me.SpecialConsiderations = specialConsiderations
        Me.PaymentMethod = paymentMethod
        Me.CreditCardNum = creditCardNum
        Me.ExpDate = expDate
        Me.CSCCode = cscCode
        Me.TotalAmount = totalAmount
        Me.TransactionId = transactionId
        Me.Status = status
        Me.AuthCode = authCode
        Me.ResponseStr = responseStr
        Me.PNRef = pnRef
        Me.DoctorId = doctorId
        Me.RegistrationDate = registrationDate
        Me.StudentAgreementForm = studentAgreementForm
        Me.StudentAgreementFormFileName = studentAgreementFormFileName
        Me.StudentAgreementFormFileType = studentAgreementFormFileType
    End Sub

    Public Function GetSeminarRegistrationById(ByVal Id As Integer) As SeminarRegistration
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetSeminarRegistrationByIdParameters(Id)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminarRegistrationById", parameters, SqlHelper.ConnectionString)
        Return MapSeminarRegistration(dataTable).FirstOrDefault()
    End Function

    Public Function GetSeminarRegistrationBySeminarId(ByVal Id As Integer) As List(Of SeminarRegistration)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetSeminarRegistrationBySeminarIdParameters(Id)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminarRegistrationBySeminarId", parameters, SqlHelper.ConnectionString)
        Return MapSeminarRegistration(dataTable)
    End Function

    Public Function UpdateSeminarRegistrationById(ByVal seminarReg As SeminarRegistration) As Boolean
        Dim parameters As List(Of SqlParameter) = SqlHelper.UpdateSeminarRegistrationByIdParameters(seminarReg)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_UpdateSeminarRegistrationById", parameters, SqlHelper.ConnectionString)
        Return Not IsNothing(dataTable)
    End Function

    Private Function MapSeminarRegistration(ByVal dataTable As DataTable) As List(Of SeminarRegistration)
        Dim returnList As New List(Of SeminarRegistration)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim seminarRegistration As New SeminarRegistration
                With seminarRegistration
                    .SeminarRegistrantId = SqlHelper.CheckForNull(dataRow.Item("SeminarRegistrantId"), GetType(Integer))
                    .SeminarRegistrationId = SqlHelper.CheckForNull(dataRow.Item("SeminarRegistrationId"), GetType(Integer))
                    .SeminarType = SqlHelper.CheckForNull(dataRow.Item("SeminarType"), GetType(EnumHelper.SeminarType))
                    .SeminarDate = SqlHelper.CheckForNull(dataRow.Item("SeminarDate"), GetType(DateTime))
                    .SeminarId = SqlHelper.CheckForNull(dataRow.Item("SeminarId"), GetType(Integer))
                    .RegistrantStatusId = SqlHelper.CheckForNull(dataRow.Item("RegistrantStatusId"), GetType(EnumHelper.RegistrantStatus))
                    .FirstName = SqlHelper.CheckForNull(dataRow.Item("FirstName"), GetType(String))
                    .LastName = SqlHelper.CheckForNull(dataRow.Item("LastName"), GetType(String))
                    .Degree = SqlHelper.CheckForNull(dataRow.Item("Degree"), GetType(String))
                    .YearGraduated = SqlHelper.CheckForNull(dataRow.Item("YearGraduated"), GetType(String))
                    .LicenseNumber = SqlHelper.CheckForNull(dataRow.Item("LicenseNumber"), GetType(String))
                    .YearAttendedNAETBasic = SqlHelper.CheckForNull(dataRow.Item("YearAttendedNAETBasic"), GetType(String))
                    .YearAttendedNAETAdv1 = SqlHelper.CheckForNull(dataRow.Item("YearAttendedNAETAdv1"), GetType(String))
                    .Member = SqlHelper.CheckForNull(dataRow.Item("Member"), GetType(Boolean))
                    .Referrer = SqlHelper.CheckForNull(dataRow.Item("Referrer"), GetType(String))
                    .Address = SqlHelper.CheckForNull(dataRow.Item("Address"), GetType(String))
                    .City = SqlHelper.CheckForNull(dataRow.Item("City"), GetType(String))
                    .State = SqlHelper.CheckForNull(dataRow.Item("State"), GetType(String))
                    .Zip = SqlHelper.CheckForNull(dataRow.Item("Zip"), GetType(String))
                    .Country = SqlHelper.CheckForNull(dataRow.Item("Country"), GetType(String))
                    .Phone = SqlHelper.CheckForNull(dataRow.Item("Phone"), GetType(String))
                    .Mobile = SqlHelper.CheckForNull(dataRow.Item("Mobile"), GetType(String))
                    .Fax = SqlHelper.CheckForNull(dataRow.Item("Fax"), GetType(String))
                    .Email = SqlHelper.CheckForNull(dataRow.Item("Email"), GetType(String))
                    .SpecialConsiderations = SqlHelper.CheckForNull(dataRow.Item("SpecialConsiderations"), GetType(String))
                    .PaymentMethod = SqlHelper.CheckForNull(dataRow.Item("PaymentMethod"), GetType(String))
                    .CreditCardNum = SqlHelper.CheckForNull(dataRow.Item("CreditCardNum"), GetType(String))
                    .ExpDate = SqlHelper.CheckForNull(dataRow.Item("ExpDate"), GetType(String))
                    .CSCCode = SqlHelper.CheckForNull(dataRow.Item("CSCCode"), GetType(String))
                    .TotalAmount = SqlHelper.CheckForNull(dataRow.Item("TotalAmount"), GetType(String))
                    .TransactionId = SqlHelper.CheckForNull(dataRow.Item("TransactionId"), GetType(String))
                    .Status = SqlHelper.CheckForNull(dataRow.Item("Status"), GetType(String))
                    .AuthCode = SqlHelper.CheckForNull(dataRow.Item("AuthCode"), GetType(String))
                    .ResponseStr = SqlHelper.CheckForNull(dataRow.Item("ResponseStr"), GetType(String))
                    .PNRef = SqlHelper.CheckForNull(dataRow.Item("PNRef"), GetType(String))
                    .DoctorId = SqlHelper.CheckForNull(dataRow.Item("DoctorId"), GetType(Integer))
                    .RegistrationDate = SqlHelper.CheckForNull(dataRow.Item("RegistrationDate"), GetType(DateTime))
                    If (dataRow.Table.Columns.Contains("StudentAgreementForm")) Then
                        .StudentAgreementForm = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementForm"), GetType(Byte()))
                    End If
                    .StudentAgreementFormFileName = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementFormFileName"), GetType(String))
                    .StudentAgreementFormFileType = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementFormFileType"), GetType(String))
                End With
                returnList.Add(seminarRegistration)
            Next
        End If
        Return returnList
    End Function
End Class
