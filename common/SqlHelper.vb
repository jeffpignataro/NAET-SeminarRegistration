Imports System.Data.SqlClient
Imports System.Data

Public Class SqlHelper

    Public Shared ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("DBConnStr").ConnectionString
        End Get
    End Property

    Public Shared Function ExecuteStoredProcedure(ByVal storedProcedure As String, ByVal parameters As List(Of SqlParameter), ByVal connstring As String) As DataTable
        Dim sqlConnection As New SqlConnection(connstring)
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Dim returnDataTable As New DataTable

        cmd.CommandText = storedProcedure
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddRange(parameters.ToArray())
        cmd.Connection = sqlConnection
        Try
            sqlConnection.Open()

            reader = cmd.ExecuteReader()
            returnDataTable.Load(reader)

            sqlConnection.Close()
        Catch ex As Exception
            If (sqlConnection.State = ConnectionState.Open) Then
                sqlConnection.Close()
            End If
            Return Nothing
        End Try
        Return returnDataTable
    End Function

    Public Shared Function CheckForNull(ByVal o As Object, ByVal type As Type) As Object
        If (type = GetType(Byte())) Then
            'Byte() can't convert to String for IsNullOrEmpty
            Return o
        End If
        If (IsDBNull(o) OrElse String.IsNullOrEmpty(o)) Then
            Select Case (type)
                Case GetType(String)
                    Return String.Empty
                Case GetType(Integer)
                    Return 0
                Case GetType(Date)
                    Return Date.MinValue
                Case GetType(Guid)
                    Return String.Empty
                Case GetType(Boolean)
                    Return False
                Case GetType(DateTime)
                    Return DateTime.MinValue
                Case Else
                    Return String.Empty
            End Select
        End If
        Return o
    End Function

    Public Shared Function AddTransactionParameters(ByVal transaction As Transaction) As List(Of SqlParameter)
        Dim sqlParameterCollection As New List(Of SqlParameter)
        '@FirstName				 nvarchar(50),
        '@LastName				 nvarchar(250),
        '@Degree			     nvarchar(250),
        '@YearGraduated			 nvarchar(50),
        '@LicenseNumber			 nvarchar(50),
        '@YearAttendedNAETBasic	 nvarchar(50),
        '@YearAttendedNAETAdv1	 nvarchar(50),
        '@Member			     bit,
        '@Referrer				 nvarchar(250),
        '@Address				 nvarchar(500),
        '@City					 nvarchar(250),
        '@State					 nvarchar(250),
        '@Zip					 nvarchar(50),
        '@Country				 nvarchar(250),
        '@Phone					 nvarchar(50),
        '@Mobile				 nvarchar(50),
        '@Fax					 nvarchar(50),
        '@Email					 nvarchar(50),
        '@SpecialConsiderations	 text,
        '@PaymentMethod			 nvarchar(50),
        '@CreditCardNum			 nvarchar(50),
        '@ExpDate				 nvarchar(50),
        '@CSCCode				 nvarchar(50),
        '@TotalAmount			 decimal,
        '@StudentAgreementForm	 varbinary(max),
        '@DoctorId				 int
        '@StudentAgreementFormFileName
        '@StudentAgreementFormFileType
        sqlParameterCollection.Add(New SqlParameter("@FirstName", transaction.BillingAddress.FirstName))
        sqlParameterCollection.Add(New SqlParameter("@LastName", transaction.BillingAddress.LastName))
        sqlParameterCollection.Add(New SqlParameter("@Degree", transaction.MemberInfo.Degree))
        sqlParameterCollection.Add(New SqlParameter("@YearGraduated", transaction.MemberInfo.YearGraduated))
        sqlParameterCollection.Add(New SqlParameter("@LicenseNumber", transaction.MemberInfo.LicenseNumber))
        sqlParameterCollection.Add(New SqlParameter("@YearAttendedNAETBasic", transaction.MemberInfo.YearAttendedNaetBasic))
        sqlParameterCollection.Add(New SqlParameter("@YearAttendedNAETAdv1", transaction.MemberInfo.YearAttendedNaetAdv1))
        sqlParameterCollection.Add(New SqlParameter("@Member", transaction.MemberInfo.Member))
        sqlParameterCollection.Add(New SqlParameter("@Referrer", transaction.MemberInfo.Referrer))
        sqlParameterCollection.Add(New SqlParameter("@Address", transaction.BillingAddress.Address))
        sqlParameterCollection.Add(New SqlParameter("@City", transaction.BillingAddress.City))
        sqlParameterCollection.Add(New SqlParameter("@State", transaction.BillingAddress.State))
        sqlParameterCollection.Add(New SqlParameter("@Zip", transaction.BillingAddress.Zip))
        sqlParameterCollection.Add(New SqlParameter("@Country", transaction.BillingAddress.Country))
        sqlParameterCollection.Add(New SqlParameter("@Phone", transaction.BillingAddress.Phone))
        sqlParameterCollection.Add(New SqlParameter("@Mobile", transaction.BillingAddress.Mobile))
        sqlParameterCollection.Add(New SqlParameter("@Fax", transaction.BillingAddress.Fax))
        sqlParameterCollection.Add(New SqlParameter("@Email", transaction.BillingAddress.Email))
        sqlParameterCollection.Add(New SqlParameter("@SpecialConsiderations", transaction.MemberInfo.SpecialConsiderations))
        sqlParameterCollection.Add(New SqlParameter("@PaymentMethod", transaction.CardType))
        sqlParameterCollection.Add(New SqlParameter("@CreditCardNum", transaction.CardNumLast4))
        sqlParameterCollection.Add(New SqlParameter("@ExpDate", transaction.CardExp))
        sqlParameterCollection.Add(New SqlParameter("@CSCCode", transaction.CscCode))
        sqlParameterCollection.Add(New SqlParameter("@TotalAmount", transaction.TotalAmount))
        sqlParameterCollection.Add(New SqlParameter("@StudentAgreementForm", transaction.MemberInfo.StudentAgreementForm))
        sqlParameterCollection.Add(New SqlParameter("@DoctorId", transaction.MemberInfo.DoctorId))
        sqlParameterCollection.Add(New SqlParameter("@StudentAgreementFormFileName", transaction.MemberInfo.StudentAgreementFormFileName))
        sqlParameterCollection.Add(New SqlParameter("@StudentAgreementFormFileType", transaction.MemberInfo.StudentAgreementFormFileType))
        Return sqlParameterCollection
    End Function

    Public Shared Function UpdateTransactionParameters(ByVal transaction As Transaction) As List(Of SqlParameter)
        Dim sqlParameterCollection As New List(Of SqlParameter)
        '@SeminarRegistrationId int
        '@TransactionId nvarchar(50),
        '@Status int,
        '@AuthCode nvarchar(50),
        '@ResponseStr nvarchar(50),
        '@PNFRef nvarchar(50)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrationId", transaction.TransactionId))
        sqlParameterCollection.Add(New SqlParameter("@TransactionId", transaction.AuthNetTransId))
        sqlParameterCollection.Add(New SqlParameter("@Status", transaction.Status))
        sqlParameterCollection.Add(New SqlParameter("@AuthCode", transaction.AuthNetCode))
        sqlParameterCollection.Add(New SqlParameter("@ResponseStr", transaction.RtnMesg))
        sqlParameterCollection.Add(New SqlParameter("@PNFRef", transaction.PNRef))
        Return sqlParameterCollection
    End Function

    Public Shared Function AddSeminar(ByVal transactionId As String, ByVal seminar As Seminar) As List(Of SqlParameter)
        '@SeminarRegistrationId int,
        '@SeminarType nvarchar(50),
        '@SeminarDate datetime,
        '@SeminarId int = 0
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrationId", transactionId))
        sqlParameterCollection.Add(New SqlParameter("@SeminarType", seminar.Type))
        sqlParameterCollection.Add(New SqlParameter("@SeminarDate", seminar.SeminarDate))
        sqlParameterCollection.Add(New SqlParameter("@SeminarId", seminar.Id))
        Return sqlParameterCollection

    End Function

    Public Shared Function AddLicenseParameters(ByVal license As License) As List(Of SqlParameter)
        '@SeminarRegistrationId int,
        '@FileContent varbinary(max),
        '@FileName nvarchar(100),
        '@FileType nvarchar(100)
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrationId", license.SeminarRegistrationId))
        sqlParameterCollection.Add(New SqlParameter("@FileContent", license.FileContent))
        sqlParameterCollection.Add(New SqlParameter("@FileName", license.FileName))
        sqlParameterCollection.Add(New SqlParameter("@FileType", license.FileType))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetTransactionParameters(ByVal id As String) As List(Of SqlParameter)
        '@Id int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@Id", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetSeminarParameters(ByVal id As Integer) As List(Of SqlParameter)
        '@Id int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistartionId ", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetLicenseParameters(ByVal seminarRegistartionId As Integer) As List(Of SqlParameter)
        '@SeminarRegistrationId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrationId ", seminarRegistartionId))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetStudentAgreementFormParameters(ByVal seminarRegistrationId As Integer) As List(Of SqlParameter)
        '@SeminarRegistrationId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrationId ", seminarRegistrationId))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetAuthenticationParameters(ByVal username As String, ByVal password As String) As List(Of SqlParameter)
        '@ipUserID int
        '@ipPassword
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@ipUserID", CheckForNull(username, GetType(String))))
        sqlParameterCollection.Add(New SqlParameter("@ipPassword", CheckForNull(password, GetType(String))))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetCourseParameters(ByVal id As String) As List(Of SqlParameter)
        '@ClassId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@ClassId", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetCoursebyCourseTypeParameters(ByVal courseType As Integer) As List(Of SqlParameter)
        '@ClassType int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@ClassType", courseType))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetRegistrantParameters(ByVal id As String) As List(Of SqlParameter)
        '@SeminarRegistrantId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrantId", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetRegistrantBySeminarIdParameters(ByVal id As String) As List(Of SqlParameter)
        '@SeminarId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarId", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetSeminarRegistrationBySeminarIdParameters(ByVal id As Integer) As List(Of SqlParameter)
        '@SeminarId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarId", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetSeminarRegistrationByIdParameters(ByVal id As Integer) As List(Of SqlParameter)
        '@Id int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@Id", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function UpdateRegistrantByIdParameters(ByVal id As Integer, ByVal registrantStatusId As Integer) As List(Of SqlParameter)
        '@Id int
        '@RegistrantStatusId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@Id", id))
        sqlParameterCollection.Add(New SqlParameter("@RegistrantStatusId", registrantStatusId))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetDoctorParameters(ByVal id As Integer) As List(Of SqlParameter)
        '@ipkey int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@ipkey", id))
        Return sqlParameterCollection
    End Function

    Public Shared Function AddAttendeeAddRegistrationParameters(ByVal doctorId As Integer, ByVal classId As Integer) As List(Of SqlParameter)
        '@DoctorId int
        '@ClassId int
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@DoctorId", doctorId))
        sqlParameterCollection.Add(New SqlParameter("@ClassId", classId))
        Return sqlParameterCollection
    End Function

    Public Shared Function AddRegistrantStatusAuditParameters(ByVal seminarRegistrantId As Integer, ByVal registrantStatusId As String, ByVal username As String, ByVal dateTimeStamp As Date) As List(Of SqlParameter)
        '@SeminarRegistrantId int
        '@RegistrantStatusId int
        '@Username nvarchar(50)
        '@DateTimeStamp datetime
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrantId", seminarRegistrantId))
        sqlParameterCollection.Add(New SqlParameter("@RegistrantStatusId", registrantStatusId))
        sqlParameterCollection.Add(New SqlParameter("@Username", username))
        sqlParameterCollection.Add(New SqlParameter("@DateTimeStamp", dateTimeStamp))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetDoctorLastNameParameters(ByVal lastName As String) As List(Of SqlParameter)
        '@lastname varchar(50)
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@lastname", lastName))
        Return sqlParameterCollection
    End Function

    Public Shared Function GetDoctorAddress(ByVal doctorId As Integer, ByVal isPrimaryContact As Int32) As List(Of SqlParameter)
        '@DoctorId int
        '@IsPrimaryContact bit = 1
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@DoctorId", doctorId))
        sqlParameterCollection.Add(New SqlParameter("@IsPrimaryContact", isPrimaryContact))
        Return sqlParameterCollection
    End Function

    Public Shared Function UpdateSeminarRegistrationByIdParameters(ByVal seminarRegistration As SeminarRegistration) As List(Of SqlParameter)
        '@SeminarRegistrationId int,
        '@FirstName varchar(50),
        '@LastName varchar(250),
        '@Degree varchar(250),
        '@YearGraduated varchar(50),
        '@LicenseNumber varchar(50),
        '@YearAttendedNAETBasic varchar(50),
        '@YearAttendedNAETAdv1 varchar(50),
        '@Member bit,
        '@Referrer varchar(250),
        '@Address varchar(500),
        '@City varchar(250),
        '@State varchar(250),
        '@Zip varchar(50),
        '@Country varchar(250),
        '@Phone varchar(50),
        '@Mobile varchar(50),
        '@Fax varchar(250),
        '@Email varchar(250),
        '@SpecialConsiderations text,
        '@PaymentMethod varchar(50),
        '@CreditCardNum varchar(50),
        '@ExpDate varchar(50),
        '@CSCCode varchar(50),
        '@TotalAmount decimal(18,2),
        '@TransactionId nvarchar(50),
        '@Status int,
        '@AuthCode nvarchar(50),
        '@ResponseStr nvarchar(50),
        '@PNRef nvarchar(50),
        '@StudentAgreementForm varbinary(max),
        '@DoctorId int,
        '@RegistrationDate datetime,
        '@StudentAgreementFormFileName nvarchar(100),
        '@StudentAgreementFormFileType nvarchar(100)
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@SeminarRegistrationId", seminarRegistration.SeminarRegistrationId))
        sqlParameterCollection.Add(New SqlParameter("@FirstName", seminarRegistration.FirstName))
        sqlParameterCollection.Add(New SqlParameter("@LastName", seminarRegistration.LastName))
        sqlParameterCollection.Add(New SqlParameter("@Degree", seminarRegistration.Degree))
        sqlParameterCollection.Add(New SqlParameter("@YearGraduated", seminarRegistration.YearGraduated))
        sqlParameterCollection.Add(New SqlParameter("@LicenseNumber", seminarRegistration.LicenseNumber))
        sqlParameterCollection.Add(New SqlParameter("@YearAttendedNAETBasic", seminarRegistration.YearAttendedNAETBasic))
        sqlParameterCollection.Add(New SqlParameter("@YearAttendedNAETAdv1", seminarRegistration.YearAttendedNAETAdv1))
        sqlParameterCollection.Add(New SqlParameter("@Member", seminarRegistration.Member))
        sqlParameterCollection.Add(New SqlParameter("@Referrer", seminarRegistration.Referrer))
        sqlParameterCollection.Add(New SqlParameter("@Address", seminarRegistration.Address))
        sqlParameterCollection.Add(New SqlParameter("@City", seminarRegistration.City))
        sqlParameterCollection.Add(New SqlParameter("@State", seminarRegistration.State))
        sqlParameterCollection.Add(New SqlParameter("@Zip", seminarRegistration.Zip))
        sqlParameterCollection.Add(New SqlParameter("@Country", seminarRegistration.Country))
        sqlParameterCollection.Add(New SqlParameter("@Phone", seminarRegistration.Phone))
        sqlParameterCollection.Add(New SqlParameter("@Mobile", seminarRegistration.Mobile))
        sqlParameterCollection.Add(New SqlParameter("@Fax", seminarRegistration.Fax))
        sqlParameterCollection.Add(New SqlParameter("@Email", seminarRegistration.Email))
        sqlParameterCollection.Add(New SqlParameter("@SpecialConsiderations", seminarRegistration.SpecialConsiderations))
        sqlParameterCollection.Add(New SqlParameter("@PaymentMethod", seminarRegistration.PaymentMethod))
        sqlParameterCollection.Add(New SqlParameter("@CreditCardNum", seminarRegistration.CreditCardNum))
        sqlParameterCollection.Add(New SqlParameter("@ExpDate", seminarRegistration.ExpDate))
        sqlParameterCollection.Add(New SqlParameter("@CSCCode", seminarRegistration.CSCCode))
        sqlParameterCollection.Add(New SqlParameter("@TotalAmount", seminarRegistration.TotalAmount))
        sqlParameterCollection.Add(New SqlParameter("@TransactionId", seminarRegistration.TransactionId))
        sqlParameterCollection.Add(New SqlParameter("@Status", seminarRegistration.Status))
        sqlParameterCollection.Add(New SqlParameter("@AuthCode", seminarRegistration.AuthCode))
        sqlParameterCollection.Add(New SqlParameter("@ResponseStr", seminarRegistration.ResponseStr))
        sqlParameterCollection.Add(New SqlParameter("@PNRef", seminarRegistration.PNRef))
        sqlParameterCollection.Add(New SqlParameter("@DoctorId", seminarRegistration.DoctorId))
        sqlParameterCollection.Add(New SqlParameter("@RegistrationDate", seminarRegistration.RegistrationDate))
        sqlParameterCollection.Add(New SqlParameter("@StudentAgreementFormFileName", seminarRegistration.StudentAgreementFormFileName))
        sqlParameterCollection.Add(New SqlParameter("@StudentAgreementFormFileType", seminarRegistration.StudentAgreementFormFileType))
        Return sqlParameterCollection
    End Function

    Public Shared Function FindDoctorNameParameters(ByVal lastname As String, ByVal firstname As String) As List(Of SqlParameter)
        '@LastName varchar(50)
        '@FirstName varchar(50)
        Dim sqlParameterCollection As New List(Of SqlParameter)
        sqlParameterCollection.Add(New SqlParameter("@LastName", lastname))
        sqlParameterCollection.Add(New SqlParameter("@FirstName", firstname))
        Return sqlParameterCollection
    End Function
End Class
