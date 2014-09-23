Public Class ContactAddress
    Property FirstName() As String
    Property LastName() As String
    Property Address() As String
    Property Address2() As String
    Property City() As String
    Property State() As String
    Property Zip() As String
    Property Country() As String
    Property Company() As String
    Property Phone() As String
    Property Mobile() As String
    Property Fax() As String
    Property Email() As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal address As String, ByVal address2 As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal country As String, ByVal company As String, ByVal phone As String, ByVal mobile As String, ByVal fax As String, ByVal email As String)
        Me.FirstName = firstName
        Me.LastName = lastName
        Me.Address = address
        Me.Address2 = address2
        Me.City = city
        Me.State = state
        Me.Zip = zip
        Me.Country = country
        Me.Company = company
        Me.Phone = phone
        Me.Mobile = mobile
        Me.Fax = fax
        Me.Email = email
    End Sub
End Class
