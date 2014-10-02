Imports System.Data.SqlClient
Imports System.Linq
Imports System.Activities.Statements

Public Class Doctor

    Property DoctorKey As Integer
    Property Username As String
    Property Password As String
    Property FirstName As String
    Property LastName As String
    Property Title As String
    Property Company As String
    Property Phone As String
    Property Fax As String
    Property HomePage As String
    Property Email As String
    Property NewsLetterDate As String
    Property Status As Boolean
    Property ChangedBy As String
    Property ChangedOn As Date
    Property GroupNames As String
    Property Expire As String
    Property NTuser As String
    Property NTpassword As String
    Property Lastlogin As Date
    Property HasAccess As Boolean
    Property StudyAccess As Integer
    Property ShowLink As Boolean
    Property AllowNewsletterRenewal As Boolean
    Property LastRenewalEmail As Date
    Property AutismApproved As Boolean
    Property EPW As String
    Property RequiredData As String
    Property ChangeRequestPW As String
    Property ChangeRequestTime As Date
    Property PWReset As Date
    Property PWResetIP As String
    Property AcupunctureTrained As Boolean
    Property AcupunctureDates As String
    Property DoctorTierID As Integer
    Property PractitionerDisposition As String
    Property Override_RenewalRules As Boolean
    Property DoctorLocation() As DoctorLocation

    Public Sub New()
    End Sub

    Public Sub New(ByVal doctorKey As Integer, ByVal username As String, ByVal password As String, ByVal firstname As String, ByVal lastName As String, ByVal title As String, ByVal company As String, ByVal phone As String, ByVal fax As String, ByVal homePage As String, ByVal email As String, ByVal newsLetterDate As String, ByVal status As Boolean, ByVal changedBy As String, ByVal changedOn As Date, ByVal groupNames As String, ByVal expire As String, ByVal nTuser As String, ByVal nTpassword As String, ByVal lastlogin As Date, ByVal hasAccess As Boolean, ByVal studyAccess As Integer, ByVal showLink As Boolean, ByVal allowNewsletterRenewal As Boolean, ByVal lastRenewalEmail As Date, ByVal autismApproved As Boolean, ByVal epw As String, ByVal requiredData As String, ByVal changeRequestPw As String, ByVal changeRequestTime As Object, ByVal pwReset As Date, ByVal pwResetIp As String, ByVal acupunctureTrained As Boolean, ByVal acupunctureDates As String, ByVal doctorTierId As Integer, ByVal practitionerDisposition As String, ByVal overrideRenewalRules As Boolean)
        Me.DoctorKey = doctorKey
        Me.Username = username
        Me.Password = password
        Me.FirstName = firstname
        Me.LastName = lastName
        Me.Title = title
        Me.Company = company
        Me.Phone = phone
        Me.Fax = fax
        Me.HomePage = homePage
        Me.Email = email
        Me.NewsLetterDate = newsLetterDate
        Me.Status = status
        Me.ChangedBy = changedBy
        Me.ChangedOn = changedOn
        Me.GroupNames = groupNames
        Me.Expire = expire
        Me.NTuser = nTuser
        Me.NTpassword = nTpassword
        Me.Lastlogin = lastlogin
        Me.HasAccess = hasAccess
        Me.StudyAccess = studyAccess
        Me.ShowLink = showLink
        Me.AllowNewsletterRenewal = allowNewsletterRenewal
        Me.LastRenewalEmail = lastRenewalEmail
        Me.AutismApproved = autismApproved
        Me.EPW = epw
        Me.RequiredData = requiredData
        Me.ChangeRequestPW = changeRequestPw
        Me.ChangeRequestTime = changeRequestTime
        Me.PWReset = pwReset
        Me.PWResetIP = pwResetIp
        Me.AcupunctureTrained = acupunctureTrained
        Me.AcupunctureDates = acupunctureDates
        Me.DoctorTierID = doctorTierId
        Me.PractitionerDisposition = practitionerDisposition
        Me.Override_RenewalRules = overrideRenewalRules
    End Sub

    Private Shared Function MapDoctor(ByVal dataTable As DataTable) As List(Of Doctor)
        Dim returnList As New List(Of Doctor)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim doctor As New Doctor
                With doctor
                    .DoctorKey = SqlHelper.CheckForNull(dataRow.Item("DoctorKey"), GetType(Integer))
                    .Username = SqlHelper.CheckForNull(dataRow.Item("Username"), GetType(String))
                    .Password = SqlHelper.CheckForNull(dataRow.Item("Password"), GetType(String))
                    .FirstName = SqlHelper.CheckForNull(dataRow.Item("Firstname"), GetType(String))
                    .LastName = SqlHelper.CheckForNull(dataRow.Item("LastName"), GetType(String))
                    .Title = SqlHelper.CheckForNull(dataRow.Item("Title"), GetType(String))
                    .Company = SqlHelper.CheckForNull(dataRow.Item("Company"), GetType(String))
                    .Phone = SqlHelper.CheckForNull(dataRow.Item("Phone"), GetType(String))
                    .Fax = SqlHelper.CheckForNull(dataRow.Item("Fax"), GetType(String))
                    .HomePage = SqlHelper.CheckForNull(dataRow.Item("HomePage"), GetType(String))
                    .Email = SqlHelper.CheckForNull(dataRow.Item("Email"), GetType(String))
                    .NewsLetterDate = SqlHelper.CheckForNull(dataRow.Item("NewsLetterDate"), GetType(String))
                    .Status = SqlHelper.CheckForNull(dataRow.Item("Status"), GetType(Boolean))
                    .ChangedBy = SqlHelper.CheckForNull(dataRow.Item("ChangedBy"), GetType(String))
                    .ChangedOn = SqlHelper.CheckForNull(dataRow.Item("ChangedOn"), GetType(Date))
                    .GroupNames = SqlHelper.CheckForNull(dataRow.Item("GroupNames"), GetType(String))
                    .Expire = SqlHelper.CheckForNull(dataRow.Item("Expire"), GetType(String))
                    .NTuser = SqlHelper.CheckForNull(dataRow.Item("NTuser"), GetType(String))
                    .NTpassword = SqlHelper.CheckForNull(dataRow.Item("NTpassword"), GetType(String))
                    .Lastlogin = SqlHelper.CheckForNull(dataRow.Item("Lastlogin"), GetType(Date))
                    .HasAccess = SqlHelper.CheckForNull(dataRow.Item("HasAccess"), GetType(Boolean))
                    .StudyAccess = SqlHelper.CheckForNull(dataRow.Item("StudyAccess"), GetType(Integer))
                    .ShowLink = SqlHelper.CheckForNull(dataRow.Item("ShowLink"), GetType(Boolean))
                    .AllowNewsletterRenewal = SqlHelper.CheckForNull(dataRow.Item("AllowNewsletterRenewal"), GetType(Boolean))
                    .LastRenewalEmail = SqlHelper.CheckForNull(dataRow.Item("LastRenewalEmail"), GetType(Date))
                    .AutismApproved = SqlHelper.CheckForNull(dataRow.Item("AutismApproved"), GetType(Boolean))
                    .EPW = SqlHelper.CheckForNull(dataRow.Item("EPW"), GetType(String))
                    .RequiredData = SqlHelper.CheckForNull(dataRow.Item("RequiredData"), GetType(String))
                    .ChangeRequestPW = SqlHelper.CheckForNull(dataRow.Item("ChangeRequestPW"), GetType(String))
                    .ChangeRequestTime = SqlHelper.CheckForNull(dataRow.Item("ChangeRequestTime"), GetType(Date))
                    .PWReset = SqlHelper.CheckForNull(dataRow.Item("PWReset"), GetType(Date))
                    .PWResetIP = SqlHelper.CheckForNull(dataRow.Item("PWResetIP"), GetType(String))
                    .AcupunctureTrained = SqlHelper.CheckForNull(dataRow.Item("AcupunctureTrained"), GetType(Boolean))
                    .AcupunctureDates = SqlHelper.CheckForNull(dataRow.Item("AcupunctureDates"), GetType(String))
                    .DoctorTierID = SqlHelper.CheckForNull(dataRow.Item("DoctorTierID"), GetType(Integer))
                    .PractitionerDisposition = SqlHelper.CheckForNull(dataRow.Item("PractitionerDisposition"), GetType(String))
                    .Override_RenewalRules = SqlHelper.CheckForNull(dataRow.Item("Override_RenewalRules"), GetType(Boolean))
                End With
                returnList.Add(doctor)
            Next
        End If
        Return returnList
    End Function

    Public Function GetDoctorByDoctorId(ByVal id As Integer) As Doctor
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetDoctorParameters(id)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_DoctorFromkey", parameters, SqlHelper.ConnectionString)
        Dim mapDoctor As List(Of Doctor) = Doctor.MapDoctor(dataTable)
        Return mapDoctor.FirstOrDefault()
    End Function

    Public Shared Function AddAttendeeAddRegistration(ByRef doctorId As Integer, ByVal classId As Integer) As DataTable
        Dim parameters As List(Of SqlParameter) = SqlHelper.AddAttendeeAddRegistrationParameters(doctorId, classId)
        Return SqlHelper.ExecuteStoredProcedure("sp_AddAttendeeAddRegistration", parameters, SqlHelper.ConnectionString)
    End Function

    Public Shared Function AddDoctor(ByRef doctor As Doctor) As DataTable
        Dim parameters As List(Of SqlParameter) = SqlHelper.AddDoctorParameters(doctor)
        Return SqlHelper.ExecuteStoredProcedure("sp_AddDoctor", parameters, SqlHelper.ConnectionString)
    End Function

    Public Function FindDoctorByLastName(ByVal doctorLastName As String) As List(Of Doctor)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetDoctorLastNameParameters(doctorLastName)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_DoctorFromLastName", parameters, SqlHelper.ConnectionString)
        Dim mapDoctor As List(Of Doctor) = Doctor.MapDoctor(dataTable)
        Return mapDoctor
    End Function

    'This function is intended for trying to determine if a doctor has already been added to the doctor database before adding them automaticallly.
    Public Function FindDoctorByName(ByVal doctorLastName As String, ByVal doctorFirstName As String) As List(Of Doctor)
        Dim parameters As List(Of SqlParameter) = SqlHelper.FindDoctorNameParameters(doctorLastName, doctorFirstName)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_FindDoctorByName", parameters, SqlHelper.ConnectionString)
        Dim mapDoctor As List(Of Doctor) = Doctor.MapDoctor(dataTable)
        Return mapDoctor
    End Function

    Public Function CreateDoctorFromSeminarRegistration(ByVal seminarRegistrationById As SeminarRegistration) As Integer
        Dim newDoctor As Doctor = New Doctor() With {
            .Username = seminarRegistrationById.FirstName.Substring(0, 1) + seminarRegistrationById.LastName,
            .Password = "",
            .FirstName = seminarRegistrationById.FirstName,
            .LastName = seminarRegistrationById.LastName,
            .Title = seminarRegistrationById.Degree,
            .Company = "",
            .Phone = seminarRegistrationById.Phone,
            .Fax = seminarRegistrationById.Fax,
            .HomePage = "",
            .Email = seminarRegistrationById.Email,
            .NewsLetterDate = Now.AddDays(365),
            .Status = seminarRegistrationById.Status,
            .ChangedBy = "Auto-Create",
            .ChangedOn = Now,
            .GroupNames = ",Doctor,",
            .Expire = "",
            .NTuser = "",
            .NTpassword = "",
            .Lastlogin = Date.MinValue,
            .HasAccess = True,
            .StudyAccess = 0,
            .ShowLink = True,
            .AllowNewsletterRenewal = True,
            .LastRenewalEmail = Now.AddDays(365),
            .AutismApproved = False,
            .EPW = "",
            .RequiredData = "",
            .ChangeRequestPW = "",
            .ChangeRequestTime = Date.MinValue,
            .PWReset = Date.MinValue,
            .PWResetIP = "",
            .AcupunctureTrained = 0,
            .AcupunctureDates = "",
            .DoctorTierID = 1,
            .PractitionerDisposition = "",
            .Override_RenewalRules = False
        }
        Dim dataTable As DataTable = AddDoctor(newDoctor)
        If Not (IsNothing(dataTable)) Then
            Return dataTable.Rows(0).Item("DoctorKey")
        End If
    End Function
End Class