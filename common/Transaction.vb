Imports System.Data.SqlClient
Imports System.Linq

Public Class Transaction

#Region "Properties"

    Property Id() As Integer
    Property TransactionId() As String
    Property BillingAddress() As BillingAddress
    Property ContactAddress() As ContactAddress
    Property MemberInfo() As MemberInfo
    Property CardNum() As String
    ReadOnly Property CardNumLast4() As String
        Get
            'With Manual and Check payment methods this will fail without .Length check
            Return If(CardNum.Length >= 4, CardNum.Substring(CardNum.Length - 4), "")
        End Get
    End Property
    Property CardExpMo() As String
    Property CardExpYear() As String
    ReadOnly Property CardExp() As String
        Get
            Return Me.CardExpMo & "/" & Me.CardExpYear
        End Get
    End Property
    Property CardType() As String
    Property CscCode() As String
    Property SameAs() As Boolean
    Property TotalAmount() As Decimal
    Property AuthNetCode() As String
    Property AuthNetTransId() As String
    Property RtnMesg() As String
    Property Notes() As String
    Property Mode() As EnumHelper.TransactionMode
    Property Status() As Integer
    Property PNRef() As String
    Property License() As License
    Property RegistrationDate() As Date

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

    Public Sub New(ByVal billingAddress As BillingAddress, ByVal contactAddress As ContactAddress, ByVal cardNum As String, ByVal cardExpMo As String, ByVal cardExpYear As String, ByVal cardType As String, ByVal cscCode As String, ByVal sameAs As Boolean, ByVal totalAmount As Decimal, ByVal authNetCode As String, ByVal authNetTransId As String, ByVal rtnMesg As String, ByVal notes As String, ByVal mode As EnumHelper.TransactionMode)
        Me.BillingAddress = billingAddress
        Me.ContactAddress = contactAddress
        Me.CardNum = cardNum
        Me.CardExpMo = cardExpMo
        Me.CardExpYear = cardExpYear
        Me.CardType = cardType
        Me.CscCode = cscCode
        Me.SameAs = sameAs
        Me.TotalAmount = totalAmount
        Me.AuthNetCode = authNetCode
        Me.AuthNetTransId = authNetTransId
        Me.RtnMesg = rtnMesg
        Me.Notes = notes
        Me.Mode = mode
    End Sub

    Public Sub New(ByVal transactionId As String, ByVal billingAddress As BillingAddress, ByVal contactAddress As ContactAddress, ByVal cardNum As String, ByVal cardExpMo As String, ByVal cardExpYear As String, ByVal cardType As String, ByVal cscCode As String, ByVal sameAs As Boolean, ByVal totalAmount As Decimal, ByVal authNetCode As String, ByVal authNetTransId As String, ByVal rtnMesg As String, ByVal notes As String, ByVal mode As EnumHelper.TransactionMode)
        Me.TransactionId = transactionId
        Me.BillingAddress = billingAddress
        Me.ContactAddress = contactAddress
        Me.CardNum = cardNum
        Me.CardExpMo = cardExpMo
        Me.CardExpYear = cardExpYear
        Me.CardType = cardType
        Me.CscCode = cscCode
        Me.SameAs = sameAs
        Me.TotalAmount = totalAmount
        Me.AuthNetCode = authNetCode
        Me.AuthNetTransId = authNetTransId
        Me.RtnMesg = rtnMesg
        Me.Notes = notes
        Me.Mode = mode
    End Sub

    Public Sub New(ByVal transactionId As String, ByVal billingAddress As BillingAddress, ByVal contactAddress As ContactAddress, ByVal cardNum As String, ByVal cardExpMo As String, ByVal cardExpYear As String, ByVal cardType As String, ByVal cscCode As String, ByVal sameAs As Boolean, ByVal totalAmount As Decimal, ByVal authNetCode As String, ByVal authNetTransId As String, ByVal rtnMesg As String, ByVal notes As String, ByVal mode As EnumHelper.TransactionMode, ByVal status As Integer, ByVal pnRef As String)
        Me.TransactionId = transactionId
        Me.BillingAddress = billingAddress
        Me.ContactAddress = contactAddress
        Me.CardNum = cardNum
        Me.CardExpMo = cardExpMo
        Me.CardExpYear = cardExpYear
        Me.CardType = cardType
        Me.CscCode = cscCode
        Me.SameAs = sameAs
        Me.TotalAmount = totalAmount
        Me.AuthNetCode = authNetCode
        Me.AuthNetTransId = authNetTransId
        Me.RtnMesg = rtnMesg
        Me.Notes = notes
        Me.Mode = mode
        Me.Status = status
        Me.PNRef = pnRef
    End Sub

    Public Sub New(ByVal transactionId As String, ByVal billingAddress As BillingAddress, ByVal contactAddress As ContactAddress, ByVal memberInfo As MemberInfo, ByVal cardNum As String, ByVal cardExpMo As String, ByVal cardExpYear As String, ByVal cardType As String, ByVal cscCode As String, ByVal sameAs As Boolean, ByVal totalAmount As Decimal, ByVal authNetCode As String, ByVal authNetTransId As String, ByVal rtnMesg As String, ByVal notes As String, ByVal mode As EnumHelper.TransactionMode, ByVal status As Integer, ByVal pnRef As String)
        Me.TransactionId = transactionId
        Me.BillingAddress = billingAddress
        Me.ContactAddress = contactAddress
        Me.MemberInfo = memberInfo
        Me.CardNum = cardNum
        Me.CardExpMo = cardExpMo
        Me.CardExpYear = cardExpYear
        Me.CardType = cardType
        Me.CscCode = cscCode
        Me.SameAs = sameAs
        Me.TotalAmount = totalAmount
        Me.AuthNetCode = authNetCode
        Me.AuthNetTransId = authNetTransId
        Me.RtnMesg = rtnMesg
        Me.Notes = notes
        Me.Mode = mode
        Me.Status = status
        Me.PNRef = pnRef
    End Sub

    Public Sub New(ByVal transactionId As String, ByVal billingAddress As BillingAddress, ByVal contactAddress As ContactAddress, ByVal memberInfo As MemberInfo, ByVal cardNum As String, ByVal cardExpMo As String, ByVal cardExpYear As String, ByVal cardType As String, ByVal cscCode As String, ByVal sameAs As Boolean, ByVal totalAmount As Decimal, ByVal authNetCode As String, ByVal authNetTransId As String, ByVal rtnMesg As String, ByVal notes As String, ByVal mode As EnumHelper.TransactionMode, ByVal status As Integer, ByVal pnRef As String, ByVal license As License)
        Me.TransactionId = transactionId
        Me.BillingAddress = billingAddress
        Me.ContactAddress = contactAddress
        Me.MemberInfo = memberInfo
        Me.CardNum = cardNum
        Me.CardExpMo = cardExpMo
        Me.CardExpYear = cardExpYear
        Me.CardType = cardType
        Me.CscCode = cscCode
        Me.SameAs = sameAs
        Me.TotalAmount = totalAmount
        Me.AuthNetCode = authNetCode
        Me.AuthNetTransId = authNetTransId
        Me.RtnMesg = rtnMesg
        Me.Notes = notes
        Me.Mode = mode
        Me.Status = status
        Me.PNRef = pnRef
        Me.License = license
    End Sub

