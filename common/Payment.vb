Imports System.Data.SqlClient
Imports System.Data
Imports System.Linq

Public Class Payment

#Region "Declarations"
    Public Const PaymentProcessorID = 1   'Hardcoded from DB
    Public Const FldID_ConfigurationID = 0
    Public Const FldID_ProcessorID = 1
    Public Const FldID_Configuration_Title = 2
    Public Const FldID_Configuration_Key = 3
    Public Const FldID_Configuration_Value = 4
    Public Const FldID_Configuration_Description = 5
    Public Const FldID_Configuration_Group_ID = 6
    Public Const FldID_Configuration_Sort_Order = 7
    Public Const FldID_Configuration_Date_Added = 8
    Public Const PgBaseTop As Integer = 50
    Public Const PgControlHeight As Integer = 30

    Public MODULE_PAYMENT_STATUS As String
    Public MODULE_PAYMENT_CURRENCY As String
    Public MODULE_PAYMENT_HOSTADDRESS As String
    Public MODULE_PAYMENT_HOSTPORT As String
    Public MODULE_PAYMENT_TRXTYPE As String
    Public MODULE_PAYMENT_TENDER As String
    Public MODULE_PAYMENT_PARTNER As String
    Public MODULE_PAYMENT_VENDOR As String
    Public MODULE_PAYMENT_USER As String
    Public MODULE_PAYMENT_PWD As String
    Public MODULE_PAYMENT_TIMEOUT As String
    Public MODULE_PAYMENT_PROXY_ADDRESS As String
    Public MODULE_PAYMENT_PROXY_PORT As String
    Public MODULE_PAYMENT_PROXY_LOGON As String
    Public MODULE_PAYMENT_PROXY_PASSWORD As String
    Public MODULE_PAYMENT_CSC As String
    Public MODULE_PAYMENT_LD_LIBRARY_PATH_ENV As String
    Public MODULE_PAYMENT_PFPRO_CERT_PATH_ENV As String
    Public MODULE_PAYMENT_PFPRO_EXE_PATH As String

    Public tblPayment As DataTable
    Public tblChoice As DataTable

    Public mResultMsg As String
    Public mAuthCode As String
    Public mPnRef As String
    Public mTransID As String

    Public ReadOnly Property TransID() As String
        Get
            TransID = mTransID
        End Get
    End Property
    Public ReadOnly Property ResultMsg() As String
        Get
            ResultMsg = mResultMsg
        End Get
    End Property
    Public ReadOnly Property AuthCode() As String
        Get
            AuthCode = mAuthCode
        End Get
    End Property
    Public ReadOnly Property PnRef() As String
        Get
            PnRef = mPnRef
        End Get
    End Property

