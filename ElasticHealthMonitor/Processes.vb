Imports System.Net
Imports System.IO
Module Processes
    Function GetClusterStatus(ByVal myData As ElasticDetails) As JSONResponse
        Dim _JSON As New JSONResponse

        Dim request As WebRequest = WebRequest.Create(myData.URL)
        request.ContentType = "application/json"

        Dim response As WebResponse
        Dim responseFromServer As String = ""

        Try
            response = request.GetResponse()
            Dim dataStream As Stream = response.GetResponseStream()

            ' Open the stream using a StreamReader for easy access.
            Dim reader As New StreamReader(dataStream)

            ' Read the content.
            responseFromServer = reader.ReadToEnd()

        Catch ex As Exception
            Dim msg As String = ex.Message
        Finally

            request.Abort()

        End Try
        If responseFromServer <> "" Then
            '  Deserialize the JSON response in to our JSONResponse Class.
            _JSON = Global.Newtonsoft.Json.JsonConvert.DeserializeObject(Of JSONResponse)(responseFromServer)
        End If

        Return _JSON
    End Function

    Sub SendAlert(ByVal Elastic As ElasticDetails, ByVal strSubject As String, ByVal message As String)

        ' -- Uses the dotNet mail sending namespace.
        Dim msgAlert As New System.Net.Mail.MailMessage()
        msgAlert.To.Add(Elastic.emailaddress)

        ' -- Make the noreply@noreply.com be anything you want it to be
        '    First Param  = FromAddress
        '    Second Param = FromName
        Dim fromMailAddrAlert As New System.Net.Mail.MailAddress("noreply@noreply.com", "Elastic Status Monitor")
        msgAlert.From = fromMailAddrAlert

        msgAlert.Subject = strSubject
        msgAlert.Body = message
        msgAlert.IsBodyHtml = True

        Dim SMPclintAlert As New System.Net.Mail.SmtpClient
        SMPclintAlert.Host = Elastic.smtpserver

        ' -- Uses and assumes the SMTP server does not need any special requirements to send the email.
        '    If your SMTP server does require special credentials, then alter this accordingly.
        SMPclintAlert.EnableSsl = False

        'SMPclint.Port = 587
        Dim emailSend As Boolean = False
        Dim tmp As String = ""

        ' -- Just does a simple try catch, with no actual error logging.
        '    Made this quote generic, so if you want to add any logging of processes, do it here for any failures etc
        Try
            SMPclintAlert.Send(msgAlert)
            emailSend = True

        Catch ex As Exception
            emailSend = False

        Finally
            msgAlert = Nothing
            SMPclintAlert = Nothing

        End Try

    End Sub
End Module
