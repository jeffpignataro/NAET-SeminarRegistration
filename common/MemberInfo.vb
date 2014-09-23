Imports System.Data.SqlClient
Imports System.Linq

Public Class MemberInfo
    Property Degree() As String
    Property YearGraduated() As String
    Property LicenseNumber() As String
    Property YearAttendedNaetBasic() As String
    Property YearAttendedNaetAdv1() As String
    Property Member() As Boolean
    Property Referrer() As String
    Property SpecialConsiderations() As String
    Property StudentAgreementForm() As Object
    Property StudentAgreementFormFileName() As String
    Property StudentAgreementFormFileType() As String
    Property DoctorId() As Integer

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(ByVal degree As String, ByVal yearGraduated As String, ByVal licenseNumber As String, ByVal yearAttendedNaetBasic As String, ByVal yearAttendedNaetAdv1 As String, ByVal member As Boolean, ByVal referrer As String, ByVal specialConsiderations As String)
        Me.Degree = degree
        Me.YearGraduated = yearGraduated
        Me.LicenseNumber = licenseNumber
        Me.YearAttendedNaetBasic = yearAttendedNaetBasic
        Me.YearAttendedNaetAdv1 = yearAttendedNaetAdv1
        Me.Member = member
        Me.Referrer = referrer
        Me.SpecialConsiderations = specialConsiderations
    End Sub

    Public Sub New(ByVal degree As String, ByVal yearGraduated As String, ByVal licenseNumber As String, ByVal yearAttendedNaetBasic As String, ByVal yearAttendedNaetAdv1 As String, ByVal member As Boolean, ByVal referrer As String, ByVal specialConsiderations As String, ByVal studentAgreementForm As Object)
        Me.Degree = degree
        Me.YearGraduated = yearGraduated
        Me.LicenseNumber = licenseNumber
        Me.YearAttendedNaetBasic = yearAttendedNaetBasic
        Me.YearAttendedNaetAdv1 = yearAttendedNaetAdv1
        Me.Member = member
        Me.Referrer = referrer
        Me.SpecialConsiderations = specialConsiderations
        Me.StudentAgreementForm = studentAgreementForm
    End Sub

    Public Sub New(ByVal degree As String, ByVal yearGraduated As String, ByVal licenseNumber As String, ByVal yearAttendedNaetBasic As String, ByVal yearAttendedNaetAdv1 As String, ByVal member As Boolean, ByVal referrer As String, ByVal specialConsiderations As String, ByVal studentAgreementForm As Object, ByVal studentAgreementFormFileName As String, ByVal studentAgreementFormFileType As String)
        Me.Degree = degree
        Me.YearGraduated = yearGraduated
        Me.LicenseNumber = licenseNumber
        Me.YearAttendedNaetBasic = yearAttendedNaetBasic
        Me.YearAttendedNaetAdv1 = yearAttendedNaetAdv1
        Me.Member = member
        Me.Referrer = referrer
        Me.SpecialConsiderations = specialConsiderations
        Me.StudentAgreementForm = studentAgreementForm
        Me.StudentAgreementFormFileName = studentAgreementFormFileName
        Me.StudentAgreementFormFileType = studentAgreementFormFileType
    End Sub

    Public Sub New(ByVal degree As String, ByVal yearGraduated As String, ByVal licenseNumber As String, ByVal yearAttendedNaetBasic As String, ByVal yearAttendedNaetAdv1 As String, ByVal member As Boolean, ByVal referrer As String, ByVal specialConsiderations As String, ByVal studentAgreementForm As Object, ByVal studentAgreementFormFileName As String, ByVal studentAgreementFormFileType As String, ByVal doctorId As Integer)
        Me.Degree = degree
        Me.YearGraduated = yearGraduated
        Me.LicenseNumber = licenseNumber
        Me.YearAttendedNaetBasic = yearAttendedNaetBasic
        Me.YearAttendedNaetAdv1 = yearAttendedNaetAdv1
        Me.Member = member
        Me.Referrer = referrer
        Me.SpecialConsiderations = specialConsiderations
        Me.StudentAgreementForm = studentAgreementForm
        Me.StudentAgreementFormFileName = studentAgreementFormFileName
        Me.StudentAgreementFormFileType = studentAgreementFormFileType
        Me.DoctorId = doctorId
    End Sub
#End Region

    Public Function GetStudentAgreementForm(ByVal seminarRegistrationId As Integer) As MemberInfo
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetStudentAgreementFormParameters(seminarRegistrationId)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetStudentAgreementForm", parameters, SqlHelper.ConnectionString)
        Dim studentAgreementFormList As List(Of MemberInfo) = MapStudentAgreementForm(dataTable)
        If (studentAgreementFormList.Count = 0) Then
            Return Nothing
        End If
        Return studentAgreementFormList.FirstOrDefault()
    End Function

    Private Function MapStudentAgreementForm(ByVal dataTable As DataTable) As List(Of MemberInfo)
        Dim returnList As New List(Of MemberInfo)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim memberInfo As New MemberInfo
                memberInfo.StudentAgreementForm = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementForm"), GetType(Byte()))
                memberInfo.StudentAgreementFormFileName = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementFormFileName"), GetType(String))
                memberInfo.StudentAgreementFormFileType = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementFormFileType"), GetType(String))
                Dim checkString As String = Encoding.Default.GetString(memberInfo.StudentAgreementForm)
                If (checkString.Length > 0) Then 'Length = 0 means there's no form
                    returnList.Add(memberInfo)
                End If
            Next
        End If
        Return returnList
    End Function
End Class