#End Region

    Public Function AuthorizePayment(ByRef transaction As Transaction) As Boolean
        Dim AuthNetVersion As String = "3.1"
        ' Contains CCV support 
        Dim AuthNetLoginID As String = ConfigurationManager.AppSettings("AuthnetLogin") '"nar714"
        Dim AuthNetPassword As String = ConfigurationManager.AppSettings("AuthnetPWD") '"5yf9h6UuKB"
        ' Get this from your authorize.net merchant interface 
        Dim AuthNetTransKey As String = ConfigurationManager.AppSettings("AuthnetKey") '"7vRg9x559T2n8XWv"

        Dim objRequest As New Net.WebClient()
        Dim objInf As New NameValueCollection(30)
        Dim objRetBytes As Byte()
        Dim objRetVals As String()
        Dim strError As String
        Dim x_test_request As String


        objInf.Add("x_version", AuthNetVersion)
        objInf.Add("x_delim_data", "True")
        objInf.Add("x_login", AuthNetLoginID)
        objInf.Add("x_password", AuthNetPassword)
        objInf.Add("x_tran_key", AuthNetTransKey)
        objInf.Add("x_relay_response", "False")

        ' Switch this to False once you go live 
        x_test_request = ConfigurationManager.AppSettings("x_test_request")
        objInf.Add("x_test_request", x_test_request)

        objInf.Add("x_delim_char", ",")
        objInf.Add("x_encap_char", "|")

        ' Billing Address 
        objInf.Add("x_first_name", transaction.BillingAddress.FirstName)
        objInf.Add("x_last_name", transaction.BillingAddress.LastName)
        objInf.Add("x_address", transaction.BillingAddress.Address)
        objInf.Add("x_city", transaction.BillingAddress.City)
        objInf.Add("x_state", transaction.BillingAddress.State)
        objInf.Add("x_zip", transaction.BillingAddress.Zip)
        objInf.Add("x_country", transaction.BillingAddress.Country)
        objInf.Add("x_email", transaction.BillingAddress.Email)

        objInf.Add("x_description", transaction.Notes)

        ' Card Details 
        objInf.Add("x_card_num", transaction.CardNum)        '"4111111111111111"
        objInf.Add("x_exp_date", transaction.CardExp)   '"01/06")

        ' Authorisation code of the card (CCV) 
        objInf.Add("x_card_code", transaction.CscCode) '"123")

        objInf.Add("x_method", "CC")
        objInf.Add("x_type", "AUTH_CAPTURE")
        objInf.Add("x_amount", transaction.TotalAmount)

        ' Currency setting. Check the guide for other supported currencies 
        objInf.Add("x_currency_code", "USD")

        Try
            If (transaction.Mode = EnumHelper.TransactionMode.Dev) Then
                ' Pure Test Server 
                objRequest.BaseAddress = "https://test.authorize.net/gateway/transact.dll"
            ElseIf (transaction.Mode = EnumHelper.TransactionMode.Live) Then
                ' Actual Server 
                '(uncomment the following line and also set above Testmode=off to go live) 
                objRequest.BaseAddress = "https://secure.authorize.net/gateway/transact.dll"
            Else 'If no mode is specified treat as live
                ' Actual Server 
                '(uncomment the following line and also set above Testmode=off to go live) 
                objRequest.BaseAddress = "https://secure.authorize.net/gateway/transact.dll"
            End If

            objRetBytes = objRequest.UploadValues(objRequest.BaseAddress, "POST", objInf)
            objRetVals = Encoding.ASCII.GetString(objRetBytes).Split(",".ToCharArray())

            If objRetVals(0).Trim(Char.Parse("|")) = "1" Then
                ' Returned Authorisation Code 
                transaction.AuthNetCode = objRetVals(4).Trim(Char.Parse("|"))
                ' Returned Transaction ID 
                transaction.AuthNetTransId = objRetVals(6).Trim(Char.Parse("|"))
                ' not sure why this is done... very redundant.
                transaction.PNRef = objRetVals(6).Trim(Char.Parse("|"))
                Return True
            Else
                ' Error! 
                strError = (objRetVals(3).Trim(Char.Parse("|")) & " (") + objRetVals(2).Trim(Char.Parse("|")) & ")"

                If objRetVals(2).Trim(Char.Parse("|")) = "44" Then
                    ' CCV transaction decline 
                    strError += "Our Card Code Verification (CCV) returned " & "the following error: "

                    Select Case objRetVals(38).Trim(Char.Parse("|"))
                        Case "M"
                            strError += "Merchant Login Error."
                            Exit Select
                        Case "N"
                            strError += "Card Code does not match."
                            Exit Select
                        Case "P"
                            strError += "Card Code was not processed."
                            Exit Select
                        Case "S"
                            strError += "Card Code should be on card but was not indicated."
                            Exit Select
                        Case "U"
                            strError += "Issuer was not certified for Card Code."
                            Exit Select
                    End Select
                End If

                If objRetVals(2).Trim(Char.Parse("|")) = "45" Then
                    If strError.Length > 1 Then
                        strError += "<br />n"
                    End If

                    ' AVS transaction decline 
                    strError += "Our Address Verification System (AVS) " & "returned the following error: "

                    Select Case objRetVals(5).Trim(Char.Parse("|"))
                        Case "A"
                            strError += " the zip code entered does not match " & "the billing address."
                            Exit Select
                        Case "B"
                            strError += " no information was provided for the AVS check."
                            Exit Select
                        Case "E"
                            strError += " a general error occurred in the AVS system."
                            Exit Select
                        Case "G"
                            strError += " the credit card was issued by a non-US bank."
                            Exit Select
                        Case "N"
                            strError += " neither the entered street address nor zip " & "code matches the billing address."
                            Exit Select
                        Case "P"
                            strError += " AVS is not applicable for this transaction."
                            Exit Select
                        Case "R"
                            strError += " please retry the transaction; the AVS system " & "was unavailable or timed out."
                            Exit Select
                        Case "S"
                            strError += " the AVS service is not supported by your " & "credit card issuer."
                            Exit Select
                        Case "U"
                            strError += " address information is unavailable for the " & "credit card."
                            Exit Select
                        Case "W"
                            strError += " the 9 digit zip code matches, but the " & "street address does not."
                            Exit Select
                        Case "Z"
                            strError += " the zip code matches, but the address does not."
                            Exit Select
                    End Select
                End If

                ' strError contains the actual error 
                transaction.RtnMesg = strError
                Return False
            End If
        Catch ex As Exception
            transaction.RtnMesg = ex.Message
            Return False
        End Try
    End Function

End Class
