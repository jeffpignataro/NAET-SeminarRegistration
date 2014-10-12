Imports System.Web.Services
Imports System.Linq
Imports Newtonsoft.Json

Public Class Ajax
    Inherits System.Web.UI.Page
    Shared ReadOnly _SeminarRegistration As SeminarRegistration = New SeminarRegistration()

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        AuthenticationHelper.ValidateAdminWithRedirect(Session("username"), Session("password"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <WebMethod>
    Public Shared Function CheckForDoctorRecord(ByVal jsonString As String) As String
        Dim seminarRegistration As SeminarRegistration = JsonConvert.DeserializeObject(Of SeminarRegistration)(jsonString)
        'Doctor needs to be added to the database
        Dim _doctor As Doctor = New Doctor()
        Dim findDoctorByName As List(Of Doctor) = _doctor.FindDoctorByName(seminarRegistration.LastName, seminarRegistration.FirstName.Substring(0, 1))
        If (findDoctorByName.Count > 0) Then
            Return JsonConvert.SerializeObject(findDoctorByName)
        End If
        Return Nothing
    End Function

    <WebMethod>
    Public Shared Function UpdateDoctorIdInSeminarRegistration(ByVal doctorId As Integer, ByVal seminarRegistrationId As Integer) As Boolean
        Dim seminarRegistrationById As SeminarRegistration = _SeminarRegistration.GetSeminarRegistrationById(seminarRegistrationId)
        seminarRegistrationById.DoctorId = doctorId
        Return _SeminarRegistration.UpdateSeminarRegistrationById(seminarRegistrationById)
    End Function

    <WebMethod>
    Public Shared Function AddDoctorAsNew(ByVal seminarRegistrationId As Integer) As String
        Dim seminarRegistrationById As SeminarRegistration = _SeminarRegistration.GetSeminarRegistrationById(seminarRegistrationId)
        Dim _doctor As Doctor = New Doctor()
        Dim createDoctorId As Integer = _doctor.CreateDoctorFromSeminarRegistration(seminarRegistrationById)
        If (createDoctorId > 0) Then
            Dim returnObj = New With {.doctorId = createDoctorId.ToString()}
            Return JsonConvert.SerializeObject(returnObj)
        Else
            Dim returnObj = New With {.ErrorMessage = "An error has occurred."}
            Return JsonConvert.SerializeObject(returnObj)
        End If
    End Function

End Class