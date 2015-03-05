Imports System.IO
Imports System.Net


Public Class Form1

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Timer1.Stop()
        status_value.Text = "Working"
        Application.DoEvents()

        Dim ESData As New ElasticDetails
        Dim JSONData As New JSONResponse

        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                ESData.URL = My.Application.CommandLineArgs(0).ToString
                ESData.emailaddress = My.Application.CommandLineArgs(1).ToString
                ESData.smtpserver = My.Application.CommandLineArgs(2).ToString
                If My.Application.CommandLineArgs.Count = 4 Then
                    ESData.sendwhengreen = My.Application.CommandLineArgs(3).ToString
                End If

            End If

            If ESData.URL <> "" Then ' if we have a URL in the first parameter.
                url_value.Text = ESData.URL
                email_value.Text = ESData.emailaddress
                smtp_value.Text = ESData.smtpserver
                Application.DoEvents()

                ' -- Make a call to the URL to get the JSON response back
                JSONData = GetClusterStatus(ESData)

                ' -- If our JSONClass object has something in and is not nothing.
                If IsNothing(JSONData) = False Then

                    '-- Check to see the status section.
                    If LCase(JSONData.status) <> "green" Then

                        ' -- Send the alert out
                        ' -- Construct a basic message for the email body
                        Dim msg As String = "URL: " & ESData.URL & "<br />" & _
                                            "Time: " & System.DateTime.Now & "<br />" & _
                                            "Status : " & JSONData.status & "<br /><br />" & _
                                            "cluster_name : " & JSONData.cluster_name & "<br />" & _
                                            "status : " & JSONData.status & "<br />" & _
                                            "timed_out : " & JSONData.timed_out & "<br />" & _
                                            "number_of_nodes : " & JSONData.number_of_nodes & "<br />" & _
                                            "number_of_data_nodes : " & JSONData.number_of_data_nodes & "<br />" & _
                                            "active_primary_shards : " & JSONData.active_primary_shards & "<br />" & _
                                            "active_shards : " & JSONData.active_shards & "<br />" & _
                                            "relocating_shards : " & JSONData.relocating_shards & "<br />" & _
                                            "initializing_shards : " & JSONData.initializing_shards & "<br />" & _
                                            "unassigned_shards : " & JSONData.unassigned_shards & "<br />"

                        ' -- Send the Email
                        SendAlert(ESData, UCase(JSONData.status) & " Detected", msg)
                    ElseIf LCase(JSONData.status) = "green" Then
                        ' -- If you want to send an email when the status is green anyway (so you get some sort of email)
                        '    4th Parameter when calling the exe.
                        If ESData.sendwhengreen = 1 Then
                            Dim msg As String = "URL: " & ESData.URL & "<br />" & _
                                                    "Time: " & System.DateTime.Now & "<br />" & _
                                                    "Status : " & JSONData.status & "<br /><br />"

                            ' -- Send the alert that all is ok and Green and no problems.
                            SendAlert(ESData, UCase(JSONData.status) & " Detected", msg)

                        End If

                    End If
                ElseIf IsNothing(JSONData) = True Then

                    '-- It could not get a response, so send an alert out for none responsive .. response.
                    Dim msg As String = "URL: " & ESData.URL & "<br />" & _
                                            "Time: " & System.DateTime.Now & "<br />" & _
                                            "Status : UNKNOWN <br /><br />" & _
                                            "Reason : Unable to get a response from supplied URL"

                    SendAlert(ESData, "UNKNOWN Response", msg)
                End If
            End If


        Catch ex As Exception

        Finally

        End Try

        End

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        url_value.Text = ""
        status_value.Text = "IDLE"
        Application.DoEvents()

        Timer1.Enabled = True
        Timer1.Start()

    End Sub
End Class