#End Region

    Public Function AddTransaction(ByRef transaction As Transaction) As DataTable
        Dim parameters As List(Of SqlParameter) = SqlHelper.AddTransactionParameters(transaction)
        Return SqlHelper.ExecuteStoredProcedure("sp_AddSeminarRegistration", parameters, SqlHelper.ConnectionString)
    End Function

    Public Function UpdateTransaction(ByVal transaction As Transaction) As DataTable
        Dim parameters As List(Of SqlParameter) = SqlHelper.UpdateTransactionParameters(transaction)
        Return SqlHelper.ExecuteStoredProcedure("sp_UpdateSeminarRegistration", parameters, SqlHelper.ConnectionString)
    End Function

    Public Function GetTransactions() As List(Of Transaction)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetTransactions", New List(Of SqlParameter)(), SqlHelper.ConnectionString)
        Return Transaction.MapTransaction(dataTable)
    End Function

    Public Function GetTransaction(ByVal id As String) As Transaction
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetTransactionParameters(id)
        Dim dataTable = SqlHelper.ExecuteStoredProcedure("sp_GetTransaction", parameters, SqlHelper.ConnectionString)
        Dim mapTransaction As List(Of Transaction) = Transaction.MapTransaction(dataTable)
        Return mapTransaction.FirstOrDefault()
    End Function

    Public Shared Function MapTransaction(ByVal dataTable As DataTable) As List(Of Transaction)
        Dim returnList As New List(Of Transaction)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim transaction As New Transaction
                Dim billingAddress As New BillingAddress
                Dim contactAddress As New ContactAddress
                Dim memberInfo As New MemberInfo

                With billingAddress
                    .FirstName = SqlHelper.CheckForNull(dataRow.Item("FirstName"), GetType(String))
                    .LastName = SqlHelper.CheckForNull(dataRow.Item("LastName"), GetType(String))
                    .Address = SqlHelper.CheckForNull(dataRow.Item("Address"), GetType(String))
                    .City = SqlHelper.CheckForNull(dataRow.Item("City"), GetType(String))
                    .State = SqlHelper.CheckForNull(dataRow.Item("State"), GetType(String))
                    .Zip = SqlHelper.CheckForNull(dataRow.Item("Zip"), GetType(String))
                    .Country = SqlHelper.CheckForNull(dataRow.Item("Country"), GetType(String))
                    .Phone = SqlHelper.CheckForNull(dataRow.Item("Phone"), GetType(String))
                    .Mobile = SqlHelper.CheckForNull(dataRow.Item("Mobile"), GetType(String))
                    .Fax = SqlHelper.CheckForNull(dataRow.Item("Fax"), GetType(String))
                    .Email = SqlHelper.CheckForNull(dataRow.Item("Email"), GetType(String))
                End With

                With contactAddress
                    .FirstName = SqlHelper.CheckForNull(dataRow.Item("FirstName"), GetType(String))
                    .LastName = SqlHelper.CheckForNull(dataRow.Item("LastName"), GetType(String))
                    .Address = SqlHelper.CheckForNull(dataRow.Item("Address"), GetType(String))
                    .City = SqlHelper.CheckForNull(dataRow.Item("City"), GetType(String))
                    .State = SqlHelper.CheckForNull(dataRow.Item("State"), GetType(String))
                    .Zip = SqlHelper.CheckForNull(dataRow.Item("Zip"), GetType(String))
                    .Country = SqlHelper.CheckForNull(dataRow.Item("Country"), GetType(String))
                    .Phone = SqlHelper.CheckForNull(dataRow.Item("Phone"), GetType(String))
                    .Mobile = SqlHelper.CheckForNull(dataRow.Item("Mobile"), GetType(String))
                    .Fax = SqlHelper.CheckForNull(dataRow.Item("Fax"), GetType(String))
                    .Email = SqlHelper.CheckForNull(dataRow.Item("Email"), GetType(String))
                End With

                With memberInfo
                    .Degree = SqlHelper.CheckForNull(dataRow.Item("Degree"), GetType(String))
                    .YearGraduated = SqlHelper.CheckForNull(dataRow.Item("YearGraduated"), GetType(String))
                    .LicenseNumber = SqlHelper.CheckForNull(dataRow.Item("LicenseNumber"), GetType(String))
                    .YearAttendedNaetBasic = SqlHelper.CheckForNull(dataRow.Item("YearAttendedNAETBasic"), GetType(String))
                    .YearAttendedNaetAdv1 = SqlHelper.CheckForNull(dataRow.Item("YearAttendedNAETAdv1"), GetType(String))
                    .Member = SqlHelper.CheckForNull(dataRow.Item("Member"), GetType(Boolean))
                    .Referrer = SqlHelper.CheckForNull(dataRow.Item("Referrer"), GetType(String))
                    If (dataRow.Table.Columns.Contains("StudentAgreementForm")) Then 'When getting all transactions we don't have to have to get all forms
                        .StudentAgreementForm = SqlHelper.CheckForNull(dataRow.Item("StudentAgreementForm"), GetType(Byte()))
                    End If
                    .SpecialConsiderations = SqlHelper.CheckForNull(dataRow.Item("SpecialConsiderations"), GetType(String))
                End With

                With transaction
                    .BillingAddress = billingAddress
                    .ContactAddress = contactAddress
                    .MemberInfo = memberInfo
                    .Id = SqlHelper.CheckForNull(dataRow.Item("SeminarRegistrationId"), GetType(Integer))
                    .CardType = SqlHelper.CheckForNull(dataRow.Item("PaymentMethod"), GetType(String))
                    .CardNum = SqlHelper.CheckForNull(dataRow.Item("CreditCardNum"), GetType(String))
                    .CardExpMo = CType(SqlHelper.CheckForNull(dataRow.Item("ExpDate"), GetType(Date)), Date).Month
                    .CardExpYear = CType(SqlHelper.CheckForNull(dataRow.Item("ExpDate"), GetType(Date)), Date).Year
                    .CscCode = SqlHelper.CheckForNull(dataRow.Item("CSCCode"), GetType(String))
                    .TotalAmount = SqlHelper.CheckForNull(dataRow.Item("TotalAmount"), GetType(String))
                    .TransactionId = SqlHelper.CheckForNull(dataRow.Item("TransactionId"), GetType(Guid))
                    .Status = SqlHelper.CheckForNull(dataRow.Item("Status"), GetType(Integer))
                    .AuthNetCode = SqlHelper.CheckForNull(dataRow.Item("AuthCode"), GetType(String))
                    .RtnMesg = SqlHelper.CheckForNull(dataRow.Item("ResponseStr"), GetType(String))
                    .PNRef = SqlHelper.CheckForNull(dataRow.Item("PnRef"), GetType(String))
                    .RegistrationDate = SqlHelper.CheckForNull(dataRow.Item("RegistrationDate"), GetType(Date))
                End With

                returnList.Add(transaction)
            Next
        End If
        Return returnList
    End Function
End Class