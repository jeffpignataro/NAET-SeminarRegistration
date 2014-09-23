Imports System.Data.SqlClient
Imports System.Activities.Debugger

Public Class DoctorLocation

    Property DoctorKey As Integer
    Property AddressId() As Object
    Property DoctorId() As Object
    Property Description() As Object
    Property Company() As Object
    Property Phone() As Object
    Property Title() As Object
    Property Fax() As Object
    Property Address1() As Object
    Property Address2() As Object
    Property City() As Object
    Property State() As Object
    Property Zip() As Object
    Property Country() As Object
    Property PrimaryContact() As Object
    Property ChangedBy() As Object
    Property ChangedOn() As Object

    Public Sub New()
    End Sub

    Public Sub New(ByVal doctorKey As Integer, ByVal addressId As Object, ByVal doctorId As Object, ByVal description As Object, ByVal company As Object, ByVal phone As Object, ByVal title As Object, ByVal fax As Object, ByVal address1 As Object, ByVal address2 As Object, ByVal city As Object, ByVal state As Object, ByVal zip As Object, ByVal country As Object, ByVal primaryContact As Object, ByVal changedBy As Object, ByVal changedOn As Object)
        Me.DoctorKey = doctorKey
        Me.AddressId = addressId
        Me.DoctorId = doctorId
        Me.Description = description
        Me.Company = company
        Me.Phone = phone
        Me.Title = title
        Me.Fax = fax
        Me.Address1 = address1
        Me.Address2 = address2
        Me.City = city
        Me.State = state
        Me.Zip = zip
        Me.Country = country
        Me.PrimaryContact = primaryContact
        Me.ChangedBy = changedBy
        Me.ChangedOn = changedOn
    End Sub

    Public Function GetDoctorAddressByDoctorId(ByVal doctorId As Int32, Optional isPrimaryContact As Int32 = 1) As List(Of DoctorLocation)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetDoctorAddress(doctorId, isPrimaryContact)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetDoctorAddressByDoctorId", parameters, SqlHelper.ConnectionString)
        Dim mapDoctor As List(Of DoctorLocation) = DoctorLocation.MapDoctorAddress(dataTable)
        Return mapDoctor
    End Function

    Private Shared Function MapDoctorAddress(ByVal dataTable As DataTable) As List(Of DoctorLocation)

        Dim returnList As New List(Of DoctorLocation)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim doctorLocation As New DoctorLocation
                With doctorLocation
                    .AddressId = SqlHelper.CheckForNull(dataRow.Item("AddressID"), GetType(Integer))
                    .DoctorId = SqlHelper.CheckForNull(dataRow.Item("DoctorID"), GetType(Integer))
                    .Description = SqlHelper.CheckForNull(dataRow.Item("Description"), GetType(String))
                    .Company = SqlHelper.CheckForNull(dataRow.Item("Company"), GetType(String))
                    .Phone = SqlHelper.CheckForNull(dataRow.Item("Phone"), GetType(String))
                    .Fax = SqlHelper.CheckForNull(dataRow.Item("Fax"), GetType(String))
                    .Address1 = SqlHelper.CheckForNull(dataRow.Item("Address1"), GetType(String))
                    .Address2 = SqlHelper.CheckForNull(dataRow.Item("Address2"), GetType(String))
                    .City = SqlHelper.CheckForNull(dataRow.Item("City"), GetType(String))
                    .State = SqlHelper.CheckForNull(dataRow.Item("State"), GetType(String))
                    .Zip = SqlHelper.CheckForNull(dataRow.Item("Zip"), GetType(String))
                    .Country = SqlHelper.CheckForNull(dataRow.Item("Country"), GetType(String))
                    .PrimaryContact = SqlHelper.CheckForNull(dataRow.Item("PrimaryContact"), GetType(Boolean))
                    .ChangedOn = SqlHelper.CheckForNull(dataRow.Item("ChangedOn"), GetType(Date))
                    .ChangedBy = SqlHelper.CheckForNull(dataRow.Item("ChangedBy"), GetType(String))
                End With
                returnList.Add(doctorLocation)
            Next
        End If
        Return returnList
    End Function
End Class
