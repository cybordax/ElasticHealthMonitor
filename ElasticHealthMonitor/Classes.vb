Module Classes

    Class ElasticDetails
        Public Property URL As String = ""           ' The URL of where the elastic DB instance is including the params _cluster/health?pretty=true
        Public Property emailaddress As String = ""  ' Email address of where to send the alerts to.
        Public Property smtpserver As String = ""    ' SMTP server ID to use when sending the email
        Public Property sendwhengreen As Integer = 0 ' Send email alerts even if the status is green
    End Class


    Class JSONResponse
        Public Property cluster_name As String = ""
        Public Property status As String = ""
        Public Property timed_out As Boolean = False
        Public Property number_of_nodes As Integer = 0
        Public Property number_of_data_nodes As Integer = 0
        Public Property active_primary_shards As Integer = 0
        Public Property active_shards As Integer = 0
        Public Property relocating_shards As Integer = 0
        Public Property initializing_shards As Integer = 0
        Public Property unassigned_shards As Integer = 0

    End Class
End Module
